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
    public class DoctorVisitTypeController : Controller
    {
        //
        // GET: /DoctorVisitType/
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult mPostDoctorVisitType()
        {

            var allLedger = "";
            return Json(allLedger, JsonRequestBehavior.AllowGet);
        }

        public ActionResult mPostDoctorVisit()
        {

            var allLedger = "";
            return Json(allLedger, JsonRequestBehavior.AllowGet);
        }


        public ActionResult mGetDoctorLedgerList(Doctorlist obj)
        {

            var allLedger = mGetDoctorLedger(obj);
            var jsonResult = Json(allLedger, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;

        }


             public ActionResult mUpdateDoctorVisit()
        {

            var allLedger = "";
            return Json(allLedger, JsonRequestBehavior.AllowGet);
        }



        public ActionResult mGetDoctorVisitList(DoctorVisit obj)
        {

            var allLedger = mGetDoctorVisit(obj);
            var jsonResult = Json(allLedger, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;

        }





        public ActionResult mGetDoctorLedgerTypeList(DoctorVisitType obj)
        {

            var allLedger = mGetDoctorLedgerType(obj);
            var jsonResult = Json(allLedger, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;

        }


        public ActionResult mUpdateDoctotVisitType()
        {

            var allLedger = "";
            return Json(allLedger, JsonRequestBehavior.AllowGet);
        }




              public ActionResult mGetDoctorVisitGiftList(DoctorVisitGift obj)
        {

            var allLedger = mGetDoctorVisitGift(obj);
            var jsonResult = Json(allLedger, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;

        }


        [HttpPost]
        public string mPostDoctorVisitType(DoctorVisitType obj)
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
                    strSQL = "INSERT INTO HRS_DOCTOR_VISIT_TYPE (";
                    strSQL = strSQL + "VISIT_NAME,STATUS";
                    strSQL = strSQL + ") ";
                    strSQL = strSQL + "VALUES (";
                    strSQL = strSQL + "'" + obj.strName + "',";
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
        public string mPostDoctorVisit(List<DoctorVisit> objs)
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


                    foreach (var obj in objs)
                    {

                        strSQL = "INSERT INTO HRS_DOCTOR_VISIT (";
                        strSQL = strSQL + " USER_NAME, EMP_CARD_NO, VISITED_WITH, DOCTOR_NAME, INSTITUTE, CHAMBER, MARKET, VISITED_AT, SHIFT, LOCATION, DISTATNCE, STATUS, NOTE ";
                        strSQL = strSQL + ")";
                        strSQL = strSQL + " VALUES(";
                       
                        strSQL = strSQL + "'" + obj.strUser_Name + "',";
                        strSQL = strSQL + "'" + obj.strCardNO + "',";
                        strSQL = strSQL + "'" + obj.strVisited_With + "',";
                        strSQL = strSQL + "'" + obj.strDoctor + "',";
                        strSQL = strSQL + "'" + obj.strInstitute + "',";
                        strSQL = strSQL + "'" + obj.strChamber + "',";
                        strSQL = strSQL + "'" + obj.strMarket + "',";
                        strSQL = strSQL + "'" + obj.strVisited_At + "',";
                        strSQL = strSQL + "'" + obj.strShift + "',";
                        strSQL = strSQL + "'" + obj.strLocation + "',";
                        strSQL = strSQL + "'" + obj.strDistance + "',";
                        strSQL = strSQL + "'" + obj.strActions + "',";
                        strSQL = strSQL + "N'" + obj.strNOTE + "'";
                   

                        strSQL = strSQL + ")";

                        cmdInsert.CommandText = strSQL;
                        cmdInsert.ExecuteNonQuery();
                    }
                    cmdInsert.Transaction.Commit();
                    strresponse = "Inserted successfully";
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


        

        public List<Doctorlist> mGetDoctorLedger(Doctorlist obj)
        {
            string strSQL = null;
            string connectionString = Utility.SQLConnstringComSwitch("0001");
            List<Doctorlist> DoctorlistList = new List<Doctorlist>();

            using (SqlConnection gcnMain = new SqlConnection(connectionString))
            {
                gcnMain.Open();

                strSQL = "select * from SMART0005.dbo.ACC_DOCTOR_LIST_VIEW ";

                using (SqlCommand cmd = new SqlCommand(strSQL, gcnMain))
                {
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Doctorlist Doctor = new Doctorlist();
                            Doctor.strLEDGER_REP_NAME = dr["LEDGER_REP_NAME"].ToString();
                            Doctor.strCODE = dr["CODE"].ToString();
                            Doctor.strTERRITORRY_NAME = dr["TERRITORRY_NAME"].ToString();
                            Doctor.strLEDGER_NAME_MERZE = dr["LEDGER_NAME_MERZE"].ToString();
                            Doctor.strLEDGER_NAME = dr["LEDGER_NAME"].ToString();
                            Doctor.strLEDGER_PARENT_GROUP = dr["LEDGER_PARENT_GROUP"].ToString();

                            DoctorlistList.Add(Doctor);
                        }
                    }
                }
            }

            if (DoctorlistList.Count == 0)
            {

                DoctorlistList.Add(new Doctorlist());
            }

            return DoctorlistList;
        }







        public List<DoctorVisitType> mGetDoctorLedgerType(DoctorVisitType obj)
        {
            string strSQL = null;
            string connectionString = Utility.SQLConnstringComSwitch("0001");
            List<DoctorVisitType> DoctorlistList = new List<DoctorVisitType>();

            using (SqlConnection gcnMain = new SqlConnection(connectionString))
            {
                gcnMain.Open();

                string VistID = DateTime.UtcNow.ToString("yyMMddHHmmss");

                strSQL = "select * from HRS_DOCTOR_VISIT_TYPE ";

                using (SqlCommand cmd = new SqlCommand(strSQL, gcnMain))
                {
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            DoctorVisitType Doctor = new DoctorVisitType();
                            Doctor.strSERIAL_NO = dr["SERIAL_NO"].ToString();
                            Doctor.strVISIT_NAME = dr["VISIT_NAME"].ToString();
                            Doctor.strSTATUS = dr["STATUS"].ToString();
                           

                            DoctorlistList.Add(Doctor);
                        }
                    }
                }
            }

            if (DoctorlistList.Count == 0)
            {

                DoctorlistList.Add(new DoctorVisitType());
            }

            return DoctorlistList;
        }



        [HttpPost]

        //public string mUpdateDoctotVisitType(DoctorVisitType obj)
        //{
        //    string strSQL = null;
        //    string connectionString = Utility.SQLConnstringComSwitch("0001");

        //    using (SqlConnection gcnMain = new SqlConnection(connectionString))
        //    {
        //        if (gcnMain.State == ConnectionState.Open)
        //        {
        //            gcnMain.Close();
        //        }
        //        try
        //        {
        //            gcnMain.Open();

        //            SqlCommand cmdInsert = new SqlCommand();
        //            SqlTransaction myTrans;
        //            myTrans = gcnMain.BeginTransaction();
        //            cmdInsert.Connection = gcnMain;
        //            cmdInsert.Transaction = myTrans;
        //            string VistID = DateTime.UtcNow.ToString("yyMMddHHmmss");
        //            strSQL = "UPDATE HRS_DOCTOR_VISIT_TYPE_NEW SET ";

                  
        //            strSQL += "Actions = '" + obj.strActions + "' ,";
        //            strSQL += "VistID = '" + obj.strVistID + "' ";
                   


        //            cmdInsert.CommandText = strSQL;
        //            cmdInsert.ExecuteNonQuery();

        //            cmdInsert.Transaction.Commit();
        //            return "Updated";
        //        }
        //        catch (SqlException ex)
        //        {
        //            return ex.ToString();
        //        }
        //        finally
        //        {
        //            gcnMain.Dispose();
        //        }
        //    }
        //}


        public string mUpdateDoctotVisitType(DoctorVisitType obj)
        {
            string connectionString = Utility.SQLConnstringComSwitch("0001");

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string selectQuery = "SELECT COUNT(*) FROM HRS_DOCTOR_VISIT_TYPE WHERE SERIAL_NO = @SERIAL_NO";
                    using (SqlCommand cmdSelect = new SqlCommand(selectQuery, connection))
                    {
                        cmdSelect.Parameters.AddWithValue("@SERIAL_NO", obj.strSERIAL_NO);
                        int count = (int)cmdSelect.ExecuteScalar();

                        if (count > 0)
                        {
                            string updateQuery = "UPDATE HRS_DOCTOR_VISIT_TYPE SET STATUS = @STATUS, VISIT_NAME = @VISIT_NAME WHERE SERIAL_NO = @SERIAL_NO";
                            using (SqlCommand cmdUpdate = new SqlCommand(updateQuery, connection))
                            {
                                cmdUpdate.Parameters.AddWithValue("@STATUS", obj.strSTATUS);
                                cmdUpdate.Parameters.AddWithValue("@VISIT_NAME", obj.strVISIT_NAME);
                                cmdUpdate.Parameters.AddWithValue("@SERIAL_NO", obj.strSERIAL_NO);

                                int rowsAffected = cmdUpdate.ExecuteNonQuery();
                                if (rowsAffected > 0)
                                    return "Updated";
                                else
                                    return "Update failed";
                            }
                        }
                        else
                        {
                            return "No matching serial number found.";
                        }
                    }
                }
                catch (SqlException ex)
                {
                    return "Error: " + ex.Message;
                }
            }
        }


       
        public List<DoctorVisit> mGetDoctorVisit(DoctorVisit obj)
        {
           
            string strSQL = null;
            string connectionString = Utility.SQLConnstringComSwitch("0001");
            List<DoctorVisit> DoctorVisitList = new List<DoctorVisit>();

           
            using (SqlConnection gcnMain = new SqlConnection(connectionString))
            {
                gcnMain.Open();

                
                strSQL = "select * from HRS_DOCTOR_VISIT";

                
                using (SqlCommand cmd = new SqlCommand(strSQL, gcnMain))
                {
                    
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                       
                        while (dr.Read())
                        {
                           
                            DoctorVisit Doctor = new DoctorVisit();

                
                            Doctor.strSerialNO = dr["SerialNO"].ToString();
                            Doctor.strVISIT_NAME = dr["VISIT_NAME"].ToString();
                            Doctor.strUSER_NAME = dr["USER_NAME"].ToString();
                            Doctor.strEMP_CARD_NO = dr["EMP_CARD_NO"].ToString();
                            Doctor.strVISITED_WITH = dr["VISITED_WITH"].ToString();
                            Doctor.strDOCTOR_NAME = dr["DOCTOR_NAME"].ToString();
                            Doctor.strINSTITUTE = dr["INSTITUTE"].ToString();
                            Doctor.strCHAMBER = dr["CHAMBER"].ToString();
                            Doctor.strMARKET = dr["MARKET"].ToString();
                            Doctor.strVISITED_AT = dr["VISITED_AT"].ToString();
                            Doctor.strSHIFT = dr["SHIFT"].ToString();
                            Doctor.strLOCATION = dr["LOCATION"].ToString();
                            Doctor.strDISTATNCE = dr["DISTATNCE"].ToString();
                            Doctor.strSTATUS = dr["STATUS"].ToString();
                            Doctor.strNOTE = dr["NOTE"].ToString();

                           
                            DoctorVisitList.Add(Doctor);
                        }
                    }
                }
            }

          
            if (DoctorVisitList.Count == 0)
            {
                DoctorVisitList.Add(new DoctorVisit());
            }

           
            return DoctorVisitList;
        }





        [HttpPost]
        public string mUpdateDoctorVisit(DoctorVisittwo obj)
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

                    strSQL = "UPDATE HRS_DOCTOR_VISIT SET STATUS='" + obj.strSTATUS + "' ";
                    strSQL = strSQL + "WHERE SerialNO ='" + obj.strSerialNO + "'";
                    cmdInsert.CommandText = strSQL;
                    cmdInsert.ExecuteNonQuery();



                    cmdInsert.Transaction.Commit();
                    return "Updated";
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



        public List<DoctorVisitGift> mGetDoctorVisitGift(DoctorVisitGift obj)
        {

            string strSQL = null;
            string connectionString = Utility.SQLConnstringComSwitch("0001");
            List<DoctorVisitGift> DoctorVisitList = new List<DoctorVisitGift>();


            using (SqlConnection gcnMain = new SqlConnection(connectionString))
            {
                gcnMain.Open();


                strSQL = "SELECT SMART0005.dbo.ACC_LEDGER_Z_D_A.MPO_CARD_NO , SMART0005.dbo.ACC_LEDGER_Z_D_A.LEDGER_NAME_MERZE,SMART0005.dbo.INV_TRAN.STOCKITEM_NAME,ABS(ISNULL(SUM(SMART0005.dbo.INV_TRAN.INV_TRAN_QUANTITY),0))INV_TRAN_QUANTITY    FROM SMART0005.dbo.ACC_COMPANY_VOUCHER ,SMART0005.dbo.INV_TRAN,SMART0005.dbo.ACC_LEDGER_Z_D_A  WHERE SMART0005.dbo.ACC_LEDGER_Z_D_A.LEDGER_NAME =SMART0005.dbo.ACC_COMPANY_VOUCHER.LEDGER_NAME AND SMART0005.dbo.ACC_COMPANY_VOUCHER.COMP_REF_NO =SMART0005.dbo.INV_TRAN.INV_REF_NO AND SMART0005.dbo.INV_TRAN.INV_VOUCHER_TYPE  =15 AND COMP_VOUCHER_NARRATION LIKE '%cLASS%' AND SMART0005.dbo.ACC_COMPANY_VOUCHER.COMP_VOUCHER_DATE BETWEEN CONVERT (DATETIME,'01-01-2024',103) AND  CONVERT (DATETIME,'31-01-2024',103) GROUP BY SMART0005.dbo.ACC_LEDGER_Z_D_A.MPO_CARD_NO,SMART0005.dbo.ACC_LEDGER_Z_D_A.LEDGER_NAME_MERZE,SMART0005.dbo.INV_TRAN.STOCKITEM_NAME";

                using (SqlCommand cmd = new SqlCommand(strSQL, gcnMain))
                {

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {

                        while (dr.Read())
                        {

                            DoctorVisitGift Doctor = new DoctorVisitGift();


                            Doctor.strMPO_CARD_NO = dr["MPO_CARD_NO"].ToString();
                            Doctor.strLEDGER_NAME_MERZE = dr["LEDGER_NAME_MERZE"].ToString();
                            Doctor.strSTOCKITEM_NAME = dr["STOCKITEM_NAME"].ToString();
                            Doctor.strINV_TRAN_QUANTITY = dr["INV_TRAN_QUANTITY"].ToString();

                            DoctorVisitList.Add(Doctor);
                        }
                    }
                }
            }


            if (DoctorVisitList.Count == 0)
            {
                DoctorVisitList.Add(new DoctorVisitGift());
            }


            return DoctorVisitList;
        }



	}
}