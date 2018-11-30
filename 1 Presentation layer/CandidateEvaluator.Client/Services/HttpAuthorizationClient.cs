using System;
using System.Collections.Generic;
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
    public class HttpAuthorizationClient
    {
        protected readonly HttpClient Http;
        protected readonly IUriHelper UriHelper;

        public string Code { get; set; }
        public string BearerToken { get; private set; } = string.Empty;
        public string RefreshToken { get; private set; }

        public HttpAuthorizationClient(
            HttpClient http,
            IUriHelper uriHelper)
        {
            Http = http;
            UriHelper = uriHelper;
        }

        public async Task GetBearerToken()
        {
            var nameValueCollection = new[]
            {
                new KeyValuePair<string, string>("code", Code),
                new KeyValuePair<string, string>("redirect_uri", $"{UriHelper.GetBaseUri()}code")
            };

            var response = await Http.PostAsync("/api/auth", new FormUrlEncodedContent(nameValueCollection));
            var authTokens = JsonConvert.DeserializeObject<AuthTokens>(await response.Content.ReadAsStringAsync());
            BearerToken = authTokens.BearerToken;
            RefreshToken = authTokens.RefreshToken;
        }

        public async Task GetRefreshToken()
        {
            var nameValueCollection = new[]
            {
                new KeyValuePair<string, string>("code", Code),
                new KeyValuePair<string, string>("redirect_uri", $"{UriHelper.GetBaseUri()}code"),
                new KeyValuePair<string, string>("refresh_token", RefreshToken)
            };

            var response = await Http.PostAsync("/api/auth", new FormUrlEncodedContent(nameValueCollection));
            var authTokens = JsonConvert.DeserializeObject<AuthTokens>(await response.Content.ReadAsStringAsync());
            BearerToken = authTokens.BearerToken;
            RefreshToken = authTokens.RefreshToken;
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
                Headers = { { "Authorization", $"Bearer {BearerToken}" } },
                Content = content != null 
                    ? new StringContent(Json.Serialize(content), Encoding.UTF8, "application/json") 
                    : null
            };
        }

        private async Task<HttpResponseMessage> RetryOnUnauthorized(Task<HttpResponseMessage> task, int tries = 3)
        {
            if(string.IsNullOrEmpty(BearerToken))
                throw new Exception("Logout");

            var result = await task;
            if (result.StatusCode == HttpStatusCode.Unauthorized && tries > 0)
            {
                await GetRefreshToken();
                Http.DefaultRequestHeaders.Remove("Authorization");
                Http.DefaultRequestHeaders.Add("Authorization", $"Bearer {BearerToken}");
                await RetryOnUnauthorized(task, tries - 1);
            }
            else if(result.StatusCode == HttpStatusCode.Unauthorized)
            {
                Clean();
                throw new Exception("Logout");
            }

            return result;
        }

        public void Clean()
        {
            Code = string.Empty;
            BearerToken = string.Empty;
            RefreshToken = string.Empty;
        }
    }
}
