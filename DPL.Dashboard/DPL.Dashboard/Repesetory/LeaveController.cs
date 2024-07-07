using DPL.DASHBOARD.Models;
using Dutility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DPL.DASHBOARD.Repesetory
{
    public class LeaveController : Controller
    {
        //
        // GET: /Leave/
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult mGetHolyDay()
        {

            var allLedger = mGetHoly();
            return Json(allLedger, JsonRequestBehavior.AllowGet);
        }

        public ActionResult mPostHolyDay()
        {

            var allLedger = "";
            return Json(allLedger, JsonRequestBehavior.AllowGet);
        }



        public ActionResult mGetTotalLeave()
        {

            var allLedger = mGetLeave();
            return Json(allLedger, JsonRequestBehavior.AllowGet);
        }

        public ActionResult mPostLeave()
        {

            var allLedger = "";
            return Json(allLedger, JsonRequestBehavior.AllowGet);
        }


        public ActionResult mUpdateLeave()
        {

            var allLedger = "";
            return Json(allLedger, JsonRequestBehavior.AllowGet);
        }


        public ActionResult mPostUserLeave()
        {

            var allLedger = "";
            return Json(allLedger, JsonRequestBehavior.AllowGet);
        }

        public ActionResult mUpdateHoly()
        {

            var allLedger = "";
            return Json(allLedger, JsonRequestBehavior.AllowGet);
        }

        public ActionResult mGetWeekendList()
        {

            var allLedger = mGetWeekend();
            return Json(allLedger, JsonRequestBehavior.AllowGet);
        }


        public ActionResult mPostUserWeekend()
        {

            var allLedger = "";
            return Json(allLedger, JsonRequestBehavior.AllowGet);
        }



        public ActionResult mUpdateLeaveStatus()
        {

            var allLedger = "";
            return Json(allLedger, JsonRequestBehavior.AllowGet);
        }

        public ActionResult mUpdateWeekendStatus()
        {

            var allLedger = "";
            return Json(allLedger, JsonRequestBehavior.AllowGet);
        }




        public ActionResult mGetLeaveUserList()
        {

            var allLedger = mGetLeaveList();
            //return Json(allLedger, JsonRequestBehavior.AllowGet);
            var jsonResult = Json(allLedger, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;

        }

           public ActionResult mGetUserLeaveReturnVal(LeaveConfig obj )
        {
            List<LeaveConfig> UserList = new List<LeaveConfig>();
            UserList = mGetUserLeaveReturn(obj);
            return Json(UserList, JsonRequestBehavior.AllowGet);
        }

        



        public List<LeaveConfig> mGetHoly()
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

                List<LeaveConfig> oooChequePrint = new List<LeaveConfig>();

                strSQL = "SELECT SERIAL_NO, HOLIDAY_DATE, DESCRIPTION, DIVISION_NAME ";
                strSQL = strSQL + "FROM           HRS_HOLIDAY ";


                SqlCommand cmd = new SqlCommand(strSQL, gcnMain);
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {

                    LeaveConfig oLedg = new LeaveConfig();
                    oLedg.intSERIAL_NO = Convert.ToInt32(dr["SERIAL_NO"]);
                    oLedg.strHOLIDAY_DATE = Convert.ToDateTime(dr["HOLIDAY_DATE"]).ToString("yyyy-MM-dd");
                    oLedg.strDESCRIPTION = dr["DESCRIPTION"].ToString();
                    oLedg.strDIVISION_NAME = dr["DIVISION_NAME"].ToString();
                   


                    oooChequePrint.Add(oLedg);
                }

                if (!dr.HasRows)
                {
                    LeaveConfig oLedg = new LeaveConfig();
                    oLedg.intSERIAL_NO = 0;
                    oLedg.strHOLIDAY_DATE = "";
                    oLedg.strDESCRIPTION = "";
                    oLedg.strDIVISION_NAME = "";
                   
                    oooChequePrint.Add(oLedg);
                }
                dr.Close();
                gcnMain.Close();
                cmd.Dispose();
                return oooChequePrint;
            }

        }


         [HttpPost]
        public string mPostHolyDay(LeaveConfig obj)
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
                    strSQL = "INSERT INTO HRS_HOLIDAY (";
                    strSQL = strSQL + " HOLIDAY_DATE, DESCRIPTION, DIVISION_NAME ";
                    strSQL = strSQL + ") ";
                    strSQL = strSQL + "VALUES (";
                    strSQL = strSQL + "'" + obj.strHOLIDAY_DATE + "',";
                    strSQL = strSQL + "'" + obj.strDESCRIPTION + "',";
                    strSQL = strSQL + "'" + obj.strDIVISION_NAME + "'";
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



         public List<LeaveConfig> mGetLeave()
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

                 List<LeaveConfig> oooChequePrint = new List<LeaveConfig>();

                 strSQL = "SELECT LEAVE_ID, LEAVE_NAME, NO_OF_DAYS,ALLOW_DEDUCTION_YN,DEDUCTION_ON,REF_HEAD,LEAVE_NATURE,DEFAULT_STATUS ";
                 strSQL = strSQL + "FROM         HRS_LEAVE_CONFIG ";


                 SqlCommand cmd = new SqlCommand(strSQL, gcnMain);
                 dr = cmd.ExecuteReader();
                 while (dr.Read())
                 {

                     LeaveConfig oLedg = new LeaveConfig();

                     oLedg.strLEAVE_ID = dr["LEAVE_ID"].ToString();
                     oLedg.strLEAVE_NAME = dr["LEAVE_NAME"].ToString();
                     oLedg.intNO_OF_DAYS = Convert.ToInt32(dr["NO_OF_DAYS"]);
                     oLedg.strALLOW_DEDUCTION_YN = dr["ALLOW_DEDUCTION_YN"].ToString();
                     oLedg.strDEDUCTION_ON = dr["DEDUCTION_ON"].ToString();
                     oLedg.strREF_HEAD = dr["REF_HEAD"].ToString();
                     oLedg.strLEAVE_NATURE = dr["LEAVE_NATURE"].ToString();
                     oLedg.strDEFAULT_STATUS = dr["DEFAULT_STATUS"].ToString();



                     oooChequePrint.Add(oLedg);
                 }

                 if (!dr.HasRows)
                 {
                     LeaveConfig oLedg = new LeaveConfig();
                     oLedg.strLEAVE_ID = "";
                     oLedg.strLEAVE_NAME = "";
                     oLedg.intNO_OF_DAYS = 0;
                     oLedg.strALLOW_DEDUCTION_YN  = "";
                     oLedg.strDEDUCTION_ON = "";
                     oLedg.strREF_HEAD = "";
                     oLedg.strLEAVE_NATURE = "";
                     oLedg.strDEFAULT_STATUS = "";
                     

                     oooChequePrint.Add(oLedg);
                 }
                 dr.Close();
                 gcnMain.Close();
                 cmd.Dispose();
                 return oooChequePrint;
             }

         }





         [HttpPost]
         public string mPostLeave(LeaveConfig obj)
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
                     strSQL = "INSERT INTO HRS_LEAVE_CONFIG (";
                     strSQL = strSQL + " LEAVE_ID, LEAVE_NAME, NO_OF_DAYS,ALLOW_DEDUCTION_YN,";
                     strSQL = strSQL + " DEDUCTION_ON,REF_HEAD,LEAVE_NATURE,DEFAULT_STATUS ";
                     strSQL = strSQL + ") ";
                     strSQL = strSQL + "VALUES (";
                     strSQL = strSQL + "'" + obj.strLEAVE_ID + "',";
                     strSQL = strSQL + "'" + obj.strLEAVE_NAME + "',";
                     strSQL = strSQL + "'" + obj.intNO_OF_DAYS + "',";
                     strSQL = strSQL + "'" + obj.strALLOW_DEDUCTION_YN + "',";
                     strSQL = strSQL + "'" + obj.strDEDUCTION_ON + "',";
                     strSQL = strSQL + "'" + obj.strREF_HEAD + "',";
                     strSQL = strSQL + "'" + obj.strLEAVE_NATURE + "',";
                     strSQL = strSQL + "'" + obj.strDEFAULT_STATUS + "'";
                     
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



          [HttpPost]

         public string mUpdateLeave(LeaveConfig obj)
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

                     strSQL = "UPDATE HRS_LEAVE_CONFIG SET ";
                     strSQL += "LEAVE_NAME = '" + obj.strLEAVE_NAME + "', ";
                     strSQL += "NO_OF_DAYS = '" + obj.intNO_OF_DAYS + "', ";
                     strSQL += "ALLOW_DEDUCTION_YN = '" + obj.strALLOW_DEDUCTION_YN + "', ";
                     strSQL += "DEDUCTION_ON = '" + obj.strDEDUCTION_ON + "', ";
                     strSQL += "REF_HEAD = '" + obj.strREF_HEAD + "', ";
                     strSQL += "LEAVE_NATURE = '" + obj.strLEAVE_NATURE + "', ";
                     strSQL += "DEFAULT_STATUS = '" + obj.strDEFAULT_STATUS + "' ";
                     strSQL += "WHERE LEAVE_ID = '" + obj.strLEAVE_ID + "'";

                     cmdInsert.CommandText = strSQL;
                     cmdInsert.ExecuteNonQuery();

                     cmdInsert.Transaction.Commit();
                     return "1";
                 }
                 catch (SqlException ex)
                 {
                     return ex.ToString();
                 }
                 finally
                 {
                     gcnMain.Dispose();
                 }
             }
         }



        [HttpPost]
         public string mPostUserLeave(LeaveConfig obj)
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
                     strSQL = "INSERT INTO HRS_EMP_LEAVE (";
                     strSQL = strSQL + " EMP_LEAVE_KEY, EMP_CARD_NO, LEAVE_ID, FRIDAY, FROM_DATE, TO_DATE, NO_OF_DAYS,";
                     strSQL = strSQL + "FIRST_DATE_MLEAVE, SECOND_DATE_MLEAVE, APPROVED_STATUS, COMMENTS, ";
                     strSQL = strSQL + " RES_PEREMP_CARD_NO, FAL_HR_APP, DESTINATION, USER_LOGIN_NAME, INSERT_DATE, UPDATE_DATE, HOD_APP_DATE, HR_APP_DATE, B_LEAVE_KEY, B_M_R";
                     strSQL = strSQL + ") ";
                     strSQL = strSQL + "VALUES (";
                     strSQL = strSQL + "'" + obj.strEMP_LEAVE_KEY + "',";
                     strSQL = strSQL + "'" + obj.strEMP_CARD_NO + "',";
                     strSQL = strSQL + "'" + obj.strLEAVE_ID + "',";
                     strSQL = strSQL + "'" + obj.intFRIDAY + "',";
                     strSQL = strSQL + "'" + obj.strFROM_DATE + "',";
                     strSQL = strSQL + "'" + obj.strTO_DATE + "',";
                     strSQL = strSQL + "'" + obj.intNO_OF_DAYS + "',";
                     strSQL = strSQL + "'" + obj.strFIRST_DATE_MLEAVE + "',";
                     strSQL = strSQL + "'" + obj.strSECOND_DATE_MLEAVE + "',";
                     strSQL = strSQL + "'" + obj.strAPPROVED_STATUS + "',";
                     strSQL = strSQL + "'" + obj.strCOMMENTS + "',";
                     strSQL = strSQL + "'" + obj.strRES_PEREMP_CARD_NO + "',";
                     strSQL = strSQL + "'" + obj.strFAL_HR_APP + "',";
                     strSQL = strSQL + "'" + obj.strDESTINATION + "',";
                     strSQL = strSQL + "'" + obj.strUSER_LOGIN_NAME + "',";
                     strSQL = strSQL + "'" + obj.strINSERT_DATE + "',";
                     strSQL = strSQL + "'" + obj.strUPDATE_DATE + "',";
                     strSQL = strSQL + "'" + obj.strHOD_APP_DATE + "',";
                     strSQL = strSQL + "'" + obj.strHR_APP_DATE + "',";
                     strSQL = strSQL + "'" + obj.intB_LEAVE_KEY + "',";
                     strSQL = strSQL + "'" + obj.strB_M_R + "'";
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




        [HttpPost]

        public string mUpdateHoly(LeaveConfig obj)
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

                    strSQL = "UPDATE HRS_HOLIDAY SET ";
                    strSQL += "HOLIDAY_DATE = '" + obj.strHOLIDAY_DATE + "', ";
                    strSQL += "DIVISION_NAME = '" + obj.strDIVISION_NAME + "' ";
                    strSQL += "WHERE DESCRIPTION = '" + obj.strDESCRIPTION + "'";

                    cmdInsert.CommandText = strSQL;
                    cmdInsert.ExecuteNonQuery();

                    cmdInsert.Transaction.Commit();
                    return "1";
                }
                catch (SqlException ex)
                {
                    return ex.ToString();
                }
                finally
                {
                    gcnMain.Dispose();
                }
            }
        }




        //public List<LeaveConfig> mGetWeekend()
        //{
        //    string strSQL = null;
        //    string connectionString = Utility.SQLConnstringComSwitch("0001");

        //    using (SqlConnection gcnMain = new SqlConnection(connectionString))
        //    {
        //        if (gcnMain.State == ConnectionState.Open)
        //        {
        //            gcnMain.Close();
        //        }
        //        gcnMain.Open();
        //        SqlDataReader dr;

        //        List<LeaveConfig> oooChequePrint = new List<LeaveConfig>();


        //        strSQL = "SELECT  SerialNumber,WEEKND_KEY, EMP_CARD_NO, EFFECTIVE_DATE, EMP_WEEKEND,POS_TYPE,REF_NO, ACTION ";

        //        strSQL = strSQL + "FROM           HRS_EMPLOYEE_WEEKEND ";





        //        SqlCommand cmd = new SqlCommand(strSQL, gcnMain);
        //        dr = cmd.ExecuteReader();
        //        while (dr.Read())
        //        {

        //            LeaveConfig oLedg = new LeaveConfig();

        //            oLedg.intSerialNumber = Convert.ToInt32(dr["SerialNumber"]);
        //            oLedg.strWEEKND_KEY = dr["WEEKND_KEY"].ToString();
        //            oLedg.strEMP_CARD_NO = dr["EMP_CARD_NO"].ToString();
        //            oLedg.strEFFECTIVE_DATE = Convert.ToDateTime(dr["EFFECTIVE_DATE"]).ToString("yyyy-MM-dd");
        //            oLedg.strEMP_WEEKEND = dr["EMP_WEEKEND"].ToString();
        //            oLedg.intPOS_TYPE = Convert.ToInt32(dr["POS_TYPE"]);
        //            oLedg.strREF_NO = dr["REF_NO"].ToString();
        //            oLedg.strACTION = dr["ACTION"].ToString();







        //            oooChequePrint.Add(oLedg);
        //        }

        //        if (!dr.HasRows)
        //        {
        //            LeaveConfig oLedg = new LeaveConfig();
        //            oLedg.intSerialNumber = 0;
        //            oLedg.strWEEKND_KEY = "";
        //            oLedg.strEMP_CARD_NO = "";
        //            oLedg.strEFFECTIVE_DATE = "";
        //            oLedg.strEMP_WEEKEND = "";
        //            oLedg.intPOS_TYPE = 0;
        //            oLedg.strREF_NO = "";
        //            oLedg.strACTION = "";



        //            oooChequePrint.Add(oLedg);
        //        }
        //        dr.Close();
        //        gcnMain.Close();
        //        cmd.Dispose();
        //        return oooChequePrint;
        //    }

        //}



        //public List<LeaveConfig> mGetWeekend()
        //{
        //    string strSQL = "SELECT SerialNumber, WEEKND_KEY, EMP_CARD_NO, EFFECTIVE_DATE, EMP_WEEKEND, POS_TYPE, REF_NO, ACTION " +
        //                    "FROM HRS_EMPLOYEE_WEEKEND";

        //    string connectionString = Utility.SQLConnstringComSwitch("0001");

        //    List<LeaveConfig> leaveConfigs = new List<LeaveConfig>();

        //    using (SqlConnection connection = new SqlConnection(connectionString))
        //    {
        //        using (SqlCommand command = new SqlCommand(strSQL, connection))
        //        {
        //            try
        //            {
        //                connection.Open();
        //                SqlDataReader reader = command.ExecuteReader();

        //                while (reader.Read())
        //                {
        //                    LeaveConfig leaveConfig = new LeaveConfig
        //                    {
        //                        intSerialNumber = Convert.ToInt32(reader["SerialNumber"]),
        //                        strWEEKND_KEY = reader["WEEKND_KEY"].ToString(),
        //                        strEMP_CARD_NO = reader["EMP_CARD_NO"].ToString(),
        //                        strEFFECTIVE_DATE = Convert.ToDateTime(reader["EFFECTIVE_DATE"]).ToString("yyyy-MM-dd"),
        //                        strEMP_WEEKEND = reader["EMP_WEEKEND"].ToString(),
        //                        intPOS_TYPE = Convert.ToInt32(reader["POS_TYPE"]),
        //                        strREF_NO = reader["REF_NO"].ToString(),
        //                        strACTION = reader["ACTION"].ToString()
        //                    };

        //                    leaveConfigs.Add(leaveConfig);
        //                }

        //                if (leaveConfigs.Count == 0)
        //                {
        //                    // If no rows returned, add a default LeaveConfig object
        //                    leaveConfigs.Add(new LeaveConfig());
        //                }

        //                reader.Close();
        //            }
        //            catch (Exception ex)
        //            {
        //                // Handle any exceptions here
        //                Console.WriteLine("An error occurred: " + ex.Message);
        //            }
        //        }
        //    }

        //    return leaveConfigs;
        //}


        public List<LeaveConfig> mGetWeekend()
        {
            string strSQL = "SELECT  WEEKND_KEY, EMP_CARD_NO, EFFECTIVE_DATE, EMP_WEEKEND, POS_TYPE, REF_NO " +
                            "FROM HRS_EMPLOYEE_WEEKEND";

            string connectionString = Utility.SQLConnstringComSwitch("0001");

            List<LeaveConfig> leaveConfigs = new List<LeaveConfig>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(strSQL, connection))
                {
                    try
                    {
                        connection.Open();
                        SqlDataReader reader = command.ExecuteReader();

                        while (reader.Read())
                        {
                            LeaveConfig leaveConfig = new LeaveConfig
                            {
                                //intSerialNumber = Convert.ToInt32(reader["SerialNumber"]),
                                strWEEKND_KEY = reader["WEEKND_KEY"].ToString(),
                                strEMP_CARD_NO = reader["EMP_CARD_NO"].ToString(),
                                strEFFECTIVE_DATE = Convert.ToDateTime(reader["EFFECTIVE_DATE"]).ToString("yyyy-MM-dd"),
                                strEMP_WEEKEND = reader["EMP_WEEKEND"].ToString(),
                                intPOS_TYPE = Convert.ToInt32(reader["POS_TYPE"]),
                                strREF_NO = reader["REF_NO"].ToString()
                               
                            };

                            leaveConfigs.Add(leaveConfig);
                        }

                        if (leaveConfigs.Count == 0)
                        {
                            // If no rows returned, add a default LeaveConfig object
                            leaveConfigs.Add(new LeaveConfig());
                        }

                        reader.Close();
                    }
                    catch (Exception ex)
                    {
                        // Handle any exceptions here
                        Console.WriteLine("An error occurred: " + ex.Message);
                    }
                }
            }

            return leaveConfigs;
        }




        public List<LeaveConfig> mGetLeaveList()
        {
            string sqlQuery = @"SELECT HRS_EMP_LEAVE.EMP_CARD_NO, EMP_NAME, EMP_LEAVE_KEY, LEAVE_ID, FRIDAY, FROM_DATE, TO_DATE, NO_OF_DAYS, PAY_LEAVE_SERIAL,
                            FIRST_DATE_MLEAVE, SECOND_DATE_MLEAVE, APPROVED_STATUS, COMMENTS, 
                            RES_PEREMP_CARD_NO, FAL_HR_APP, DESTINATION, USER_LOGIN_NAME,
                            INSERT_DATE, UPDATE_DATE, HOD_APP_DATE, HR_APP_DATE, B_LEAVE_KEY, B_M_R 
                       FROM HRS_EMP_LEAVE 
                       JOIN HRS_EMPLOYEE ON HRS_EMP_LEAVE.EMP_CARD_NO = HRS_EMPLOYEE.EMP_CARD_NO";


            string connectionString = Utility.SQLConnstringComSwitch("0001");

            List<LeaveConfig> leaveList = new List<LeaveConfig>();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            LeaveConfig leave = MapLeaveConfigFromDataReader(reader);
                            leaveList.Add(leave);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Log the error details, including the stack trace
                Console.WriteLine("Error in mGetLeaveList: {ex.Message}");
            }

            return leaveList;
        }


        private LeaveConfig MapLeaveConfigFromDataReader(SqlDataReader reader)
        {
            return new LeaveConfig
            {
                strEMP_NAME = reader["EMP_NAME"].ToString(),
                strEMP_LEAVE_KEY = reader["EMP_LEAVE_KEY"].ToString(),
                strEMP_CARD_NO = reader["EMP_CARD_NO"].ToString(),
                strLEAVE_ID = reader["LEAVE_ID"].ToString(),
                intFRIDAY = Convert.ToInt32(reader["FRIDAY"]),
                strFROM_DATE = GetSafeDateString(reader, "FROM_DATE"),
                strTO_DATE = GetSafeDateString(reader, "TO_DATE"),
                intNO_OF_DAYS = Convert.ToInt32(reader["NO_OF_DAYS"]),
                intPAY_LEAVE_SERIAL = Convert.ToInt32(reader["PAY_LEAVE_SERIAL"]),
                strFIRST_DATE_MLEAVE = GetSafeDateString(reader, "FIRST_DATE_MLEAVE"),
                strSECOND_DATE_MLEAVE = GetSafeDateString(reader, "SECOND_DATE_MLEAVE"),
                strAPPROVED_STATUS = reader["APPROVED_STATUS"].ToString(),
                strCOMMENTS = reader["COMMENTS"].ToString(),
                strRES_PEREMP_CARD_NO = reader["RES_PEREMP_CARD_NO"].ToString(),
                strFAL_HR_APP = reader["FAL_HR_APP"].ToString(),
                strDESTINATION = reader["DESTINATION"].ToString(),
                strUSER_LOGIN_NAME = reader["USER_LOGIN_NAME"].ToString(),
                strINSERT_DATE = GetSafeDateString(reader, "INSERT_DATE"),
                strUPDATE_DATE = GetSafeDateString(reader, "UPDATE_DATE"),
                strHOD_APP_DATE = GetSafeDateString(reader, "HOD_APP_DATE"),
                strHR_APP_DATE = GetSafeDateString(reader, "HR_APP_DATE"),
                intB_LEAVE_KEY = Convert.ToInt32(reader["B_LEAVE_KEY"]),
                strB_M_R = reader["B_M_R"].ToString()
            };
        }
       

        private string GetSafeDateString(SqlDataReader reader, string columnName)
        {
            object value = reader[columnName];
            return (value != DBNull.Value) ? Convert.ToDateTime(value).ToString("yyyy-MM-dd") : null;
        }




      
       


