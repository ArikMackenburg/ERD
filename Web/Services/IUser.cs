using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Models;
using Web.Models.Register;

namespace Web.Services
{
    public interface IUser
    {
        
        Task<ApplicationUser> Register(RegisterData data);
    }
}
