using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BarkBuddies.Services
{
    interface IAnimalsService
    {
        Task<Animal> Get();
    }
}
