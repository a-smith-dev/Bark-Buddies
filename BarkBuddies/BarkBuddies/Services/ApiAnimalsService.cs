using BarkBuddies.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace BarkBuddies.Services
{
    public class ApiAnimalsService : IAnimalsService
    {
        private readonly HttpContext _context;
        private readonly HttpClient _client;
        private readonly IMemoryCache _cache;
        private readonly PetFinderApiCredentials _petfinderDetails;
        private const string CacheKey = "TokenCacheKey";

        public ApiAnimalsService(HttpContext context, HttpClient client, IMemoryCache cache, IOptions<PetFinderApiCredentials> petfinderDetails)
        {
            _context = context;
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

        public async Task<ApiResponse> Get(IEnumerable<Pet> petList, string choice)
        {
            string token = GetToken().Result;
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            string query = GetQuery(petList, choice);

            return await _client.GetFromJsonAsync<ApiResponse>($"animals?{query}");
        }

        private string GetQuery(IEnumerable<Pet> petList, string choice)
        {
            var queryString = HttpUtility.ParseQueryString(string.Empty);
            queryString.Add("type", "dog");
            queryString.Add("location", "zipCodeFromUserHere"); // zip code from user. Possibly pass in user?

            foreach (var pet in petList)
            {
                queryString.Add("size", GetSize(pet.Size, choice));
            }

            return queryString.ToString();
        }

        private string GetSize(string size, string choice) // GetSize(Enum size, string choice)
        {
            switch (choice)
            {
                case "smaller":
                    if (size == "small")
                        return size;
                    return "size minus one"; // return (size - 1).ToString();
                case "same":
                    return size;
                default:
                    if (size == "xlarge")
                        return size;
                    return "size plus one";
            }
        }

        //private string GetAge(Enum age, string choice)
        //{

        //}

    }
}
