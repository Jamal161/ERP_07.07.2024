using DPL.DASHBOARD.Models;
using Dutility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
//using FirebaseAdmin;
//using FirebaseAdmin.Messaging;
//using Google.Apis.Auth.OAuth2;
using System.Web.Services.Description;
using System.Net;
using System.IO;


namespace DPL.DASHBOARD.Repesetory
{
    public class NoticeController : Controller
    {
        //
        // GET: /Notice/
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult mPostNotice(List<Notice> obj)
        {

            string strtite = obj[0].strTITLE;
            string strANNOUNCEMENT = obj[0].strANNOUNCEMENT;

            var ddd = mPostNoticerr(obj);
            var allLedger = SendNotificationJSON("/topics/alldevice", obj[0].strTITLE, strANNOUNCEMENT, "beep");
            

            return Json(allLedger, JsonRequestBehavior.AllowGet);
        }


        public ActionResult mGetNoticeList(Notice obj)
        {

            var allLedger = mGetNotice(obj);
            return Json(allLedger, JsonRequestBehavior.AllowGet);
        }

        public ActionResult mGetNoticesLedgerList(NoticeCofig obj)
        {

            var allLedger = mGetNoticesLedger(obj);
            return Json(allLedger, JsonRequestBehavior.AllowGet);
        }



        public string SendNotificationJSON(string deviceId, string title, string body, string click_action)
        {
            string SERVER_KEY_TOKEN = "AAAAul1cCIk:APA91bF-WqneidZqM3DDI84q9fBNRMjlOG5k7YUL8pdwBxaqblZNVWC438ZR8Fwkj2y5ghbi4-7v9uK60UjlP_9SBvzEF5AUCEy8cISw_fmSiSiig7wDWK91Rp7Ssz4ASpygj_H-kR4C";
            

            WebRequest tRequest;
            tRequest = WebRequest.Create("https://fcm.googleapis.com/fcm/send");
            tRequest.Method = "post";
            tRequest.ContentType = " application/json";

            tRequest.Headers.Add(string.Format("Authorization: key={0}", SERVER_KEY_TOKEN));
           

            var a = new
            {
                notification = new
                {
                    title,
                    body
                    //icon = "https://domain/path/to/logo.png",
                    //click_action,
                    //sound = "mySound"
                },
                to = deviceId
            };

            byte[] byteArray = Encoding.UTF8.GetBytes(Newtonsoft.Json.JsonConvert.SerializeObject(a));
            tRequest.ContentLength = byteArray.Length;

            Stream dataStream = tRequest.GetRequestStream();
            dataStream.Write(byteArray, 0, byteArray.Length);
            dataStream.Close();

            WebResponse tResponse = tRequest.GetResponse();
            dataStream = tResponse.GetResponseStream();

            StreamReader tReader = new StreamReader(dataStream);
            string sResponseFromServer = tReader.ReadToEnd();


         

            tReader.Close();
            dataStream.Close();
            tResponse.Close();

            return sResponseFromServer;
        }


        [HttpPost]
        public string mPostNoticerr(List<Notice> Notices)
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

