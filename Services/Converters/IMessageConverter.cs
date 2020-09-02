using System.Threading.Tasks;
using WebChat.Models;
using WebChat.ViewModels;

namespace WebChat.Services.Converters
{
    public interface IMessageConverter
    {
        Message ToModel(MessageViewModel veiwModel);
        Task<MessageViewModel> ToViewModel(Message message);
    }
}