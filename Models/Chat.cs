using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebChat.Models
{
    public class Chat
    {
        public virtual ICollection<ChatUsers> ChatUsers { get; set; } = new List<ChatUsers>();
        public FilesContext Photo { get; set; }
        public User Admin { get; set; }
        
        public int PhotoID { get; set; }
        public string AdminID { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }

    }
}
