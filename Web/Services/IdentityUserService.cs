using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Models;
using Web.Models.Register;

namespace Web.Services
{
    public class IdentityUserService : IUser
    {
        private readonly UserManager<ApplicationUser> userManager;
        public async Task<ApplicationUser> Register(RegisterData data)
        {
            var user = new ApplicationUser
            {
                UserName = data.UserName,
                Email = data.Email

            };

            await userManager.CreateAsync(user, data.Password);
            return user;
            
        }
    }
}
