using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using QuizForms.Data.Managers;
using QuizForms.Data.Models.Forms;

namespace QuizForms.Web.Controllers
{
    [Route("forms")]
    public class QuizFormController : Controller
    {
        [Route("{id}")]
        [HttpGet]        
        public IActionResult Index(string id)
        {
            // Temp for testing
            string dir = @"D:\GitHub Workspace\quizforms\data\forms";
            Form form = QuizFormManager.GetFormByid(dir, id);

            return View(form);
        }
    }
}