﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication1.Controllers
{
    public class PolicyController : Controller
    {
        // GET: Policy
        public ActionResult Policy()
        {
            return View();
        }

        public ActionResult Warranty()
        {
            return View();
        }

        public ActionResult Condition()
        {
            return View();
        }
    }
}