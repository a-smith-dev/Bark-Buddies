using BarkBuddies.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using Microsoft.Net.Http.Headers;
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
            string query = GetQuery(petList /*, string choice*/);

            return await _client.GetFromJsonAsync<ApiResponse>($"animals?{query}");
        }

        private string GetQuery(IEnumerable<Pet> petList /*, string choice*/)
        {
            var queryString = HttpUtility.ParseQueryString(string.Empty);
            queryString.Add("type", "dog");
            //queryString.Add("location", "zipCodeFromUserHere"); // zip code from user. Possibly pass in user?

            foreach (var pet in petList)
            {
                queryString.Add("size", pet.Size.ToString("G"));
                queryString.Add("age", pet.Age.ToString("G"));
                //queryString.Add("size", GetSize(pet.Size, choice));
                //queryString.Add("age", GetAge(pet.Age, choice));
            }

            return queryString.ToString();
        }

        private string GetSize(Size size, string choice)
        {
            switch (choice)
            {
                case "smaller":
                    if (size == Size.small)
                        return size.ToString("G");
                    return DecreaseSize(size).ToString("G");
                case "same":
                    return size.ToString("G");
                default:
                    if (size == Size.xlarge)
                        return size.ToString("G");
                    return IncreaseSize(size).ToString("G");
            }
        }

        private Size IncreaseSize(Size size)
        {
            var newSize = 0;
            var sizeInt = size.ToString("D");
            int.TryParse(sizeInt, out newSize);
            return (Size)(newSize + 1);
        }

        private Size DecreaseSize(Size size)
        {
            var newSize = 1;
            var sizeInt = size.ToString("D");
            int.TryParse(sizeInt, out newSize);
            return (Size)(newSize - 1);
        }

        private string GetAge(Age age, string choice)
        {
            switch (choice)
            {
                case "younger":
                    if (age == Age.baby)
                        return age.ToString("G");
                    return DecreaseAge(age).ToString("G");
                case "same":
                    return age.ToString("G");
                default:
                    if (age == Age.senior)
                        return age.ToString("G");
                    return IncreaseAge(age).ToString("G");
            }
        }

        private Age IncreaseAge(Age age)
        {
            var newAge = 0;
            var sizeInt = age.ToString("D");
            int.TryParse(sizeInt, out newAge);
            return (Age)(newAge + 1);
        }

        private Age DecreaseAge(Age age)
        {
            var newAge = 1;
            var sizeInt = age.ToString("D");
            int.TryParse(sizeInt, out newAge);
            return (Age)(newAge - 1);
        }

    }
}
