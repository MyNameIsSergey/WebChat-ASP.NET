using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebChat.Services.Converters;
using WebChat.Models;
using WebChat.ViewModels;

namespace WebChat.Services
{
    public class ChatService : IChatService
    {
        IChatConverter _chatConverter;
        ApplicationContext _application;
        FilesContext _files;
        UserManager<User> _users;
        public ChatService(ApplicationContext context, FilesContext files, UserManager<User> users)
        {
            _users = users;
            _files = files;
            _application = context;
            _chatConverter = new ChatConverter(_files);
        }

        public ChatViewModel[] SelectChatsByUserID(string userID)
        {
            var chatUsers = _application.ChatUsers.Where(u => u.UserID == userID).ToArray();
            var list = new ChatViewModel[chatUsers.Length];
            int i = 0;

            foreach (var c in chatUsers)
            {
                int chatId = c.ChatID;
                Chat chat = _application.Chats.FirstOrDefault(chat => chat.Id == chatId);
                list[i++] = _chatConverter.ToViewModel(chat);
            }
            return list;
        }
        public ChatViewModel CreateChat(string name, string userID)
        {
            Chat chat = new Chat
            { Name = name, AdminID = userID, PhotoID = 1 };
            _application.Chats.Add(chat);
            _application.SaveChanges();
            ChatUsers chatUsers = new ChatUsers
            { ChatID = chat.Id, UserID = userID };
            _application.ChatUsers.Add(chatUsers);
            _application.SaveChanges();
            return _chatConverter.ToViewModel(chat);
        }
        public async Task<ChatViewModel> SetChatImage(IFormFile file, int chatID)
        {
            var model = _files.SaveFile(file);
            Chat chat = _application.Chats.FirstOrDefault(chat => chat.Id == chatID);
            if (chat == null)
                return null;
            chat.PhotoID = model.Id;
            _application.Update(chat);
            await _application.SaveChangesAsync();
            return _chatConverter.ToViewModel(chat);
        }
        public void RemoveFromChat(int chatID, string userID)
        {
            _application.ChatUsers.Remove(new ChatUsers { ChatID = chatID, UserID = userID });
            var chat = _application.Chats.FirstOrDefault(c => c.Id == chatID);
            if (chat.ChatUsers.Count() == 0)
            {
                _application.Chats.Remove(chat);
            }
            _application.SaveChanges();
        }

        public string[] ChatUsersID(int chatID)
        {
            var chatUsers = _application.ChatUsers.Where(c => c.ChatID == chatID);
            string[] idArray = new string[chatUsers.Count()];
            IEnumerator<ChatUsers> c = chatUsers.GetEnumerator();
            for (int i = 0; c.MoveNext(); i++)
            {
                idArray[i] = c.Current.UserID;
            }
            return idArray;
        }

        public async Task<ChatViewModel> AddChatMemberAsync(string userID, int chatID)
        {
            Chat chat = _application.Chats.FirstOrDefault(c => c.Id == chatID);
            if (await _users.FindByIdAsync(userID) == null || chat == null)
            {
                return null;
            }
            _application.ChatUsers.Add(new ChatUsers() { ChatID = chatID, UserID = userID });
            _application.SaveChanges();

            return _chatConverter.ToViewModel(chat);
        }
    }
}
