﻿using EnvanterTakipYönetimSistemi.Login;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EnvanterTakipYönetimSistemi.Controllers
{
    public class ServislerimController : Controller
    {
        // GET: Servislerim
        [LoginControl]
        public ActionResult Index()
        {
            return View();
        }
    }
}