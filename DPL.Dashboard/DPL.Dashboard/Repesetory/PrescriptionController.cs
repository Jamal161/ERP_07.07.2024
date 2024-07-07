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
    public class PrescriptionController : Controller
    {
        //
        // GET: /Prescription/
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult mGetTeritorryList(PrescriptionConfig obj)
        {

            var allLedger = mGetTeritorry(obj);
            return Json(allLedger, JsonRequestBehavior.AllowGet);
        }

        public ActionResult mPostPrescription()
        {

            var allLedger = "";
            return Json(allLedger, JsonRequestBehavior.AllowGet);
        }



        

        public ActionResult mGetPrescriptionList(PrescriptionConfig obj)
        {

            var allLedger = mGetPrescription(obj);
            //return Json(allLedger, JsonRequestBehavior.AllowGet);
            var jsonResult = Json(allLedger, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;

        }



        public ActionResult mUpdateConfig()
        {

            var allLedger = "";
            return Json(allLedger, JsonRequestBehavior.AllowGet);
        }


        public List<PrescriptionConfig> mGetTeritorry(PrescriptionConfig obj)
        {
            string strSQL = null;
            string connectionString = Utility.SQLConnstringComSwitch("0001");
            List<PrescriptionConfig> PrescriptionList = new List<PrescriptionConfig>();

            using (SqlConnection gcnMain = new SqlConnection(connectionString))
            {
                gcnMain.Open();

                strSQL = "select * from SMART0005.dbo.ACC_TERITORRY ";

                using (SqlCommand cmd = new SqlCommand(strSQL, gcnMain))
                {
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            PrescriptionConfig Prescription = new PrescriptionConfig();

                            Prescription.strTERITORRY_CODE = dr["TERITORRY_CODE"].ToString();
                            Prescription.strTERITORRY_NAME = dr["TERITORRY_NAME"].ToString();

                            PrescriptionList.Add(Prescription);
                        }
                    }
                }
            }

            if (PrescriptionList.Count == 0)
            {

                PrescriptionList.Add(new PrescriptionConfig());
            }

            return PrescriptionList;
        }



         [HttpPost]

