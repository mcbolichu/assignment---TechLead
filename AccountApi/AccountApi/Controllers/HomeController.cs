using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AccountApi.Controllers;

namespace AccountApi.Controllers
{
    public class HomeController : Controller
    {
        public static Hashtable htAccount = new Hashtable();

        public ActionResult Index()
        {            
            ViewBag.Title = "Home Page";

            AccountController ac = new AccountController();
            ac.generateAccountHashTable(htAccount);
          


            return View();
        }
    }
}
