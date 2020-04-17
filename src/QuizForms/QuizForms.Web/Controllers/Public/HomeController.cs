using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using QuizForms.Data.Repositories.Abstract;
using QuizForms.Data.Utilities;
using QuizForms.Web.Models;
using QuizForms.Web.Models.Home;
using QuizForms.Web.Models.Shared;
using System;
using System.Diagnostics;

namespace QuizForms.Web.Controllers.Public
{
    public class HomeController : Controller
    {        
        private readonly IQuizFormsRepository _formsRepository;

        public HomeController(IQuizFormsRepository formsRepository)
        {
            _formsRepository = formsRepository;
        }

        [Route("/")]
        [HttpGet]
        public IActionResult Index()
        {                                    
            return View(new HomeViewModel()
            {
                VisibleForms = _formsRepository.GetAllVisible(),
                LastChangedTimestamp = TimestampConvert.ConvertToTimestamp(_formsRepository.GetLastChanged())
            });
        }

        [Route("/last-changed")]
        [HttpGet]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult LastChanged()
        {
            long timestamp = TimestampConvert.ConvertToTimestamp(_formsRepository.GetLastChanged());
            return Ok(timestamp);
        }


        //[Route("/about")]
        //[HttpGet]
        //public IActionResult About()
        //{
        //    return View();
        //}

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }      
    }
}
