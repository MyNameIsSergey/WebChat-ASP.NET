using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebChat.Models
{
    public class Message
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public string UserID { get; set; }
        public int ChatID { get; set; }
        public DateTime When { get; set; }
    }
}