public string mPostPrescription(PrescriptionConfig obj)
{
    string strSQL = null;
    string connectionString = Utility.SQLConnstringComSwitch("0001");

    using (SqlConnection gcnMain = new SqlConnection(connectionString))
    {
        try
        {
            gcnMain.Open();

            SqlCommand cmdInsert = new SqlCommand();
            SqlTransaction myTrans;
            myTrans = gcnMain.BeginTransaction();
            cmdInsert.Connection = gcnMain;
            cmdInsert.Transaction = myTrans;




            string IMG_ID = DateTime.UtcNow.ToString("yyMMddHHmmss");


            strSQL = "INSERT INTO HRS_PRESCRIPTION (";
            strSQL += "IMG_ID, USER_NAME, EMP_CARD_NO, ";
            strSQL += "DOCTOR, INSTITUTION, ";
            strSQL += "PRESCRIPTION_IMG, MARKET_NAME, TERITORRY_CODE, PRODUCTS, LATITUDE, LONGITUDE, NOTE, ACTION";
            strSQL += ") ";
            strSQL += "VALUES (";
            strSQL += "@IMG_ID, @USER_NAME, @EMP_CARD_NO, ";
            strSQL += "@DOCTOR, @INSTITUTION, ";
            strSQL += "@PRESCRIPTION_IMG, @MARKET_NAME, @TERITORRY_CODE, @PRODUCTS, @LATITUDE, @LONGITUDE, @NOTE, @ACTION";
            strSQL += ")";

            cmdInsert.CommandText = strSQL;

            
            cmdInsert.Parameters.AddWithValue("@IMG_ID", IMG_ID);
            cmdInsert.Parameters.AddWithValue("@USER_NAME", obj.strUSER_NAME);
            cmdInsert.Parameters.AddWithValue("@EMP_CARD_NO", obj.strEMP_CARD_NO);
            cmdInsert.Parameters.AddWithValue("@DOCTOR", obj.strDOCTOR);
            cmdInsert.Parameters.AddWithValue("@INSTITUTION", obj.strINSTITUTION);
            cmdInsert.Parameters.AddWithValue("@PRESCRIPTION_IMG", obj.strPRESCRIPTION_IMG);
            cmdInsert.Parameters.AddWithValue("@MARKET_NAME", obj.strMARKET_NAME);
            cmdInsert.Parameters.AddWithValue("@TERITORRY_CODE", obj.strTERITORRY_CODE);
            cmdInsert.Parameters.AddWithValue("@PRODUCTS", obj.strPRODUCTS);
            cmdInsert.Parameters.AddWithValue("@LATITUDE", obj.strLATITUDE);
            cmdInsert.Parameters.AddWithValue("@LONGITUDE", obj.strLONGITUDE);
            cmdInsert.Parameters.AddWithValue("@NOTE", obj.strNOTE);
            cmdInsert.Parameters.AddWithValue("@ACTION", obj.strACTION);

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





         public List<PrescriptionConfig> mGetPrescription(PrescriptionConfig obj)
         {
             string strSQL = null;
             string connectionString = Utility.SQLConnstringComSwitch("0001");
             List<PrescriptionConfig> PrescriptionConfigList = new List<PrescriptionConfig>();

             using (SqlConnection gcnMain = new SqlConnection(connectionString))
             {
                 gcnMain.Open();

                 strSQL = "select * from HRS_PRESCRIPTION ";

                 using (SqlCommand cmd = new SqlCommand(strSQL, gcnMain))
                 {
                     using (SqlDataReader dr = cmd.ExecuteReader())
                     {
                         while (dr.Read())
                         {
                             PrescriptionConfig Prescription = new PrescriptionConfig();

                             Prescription.strIMG_ID = dr["IMG_ID"].ToString();
                             Prescription.strUSER_NAME = dr["USER_NAME"].ToString();
                             Prescription.strEMP_CARD_NO = dr["EMP_CARD_NO"].ToString();
                             Prescription.strDOCTOR = dr["DOCTOR"].ToString();
                             Prescription.strINSTITUTION = dr["INSTITUTION"].ToString();
                             Prescription.strPRESCRIPTION_IMG = dr["PRESCRIPTION_IMG"].ToString();
                             Prescription.strMARKET_NAME = dr["MARKET_NAME"].ToString();
                             Prescription.strTERITORRY_CODE = dr["TERITORRY_CODE"].ToString();
                             Prescription.strPRODUCTS = dr["PRODUCTS"].ToString();
                             Prescription.strLATITUDE = dr["LATITUDE"].ToString();
                             Prescription.strLONGITUDE = dr["LONGITUDE"].ToString();
                             Prescription.strNOTE = dr["NOTE"].ToString();
                             Prescription.strSTATUS = dr["STATUS"].ToString();
                             //Prescription.strCREATED_DATE = dr["CREATED_DATE"].ToString();
                             Prescription.strINSERT_DATE = ((DateTime)dr["INSERT_DATE"]).ToString("dd-MMM-yyyy hh:mm tt");
                             PrescriptionConfigList.Add(Prescription);
                         }
                     }
                 }
             }

             if (PrescriptionConfigList.Count == 0)
             {

                 PrescriptionConfigList.Add(new PrescriptionConfig());
             }

             return PrescriptionConfigList;
         }




         [HttpPost]
         public string mUpdateConfig(PrescriptionConfig obj)
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

                     strSQL = "UPDATE HRS_PRESCRIPTION  SET STATUS='" + obj.strACTION + "' ";
                     strSQL = strSQL + "WHERE IMG_ID='" + obj.strIMG_ID + "'";
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