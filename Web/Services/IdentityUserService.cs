using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Web.Models;
using Web.Models.Register;

namespace Web.Services
{
    public class IdentityUserService : IUser
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly JwtTokenService tokenService;

        public IdentityUserService(UserManager<ApplicationUser> userManager, JwtTokenService tokenService)
        {
            this.userManager = userManager;
            this.tokenService = tokenService;
        }

        public async Task<UserDto> Authenticate(string username, string password)
        {
            var user = await userManager.FindByNameAsync(username);
            if (await userManager.CheckPasswordAsync(user,password))
            {
                return new UserDto
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    Token = await tokenService.GetToken(user, TimeSpan.FromMinutes(5)),
                    Roles = await userManager.GetRolesAsync(user),


                };
            }
            return null;
        }

        public async Task<UserDto> GetUser(ClaimsPrincipal principal)
        {
            var user = await userManager.GetUserAsync(principal);
            return new UserDto
            {
                Id = user.Id,
                UserName = user.UserName,
                Roles = await userManager.GetRolesAsync(user),

            };
        }

        public async Task<UserDto> Register(RegisterData data, ModelStateDictionary modelState)
        {
            var user = new ApplicationUser
            {
                UserName = data.UserName,
                Email = data.Email

                

            };

            var result = await userManager.CreateAsync(user, data.Password);
            if(result.Succeeded)
            {
                await userManager.AddToRolesAsync(user, data.Roles);
                return new UserDto
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    Token = await tokenService.GetToken(user, TimeSpan.FromMinutes(5)),
                    Roles = await userManager.GetRolesAsync(user),

                };
            }
            foreach(var error in result.Errors)
            {
                var errorKey =
                    error.Code.Contains("Password") ? nameof(data.Password) :
                    error.Code.Contains("Email") ? nameof(data.Email) :
                    error.Code.Contains("UserName") ? nameof(data.Password) :
                    "";
                modelState.AddModelError(errorKey, error.Description);
            }
            return null;


            
        }
    }
}
