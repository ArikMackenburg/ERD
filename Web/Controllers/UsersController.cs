using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Web.Models;
using Web.Models.Register;
using Web.Services;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUser userService;
        public UsersController(IUser userService)
        {
            this.userService = userService;
        }
        [HttpPost("Register")]
        public async Task<ActionResult<ApplicationUser>> Register(RegisterData data)
        {
            ApplicationUser user = await userService.Register(data);
            return user;
        }
    }
}
