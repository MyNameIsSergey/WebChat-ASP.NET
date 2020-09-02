using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace WebChat.Models
{
    public class ApplicationContext : IdentityDbContext<User>
    {
        public DbSet<Chat> Chats { get; set; }
        public DbSet<ChatUsers> ChatUsers { get; set; }
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
    : base(options)
        {
            Database.EnsureCreated();
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<ChatUsers>().HasKey(u => new { u.UserID, u.ChatID });
            base.OnModelCreating(builder);
        }
    }
}
