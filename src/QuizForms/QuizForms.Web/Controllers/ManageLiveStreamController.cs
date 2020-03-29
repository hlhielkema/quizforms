using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace QuizForms.Web.Controllers
{
    [Authorize]
    [Route("admin/livestream")]
    public class ManageLiveStreamController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
    }
}