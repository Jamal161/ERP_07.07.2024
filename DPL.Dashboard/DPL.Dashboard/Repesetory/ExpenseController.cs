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
    public class ExpenseController : Controller
    {
        //
        // GET: /Expense/
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult mPostMonthlyAllowance()
        {

            var allLedger = "";
            return Json(allLedger, JsonRequestBehavior.AllowGet);
        }


        public ActionResult mPostExpenseClaim()
        {

            var allLedger = "";
            return Json(allLedger, JsonRequestBehavior.AllowGet);
        }



        public ActionResult mPostExpenseType()
        {

            var allLedger = "";
            return Json(allLedger, JsonRequestBehavior.AllowGet);
        }



        public ActionResult mPostTadaClaim()
        {

            var allLedger = "";
            return Json(allLedger, JsonRequestBehavior.AllowGet);
        }


        public ActionResult mPostTaDa()
        {

            var allLedger = "";
            return Json(allLedger, JsonRequestBehavior.AllowGet);
        }


        public ActionResult mPostMileageClaim()
        {

            var allLedger = "";
            return Json(allLedger, JsonRequestBehavior.AllowGet);
        }



        public ActionResult GetTadaClaimslist()
        {

            var allLedger = GetTadaClaims();
            return Json(allLedger, JsonRequestBehavior.AllowGet);
        }




        public ActionResult mUpdateTada()
        {

            var allLedger = "";
            return Json(allLedger, JsonRequestBehavior.AllowGet);
        }


        public ActionResult GetMonthlyAllowancesList()
        {

            var allLedger = GetMonthlyAllowances();
            return Json(allLedger, JsonRequestBehavior.AllowGet);
        }




        public ActionResult DeleteMonthDelete(string strEXPENSEID)
        {

            var allLedger = DeleteMonth(strEXPENSEID);
            return Json(allLedger, JsonRequestBehavior.AllowGet);
        }




        [HttpPost]
        public string mPostMonthlyAllowance(MonthlyAllowances obj)
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

                    string ExpenseId = DateTime.UtcNow.ToString("yyMMddHHmmss");

                    SqlCommand cmdInsert = new SqlCommand();
                    SqlTransaction myTrans;
                    myTrans = gcnMain.BeginTransaction();
                    cmdInsert.Connection = gcnMain;
                    cmdInsert.Transaction = myTrans;
                    strSQL = "INSERT INTO HRS_MONTHLYALLOWANCE (";
                    strSQL = strSQL + " EXPENSEID ,USER_NAME,ALLOWANCE,ACTIONS";
                    strSQL = strSQL + ") ";
                    strSQL = strSQL + "VALUES (";
                    strSQL = strSQL + "'" + ExpenseId + "',";
                    strSQL = strSQL + "'" + obj.strName + "',";
                    strSQL = strSQL + "'" + obj.strAllowance + "',";
                    strSQL = strSQL + "'" + obj.strActions + "'";

                    strSQL = strSQL + ")";
                    cmdInsert.CommandText = strSQL;
                    cmdInsert.ExecuteNonQuery();
                    cmdInsert.Transaction.Commit();
                    return "inserted successfully";
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
        public string mPostExpenseClaim(ExpenseClaim obj)
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

                    string ExpenseId = DateTime.UtcNow.ToString("yyMMddHHmmss");

                    SqlCommand cmdInsert = new SqlCommand();
                    SqlTransaction myTrans;
                    myTrans = gcnMain.BeginTransaction();
                    cmdInsert.Connection = gcnMain;
                    cmdInsert.Transaction = myTrans;
                    strSQL = "INSERT INTO HRS_ExpenseClaim_NEW (";
                    strSQL = strSQL + " ExpenseId,UserName,CardNO,UserDesignation,Expense,ExpenseType,Image,ClaimDate,Approvers,Actions";
                    strSQL = strSQL + ") ";
                    strSQL = strSQL + "VALUES (";
                    strSQL = strSQL + "'" + ExpenseId + "',";
                    strSQL = strSQL + "'" + obj.strUserName + "',";
                    strSQL = strSQL + "'" + obj.strCardNO + "',";
                    strSQL = strSQL + "'" + obj.strUserDesignation + "',";
                    strSQL = strSQL + "'" + obj.strExpense + "',";
                    strSQL = strSQL + "'" + obj.strExpenseType + "',";
                    strSQL = strSQL + "N'" + obj.strImage + "',";
                    strSQL = strSQL + "'" + obj.strClaimDate + "',";
                    strSQL = strSQL + "'" + obj.strApprovers + "',";
                    strSQL = strSQL + "'" + obj.strActions + "'";

                    strSQL = strSQL + ")";
                    cmdInsert.CommandText = strSQL;
                    cmdInsert.ExecuteNonQuery();
                    cmdInsert.Transaction.Commit();
                    return "inserted successfully";
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
        public string mPostExpenseType(ExpenseType obj)
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

                    string ExpenseId = DateTime.UtcNow.ToString("yyMMddHHmmss");

                    SqlCommand cmdInsert = new SqlCommand();
                    SqlTransaction myTrans;
                    myTrans = gcnMain.BeginTransaction();
                    cmdInsert.Connection = gcnMain;
                    cmdInsert.Transaction = myTrans;
                    strSQL = "INSERT INTO HRS_EXPENSETYPE (";
                    strSQL = strSQL + " EXPENSEID,USER_NAME,IMAGEREQUIRED,ACTIONS";
                    strSQL = strSQL + ") ";
                    strSQL = strSQL + "VALUES (";
                    strSQL = strSQL + "'" + ExpenseId + "',";
                    strSQL = strSQL + "'" + obj.strName + "',";
                    strSQL = strSQL + "N'" + obj.strImageRequired + "',";
                    strSQL = strSQL + "'" + obj.strActions + "'";

                    strSQL = strSQL + ")";
                    cmdInsert.CommandText = strSQL;
                    cmdInsert.ExecuteNonQuery();
                    cmdInsert.Transaction.Commit();
                    return "inserted successfully";
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
        public string mPostTadaClaim(TadaClaim obj)
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

                    string ExpenseId = DateTime.UtcNow.ToString("yyMMddHHmmss");

                    SqlCommand cmdInsert = new SqlCommand();
                    SqlTransaction myTrans;
                    myTrans = gcnMain.BeginTransaction();
                    cmdInsert.Connection = gcnMain;
                    cmdInsert.Transaction = myTrans;
                    strSQL = "INSERT INTO HRS_TADA_CLAIM (";
                    strSQL = strSQL + " EXPENSEID,MARKET,USER_NAME,EMP_CARD_NO,TA,DA,TOUR_TYPE,CLAIM_DATE,NOTE,APPROVERS,ACTIONS";
                    strSQL = strSQL + ") ";
                    strSQL = strSQL + "VALUES (";
                    strSQL = strSQL + "'" + ExpenseId + "',";
                    strSQL = strSQL + "'" + obj.strMarket + "',";
                    strSQL = strSQL + "'" + obj.strUserName + "',";
                    strSQL = strSQL + "'" + obj.strCardNO + "',";
                    strSQL = strSQL + "'" + obj.strTA + "',";
                    strSQL = strSQL + "'" + obj.strDA + "',";
                    strSQL = strSQL + "'" + obj.strTourType + "',";
                    strSQL = strSQL + "'" + obj.strClaimDate + "',";
                    strSQL = strSQL + "N'" + obj.strNote + "',";
                    strSQL = strSQL + "'" + obj.strApprovers + "',";
                    strSQL = strSQL + "'" + obj.strActions + "'";

                    strSQL = strSQL + ")";
                    cmdInsert.CommandText = strSQL;
                    cmdInsert.ExecuteNonQuery();
                    cmdInsert.Transaction.Commit();
                    return "inserted successfully";
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
        public string mPostTaDa(TADA obj)
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

                    string ExpenseId = DateTime.UtcNow.ToString("yyMMddHHmmss");

                    SqlCommand cmdInsert = new SqlCommand();
                    SqlTransaction myTrans;
                    myTrans = gcnMain.BeginTransaction();
                    cmdInsert.Connection = gcnMain;
                    cmdInsert.Transaction = myTrans;
                    strSQL = "INSERT INTO HRS_TADA_NEW (";
                    strSQL = strSQL + " ExpenseId,Market,TourType,UserRole,TA,DA,Actions";
                    strSQL = strSQL + ") ";
                    strSQL = strSQL + "VALUES (";
                    strSQL = strSQL + "'" + ExpenseId + "',";
                    strSQL = strSQL + "'" + obj.strMarket + "',";
                    strSQL = strSQL + "'" + obj.strTourType + "',";
                    strSQL = strSQL + "'" + obj.strUserRole + "',";
                    strSQL = strSQL + "'" + obj.strTA + "',";
                    strSQL = strSQL + "'" + obj.strDA + "',";
                    strSQL = strSQL + "'" + obj.strActions + "'";

                    strSQL = strSQL + ")";
                    cmdInsert.CommandText = strSQL;
                    cmdInsert.ExecuteNonQuery();
                    cmdInsert.Transaction.Commit();
                    return "inserted successfully";
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
        public string mPostMileageClaim(MileageClaim obj)
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

                    string ExpenseId = DateTime.UtcNow.ToString("yyMMddHHmmss");

                    SqlCommand cmdInsert = new SqlCommand();
                    SqlTransaction myTrans;
                    myTrans = gcnMain.BeginTransaction();
                    cmdInsert.Connection = gcnMain;
                    cmdInsert.Transaction = myTrans;
                    strSQL = "INSERT INTO HRS_MileageClaim_NEW (";
                    strSQL = strSQL + " ExpenseId,UserName,CardNO,Mileage,Expense,ClaimDate,MeterReading,MeterImage,Market,Actions";
                    strSQL = strSQL + ") ";
                    strSQL = strSQL + "VALUES (";
                    strSQL = strSQL + "'" + ExpenseId + "',";
                    strSQL = strSQL + "'" + obj.strUserName + "',";
                    strSQL = strSQL + "'" + obj.strCardNO + "',";
                    strSQL = strSQL + "'" + obj.strMileage + "',";
                    strSQL = strSQL + "'" + obj.strExpense + "',";
                    strSQL = strSQL + "'" + obj.strClaimDate + "',";
                    strSQL = strSQL + "'" + obj.strMeterReading + "',";
                    strSQL = strSQL + "N'" + obj.strMeterImage + "',";
                    strSQL = strSQL + "'" + obj.strMarket + "',";
                    strSQL = strSQL + "'" + obj.strActions + "'";

                    strSQL = strSQL + ")";
                    cmdInsert.CommandText = strSQL;
                    cmdInsert.ExecuteNonQuery();
                    cmdInsert.Transaction.Commit();
                    return "inserted successfully";
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


        public List<TadaClaims> GetTadaClaims()
        {
            List<TadaClaims> tadaClaims = new List<TadaClaims>();
            string connectionString = Utility.SQLConnstringComSwitch("0001");

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string strSQL = "SELECT EXPENSEID,MARKET,USER_NAME,EMP_CARD_NO,TA,DA,TOUR_TYPE,CLAIM_DATE,NOTE,APPROVERS,ACTIONS FROM HRS_TADA_CLAIM";

                    using (SqlCommand cmdSelect = new SqlCommand(strSQL, connection))
                    {
                        using (SqlDataReader reader = cmdSelect.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                TadaClaims tadaClaim = new TadaClaims();
                                tadaClaim.strEXPENSEID = reader.GetString(0);
                                tadaClaim.strMARKET = reader.GetString(1);
                                tadaClaim.strUSER_NAME = reader.GetString(2);
                                tadaClaim.strEMP_CARD_NO = reader.GetString(3);
                                tadaClaim.strTA = reader.GetString(4);
                                tadaClaim.strDA = reader.GetString(5);
                                tadaClaim.strTOUR_TYPE = reader.GetString(6);
                                tadaClaim.strCLAIM_DATE = reader.GetString(7);
                                tadaClaim.strNOTE = reader.GetString(8);
                                tadaClaim.strAPPROVERS = reader.GetString(9);
                                tadaClaim.strACTIONS = reader.GetString(10);

                                tadaClaims.Add(tadaClaim);
                            }
                        }
                    }
                }

                return tadaClaims;
            }
            catch (SqlException ex)
            {

                Console.WriteLine(ex.Message);
                return null;
            }
        }




        [HttpPost]
        public string mUpdateTada(TadaClaimupdate obj)
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
                    
                    strSQL = "UPDATE HRS_TADA_CLAIM SET ACTIONS='" + obj.strACTIONS + "' ";
                    strSQL = strSQL + "WHERE EXPENSEID='" + obj.strEXPENSEID + "'";
                    cmdInsert.CommandText = strSQL;
                    cmdInsert.ExecuteNonQuery();



                    cmdInsert.Transaction.Commit();
                    return "Updated ";
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





        public ActionResult GetMonthlyAllowances()
        {
            List<MonthlyAllowance> monthlyAllowances = new List<MonthlyAllowance>();

            string connectionString = Utility.SQLConnstringComSwitch("0001");

            using (SqlConnection gcnMain = new SqlConnection(connectionString))
            {
                try
                {
                    gcnMain.Open();

                    SqlCommand cmdSelect = new SqlCommand("SELECT * FROM HRS_MONTHLYALLOWANCE", gcnMain);
                    SqlDataReader reader = cmdSelect.ExecuteReader();

                    while (reader.Read())
                    {
                        MonthlyAllowance allowance = new MonthlyAllowance
                        {
                            strEXPENSEID = reader["EXPENSEID"].ToString(),
                            strUSER_NAME = reader["USER_NAME"].ToString(),
                            strALLOWANCE = reader["ALLOWANCE"].ToString(),
                            strACTIONS = reader["ACTIONS"].ToString()
                        };

                        monthlyAllowances.Add(allowance);
                    }


                    return Json(monthlyAllowances, JsonRequestBehavior.AllowGet);
                }
                catch (SqlException ex)
                {

                    Console.WriteLine(ex.Message);


                    return View("Error");
                }
            }
        }







        public string DeleteMonth(string strEXPENSEID)
        {
            string strResponse = "";
            string connectionString = Utility.SQLConnstringComSwitch("0001");

            try
            {
                using (SqlConnection gcnMain = new SqlConnection(connectionString))
                {
                    gcnMain.Open();

                    SqlCommand cmdDelete = new SqlCommand();
                    SqlTransaction myTrans;
                    myTrans = gcnMain.BeginTransaction();
                    cmdDelete.Connection = gcnMain;
                    cmdDelete.Transaction = myTrans;


                    string strSQL = "DELETE FROM HRS_MONTHLYALLOWANCE WHERE EXPENSEID = @EXPENSEID";
                    cmdDelete.CommandText = strSQL;
                    cmdDelete.Parameters.AddWithValue("@EXPENSEID", strEXPENSEID);
                    cmdDelete.ExecuteNonQuery();

                    strResponse = "Deleted...";

                    myTrans.Commit();
                }

                return strResponse;
            }
            catch (Exception ex)
            {

                Console.WriteLine("An error occurred while deleting record: " + ex.Message);
                strResponse = "Delete failed...";
                return strResponse;
            }
        }





        public object strEXPENSEID { get; set; }
    }
}