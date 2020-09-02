using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebChat.Models;

namespace WebChat.ViewModels
{
    public class UserViewModel
    {
        public string Id { get; set; }
        public ImageData Photo { get; set; }
        public string UserName { get; set; }
    }
}