                    for (int i = 0; i < Notices.Count; i++)
                    {
                        int intid = i;

                        if (intid == 0)
                        {


                            //string STRiMAGE = Notices[i].strNOTICE_IMG.Substring(Notices[i].strNOTICE_IMG.LastIndexOf('\\') + 1);





                            strSQL = "INSERT INTO HRS_NOTICE_NEW(";
                            strSQL = strSQL + "NOTICE_ID, TITLE, ANNOUNCEMENT, START_DATE, END_DATE,NOTICE_IMG , ACTION, CREATED_BY";

                            strSQL = strSQL + ")";
                            strSQL = strSQL + " VALUES(";
                            strSQL = strSQL + "'" + NOTICE_ID + "',";
                            strSQL = strSQL + "N'" + Notices[i].strTITLE + "',";
                            strSQL = strSQL + "N'" + Notices[i].strANNOUNCEMENT + "',";
                            strSQL = strSQL + "'" + Notices[i].strSTART_DATE + "',";
                            strSQL = strSQL + "'" + Notices[i].strEND_DATE + "',";
                            strSQL = strSQL + "'" + Notices[i].strNOTICE_IMG + "',";
                            strSQL = strSQL + "'" + Notices[i].strACTION + "',";
                            strSQL = strSQL + "'" + Notices[i].strCREATED_BY + "'";



                            strSQL = strSQL + ")";

                            cmdInsert.CommandText = strSQL;
                            cmdInsert.ExecuteNonQuery();


                        }
                        else if (intid == 1)
                        {
                            strSQL = "INSERT INTO HRS_MARKET_ACCESS_NEW (";
                            strSQL = strSQL + "NOTICE_ID,NATIONAL_HEAD, TEAM,ZONE,DIVISION,AREA,MARKET,ROUTE";

                            strSQL = strSQL + ")";
                            strSQL = strSQL + " VALUES(";
                            strSQL = strSQL + "'" + NOTICE_ID + "',";
                            strSQL = strSQL + "'" + Notices[i].strNATIONAL_HEAD + "',";
                            strSQL = strSQL + "'" + Notices[i].strTEAM + "',";
                            strSQL = strSQL + "'" + Notices[i].strZONE + "',";
                            strSQL = strSQL + "'" + Notices[i].strDIVISION + "',";
                            strSQL = strSQL + "'" + Notices[i].strAREA + "',";
                            strSQL = strSQL + "'" + Notices[i].strMARKET + "',";
                            strSQL = strSQL + "'" + Notices[i].strROUTE + "'";



                            strSQL = strSQL + ")";

                            cmdInsert.CommandText = strSQL;
                            cmdInsert.ExecuteNonQuery();

                        }
                        else if (intid == 2)
                        {
                            strSQL = "INSERT INTO HRS_USER_NEW (";
                            strSQL = strSQL + "NOTICE_ID,NATIONAL_HEAD, TEAM,ZONE,DIVISION,AREA,MARKET,ROUTE,USER_NAME,ROLE";

                            strSQL = strSQL + ")";
                            strSQL = strSQL + " VALUES(";
                            strSQL = strSQL + "'" + NOTICE_ID + "',";
                            strSQL = strSQL + "'" + Notices[i].strNATIONAL_HEAD + "',";
                            strSQL = strSQL + "'" + Notices[i].strTEAM + "',";
                            strSQL = strSQL + "'" + Notices[i].strZONE + "',";
                            strSQL = strSQL + "'" + Notices[i].strDIVISION + "',";
                            strSQL = strSQL + "'" + Notices[i].strAREA + "',";
                            strSQL = strSQL + "'" + Notices[i].strMARKET + "',";
                            strSQL = strSQL + "'" + Notices[i].strROUTE + "',";
                            strSQL = strSQL + "'" + Notices[i].strUSER_NAME + "',";
                            strSQL = strSQL + "'" + Notices[i].strROLE+ "'";



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



        public List<Notice> mGetNotice(Notice obj)
        {
            string strSQL = null;
            string connectionString = Utility.SQLConnstringComSwitch("0001");
            List<Notice> NoticeList = new List<Notice>();

            using (SqlConnection gcnMain = new SqlConnection(connectionString))
            {
                gcnMain.Open();

                strSQL = "select * from HRS_NOTICE_NEW  N ,HRS_USER_NEW U,HRS_MARKET_ACCESS_NEW M where N.NOTICE_ID= U.NOTICE_ID AND N.NOTICE_ID= M.NOTICE_ID ";

                using (SqlCommand cmd = new SqlCommand(strSQL, gcnMain))
                {
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Notice Notices = new Notice();
                            Notices.strNOTICE_ID = dr["NOTICE_ID"].ToString();
                            Notices.strTITLE = dr["TITLE"].ToString();
                            Notices.strANNOUNCEMENT = dr["ANNOUNCEMENT"].ToString();
                            Notices.strSTART_DATE = Convert.ToDateTime(dr["START_DATE"]).ToString("dd-MMM-yyyy");
                            Notices.strEND_DATE = Convert.ToDateTime(dr["END_DATE"]).ToString("dd-MMM-yyyy");
                            Notices.strACTION = dr["ACTION"].ToString();
                            Notices.strCREATED_BY = dr["CREATED_BY"].ToString();
                            Notices.strNOTICE_IMG = dr["NOTICE_IMG"].ToString();
                            Notices.strNATIONAL_HEAD = dr["NATIONAL_HEAD"].ToString();
                            Notices.strTEAM = dr["TEAM"].ToString();
                            Notices.strZONE = dr["ZONE"].ToString();
                            Notices.strDIVISION = dr["DIVISION"].ToString();
                            Notices.strAREA = dr["AREA"].ToString();
                            Notices.strMARKET = dr["MARKET"].ToString();
                            Notices.strROUTE = dr["ROUTE"].ToString();
                            Notices.strNATIONAL_HEAD = dr["NATIONAL_HEAD"].ToString();
                            Notices.strTEAM = dr["TEAM"].ToString();
                            Notices.strZONE = dr["ZONE"].ToString();
                            Notices.strDIVISION = dr["DIVISION"].ToString();
                            Notices.strAREA = dr["AREA"].ToString();
                            Notices.strMARKET = dr["MARKET"].ToString();
                            Notices.strROUTE = dr["ROUTE"].ToString();
                            Notices.strROLE = dr["ROLE"].ToString();
                            NoticeList.Add(Notices);
                        }
                    }
                }
            }

            if (NoticeList.Count == 0)
            {

                NoticeList.Add(new Notice());
            }

            return NoticeList;
        }




        public List<NoticeCofig> mGetNoticesLedger(NoticeCofig obj)
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
                            NoticeCofig tour = new NoticeCofig();
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
                            tour.strZONE_NAME = dr["ZONE_NAME"].ToString();
                            tour.strTEAM_NAME = dr["TEAM_NAME"].ToString();
                            //tour.intTEAM_CODE = Convert.ToInt32(dr["TEAM_CODE"]);
                            NoticeCofigList.Add(tour);
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



        public class Message
        {
            public string Topic { get; set; }
            public Notice Notice { get; set; }
            public string to { get; set; }
            public List<NotificationList> notification { get; set; }

        }

        public class NotificationList
        {
            public string title { get; set; }
            public string body { get; set; }
            public string mutable_content { get; set; }
            public string sound { get; set; }


        }

    }
}