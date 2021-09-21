using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ithome_2021_API.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public bool Verified { get; set; }
    }
}
