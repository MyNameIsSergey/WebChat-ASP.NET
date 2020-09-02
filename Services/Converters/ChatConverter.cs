using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WebChat.Models;
using WebChat.ViewModels;

namespace WebChat.Services.Converters
{
    public class ChatConverter : IChatConverter
    {
        FilesContext _files;
        public ChatConverter(FilesContext files)
        { 
            _files = files;
        }
        public Chat ToModel(ChatViewModel viewModel)
        {
            throw new NotImplementedException();
        }

        public ChatViewModel ToViewModel(Chat chat)
        {
            ChatViewModel viewModel = new ChatViewModel();
            viewModel.Name = chat.Name;
            viewModel.Id = chat.Id;
            FileModel file = _files.Files.FirstOrDefault(f => f.Id == chat.PhotoID);
            viewModel.Image = new ImageData
            { ImageBinary = file.Data, ImageHeaders = "data:image/" + Path.GetExtension(file.Name).Replace(".", "") + ";base64," };
            return viewModel;
        }
    }
}