[HttpPost]
public List<LeaveConfig> mGetUserLeaveReturn(LeaveConfig obj)
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

        List<LeaveConfig> oooChequePrint = new List<LeaveConfig>();

        // Corrected SQL query
        strSQL = "SELECT * FROM HRS_EMP_LEAVE WHERE EMP_CARD_NO='" + obj.strEMP_CARD_NO + "' AND FROM_DATE >= '" + obj.strFROM_DATE + "' AND TO_DATE <= '" + obj.strTO_DATE + "'";

        SqlCommand cmd = new SqlCommand(strSQL, gcnMain);
        dr = cmd.ExecuteReader();

        while (dr.Read())
        {
            LeaveConfig oLedg = new LeaveConfig();

            oLedg.strEMP_LEAVE_KEY = dr["EMP_LEAVE_KEY"].ToString();
            oLedg.strEMP_CARD_NO = dr["EMP_CARD_NO"].ToString();
            oLedg.strLEAVE_ID = dr["LEAVE_ID"].ToString();
            oLedg.intFRIDAY = Convert.ToInt32(dr["FRIDAY"]);
            oLedg.strFROM_DATE = Convert.ToDateTime(dr["FROM_DATE"]).ToString("yyyy-MM-dd");
            oLedg.strTO_DATE = Convert.ToDateTime(dr["TO_DATE"]).ToString("yyyy-MM-dd");
            oLedg.intNO_OF_DAYS = Convert.ToInt32(dr["NO_OF_DAYS"]);
            oLedg.strFIRST_DATE_MLEAVE = dr["FIRST_DATE_MLEAVE"].ToString();
            oLedg.strSECOND_DATE_MLEAVE = dr["SECOND_DATE_MLEAVE"].ToString();
            oLedg.strAPPROVED_STATUS = dr["APPROVED_STATUS"].ToString();
            oLedg.strCOMMENTS = dr["COMMENTS"].ToString();
            oLedg.strRES_PEREMP_CARD_NO = dr["RES_PEREMP_CARD_NO"].ToString();
            oLedg.strFAL_HR_APP = dr["FAL_HR_APP"].ToString();
            oLedg.strDESTINATION = dr["DESTINATION"].ToString();
            oLedg.strUSER_LOGIN_NAME = dr["USER_LOGIN_NAME"].ToString();
            oLedg.strINSERT_DATE = dr["INSERT_DATE"].ToString();
            oLedg.strUPDATE_DATE = dr["UPDATE_DATE"].ToString();
            oLedg.strHOD_APP_DATE = dr["HOD_APP_DATE"].ToString();
            oLedg.strHR_APP_DATE = dr["HR_APP_DATE"].ToString();
            oLedg.intB_LEAVE_KEY = Convert.ToInt32(dr["B_LEAVE_KEY"]);
            oLedg.strB_M_R = dr["B_M_R"].ToString();

            oooChequePrint.Add(oLedg);
        }

        if (!dr.HasRows)
        {
            LeaveConfig oLedg = new LeaveConfig();

            oLedg.strEMP_LEAVE_KEY = "";
            oLedg.strEMP_CARD_NO = "";
            oLedg.strLEAVE_ID = "";
            oLedg.intFRIDAY = 0;
            oLedg.strFROM_DATE = "";
            oLedg.strTO_DATE = "";
            oLedg.intNO_OF_DAYS = 0;
            oLedg.strFIRST_DATE_MLEAVE = "";
            oLedg.strSECOND_DATE_MLEAVE = "";
            oLedg.strAPPROVED_STATUS = "";
            oLedg.strCOMMENTS = "";
            oLedg.strRES_PEREMP_CARD_NO = "";
            oLedg.strFAL_HR_APP = "";
            oLedg.strDESTINATION = "";
            oLedg.strUSER_LOGIN_NAME = "";
            oLedg.strINSERT_DATE = "";
            oLedg.strUPDATE_DATE = "";
            oLedg.strHOD_APP_DATE = "";
            oLedg.strHR_APP_DATE = "";
            oLedg.intB_LEAVE_KEY = 0;
            oLedg.strB_M_R = "";

            oooChequePrint.Add(oLedg);
        }

        dr.Close();
        gcnMain.Close();
        cmd.Dispose();
        return oooChequePrint;
    }
}







