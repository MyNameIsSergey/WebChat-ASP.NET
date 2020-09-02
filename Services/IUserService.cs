using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebChat.ViewModels;

namespace WebChat.Services
{
    public interface IUserService
    {
        Task<UserViewModel> GetUserInfoAsync(string id);
        Task<UserViewModel> SetUserImage(IFormFile file, string userID);
    }
}
