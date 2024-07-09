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




        public ActionResult mGetKibriadatazone()
        {

            var allLedger = "" ;
            return Json(allLedger, JsonRequestBehavior.AllowGet);
        }

        public ActionResult mGetKibriadatadiv()
        {

            var allLedger = "";
            return Json(allLedger, JsonRequestBehavior.AllowGet);
        }

        public ActionResult mGetKibriadataarea()
        {

            var allLedger = "";
            return Json(allLedger, JsonRequestBehavior.AllowGet);
        }
       
           public ActionResult  mGetKibriadatamarket()
        {

            var allLedger = "";
            return Json(allLedger, JsonRequestBehavior.AllowGet);
        }


           public ActionResult mGetKibriadataroute()
        {

            var allLedger = "";
            return Json(allLedger, JsonRequestBehavior.AllowGet);
        }

     

        //public ActionResult mGetTotal()
        //{
        //    List<AttendentCongfig> allLedger = new List<AttendentCongfig>();
        //    string vstrRole = Session["UserRole"].ToString();
        //    string vstrUserID = Session["userID"].ToString();
        //    string vstrLedgerName = Session["userName"].ToString();
        //    string vstrCardNo = Session["userCardNo"].ToString();
        //    if (vstrRole.ToUpper() == "AH")
        //    {
        //        allLedger = mGetUserTotalAH(vstrLedgerName);
        //    }
        //    else if (vstrRole.ToUpper() == "DH")
        //    {
        //        allLedger = mGetUserTotalDH(vstrLedgerName);
        //    }
        //    else
        //    {
        //        allLedger = mGetUserTotal();
        //    }
        //    //return Json(allLedger, JsonRequestBehavior.AllowGet);
        //    var jsonResult = Json(allLedger, JsonRequestBehavior.AllowGet);
        //    jsonResult.MaxJsonLength = int.MaxValue;
        //    return jsonResult;
        //}



        public ActionResult mGetKibriadatalist(kibria obj)
        {

            var allLedger = mGetKibriadata(obj);
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


        public ActionResult mGetTeamList(Team obj)
        {

            var allLedger = mGetTeam(obj);
            return Json(allLedger, JsonRequestBehavior.AllowGet);
        }






        public ActionResult mGetZoneList(Zone obj)
        {

            var allLedger = mGetZone(obj);
            return Json(allLedger, JsonRequestBehavior.AllowGet);
        }


        
        //public ActionResult mGetDateRange()
        //{
        //    AttendentCongfig obj;
        //    //var allLedger = mGetDate(obj);
        //    return Json("", JsonRequestBehavior.AllowGet);
        //}

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

                strSQL = "SELECT  ATTEN_SERIAL, USER_NAME, ROLE, EMP_CARD_NO, ";
                strSQL = strSQL + " ATTEN_DATEIN, ATTEN_TIMEIN, LATITUDE, LONGITUDE, ADDRESS, DISTANCE, ATTEN_STATUS,ATTEN_SHIFT, ATTEN_COMMENTS ,ACTION,EMP_IMAGE  ";
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

                    oLedg.strATTEN_DATEIN = Convert.ToDateTime(dr["ATTEN_DATEIN"]).ToString("yyyy-MM-dd");
                    oLedg.strATTEN_TIMEIN = dr["ATTEN_TIMEIN"].ToString();
                    oLedg.strLATITUDE = dr["LATITUDE"].ToString();
                    oLedg.strLONGITUDE = dr["LONGITUDE"].ToString();
                    oLedg.strADDRESS = dr["ADDRESS"].ToString();
                    oLedg.intDISTANCE = Convert.ToInt32(dr["DISTANCE"]);

                    oLedg.strATTEN_STATUS = dr["ATTEN_STATUS"].ToString();
                    oLedg.strATTEN_SHIFT = dr["ATTEN_SHIFT"].ToString();

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

                    oLedg.strATTEN_DATEIN = "";
                    oLedg.strATTEN_TIMEIN = "";
                    oLedg.strLATITUDE = "";
                    oLedg.strLONGITUDE = "";
                    oLedg.strADDRESS = "";
                    oLedg.intDISTANCE = 0;

                    oLedg.strATTEN_STATUS = "";
                    oLedg.strATTEN_SHIFT = "";

                    oLedg.strATTEN_COMMENTS = "";
                    oLedg.strACTION = "";
                    oLedg.strEMP_IMAGE = "";
                    //oLedg.strEMP_JPEG_DOC = "";
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
                    strSQL = strSQL + " ATTEN_DATEIN, ";
                    strSQL = strSQL + " ATTEN_TIMEIN, LATITUDE , LONGITUDE , ADDRESS , DISTANCE, ,ATTEN_STATUS,ATTEN_SHIFT,ATTEN_COMMENTS, ACTION, EMP_IMAGE ";
                    strSQL = strSQL + ") ";
                    strSQL = strSQL + "VALUES (";
                    strSQL = strSQL + "'" + obj.strUSER_NAME + "',";
                   
                    strSQL = strSQL + "'" + obj.strROLE + "',";
                    strSQL = strSQL + "'" + obj.strEMP_CARD_NO + "',";
                  
                    strSQL = strSQL + "'" + obj.strATTEN_DATEIN + "',";
                    strSQL = strSQL + "'" + obj.strATTEN_TIMEIN + "',";
                    strSQL = strSQL + "'" + obj.strLATITUDE + "',";
                    strSQL = strSQL + "'" + obj.strLONGITUDE + "',";
                    strSQL = strSQL + "'" + obj.strADDRESS + "',";
                    strSQL = strSQL + "'" + obj.intDISTANCE + "',";
                    
                    strSQL = strSQL + "'" + obj.strATTEN_STATUS + "',";
                    strSQL = strSQL + "'" + obj.strATTEN_SHIFT + "',";
                   
                    strSQL = strSQL + "'" + obj.strATTEN_COMMENTS + "',";
                    strSQL = strSQL + "'" + obj.strACTION + "',";
                    strSQL = strSQL + "'" + obj.strEMP_IMAGE + "',";
                   
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
      
        public string mUpdateConfig(List<AttendentCongfig> attendances)
        {
            string connectionString = Utility.SQLConnstringComSwitch("0001");

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlTransaction transaction = connection.BeginTransaction())
                    {
                        try
                        {
                            // Check if the list contains only one item
                            if (attendances.Count == 1)
                            {
                                // Single value insert
                                var attendance = attendances[0];
                                string strSQL = "UPDATE HRS_TRANS_WORK_ATTENDANCE_NEW SET ACTION=@Action ";
                                strSQL += "WHERE ATTEN_SERIAL=@AttendantSerial";

                                using (SqlCommand command = new SqlCommand(strSQL, connection, transaction))
                                {
                                    command.Parameters.AddWithValue("@Action", attendance.strACTION);
                                    command.Parameters.AddWithValue("@AttendantSerial", attendance.intATTEN_SERIAL);
                                    command.ExecuteNonQuery();
                                }
                            }
                            else if (attendances.Count > 1)
                            {
                                // Multiple value insert
                                foreach (var attendance in attendances)
                                {
                                    string strSQL = "UPDATE HRS_TRANS_WORK_ATTENDANCE_NEW SET ACTION=@Action ";
                                    strSQL += "WHERE ATTEN_SERIAL=@AttendantSerial";

                                    using (SqlCommand command = new SqlCommand(strSQL, connection, transaction))
                                    {
                                        command.Parameters.AddWithValue("@Action", attendance.strACTION);
                                        command.Parameters.AddWithValue("@AttendantSerial", attendance.intATTEN_SERIAL);
                                        command.ExecuteNonQuery();
                                    }
                                }
                            }
                            else
                            {
                                // No items to insert
                                return "No items to update";
                            }

                            // Commit transaction if all updates succeed
                            transaction.Commit();
                            return "All updates succeed";
                        }
                        catch (Exception ex)
                        {
                            // Rollback transaction if any update fails
                            transaction.Rollback();
                            return ex.Message.ToString();
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                return ex.Message.ToString();
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












        public List<kibria> mGetKibriadata(kibria obj)
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


                DateTime datenow = DateTime.Now;

                List<kibria> oooChequePrint = new List<kibria>();

                strSQL = @"SELECT        tb1.TEAM_NAME, tb1.NOMPO, ISNULL(tb2.Present, 0) AS Present, ISNULL(tb3.LEAVE, 0) AS LEAVE, tb1.NOMPO - (ISNULL(tb2.Present, 0) + ISNULL(tb3.LEAVE, 0)) AS ABSENT
                FROM            (SELECT        T.TEAM_NAME, 0 AS NOMPO, 0 AS Present, COUNT(DISTINCT L.EMP_CARD_NO) AS LEAVE, 0 AS ABSENT
                FROM            HRS_EMP_LEAVE_DETAILS AS L LEFT OUTER JOIN
                SMART0005.dbo.ACC_LEDGER_Z_D_A AS V ON L.EMP_CARD_NO = V.MPO_CARD_NO INNER JOIN
                SMART0005.dbo.TEAM_CONFIG AS T ON V.ZONE = T.ZONE_NAME
                WHERE        (L.LEAVE_DATE = " + Utility.cvtSQLDateString(DateTime.Now.ToString("dd-MM-yyyy"))
                +") GROUP BY T.TEAM_NAME) AS tb3 INNER JOIN(SELECT T.TEAM_NAME, 0 AS NOMPO, COUNT(DISTINCT V.MPO_CARD_NO) AS Present, 0 AS LEAVE, 0 AS ABSENT FROM HRS_TRANS_WORK_ATTENDANCE AS A INNER JOIN SMART0005.dbo.ACC_LEDGER_Z_D_A AS V ON A.EMP_CARD_NO = V.MPO_CARD_NO INNER JOIN SMART0005.dbo.TEAM_CONFIG AS T ON V.ZONE = T.ZONE_NAME  WHERE        A.ATTEN_DATEIN = " + Utility.cvtSQLDateString(DateTime.Now.ToString("dd-MM-yyyy")) 
                + "  GROUP BY T.TEAM_NAME) AS tb2 ON tb3.TEAM_NAME = tb2.TEAM_NAME RIGHT OUTER JOIN   (SELECT        T.TEAM_NAME, COUNT(DISTINCT V.MPO_CARD_NO) AS NOMPO, 0 AS Present, 0 AS LEAVE, 0 AS ABSENT  FROM            SMART0005.dbo.ACC_LEDGER_Z_D_A AS V LEFT OUTER JOIN     SMART0005.dbo.TEAM_CONFIG AS T ON V.ZONE = T.ZONE_NAME LEFT OUTER JOIN  HRS_TRANS_WORK_ATTENDANCE AS A ON V.MPO_CARD_NO = A.EMP_CARD_NO  WHERE        (V.LEDGER_STATUS = 0) AND (V.ZONE NOT IN ('ZH-Corporate Sales', 'X-MPO Accounts-Zone')) AND (V.TERRITORRY_NAME NOT IN ('Sample', 'Sales')) AND (V.BRANCH_ID IN ('0001', '0003')) AND   (V.LEDGER_STATUS = 0) GROUP BY T.TEAM_NAME) AS tb1 ON tb2.TEAM_NAME = tb1.TEAM_NAME;";
               
               


                SqlCommand cmd = new SqlCommand(strSQL, gcnMain);
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {



                    kibria oLedg = new kibria();
                    oLedg.TEAM_NAME = dr["TEAM_NAME"].ToString();

                    oLedg.NOMPO = dr["NOMPO"].ToString();

                    oLedg.Present = dr["Present"].ToString();

                    oLedg.LEAVE = dr["LEAVE"].ToString();

                    oLedg.ABSENT = dr["ABSENT"].ToString();

               
                    


                    oooChequePrint.Add(oLedg);
                }

                if (!dr.HasRows)
                {
                    kibria oLedg = new kibria();


                    oLedg.TEAM_NAME = "";
                    oLedg.NOMPO = "";

                    oLedg.Present = "";
                    oLedg.LEAVE = "";

                    oLedg.ABSENT = "";


                    oooChequePrint.Add(oLedg);
                }
                dr.Close();
                gcnMain.Close();
                cmd.Dispose();
                return oooChequePrint;
            }

        }




          [HttpPost]
        public ActionResult mGetKibriadatazone(kibria obj)
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


                DateTime datenow = DateTime.Now;

                List<kibria> oooChequePrint = new List<kibria>();

                strSQL = @"
                SELECT        tb1.ZONE_NAME , tb1.NOMPO, ISNULL(tb2.Present, 0) AS Present, ISNULL(tb3.LEAVE, 0) AS LEAVE, tb1.NOMPO - (ISNULL(tb2.Present, 0) + ISNULL(tb3.LEAVE, 0)) AS ABSENT
                FROM            (SELECT        T.ZONE_NAME, 0 AS NOMPO, 0 AS Present, COUNT(DISTINCT L.EMP_CARD_NO) AS LEAVE, 0 AS ABSENT
                FROM            HRS_EMP_LEAVE_DETAILS AS L LEFT OUTER JOIN
                SMART0005.dbo.ACC_LEDGER_Z_D_A AS V ON L.EMP_CARD_NO = V.MPO_CARD_NO INNER JOIN
                SMART0005.dbo.TEAM_CONFIG AS T ON V.ZONE = T.ZONE_NAME
                WHERE         (L.LEAVE_DATE = " + Utility.cvtSQLDateString(DateTime.Now.ToString("dd-MM-yyyy"))
                +") AND T.TEAM_NAME ='" + obj.TEAM_NAME + "' GROUP BY T.ZONE_NAME) AS tb3 INNER JOIN (SELECT T.ZONE_NAME , 0 AS NOMPO, COUNT(DISTINCT V.MPO_CARD_NO) AS Present, 0 AS LEAVE, 0 AS ABSENT FROM HRS_TRANS_WORK_ATTENDANCE AS A INNER JOIN SMART0005.dbo.ACC_LEDGER_Z_D_A AS V ON A.EMP_CARD_NO = V.MPO_CARD_NO INNER JOIN SMART0005.dbo.TEAM_CONFIG AS T ON V.ZONE = T.ZONE_NAME WHERE A.ATTEN_DATEIN = " + Utility.cvtSQLDateString(DateTime.Now.ToString("dd-MM-yyyy")) 
                + " AND T.TEAM_NAME = '" + obj.TEAM_NAME + "' GROUP BY T.ZONE_NAME) AS tb2 ON tb3.ZONE_NAME = tb2.ZONE_NAME RIGHT OUTER JOIN  (SELECT T.ZONE_NAME , COUNT(DISTINCT V.MPO_CARD_NO) AS NOMPO, 0 AS Present, 0 AS LEAVE, 0 AS ABSENT FROM  SMART0005.dbo.ACC_LEDGER_Z_D_A AS V LEFT OUTER JOIN SMART0005.dbo.TEAM_CONFIG AS T ON V.ZONE = T.ZONE_NAME LEFT OUTER JOIN HRS_TRANS_WORK_ATTENDANCE AS A ON V.MPO_CARD_NO = A.EMP_CARD_NO WHERE (V.LEDGER_STATUS = 0) AND (V.ZONE NOT IN ('ZH-Corporate Sales', 'X-MPO Accounts-Zone')) AND (V.TERRITORRY_NAME NOT IN ('Sample', 'Sales')) AND (V.BRANCH_ID IN ('0001', '0003')) AND  (V.LEDGER_STATUS = 0) AND T.TEAM_NAME ='" + obj.TEAM_NAME + "' GROUP BY T.ZONE_NAME) AS tb1 ON tb2.ZONE_NAME = tb1.ZONE_NAME";




                SqlCommand cmd = new SqlCommand(strSQL, gcnMain);
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {



                    kibria oLedg = new kibria();
                    oLedg.ZONE_NAME = dr["ZONE_NAME"].ToString();

                    oLedg.NOMPO = dr["NOMPO"].ToString();

                    oLedg.Present = dr["Present"].ToString();

                    oLedg.LEAVE = dr["LEAVE"].ToString();

                    oLedg.ABSENT = dr["ABSENT"].ToString();





                    oooChequePrint.Add(oLedg);
                }

                if (!dr.HasRows)
                {
                    kibria oLedg = new kibria();


                    oLedg.ZONE_NAME = "";
                    oLedg.NOMPO = "";

                    oLedg.Present = "";
                    oLedg.LEAVE = "";

                    oLedg.ABSENT = "";


                    oooChequePrint.Add(oLedg);
                }
                dr.Close();
                gcnMain.Close();
                cmd.Dispose();
                return Json(oooChequePrint); ;
            }

        }




        //---------diV


          [HttpPost]
          public ActionResult mGetKibriadatadiv(kibria obj)
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


                  DateTime datenow = DateTime.Now;

                  List<kibria> oooChequePrint = new List<kibria>();

                  strSQL = @"
          SELECT        tb1.MPO_DIV , tb1.NOMPO, ISNULL(tb2.Present, 0) AS Present, ISNULL(tb3.LEAVE, 0) AS LEAVE, tb1.NOMPO - (ISNULL(tb2.Present, 0) + ISNULL(tb3.LEAVE, 0)) AS ABSENT
FROM            (SELECT      T.ZONE_NAME ,  V.MPO_DIV , 0 AS NOMPO, 0 AS Present, COUNT(DISTINCT L.EMP_CARD_NO) AS LEAVE, 0 AS ABSENT
FROM            HRS_EMP_LEAVE_DETAILS AS L LEFT OUTER JOIN
SMART0005.dbo.ACC_LEDGER_Z_D_A AS V ON L.EMP_CARD_NO = V.MPO_CARD_NO INNER JOIN
SMART0005.dbo.TEAM_CONFIG AS T ON V.ZONE = T.ZONE_NAME
WHERE        (L.LEAVE_DATE = " + Utility.cvtSQLDateString(DateTime.Now.ToString("dd-MM-yyyy"))
                  + ") AND T.TEAM_NAME ='" + obj.TEAM_NAME + "' AND T.ZONE_NAME  ='" + obj.ZONE_NAME + "' GROUP BY  T.ZONE_NAME ,V.MPO_DIV) AS tb3 INNER JOIN (SELECT T.ZONE_NAME , V.MPO_DIV , 0 AS NOMPO, COUNT(DISTINCT V.MPO_CARD_NO) AS Present, 0 AS LEAVE, 0 AS ABSENT FROM  HRS_TRANS_WORK_ATTENDANCE AS A INNER JOIN SMART0005.dbo.ACC_LEDGER_Z_D_A AS V ON A.EMP_CARD_NO = V.MPO_CARD_NO INNER JOIN SMART0005.dbo.TEAM_CONFIG AS T ON V.ZONE = T.ZONE_NAME WHERE        (A.ATTEN_DATEIN = " + Utility.cvtSQLDateString(DateTime.Now.ToString("dd-MM-yyyy"))
                  + ") AND T.TEAM_NAME ='" + obj.TEAM_NAME + "' AND T.ZONE_NAME  ='" + obj.ZONE_NAME + "'GROUP BY  T.ZONE_NAME ,V.MPO_DIV) AS tb2 ON tb3.ZONE_NAME = tb2.ZONE_NAME RIGHT OUTER JOIN (SELECT      T.ZONE_NAME ,  V.MPO_DIV , COUNT(DISTINCT V.MPO_CARD_NO) AS NOMPO, 0 AS Present, 0 AS LEAVE, 0 AS ABSENT FROM SMART0005.dbo.ACC_LEDGER_Z_D_A AS V LEFT OUTER JOIN SMART0005.dbo.TEAM_CONFIG AS T ON V.ZONE = T.ZONE_NAME LEFT OUTER JOIN HRS_TRANS_WORK_ATTENDANCE AS A ON V.MPO_CARD_NO = A.EMP_CARD_NO WHERE        (V.LEDGER_STATUS = 0) AND (V.ZONE NOT IN ('ZH-Corporate Sales', 'X-MPO Accounts-Zone')) AND (V.TERRITORRY_NAME NOT IN ('Sample', 'Sales')) AND (V.BRANCH_ID IN ('0001', '0003')) AND  (V.LEDGER_STATUS = 0) AND T.TEAM_NAME ='CHALLENGER'  AND T.ZONE_NAME  ='NORTH ZONE' GROUP BY  T.ZONE_NAME ,V.MPO_DIV) AS tb1 ON tb2.ZONE_NAME = tb1.ZONE_NAME";










                  SqlCommand cmd = new SqlCommand(strSQL, gcnMain);
                  dr = cmd.ExecuteReader();
                  while (dr.Read())
                  {



                      kibria oLedg = new kibria();
                      oLedg.MPO_DIV = dr["MPO_DIV"].ToString();

                      oLedg.NOMPO = dr["NOMPO"].ToString();

                      oLedg.Present = dr["Present"].ToString();

                      oLedg.LEAVE = dr["LEAVE"].ToString();

                      oLedg.ABSENT = dr["ABSENT"].ToString();





                      oooChequePrint.Add(oLedg);
                  }

                  if (!dr.HasRows)
                  {
                      kibria oLedg = new kibria();


                      oLedg.ZONE_NAME = "";
                      oLedg.NOMPO = "";

                      oLedg.Present = "";
                      oLedg.LEAVE = "";

                      oLedg.ABSENT = "";


                      oooChequePrint.Add(oLedg);
                  }
                  dr.Close();
                  gcnMain.Close();
                  cmd.Dispose();
                  return Json(oooChequePrint); ;
              }

          }




          //---------aREA


          [HttpPost]
          public ActionResult mGetKibriadataarea(kibria obj)
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


                  DateTime datenow = DateTime.Now;

                  List<kibria> oooChequePrint = new List<kibria>();

                  strSQL = @"


SELECT        tb1.MPO_AREA , tb1.NOMPO, ISNULL(tb2.Present, 0) AS Present, ISNULL(tb3.LEAVE, 0) AS LEAVE, tb1.NOMPO - (ISNULL(tb2.Present, 0) + ISNULL(tb3.LEAVE, 0)) AS ABSENT
	FROM            (SELECT      T.ZONE_NAME , (CASE WHEN  V.MPO_AREA IS NULL THEN V.MPO_DIV ELSE V.MPO_AREA END )MPO_AREA , 0 AS NOMPO, 0 AS Present, COUNT(DISTINCT L.EMP_CARD_NO) AS LEAVE, 0 AS ABSENT
	FROM            HRS_EMP_LEAVE_DETAILS AS L LEFT OUTER JOIN
	SMART0005.dbo.ACC_LEDGER_Z_D_A AS V ON L.EMP_CARD_NO = V.MPO_CARD_NO INNER JOIN
	SMART0005.dbo.TEAM_CONFIG AS T ON V.ZONE = T.ZONE_NAME
	WHERE       (L.LEAVE_DATE = " + Utility.cvtSQLDateString(DateTime.Now.ToString("dd-MM-yyyy"))
                  + ")AND T.TEAM_NAME ='" + obj.TEAM_NAME + "' AND T.ZONE_NAME  ='" + obj.ZONE_NAME + "' AND V.MPO_DIV ='" + obj.MPO_DIV + "'GROUP BY  T.ZONE_NAME ,V.MPO_AREA,V.MPO_DIV) AS tb3 INNER JOIN(SELECT       T.ZONE_NAME , (CASE WHEN  V.MPO_AREA IS NULL THEN V.MPO_DIV ELSE V.MPO_AREA  END )MPO_AREA , 0 AS NOMPO, COUNT(DISTINCT V.MPO_CARD_NO) AS Present, 0 AS LEAVE, 0 AS ABSENT FROM            HRS_TRANS_WORK_ATTENDANCE AS A INNER JOIN SMART0005.dbo.ACC_LEDGER_Z_D_A AS V ON A.EMP_CARD_NO = V.MPO_CARD_NO INNER JOIN SMART0005.dbo.TEAM_CONFIG AS T ON V.ZONE = T.ZONE_NAME WHERE (A.ATTEN_DATEIN = " + Utility.cvtSQLDateString(DateTime.Now.ToString("dd-MM-yyyy"))
                  + ")AND T.TEAM_NAME ='" + obj.TEAM_NAME + "'  AND T.ZONE_NAME  ='" + obj.ZONE_NAME + "' AND V.MPO_DIV ='" + obj.MPO_DIV + "'	GROUP BY  T.ZONE_NAME ,V.MPO_AREA,V.MPO_DIV) AS tb2 ON tb3.ZONE_NAME = tb2.ZONE_NAME RIGHT OUTER JOIN (SELECT      T.ZONE_NAME ,  (CASE WHEN  V.MPO_AREA IS NULL THEN V.MPO_DIV ELSE V.MPO_AREA END )MPO_AREA , COUNT(DISTINCT V.MPO_CARD_NO) AS NOMPO, 0 AS Present, 0 AS LEAVE, 0 AS ABSENT FROM            SMART0005.dbo.ACC_LEDGER_Z_D_A AS V LEFT OUTER JOIN SMART0005.dbo.TEAM_CONFIG AS T ON V.ZONE = T.ZONE_NAME LEFT OUTER JOIN HRS_TRANS_WORK_ATTENDANCE AS A ON V.MPO_CARD_NO = A.EMP_CARD_NO WHERE        (V.LEDGER_STATUS = 0) AND (V.ZONE NOT IN ('ZH-Corporate Sales', 'X-MPO Accounts-Zone')) AND (V.TERRITORRY_NAME NOT IN ('Sample', 'Sales')) AND (V.BRANCH_ID IN ('0001', '0003')) AND  (V.LEDGER_STATUS = 0) AND T.TEAM_NAME ='" + obj.TEAM_NAME + "' AND T.ZONE_NAME  ='" + obj.ZONE_NAME + "' AND V.MPO_DIV ='" + obj.MPO_DIV + "' GROUP BY  T.ZONE_NAME ,V.MPO_AREA,V.MPO_DIV) AS tb1 ON tb2.ZONE_NAME = tb1.ZONE_NAME ";



                  SqlCommand cmd = new SqlCommand(strSQL, gcnMain);
                  dr = cmd.ExecuteReader();
                  while (dr.Read())
                  {



                      kibria oLedg = new kibria();
                      oLedg.MPO_AREA = dr["MPO_AREA"].ToString();

                      oLedg.NOMPO = dr["NOMPO"].ToString();

                      oLedg.Present = dr["Present"].ToString();

                      oLedg.LEAVE = dr["LEAVE"].ToString();

                      oLedg.ABSENT = dr["ABSENT"].ToString();





                      oooChequePrint.Add(oLedg);
                  }

                  if (!dr.HasRows)
                  {
                      kibria oLedg = new kibria();


                      oLedg.MPO_AREA = "";
                      oLedg.NOMPO = "";

                      oLedg.Present = "";
                      oLedg.LEAVE = "";

                      oLedg.ABSENT = "";


                      oooChequePrint.Add(oLedg);
                  }
                  dr.Close();
                  gcnMain.Close();
                  cmd.Dispose();
                  return Json(oooChequePrint); ;
              }

          }






          //---------market


          [HttpPost]
          public ActionResult mGetKibriadatamarket(kibria obj)
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


                  DateTime datenow = DateTime.Now;

                  List<kibria> oooChequePrint = new List<kibria>();

                  strSQL = @"SELECT        tb1.TERRITORRY_NAME  , tb1.NOMPO, ISNULL(tb2.Present, 0) AS Present, ISNULL(tb3.LEAVE, 0) AS LEAVE, tb1.NOMPO - (ISNULL(tb2.Present, 0) + ISNULL(tb3.LEAVE, 0)) AS ABSENT
FROM            (SELECT      T.ZONE_NAME , V.TERRITORRY_NAME  , 0 AS NOMPO, 0 AS Present, COUNT(DISTINCT L.EMP_CARD_NO) AS LEAVE, 0 AS ABSENT
FROM            HRS_EMP_LEAVE_DETAILS AS L LEFT OUTER JOIN
SMART0005.dbo.ACC_LEDGER_Z_D_A AS V ON L.EMP_CARD_NO = V.MPO_CARD_NO INNER JOIN
SMART0005.dbo.TEAM_CONFIG AS T ON V.ZONE = T.ZONE_NAME
WHERE        (L.LEAVE_DATE = " + Utility.cvtSQLDateString(DateTime.Now.ToString("dd-MM-yyyy")) 
             + ")AND T.TEAM_NAME ='CHALLENGER' AND T.ZONE_NAME  ='NORTH ZONE' AND V.MPO_DIV ='Bogura' AND V.MPO_AREA  ='Bogura' GROUP BY  T.ZONE_NAME , V.TERRITORRY_NAME) AS tb3 INNER JOIN (SELECT       T.ZONE_NAME ,  V.TERRITORRY_NAME , 0 AS NOMPO, COUNT(DISTINCT V.MPO_CARD_NO) AS Present, 0 AS LEAVE, 0 AS ABSENT FROM  HRS_TRANS_WORK_ATTENDANCE AS A INNER JOIN SMART0005.dbo.ACC_LEDGER_Z_D_A AS V ON A.EMP_CARD_NO = V.MPO_CARD_NO INNER JOIN SMART0005.dbo.TEAM_CONFIG AS T ON V.ZONE = T.ZONE_NAME WHERE       (A.ATTEN_DATEIN = " + Utility.cvtSQLDateString(DateTime.Now.ToString("dd-MM-yyyy"))  + ") AND T.TEAM_NAME ='" + obj.TEAM_NAME + "' AND T.ZONE_NAME  ='" + obj.ZONE_NAME + "' AND V.MPO_DIV ='" + obj.MPO_DIV + 
             "' AND V.MPO_AREA  ='" + obj.MPO_AREA + "' GROUP BY  T.ZONE_NAME , V.TERRITORRY_NAME) AS tb2 ON tb3.ZONE_NAME = tb2.ZONE_NAME RIGHT OUTER JOIN (SELECT T.ZONE_NAME ,   V.TERRITORRY_NAME , COUNT(DISTINCT V.MPO_CARD_NO) AS NOMPO, 0 AS Present, 0 AS LEAVE, 0 AS ABSENT FROM   SMART0005.dbo.ACC_LEDGER_Z_D_A AS V LEFT OUTER JOIN SMART0005.dbo.TEAM_CONFIG AS T ON V.ZONE = T.ZONE_NAME LEFT OUTER JOIN HRS_TRANS_WORK_ATTENDANCE AS A ON V.MPO_CARD_NO = A.EMP_CARD_NO WHERE        (V.LEDGER_STATUS = 0) AND (V.ZONE NOT IN ('ZH-Corporate Sales', 'X-MPO Accounts-Zone')) AND (V.TERRITORRY_NAME NOT IN ('Sample', 'Sales')) AND (V.BRANCH_ID IN ('0001', '0003')) AND  (V.LEDGER_STATUS = 0) AND T.TEAM_NAME ='" + obj.TEAM_NAME + "' AND T.ZONE_NAME  ='" + obj.ZONE_NAME + "' AND V.MPO_DIV ='" + obj.MPO_DIV + "' AND V.MPO_AREA  ='" + obj.MPO_AREA + "' GROUP BY  T.ZONE_NAME , V.TERRITORRY_NAME) AS tb1 ON tb2.ZONE_NAME = tb1.ZONE_NAME";


                  SqlCommand cmd = new SqlCommand(strSQL, gcnMain);
                  dr = cmd.ExecuteReader();
                  while (dr.Read())
                  {



                      kibria oLedg = new kibria();
                      oLedg.TERRITORRY_NAME = dr["TERRITORRY_NAME"].ToString();

                      oLedg.NOMPO = dr["NOMPO"].ToString();

                      oLedg.Present = dr["Present"].ToString();

                      oLedg.LEAVE = dr["LEAVE"].ToString();

                      oLedg.ABSENT = dr["ABSENT"].ToString();





                      oooChequePrint.Add(oLedg);
                  }

                  if (!dr.HasRows)
                  {
                      kibria oLedg = new kibria();


                      oLedg.TERRITORRY_NAME = "";
                      oLedg.NOMPO = "";

                      oLedg.Present = "";
                      oLedg.LEAVE = "";

                      oLedg.ABSENT = "";


                      oooChequePrint.Add(oLedg);
                  }
                  dr.Close();
                  gcnMain.Close();
                  cmd.Dispose();
                  return Json(oooChequePrint); ;
              }

          }






          //---------route


          [HttpPost]
          public ActionResult mGetKibriadataroute(kibria obj)
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


                  DateTime datenow = DateTime.Now;

                  List<kibria> oooChequePrint = new List<kibria>();

                  strSQL = @"SELECT    tb1.TERRITORRY_NAME  , tb1.NOMPO, ISNULL(tb2.Present, 0) AS Present, ISNULL(tb3.LEAVE, 0) AS LEAVE, tb1.NOMPO - (ISNULL(tb2.Present, 0) + ISNULL(tb3.LEAVE, 0)) AS ABSENT
FROM   (SELECT      V.ZONE  , V.TERRITORRY_NAME  , 0 AS NOMPO, 0 AS Present, COUNT(DISTINCT L.EMP_CARD_NO) AS LEAVE, 0 AS ABSENT
FROM HRS_EMP_LEAVE_DETAILS AS L LEFT OUTER JOIN
SMART0005.dbo.ACC_LEDGER_Z_D_A AS V ON L.EMP_CARD_NO = V.MPO_CARD_NO INNER JOIN
SMART0005.dbo.TEAM_CONFIG AS T ON V.ZONE = T.ZONE_NAME RIGHT OUTER JOIN
SMART0005.dbo.MARKET_ROUTE AS R ON V.TERITORRY_CODE = R.TERITORRY_CODE
WHERE       (L.LEAVE_DATE = " + Utility.cvtSQLDateString(DateTime.Now.ToString("dd-MM-yyyy"))
 + ")AND T.TEAM_NAME ='" + obj.TEAM_NAME + "' AND T.ZONE_NAME  ='" + obj.ZONE_NAME + "' AND V.MPO_DIV ='" + obj.MPO_DIV + "' AND V.MPO_AREA  ='" + obj.MPO_AREA + "' and v.TERRITORRY_NAME ='" + obj.TERRITORRY_NAME + 
 "'GROUP BY  V.ZONE , V.TERRITORRY_NAME) AS tb3 INNER JOIN (SELECT T.ZONE_NAME ,  V.TERRITORRY_NAME , 0 AS NOMPO, COUNT(DISTINCT V.MPO_CARD_NO) AS Present, 0 AS LEAVE, 0 AS ABSENT FROM HRS_TRANS_WORK_ATTENDANCE AS A INNER JOIN SMART0005.dbo.ACC_LEDGER_Z_D_A AS V ON A.EMP_CARD_NO = V.MPO_CARD_NO INNER JOIN SMART0005.dbo.TEAM_CONFIG AS T ON V.ZONE = T.ZONE_NAME RIGHT OUTER JOIN SMART0005.dbo.MARKET_ROUTE AS R ON V.TERITORRY_CODE = R.TERITORRY_CODE AND (A.ATTEN_DATEIN = " + Utility.cvtSQLDateString(DateTime.Now.ToString("dd-MM-yyyy")) 
     + ")AND T.TEAM_NAME ='" + obj.TEAM_NAME + "' AND T.ZONE_NAME  ='" + obj.ZONE_NAME + "' AND V.MPO_DIV ='" + obj.MPO_DIV + "' AND V.MPO_AREA  ='" + obj.MPO_AREA + "' and v.TERRITORRY_NAME ='" + obj.TERRITORRY_NAME + "' GROUP BY  T.ZONE_NAME , V.TERRITORRY_NAME) AS tb2 ON tb3.ZONE = tb2.ZONE_NAME RIGHT OUTER JOIN (SELECT  T.ZONE_NAME ,   V.TERRITORRY_NAME , COUNT(DISTINCT V.MPO_CARD_NO) AS NOMPO, 0 AS Present, 0 AS LEAVE, 0 AS ABSENT FROM   SMART0005.dbo.ACC_LEDGER_Z_D_A AS V LEFT OUTER JOIN SMART0005.dbo.TEAM_CONFIG AS T ON V.ZONE = T.ZONE_NAME LEFT OUTER JOIN HRS_TRANS_WORK_ATTENDANCE AS A ON V.MPO_CARD_NO = A.EMP_CARD_NO RIGHT OUTER JOIN SMART0005.dbo.MARKET_ROUTE AS R ON V.TERITORRY_CODE = R.TERITORRY_CODE WHERE (V.LEDGER_STATUS = 0) AND (V.ZONE NOT IN ('ZH-Corporate Sales', 'X-MPO Accounts-Zone')) AND (V.TERRITORRY_NAME NOT IN ('Sample', 'Sales')) AND (V.BRANCH_ID IN ('0001', '0003')) AND  (V.LEDGER_STATUS = 0) AND T.TEAM_NAME ='" + obj.TEAM_NAME + "' AND T.ZONE_NAME  ='" + obj.ZONE_NAME + "' AND V.MPO_DIV ='" + obj.MPO_DIV + "' AND V.MPO_AREA  ='" + obj.MPO_AREA + "' and v.TERRITORRY_NAME ='" + obj.TERRITORRY_NAME + "' GROUP BY  T.ZONE_NAME , V.TERRITORRY_NAME) AS tb1 ON tb2.ZONE_NAME = tb1.ZONE_NAME";








                  SqlCommand cmd = new SqlCommand(strSQL, gcnMain);
                  dr = cmd.ExecuteReader();
                  while (dr.Read())
                  {



                      kibria oLedg = new kibria();
                      oLedg.TERRITORRY_NAME = dr["TERRITORRY_NAME"].ToString();

                      oLedg.NOMPO = dr["NOMPO"].ToString();

                      oLedg.Present = dr["Present"].ToString();

                      oLedg.LEAVE = dr["LEAVE"].ToString();

                      oLedg.ABSENT = dr["ABSENT"].ToString();





                      oooChequePrint.Add(oLedg);
                  }

                  if (!dr.HasRows)
                  {
                      kibria oLedg = new kibria();


                      oLedg.TERRITORRY_NAME = "";
                      oLedg.NOMPO = "";

                      oLedg.Present = "";
                      oLedg.LEAVE = "";

                      oLedg.ABSENT = "";


                      oooChequePrint.Add(oLedg);
                  }
                  dr.Close();
                  gcnMain.Close();
                  cmd.Dispose();
                  return Json(oooChequePrint); ;
              }

          }






        //SELECT COUNT(*) AS Countt, COUNT(EMP_CARD_NO) AS EMP_CARD_NO, COUNT(USER_NAME) AS USER_NAME, ROLE, NULL AS TEAM_NAME, NULL AS TEAM_CODE
        //    FROM HRS_TRANS_WORK_ATTENDANCE_NEW 
        //    GROUP BY ROLE 
        //    HAVING COUNT(*) > 0 AND COUNT(EMP_CARD_NO) > 0 AND COUNT(USER_NAME) > 0 AND ROLE IS NOT NULL 
        //    UNION ALL 
        //    SELECT NULL, NULL, NULL, NULL, TEAM_NAME, TEAM_CODE
        //    FROM (SELECT DISTINCT TEAM_NAME, TEAM_CODE FROM SMART0005.dbo.TEAM_CONFIG) AS UniqueTeamNames 
        //    WHERE TEAM_NAME IS NOT NULL





//        public List<AttendentCongfig> mGetUserTotal()
//        {
//            string connectionString = Utility.SQLConnstringComSwitch("0001");
//            string strSQL = @"SELECT TEAM_NAME,ZONE,DIV,AREA,TERRITORRY_NAME, MARKET_ROUTE_NMAE,TYPE,SUM(TOTALMPO)TOTALMPO,SUM(TOTALAREA)TOTALAREA,SUM(TOTALDIV)TOTALDIV,SUM(TOTALZH)TOTALZH,SUM(TOTALHEAD)TOTALHEAD,SUM(PRESENT)PRESENT,SUM(ABSENT)ABSENT,SUM(CL)CL,
//            SUM(ML)ML FROM ATTEN_DASHBOARD_DETAILS
//            WHERE TYPE='MPO'
//            GROUP BY TEAM_NAME,ZONE,DIV,AREA,TERRITORRY_NAME, MARKET_ROUTE_NMAE,TYPE
//            UNION ALL
//            SELECT TEAM_NAME,ZONE,DIV,AREA,TERRITORRY_NAME, MARKET_ROUTE_NMAE,TYPE,SUM(TOTALMPO)TOTALMPO,SUM(TOTALAREA)TOTALAREA,SUM(TOTALDIV)TOTALDIV,SUM(TOTALZH)TOTALZH,SUM(TOTALHEAD)TOTALHEAD,SUM(PRESENT)PRESENT,SUM(ABSENT)ABSENT,SUM(CL)CL,
//            SUM(ML)ML FROM ATTEN_DASHBOARD_DETAILS
//            WHERE TYPE='AH'
//            GROUP BY TEAM_NAME,ZONE,DIV,AREA,TERRITORRY_NAME, MARKET_ROUTE_NMAE,TYPE
//            UNION ALL
//            SELECT TEAM_NAME,ZONE,DIV,AREA,TERRITORRY_NAME, MARKET_ROUTE_NMAE,TYPE,SUM(TOTALMPO)TOTALMPO,SUM(TOTALAREA)TOTALAREA,SUM(TOTALDIV)TOTALDIV,SUM(TOTALZH)TOTALZH,SUM(TOTALHEAD)TOTALHEAD,SUM(PRESENT)PRESENT,SUM(ABSENT)ABSENT,SUM(CL)CL,
//            SUM(ML)ML FROM ATTEN_DASHBOARD_DETAILS
//            WHERE TYPE='DH'
//            GROUP BY TEAM_NAME,ZONE,DIV,AREA,TERRITORRY_NAME, MARKET_ROUTE_NMAE,TYPE
//            UNION ALL
//            SELECT TEAM_NAME,ZONE,DIV,AREA,TERRITORRY_NAME, MARKET_ROUTE_NMAE,TYPE,SUM(TOTALMPO)TOTALMPO,SUM(TOTALAREA)TOTALAREA,SUM(TOTALDIV)TOTALDIV,SUM(TOTALZH)TOTALZH,SUM(TOTALHEAD)TOTALHEAD,SUM(PRESENT)PRESENT,SUM(ABSENT)ABSENT,SUM(CL)CL,
//            SUM(ML)ML FROM ATTEN_DASHBOARD_DETAILS
//            WHERE TYPE='ZH'
//            GROUP BY TEAM_NAME,ZONE,DIV,AREA, TERRITORRY_NAME, MARKET_ROUTE_NMAE,TYPE";

//            List<AttendentCongfig> resultList = new List<AttendentCongfig>();
//            using (SqlConnection connection = new SqlConnection(connectionString))
//            {
//                using (SqlCommand command = new SqlCommand(strSQL, connection))
//                {
//                    connection.Open();
//                    SqlDataReader reader = command.ExecuteReader();

//                    while (reader.Read())
//                    {
//                        AttendentCongfig config = new AttendentCongfig();
//                        config.PRESENT = Convert.ToInt32(reader["PRESENT"] == DBNull.Value ? 0 : reader["PRESENT"]);
//                        config.ABSENT = Convert.ToInt32(reader["ABSENT"] == DBNull.Value ? 0 : reader["ABSENT"]);
//                        config.CL = Convert.ToInt32(reader["CL"] == DBNull.Value ? 0 : reader["CL"]);
//                        config.ML = Convert.ToInt32(reader["ML"] == DBNull.Value ? 0 : reader["ML"]);
//                        config.TYPE = reader["TYPE"].ToString();
//                        config.TEAM_NAME = reader["TEAM_NAME"].ToString();
//                        config.DIV = reader["DIV"].ToString();
//                        config.AREA = reader["AREA"].ToString();
//                        config.MARKET = reader["TERRITORRY_NAME"].ToString();
//                        config.MARKET_ROUTE_NMAE = reader["MARKET_ROUTE_NMAE"].ToString();
//                        config.ZONE = reader["ZONE"].ToString();
//                        config.TOTALMPO = reader["TOTALMPO"].ToString();
//                        config.TOTALAREA = reader["TOTALAREA"].ToString();
//                        config.TOTALDIV = reader["TOTALDIV"].ToString();
//                        config.TOTALZH = reader["TOTALZH"].ToString();
//                        config.TOTALHEAD = reader["TOTALHEAD"].ToString();
//                        resultList.Add(config);
//                    }

//                    reader.Close();
//                }
//            }

//            return resultList;
//        }

//        public List<AttendentCongfig> mGetUserTotalAH(string vstrLedgerName)
//        {
//            string connectionString = Utility.SQLConnstringComSwitch("0001");
//            string strSQL = "SELECT MARKET,ROUTE_NMAE,TYPE,SUM(TOTALMPO)TOTALMPO,SUM(TOTALAREA)TOTALAREA,SUM(TOTALDIV)TOTALDIV,SUM(TOTALZH)TOTALZH,SUM(TOTALHEAD)TOTALHEAD,SUM(PRESENT)PRESENT,SUM(ABSENT)ABSENT,SUM(CL)CL,";
//            strSQL = strSQL + "SUM(ML)ML FROM ATTEN_DASHBOARD_DETAILS_AH ";
//            strSQL = strSQL + "WHERE TYPE='AH' AND AREA ='" + vstrLedgerName + "' ";
//            strSQL = strSQL + "GROUP BY MARKET,ROUTE_NMAE,TYPE ";
//            List<AttendentCongfig> resultList = new List<AttendentCongfig>();
//            using (SqlConnection connection = new SqlConnection(connectionString))
//            {
//                using (SqlCommand command = new SqlCommand(strSQL, connection))
//                {
//                    connection.Open();
//                    SqlDataReader reader = command.ExecuteReader();

//                    while (reader.Read())
//                    {
//                        AttendentCongfig config = new AttendentCongfig();
//                        config.PRESENT = Convert.ToInt32(reader["PRESENT"] == DBNull.Value ? 0 : reader["PRESENT"]);
//                        config.ABSENT = Convert.ToInt32(reader["ABSENT"] == DBNull.Value ? 0 : reader["ABSENT"]);
//                        config.CL = Convert.ToInt32(reader["CL"] == DBNull.Value ? 0 : reader["CL"]);
//                        config.ML = Convert.ToInt32(reader["ML"] == DBNull.Value ? 0 : reader["ML"]);
//                        config.TYPE = reader["TYPE"].ToString();
//                        config.TEAM_NAME = reader["MARKET"].ToString();
//                        config.ZONE = reader["ROUTE_NMAE"].ToString();
//                        config.TOTALMPO = reader["TOTALMPO"].ToString();
//                        config.TOTALAREA = reader["TOTALAREA"].ToString();
//                        config.TOTALDIV = reader["TOTALDIV"].ToString();
//                        config.TOTALZH = reader["TOTALZH"].ToString();
//                        config.TOTALHEAD = reader["TOTALHEAD"].ToString();
//                        resultList.Add(config);
//                    }

//                    reader.Close();
//                }
//            }

//            return resultList;
//        }

//        public List<AttendentCongfig> mGetUserTotalDH(string vstrLedgerName)
//        {
//            string connectionString = Utility.SQLConnstringComSwitch("0001");
//            string strSQL = "SELECT AREANAME,MARKET, ROUTE_NMAE,TYPE,SUM(TOTALMPO)TOTALMPO,SUM(TOTALAREA)TOTALAREA,SUM(TOTALDIV)TOTALDIV,SUM(TOTALZH)TOTALZH,SUM(TOTALHEAD)TOTALHEAD,SUM(PRESENT)PRESENT,SUM(ABSENT)ABSENT,SUM(CL)CL,";
//            strSQL = strSQL + "SUM(ML)ML FROM ATTEN_DASHBOARD_DETAILS_DH ";
//            strSQL = strSQL + "WHERE TYPE='DH' AND DIVISION ='" + vstrLedgerName + "' ";
//            strSQL = strSQL + "GROUP BY AREANAME,MARKET, ROUTE_NMAE,TYPE ";
//            List<AttendentCongfig> resultList = new List<AttendentCongfig>();
//            using (SqlConnection connection = new SqlConnection(connectionString))
//            {
//                using (SqlCommand command = new SqlCommand(strSQL, connection))
//                {
//                    connection.Open();
//                    SqlDataReader reader = command.ExecuteReader();

//                    while (reader.Read())
//                    {
//                        AttendentCongfig config = new AttendentCongfig();
//                        config.PRESENT = Convert.ToInt32(reader["PRESENT"] == DBNull.Value ? 0 : reader["PRESENT"]);
//                        config.ABSENT = Convert.ToInt32(reader["ABSENT"] == DBNull.Value ? 0 : reader["ABSENT"]);
//                        config.CL = Convert.ToInt32(reader["CL"] == DBNull.Value ? 0 : reader["CL"]);
//                        config.ML = Convert.ToInt32(reader["ML"] == DBNull.Value ? 0 : reader["ML"]);
//                        config.TYPE = reader["TYPE"].ToString();
//                        config.TEAM_NAME = reader["AREANAME"].ToString();
//                        config.ZONE = reader["MARKET"].ToString();
//                        config.strROUTE = reader["ROUTE_NMAE"].ToString();
//                        config.TOTALMPO = reader["TOTALMPO"].ToString();
//                        config.TOTALAREA = reader["TOTALAREA"].ToString();
//                        config.TOTALDIV = reader["TOTALDIV"].ToString();
//                        config.TOTALZH = reader["TOTALZH"].ToString();
//                        config.TOTALHEAD = reader["TOTALHEAD"].ToString();
//                        resultList.Add(config);
//                    }

//                    reader.Close();
//                }
//            }

//            return resultList;
//        }







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


                string strFdate = "";
                string strTdate = "";
                strFdate = Convert.ToDateTime(obj.strATTEN_DATEINFROM).ToString(("dd-MM-yyyy"));
                strTdate = Convert.ToDateTime(obj.strATTEN_DATEINTO).ToString(("dd-MM-yyyy"));
                strSQL = "SELECT * FROM HRS_TRANS_WORK_ATTENDANCE_NEW WHERE ATTEN_DATEIN BETWEEN " + Utility.cvtSQLDateString(strFdate) + " and " + Utility.cvtSQLDateString(strTdate) + " ";
                SqlCommand cmd = new SqlCommand(strSQL, gcnMain);
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {


                    AttendentCongfig oLedg = new AttendentCongfig();
                    oLedg.intATTEN_SERIAL = Convert.ToInt32(dr["ATTEN_SERIAL"]);
                    oLedg.strUSER_NAME = dr["USER_NAME"].ToString();
                    oLedg.strROLE = dr["ROLE"].ToString();
                    oLedg.strEMP_CARD_NO = dr["EMP_CARD_NO"].ToString();

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


                    oooChequePrint.Add(oLedg);
                }
                dr.Close();
                gcnMain.Close();
                cmd.Dispose();
                return oooChequePrint;
            }

        }







        public List<Team> mGetTeam(Team obj)
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

                List<Team> oooChequePrint = new List<Team>();

                strSQL = @"SELECT 
                  TEAM_CONFIG.TEAM_NAME, 
                 SUM(TEST.TOTALMPO) AS TOTALMPO, 
                 SUM(TEST.TOTALAREA) AS AREA, 
                 SUM(TEST.TOTALDIV) AS TOTALDIV, 
                 SUM(TEST.TOTALHEAD) AS TOTALHEAD, 
                 SUM(TEST.TOTALZH) AS TOTALZH,
                SUM(TEST.PRESENT) AS PRESENT, 
                SUM(TEST.ABSENT) AS ABSENT, 
               SUM(TEST.LEAVE) AS LEAVE 
               FROM 
                TEST 
                INNER JOIN 
                SMART0005.DBO.TEAM_CONFIG ON TEAM_CONFIG.ZONE_NAME = TEST.ZONE
                 GROUP BY 
               TEAM_CONFIG.TEAM_NAME;";
               


                SqlCommand cmd = new SqlCommand(strSQL, gcnMain);
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {



                    Team oLedg = new Team();
                    oLedg.strTOTALMPO = dr["TOTALMPO"].ToString();

                    oLedg.strTEAM_NAME = dr["TEAM_NAME"].ToString();
                    


                    oooChequePrint.Add(oLedg);
                }

                if (!dr.HasRows)
                {
                    Team oLedg = new Team();


                    oLedg.strTOTALMPO = "";
                    oLedg.strTEAM_NAME = "";
                    



                    oooChequePrint.Add(oLedg);
                }
                dr.Close();
                gcnMain.Close();
                cmd.Dispose();
                return oooChequePrint;
            }

        }





        public List<Zone> mGetZone(Zone obj)
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

                List<Zone> oooChequePrint = new List<Zone>();

                strSQL = @"SELECT 
    ACC_LEDGER_Z_D_A.ZONE,
	TEAM_CONFIG.TEAM_NAME,
    COUNT(ACC_LEDGER_Z_D_A.MPO_CARD_NO) AS TOTAL_MPO,
    SUM(CASE WHEN HRS_TRANS_WORK_ATTENDANCE_NEW.ATTEN_STATUS = 'P' THEN 1 ELSE 0 END) AS PRESENT,
    SUM(CASE WHEN HRS_TRANS_WORK_ATTENDANCE_NEW.ATTEN_STATUS = 'A' THEN 1 ELSE 0 END) AS ABSENT,
    SUM(CASE WHEN HRS_TRANS_WORK_ATTENDANCE_NEW.ATTEN_STATUS = 'L' THEN 1 ELSE 0 END) AS LEAVE
