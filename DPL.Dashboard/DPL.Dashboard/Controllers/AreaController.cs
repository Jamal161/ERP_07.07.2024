using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DPL.DASHBOARD.Controllers
{
    public class AreaController : Controller
    {
        //
        // GET: /Area/
        public ActionResult Index()
        {


            string userRole = (string)Session["UserRole"];
            if (userRole != null && (userRole.Trim() == "AH"))
            {
                ViewBag.Message = "Your User page.";
                return View();
            }
            else
            {

                return RedirectToAction("Login", "Home");
            }

        }


        //public ActionResult Areasalesstatement()
        //{
        //    string userRole = (string)Session["UserRole"];
        //    if (userRole != null && ( userRole.Trim() == "AH"))
        //    {
        //        ViewBag.Message = "Your User page.";
        //        return View();
        //    }
        //    else
        //    {

        //        return RedirectToAction("Login", "Home");
        //    }
        //}
        //public ActionResult Areamarketmonitoringsheet()
        //{
        //    string userRole = (string)Session["UserRole"];
        //    if (userRole != null && (userRole.Trim() == "AH"))
        //    {
        //        ViewBag.Message = "Your User page.";
        //        return View();
        //    }
        //    else
        //    {

        //        return RedirectToAction("Login", "Home");
        //    }
        //}
        //public ActionResult Areampoledger()
        //{
        //    string userRole = (string)Session["UserRole"];
        //    if (userRole != null && ( userRole.Trim() == "AH"))
        //    {
        //        ViewBag.Message = "Your User page.";
        //        return View();
        //    }
        //    else
        //    {

        //        return RedirectToAction("Login", "Home");
        //    }
        //}
        //public ActionResult Areatouchuntouch()
        //{
        //    string userRole = (string)Session["UserRole"];
        //    if (userRole != null && ( userRole.Trim() == "AH"))
        //    {
        //        ViewBag.Message = "Your User page.";
        //        return View();
        //    }
        //    else
        //    {

        //        return RedirectToAction("Login", "Home");
        //    }
        //}
        //public ActionResult Areadailymonitoringsheet()
        //{
        //    string userRole = (string)Session["UserRole"];
        //    if (userRole != null && (userRole.Trim() == "AH"))
        //    {
        //        ViewBag.Message = "Your User page.";
        //        return View();
        //    }
        //    else
        //    {

        //        return RedirectToAction("Login", "Home");
        //    }
        //}
        //public ActionResult Areasalescollectionachievement()
        //{
        //    string userRole = (string)Session["UserRole"];
        //    if (userRole != null && (userRole.Trim() == "AH"))
        //    {
        //        ViewBag.Message = "Your User page.";
        //        return View();
        //    }
        //    else
        //    {

        //        return RedirectToAction("Login", "Home");
        //    }
        //}
        //public ActionResult Areasalesperformance()
        //{
        //    string userRole = (string)Session["UserRole"];
        //    if (userRole != null && (userRole.Trim() == "AH"))
        //    {
        //        ViewBag.Message = "Your User page.";
        //        return View();
        //    }
        //    else
        //    {

        //        return RedirectToAction("Login", "Home");
        //    }
        //}
        //public ActionResult Areasaleschalandelivery()
        //{
        //    string userRole = (string)Session["UserRole"];
        //    if (userRole != null && ( userRole.Trim() == "AH"))
        //    {
        //        ViewBag.Message = "Your User page.";
        //        return View();
        //    }
        //    else
        //    {

        //        return RedirectToAction("Login", "Home");
        //    }
        //}

        //public ActionResult Areaproductwisetarget()
        //{
        //    string userRole = (string)Session["UserRole"];
        //    if (userRole != null && ( userRole.Trim() == "AH"))
        //    {
        //        ViewBag.Message = "Your User page.";
        //        return View();
        //    }
        //    else
        //    {

        //        return RedirectToAction("Login", "Home");
        //    }
        //}


        //public ActionResult logout()
        //{
        //    Session.Clear();
        //    Session.Abandon();
        //    return RedirectToAction("Login", "Home");
        //}


        public ActionResult About()
        {
            string userRole = (string)Session["UserRole"];
            if (userRole != null && (userRole.Trim() == "AH"))
            {
                ViewBag.Message = "Your User page.";
                return View();
            }
            else
            {

                return RedirectToAction("Login", "Home");
            }
        }


        public ActionResult Contact()
        {
            string userRole = (string)Session["UserRole"];
            if (userRole != null && (userRole.Trim() == "AH"))
            {
                ViewBag.Message = "Your User page.";
                return View();
            }
            else
            {

                return RedirectToAction("Login", "Home");
            }
        }


        public ActionResult Attendancelist()
        {
            string userRole = (string)Session["UserRole"];
            if (userRole != null && (userRole.Trim() == "AH"))
            {
                ViewBag.Message = "Your User page.";
                return View();
            }
            else
            {

                return RedirectToAction("Login", "Home");
            }
        }


        public ActionResult Dailyshift()
        {
            string userRole = (string)Session["UserRole"];
            if (userRole != null && (userRole.Trim() == "AH"))
            {
                ViewBag.Message = "Your User page.";
                return View();
            }
            else
            {

                return RedirectToAction("Login", "Home");
            }
        }


        public ActionResult Examlist()
        {
            string userRole = (string)Session["UserRole"];
            if (userRole != null && (userRole.Trim() == "AH"))
            {
                ViewBag.Message = "Your User page.";
                return View();
            }
            else
            {

                return RedirectToAction("Login", "Home");
            }
        }


        public ActionResult Holydaylist()
        {
            string userRole = (string)Session["UserRole"];
            if (userRole != null && (userRole.Trim() == "AH"))
            {
                ViewBag.Message = "Your User page.";
                return View();
            }
            else
            {

                return RedirectToAction("Login", "Home");
            }
        }


        public ActionResult Leavetypelist()
        {
            string userRole = (string)Session["UserRole"];
            if (userRole != null && (userRole.Trim() == "AH"))
            {
                ViewBag.Message = "Your User page.";
                return View();
            }
            else
            {

                return RedirectToAction("Login", "Home");
            }
        }



        public ActionResult Weekendlist()
        {
            string userRole = (string)Session["UserRole"];
            if (userRole != null && (userRole.Trim() == "AH"))
            {
                ViewBag.Message = "Your User page.";
                return View();
            }
            else
            {

                return RedirectToAction("Login", "Home");
            }
        }


        public ActionResult Leaveallocationlist()
        {
            string userRole = (string)Session["UserRole"];
            if (userRole != null && (userRole.Trim() == "AH"))
            {
                ViewBag.Message = "Your User page.";
                return View();
            }
            else
            {

                return RedirectToAction("Login", "Home");
            }
        }


        public ActionResult Leavelist()
        {
            string userRole = (string)Session["UserRole"];
            if (userRole != null && (userRole.Trim() == "AH"))
            {
                ViewBag.Message = "Your User page.";
                return View();
            }
            else
            {

                return RedirectToAction("Login", "Home");
            }

        }



        public ActionResult Leaveaccount()
        {
            string userRole = (string)Session["UserRole"];
            if (userRole != null && (userRole.Trim() == "AH"))
            {
                ViewBag.Message = "Your User page.";
                return View();
            }
            else
            {

                return RedirectToAction("Login", "Home");
            }
        }



        public ActionResult Tracking()
        {
            string userRole = (string)Session["UserRole"];
            if (userRole != null && (userRole.Trim() == "AH"))
            {
                ViewBag.Message = "Your User page.";
                return View();
            }
            else
            {

                return RedirectToAction("Login", "Home");
            }
        }


        public ActionResult Userlastlocation()
        {
            string userRole = (string)Session["UserRole"];
            if (userRole != null && (userRole.Trim() == "AH"))
            {
                ViewBag.Message = "Your User page.";
                return View();
            }
            else
            {

                return RedirectToAction("Login", "Home");
            }
        }



        public ActionResult Multipledates()
        {
            string userRole = (string)Session["UserRole"];
            if (userRole != null && (userRole.Trim() == "AH"))
            {
                ViewBag.Message = "Your User page.";
                return View();
            }
            else
            {

                return RedirectToAction("Login", "Home");
            }
        }



        public ActionResult Doctorlocation()
        {
            string userRole = (string)Session["UserRole"];
            if (userRole != null && (userRole.Trim() == "AH"))
            {
                ViewBag.Message = "Your User page.";
                return View();
            }
            else
            {

                return RedirectToAction("Login", "Home");
            }
        }



        public ActionResult TourPlanneduserlist()
        {
            string userRole = (string)Session["UserRole"];
            if (userRole != null && (userRole.Trim() == "AH"))
            {
                ViewBag.Message = "Your User page.";
                return View();
            }
            else
            {

                return RedirectToAction("Login", "Home");
            }
        }



        public ActionResult Tourtypelist()
        {
            string userRole = (string)Session["UserRole"];
            if (userRole != null && (userRole.Trim() == "AH"))
            {
                ViewBag.Message = "Your User page.";
                return View();
            }
            else
            {

                return RedirectToAction("Login", "Home");
            }
        }


        public ActionResult Tourpurposelist()
        {
            string userRole = (string)Session["UserRole"];
            if (userRole != null && (userRole.Trim() == "AH"))
            {
                ViewBag.Message = "Your User page.";
                return View();
            }
            else
            {

                return RedirectToAction("Login", "Home");
            }
        }


        public ActionResult Expenseclaimlist()
        {
            string userRole = (string)Session["UserRole"];
            if (userRole != null && (userRole.Trim() == "AH"))
            {
                ViewBag.Message = "Your User page.";
                return View();
            }
            else
            {

                return RedirectToAction("Login", "Home");
            }
        }


        public ActionResult Monthlyallowslist()
        {
            string userRole = (string)Session["UserRole"];
            if (userRole != null && (userRole.Trim() == "AH"))
            {
                ViewBag.Message = "Your User page.";
                return View();
            }
            else
            {

                return RedirectToAction("Login", "Home");
            }
        }


        public ActionResult Tadamarketrulelist()
        {
            string userRole = (string)Session["UserRole"];
            if (userRole != null && (userRole.Trim() == "AH"))
            {
                ViewBag.Message = "Your User page.";
                return View();
            }
            else
            {

                return RedirectToAction("Login", "Home");
            }
        }


        public ActionResult Tadaclaimlist()
        {
            string userRole = (string)Session["UserRole"];
            if (userRole != null && (userRole.Trim() == "AH"))
            {
                ViewBag.Message = "Your User page.";
                return View();
            }
            else
            {

                return RedirectToAction("Login", "Home");
            }
        }

        public ActionResult Expensetypelist()
        {
            string userRole = (string)Session["UserRole"];
            if (userRole != null && (userRole.Trim() == "AH"))
            {
                ViewBag.Message = "Your User page.";
                return View();
            }
            else
            {

                return RedirectToAction("Login", "Home");
            }
        }


        public ActionResult Milagesclaimlist()
        {
            string userRole = (string)Session["UserRole"];
            if (userRole != null && (userRole.Trim() == "AH"))
            {
                ViewBag.Message = "Your User page.";
                return View();
            }
            else
            {

                return RedirectToAction("Login", "Home");
            }
        }


        public ActionResult Salesstatement()
        {
            string userRole = (string)Session["UserRole"];
            if (userRole != null && (userRole.Trim() == "AH"))
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
            if (userRole != null && (userRole.Trim() == "AH"))
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
            if (userRole != null && (userRole.Trim() == "AH"))
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
            if (userRole != null && (userRole.Trim() == "AH"))
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
            if (userRole != null && (userRole.Trim() == "AH"))
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
            if (userRole != null && (userRole.Trim() == "AH"))
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
            if (userRole != null && (userRole.Trim() == "AH"))
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
            if (userRole != null && (userRole.Trim() == "AH"))
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
            if (userRole != null && (userRole.Trim() == "AH"))
            {
                ViewBag.Message = "Your User page.";
                return View();
            }
            else
            {

                return RedirectToAction("Login", "Home");
            }
        }


        public ActionResult Adminsalesstatement()
        {
            string userRole = (string)Session["UserRole"];
            if (userRole != null && (userRole.Trim() == "AH"))
            {
                ViewBag.Message = "Your User page.";
                return View();
            }
            else
            {

                return RedirectToAction("Login", "Home");
            }
        }


        public ActionResult Adminmarketmonitoringsheet()
        {
            string userRole = (string)Session["UserRole"];
            if (userRole != null && (userRole.Trim() == "AH"))
            {
                ViewBag.Message = "Your User page.";
                return View();
            }
            else
            {

                return RedirectToAction("Login", "Home");
            }
        }


        public ActionResult Adminmpoledger()
        {
            string userRole = (string)Session["UserRole"];
            if (userRole != null && (userRole.Trim() == "AH"))
            {
                ViewBag.Message = "Your User page.";
                return View();
            }
            else
            {

                return RedirectToAction("Login", "Home");
            }
        }


        public ActionResult Admintouchuntouch()
        {
            string userRole = (string)Session["UserRole"];
            if (userRole != null && (userRole.Trim() == "AH"))
            {
                ViewBag.Message = "Your User page.";
                return View();
            }
            else
            {

                return RedirectToAction("Login", "Home");
            }
        }

        public ActionResult Admindailymonitoringsheet()
        {
            string userRole = (string)Session["UserRole"];
            if (userRole != null && (userRole.Trim() == "AH"))
            {
                ViewBag.Message = "Your User page.";
                return View();
            }
            else
            {

                return RedirectToAction("Login", "Home");
            }
        }

        public ActionResult Adminsalescollectionachievement()
        {
            string userRole = (string)Session["UserRole"];
            if (userRole != null && (userRole.Trim() == "AH"))
            {
                ViewBag.Message = "Your User page.";
                return View();
            }
            else
            {

                return RedirectToAction("Login", "Home");
            }
        }


        public ActionResult Adminsalesperformance()
        {
            string userRole = (string)Session["UserRole"];
            if (userRole != null && (userRole.Trim() == "AH"))
            {
                ViewBag.Message = "Your User page.";
                return View();
            }
            else
            {

                return RedirectToAction("Login", "Home");
            }
        }


        public ActionResult Adminsaleschalandelivery()
        {
            string userRole = (string)Session["UserRole"];
            if (userRole != null && (userRole.Trim() == "AH"))
            {
                ViewBag.Message = "Your User page.";
                return View();
            }
            else
            {

                return RedirectToAction("Login", "Home");
            }
        }


        public ActionResult Adminproductwisetarget()
        {
            string userRole = (string)Session["UserRole"];
            if (userRole != null && (userRole.Trim() == "AH"))
            {
                ViewBag.Message = "Your User page.";
                return View();
            }
            else
            {

                return RedirectToAction("Login", "Home");
            }
        }


        public ActionResult Divisionsalesstatement()
        {
            string userRole = (string)Session["UserRole"];
            if (userRole != null && (userRole.Trim() == "AH"))
            {
                ViewBag.Message = "Your User page.";
                return View();
            }
            else
            {

                return RedirectToAction("Login", "Home");
            }
        }


        public ActionResult Divisionmarketmonitoringsheet()
        {
            string userRole = (string)Session["UserRole"];
            if (userRole != null && (userRole.Trim() == "AH"))
            {
                ViewBag.Message = "Your User page.";
                return View();
            }
            else
            {

                return RedirectToAction("Login", "Home");
            }
        }


        public ActionResult Divisionmpoledger()
        {
            string userRole = (string)Session["UserRole"];
            if (userRole != null && (userRole.Trim() == "AH"))
            {
                ViewBag.Message = "Your User page.";
                return View();
            }
            else
            {

                return RedirectToAction("Login", "Home");
            }
        }


        public ActionResult Divisiontouchuntouch()
        {
            string userRole = (string)Session["UserRole"];
            if (userRole != null && (userRole.Trim() == "AH"))
            {
                ViewBag.Message = "Your User page.";
                return View();
            }
            else
            {

                return RedirectToAction("Login", "Home");
            }
        }


        public ActionResult Divisiondailymonitoringsheet()
        {
            string userRole = (string)Session["UserRole"];
            if (userRole != null && (userRole.Trim() == "AH"))
            {
                ViewBag.Message = "Your User page.";
                return View();
            }
            else
            {

                return RedirectToAction("Login", "Home");
            }
        }


        public ActionResult Divisionsalescollectionachievement()
        {
            string userRole = (string)Session["UserRole"];
            if (userRole != null && (userRole.Trim() == "AH"))
            {
                ViewBag.Message = "Your User page.";
                return View();
            }
            else
            {

                return RedirectToAction("Login", "Home");
            }
        }


        public ActionResult Divisionsalesperformance()
        {
            string userRole = (string)Session["UserRole"];
            if (userRole != null && (userRole.Trim() == "AH"))
            {
                ViewBag.Message = "Your User page.";
                return View();
            }
            else
            {

                return RedirectToAction("Login", "Home");
            }
        }


        public ActionResult Divisionsaleschalandelivery()
        {
            string userRole = (string)Session["UserRole"];
            if (userRole != null && (userRole.Trim() == "AH"))
            {
                ViewBag.Message = "Your User page.";
                return View();
            }
            else
            {

                return RedirectToAction("Login", "Home");
            }
        }

        public ActionResult Divisionproductwisetarget()
        {
            string userRole = (string)Session["UserRole"];
            if (userRole != null && (userRole.Trim() == "AH"))
            {
                ViewBag.Message = "Your User page.";
                return View();
            }
            else
            {

                return RedirectToAction("Login", "Home");
            }
        }


        public ActionResult Areasalesstatement()
        {
            string userRole = (string)Session["UserRole"];
            if (userRole != null && (userRole.Trim() == "AH"))
            {
                ViewBag.Message = "Your User page.";
                return View();
            }
            else
            {

                return RedirectToAction("Login", "Home");
            }
        }


        public ActionResult Areamarketmonitoringsheet()
        {
            string userRole = (string)Session["UserRole"];
            if (userRole != null && (userRole.Trim() == "AH"))
            {
                ViewBag.Message = "Your User page.";
                return View();
            }
            else
            {

                return RedirectToAction("Login", "Home");
            }
        }


        public ActionResult Areampoledger()
        {
            string userRole = (string)Session["UserRole"];
            if (userRole != null && (userRole.Trim() == "AH"))
            {
                ViewBag.Message = "Your User page.";
                return View();
            }
            else
            {

                return RedirectToAction("Login", "Home");
            }
        }


        public ActionResult Areatouchuntouch()
        {
            string userRole = (string)Session["UserRole"];
            if (userRole != null && (userRole.Trim() == "AH"))
            {
                ViewBag.Message = "Your User page.";
                return View();
            }
            else
            {

                return RedirectToAction("Login", "Home");
            }
        }


        public ActionResult Areadailymonitoringsheet()
        {
            string userRole = (string)Session["UserRole"];
            if (userRole != null && (userRole.Trim() == "AH"))
            {
                ViewBag.Message = "Your User page.";
                return View();
            }
            else
            {

                return RedirectToAction("Login", "Home");
            }
        }

        public ActionResult Areasalescollectionachievement()
        {
            string userRole = (string)Session["UserRole"];
            if (userRole != null && (userRole.Trim() == "AH"))
            {
                ViewBag.Message = "Your User page.";
                return View();
            }
            else
            {

                return RedirectToAction("Login", "Home");
            }
        }


        public ActionResult Areasalesperformance()
        {
            string userRole = (string)Session["UserRole"];
            if (userRole != null && (userRole.Trim() == "AH"))
            {
                ViewBag.Message = "Your User page.";
                return View();
            }
            else
            {

                return RedirectToAction("Login", "Home");
            }
        }


        public ActionResult Areasaleschalandelivery()
        {
            string userRole = (string)Session["UserRole"];
            if (userRole != null && (userRole.Trim() == "AH"))
            {
                ViewBag.Message = "Your User page.";
                return View();
            }
            else
            {

                return RedirectToAction("Login", "Home");
            }
        }


        public ActionResult PrescriptionGallery()
        {
            string userRole = (string)Session["UserRole"];
            if (userRole != null && (userRole.Trim() == "AH"))
            {
                ViewBag.Message = "Your User page.";
                return View();
            }
            else
            {

                return RedirectToAction("Login", "Home");
            }
        }

        public ActionResult PrescriptionList()
        {
            string userRole = (string)Session["UserRole"];
            if (userRole != null && (userRole.Trim() == "AH"))
            {
                ViewBag.Message = "Your User page.";
                return View();
            }
            else
            {

                return RedirectToAction("Login", "Home");
            }
        }

        public ActionResult Areaproductwisetarget()
        {
            string userRole = (string)Session["UserRole"];
            if (userRole != null && (userRole.Trim() == "AH"))
            {
                ViewBag.Message = "Your User page.";
                return View();
            }
            else
            {

                return RedirectToAction("Login", "Home");
            }
        }



        public ActionResult Notices()
        {
            string userRole = (string)Session["UserRole"];
            if (userRole != null && (userRole.Trim() == "AH"))
            {
                ViewBag.Message = "Your User page.";
                return View();
            }
            else
            {

                return RedirectToAction("Login", "Home");
            }
        }

        public ActionResult Training()
        {
            string userRole = (string)Session["UserRole"];
            if (userRole != null && (userRole.Trim() == "AH"))
            {
                ViewBag.Message = "Your User page.";
                return View();
            }
            else
            {

                return RedirectToAction("Login", "Home");
            }
        }

        public ActionResult QuestionAdd()
        {
            string userRole = (string)Session["UserRole"];
            if (userRole != null && (userRole.Trim() == "AH"))
            {
                ViewBag.Message = "Your User page.";
                return View();
            }
            else
            {

                return RedirectToAction("Login", "Home");
            }
        }

        public ActionResult ExamResult()
        {
            string userRole = (string)Session["UserRole"];
            if (userRole != null && (userRole.Trim() == "AH"))
            {
                ViewBag.Message = "Your User page.";
                return View();
            }
            else
            {

                return RedirectToAction("Login", "Home");
            }
        }

        public ActionResult TodayTask()
        {
            string userRole = (string)Session["UserRole"];
            if (userRole != null && (userRole.Trim() == "AH"))
            {
                ViewBag.Message = "Your User page.";
                return View();
            }
            else
            {

                return RedirectToAction("Login", "Home");
            }
        }

        public ActionResult DailyTaskAdd()
        {
            string userRole = (string)Session["UserRole"];
            if (userRole != null && (userRole.Trim() == "AH"))
            {
                ViewBag.Message = "Your User page.";
                return View();
            }
            else
            {

                return RedirectToAction("Login", "Home");
            }
        }


        public ActionResult NoticesAdd()
        {
            string userRole = (string)Session["UserRole"];
            if (userRole != null && (userRole.Trim() == "AH"))
            {
                ViewBag.Message = "Your User page.";
                return View();
            }
            else
            {

                return RedirectToAction("Login", "Home");
            }
        }


        public ActionResult TrainingAdd()
        {
            string userRole = (string)Session["UserRole"];
            if (userRole != null && (userRole.Trim() == "AH"))
            {
                ViewBag.Message = "Your User page.";
                return View();
            }
            else
            {

                return RedirectToAction("Login", "Home");
            }
        }

        public ActionResult DoctorVisitType()
        {
            string userRole = (string)Session["UserRole"];
            if (userRole != null && (userRole.Trim() == "AH"))
            {
                ViewBag.Message = "Your User page.";
                return View();
            }
            else
            {

                return RedirectToAction("Login", "Home");
            }
        }

        public ActionResult DoctorVisitList()
        {
            string userRole = (string)Session["UserRole"];
            if (userRole != null && (userRole.Trim() == "AH"))
            {
                ViewBag.Message = "Your User page.";
                return View();
            }
            else
            {

                return RedirectToAction("Login", "Home");
            }
        }

        public ActionResult DoctorVisitPlanUserList()
        {
            string userRole = (string)Session["UserRole"];
            if (userRole != null && (userRole.Trim() == "AH"))
            {
                ViewBag.Message = "Your User page.";
                return View();
            }
            else
            {

                return RedirectToAction("Login", "Home");
            }
        }

        public ActionResult DoctorVisitPlanOneUSer()
        {
            string userRole = (string)Session["UserRole"];
            if (userRole != null && (userRole.Trim() == "AH"))
            {
                ViewBag.Message = "Your User page.";
                return View();
            }
            else
            {

                return RedirectToAction("Login", "Home");
            }
        }

        public ActionResult DoctorVisitPlan()
        {
            string userRole = (string)Session["UserRole"];
            if (userRole != null && (userRole.Trim() == "AH"))
            {
                ViewBag.Message = "Your User page.";
                return View();
            }
            else
            {

                return RedirectToAction("Login", "Home");
            }
        }


        public ActionResult NationalHead()
        {
            string userRole = (string)Session["UserRole"];
            if (userRole != null && (userRole.Trim() == "AH"))
            {
                ViewBag.Message = "Your User page.";
                return View();
            }
            else
            {

                return RedirectToAction("Login", "Home");
            }
        }


        public ActionResult Team()
        {
            string userRole = (string)Session["UserRole"];
            if (userRole != null && (userRole.Trim() == "AH"))
            {
                ViewBag.Message = "Your User page.";
                return View();
            }
            else
            {

                return RedirectToAction("Login", "Home");
            }
        }


        public ActionResult Zone()
        {
            string userRole = (string)Session["UserRole"];
            if (userRole != null && (userRole.Trim() == "AH"))
            {
                ViewBag.Message = "Your User page.";
                return View();
            }
            else
            {

                return RedirectToAction("Login", "Home");
            }
        }


        public ActionResult Division()
        {
            string userRole = (string)Session["UserRole"];
            if (userRole != null && (userRole.Trim() == "AH"))
            {
                ViewBag.Message = "Your User page.";
                return View();
            }
            else
            {

                return RedirectToAction("Login", "Home");
            }
        }


        public ActionResult Area()
        {
            string userRole = (string)Session["UserRole"];
            if (userRole != null && (userRole.Trim() == "AH"))
            {
                ViewBag.Message = "Your User page.";
                return View();
            }
            else
            {

                return RedirectToAction("Login", "Home");
            }
        }

        public ActionResult Market()
        {
            string userRole = (string)Session["UserRole"];
            if (userRole != null && (userRole.Trim() == "AH"))
            {
                ViewBag.Message = "Your User page.";
                return View();
            }
            else
            {

                return RedirectToAction("Login", "Home");
            }
        }

        public ActionResult Route()
        {
            string userRole = (string)Session["UserRole"];
            if (userRole != null && (userRole.Trim() == "AH"))
            {
                ViewBag.Message = "Your User page.";
                return View();
            }
            else
            {

                return RedirectToAction("Login", "Home");
            }
        }


        public ActionResult productname()
        {
            string userRole = (string)Session["UserRole"];
            if (userRole != null && (userRole.Trim() == "AH"))
            {
                ViewBag.Message = "Your User page.";
                return View();
            }
            else
            {

                return RedirectToAction("Login", "Home");
            }
        }

        public ActionResult UpcomingExamsQuestion()
        {
            string userRole = (string)Session["UserRole"];
            if (userRole != null && (userRole.Trim() == "AH"))
            {
                ViewBag.Message = "Your User page.";
                return View();
            }
            else
            {

                return RedirectToAction("Login", "Home");
            }
        }

        public ActionResult PastExamsQuestion()
        {
            string userRole = (string)Session["UserRole"];
            if (userRole != null && (userRole.Trim() == "AH"))
            {
                ViewBag.Message = "Your User page.";
                return View();
            }
            else
            {

                return RedirectToAction("Login", "Home");
            }
        }

        public ActionResult Details()
        {
            string userRole = (string)Session["UserRole"];
            if (userRole != null && (userRole.Trim() == "AH"))
            {
                ViewBag.Message = "Your User page.";
                return View();
            }
            else
            {

                return RedirectToAction("Login", "Home");
            }
        }

       
	}
}