using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebChat.ViewModels;

namespace WebChat.Services
{
    public interface IMessageService
    {
        Task<MessageViewModel> SendAsync(string text, int chatID, string senderID);
        Task<MessageViewModel[]> SelectMessagesByChatIDAsync(int chatID);

    }
}
