using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebChat.Services.Converters;
using WebChat.Models;
using WebChat.ViewModels;
using Microsoft.AspNetCore.Http;

namespace WebChat.Services
{
    public class UserService : IUserService
    {
        FilesContext _files;
        UserManager<User> _users;
        UserConverter _converter;

        public UserService(UserManager<User> userManager, FilesContext files)
        {
            _files = files;
            _users = userManager;
            _converter = new UserConverter(_files);
        }

        public async Task<UserViewModel> GetUserInfoAsync(string id)
        {
            User user = await _users.FindByIdAsync(id);
            UserViewModel viewModel = _converter.ToViewModel(user);
            return viewModel;
        }

        public async Task<UserViewModel> SetUserImage(IFormFile file, string userID)
        {
            User user = await _users.FindByIdAsync(userID);
            if(user.PhotoID != 1)
            {
                _files.Files.Remove(_files.Files.FirstOrDefault(f => f.Id == user.PhotoID));
                _files.SaveChanges();
            }
            var model = _files.SaveFile(file);
            user.PhotoID = model.Id;
            await _users.UpdateAsync(user);
            return new UserViewModel 
            { 
                Id = user.Id, 
                Photo = new ImageData 
                { 
                    ImageBinary = model.Data, 
                    ImageHeaders = model.ContentType 
                }, 
                UserName = user.Name 
            };
        }
    }
}
