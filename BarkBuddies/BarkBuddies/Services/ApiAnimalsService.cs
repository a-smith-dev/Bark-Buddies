using BarkBuddies.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Specialized;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace BarkBuddies.Services
{
    public class ApiAnimalsService : IAnimalsService
    {
        private readonly HttpClient _client;
        private readonly IMemoryCache _cache;
        private readonly PetFinderApiCredentials _petfinderDetails;
        private const string CacheKey = "TokenCacheKey";

        public ApiAnimalsService(HttpClient client, IMemoryCache cache, IOptions<PetFinderApiCredentials> petfinderDetails)
        {
            _client = client;
            _cache = cache;
            _petfinderDetails = petfinderDetails.Value;
        }

        public async Task<ApiResponse> Get()
        {
            string token = GetToken().Result;

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            return await _client.GetFromJsonAsync<ApiResponse>($"animals"); //append the user's query to this to narrow the search
        }

        private async Task<string> GetToken()
        {
            var token = await _cache.GetOrCreateAsync(CacheKey, async entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30);
                var content = new StringContent(JsonConvert.SerializeObject(
                   new
                   {
                       client_id = _petfinderDetails.ApiKey,
                       client_secret = _petfinderDetails.ApiSecret,
                       grant_type = "client_credentials"
                   }), Encoding.UTF8, "application/json");

                var response = await _client.PostAsync("oauth2/token", content);
                return await response.Content.ReadAsStringAsync();

            });
            token = token.Replace("\"", "")
                         .Replace("token_type:Bearer,expires_in:3600,access_token:", "")
                         .Replace("{", string.Empty)
                         .Replace("}", string.Empty);

            return token;
        }

        public Task<IActionResult> Create(Animal animal)
        {
            throw new NotImplementedException();
        
        }

        public Task<ApiResponse> Get(NameValueCollection nvc)
        {
            throw new NotImplementedException();
        }
    }
}
