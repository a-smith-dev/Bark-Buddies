using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace BarkBuddies.Services
{
    public class ApiAnimalsService : IAnimalsService
    {
        private readonly HttpClient _client;

        public ApiAnimalsService(HttpClient client)
        {
            _client = client;
        }

        public async Task<Animal> Get()
        {
            return await _client.GetFromJsonAsync<Animal>($""); //need to add api extension
        }
    }
}
