using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace QuizForms.Web.Controllers.Public
{
    [Route("livestream")]
    public class LiveStreamController : Controller
    {
        [Route("twitch")]
        [HttpGet]
        public IActionResult Twitch()
        {
            return View();
        }
    }
}