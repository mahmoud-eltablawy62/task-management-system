using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementSystem.core.Entities;
using TaskManagementSystem.Core.Entities.Identity;

namespace TaskManagementSystem.Repository.Identity
{
    public static class UserContextSeed
    {
        public static async Task UserSeedAsync(UserManager<User> _User)
        {
            if (_User.Users.Count() == 0)
            {
                var user = new User()
                {
                    Name = "Mahmoud",
                    Email = "mahmoudeltablawy702@gmail.com",
                    UserName = "Eltablawy",
                    PhoneNumber = "01554847412"
                };
                await _User.CreateAsync(user, "mahM2#");
            }

        }
    }
}



