using DPL.DASHBOARD.Models;
using Dutility;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DPL.DASHBOARD.Repesetory
{
    public class TypeController : Controller
    {
        //
        // GET: /Type/
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult mGetTypesLedgerList(TypeControll obj)
        {

            var allLedger = mGetTypesLedger(obj);
            return Json(allLedger, JsonRequestBehavior.AllowGet);
        }






        public List<TypeControll> mGetTypesLedger(TypeControll obj)
        {
            string strSQL = null;
            string connectionString = Utility.SQLConnstringComSwitch("0001");
            List<TypeControll> TypeControllList = new List<TypeControll>();

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
                            TypeControll types = new TypeControll();
                            types.intBRANCH_ID = Convert.ToInt32(dr["BRANCH_ID"]);
                            types.strZONE = dr["ZONE"].ToString();
                            types.strDIVISION = dr["DIVISION"].ToString();
                            types.strAREA = dr["AREA"].ToString();
                            types.strLEDGER_NAME = dr["LEDGER_NAME"].ToString();
                            types.strTERITORRY_CODE = dr["TERITORRY_CODE"].ToString();
                            types.strTERRITORRY_NAME = dr["TERRITORRY_NAME"].ToString();
                            types.strLEDGER_NAME_MERZE = dr["LEDGER_NAME_MERZE"].ToString();
                            types.intLEDGER_STATUS = Convert.ToInt32(dr["LEDGER_STATUS"]);
                            types.strGR_MOBILE_NO = dr["GR_MOBILE_NO"].ToString();
                            types.intHALT_MPO = Convert.ToInt32(dr["HALT_MPO"]);
                            types.strHL_LEDGER_NAME = dr["HL_LEDGER_NAME"].ToString();
                            types.strPF_LEDGER_NAME = dr["PF_LEDGER_NAME"].ToString();
                            types.strINSERT_DATE = dr["INSERT_DATE"].ToString();
                            types.strROUTE_NAME = dr["ROUTE_NAME"].ToString();
                            types.strLEDGER_CLASS = dr["LEDGER_CLASS"].ToString();
                            types.strLEDGER_ADD_DATE = dr["LEDGER_ADD_DATE"].ToString();
                            types.strLEDGER_RESIGN_DATE = dr["LEDGER_RESIGN_DATE"].ToString();
                            types.strMPO_DIV = dr["MPO_DIV"].ToString();
                            types.strGODOWNS_NAME = dr["GODOWNS_NAME"].ToString();
                            types.strMPO_CARD_NO = dr["MPO_CARD_NO"].ToString();
                            types.intCARTON_AMNT = Convert.ToInt32(dr["CARTON_AMNT"]);
                            types.strZONE_NAME = dr["ZONE_NAME"].ToString();
                            types.strTEAM_NAME = dr["TEAM_NAME"].ToString();
                            //types.intTEAM_CODE = Convert.ToInt32(dr["TEAM_CODE"]);
                            TypeControllList.Add(types);
                        }
                    }
                }
            }

            if (TypeControllList.Count == 0)
            {

                TypeControllList.Add(new TypeControll());
            }

            return TypeControllList;
        }




	}
}