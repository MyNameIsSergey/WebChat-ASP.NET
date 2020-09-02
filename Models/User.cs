using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebChat.Models
{
    public class User : IdentityUser
    {
        public int PhotoID { get; set; } = 1;
        public string Name { get; set; }

        public virtual ICollection<ChatUsers> ChatUsers { get; set; } = new List<ChatUsers>();
    }
}
