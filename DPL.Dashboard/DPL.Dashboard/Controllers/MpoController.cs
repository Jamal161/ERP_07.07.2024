using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DPL.DASHBOARD.Controllers
{
    public class MpoController : Controller
    {
        //
        // GET: /Mpo/
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult Salesstatement()
        {
            string userRole = (string)Session["UserRole"];
            if (userRole != null && (userRole.Trim() == "MPO" ))
            {
                ViewBag.Message = "Your User page.";
                return View();
            }
            else
            {

                return RedirectToAction("Login", "Home");
            }
        }
        public ActionResult Marketmonitoringsheet()
        {
            string userRole = (string)Session["UserRole"];
            if (userRole != null && (userRole.Trim() == "MPO" ))
            {
                ViewBag.Message = "Your User page.";
                return View();
            }
            else
            {

                return RedirectToAction("Login", "Home");
            }
        }
        public ActionResult Mpoledger()
        {
            string userRole = (string)Session["UserRole"];
            if (userRole != null && (userRole.Trim() == "MPO" ))
            {
                ViewBag.Message = "Your User page.";
                return View();
            }
            else
            {

                return RedirectToAction("Login", "Home");
            }
        }
        public ActionResult Touchuntouch()
        {
            string userRole = (string)Session["UserRole"];
            if (userRole != null && (userRole.Trim() == "MPO" ))
            {
                ViewBag.Message = "Your User page.";
                return View();
            }
            else
            {

                return RedirectToAction("Login", "Home");
            }
        }
        public ActionResult Dailymonitoringsheet()
        {
            string userRole = (string)Session["UserRole"];
            if (userRole != null && (userRole.Trim() == "MPO" ))
            {
                ViewBag.Message = "Your User page.";
                return View();
            }
            else
            {

                return RedirectToAction("Login", "Home");
            }
        }

        public ActionResult Salescollectionachievement()
        {
            string userRole = (string)Session["UserRole"];
            if (userRole != null && (userRole.Trim() == "MPO" ))
            {
                ViewBag.Message = "Your User page.";
                return View();
            }
            else
            {

                return RedirectToAction("Login", "Home");
            }
        }
        public ActionResult Salesperformance()
        {
            string userRole = (string)Session["UserRole"];
            if (userRole != null && (userRole.Trim() == "MPO" ))
            {
                ViewBag.Message = "Your User page.";
                return View();
            }
            else
            {

                return RedirectToAction("Login", "Home");
            }
        }
        public ActionResult Saleschalandelivery()
        {
            string userRole = (string)Session["UserRole"];
            if (userRole != null && (userRole.Trim() == "MPO" ))
            {
                ViewBag.Message = "Your User page.";
                return View();
            }
            else
            {

                return RedirectToAction("Login", "Home");
            }
        }
        public ActionResult Productwisetarget()
        {
            string userRole = (string)Session["UserRole"];
            if (userRole != null && (userRole.Trim() == "MPO" ))
            {
                ViewBag.Message = "Your User page.";
                return View();
            }
            else
            {

                return RedirectToAction("Login", "Home");
            }
        }



        public ActionResult logout()
        {
            Session.Clear();
            Session.Abandon();
            return RedirectToAction("Login", "Home");
        }

	}
}