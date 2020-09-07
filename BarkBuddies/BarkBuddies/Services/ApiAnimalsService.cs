using BarkBuddies.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
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
            string token = await _cache.GetOrCreateAsync(CacheKey, async entry =>
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
            token = token.Replace("\"", "");
            token = token.Replace("token_type:Bearer,expires_in:3600,access_token:", "");
            token = token.Replace("{", string.Empty).Replace("}", string.Empty);
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            return await _client.GetFromJsonAsync<ApiResponse>($"animals");
        }
        public Task<IActionResult> Create(Animal animal)
        {
            throw new NotImplementedException();
        
        }
    }
}
