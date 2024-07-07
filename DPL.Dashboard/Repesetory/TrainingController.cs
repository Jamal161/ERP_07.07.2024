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
    public class TrainingController : Controller
    {
        //
        // GET: /Training/
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult mPostTrain()
        {
            var allLedger = "";
            return Json(allLedger, JsonRequestBehavior.AllowGet);
        }


        public ActionResult mGetTrainingList(Training obj)
        {

            var allLedger = mGetTrain(obj);
            return Json(allLedger, JsonRequestBehavior.AllowGet);
        }

        public ActionResult mGetTrainingLedgerList(NoticeCofig obj)
        {

            var allLedger = mGetTrainLedger(obj);
            return Json(allLedger, JsonRequestBehavior.AllowGet);
        }




        [HttpPost]
        public string mPostTrain(List<Training> Train)
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




                    string NOTICE_ID = DateTime.UtcNow.ToString("yyMMddHHmmss");

                    for (int i = 0; i < Train.Count; i++)
                    {
                        int intid = i;

                        if (intid == 0)
                        {


                            //string STRiMAGE = Notices[i].strNOTICE_IMG.Substring(Notices[i].strNOTICE_IMG.LastIndexOf('\\') + 1);





                            strSQL = "INSERT INTO HRS_TRAINING_NEW(";
                            strSQL = strSQL + "NOTICE_ID, TITLE, ANNOUNCEMENT, START_DATE, END_DATE, EDITOR, CREATED_BY";

                            strSQL = strSQL + ")";
                            strSQL = strSQL + " VALUES(";
                            strSQL = strSQL + "'" + NOTICE_ID + "',";
                            strSQL = strSQL + "N'" + Train[i].strTITLE + "',";
                            strSQL = strSQL + "N'" + Train[i].strANNOUNCEMENT + "',";
                            strSQL = strSQL + "'" + Train[i].strSTART_DATE + "',";
                            strSQL = strSQL + "'" + Train[i].strEND_DATE + "',";
                            strSQL = strSQL + "'" + Train[i].strEDITOR + "',";
                            strSQL = strSQL + "'" + Train[i].strCREATED_BY + "'";

                            strSQL = strSQL + ")";

                            cmdInsert.CommandText = strSQL;
                            cmdInsert.ExecuteNonQuery();


                        }
                        else if (intid == 1)
                        {
                            strSQL = "INSERT INTO HRS_MARKET_ACCESS_TRAINING_NEW (";
                            strSQL = strSQL + "NOTICE_ID,NATIONAL_HEAD, TEAM,ZONE,DIVISION,AREA,MARKET,ROUTE";

                            strSQL = strSQL + ")";
                            strSQL = strSQL + " VALUES(";
                            strSQL = strSQL + "'" + NOTICE_ID + "',";
                            strSQL = strSQL + "'" + Train[i].strNATIONAL_HEAD + "',";
                            strSQL = strSQL + "'" + Train[i].strTEAM + "',";
                            strSQL = strSQL + "'" + Train[i].strZONE + "',";
                            strSQL = strSQL + "'" + Train[i].strDIVISION + "',";
                            strSQL = strSQL + "'" + Train[i].strAREA + "',";
                            strSQL = strSQL + "'" + Train[i].strMARKET + "',";
                            strSQL = strSQL + "'" + Train[i].strROUTE + "'";



                            strSQL = strSQL + ")";

                            cmdInsert.CommandText = strSQL;
                            cmdInsert.ExecuteNonQuery();

                        }
                        else if (intid == 2)
                        {
                            strSQL = "INSERT INTO HRS_USER_TRAINING_NEW (";
                            strSQL = strSQL + "NOTICE_ID,ROLE";

                            strSQL = strSQL + ")";
                            strSQL = strSQL + " VALUES(";
                            strSQL = strSQL + "'" + NOTICE_ID + "',";
                            strSQL = strSQL + "'" + Train[i].strROLE + "'";



                            strSQL = strSQL + ")";

                            cmdInsert.CommandText = strSQL;
                            cmdInsert.ExecuteNonQuery();


                        }

                    }

                    cmdInsert.Transaction.Commit();
                    strresponse = "Notices inserted and notifications sent successfully";
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



        public List<Training> mGetTrain(Training obj)
        {
            string strSQL = null;
            string connectionString = Utility.SQLConnstringComSwitch("0001");
            List<Training> TrainingList = new List<Training>();

            using (SqlConnection gcnMain = new SqlConnection(connectionString))
            {
                gcnMain.Open();

                strSQL = "select * from HRS_TRAINING_NEW  N ,HRS_USER_TRAINING_NEW U,HRS_MARKET_ACCESS_TRAINING_NEW M where N.NOTICE_ID= U.NOTICE_ID AND N.NOTICE_ID= M.NOTICE_ID ";

                using (SqlCommand cmd = new SqlCommand(strSQL, gcnMain))
                {
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Training Train = new Training();
                            Train.strNOTICE_ID = dr["NOTICE_ID"].ToString();
                            Train.strTITLE = dr["TITLE"].ToString();
                            Train.strANNOUNCEMENT = dr["ANNOUNCEMENT"].ToString();
                            Train.strSTART_DATE = dr["START_DATE"].ToString();
                            Train.strEND_DATE = dr["END_DATE"].ToString();
                            Train.strEDITOR = dr["EDITOR"].ToString();
                            Train.strCREATED_BY = dr["CREATED_BY"].ToString();
                            Train.strNATIONAL_HEAD = dr["NATIONAL_HEAD"].ToString();
                            Train.strTEAM = dr["TEAM"].ToString();
                            Train.strZONE = dr["ZONE"].ToString();
                            Train.strDIVISION = dr["DIVISION"].ToString();
                            Train.strAREA = dr["AREA"].ToString();
                            Train.strMARKET = dr["MARKET"].ToString();
                            Train.strROUTE = dr["ROUTE"].ToString();
                            Train.strROLE = dr["ROLE"].ToString();
                            TrainingList.Add(Train);
                        }
                    }
                }
            }

            if (TrainingList.Count == 0)
            {

                TrainingList.Add(new Training());
            }

            return TrainingList;
        }




        public List<NoticeCofig> mGetTrainLedger(NoticeCofig obj)
        {
            string strSQL = null;
            string connectionString = Utility.SQLConnstringComSwitch("0001");
            List<NoticeCofig> NoticeCofigList = new List<NoticeCofig>();

            using (SqlConnection gcnMain = new SqlConnection(connectionString))
            {
                gcnMain.Open();

                strSQL = @" SELECT * FROM SMART0005.dbo.ACC_LEDGER_Z_D_A  JOIN SMART0005.dbo.TEAM_CONFIG ON SMART0005.dbo.TEAM_CONFIG.ZONE_NAME = SMART0005.dbo.ACC_LEDGER_Z_D_A.ZONE";


                using (SqlCommand cmd = new SqlCommand(strSQL, gcnMain))
                {
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            NoticeCofig train = new NoticeCofig();
                            train.intBRANCH_ID = Convert.ToInt32(dr["BRANCH_ID"]);
                            train.strZONE = dr["ZONE"].ToString();
                            train.strDIVISION = dr["DIVISION"].ToString();
                            train.strAREA = dr["AREA"].ToString();
                            train.strLEDGER_NAME = dr["LEDGER_NAME"].ToString();
                            train.strTERITORRY_CODE = dr["TERITORRY_CODE"].ToString();
                            train.strTERRITORRY_NAME = dr["TERRITORRY_NAME"].ToString();
                            train.strLEDGER_NAME_MERZE = dr["LEDGER_NAME_MERZE"].ToString();
                            train.intLEDGER_STATUS = Convert.ToInt32(dr["LEDGER_STATUS"]);
                            train.strGR_MOBILE_NO = dr["GR_MOBILE_NO"].ToString();
                            train.intHALT_MPO = Convert.ToInt32(dr["HALT_MPO"]);
                            train.strHL_LEDGER_NAME = dr["HL_LEDGER_NAME"].ToString();
                            train.strPF_LEDGER_NAME = dr["PF_LEDGER_NAME"].ToString();
                            train.strINSERT_DATE = dr["INSERT_DATE"].ToString();
                            train.strROUTE_NAME = dr["ROUTE_NAME"].ToString();
                            train.strLEDGER_CLASS = dr["LEDGER_CLASS"].ToString();
                            train.strLEDGER_ADD_DATE = dr["LEDGER_ADD_DATE"].ToString();
                            train.strLEDGER_RESIGN_DATE = dr["LEDGER_RESIGN_DATE"].ToString();
                            train.strMPO_DIV = dr["MPO_DIV"].ToString();
                            train.strGODOWNS_NAME = dr["GODOWNS_NAME"].ToString();
                            train.strMPO_CARD_NO = dr["MPO_CARD_NO"].ToString();
                            train.intCARTON_AMNT = Convert.ToInt32(dr["CARTON_AMNT"]);
                            train.strZONE_NAME = dr["ZONE_NAME"].ToString();
                            train.strTEAM_NAME = dr["TEAM_NAME"].ToString();
                            //tour.intTEAM_CODE = Convert.ToInt32(dr["TEAM_CODE"]);
                            NoticeCofigList.Add(train);
                        }
                    }
                }
            }

            if (NoticeCofigList.Count == 0)
            {

                NoticeCofigList.Add(new NoticeCofig());
            }

            return NoticeCofigList;
        }

	}
}