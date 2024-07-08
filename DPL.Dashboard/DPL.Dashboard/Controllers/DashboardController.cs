using DPL.DASHBOARD.Models;
using Dutility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace DPL.DASHBOARD.Controllers
{
    public class DashboardController : Controller
    {
        //
        // GET: /Dashboard/

    
        public ActionResult Index()
        {


            string userRole = (string)Session["UserRole"];
            if (userRole != null && (userRole.Trim() == "Admin" ))
            {
                ViewBag.Message = "Your User page.";
                return View();
            }
            else
            {

                return RedirectToAction("Login", "Home");
            }
            
        }


       
        public ActionResult About()
        {
            string userRole = (string)Session["UserRole"];
            if (userRole != null && (userRole.Trim() == "Admin" ))
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
            if (userRole != null && (userRole.Trim() == "Admin" ))
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
            if (userRole != null && (userRole.Trim() == "Admin" ))
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
            if (userRole != null && (userRole.Trim() == "Admin" ))
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
            if (userRole != null && (userRole.Trim() == "Admin" ))
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
            if (userRole != null && (userRole.Trim() == "Admin" ))
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
            if (userRole != null && (userRole.Trim() == "Admin" ))
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
            if (userRole != null && (userRole.Trim() == "Admin" ))
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
            if (userRole != null && (userRole.Trim() == "Admin" ))
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
            if (userRole != null && (userRole.Trim() == "Admin"))
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
            if (userRole != null && (userRole.Trim() == "Admin" ))
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
            if (userRole != null && (userRole.Trim() == "Admin" ))
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
            if (userRole != null && (userRole.Trim() == "Admin"))
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
            if (userRole != null && (userRole.Trim() == "Admin"))
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
            if (userRole != null && (userRole.Trim() == "Admin"))
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
            if (userRole != null && (userRole.Trim() == "Admin"))
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
            if (userRole != null && (userRole.Trim() == "Admin"))
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
            if (userRole != null && (userRole.Trim() == "Admin" ))
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
            if (userRole != null && (userRole.Trim() == "Admin" ))
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
            if (userRole != null && (userRole.Trim() == "Admin"))
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
            if (userRole != null && (userRole.Trim() == "Admin" ))
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
            if (userRole != null && (userRole.Trim() == "Admin" ))
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
            if (userRole != null && (userRole.Trim() == "Admin"))
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
            if (userRole != null && (userRole.Trim() == "Admin" ))
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
            if (userRole != null && (userRole.Trim() == "Admin" ))
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
            if (userRole != null && (userRole.Trim() == "Admin"))
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
            if (userRole != null && (userRole.Trim() == "Admin"))
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
            if (userRole != null && (userRole.Trim() == "Admin" ))
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
            if (userRole != null && (userRole.Trim() == "Admin" ))
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
            if (userRole != null && (userRole.Trim() == "Admin" ))
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
            if (userRole != null && (userRole.Trim() == "Admin" ))
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
            if (userRole != null && (userRole.Trim() == "Admin" ))
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
            if (userRole != null && (userRole.Trim() == "Admin" ))
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
            if (userRole != null && (userRole.Trim() == "Admin" ))
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
            if (userRole != null && (userRole.Trim() == "Admin" ))
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
            if (userRole != null && (userRole.Trim() == "Admin" ))
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
            if (userRole != null && (userRole.Trim() == "Admin" ))
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
            if (userRole != null && (userRole.Trim() == "Admin"))
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
            if (userRole != null && (userRole.Trim() == "Admin" ))
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
            if (userRole != null && (userRole.Trim() == "Admin" ))
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
            if (userRole != null && (userRole.Trim() == "Admin" ))
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
            if (userRole != null && (userRole.Trim() == "Admin" ))
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
            if (userRole != null && (userRole.Trim() == "Admin" ))
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
            if (userRole != null && (userRole.Trim() == "Admin" ))
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
            if (userRole != null && (userRole.Trim() == "Admin" ))
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
            if (userRole != null && (userRole.Trim() == "Admin" ))
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
            if (userRole != null && (userRole.Trim() == "Admin"))
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
            if (userRole != null && (userRole.Trim() == "Admin" ))
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
            if (userRole != null && (userRole.Trim() == "Admin" ))
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
            if (userRole != null && (userRole.Trim() == "Admin" ))
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
            if (userRole != null && (userRole.Trim() == "Admin" ))
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
            if (userRole != null && (userRole.Trim() == "Admin" ))
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
            if (userRole != null && (userRole.Trim() == "Admin" ))
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
            if (userRole != null && (userRole.Trim() == "Admin" ))
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
            if (userRole != null && (userRole.Trim() == "Admin" ))
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
            if (userRole != null && (userRole.Trim() == "Admin" ))
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
            if (userRole != null && (userRole.Trim() == "Admin" ))
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
            if (userRole != null && (userRole.Trim() == "Admin" ))
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
            if (userRole != null && (userRole.Trim() == "Admin"))
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
            if (userRole != null && (userRole.Trim() == "Admin" ))
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
            if (userRole != null && (userRole.Trim() == "Admin" ))
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
            if (userRole != null && (userRole.Trim() == "Admin" ))
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
            if (userRole != null && (userRole.Trim() == "Admin"))
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
            if (userRole != null && (userRole.Trim() == "Admin" ))
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
            if (userRole != null && (userRole.Trim() == "Admin" ))
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
            if (userRole != null && (userRole.Trim() == "Admin" ))
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
            if (userRole != null && (userRole.Trim() == "Admin" ))
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
            if (userRole != null && (userRole.Trim() == "Admin" ))
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
            if (userRole != null && (userRole.Trim() == "Admin" ))
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
            if (userRole != null && (userRole.Trim() == "Admin" ))
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
            if (userRole != null && (userRole.Trim() == "Admin" ))
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
            if (userRole != null && (userRole.Trim() == "Admin" ))
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
            if (userRole != null && (userRole.Trim() == "Admin" ))
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
            if (userRole != null && (userRole.Trim() == "Admin" ))
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
            if (userRole != null && (userRole.Trim() == "Admin"))
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
            if (userRole != null && (userRole.Trim() == "Admin" ))
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
            if (userRole != null && (userRole.Trim() == "Admin" ))
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
            if (userRole != null && (userRole.Trim() == "Admin" ))
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
            if (userRole != null && (userRole.Trim() == "Admin" ))
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
            if (userRole != null && (userRole.Trim() == "Admin" ))
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
            if (userRole != null && (userRole.Trim() == "Admin" ))
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
            if (userRole != null && (userRole.Trim() == "Admin" ))
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
            if (userRole != null && (userRole.Trim() == "Admin" ))
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
            if (userRole != null && (userRole.Trim() == "Admin" ))
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
            if (userRole != null && (userRole.Trim() == "Admin" ))
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
            if (userRole != null && (userRole.Trim() == "Admin" ))
            {
                ViewBag.Message = "Your User page.";
                return View();
            }
            else
            {

                return RedirectToAction("Login", "Home");
            }
        }

       
       

      
        public ActionResult DhUser()
        {
            string userRole = (string)Session["UserRole"];
            if (userRole != null && ( userRole.Trim() == "DH" ))
            {
                ViewBag.Message = "Your User page.";
                return View();
            }
            else
            {

                return RedirectToAction("Login", "Home");
            }
        }

       
        public ActionResult AhUser()
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



       
        public ActionResult User()
        {
            string userRole = (string)Session["UserRole"];
            if (userRole != null && (userRole.Trim() == "MPO" || userRole.Trim() == "ZH" || userRole.Trim() == "DH" || userRole.Trim() == "AH" ))
            {
                ViewBag.Message = "Your User page.";
                return View();
            }
            else
            {
                
                return RedirectToAction("Login", "Home");
            }

        }

       
        public ActionResult gstrGetMpoAreaDevisionLists(MPO obj)
        {
            List<MPO> UserList = new List<MPO>();
            UserList = gstrGetMpoAreaDevisionList(obj);

            string userRole = null;
            if (UserList != null && UserList.Count > 0)
            {
                userRole = UserList[0].strRole.Trim();
                Session["UserRole"] = userRole;
                Session["userID"] = UserList[0].strUserID.Trim();
                Session["userName"] = UserList[0].strLedgerName.Trim();
                Session["userCardNo"] = UserList[0].strCardNo.Trim();
            }


            return Json(UserList, JsonRequestBehavior.AllowGet);
        }


        public ActionResult Logout()
        {
            Session.Clear();
            Session.Abandon();
            return RedirectToAction("Login", "Home");
        }




        [HttpPost]
        public List<MPO> gstrGetMpoAreaDevisionList(MPO obj)
        {


            string strSQL = "", strbid = "", strUserRole = "";

          

            SqlDataReader drGetGroup;
            List<MPO> oogrp = new List<MPO>();
            MPO ogrp = new MPO();
           
            byte[] strImage = null;
            SqlCommand cmd = new SqlCommand();
            connstring = Utility.SQLConnstringComSwitch(obj.strDeComID);
            using (SqlConnection gcnMain = new SqlConnection(connstring))

                connstring = Utility.SQLConnstringComSwitch(obj.strDeComID);
            using (SqlConnection gcnMain = new SqlConnection(connstring))
            {
                if (gcnMain.State == ConnectionState.Open)
                {
                    gcnMain.Close();
                }
                gcnMain.Open();
                cmd.Connection = gcnMain;

                if (obj.intAdnin == 0)
                {
                    strSQL = "SELECT USER_ROLE FROM SMART0005.dbo.USER_ONLILE_SECURITY ";
                    strSQL = strSQL + "WHERE STATUS=0 ";
                    strSQL = strSQL + "AND (USER_ID =" + Utility.Val(obj.strUserID) + " ";
                    strSQL = strSQL + "OR EMP_CARD_NO ='" + obj.strCardNo + "')";
                    cmd.CommandText = strSQL;
                    drGetGroup = cmd.ExecuteReader();
                    if (drGetGroup.Read())
                    {
                        strUserRole = drGetGroup["USER_ROLE"].ToString().Trim();
                    }
                    drGetGroup.Close();

                    strSQL = "SELECT USER_ID,BRANCH_ID FROM SMART0005.dbo.USER_ONLILE_SECURITY ";
                    strSQL = strSQL + "WHERE STATUS=0 ";
                    strSQL = strSQL + "AND (USER_ID =" + Utility.Val(obj.strUserID) + " ";
                    strSQL = strSQL + "OR EMP_CARD_NO ='" + obj.strCardNo + "')";
                    cmd.CommandText = strSQL;
                    drGetGroup = cmd.ExecuteReader();
                    if (!drGetGroup.HasRows)
                    {

                        ogrp.strResponse = "User ID Mismatch" + Environment.NewLine + " ইউজার আইডি অমিল";

                        return obj.ogrp;
                    }
                    drGetGroup.Close();
                    strSQL = "SELECT USER_ID,BRANCH_ID FROM SMART0005.dbo.USER_ONLILE_SECURITY ";
                    strSQL = strSQL + "WHERE STATUS=0 ";
                    strSQL = strSQL + "AND PASSWORD ='" + obj.strPassWord + "' ";
               
                    strSQL = strSQL + "AND (USER_ID =" + Utility.Val(obj.strUserID) + " ";
                    strSQL = strSQL + "OR EMP_CARD_NO ='" + obj.strCardNo + "')";
                    cmd.CommandText = strSQL;
                    drGetGroup = cmd.ExecuteReader();
                    if (!drGetGroup.HasRows)
                    {

                        ogrp.strResponse = "Password Mismatch" + Environment.NewLine + " পাসওয়ার্ড মেলেনি";
                    

                        return obj.ogrp;
                    }
                    drGetGroup.Close();

                    strSQL = "SELECT HRS_EMPLOYEE_IMAGE.EMP_IMAGE  FROM SMART0005.dbo.USER_ONLILE_SECURITY,HRS_EMPLOYEE_IMAGE  WHERE HRS_EMPLOYEE_IMAGE.EMP_CARD_NO =SMART0005.dbo.USER_ONLILE_SECURITY.EMP_CARD_NO ";
                    strSQL = strSQL + "AND (SMART0005.dbo.USER_ONLILE_SECURITY.USER_ID =" + Utility.Val(obj.strUserID) + " ";
                    strSQL = strSQL + "OR HRS_EMPLOYEE_IMAGE.EMP_CARD_NO ='" + obj.strCardNo + "')";
                    cmd.CommandText = strSQL;
                    drGetGroup = cmd.ExecuteReader();
                    if (drGetGroup.Read())
                    {
                        if (drGetGroup["EMP_IMAGE"].ToString() != "")
                        {

                            strImage = drGetGroup["EMP_IMAGE"].ToByteArray();
                           

                            long bufLength = drGetGroup.GetBytes(0, 0, null, 0, 0);
                          
                            strImage = new byte[bufLength];
                            
                            drGetGroup.GetBytes(0, 0, strImage, 0, (int)bufLength);
                        }


                    }

                    drGetGroup.Close();

                    if (strUserRole == "MPO")
                    {
                        strSQL = "SELECT SMART0005.dbo.USER_ONLILE_SECURITY.BRANCH_ID,SMART0005.dbo.USER_ONLILE_SECURITY.USER_ID,SMART0005.dbo.USER_ONLILE_SECURITY.PASSWORD,SMART0005.dbo.TEAM_CONFIG.TEAM_NAME ,SMART0005.dbo.USER_ONLILE_SECURITY.COR_MOBILE_NO,SMART0005.dbo.USER_ONLILE_SECURITY.USER_ROLE,SMART0005.dbo.ACC_LEDGER_Z_D_A.LEDGER_NAME_MERZE ,";
                        strSQL = strSQL + "SMART0005.dbo.USER_ONLILE_SECURITY.EMP_CARD_NO ,SMART0005.dbo.ACC_LEDGER_Z_D_A.TERITORRY_CODE TC ,SMART0005.dbo.ACC_LEDGER_Z_D_A.TERRITORRY_NAME TCNAME,SMART0005.dbo.USER_ONLILE_SECURITY.MPO_TYPE,SMART0005.dbo.USER_ONLILE_SECURITY.LIST_M_D_A, ";
                        strSQL = strSQL + "SMART0005.dbo.ACC_LEDGER_Z_D_A.ZONE,SMART0005.dbo.ACC_LEDGER_Z_D_A.DIVISION,SMART0005.dbo.ACC_LEDGER_Z_D_A.AREA,SMART0005.dbo.ACC_LEDGER_Z_D_A.LEDGER_NAME,SMART0005.dbo.USER_ONLILE_SECURITY.BRANCH_ID  ";
                        strSQL = strSQL + "FROM SMART0005.dbo.USER_ONLILE_SECURITY,SMART0005.dbo.ACC_LEDGER_Z_D_A,SMART0005.dbo.TEAM_CONFIG  WHERE  SMART0005.dbo.ACC_LEDGER_Z_D_A.MPO_CARD_NO =SMART0005.dbo.USER_ONLILE_SECURITY.EMP_CARD_NO  ";
                        strSQL = strSQL + "AND SMART0005.dbo.ACC_LEDGER_Z_D_A.ZONE =SMART0005.dbo.TEAM_CONFIG.ZONE_NAME ";
                        strSQL = strSQL + "AND (SMART0005.dbo.USER_ONLILE_SECURITY.USER_ID =" + Utility.Val(obj.strUserID) + " ";
                        strSQL = strSQL + "OR SMART0005.dbo.USER_ONLILE_SECURITY.EMP_CARD_NO ='" + obj.strCardNo + "')";
                    }
                    else if (strUserRole == "AH")
                    {

                        strSQL = "SELECT DISTINCT SMART0005.dbo.USER_ONLILE_SECURITY.BRANCH_ID,SMART0005.dbo.USER_ONLILE_SECURITY.USER_ID,SMART0005.dbo.USER_ONLILE_SECURITY.PASSWORD,SMART0005.dbo.TEAM_CONFIG.TEAM_NAME ,SMART0005.dbo.USER_ONLILE_SECURITY.COR_MOBILE_NO,SMART0005.dbo.USER_ONLILE_SECURITY.USER_ROLE,SMART0005.dbo.ACC_LEDGER_Z_D_A.AREA LEDGER_NAME_MERZE  ";
                        strSQL = strSQL + ",SMART0005.dbo.USER_ONLILE_SECURITY.EMP_CARD_NO ,'' TC ,'' TCNAME,SMART0005.dbo.USER_ONLILE_SECURITY.MPO_TYPE,SMART0005.dbo.USER_ONLILE_SECURITY.LIST_M_D_A, SMART0005.dbo.ACC_LEDGER_Z_D_A.ZONE,SMART0005.dbo.ACC_LEDGER_Z_D_A.DIVISION,SMART0005.dbo.ACC_LEDGER_Z_D_A.AREA ";
                        strSQL = strSQL + ",SMART0005.dbo.ACC_LEDGER_Z_D_A.AREA LEDGER_NAME,SMART0005.dbo.USER_ONLILE_SECURITY.BRANCH_ID FROM SMART0005.dbo.USER_ONLILE_SECURITY,SMART0005.dbo.ACC_LEDGERGROUP,SMART0005.dbo.ACC_LEDGER_Z_D_A,SMART0005.dbo.TEAM_CONFIG WHERE SMART0005.dbo.ACC_LEDGERGROUP.EMP_CARD_NO =SMART0005.dbo.USER_ONLILE_SECURITY.EMP_CARD_NO AND SMART0005.dbo.ACC_LEDGER_Z_D_A.AREA =SMART0005.dbo.ACC_LEDGERGROUP.GR_NAME ";
                        strSQL = strSQL + "AND SMART0005.dbo.ACC_LEDGER_Z_D_A.ZONE =SMART0005.dbo.TEAM_CONFIG.ZONE_NAME ";
                        strSQL = strSQL + "AND (SMART0005.dbo.USER_ONLILE_SECURITY.USER_ID =" + Utility.Val(obj.strUserID) + " ";
                        strSQL = strSQL + "OR SMART0005.dbo.USER_ONLILE_SECURITY.EMP_CARD_NO ='" + obj.strCardNo + "')";
                    }
                    else if (strUserRole == "DH")
                    {
                        strSQL = "SELECT DISTINCT SMART0005.dbo.USER_ONLILE_SECURITY.BRANCH_ID,SMART0005.dbo.USER_ONLILE_SECURITY.USER_ID,SMART0005.dbo.USER_ONLILE_SECURITY.PASSWORD,SMART0005.dbo.TEAM_CONFIG.TEAM_NAME ,SMART0005.dbo.USER_ONLILE_SECURITY.COR_MOBILE_NO,SMART0005.dbo.USER_ONLILE_SECURITY.USER_ROLE,SMART0005.dbo.ACC_LEDGER_Z_D_A.DIVISION LEDGER_NAME_MERZE  ";
                        strSQL = strSQL + ",SMART0005.dbo.USER_ONLILE_SECURITY.EMP_CARD_NO ,'' TC ,'' TCNAME,SMART0005.dbo.USER_ONLILE_SECURITY.MPO_TYPE,SMART0005.dbo.USER_ONLILE_SECURITY.LIST_M_D_A, SMART0005.dbo.ACC_LEDGER_Z_D_A.ZONE,'' AREA,SMART0005.dbo.ACC_LEDGER_Z_D_A.DIVISION ";
                        strSQL = strSQL + ",SMART0005.dbo.ACC_LEDGER_Z_D_A.DIVISION LEDGER_NAME,SMART0005.dbo.USER_ONLILE_SECURITY.BRANCH_ID FROM SMART0005.dbo.USER_ONLILE_SECURITY,SMART0005.dbo.ACC_LEDGERGROUP,SMART0005.dbo.ACC_LEDGER_Z_D_A,SMART0005.dbo.TEAM_CONFIG WHERE SMART0005.dbo.ACC_LEDGERGROUP.EMP_CARD_NO =SMART0005.dbo.USER_ONLILE_SECURITY.EMP_CARD_NO AND SMART0005.dbo.ACC_LEDGER_Z_D_A.DIVISION =SMART0005.dbo.ACC_LEDGERGROUP.GR_NAME ";
                        strSQL = strSQL + "AND SMART0005.dbo.ACC_LEDGER_Z_D_A.ZONE =SMART0005.dbo.TEAM_CONFIG.ZONE_NAME ";
                        strSQL = strSQL + "AND (SMART0005.dbo.USER_ONLILE_SECURITY.USER_ID =" + Utility.Val(obj.strUserID) + " ";
                        strSQL = strSQL + "OR SMART0005.dbo.USER_ONLILE_SECURITY.EMP_CARD_NO ='" + obj.strCardNo + "')";
                    }
                    else if (strUserRole == "ZH")
                    {
                        strSQL = "SELECT DISTINCT USER_ONLILE_SECURITY.BRANCH_ID,USER_ONLILE_SECURITY.USER_ID,USER_ONLILE_SECURITY.PASSWORD,TEAM_CONFIG.TEAM_NAME ,USER_ONLILE_SECURITY.COR_MOBILE_NO,USER_ONLILE_SECURITY.USER_ROLE,ACC_LEDGER_Z_D_A.DIVISION LEDGER_NAME_MERZE  ";
                        strSQL = strSQL + ",USER_ONLILE_SECURITY.EMP_CARD_NO ,'' TC ,'' TCNAME,USER_ONLILE_SECURITY.MPO_TYPE,USER_ONLILE_SECURITY.LIST_M_D_A, ACC_LEDGER_Z_D_A.ZONE,'' AREA,ACC_LEDGER_Z_D_A.DIVISION ";
                        strSQL = strSQL + ",ACC_LEDGER_Z_D_A.DIVISION LEDGER_NAME,SMART0005.dbo.USER_ONLILE_SECURITY.BRANCH_ID FROM USER_ONLILE_SECURITY,ACC_LEDGERGROUP,ACC_LEDGER_Z_D_A,TEAM_CONFIG WHERE ACC_LEDGERGROUP.EMP_CARD_NO =USER_ONLILE_SECURITY.EMP_CARD_NO AND ACC_LEDGER_Z_D_A.DIVISION =ACC_LEDGERGROUP.GR_NAME ";
                        strSQL = strSQL + "AND ACC_LEDGER_Z_D_A.ZONE =TEAM_CONFIG.ZONE_NAME ";
                        strSQL = strSQL + "AND (SMART0005.dbo.USER_ONLILE_SECURITY.USER_ID =" + Utility.Val(obj.strUserID) + " ";
                        strSQL = strSQL + "OR SMART0005.dbo.USER_ONLILE_SECURITY.EMP_CARD_NO ='" + obj.strCardNo + "')";
                    }
                    else
                    {
                        strSQL = "SELECT DISTINCT BRANCH_ID, USER_ID, PASSWORD,'' TEAM_NAME, COR_MOBILE_NO, USER_ROLE,'' LEDGER_NAME_MERZE,'' EMP_CARD_NO, '' AS TC, '' AS TCNAME, MPO_TYPE, LIST_M_D_A,'' ZONE, '' AS AREA,'' DIVISION,'' LEDGER_NAME ";
                        strSQL = strSQL + " FROM SMART0005.dbo.USER_ONLILE_SECURITY WHERE USER_ID =" + Utility.Val(obj.strUserID) + " ";
                       
                    
                    }
                    cmd.Connection = gcnMain;
                    cmd.CommandText = strSQL;
                    drGetGroup = cmd.ExecuteReader();
                    if (drGetGroup.Read())
                    {

                        //MPO ogrp = new MPO();
                        if (drGetGroup["BRANCH_ID"].ToString() != "")
                        {
                            strbid = drGetGroup["BRANCH_ID"].ToString();
                        }
                        //if (strbid != obj.branchid)
                        //{

                        //    ogrp.strResponse = "Branch ID Mismatch" + Environment.NewLine + " শাখা আইডি অমিল";
                        //}
                        //else
                        //{

                            ogrp.strbranchID = drGetGroup["BRANCH_ID"].ToString();
                            ogrp.strRole = drGetGroup["USER_ROLE"].ToString();
                            ogrp.strUserID = drGetGroup["USER_ID"].ToString();
                            ogrp.strUserPassword = drGetGroup["PASSWORD"].ToString();
                            ogrp.strTeritorryCode = drGetGroup["TC"].ToString();
                            ogrp.strTeritorryName = drGetGroup["TCNAME"].ToString();
                           
                            if (drGetGroup["LEDGER_NAME"].ToString() != "")
                            {
                                ogrp.strLedgerName = drGetGroup["LEDGER_NAME"].ToString();
                            }
                            else
                            {
                                ogrp.strLedgerName = "";
                            }
                            if (drGetGroup["LEDGER_NAME_MERZE"].ToString() != "")
                            {
                                ogrp.strMerzeName = drGetGroup["LEDGER_NAME_MERZE"].ToString();
                            }
                            else
                            {
                                ogrp.strMerzeName = "";
                            }
                            ogrp.intMpoType = Convert.ToInt16(drGetGroup["MPO_TYPE"].ToString());
                            if (drGetGroup["COR_MOBILE_NO"].ToString() != "")
                            {
                                ogrp.strMobileNo = drGetGroup["COR_MOBILE_NO"].ToString();
                            }
                            else
                            {
                                ogrp.strMobileNo = "";
                            }

                            ogrp.lngUniqueNo = Convert.ToInt16(drGetGroup["LIST_M_D_A"].ToString());
                            ogrp.strResponse = "Yes";
                            if (drGetGroup["EMP_CARD_NO"].ToString() != "")
                            {
                                ogrp.strCardNo = drGetGroup["EMP_CARD_NO"].ToString();
                                ogrp.strEMP_CARD_NO = drGetGroup["EMP_CARD_NO"].ToString();
                            }
                            else
                            {
                                ogrp.strCardNo = "";
                            }

                            if (drGetGroup["TEAM_NAME"].ToString() != "")
                            {
                                ogrp.strTeam = drGetGroup["TEAM_NAME"].ToString();
                            }
                            else
                            {
                                ogrp.strTeam = "";
                            }
                            if (drGetGroup["ZONE"].ToString() != "")
                            {
                                ogrp.strZone = drGetGroup["ZONE"].ToString();
                            }
                            else
                            {
                                ogrp.strZone = "";
                            }
                            if (drGetGroup["DIVISION"].ToString() != "")
                            {
                                ogrp.strDivision = drGetGroup["DIVISION"].ToString();
                            }
                            else
                            {
                                ogrp.strDivision = "";
                            }
                            if (drGetGroup["AREA"].ToString() != "")
                            {
                                ogrp.strArea = drGetGroup["AREA"].ToString();
                            }
                            else
                            {
                                ogrp.strArea = "";
                            }
                            if (strImage != null && strImage.Length > 0)
                            {
                                string base64String = Convert.ToBase64String(strImage); 
                                ogrp.strIamge = base64String; 
                            }


                            oogrp.Add(ogrp);
                    }

                    drGetGroup.Close();
                }
                else
                {


                    strSQL = "SELECT SMART0005.dbo.USER_CONFIG.USER_LOGIN_SERIAL,SMART0005.dbo.USER_CONFIG.USER_LOGIN_NAME,SMART0005.dbo.USER_CONFIG.USER_PASS,SMART0005.dbo.USER_CONFIG.USER_LEBEL,SMART0005.dbo.USER_CONFIG.USER_STATUS ";
                    strSQL = strSQL + "FROM SMART0005.dbo.USER_CONFIG,USER_PRIVILEGES_BRANCH WHERE SMART0005.dbo.USER_CONFIG.USER_LOGIN_NAME =SMART0005.dbo.USER_PRIVILEGES_BRANCH.USER_LOGIN_NAME ";
                    strSQL = strSQL + "AND SMART0005.dbo.USER_PRIVILEGES_BRANCH.USER_LOGIN_NAME='" + obj.strUserID + "' and SMART0005.dbo.USER_PRIVILEGES_BRANCH.BRANCH_ID IN (SELECT BRANCH_ID FROM SMART0005.dbo.USER_PRIVILEGES_BRANCH WHERE BRANCH_ID = '" + obj.branchid + "') ";
                    cmd.Connection = gcnMain;
                    cmd.CommandText = strSQL;
                    drGetGroup = cmd.ExecuteReader();
                    if (drGetGroup.Read())
                    {

                        if (drGetGroup["USER_STATUS"].ToString() == "S")
                        {
                            ogrp.strResponse = "Sorry, The User's has been suspended, Please contact with Administrator" + Environment.NewLine + " দুঃখিত, ব্যবহারকারীর স্থগিত করা হয়েছে, প্রশাসকের সাথে যোগাযোগ করুন";
                        }
                        string vstrPassword = Utility.Decrypt(drGetGroup["USER_PASS"].ToString(), drGetGroup["USER_LOGIN_NAME"].ToString()).ToString();

                        if (vstrPassword.Trim() != obj.strPassWord.Trim())
                        {
                            ogrp.strResponse = "Login failed. Make sure user name and password are correct." + Environment.NewLine + " লগইন ব্যর্থ. নিশ্চিত করুন যে ব্যবহারকারীর নাম এবং পাসওয়ার্ড সঠিক";
                            ogrp.strUserID = drGetGroup["USER_LOGIN_NAME"].ToString();
                            ogrp.strUserPassword = "";
                            ogrp.strbranchID = "";
                            ogrp.strTeritorryCode = "";
                            ogrp.strTeritorryName = "";
                            ogrp.strLedgerName = "";
                            ogrp.intMpoType = 0;
                            ogrp.strCardNo = "";
                          
                            oogrp.Add(ogrp);
                        }
                        else
                        {
                            ogrp.strUserID = drGetGroup["USER_LOGIN_NAME"].ToString();
                            ogrp.strUserPassword = "";
                            ogrp.strbranchID = "";
                            ogrp.strTeritorryCode = "";
                            ogrp.strTeritorryName = "";
                            ogrp.strLedgerName = "";
                            ogrp.intMpoType = 1;
                            ogrp.strResponse = "Yes";
                            ogrp.strCardNo = "";
                           
                              oogrp.Add(ogrp);
                        }

                    }
                    else
                    {
                        ogrp.strUserID = "";
                        ogrp.strUserPassword = "";
                        ogrp.strbranchID = "";
                        ogrp.strTeritorryCode = "";
                        ogrp.strTeritorryName = "";
                        ogrp.strLedgerName = "";
                        ogrp.intMpoType = 0;
                        ogrp.strCardNo = "";
                       

                        ogrp.strResponse = "Branch ID Mismatch" + Environment.NewLine + " শাখা আইডি অমিল";
                        oogrp.Add(ogrp);
                    }
                }
                drGetGroup.Close();
                gcnMain.Dispose();
                return oogrp;
            }
        }





       


     

        public string connstring { get; set; }

        public string strIamge { get; set; }

        public int bufLength { get; set; }
    }
}
