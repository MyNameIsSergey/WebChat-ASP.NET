using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebChat.Models;
using WebChat.ViewModels;

namespace WebChat.Services.Converters
{
    public class UserConverter : IUserConverter
    {
        FilesContext _files;
        public UserConverter(FilesContext files)
        {
            _files = files;
        }
        public User ToModel(UserViewModel viewModel)
        {
            throw new NotImplementedException();
        }

        public UserViewModel ToViewModel(User user)
        {
            FileModel m = _files.Files.FirstOrDefault(f => f.Id == user.PhotoID);
            return new UserViewModel
            {
                Id = user.Id,
                Photo = new ImageData
                {
                    ImageHeaders = "data:image/" + m.ContentType + ";base64,",
                    ImageBinary = m.Data
                },
                UserName = user.Name
            };
        }
    }
}
