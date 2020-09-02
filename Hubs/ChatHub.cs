using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Linq;
using System.Threading.Tasks;
using WebChat.ViewModels;
using Microsoft.AspNetCore.Authorization;
using WebChat.Services;

namespace WebChat.Hubs
{
    [Authorize]
    public class ChatHub : Hub
    {
        IChatService _chatService;
        IMessageService _messageService;
        IUserService _userService;
        public ChatHub(IChatService chatService, IMessageService messageService, IUserService userService)
        {
            _userService = userService;
            _chatService = chatService;
            _messageService = messageService;
        }
        public async Task Send(string message, int chatID)
        {
            await Clients.
                Group(chatID.ToString()).
                SendAsync("Send", await _messageService.SendAsync(message, chatID, UserId()));
        }
        public async Task GetChatList()
        {
            await Clients.
                Caller.
                SendAsync("UpdateChatList", _chatService.SelectChatsByUserID(UserId()));
        }
        public async Task GetChatUsers(int chatID)
        {
            await Clients.Caller.
                SendAsync("UpdateChatUsers", chatID, _chatService.ChatUsersID(chatID));
        }
        public async Task GetMessages(int chatID)
        {
            await Clients.
                Caller.
                SendAsync("UpdateMessages", await _messageService.SelectMessagesByChatIDAsync(chatID), chatID);
        }
        public async Task GetUserID()
        {
            await Clients.Caller.SendAsync("ChangeID", UserId());
        }
        public async Task CreateChat(string name)
        {
            ChatViewModel chat = _chatService.CreateChat(name, UserId());
            await Groups.AddToGroupAsync(Context.ConnectionId, chat.Id.ToString());
            await Clients.Caller.SendAsync("AddChat", chat);
        }
        public async Task SetChatImage(IFormFile file, int chatID)
        {
            await Clients.Caller.SendAsync("UpdateChat", await _chatService.SetChatImage(file, chatID));
        }
        public async Task SetUserImage(object file)
        {
            await Clients.Caller.SendAsync("UpdateUserInfo", await _userService.SetUserImage((IFormFile)file, UserId()));
        }
        public async Task GetUserInfo(string id)
        {
            if(!string.IsNullOrEmpty(id))
                await Clients.Caller.SendAsync("UserInfo", await _userService.GetUserInfoAsync(id));
        }
        public async Task LeaveChat(int id)
        {
            _chatService.RemoveFromChat(id, UserId());
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, id.ToString());
            await Clients.Caller.SendAsync("DeleteChat", id);
        }
        public async Task AddChatMember(string id, int chatID)
        {
            ChatViewModel chat;
            if ((chat = await _chatService.AddChatMemberAsync(id, chatID)) != null)
            {
                await Clients.User(id)?.SendAsync("AddChat", chat);
            }
        }
        #region Connection
        public async override Task OnConnectedAsync()
        {
            await base.OnConnectedAsync();

            var chats = _chatService.SelectChatsByUserID(UserId());
            foreach(var c in chats)
            {
                await Groups.AddToGroupAsync(Context.ConnectionId, c.Id.ToString());
            }
            await Clients.Caller.SendAsync("UpdateChatList", chats);
            await Clients.Caller.SendAsync("ChangeID", UserId());

        }
        public override Task OnDisconnectedAsync(Exception exception)
        {
            return base.OnDisconnectedAsync(exception);
        }
        #endregion
        private string UserId()
        {
            return Context.User.Claims.First().Value;
        }

    }
}
