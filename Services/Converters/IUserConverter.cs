using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebChat.Models;
using WebChat.ViewModels;

namespace WebChat.Services.Converters
{
    public interface IUserConverter
    {
        User ToModel(UserViewModel viewModel);
        UserViewModel ToViewModel(User user);
    }
}
