using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebChat.ViewModels;

namespace WebChat.Services
{
    public interface IChatService
    {
        ChatViewModel[] SelectChatsByUserID(string userID);
        ChatViewModel CreateChat(string name, string userID);
        Task<ChatViewModel> SetChatImage(IFormFile file, int chatID);
        void RemoveFromChat(int chatID, string userID);
        string[] ChatUsersID(int chatID);
        Task<ChatViewModel> AddChatMemberAsync(string userID, int chatID);
    }
}
