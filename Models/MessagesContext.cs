using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace WebChat.Models
{
    public class MessagesContext : DbContext
    {
        public DbSet<Message> Messages { get; set; }
        public MessagesContext(DbContextOptions<MessagesContext> options)
: base(options)
        {
            Database.EnsureCreated();
        }
    }
}
