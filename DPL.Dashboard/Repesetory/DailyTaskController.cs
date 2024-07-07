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
    public class DailyTaskController : Controller
    {
        //
        // GET: /DailyTask/
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult mPostDailyTask()
        {

            var allLedger = "";
            return Json(allLedger, JsonRequestBehavior.AllowGet);
        }


        public ActionResult mGetdailyTaskList(DailyTask obj)
        {
            List<DailyTask> UserList = new List<DailyTask>();
            UserList = mGetdailyTask(obj);
            return Json(UserList, JsonRequestBehavior.AllowGet);
        }



         public ActionResult mGetdailyTasktodayList(DailyTask obj)
        {
            List<DailyTask> UserList = new List<DailyTask>();
            UserList = mGetdailyTasktoday(obj);
            return Json(UserList, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public string mPostDailyTask(DailyTask obj)
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

                    string Task_ID = DateTime.UtcNow.ToString("yyMMddHHmmss");

                    SqlCommand cmdInsert = new SqlCommand();
                    SqlTransaction myTrans;
                    myTrans = gcnMain.BeginTransaction();
                    cmdInsert.Connection = gcnMain;
                    cmdInsert.Transaction = myTrans;
                    strSQL = "INSERT INTO HRS_DAILY_TASK (";
                    strSQL = strSQL + " Task_ID,Title,Body,Deadline, ";
                    strSQL = strSQL + "status, NationalHead, ";
                    strSQL = strSQL + " Team, Zone , Division , Area ,Market,Route,Role,CardNo ";
                    strSQL = strSQL + ") ";
                    strSQL = strSQL + "VALUES (";
                    strSQL = strSQL + "'" + Task_ID + "',";
                    strSQL = strSQL + "N'" + obj.strTitle + "',";
                    strSQL = strSQL + "N'" + obj.strBody + "',";
                    strSQL = strSQL + "'" + obj.strDeadline + "',";
                    strSQL = strSQL + "'" + obj.strstatus + "',";
                    strSQL = strSQL + "'" + obj.strNationalHead + "',";
                    strSQL = strSQL + "'" + obj.strTeam + "',";
                    strSQL = strSQL + "'" + obj.strZone + "',";
                    strSQL = strSQL + "'" + obj.strDivision + "',";
                    strSQL = strSQL + "'" + obj.strArea + "',";
                    strSQL = strSQL + "'" + obj.strMarket + "',";
                    strSQL = strSQL + "'" + obj.strRoute + "',";
                    strSQL = strSQL + "'" + obj.strRole + "',";
                    strSQL = strSQL + "'" + obj.strCardNo + "'";

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
        public List<DailyTask> mGetdailyTask(DailyTask obj)
        {
            string strSQL = null;
            string connectionString = Utility.SQLConnstringComSwitch("0001");
            List<DailyTask> DailyTaskList = new List<DailyTask>();

            using (SqlConnection gcnMain = new SqlConnection(connectionString))
            {
                gcnMain.Open();

                strSQL = "SELECT * FROM HRS_DAILY_TASK WHERE CardNo='" + obj.strCardNo + "' AND Deadline >= '" + obj.strDeadline + "' ";

                using (SqlCommand cmd = new SqlCommand(strSQL, gcnMain))
                {
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            DailyTask DailyTasks = new DailyTask();
                            DailyTasks.strTask_ID = dr["Task_ID"].ToString();
                            DailyTasks.strTitle = dr["Title"].ToString();
                            DailyTasks.strBody = dr["Body"].ToString();
                            DailyTasks.strDeadline = dr["Deadline"].ToString();
                            DailyTasks.strstatus = dr["status"].ToString();
                            DailyTasks.strNationalHead = dr["NationalHead"].ToString();
                            DailyTasks.strTeam = dr["Team"].ToString();
                            DailyTasks.strZone = dr["Zone"].ToString();
                            DailyTasks.strDivision = dr["Division"].ToString();
                            DailyTasks.strArea = dr["Area"].ToString();
                            DailyTasks.strMarket = dr["Market"].ToString();
                            DailyTasks.strRoute = dr["Route"].ToString();
                            DailyTasks.strRole = dr["Role"].ToString();
                            DailyTasks.strCardNo = dr["CardNo"].ToString();
                            DailyTaskList.Add(DailyTasks);
                        }
                    }
                }
            }

            if (DailyTaskList.Count == 0)
            {

                DailyTaskList.Add(new DailyTask());
            }

            return DailyTaskList;
        }





        public List<DailyTask> mGetdailyTasktoday(DailyTask obj)
        {
            string strSQL = null;
            string connectionString = Utility.SQLConnstringComSwitch("0001");
            List<DailyTask> DailyTaskList = new List<DailyTask>();

            using (SqlConnection gcnMain = new SqlConnection(connectionString))
            {
                gcnMain.Open();

                strSQL = "SELECT * FROM HRS_DAILY_TASK";

                using (SqlCommand cmd = new SqlCommand(strSQL, gcnMain))
                {
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            DailyTask DailyTasks = new DailyTask();
                            DailyTasks.strTask_ID = dr["Task_ID"].ToString();
                            DailyTasks.strTitle = dr["Title"].ToString();
                            DailyTasks.strBody = dr["Body"].ToString();
                            DailyTasks.strDeadline = dr["Deadline"].ToString();
                            DailyTasks.strstatus = dr["status"].ToString();
                            DailyTasks.strNationalHead = dr["NationalHead"].ToString();
                            DailyTasks.strTeam = dr["Team"].ToString();
                            DailyTasks.strZone = dr["Zone"].ToString();
                            DailyTasks.strDivision = dr["Division"].ToString();
                            DailyTasks.strArea = dr["Area"].ToString();
                            DailyTasks.strMarket = dr["Market"].ToString();
                            DailyTasks.strRoute = dr["Route"].ToString();
                            DailyTasks.strRole = dr["Role"].ToString();
                            DailyTasks.strCardNo = dr["CardNo"].ToString();
                            DailyTaskList.Add(DailyTasks);
                        }
                    }
                }
            }

            if (DailyTaskList.Count == 0)
            {

                DailyTaskList.Add(new DailyTask());
            }

            return DailyTaskList;
        }


	}
}