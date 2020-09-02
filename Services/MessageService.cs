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
    public class MessageService : IMessageService
    {
        IMessageConverter _messageConverter;
        MessagesContext _messages;
        public MessageService(MessagesContext messages, UserManager<User> users)
        {
            _messageConverter = new MessageConverter(users);
            _messages = messages;
        }

        public async Task<MessageViewModel> SendAsync(string text, int chatID, string senderID)
        {
            Message m = new Message { ChatID = chatID, Content = text, UserID = senderID, When = DateTime.Now };
            _messages.Messages.Add(m);
            _messages.SaveChanges();
            return await _messageConverter.ToViewModel(m);
        }
        public async Task<MessageViewModel[]> SelectMessagesByChatIDAsync(int chatID)
        {
            var m = _messages.Messages.Where(m => m.ChatID == chatID);
            MessageViewModel[] messages = new MessageViewModel[m.Count()];
            int i = 0;
            foreach (var msg in m)
            {
                MessageViewModel message = await _messageConverter.ToViewModel(msg);
                messages[i++] = message;
            }
            return messages;
        }
    }   
}
 