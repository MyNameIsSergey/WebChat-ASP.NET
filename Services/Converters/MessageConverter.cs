using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebChat.Models;
using WebChat.ViewModels;

namespace WebChat.Services.Converters
{
    public class MessageConverter : IMessageConverter
    {
        private readonly UserManager<User> _users;
        public MessageConverter(UserManager<User> users)
        {
            _users = users;
        }
        public Message ToModel(MessageViewModel viewModel)
        {
            Message message = new Message();
            message.Id = viewModel.Id;
            message.UserID = viewModel.SenderID;
            message.When = viewModel.When;
            message.ChatID = viewModel.ChatID;
            message.Content = viewModel.Content;
            return message;
        }

        public async Task<MessageViewModel> ToViewModel(Message message)
        {
            MessageViewModel viewModel = new MessageViewModel();
            var user = await _users.FindByIdAsync(message.UserID);
            if (user == null)
                return null;
            viewModel.ChatID = message.ChatID;
            viewModel.Content = message.Content;
            viewModel.Id = message.Id;
            viewModel.SenderID = message.UserID;
            viewModel.SenderName = user.Name;
            viewModel.When = message.When;
            return viewModel;
        }

    }
}
