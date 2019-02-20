using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;
using System.Threading.Tasks;
using CandidateEvaluator.Common.Dtos;
using CandidateEvaluator.Contract.Configuration;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using AuthDto = CandidateEvaluator.Common.Dtos.AuthDto;

namespace CandidateEvaluator.Server.Controllers
{
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly HttpClient _client;
        private readonly AadOptions _options;

        public AuthController(HttpClient client, AadOptions options)
        {
            _client = client;
            _options = options;
        }

        [HttpPost]
        public async Task<IActionResult> GetToken(
            [FromForm] string code, 
            [FromForm(Name = "redirect_uri")] string redirectUri, 
            [FromForm(Name = "refresh_token")] string refreshToken)
        {
            KeyValuePair<string, string>[] formCollection;

            if (refreshToken == null)
            {
                formCollection = new[]
                {
                    new KeyValuePair<string, string>("grant_type", "authorization_code"),
                    new KeyValuePair<string, string>("client_id", _options.ClientId),
                    new KeyValuePair<string, string>("resource", _options.ClientId),
                    new KeyValuePair<string, string>("code", code),
                    new KeyValuePair<string, string>("redirect_uri", redirectUri)
                };
            }
            else
            {
                formCollection = new[]
                {
                    new KeyValuePair<string, string>("grant_type", "refresh_token"),
                    new KeyValuePair<string, string>("client_id", _options.ClientId),
                    new KeyValuePair<string, string>("refresh_token", refreshToken)
                };
            }

            var url = $"https://login.microsoftonline.com/{_options.TenantId}/oauth2/token";
            var result = await _client.PostAsync(url, new FormUrlEncodedContent(formCollection));
            var parsed = JToken.Parse(await result.Content.ReadAsStringAsync());
            var tokens = new AuthDto
            {
                BearerToken = parsed["access_token"].ToString(),
                RefreshToken = parsed["refresh_token"].ToString(),
                Username = new JwtSecurityTokenHandler()
                    .ReadJwtToken(parsed["access_token"].ToString())
                    .Payload["name"].ToString(),
                Code = code
            };
            
            return Ok(tokens);
        }

        [HttpGet]
        public IActionResult GetOptions()
        {
            return Ok(new AadOptionsDto
            {
                ClientId = _options.ClientId,
                TenantId = _options.TenantId
            });
        }
    }
}
