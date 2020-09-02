 using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebChat.Models
{
    public class ChatUsers
    {   
        [Key]
        [Column(Order = 1)]
        public int ChatID { get; set; }
        [Key]
        [Column(Order = 2)]
        public string UserID { get; set; }

        public virtual Chat Chat { get; set; }
        public virtual User User { get; set; }
    }
}
