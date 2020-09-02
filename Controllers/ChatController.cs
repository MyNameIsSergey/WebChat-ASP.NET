using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WebChat.Hubs;
using WebChat.Models;
using WebChat.Services;
using WebChat.ViewModels;

namespace WebChat.Controllers
{
    [Authorize]
    public class ChatController : Controller
    {
        IHubContext<ChatHub> _context;
        FilesContext _files;
        UserManager<User> _users;
        public ChatController(IHubContext<ChatHub> context, FilesContext files, UserManager<User> users)
        {
            _users = users;
            _files = files;
            _context = context;
        }        
        [HttpGet]
        [Authorize]
        public IActionResult Index()
        {   
            return View();
        }

        [HttpGet]
        [Authorize]
        public IActionResult Logout()
        {
            return RedirectToAction("~/Account/Logout");
        }

        [HttpPost]
        public async Task<JsonResult> UpdateUserPhoto([FromForm]IFormFile photo)
        {
            //var photo = Request.Form.Files.FirstOrDefault();
           
            if (photo != null)
            {
                string id = ControllerContext.HttpContext.User.Claims.ElementAt(0).Value;
                return Json(await UpdatePhotoAsync(photo, id));
                
            }
            return Json(null);
        }

        private async Task<UserViewModel> UpdatePhotoAsync(IFormFile file, string userID)
        {
            User user = await _users.FindByIdAsync(userID);
            if (user.PhotoID != 1)
            {
                _files.Files.Remove(_files.Files.FirstOrDefault(f => f.Id == user.PhotoID));
                _files.SaveChanges();
            }
            var model = _files.SaveFile(file);
            user.PhotoID = model.Id;
            await _users.UpdateAsync(user);
            return new UserViewModel
            {
                Id = user.Id,
                Photo = new ImageData
                {
                    ImageBinary = model.Data,
                    ImageHeaders = "data:image/" + model.ContentType + ";base64,"
                },
                UserName = user.Name
            };
        }
    }
}
