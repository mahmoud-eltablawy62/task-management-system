using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementSystem.core.Entities;
using TaskManagementSystem.Core.Entities.Identity;


namespace TaskManagementSystem.core.Services.Contract
{
    public interface IAuthService 
    {

        Task<string> CreateTokenAsync(User userApp, UserManager<User> userManager);


    }
}
