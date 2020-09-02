using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebChat.ViewModels
{
    public class MessageViewModel
    {
        public int Id { get; set; }
        public string SenderID { get; set; }
        public string SenderName { get; set; }
        public string Content { get; set; }
        public int ChatID { get; set; }
        public DateTime When { get; set; }
    }
}
