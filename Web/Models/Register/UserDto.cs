using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Models.Register
{
    public class UserDto
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Token { get; set; }
        public IList<string> Roles { get; internal set; }
    }
}
