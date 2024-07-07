using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DPL.DASHBOARD.Controllers
{
    public class TestController : Controller
    {
        //
        // GET: /Test/
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Searchitem()
        {
            ViewBag.Message = "Test.";

            return View();
        }

        public ActionResult Searchitems()
        {
            ViewBag.Message = "Test.";

            return View();
        }
	}
}