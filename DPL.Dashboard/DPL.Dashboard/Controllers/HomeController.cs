using DPL.DASHBOARD.Models;
using Dutility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DPL.DASHBOARD.Controllers
{
    public class HomeController : Controller
    {
        private string strSQL;
        

public  string strDeComID { get; set; }public  string connstring { get; set; }
//public ActionResult Index()
//{
//    return View();
//}




public ActionResult Login()
{

    return View();
}



    




        public ActionResult mGetHeader()
        {
        
            var allLedger = mGetHeader1();
            return Json(allLedger, JsonRequestBehavior.AllowGet);
        }
        public ActionResult shiftConfigDelete(int intShiftCongfigID)
        {

            var allLedger = DeleteShiftConfig(intShiftCongfigID);
            return Json(allLedger, JsonRequestBehavior.AllowGet);
        }

        public ActionResult mPostShiftConfig()
        {

            var allLedger = "";
            return Json(allLedger, JsonRequestBehavior.AllowGet);
        }
     
    



        public List<ShiftCongfig> mGetHeader1()
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

                List<ShiftCongfig> oooChequePrint = new List<ShiftCongfig>();

                strSQL = "SELECT  SHIFT_CONFIG_SERL, EMPLOYEE_SHIFT_NAME, EMPLOYEE_SHIFT_NAME_BANGLA, ";
                strSQL = strSQL + "SHIFT_CONFIG_DATE, SHIFT_CONFIG_START_TIME, SHIFT_CONFIG_END_TIME, OT_STATUS ";
                strSQL = strSQL + "FROM            HRS_SHIFT_CONFIG ";


                SqlCommand cmd = new SqlCommand(strSQL, gcnMain);
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {

                    ShiftCongfig oLedg = new ShiftCongfig();
                    oLedg.intShiftCongfigID = Convert.ToInt32(dr["SHIFT_CONFIG_SERL"]);
                    oLedg.strShiftName = dr["EMPLOYEE_SHIFT_NAME"].ToString();
                    oLedg.strShiftNameBangla = dr["EMPLOYEE_SHIFT_NAME_BANGLA"].ToString();
                    //oLedg.strShiftCongfigDate = ToDateTime(dr["SHIFT_CONFIG_DATE"]);
                    oLedg.strShiftCongfigStartTime = dr["SHIFT_CONFIG_START_TIME"].ToString();
                    oLedg.strShiftCongfigEndTime = dr["SHIFT_CONFIG_END_TIME"].ToString();
                    oLedg.strOtStatus = dr["OT_STATUS"].ToString();
                    oooChequePrint.Add(oLedg);
                }

                if (!dr.HasRows)
                {
                    ShiftCongfig oLedg = new ShiftCongfig();
                    oLedg.intShiftCongfigID = 0;
                    oLedg.strShiftName = "";
                    oLedg.strShiftNameBangla = "";
                    oLedg.strShiftCongfigDate = "";
                    oLedg.strShiftCongfigStartTime = "";
                    oLedg.strShiftCongfigEndTime = "";
                    oLedg.strOtStatus = "";
                    oooChequePrint.Add(oLedg);
                }
                dr.Close();
                gcnMain.Close();
                cmd.Dispose();
                return oooChequePrint;
            }

        }


        [HttpPost]
        public string mPostShiftConfig(ShiftCongfig obj)
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
                    strSQL = "INSERT INTO HRS_SHIFT_CONFIG (";
                    strSQL = strSQL + " EMPLOYEE_SHIFT_NAME,EMPLOYEE_SHIFT_NAME_BANGLA, ";
                    strSQL = strSQL + " SHIFT_CONFIG_START_TIME,SHIFT_CONFIG_END_TIME, OT_STATUS";
                    strSQL = strSQL + ") ";
                    strSQL = strSQL + "VALUES (";
                    strSQL = strSQL + "'" + obj.strEMPLOYEE_SHIFT_NAME + "',";
                    strSQL = strSQL + "'" + obj.strEMPLOYEE_SHIFT_NAME_BANGLA + "',";
                    strSQL = strSQL + "'" + obj.strSHIFT_CONFIG_START_TIME + "',";
                    strSQL = strSQL + "'" + obj.strSHIFT_CONFIG_END_TIME + "',";
                    strSQL = strSQL + "'" + obj.strOT_STATUS + "'";
                    strSQL = strSQL + ")";
                    cmdInsert.CommandText = strSQL;
                    cmdInsert.ExecuteNonQuery();
                    cmdInsert.Transaction.Commit();
                    return "added successfully";
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




         public string mUpdateShiftConfig(int intShiftCongfigID, string strShiftName, string strShiftNameBangla, string strShiftCongfigDate, string strShiftCongfigStartTime, string strShiftCongfigEndTime, string strOtStatus)
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

                     strSQL = "UPDATE HRS_SHIFT_CONFIG SET ";
                     strSQL = strSQL + "EMPLOYEE_SHIFT_NAME='" + strShiftName + "',";
                     strSQL = strSQL + "EMPLOYEE_SHIFT_NAME='" + strShiftNameBangla + "',";
                     //strSQL = strSQL + "SHIFT_CONFIG_DATE='" + strShiftConfigDate + "',";
                     //strSQL = strSQL + "SHIFT_CONFIG_EFFECTIVE_DATE=" + Utility.cvtSQLDateString(strShiftEffectiveDate) + ",";
                     strSQL = strSQL + "SHIFT_CONFIG_START_TIME=" + strShiftCongfigStartTime + ",";
                     strSQL = strSQL + "SHIFT_CONFIG_END_TIME=" + strShiftCongfigEndTime + ",";
                   
                     strSQL = strSQL + "OT_STATUS=" + strOtStatus + ", ";
                    
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


         public string DeleteShiftConfig(int intShiftCongfigID)
         {
             string strResponse = "";
             string connectionString = Utility.SQLConnstringComSwitch("0001");
             //connstring = Utility.SQLConnstringComSwitch("0001");
             using (SqlConnection gcnMain = new SqlConnection(connectionString))
             {
                 if (gcnMain.State == ConnectionState.Open)
                 {
                     gcnMain.Close();
                 }
                 try
                 {
                     gcnMain.Open();

                     SqlDataReader rsGet;
                     SqlCommand cmdDelete = new SqlCommand();
                     SqlTransaction myTrans;
                     myTrans = gcnMain.BeginTransaction();
                     cmdDelete.Connection = gcnMain;
                     cmdDelete.Transaction = myTrans;


                     strSQL = "DELETE FROM HRS_SHIFT_CONFIG WHERE SHIFT_CONFIG_SERL = " + intShiftCongfigID + "";
                     cmdDelete.CommandText = strSQL;
                     cmdDelete.ExecuteNonQuery();
                     strResponse = "Deleted...";

                     cmdDelete.Transaction.Commit();
                     gcnMain.Close();

                     return strResponse;
                 }

                 catch (Exception)
                 {
                     strResponse = "Delete...";
                     return strResponse;
                 }
                 finally
                 {
                     gcnMain.Close();
                 }

             }

         }







         public string connectionString { get; set; }
    }
}