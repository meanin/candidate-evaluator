using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using CandidateEvaluator.Contract.Models;
using Microsoft.AspNetCore.Blazor;
using Microsoft.AspNetCore.Blazor.Services;
using Microsoft.JSInterop;
using Newtonsoft.Json;

namespace CandidateEvaluator.Client.Services
{
    public class UserIdentityService
    {
        protected readonly HttpClient Http;
        protected readonly IUriHelper UriHelper;
        private string _bearerToken = string.Empty;
        private string _refreshToken = string.Empty;
        private string _code = string.Empty;

        public User User { get; set; }

        public UserIdentityService(
            HttpClient http,
            IUriHelper uriHelper)
        {
            Http = http;
            UriHelper = uriHelper;
        }

        public async Task Login()
        {
            _code = Microsoft.AspNetCore.WebUtilities.QueryHelpers.ParseQuery(
                new Uri(UriHelper.GetAbsoluteUri()).Query)
                .TryGetValue("code", out var c) ? c.First() : "";
            var nameValueCollection = new[]
            {
                new KeyValuePair<string, string>("code", _code),
                new KeyValuePair<string, string>("redirect_uri", $"{UriHelper.GetBaseUri()}code")
            };

            var response = await Http.PostAsync("/api/auth", new FormUrlEncodedContent(nameValueCollection));
            var authTokens = JsonConvert.DeserializeObject<AuthTokens>(await response.Content.ReadAsStringAsync());
            _bearerToken = authTokens.BearerToken;
            _refreshToken = authTokens.RefreshToken;
            
            var base64EncodedBytes = Convert.FromBase64String(_bearerToken.Split('.')[1]);
            User = JsonConvert.DeserializeObject<User>(Encoding.UTF8.GetString(base64EncodedBytes));
        }

        public void Logout()
        {
            _code = string.Empty;
            _bearerToken = string.Empty;
            _refreshToken = string.Empty;
        }

        private async Task GetRefreshToken()
        {
            var nameValueCollection = new[]
            {
                new KeyValuePair<string, string>("code", _code),
                new KeyValuePair<string, string>("redirect_uri", $"{UriHelper.GetBaseUri()}code"),
                new KeyValuePair<string, string>("refresh_token", _refreshToken)
            };

            var response = await Http.PostAsync("/api/auth", new FormUrlEncodedContent(nameValueCollection));
            var authTokens = JsonConvert.DeserializeObject<AuthTokens>(await response.Content.ReadAsStringAsync());
            _bearerToken = authTokens.BearerToken;
            _refreshToken = authTokens.RefreshToken;
        }

        public Task<HttpResponseMessage> AuthorizedDeleteAsync(string requestUri)
        {
            return RetryOnUnauthorized(
                Http.SendAsync(CreateRequestMessage(HttpMethod.Delete, requestUri)));
        }

        public Task<HttpResponseMessage> AuthorizedGetAsync(string requestUri)
        {
            return RetryOnUnauthorized(Http.SendAsync(CreateRequestMessage(HttpMethod.Get, requestUri)));
        }

        public async Task<T> AuthorizedGetJsonAsync<T>(string requestUri)
        {
            var response = await RetryOnUnauthorized(AuthorizedGetAsync(requestUri));
            var text = await response.Content.ReadAsStringAsync();
            return Json.Deserialize<T>(text);
        }

        public Task<HttpResponseMessage> AuthorizedPostAsync(string requestUri, object content)
        {
            return RetryOnUnauthorized(Http.SendAsync(CreateRequestMessage(HttpMethod.Post, requestUri, content)));
        }

        public Task<T> GetJsonAsync<T>(string requestUri)
        {
            return Http.GetJsonAsync<T>(requestUri);
        }

        public async Task<T> AuthorizedPostJsonAsync<T>(string requestUri, object content)
        {
            var response = await AuthorizedPostAsync(requestUri, content);
            var text = await response.Content.ReadAsStringAsync();
            return Json.Deserialize<T>(text);
        }

        private HttpRequestMessage CreateRequestMessage(HttpMethod httpMethod, string requestUri, object content = null)
        {
            return new HttpRequestMessage(httpMethod, requestUri)
            {
                Headers = { { "Authorization", $"Bearer {_bearerToken}" } },
                Content = content != null 
                    ? new StringContent(Json.Serialize(content), Encoding.UTF8, "application/json") 
                    : null
            };
        }

        private async Task<HttpResponseMessage> RetryOnUnauthorized(Task<HttpResponseMessage> task, int tries = 3)
        {
            if(string.IsNullOrEmpty(_bearerToken))
                throw new Exception("Logout");

            var result = await task;
            if (result.StatusCode == HttpStatusCode.Unauthorized && tries > 0)
            {
                await GetRefreshToken();
                Http.DefaultRequestHeaders.Remove("Authorization");
                Http.DefaultRequestHeaders.Add("Authorization", $"Bearer {_bearerToken}");
                await RetryOnUnauthorized(task, tries - 1);
            }
            else if(result.StatusCode == HttpStatusCode.Unauthorized)
            {
                Logout();
                throw new Exception("Logout");
            }

            return result;
        }
    }
}
