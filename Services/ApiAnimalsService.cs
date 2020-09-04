﻿using Microsoft.Extensions.Caching.Memory;
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
        private const string CacheKey = "TokenCacheKey";

        public ApiAnimalsService(HttpClient client, IMemoryCache cache)
        {
            _client = client;
            _cache = cache;
        }

        public async Task<Animal> Get()
        {
            var token = await _cache.GetOrCreateAsync(CacheKey, async entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30);
                var content = new StringContent(JsonConvert.SerializeObject(
                   new
                   {
                       client_id = "",
                       client_secret = "",
                       grant_type = "client_credentials"
                   }), Encoding.UTF8, "application/json");

                var response = await _client.PostAsync("oauth2/token", content);
                return await response.Content.ReadAsStringAsync();
            });

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            return await _client.GetFromJsonAsync<Animal>($""); //need to add api extension
        }
    }
}
