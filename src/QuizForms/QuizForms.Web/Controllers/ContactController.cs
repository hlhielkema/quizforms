using Microsoft.AspNetCore.Mvc;
using QuizForms.Data.Repositories.Abstract;
using QuizForms.Web.Models;

namespace QuizForms.Web.Controllers
{
    [Route("contact")]
    public class ContactController : Controller
    {
        private readonly IContactMessagesRepository _messageRepostitory;

        public ContactController(IContactMessagesRepository messageRepostitory)
        {
            _messageRepostitory = messageRepostitory;
        }

        [Route("")]
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [Route("")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(ContactFormModel model)
        {
            if (ModelState.IsValid)
            {                  
                // Create the contact message
                _messageRepostitory.Create(model.EmailAddress, model.Message);

                // Return to the "message sent" page
                return RedirectToAction("Sent");
            }
            else
            {
                // Invalid model state
                return View(model);
            }
        }

        [Route("sent")]
        public IActionResult Sent()
        {
            return View();
        }
    }
}