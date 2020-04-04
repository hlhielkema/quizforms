using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuizForms.Data.Models.Forms;
using QuizForms.Data.Repositories.Abstract;
using QuizForms.Web.Models.Account;
using QuizForms.Web.Models.ManageQuizForms;

namespace QuizForms.Web.Controllers.Admin
{
    [Authorize]
    [Route("admin/forms")]    
    public class ManageQuizFormsController : Controller
    {
        private readonly IQuizFormsRepository _formsRepository;
        
        public ManageQuizFormsController(IQuizFormsRepository formsRepository)
        {
            _formsRepository = formsRepository;            
        }

        [HttpGet]
        public IActionResult Index()
        {
            ManageQuizFormsViewModel model = new ManageQuizFormsViewModel()
            {
                AllForms = _formsRepository.GetAll()
            };

            return View(model);
        }

        [HttpGet]
        [Route("view/{id}")]
        public IActionResult Get(string id)
        {
            Form form = _formsRepository.GetById(id);

            // TODO: error handling
            // Redirect to 404 if the form is hidden or inactive

            return View(form);
        }

        [HttpPost]
        [Route("update")]
        [ValidateAntiForgeryToken]
        public IActionResult Update(string action, string id)
        {
            if (_formsRepository.Exists(id))
            {
                switch(action)
                {
                    case "disable":
                        _formsRepository.UpdateAvailable(id, false);
                        break;
                    case "enable":
                        _formsRepository.UpdateAvailable(id, true);
                        break;
                    case "show":
                        _formsRepository.UpdateHidden(id, false);
                        break;
                    case "hide":
                        _formsRepository.UpdateHidden(id, true);
                        break;                   
                }
            }

            return LocalRedirect("/admin/forms");
        }
    }
}