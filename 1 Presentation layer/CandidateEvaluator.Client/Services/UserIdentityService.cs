using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Blazor.Extensions.Storage;
using CandidateEvaluator.Common.Responses.Auth;
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
        private readonly LocalStorage _localStorage;
        private bool _initialized;

        private const string AuthTokensKey = "AuthTokens";
        public AuthResponse AuthResponse { get; set; }

        public UserIdentityService(
            HttpClient http,
            IUriHelper uriHelper,
            LocalStorage localStorage)
        {
            Http = http;
            UriHelper = uriHelper;
            _localStorage = localStorage;
        }

        public async Task<bool> IsUserLogged()
        {
            if (!_initialized)
            {
                var authTokens = await _localStorage.GetItem<AuthResponse>(AuthTokensKey);
                AuthResponse = authTokens;
                _initialized = true;
            }

            return AuthResponse != null && 
                   !string.IsNullOrWhiteSpace(AuthResponse.BearerToken) &&
                   !string.IsNullOrWhiteSpace(AuthResponse.RefreshToken) &&
                   !string.IsNullOrWhiteSpace(AuthResponse.Username) &&
                   !string.IsNullOrWhiteSpace(AuthResponse.Code);
        }

        public async Task Login()
        {
            var code = Microsoft.AspNetCore.WebUtilities.QueryHelpers.ParseQuery(
                new Uri(UriHelper.GetAbsoluteUri()).Query)
                .TryGetValue("code", out var c) ? c.First() : "";
            var nameValueCollection = new[]
            {
                new KeyValuePair<string, string>("code", code),
                new KeyValuePair<string, string>("redirect_uri", $"{UriHelper.GetBaseUri()}code")
            };

            var response = await Http.PostAsync("/api/auth", new FormUrlEncodedContent(nameValueCollection));
            var authTokens = JsonConvert.DeserializeObject<AuthResponse>(await response.Content.ReadAsStringAsync());
            await _localStorage.SetItem(AuthTokensKey, authTokens);
            AuthResponse = authTokens;
        }

        public void Logout()
        {
            AuthResponse = null;
        }

        private async Task GetRefreshToken()
        {
            var nameValueCollection = new[]
            {
                new KeyValuePair<string, string>("code", AuthResponse.Code),
                new KeyValuePair<string, string>("redirect_uri", $"{UriHelper.GetBaseUri()}code"),
                new KeyValuePair<string, string>("refresh_token", AuthResponse.RefreshToken)
            };

            var response = await Http.PostAsync("/api/auth", new FormUrlEncodedContent(nameValueCollection));
            AuthResponse = JsonConvert.DeserializeObject<AuthResponse>(await response.Content.ReadAsStringAsync());
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
                Headers = { { "Authorization", $"Bearer {AuthResponse.BearerToken}" } },
                Content = content != null 
                    ? new StringContent(Json.Serialize(content), Encoding.UTF8, "application/json") 
                    : null
            };
        }

        private async Task<HttpResponseMessage> RetryOnUnauthorized(Task<HttpResponseMessage> task, int tries = 3)
        {
            if(string.IsNullOrEmpty(AuthResponse.BearerToken))
                throw new Exception("Logout");

            var result = await task;
            if (result.StatusCode == HttpStatusCode.Unauthorized && tries > 0)
            {
                await GetRefreshToken();
                Http.DefaultRequestHeaders.Remove("Authorization");
                Http.DefaultRequestHeaders.Add("Authorization", $"Bearer {AuthResponse.BearerToken}");
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
