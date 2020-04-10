using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuizForms.Data.Models.Contact;
using QuizForms.Data.Repositories.Abstract;

namespace QuizForms.Web.Controllers.Admin
{
    [Authorize]
    [Route("admin/contact")]
    public class ManageContactController : Controller
    {
        private readonly IContactMessagesRepository _messageRepostitory;

        public ManageContactController(IContactMessagesRepository messageRepostitory)
        {
            _messageRepostitory = messageRepostitory;
        }

        [HttpGet]
        public IActionResult Index()
        {
            List<ContactMessage> messages = _messageRepostitory.GetAll();
            return View(messages);
        }
    }
}