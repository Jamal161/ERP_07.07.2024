using Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Api.Controllers
{
    [RoutePrefix("api/ShfitConfig")]
    public class ShiftConfigController : ApiController
    {

        DAL objDal = new DAL();
        [HttpGet]
        [Route("GetShfit")]
        public IHttpActionResult GetShfit()
        {
            List<ShiftCongfig> ooList=objDal.mGetHeader1();
            return Json(ooList);
        }

        [HttpPost]
        [Route("SaveAttendance")]
        public IHttpActionResult SaveAttendance(AttendentCongfig obj)
        {

            string  ooList = objDal.mPostSaveAttendace(obj);
            return Json(ooList);
        }


        [HttpGet]
        [Route("mGetAttendance")]
        public IHttpActionResult mGetAttendance()
        {
            List<AttendentCongfig> ooList = objDal.mGetHeader2();
            return Json(ooList);
        }

        [HttpPost]
        [Route("mGetEmp")]
        public IHttpActionResult mGetEmp(AttendentCongfig obj)
        {
            List<AttendentCongfig> ooList = new List<AttendentCongfig>();
            ooList = objDal.mGetUserCard(obj);
            return Json(ooList);
        }

        [HttpPost]
        [Route("mGetUserReturnVal")]
        public IHttpActionResult mGetUserReturnVal(AttendentCongfig obj)
        {

            List<AttendentCongfig> ooList = new List<AttendentCongfig>();
            ooList =objDal. mGetUserReturn(obj);
            return Json(ooList);
        }


        [HttpGet]
        [Route("mGetTotalLeave")]
        public IHttpActionResult mGetTotalLeave()
        {
            List<LeaveConfig> ooList = objDal.mGetLeave();
            return Json(ooList);
        }


        [HttpGet]
        [Route("mGetTotalHolyDay")]
        public IHttpActionResult mGetTotalHolyDay()
        {
            List<LeaveConfig> ooList = objDal.mGetHoly();
            return Json(ooList);
        }


          [HttpPost]
          [Route("mPostLeave")]
        public IHttpActionResult mPostLeave(Leavaes obj)
        {

            string ooList = objDal.mPostUserLeave(obj);
            return Json(ooList);
        }


         

          [HttpPost]
          [Route("mGetUserLeave")]
          public IHttpActionResult mGetUserLeave(LeaveConfig obj)
          {
              try
              {
                  List<LeaveConfig> ooList = objDal.mGetUserLeaveReturn(obj);
                  return Json(ooList);
              }
              catch (Exception ex)
              {
                  
                  return InternalServerError(ex);
              }
          }




          [HttpPost]
          [Route("mPostWeekend")]
          public IHttpActionResult mPostWeekend(LeaveConfig obj)
          {

              string ooList = objDal.mPostUserWeekend(obj);
              return Json(ooList);
          }



          [HttpPost]
          [Route("mGetUserWeekendList")]
          public IHttpActionResult mGetUserWeekendList(LeaveConfig obj)
          {
              List<LeaveConfig> ooList = new List<LeaveConfig>();
              ooList = objDal.mGetUserWeekendReturn(obj);
              return Json(ooList);
          }



          [HttpGet]
          [Route("mGetTourList")]
          public IHttpActionResult mGetTourList(TourConfig obj)
          {
              List<TourConfig> ooList = objDal.mGetTour(obj);
              return Json(ooList);
          }


          [HttpPost]
          [Route("mGetTourLedgerList")]
          public IHttpActionResult mGetTourLedgerList(TourConfigLedger obj)
          {
              List<TourConfigLedger> ooList = objDal.mGetTourLedger(obj);
              return Json(ooList);
          }


          [HttpGet]
          [Route("mGetTourPlanList")]
          public IHttpActionResult mGetTourPlanList(TourPlanConfig obj)
          {
              List<TourPlanConfig> ooList = objDal.mGetTourPlan(obj);
              return Json(ooList);
          }




          [HttpPost]
          [Route("mPostTourPlanRouteList")]
          public IHttpActionResult mPostTourPlanRouteList(List<TourPlanShiftConfig> tourPlans)
          {
              List<TourPlanShiftConfig> ooList = new List<TourPlanShiftConfig>();
              string i = objDal.mPostTourPlanRoute(tourPlans);
              return Json(ooList);
          }




          [HttpPost]
          [Route("mGetTourPlanRouteReturnValue")]
          public IHttpActionResult mGetTourPlanRouteReturnValue(TourPlanShiftConfig obj)
          {
              List<TourPlanShiftConfig> ooList = new List<TourPlanShiftConfig>();
              ooList = objDal.mGetTourPlanRouteReturn(obj);
              return Json(ooList);
          }


          [HttpGet]
          [Route("mGetTeritorryList")]
          public IHttpActionResult mGetTeritorryList(PrescriptionConfigTeritorry obj)
          {
              List<PrescriptionConfigTeritorry> ooList = objDal.mGetTeritorry(obj);
              return Json(ooList);
          }



          [HttpPost]
          [Route("mPostPrescriptionSlip")]
          public IHttpActionResult mPostPrescriptionSlip(PrescriptionConfig obj)
          {

              string ooList = objDal.mPostPrescription(obj);
              return Json(ooList);
          }



          [HttpGet]
          [Route("mGetPrescriptionList")]
          public IHttpActionResult mGetPrescriptionList(PrescriptionConfig obj)
          {
              List<PrescriptionConfig> ooList = objDal.mGetPrescription(obj);
              return Json(ooList);
          }





          [HttpGet]
          [Route("mGetNoticeList")]
          public IHttpActionResult mGetNoticeList(Notice obj)
          {
              List<Notice> ooList = objDal.mGetNotice(obj);
              return Json(ooList);
          }


          [HttpGet]
          [Route("mGetTrainingList")]
          public IHttpActionResult mGetTrainingList(Training obj)
          {
              List<Training> ooList = objDal.mGetTrain(obj);
              return Json(ooList);
          }




          [HttpGet]
          [Route("mGetExamList")]
          public IHttpActionResult mGetExamList(Exam obj)
          {
              List<Exam> ooList = objDal.mGetExam(obj);
              return Json(ooList);
          }



          [HttpPost]
          [Route("mPostExamResults")]
          public IHttpActionResult mPostExamResults(ExamResult obj)
          {

              string ooList = objDal.mPostExamResult(obj);
              return Json(ooList);
          }




          [HttpPost]
          [Route("mGetdailyTaskList")]
          public IHttpActionResult mGetdailyTaskList(DailyTask obj)
          {
              try
              {
                  List<DailyTask> ooList = objDal.mGetdailyTask(obj);
                  return Json(ooList);
              }
              catch (Exception ex)
              {

                  return InternalServerError(ex);
              }
          }



          [HttpGet]
          [Route("mGetDoctorLedgerList")]
          public IHttpActionResult mGetDoctorLedgerList(Doctorlist obj)
          {
              List<Doctorlist> ooList = objDal.mGetDoctorLedger(obj);
              return Json(ooList);
          }



          [HttpPost]
          [Route("mPostDoctorVisits")]
          public IHttpActionResult mPostDoctorVisits(List<DoctorVisit> objs)
          {
              List<DoctorVisit> ooList = new List<DoctorVisit>();
              string i = objDal.mPostDoctorVisit(objs);
              return Json(ooList);
          }


          [HttpGet]
          [Route("mGetDoctorLedgerTypeList")]
          public IHttpActionResult mGetDoctorLedgerTypeList(DoctorVisitType obj)
          {
              List<DoctorVisitType> ooList = objDal.mGetDoctorLedgerType(obj);
              return Json(ooList);
          }



         [HttpPost]
         [Route("mPostDoctorVisitssslist")]
         public IHttpActionResult mPostDoctorVisitssslist(DoctorVisit obj)
         {
             try
             {
                 List<DoctorVisit> ooList = objDal.mPostDoctorVisitsss(obj);
                 return Json(ooList);
             }
             catch (Exception ex)
             {

                 return InternalServerError(ex);
             }
         }




          [HttpGet]
          [Route("mGetProductNameList")]
         public IHttpActionResult mGetProductNameList(ProductName obj)
          {
              List<ProductName> ooList = objDal.mGetProductName(obj);
              return Json(ooList);
          }




          [HttpPost]
          [Route("mPostTadaClaims")]
          public IHttpActionResult mPostTadaClaims(TadaClaim obj)
          {

              string ooList = objDal.mPostTadaClaim(obj);
              return Json(ooList);
          }




          [HttpPost]
          [Route("gstrGetMpoAreaDevisionLists")]
          public IHttpActionResult gstrGetMpoAreaDevisionLists(MPO obj)
          {
              try
              {
                  List<MPO> ooList = objDal.gstrGetMpoAreaDevisionList(obj);
                  return Json(ooList);
              }
              catch (Exception ex)
              {

                  return InternalServerError(ex);
              }
          }


         
    }

    
}


