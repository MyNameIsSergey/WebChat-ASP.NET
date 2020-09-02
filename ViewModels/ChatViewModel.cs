using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WebChat.Models;

namespace WebChat.ViewModels
{
    public class ChatViewModel
    {
        public ImageData Image { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }

        public ChatViewModel() { }

    }
}