FROM            
    SMART0005.dbo.ACC_LEDGER_Z_D_A 
LEFT OUTER JOIN
    HRS_TRANS_WORK_ATTENDANCE_NEW ON ACC_LEDGER_Z_D_A.MPO_CARD_NO = HRS_TRANS_WORK_ATTENDANCE_NEW.EMP_CARD_NO
LEFT OUTER JOIN
    SMART0005.dbo.TEAM_CONFIG ON ACC_LEDGER_Z_D_A.ZONE = TEAM_CONFIG.ZONE_NAME
WHERE        
    ACC_LEDGER_Z_D_A.BRANCH_ID IN ('0001', '0003')
    AND NOT (ACC_LEDGER_Z_D_A.ZONE IN ('ZH-Corporate Sales', 'X-MPO Accounts-Zone'))
GROUP BY 
    ACC_LEDGER_Z_D_A.ZONE,
	 TEAM_CONFIG.TEAM_NAME;";



                SqlCommand cmd = new SqlCommand(strSQL, gcnMain);
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {



                    Zone oLedg = new Zone();
                    oLedg.strTOTAL_MPO = dr["TOTAL_MPO"].ToString();

                    oLedg.strTEAM_NAME = dr["TEAM_NAME"].ToString();

                    oLedg.strZONE = dr["ZONE"].ToString();
                    oLedg.strPRESENT = dr["PRESENT"].ToString();
                    oLedg.strABSENT = dr["ABSENT"].ToString();
                    oLedg.strLEAVE = dr["LEAVE"].ToString();




                    oooChequePrint.Add(oLedg);
                }

                if (!dr.HasRows)
                {
                    Zone oLedg = new Zone();


                    oLedg.strTOTAL_MPO = "";
                    oLedg.strTEAM_NAME = "";

                    oLedg.strZONE = "";
                    oLedg.strPRESENT = "";
                    oLedg.strABSENT = "";
                    oLedg.strLEAVE = "";
                   




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



