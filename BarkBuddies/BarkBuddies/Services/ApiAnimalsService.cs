using BarkBuddies.Data.Entities;
using BarkBuddies.Models;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Web;

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

        public async Task<ApiResponse> Get(string id)
        {
            string token = GetToken().Result;
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var jsonOptions = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };

            ApiResponse response = null;
            try
            {
                response = await _client.GetFromJsonAsync<ApiResponse>($"animals/{id}", jsonOptions);
            }
            catch (Exception)
            {
                return response;
            }

            return response;
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

        public async Task<ApiResponse> Get(IEnumerable<Pet> petList, UserProfile user)
        {
            string token = GetToken().Result;
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            string query = GetQuery(petList, user);
            return await _client.GetFromJsonAsync<ApiResponse>($"animals?{query}");
        }

        private string GetQuery(IEnumerable<Pet> petList, UserProfile user)
        {
            var queryString = HttpUtility.ParseQueryString(string.Empty);
            queryString.Add("type", "dog");
            queryString.Add("good_with_dogs", "true");
            if (user != null)
            {
                if (user.ZipCode != null)
                {
                    queryString.Add("location", $"{user.ZipCode}");
                }
                if (user.HasChildren)
                {
                    queryString.Add("good_with_children", "true");
                }
                if (user.HasCats)
                {
                    queryString.Add("good_with_cats", "true");
                }
            }

            var count = 0;
            foreach (var pet in petList)
            {
                queryString.Add($"size[{count}]", GetSize(pet.Size, user.SizeChoice));
                queryString.Add($"age[{count}]", GetAge(pet.Age, user.AgeChoice));
                count++;
            }

            return queryString.ToString();
        }

        private string GetSize(Size size, SizeChoice choice)
        {
            switch (choice)
            {
                case SizeChoice.smaller:
                    if (size == Size.small)
                        return size.ToString("G");
                    return DecreaseSize(size).ToString("G");
                case SizeChoice.same:
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
            int.TryParse(size.ToString("D"), out newSize);
            return (Size)(newSize + 1);
        }

        private Size DecreaseSize(Size size)
        {
            var newSize = 1;
            int.TryParse(size.ToString("D"), out newSize);
            return (Size)(newSize - 1);
        }

        private string GetAge(Age age, AgeChoice choice)
        {
            switch (choice)
            {
                case AgeChoice.younger:
                    if (age == Age.baby)
                        return age.ToString("G");
                    return DecreaseAge(age).ToString("G");
                case AgeChoice.same:
                    return age.ToString("G");
                case AgeChoice.older:
                default:
                    if (age == Age.senior)
                        return age.ToString("G");
                    return IncreaseAge(age).ToString("G");
            }
        }

        private Age IncreaseAge(Age age)
        {
            var newAge = 0;
            int.TryParse(age.ToString("D"), out newAge);
            return (Age)(newAge + 1);
        }

        private Age DecreaseAge(Age age)
        {
            var newAge = 1;
            int.TryParse(age.ToString("D"), out newAge);
            return (Age)(newAge - 1);
        }
    }
}
