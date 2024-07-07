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
    public class TourController : Controller
    {
        //
        // GET: /Tour/
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult mPostTour()
        {

            var allLedger = "";
            return Json(allLedger, JsonRequestBehavior.AllowGet);
        }


        public ActionResult mGetTourList(TourConfig obj)
        {

            var allLedger = mGetTour(obj);
            return Json(allLedger, JsonRequestBehavior.AllowGet);
        }

        public ActionResult mUpdateTour()
        {

            var allLedger = "";
            return Json(allLedger, JsonRequestBehavior.AllowGet);
        }

        public ActionResult mGetTourLedgerList(TourConfigLedger obj)
        {

            var allLedger = mGetTourLedger(obj);
            return Json(allLedger, JsonRequestBehavior.AllowGet);
        }

        public ActionResult mGetTourPlanList(TourPlanConfig obj)
        {

            var allLedger = mGetTourPlan(obj);
            return Json(allLedger, JsonRequestBehavior.AllowGet);
        }



        public ActionResult mPostTourPlanRoute()
        {

            var allLedger = "";
            return Json(allLedger, JsonRequestBehavior.AllowGet);
        }




        public ActionResult mGetTourPlanRouteList(TourPlanShiftConfig obj)
        {

            var allLedger = mGetTourPlanRoute(obj);
            return Json(allLedger, JsonRequestBehavior.AllowGet);
        }



        public ActionResult getFullMonthData(TourPlanShiftConfig obj)
        {
            List<TourPlanShiftConfig> UserList = new List<TourPlanShiftConfig>();
            UserList = getFullMonth(obj);
            return Json(UserList, JsonRequestBehavior.AllowGet);
        }



        public ActionResult mGetTourPlanRouteReturnValue(TourPlanShiftConfig obj)
        {
            List<TourPlanShiftConfig> UserList = new List<TourPlanShiftConfig>();
            UserList = mGetTourPlanRouteReturn(obj);
            return Json(UserList, JsonRequestBehavior.AllowGet);
        }





        public List<TourConfig> mGetTour(TourConfig obj)
    {
        string strSQL = null;
        string connectionString = Utility.SQLConnstringComSwitch("0001");
        List<TourConfig> tourList = new List<TourConfig>();

        using (SqlConnection gcnMain = new SqlConnection(connectionString))
        {
            gcnMain.Open();

            strSQL = "SELECT TOUR_NAME, TOUR_STATUS,SERIAL_NO FROM HRS_EMP_TOUR";

            using (SqlCommand cmd = new SqlCommand(strSQL, gcnMain))
            {
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        TourConfig tour = new TourConfig();
                        tour.strTOUR_NAME = dr["TOUR_NAME"].ToString();
                        tour.strTOUR_STATUS = dr["TOUR_STATUS"].ToString();
                        tour.intSERIAL_NO =Convert.ToInt32(dr["SERIAL_NO"]);
                        tourList.Add(tour);
                    }
                }
            }
        }

        if (tourList.Count == 0)
        {
            
            tourList.Add(new TourConfig());
        }

        return tourList;
    }





        [HttpPost]
        public string mPostTour(TourConfig obj)
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
                    strSQL = "INSERT INTO HRS_EMP_TOUR (";
                    strSQL = strSQL + " TOUR_NAME, TOUR_STATUS";
                    strSQL = strSQL + ") ";
                    strSQL = strSQL + "VALUES (";
                    strSQL = strSQL + "'" + obj.strTOUR_NAME + "',";
                    strSQL = strSQL + "'" + obj.strTOUR_STATUS + "'";
                 
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

        public string mUpdateTour(TourConfig obj)
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

                    strSQL = "UPDATE HRS_EMP_TOUR SET ";
                 
                    strSQL += "TOUR_STATUS = '" + obj.strTOUR_STATUS + "' ";


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


        public List<TourConfigLedger> mGetTourLedger(TourConfigLedger obj)
        {
            string strSQL = null;
            string connectionString = Utility.SQLConnstringComSwitch("0001");
            List<TourConfigLedger> tourList = new List<TourConfigLedger>();

            using (SqlConnection gcnMain = new SqlConnection(connectionString))
            {
                gcnMain.Open();

                strSQL = "select * from SMART0005.dbo.ACC_LEDGER_Z_D_A ";

                using (SqlCommand cmd = new SqlCommand(strSQL, gcnMain))
                {
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            TourConfigLedger tour = new TourConfigLedger();
                            tour.intBRANCH_ID = Convert.ToInt32(dr["BRANCH_ID"]);
                            tour.strZONE = dr["ZONE"].ToString();
                            tour.strDIVISION = dr["DIVISION"].ToString();
                            tour.strAREA = dr["AREA"].ToString();
                            tour.strLEDGER_NAME = dr["LEDGER_NAME"].ToString();
                            tour.strTERITORRY_CODE = dr["TERITORRY_CODE"].ToString();
                            tour.strTERRITORRY_NAME = dr["TERRITORRY_NAME"].ToString();
                            tour.strLEDGER_NAME_MERZE = dr["LEDGER_NAME_MERZE"].ToString();
                            tour.intLEDGER_STATUS = Convert.ToInt32(dr["LEDGER_STATUS"]);
                            tour.strGR_MOBILE_NO = dr["GR_MOBILE_NO"].ToString();
                            tour.intHALT_MPO = Convert.ToInt32(dr["HALT_MPO"]);
                            tour.strHL_LEDGER_NAME = dr["HL_LEDGER_NAME"].ToString();
                            tour.strPF_LEDGER_NAME = dr["PF_LEDGER_NAME"].ToString();
                            tour.strINSERT_DATE = dr["INSERT_DATE"].ToString();
                            tour.strROUTE_NAME = dr["ROUTE_NAME"].ToString();
                            tour.strLEDGER_CLASS = dr["LEDGER_CLASS"].ToString();
                            tour.strLEDGER_ADD_DATE = dr["LEDGER_ADD_DATE"].ToString();
                            tour.strLEDGER_RESIGN_DATE = dr["LEDGER_RESIGN_DATE"].ToString();
                            tour.strMPO_DIV = dr["MPO_DIV"].ToString();
                            tour.strGODOWNS_NAME = dr["GODOWNS_NAME"].ToString();
                            tour.strMPO_CARD_NO = dr["MPO_CARD_NO"].ToString();
                            tour.intCARTON_AMNT = Convert.ToInt32(dr["CARTON_AMNT"]);
                            tourList.Add(tour);
                        }
                    }
                }
            }

            if (tourList.Count == 0)
            {

                tourList.Add(new TourConfigLedger());
            }

            return tourList;
        }



        public List<TourPlanConfig> mGetTourPlan(TourPlanConfig obj)
        {
            string strSQL = null;
            string connectionString = Utility.SQLConnstringComSwitch("0001");
            List<TourPlanConfig> tourList = new List<TourPlanConfig>();

            using (SqlConnection gcnMain = new SqlConnection(connectionString))
            {
                gcnMain.Open();

                strSQL = "select * from SMART0005.dbo.ACC_LEDGERGROUP WHERE MPO_TYPE IN ('AH','DH','ZH') AND GR_STATUS =0 AND EMP_CARD_NO IS NOT NULL ";

                using (SqlCommand cmd = new SqlCommand(strSQL, gcnMain))
                {
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            TourPlanConfig tour = new TourPlanConfig();
                            tour.strGR_NAME = dr["GR_NAME"].ToString();
                            tour.strEMP_CARD_NO = dr["EMP_CARD_NO"].ToString();
                            
                            tourList.Add(tour);
                        }
                    }
                }
            }

            if (tourList.Count == 0)
            {

                tourList.Add(new TourPlanConfig());
            }

            return tourList;
        }






        [HttpPost]
        public string mPostTourPlanRoute(List<TourPlanShiftConfig> tourPlans)
        {
            string strSQL = "";
            
            string strresponse = "";
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


                    foreach (var tourPlan in tourPlans)
                    {

                        strSQL = "INSERT INTO HRS_EMP_TOUR_PLAN(";
                        strSQL = strSQL + " USER_NAME, EMP_CARD_NO, MARKET_NAME, ROUTE, SHIFT,  ";
                        strSQL = strSQL + "TOUR_TYPE, NOTE, EFFECTIVE_DATE, PURPOSE, ACCOMPANY_WITH, ";
                        strSQL = strSQL + "START_LOCATION, END_LOCATION, ACTION ";
                        strSQL = strSQL + ")";
                        strSQL = strSQL + " VALUES(";
                        strSQL = strSQL + "'" + tourPlan.strUSER_NAME + "',";
                        strSQL = strSQL + "'" + tourPlan.strEMP_CARD_NO + "',";
                        strSQL = strSQL + "'" + tourPlan.strMARKET_NAME + "',";
                        strSQL = strSQL + "'" + tourPlan.strROUTE + "',";
                        strSQL = strSQL + "'" + tourPlan.strSHIFT + "',";
                        strSQL = strSQL + "'" + tourPlan.strTOUR_TYPE + "',";
                        strSQL = strSQL + "N'" + tourPlan.strNOTE + "',";
                        strSQL = strSQL + "'" + tourPlan.strEFFECTIVE_DATE + "',";
                        strSQL = strSQL + "'" + tourPlan.strPURPOSE + "',";
                        strSQL = strSQL + "'" + tourPlan.strACCOMPANY_WITH + "',";
                        strSQL = strSQL + "'" + tourPlan.strSTART_LOCATION + "',";
                        strSQL = strSQL + "'" + tourPlan.strEND_LOCATION + "',";
                        strSQL = strSQL + "'" + tourPlan.strACTION + "'";

                        strSQL = strSQL +  ")";

                        cmdInsert.CommandText = strSQL;
                        cmdInsert.ExecuteNonQuery();
                    }
                    cmdInsert.Transaction.Commit();
                    strresponse = "1";
                    return strresponse;
                }

                catch (SqlException)
                {
                    return "Sorry!  already Exists..";
                }
                finally
                {
                    gcnMain.Dispose();

                }
            }
        }







        public List<TourPlanShiftConfig> mGetTourPlanRoute(TourPlanShiftConfig obj)
        {
            string strSQL = null;
            string connectionString = Utility.SQLConnstringComSwitch("0001");
            List<TourPlanShiftConfig> tourList = new List<TourPlanShiftConfig>();

            using (SqlConnection gcnMain = new SqlConnection(connectionString))
            {
                gcnMain.Open();

                strSQL = "select * from HRS_EMP_TOUR_PLAN ";

                using (SqlCommand cmd = new SqlCommand(strSQL, gcnMain))
                {
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            TourPlanShiftConfig tour = new TourPlanShiftConfig();



                            tour.strUSER_NAME = dr["USER_NAME"].ToString();
                            tour.strEMP_CARD_NO = dr["EMP_CARD_NO"].ToString();
                            tour.strMARKET_NAME = dr["MARKET_NAME"].ToString();
                            tour.strROUTE = dr["ROUTE"].ToString();
                            tour.strSHIFT = dr["SHIFT"].ToString();
                            tour.strTOUR_TYPE = dr["TOUR_TYPE"].ToString();
                            tour.strNOTE = dr["NOTE"].ToString();
                            tour.strEFFECTIVE_DATE = dr["EFFECTIVE_DATE"].ToString();
                            tour.strPURPOSE = dr["PURPOSE"].ToString();
                            tour.strACCOMPANY_WITH = dr["ACCOMPANY_WITH"].ToString();
                            tour.strSTART_LOCATION = dr["START_LOCATION"].ToString();
                            tour.strEND_LOCATION = dr["END_LOCATION"].ToString();
                            tour.strACTION = dr["ACTION"].ToString();

                            tourList.Add(tour);
                        }
                    }
                }
            }

            if (tourList.Count == 0)
            {

                tourList.Add(new TourPlanShiftConfig());
            }

            return tourList;
        }









        [HttpPost]
        public List<TourPlanShiftConfig> mGetTourPlanRouteReturn(TourPlanShiftConfig obj)
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

                List<TourPlanShiftConfig> oooChequePrint = new List<TourPlanShiftConfig>();

                // Corrected SQL query
                strSQL = "SELECT * FROM HRS_EMP_TOUR_PLAN WHERE EMP_CARD_NO='" + obj.strEMP_CARD_NO + "' AND EFFECTIVE_DATE >= '" + obj.strEFFECTIVE_DATE + "' ";

                SqlCommand cmd = new SqlCommand(strSQL, gcnMain);
                dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    TourPlanShiftConfig oLedg = new TourPlanShiftConfig();

                    oLedg.strUSER_NAME = dr["USER_NAME"].ToString();
                    oLedg.strEMP_CARD_NO = dr["EMP_CARD_NO"].ToString();
                    oLedg.strMARKET_NAME = dr["MARKET_NAME"].ToString();
                    oLedg.strROUTE = dr["ROUTE"].ToString();
                    oLedg.strSHIFT = dr["SHIFT"].ToString();
                    oLedg.strTOUR_TYPE = dr["TOUR_TYPE"].ToString();
                    oLedg.strNOTE = dr["NOTE"].ToString();
                    oLedg.strEFFECTIVE_DATE = dr["EFFECTIVE_DATE"].ToString();
                    oLedg.strPURPOSE = dr["PURPOSE"].ToString();
                    oLedg.strACCOMPANY_WITH = dr["ACCOMPANY_WITH"].ToString();
                    oLedg.strSTART_LOCATION = dr["START_LOCATION"].ToString();
                    oLedg.strEND_LOCATION = dr["END_LOCATION"].ToString();
                    oLedg.strACTION = dr["ACTION"].ToString();


                    oooChequePrint.Add(oLedg);
                }

                if (!dr.HasRows)
                {
                    TourPlanShiftConfig oLedg = new TourPlanShiftConfig();

                    oLedg.strUSER_NAME = "";
                    oLedg.strEMP_CARD_NO = "";

                    oLedg.strMARKET_NAME = "";
                    oLedg.strROUTE = "";

                    oLedg.strSHIFT = "";
                    oLedg.strTOUR_TYPE = "";

                    oLedg.strNOTE = "";
                    oLedg.strEFFECTIVE_DATE = "";

                    oLedg.strPURPOSE = "";
                    oLedg.strACCOMPANY_WITH = "";

                    oLedg.strSTART_LOCATION = "";
                    oLedg.strEND_LOCATION = "";

                    oLedg.strACTION = "";




                    oooChequePrint.Add(oLedg);
                }

                dr.Close();
                gcnMain.Close();
                cmd.Dispose();
                return oooChequePrint;
            }
        }





        [HttpPost]
        public List<TourPlanShiftConfig> getFullMonth(TourPlanShiftConfig obj)
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

                List<TourPlanShiftConfig> oooChequePrint = new List<TourPlanShiftConfig>();

                // Corrected SQL query
                strSQL = "SELECT * FROM HRS_EMP_TOUR_PLAN WHERE EMP_CARD_NO='" + obj.strEMP_CARD_NO + "'  ";

                SqlCommand cmd = new SqlCommand(strSQL, gcnMain);
                dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    TourPlanShiftConfig oLedg = new TourPlanShiftConfig();

                    oLedg.strUSER_NAME = dr["USER_NAME"].ToString();
                    oLedg.strEMP_CARD_NO = dr["EMP_CARD_NO"].ToString();
                    oLedg.strMARKET_NAME = dr["MARKET_NAME"].ToString();
                    oLedg.strROUTE = dr["ROUTE"].ToString();
                    oLedg.strSHIFT = dr["SHIFT"].ToString();
                    oLedg.strTOUR_TYPE = dr["TOUR_TYPE"].ToString();
                    oLedg.strNOTE = dr["NOTE"].ToString();
                    oLedg.strEFFECTIVE_DATE = dr["EFFECTIVE_DATE"].ToString();
                    oLedg.strPURPOSE = dr["PURPOSE"].ToString();
                    oLedg.strACCOMPANY_WITH = dr["ACCOMPANY_WITH"].ToString();
                    oLedg.strSTART_LOCATION = dr["START_LOCATION"].ToString();
                    oLedg.strEND_LOCATION = dr["END_LOCATION"].ToString();
                    oLedg.strACTION = dr["ACTION"].ToString();


                    oooChequePrint.Add(oLedg);
                }

                if (!dr.HasRows)
                {
                    TourPlanShiftConfig oLedg = new TourPlanShiftConfig();

                    oLedg.strUSER_NAME = "";
                    oLedg.strEMP_CARD_NO = "";

                    oLedg.strMARKET_NAME = "";
                    oLedg.strROUTE = "";

                    oLedg.strSHIFT = "";
                    oLedg.strTOUR_TYPE = "";

                    oLedg.strNOTE = "";
                    oLedg.strEFFECTIVE_DATE = "";

                    oLedg.strPURPOSE = "";
                    oLedg.strACCOMPANY_WITH = "";

                    oLedg.strSTART_LOCATION = "";
                    oLedg.strEND_LOCATION = "";

                    oLedg.strACTION = "";




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