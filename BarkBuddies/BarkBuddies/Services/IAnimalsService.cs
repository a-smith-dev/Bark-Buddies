using BarkBuddies.Data.Entities;
using BarkBuddies.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Threading.Tasks;

namespace BarkBuddies.Services
{
    public interface IAnimalsService
    {
        Task<ApiResponse> Get();
        Task<ApiResponse> Get(IEnumerable<Pet> petList, string choice);
        Task<IActionResult> Create(Animal animal);

    }
}
