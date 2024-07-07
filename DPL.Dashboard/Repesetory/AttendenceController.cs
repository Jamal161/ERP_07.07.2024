using DPL.DASHBOARD.Models;
using Dutility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DPL.DASHBOARD.Repesetory
{
    public class AttendenceController : Controller
    {
        private decimal floPercentage;
        private decimal dblPercentage;
      
        

        public ActionResult Index()
        {
            return View();
        }


        public ActionResult mPostShiftConfig()
        {

            var allLedger = "";
            return Json(allLedger, JsonRequestBehavior.AllowGet);
        }

        public ActionResult mGetAttendance()
        {

            var allLedger = mGetHeader2();
            return Json(allLedger, JsonRequestBehavior.AllowGet);
        }
        public ActionResult mGetUserReturnVal(AttendentCongfig obj )
        {
            List<AttendentCongfig> UserList = new List<AttendentCongfig>();
            UserList = mGetUserReturn(obj);
            return Json(UserList, JsonRequestBehavior.AllowGet);
        }

        public ActionResult mGetEmp()
        {

            var allLedger = mGetHeader3();
            return Json(allLedger, JsonRequestBehavior.AllowGet);
        }


        public ActionResult mUpdateConfig()
        {

            var allLedger = "" ;
            return Json(allLedger, JsonRequestBehavior.AllowGet);
        }


        public ActionResult mGetTotal(AttendentCongfig obj)
        {

            var allLedger = mGetUserTotal(obj);
            return Json(allLedger, JsonRequestBehavior.AllowGet);
        }


        public ActionResult mGetPercent(AttendentCongfig obj)
        {

            var allLedger = mGetPercentage(obj);
            return Json(allLedger, JsonRequestBehavior.AllowGet);
        }



        public ActionResult mGetDateRange(AttendentCongfig obj)
        {

            var allLedger = mGetDate(obj);
            return Json(allLedger, JsonRequestBehavior.AllowGet);
        }


        public List<AttendentCongfig> mGetHeader2()
        {
            string strSQL = null;
            string connectionString = Utility.SQLConnstringComSwitch("0001");

            using (SqlConnection gcnMain = new SqlConnection(connectionString))
            {
                if (gcnMain.State == ConnectionState.Open)
                {
                    gcnMain.Close();
                }
                gcnMain.Open();
                SqlDataReader dr;

                List<AttendentCongfig> oooChequePrint = new List<AttendentCongfig>();

                strSQL = "SELECT ATTEN_SERIAL, EMP_IMAGE,USER_NAME, ROLE, EMP_CARD_NO, ";
                strSQL = strSQL + "TC, ATTEN_DATEIN, ATTEN_TIMEIN, LATITUDE, LONGITUDE, ADDRESS, DISTANCE,TOTAL_WORKING_HOUR, STAY_HOUR, ATTEN_TIMEOUT, ATTEN_STATUS,ATTEN_SHIFT, SHIFT_START , ATTEN_COMMENTS ,ACTION,EMP_JPEG_DOC  ";
                strSQL = strSQL + "FROM           HRS_TRANS_WORK_ATTENDANCE_NEW ";


                SqlCommand cmd = new SqlCommand(strSQL, gcnMain);
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {

                    AttendentCongfig oLedg = new AttendentCongfig();
                    oLedg.intATTEN_SERIAL = Convert.ToInt32(dr["ATTEN_SERIAL"]);
                    oLedg.strUSER_NAME = dr["USER_NAME"].ToString();
                    oLedg.strROLE = dr["ROLE"].ToString();
                    oLedg.strEMP_CARD_NO = dr["EMP_CARD_NO"].ToString();
                    oLedg.strTC = dr["TC"].ToString();
                    oLedg.strATTEN_DATEIN = dr["ATTEN_DATEIN"].ToString();
                    oLedg.strATTEN_TIMEIN = dr["ATTEN_TIMEIN"].ToString();
                    oLedg.strLATITUDE = dr["LATITUDE"].ToString();
                    oLedg.strLONGITUDE = dr["LONGITUDE"].ToString();
                    oLedg.strADDRESS = dr["ADDRESS"].ToString();
                    oLedg.intDISTANCE = Convert.ToInt32(dr["DISTANCE"]);
                    oLedg.intTOTAL_WORKING_HOUR = Convert.ToInt32(dr["TOTAL_WORKING_HOUR"]);
                    oLedg.strSTAY_HOUR = dr["STAY_HOUR"].ToString();
                    oLedg.strATTEN_TIMEOUT = dr["ATTEN_TIMEOUT"].ToString();
                    oLedg.strATTEN_STATUS = dr["ATTEN_STATUS"].ToString();
                    oLedg.strATTEN_SHIFT = dr["ATTEN_SHIFT"].ToString();
                    oLedg.strSHIFT_START = dr["SHIFT_START"].ToString();
                    oLedg.strATTEN_COMMENTS = dr["ATTEN_COMMENTS"].ToString();
                    oLedg.strACTION = dr["ACTION"].ToString();
                    oLedg.strEMP_IMAGE = dr["EMP_IMAGE"].ToString();
                   
             
                    oooChequePrint.Add(oLedg);
                }

                if (!dr.HasRows)
                {
                    AttendentCongfig oLedg = new AttendentCongfig();
                    oLedg.intATTEN_SERIAL = 0;
                    oLedg.strUSER_NAME = "";
                    oLedg.strROLE = "";
                    oLedg.strEMP_CARD_NO = "";
                    oLedg.strTC = "";
                    oLedg.strATTEN_DATEIN = "";
                    oLedg.strATTEN_TIMEIN = "";
                    oLedg.strLATITUDE = "";
                    oLedg.strLONGITUDE = "";
                    oLedg.strADDRESS = "";
                    oLedg.intDISTANCE = 0;
                    oLedg.intTOTAL_WORKING_HOUR = 0;
                    oLedg.strSTAY_HOUR = "";
                    oLedg.strATTEN_TIMEOUT = "";
                    oLedg.strATTEN_STATUS = "";
                    oLedg.strATTEN_SHIFT = "";
                    oLedg.strSHIFT_START = "";
                    oLedg.strATTEN_COMMENTS = "";
                    oLedg.strACTION = "";
                    //oLedg.strEMP_IMAGE = "";
                    oLedg.strEMP_JPEG_DOC = ""; 
                    oooChequePrint.Add(oLedg);
                }
                dr.Close();
                gcnMain.Close();
                cmd.Dispose();
                return oooChequePrint;
            }

        }

        public Image byteArrayToImage(byte[] byteArrayIn)
        {

            MemoryStream ms = new MemoryStream(byteArrayIn, 0, byteArrayIn.Length);
            ms.Write(byteArrayIn, 0, byteArrayIn.Length);
            Image returnImage = Image.FromStream(ms, true);//Exception occurs here
            return returnImage;

        }
        
        [HttpPost]
        public string mPostShiftConfig(AttendentCongfig obj)
        {
            string strSQL = null;
            string connectionString = Utility.SQLConnstringComSwitch("0001");

            using (SqlConnection gcnMain = new SqlConnection(connectionString))
            {
                if (gcnMain.State == ConnectionState.Open)
                {
                    gcnMain.Close();
                }
                try
                {
                    gcnMain.Open();



                    SqlCommand cmdInsert = new SqlCommand();
                    SqlTransaction myTrans;
                    myTrans = gcnMain.BeginTransaction();
                    cmdInsert.Connection = gcnMain;
                    cmdInsert.Transaction = myTrans;
                    strSQL = "INSERT INTO HRS_TRANS_WORK_ATTENDANCE_NEW (";
                    strSQL = strSQL + " USER_NAME, ROLE,EMP_CARD_NO, ";
                    strSQL = strSQL + "TC, ATTEN_DATEIN, ";
                    strSQL = strSQL + " ATTEN_TIMEIN, LATITUDE , LONGITUDE , ADDRESS , DISTANCE, TOTAL_WORKING_HOUR ,STAY_HOUR,ATTEN_TIMEOUT,ATTEN_STATUS,ATTEN_SHIFT,SHIFT_START,ATTEN_COMMENTS, ACTION, EMP_IMAGE , EMP_JPEG_DOC ";
                    strSQL = strSQL + ") ";
                    strSQL = strSQL + "VALUES (";
                    strSQL = strSQL + "'" + obj.strUSER_NAME + "',";
                    strSQL = strSQL + "'" + obj.strROLE + "',";
                    strSQL = strSQL + "'" + obj.strEMP_CARD_NO + "',";
                    strSQL = strSQL + "'" + obj.strTC + "',";
                    strSQL = strSQL + "'" + obj.strATTEN_DATEIN + "',";
                    strSQL = strSQL + "'" + obj.strATTEN_TIMEIN + "',";
                    strSQL = strSQL + "'" + obj.strLATITUDE + "',";
                    strSQL = strSQL + "'" + obj.strLONGITUDE + "',";
                    strSQL = strSQL + "'" + obj.strADDRESS + "',";
                    strSQL = strSQL + "'" + obj.intDISTANCE + "',";
                    strSQL = strSQL + "'" + obj.intTOTAL_WORKING_HOUR + "',";
                    strSQL = strSQL + "'" + obj.strSTAY_HOUR + "',";
                    strSQL = strSQL + "'" + obj.strATTEN_TIMEOUT + "',";
                    strSQL = strSQL + "'" + obj.strATTEN_STATUS + "',";
                    strSQL = strSQL + "'" + obj.strATTEN_SHIFT + "',";
                    strSQL = strSQL + "'" + obj.strSHIFT_START + "',";
                    strSQL = strSQL + "'" + obj.strATTEN_COMMENTS + "',";
                    strSQL = strSQL + "'" + obj.strACTION + "',";
                    strSQL = strSQL + "'" + obj.strEMP_IMAGE + "',";
                    strSQL = strSQL + "'" + obj.strEMP_JPEG_DOC + "'";
                    strSQL = strSQL + ")";
                    cmdInsert.CommandText = strSQL;
                    cmdInsert.ExecuteNonQuery();
                    cmdInsert.Transaction.Commit();
                    return "1";
                }
                catch (SqlException ex)
                {
                    return ex.Message.ToString();
                }
                finally
                {
                    gcnMain.Dispose();

                }
            }

        }






        public List<AttendentCongfig> mGetHeader3()
        {
            string strSQL = null;
            string connectionString = Utility.SQLConnstringComSwitch("0001");

            using (SqlConnection gcnMain = new SqlConnection(connectionString))
            {
                if (gcnMain.State == ConnectionState.Open)
                {
                    gcnMain.Close();
                }
                gcnMain.Open();
                SqlDataReader dr;

                List<AttendentCongfig> oooChequePrint = new List<AttendentCongfig>();

                strSQL = "select * from HRS_EMPLOYEE where EMP_CARD_NO='M-12382' ";

                SqlCommand cmd = new SqlCommand(strSQL, gcnMain);
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {

                    AttendentCongfig oLedg = new AttendentCongfig();

                    oLedg.strEMP_B_NAME = dr["EMP_CARD_NO"].ToString();
                   
                   
                   


                    oooChequePrint.Add(oLedg);
                }

                if (!dr.HasRows)
                {
                    AttendentCongfig oLedg = new AttendentCongfig();
                   
                    oLedg.strEMP_CARD_NO = "";
                   
                    oLedg.strATTEN_DATEIN = "";
                   
                    oooChequePrint.Add(oLedg);
                }
                dr.Close();
                gcnMain.Close();
                cmd.Dispose();
                return oooChequePrint;
            }

        }





         [HttpPost]
        public string mUpdateConfig(AttendentCongfig obj)
        {
            string strSQL = null;
            string connectionString = Utility.SQLConnstringComSwitch("0001");

            using (SqlConnection gcnMain = new SqlConnection(connectionString))
            {
                if (gcnMain.State == ConnectionState.Open)
                {
                    gcnMain.Close();
                }
                try
                {
                    gcnMain.Open();



                    SqlCommand cmdInsert = new SqlCommand();
                    SqlTransaction myTrans;
                    myTrans = gcnMain.BeginTransaction();
                    cmdInsert.Connection = gcnMain;
                    cmdInsert.Transaction = myTrans;
                
                    strSQL = "UPDATE HRS_TRANS_WORK_ATTENDANCE_NEW SET ACTION='" + obj.strACTION + "' ";
                    strSQL = strSQL + "WHERE ATTEN_SERIAL='" + obj.intATTEN_SERIAL + "'";
                    cmdInsert.CommandText = strSQL;
                    cmdInsert.ExecuteNonQuery();



                    cmdInsert.Transaction.Commit();
                    return "1";
                }
                catch (SqlException ex)
                {
                    return ex.Message.ToString();
                }
                finally
                {
                    gcnMain.Dispose();

                }
            }

        }


        [HttpPost]
         public List<AttendentCongfig> mGetUserReturn(AttendentCongfig obj)
         {
             string strSQL = null;
             string connectionString = Utility.SQLConnstringComSwitch("0001");

             using (SqlConnection gcnMain = new SqlConnection(connectionString))
             {
                 if (gcnMain.State == ConnectionState.Open)
                 {
                     gcnMain.Close();
                 }
                 gcnMain.Open();
                 SqlDataReader dr;

                 List<AttendentCongfig> oooChequePrint = new List<AttendentCongfig>();

                 strSQL = "select * from HRS_TRANS_WORK_ATTENDANCE_NEW where EMP_CARD_NO='" + obj.strEMP_CARD_NO + "' AND ATTEN_DATEIN='" + obj.strATTEN_DATEIN + "' ";

                 SqlCommand cmd = new SqlCommand(strSQL, gcnMain);
                 dr = cmd.ExecuteReader();
                 while (dr.Read())
                 {

                     AttendentCongfig oLedg = new AttendentCongfig();

                     oLedg.intATTEN_SERIAL = Convert.ToInt32(dr["ATTEN_SERIAL"]);
                     oLedg.strUSER_NAME = dr["USER_NAME"].ToString();
                     oLedg.strROLE = dr["ROLE"].ToString();
                     oLedg.strEMP_CARD_NO = dr["EMP_CARD_NO"].ToString();
                     oLedg.strTC = dr["TC"].ToString();
                     oLedg.strATTEN_DATEIN = dr["ATTEN_DATEIN"].ToString();
                     oLedg.strATTEN_TIMEIN = dr["ATTEN_TIMEIN"].ToString();
                     oLedg.strLATITUDE = dr["LATITUDE"].ToString();
                     oLedg.strLONGITUDE = dr["LONGITUDE"].ToString();
                     oLedg.strADDRESS = dr["ADDRESS"].ToString();
                     oLedg.intDISTANCE = Convert.ToInt32(dr["DISTANCE"]);
                     oLedg.intTOTAL_WORKING_HOUR = Convert.ToInt32(dr["TOTAL_WORKING_HOUR"]);
                     oLedg.strSTAY_HOUR = dr["STAY_HOUR"].ToString();
                     oLedg.strATTEN_TIMEOUT = dr["ATTEN_TIMEOUT"].ToString();
                     oLedg.strATTEN_STATUS = dr["ATTEN_STATUS"].ToString();
                     oLedg.strATTEN_SHIFT = dr["ATTEN_SHIFT"].ToString();
                     oLedg.strSHIFT_START = dr["SHIFT_START"].ToString();
                     oLedg.strATTEN_COMMENTS = dr["ATTEN_COMMENTS"].ToString();
                     oLedg.strACTION = dr["ACTION"].ToString();
                     oLedg.strEMP_IMAGE = dr["EMP_IMAGE"].ToString();

                     oooChequePrint.Add(oLedg);
                 }

                 if (!dr.HasRows)
                 {
                     AttendentCongfig oLedg = new AttendentCongfig();

                     oLedg.intATTEN_SERIAL = 0;
                     oLedg.strUSER_NAME = "";
                     oLedg.strROLE = "";
                     oLedg.strEMP_CARD_NO = "";
                     oLedg.strTC = "";
                     oLedg.strATTEN_DATEIN = "";
                     oLedg.strATTEN_TIMEIN = "";
                     oLedg.strLATITUDE = "";
                     oLedg.strLONGITUDE = "";
                     oLedg.strADDRESS = "";
                     oLedg.intDISTANCE = 0;
                     oLedg.intTOTAL_WORKING_HOUR = 0;
                     oLedg.strSTAY_HOUR = "";
                     oLedg.strATTEN_TIMEOUT = "";
                     oLedg.strATTEN_STATUS = "";
                     oLedg.strATTEN_SHIFT = "";
                     oLedg.strSHIFT_START = "";
                     oLedg.strATTEN_COMMENTS = "";
                     oLedg.strACTION = "";
                     //oLedg.strEMP_IMAGE = "";
                     oLedg.strEMP_JPEG_DOC = ""; 

                     oooChequePrint.Add(oLedg);
                 }
                 dr.Close();
                 gcnMain.Close();
                 cmd.Dispose();
                 return oooChequePrint;
             }

         }

        public List<AttendentCongfig> mGetUserTotal(AttendentCongfig obj)
        {
            string strSQL = null;
            string connectionString = Utility.SQLConnstringComSwitch("0001");

            using (SqlConnection gcnMain = new SqlConnection(connectionString))
            {
                if (gcnMain.State == ConnectionState.Open)
                {
                    gcnMain.Close();
                }
                gcnMain.Open();
                SqlDataReader dr;

                List<AttendentCongfig> oooChequePrint = new List<AttendentCongfig>();

                strSQL = "SELECT COUNT(*) as countt,COUNT(EMP_CARD_NO)EMP_CARD_NO,COUNT(USER_NAME),ROLE from HRS_TRANS_WORK_ATTENDANCE_NEW    Group by ROLE";


                SqlCommand cmd = new SqlCommand(strSQL, gcnMain);
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {

                    AttendentCongfig oLedg = new AttendentCongfig();
                    oLedg.intCountt = Convert.ToInt32(dr["Countt"]);
                    oLedg.strEMP_CARD_NO = dr["EMP_CARD_NO"].ToString();
                    oLedg.strROLE = dr["ROLE"].ToString();


                    oooChequePrint.Add(oLedg);
                }

                if (!dr.HasRows)
                {
                    AttendentCongfig oLedg = new AttendentCongfig();


                    oLedg.intCountt = 0;
                    oLedg.strEMP_CARD_NO = "";
                    oLedg.strROLE = "";



                    oooChequePrint.Add(oLedg);
                }
                dr.Close();
                gcnMain.Close();
                cmd.Dispose();
                return oooChequePrint;
            }

        }


        public List<AttendentCongfig> mGetPercentage(AttendentCongfig obj)
        {
            string strSQL = null;
            string connectionString = Utility.SQLConnstringComSwitch("0001");

            using (SqlConnection gcnMain = new SqlConnection(connectionString))
            {
                if (gcnMain.State == ConnectionState.Open)
                {
                    gcnMain.Close();
                }
                gcnMain.Open();
                SqlDataReader dr;

                List<AttendentCongfig> oooChequePrint = new List<AttendentCongfig>();

            strSQL = "select  ROLE, count(case when ACTION='Approve' then 1 end) as present, count(case when ACTION='Reject' then 1 end) as absent, ";
            strSQL = strSQL +" count(case when ACTION='' then 1 end) as leave,COUNT(*) as total ";
            strSQL = strSQL + "from HRS_TRANS_WORK_ATTENDANCE_NEW  group by ROLE ";

           
                SqlCommand cmd = new SqlCommand(strSQL, gcnMain);
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    double dblPresent = 0;
                    double dbltotal = 0;
                    double dblPre = 0;

                    

                    AttendentCongfig oLedg = new AttendentCongfig();
                    oLedg.intPresent = Convert.ToInt32(dr["present"]);
                    dblPresent = Convert.ToDouble(dr["present"]);
                    oLedg.intAbsent = Convert.ToInt32(dr["absent"]);
                    oLedg.intLeave = Convert.ToInt32(dr["leave"]);
                    oLedg.intTotal = Convert.ToInt32(dr["total"]);
                    dbltotal = Convert.ToDouble(dr["total"]);
                    dblPre = ((dblPresent / dbltotal) * 100);
                    oLedg.dblPercentage = (double)Math.Round(dblPre);
                    
                    oLedg.strROLE = dr["ROLE"].ToString();


                    oooChequePrint.Add(oLedg);
                }

                if (!dr.HasRows)
                {
                    AttendentCongfig oLedg = new AttendentCongfig();


                    oLedg.intPresent = 0;
                    oLedg.intAbsent = 0;
                    oLedg.intLeave = 0;
                    oLedg.intTotal = 0;
                    oLedg.intCountt = 0;
                    oLedg.dblPercentage = 0;
                    oLedg.strROLE = "";



                    oooChequePrint.Add(oLedg);
                }
                dr.Close();
                gcnMain.Close();
                cmd.Dispose();
                return oooChequePrint;
            }

        }



        public List<AttendentCongfig> mGetDate(AttendentCongfig obj)
        {
            string strSQL = null;
            string connectionString = Utility.SQLConnstringComSwitch("0001");

            using (SqlConnection gcnMain = new SqlConnection(connectionString))
            {
                if (gcnMain.State == ConnectionState.Open)
                {
                    gcnMain.Close();
                }
                gcnMain.Open();
                SqlDataReader dr;

                List<AttendentCongfig> oooChequePrint = new List<AttendentCongfig>();

                strSQL = "SELECT * FROM HRS_TRANS_WORK_ATTENDANCE_NEW WHERE ATTEN_DATEIN BETWEEN  CONVERT(datetime,'" + obj.strATTEN_DATEINFROM + "' ,102) and CONVERT(datetime,'" + obj.strATTEN_DATEINTO + "',102)";
            
               //" + obj.strATTEN_DATEIN + "

                SqlCommand cmd = new SqlCommand(strSQL, gcnMain);
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                   

                    AttendentCongfig oLedg = new AttendentCongfig();


                    oLedg.strATTEN_DATEIN = dr["ATTEN_DATEIN"].ToString();


                    oooChequePrint.Add(oLedg);
                }

                if (!dr.HasRows)
                {
                    AttendentCongfig oLedg = new AttendentCongfig();



                    oLedg.strATTEN_DATEIN = "";



                    oooChequePrint.Add(oLedg);
                }
                dr.Close();
                gcnMain.Close();
                cmd.Dispose();
                return oooChequePrint;
            }

        }




        
    }



}



