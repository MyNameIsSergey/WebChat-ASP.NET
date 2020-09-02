using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace WebChat.Models
{
    public class FilesContext : DbContext
    {
        public DbSet<FileModel> Files { get; set; }
        public FilesContext(DbContextOptions<FilesContext> options) : base(options) { Database.EnsureCreated(); }
        public FileModel SaveFile(IFormFile file)
        {
            if (file == null)
                return null;
            MemoryStream stream = new MemoryStream();
            file.OpenReadStream().CopyTo(stream);
            FileModel model = new FileModel()
            { Name = file.FileName, ContentType = file.ContentType, Data = stream.ToArray() };
            Files.Add(model);
            SaveChanges();
            return model;
        }
    }
}