[HttpPost]

public string mPostUserWeekend(LeaveConfig obj)
{
    string strSQL = null;
    string connectionString = Utility.SQLConnstringComSwitch("0001");

    using (SqlConnection gcnMain = new SqlConnection(connectionString))
    {
        try
        {
            gcnMain.Open();

            using (SqlCommand cmdInsert = gcnMain.CreateCommand())
            {
                SqlTransaction myTrans = gcnMain.BeginTransaction();
                cmdInsert.Transaction = myTrans;

                string lastTwoDigits = obj.strEMP_CARD_NO.Substring(obj.strEMP_CARD_NO.Length - 2);
                string[] dateParts = obj.strEFFECTIVE_DATE.Split('-');
                string empweekenKey = obj.strEMP_CARD_NO + dateParts[0] + dateParts[1] + dateParts[2];
                string SerialNumberkey = lastTwoDigits + dateParts[0] + dateParts[1] + dateParts[2];

                strSQL = "INSERT INTO HRS_EMPLOYEE_WEEKEND (SerialNumber, WEEKND_KEY, EMP_CARD_NO, EFFECTIVE_DATE, EMP_WEEKEND, POS_TYPE) ";
                strSQL += "VALUES (@SerialNumberkey, @empweekenKey, @empCardNo, @effectiveDate, @empWeekend, @posType)";

                cmdInsert.CommandText = strSQL;
                cmdInsert.Parameters.AddWithValue("@SerialNumberkey", SerialNumberkey);
                cmdInsert.Parameters.AddWithValue("@empweekenKey", empweekenKey);
                cmdInsert.Parameters.AddWithValue("@empCardNo", obj.strEMP_CARD_NO);
                cmdInsert.Parameters.AddWithValue("@effectiveDate", obj.strEFFECTIVE_DATE);
                cmdInsert.Parameters.AddWithValue("@empWeekend", obj.strEMP_WEEKEND);
                cmdInsert.Parameters.AddWithValue("@posType", obj.intPOS_TYPE);

                cmdInsert.ExecuteNonQuery();
                myTrans.Commit();
                return "1";
            }
        }
        catch (Exception ex)
        {

            return ex.Message;
        }
    }
}







[HttpPost]
public string mUpdateLeaveStatus(LeaveConfig obj)
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

            strSQL = "UPDATE HRS_EMP_LEAVE SET APPROVED_STATUS='" + obj.strAPPROVED_STATUS + "' ";
            strSQL = strSQL + "WHERE PAY_LEAVE_SERIAL='" + obj.intPAY_LEAVE_SERIAL + "'";
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
public string mUpdateWeekendStatus(LeaveConfig obj)
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

            strSQL = "UPDATE HRS_EMPLOYEE_WEEKEND SET ACTION='" + obj.strACTION + "' ";
            strSQL = strSQL + "WHERE SerialNumber='" + obj.intSerialNumber + "'";
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


         
    }
}