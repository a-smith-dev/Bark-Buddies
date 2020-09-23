using BarkBuddies.Data.Entities;
using BarkBuddies.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BarkBuddies.Services
{
    public interface IAnimalsService
    {
        Task<ApiResponse> Get(string id);
        Task<ApiResponse> Get(IEnumerable<Pet> petList, UserProfile user);
    }
}
