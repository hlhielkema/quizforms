﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace QuizForms.Web.Controllers
{
    public class CheckAnswersController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}