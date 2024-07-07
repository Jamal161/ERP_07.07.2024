//using DPL.DASHBOARD.Models;
//using Dutility;
//using System;
//using System.Collections.Generic;
//using System.Data;
//using System.Data.SqlClient;
//using System.Linq;
//using System.Web;
//using System.Web.Mvc;

//namespace DPL.DASHBOARD.Controllers
//{
//    public class RepositoryController : Controller
//    {
//        private string cs;
//        //
//        // GET: /Repository/

//        public ActionResult Index()
//        {
//            return View();
//        }



//        public List<ShiftCongfig> mGetHeader()
//        {
//            string strComid = "0001";
//            string strSQL = null;
//            string connectionString = Utility.SQLConnstringComSwitch(strComid);

//            using (SqlConnection gcnMain = new SqlConnection(connectionString))
//            {
//                if (gcnMain.State == ConnectionState.Open)
//                {
//                    gcnMain.Close();
//                }
//                gcnMain.Open();
//                SqlDataReader dr;

//                List<ShiftCongfig> oooChequePrint = new List<ShiftCongfig>();

//                strSQL = "SELECT SHIFT_CONFIG_SERL, EMPLOYEE_SHIFT_NAME, EMPLOYEE_SHIFT_NAME_BANGLA, ";
//                strSQL = strSQL + "SHIFT_CONFIG_DATE, SHIFT_CONFIG_START_TIME, SHIFT_CONFIG_END_TIME, OT_STATUS ";
//                strSQL = strSQL + "FROM            HRS_SHIFT_CONFIG ";


//                SqlCommand cmd = new SqlCommand(strSQL, gcnMain);
//                dr = cmd.ExecuteReader();
//                while (dr.Read())
//                {

//                    ShiftCongfig oLedg = new ShiftCongfig();
//                    oLedg.intShiftCongfigID = Convert.ToInt32(dr["SHIFT_CONFIG_SERL"]);
//                    oLedg.strShiftName = dr["EMPLOYEE_SHIFT_NAME"].ToString();
//                    oLedg.strShiftNameBangla = dr["EMPLOYEE_SHIFT_NAME_BANGLA"].ToString();
//                    oLedg.intShiftCongfigDate = Convert.ToInt32(dr["SHIFT_CONFIG_DATE"]);
//                    oLedg.intShiftCongfigStartTime = Convert.ToInt32(dr["SHIFT_CONFIG_START_TIME"]);
//                    oLedg.intShiftCongfigEndTime = Convert.ToInt32(dr["SHIFT_CONFIG_END_TIME"]);
//                    oLedg.intOtStatus = Convert.ToInt32(dr["OT_STATUS"]);
//                    oooChequePrint.Add(oLedg);
//                }

//                if (!dr.HasRows)
//                {
//                    ShiftCongfig oLedg = new ShiftCongfig();
//                    oLedg.intShiftCongfigID = 0;
//                    oLedg.strShiftName = "";
//                    oLedg.strShiftNameBangla = "";
//                    oLedg.intShiftCongfigDate = 0;
//                    oLedg.intShiftCongfigStartTime = 0;
//                    oLedg.intShiftCongfigEndTime = 0;
//                    oLedg.intOtStatus = 0;
//                    oooChequePrint.Add(oLedg);
//                }
//                dr.Close();
//                gcnMain.Close();
//                cmd.Dispose();
//                return oooChequePrint;
//            }

//        }

//    }

//}