﻿using System.Web.Mvc;
using WebFrontend.Attributes;

namespace WebFrontend.Controllers
{
    public class HelpController : Controller
    {
        public ActionResult ScoreEntry()
        {
            return View();
        }

        public ActionResult Stepladder()
        {
            return View();
        }

        public ActionResult Alternates()
        {
            return View();
        }
    }
}