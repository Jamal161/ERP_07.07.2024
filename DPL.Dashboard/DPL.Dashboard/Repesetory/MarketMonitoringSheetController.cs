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
    public class MarketMonitoringSheetController : Controller
    {
        //
        // GET: /MarketMonitoringSheet/
        //public ActionResult MarketMonitoringSheet(string strBranchName,int intledgerStatus,int intLedgertype, String vstrLedgerName, string fromdate, string todate,  int chkboxSpecilaMonitort,  bool boolchkboxPartyCoverage,int intDetails)
        //{
        //          string strDeComID = "0005";


        //          string userRole = (string)Session["UserRole"];
        //    //if (userRole != null && ( userRole.Trim() == "DH" ))
        //          string strBranchID = userRole.Trim();

        //          var allLedger = mGetMarketMonitoringSheet(strDeComID, fromdate, todate, Session["UserBranchID"].ToString(),
        //                               intledgerStatus, vstrLedgerName, gstrUserName, 0,
        //                                intOption, vstrLedgerName, intDay, intUpdDay, strNewFdate, strNewTdate);
        //    var jsonResult = Json(allLedger, JsonRequestBehavior.AllowGet);
        //    jsonResult.MaxJsonLength = int.MaxValue;
        //    return jsonResult;
        //}



        public ActionResult MarketMonitoringSheetList(string strBranchID, int intstatus, string strString, string strLedger,int intDay, int intUpdDay, string gstrUserName, int intSelection, int intOption, string strFate, string strTDate, string strNewFdate, string strNewTdate, string strUserLavel)
        {
            //var allLedger = "";
            var allLedger = mGetMarketMonitoringSheet(strBranchID, intstatus,  strString, strLedger,  intDay, intUpdDay , gstrUserName, intSelection, intOption, strFate, strTDate, strNewFdate, strNewTdate ,  strUserLavel);
            var jsonResult = Json(allLedger, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;

        }



        [HttpPost]
        //#region "Marketing Monitoring Sheet"



        public List<RFinalStatement> mGetMarketMonitoringSheet(string strBranchID, int intstatus, string strString, string strLedger, int intDay, int intUpdDay, string gstrUserName, int intSelection, int intOption, string strFate, string strTDate, string strNewFdate, string strNewTdate, string strUserLavel)
            
            //(string strDeComID, string strFate, string strTDate, string strBranchID,
            //                                  int intstatus, string strString, string gstrUserName, int intSelection, int intBaseTarget,
            //                                   int intOption, string strLedger, int intDay, int intUpdDay, string strNewFdate, string strNewTdate, string strUserLavel)
        {
            string strSQL = null;
            string strledger = "";
            int intCheck = 0;
            string strDeComID = "0005";
            string connstring = "Data Source=192.168.1.63\\DPL12 ;Initial Catalog= SMART0005;User ID=sa ;Password=manager ";

            using (SqlConnection gcnMain = new SqlConnection(connstring))
            {
                if (gcnMain.State == ConnectionState.Open)
                {
                    gcnMain.Close();
                }
                gcnMain.Open();
                strString = "'" + Convert.ToDateTime(strFate).ToString("MMMyy").ToUpper()  + "'" ;
                SqlDataReader dr;
                SqlCommand cmdInsert = new SqlCommand();
                SqlTransaction myTrans;
                myTrans = gcnMain.BeginTransaction();
                cmdInsert.Connection = gcnMain;
                cmdInsert.Transaction = myTrans;
                cmdInsert.CommandTimeout = 0;
                strSQL = "DELETE FROM SMART0005.dbo.ACC_MARKET_MONITROING_SHEET WHERE USER_NAME='" + gstrUserName + "' ";
                cmdInsert.CommandText = strSQL;
                cmdInsert.ExecuteNonQuery();

                #region
                if (intstatus == 1)
                {
                    strSQL = "INSERT INTO SMART0005.dbo.ACC_MARKET_MONITROING_SHEET(ZONE,DIVISION,AREA,TERITORRY_NAME,TERITORRY_CODE,MR_NAME,TOTAL_OS,USER_NAME) ";
                    strSQL = strSQL + "select g.GR_PARENT AS zone, g.GR_NAME AS Division, l.LEDGER_PARENT_GROUP AS area,l.TERRITORRY_NAME ,l.TERITORRY_CODE,l.LEDGER_NAME_MERZE,  0 COLLECTION_AMNT  ";
                    strSQL = strSQL + ",'" + gstrUserName + "' from SMART0005.dbo.ACC_LEDGERGROUP g,SMART0005.dbo.ACC_LEDGERGROUP_CATEGORY_VIEW v,SMART0005.dbo.ACC_LEDGER l, SMART0005.dbo.ACC_LEDGER_Z_D_A ";
                    strSQL = strSQL + "where L.LEDGER_NAME =SMART0005.dbo.ACC_LEDGER_Z_D_A.LEDGER_NAME AND g.GR_NAME=v.GR_PARENT  and v.GR_NAME=l.LEDGER_PARENT_GROUP AND l.LEDGER_STATUS =1   ";
                    if (strBranchID != "")
                    {
                        strSQL = strSQL + " AND l.BRANCH_ID ='" + strBranchID + "' ";
                    }
                    if (intOption == 2)
                    {
                        if (strLedger != "")
                        {
                            strSQL = strSQL + "AND SMART0005.dbo.ACC_LEDGER_Z_D_A.LEDGER_NAME_MERZE IN(" + strLedger + ") ";
                        }
                    }
                    else if (intOption == 3)
                    {
                        if (strLedger != "")
                        {
                            strSQL = strSQL + "AND SMART0005.dbo.ACC_LEDGER_Z_D_A.AREA IN(" + strLedger + ") ";
                        }
                    }
                    else if (intOption == 4)
                    {
                        if (strLedger != "")
                        {
                            strSQL = strSQL + "AND SMART0005.dbo.ACC_LEDGER_Z_D_A.DIVISION IN(" + strLedger + ") ";
                        }
                    }
                    else if (intOption == 5)
                    {
                        if (strLedger != "")
                        {
                            strSQL = strSQL + "AND SMART0005.dbo.ACC_LEDGER_Z_D_A.ZONE IN(" + strLedger + ") ";
                        }
                    }
                    if (gstrUserName != "")
                    {
                        strSQL = strSQL + " AND SMART0005.dbo.ACC_LEDGER_Z_D_A.DIVISION in( select LEDGER_GROUP_NAME from SMART0005.dbo.USER_PRIVILEGES_COLOR WHERE USER_LOGIN_NAME ='" + gstrUserName + "')";
                    }
                    strSQL = strSQL + "group by g.GR_PARENT , g.GR_NAME , l.LEDGER_PARENT_GROUP,l.LEDGER_NAME_MERZE,l.TERITORRY_CODE,l.TERRITORRY_NAME  ";
                    cmdInsert.CommandText = strSQL;
                    cmdInsert.ExecuteNonQuery();
                }
                if (intstatus == 2)
                {
                    strSQL = "INSERT INTO SMART0005.dbo.ACC_MARKET_MONITROING_SHEET(ZONE,DIVISION,AREA,TERITORRY_NAME,TERITORRY_CODE,MR_NAME,TOTAL_OS,USER_NAME) ";
                    strSQL = strSQL + "select g.GR_PARENT AS zone, g.GR_NAME AS Division, l.LEDGER_PARENT_GROUP AS area,l.TERRITORRY_NAME ,l.TERITORRY_CODE,l.LEDGER_NAME_MERZE,  0 COLLECTION_AMNT  ";
                    strSQL = strSQL + ",'" + gstrUserName + "' from SMART0005.dbo.ACC_LEDGERGROUP g,SMART0005.dbo.ACC_LEDGERGROUP_CATEGORY_VIEW v,SMART0005.dbo.ACC_LEDGER l,SMART0005.dbo.ACC_LEDGER_Z_D_A ";
                    strSQL = strSQL + "where L.LEDGER_NAME =SMART0005.dbo.ACC_LEDGER_Z_D_A.LEDGER_NAME AND  g.GR_NAME=v.GR_PARENT  and v.GR_NAME=l.LEDGER_PARENT_GROUP AND l.LEDGER_STATUS in (0,1)   ";
                    if (strBranchID != "")
                    {
                        strSQL = strSQL + " AND l.BRANCH_ID ='" + strBranchID + "' ";
                    }
                    if (intOption == 2)
                    {
                        if (strLedger != "")
                        {
                            strSQL = strSQL + "AND SMART0005.dbo.ACC_LEDGER_Z_D_A.LEDGER_NAME_MERZE IN(" + strLedger + ") ";
                        }
                    }
                    else if (intOption == 3)
                    {
                        if (strLedger != "")
                        {
                            strSQL = strSQL + "AND SMART0005.dbo.ACC_LEDGER_Z_D_A.AREA IN(" + strLedger + ") ";
                        }
                    }
                    else if (intOption == 4)
                    {
                        if (strLedger != "")
                        {
                            strSQL = strSQL + "AND SMART0005.dbo.ACC_LEDGER_Z_D_A.DIVISION IN(" + strLedger + ") ";
                        }
                    }
                    else if (intOption == 5)
                    {
                        if (strLedger != "")
                        {
                            strSQL = strSQL + "AND SMART0005.dbo.ACC_LEDGER_Z_D_A.ZONE IN(" + strLedger + ") ";
                        }
                    }
                    if (gstrUserName != "")
                    {
                        strSQL = strSQL + " AND SMART0005.dbo.ACC_LEDGER_Z_D_A.DIVISION in( select LEDGER_GROUP_NAME from USER_PRIVILEGES_COLOR WHERE USER_LOGIN_NAME ='" + gstrUserName + "')";
                    }
                    strSQL = strSQL + "group by g.GR_PARENT , g.GR_NAME , l.LEDGER_PARENT_GROUP,l.LEDGER_NAME_MERZE,l.TERITORRY_CODE,l.TERRITORRY_NAME  ";
                    cmdInsert.CommandText = strSQL;
                    cmdInsert.ExecuteNonQuery();
                }

                strSQL = "INSERT INTO SMART0005.dbo.ACC_MARKET_MONITROING_SHEET(ZONE,DIVISION,AREA,TERITORRY_NAME,TERITORRY_CODE,MR_NAME,TOTAL_OS,USER_NAME) ";
                strSQL = strSQL + "SELECT g.GR_PARENT AS zone, g.GR_NAME AS Division, l.LEDGER_PARENT_GROUP AS area,l.TERRITORRY_NAME , l.TERITORRY_CODE,l.LEDGER_NAME_MERZE, isnull(sum(l.LEDGER_OPENING_BALANCE),0) *-1 PDUES   ";
                strSQL = strSQL + ",'" + gstrUserName + "' from SMART0005.dbo.ACC_LEDGERGROUP g,SMART0005.dbo.ACC_LEDGERGROUP_CATEGORY_VIEW v,SMART0005.dbo.ACC_LEDGER l,SMART0005.dbo.ACC_LEDGER_Z_D_A where L.LEDGER_NAME =SMART0005.dbo.ACC_LEDGER_Z_D_A.LEDGER_NAME AND  g.GR_NAME=v.GR_PARENT  and v.GR_NAME=l.LEDGER_PARENT_GROUP ";
                if (strBranchID != "")
                {
                    strSQL = strSQL + "AND l.BRANCH_ID ='" + strBranchID + "' ";
                }

                if (intstatus < 2)
                {
                    strSQL = strSQL + "AND l.LEDGER_STATUS =" + intstatus + " ";
                }
                else
                {
                    strSQL = strSQL + "AND l.LEDGER_STATUS in (0,1)";
                }
                if (intOption == 2)
                {
                    if (strLedger != "")
                    {
                        strSQL = strSQL + "AND SMART0005.dbo.ACC_LEDGER_Z_D_A.LEDGER_NAME_MERZE IN(" + strLedger + ") ";
                    }
                }
                else if (intOption == 3)
                {
                    if (strLedger != "")
                    {
                        strSQL = strSQL + "AND SMART0005.dbo.ACC_LEDGER_Z_D_A.AREA IN(" + strLedger + ") ";
                    }
                }
                else if (intOption == 4)
                {
                    if (strLedger != "")
                    {
                        strSQL = strSQL + "AND SMART0005.dbo.ACC_LEDGER_Z_D_A.DIVISION IN(" + strLedger + ") ";
                    }
                }
                else if (intOption == 5)
                {
                    if (strLedger != "")
                    {
                        strSQL = strSQL + "AND SMART0005.dbo.ACC_LEDGER_Z_D_A.ZONE IN(" + strLedger + ") ";
                    }
                }
                if (gstrUserName != "")
                {
                    strSQL = strSQL + " AND SMART0005.dbo.ACC_LEDGER_Z_D_A.DIVISION in( select LEDGER_GROUP_NAME from SMART0005.dbo.USER_PRIVILEGES_COLOR WHERE USER_LOGIN_NAME ='" + gstrUserName + "')";
                }
                strSQL = strSQL + "group by g.GR_PARENT , g.GR_NAME , l.LEDGER_PARENT_GROUP,l.LEDGER_NAME_MERZE,l.TERITORRY_CODE,l.TERRITORRY_NAME ";
                cmdInsert.CommandText = strSQL;
                cmdInsert.ExecuteNonQuery();

                //Current Month
                strSQL = "INSERT INTO SMART0005.dbo.ACC_MARKET_MONITROING_SHEET(ZONE,DIVISION,AREA,TERITORRY_NAME,TERITORRY_CODE,MR_NAME,TOTAL_OS,USER_NAME) ";
                strSQL = strSQL + "select g.GR_PARENT AS zone, g.GR_NAME AS Division, l.LEDGER_PARENT_GROUP AS area,l.TERRITORRY_NAME , l.TERITORRY_CODE,l.LEDGER_NAME_MERZE, ";
                strSQL = strSQL + "abs(ISNULL(SUM(C.COMP_VOUCHER_NET_AMOUNT),0)) SALES_CURRENT_MONTH,'" + gstrUserName + "'   ";
                strSQL = strSQL + " from SMART0005.dbo.ACC_LEDGERGROUP g,SMART0005.dbo.ACC_LEDGERGROUP_CATEGORY_VIEW v,SMART0005.dbo.ACC_LEDGER l,SMART0005.dbo.ACC_COMPANY_VOUCHER c,SMART0005.dbo.ACC_LEDGER_Z_D_A ";
                strSQL = strSQL + "where L.LEDGER_NAME =SMART0005.dbo.ACC_LEDGER_Z_D_A.LEDGER_NAME AND g.GR_NAME=v.GR_PARENT  and v.GR_NAME=l.LEDGER_PARENT_GROUP and l.LEDGER_NAME =c.LEDGER_NAME ";
                strSQL = strSQL + "AND C.COMP_VOUCHER_DATE < " + Utility.cvtSQLDateString(strFate) + " ";
                strSQL = strSQL + "AND  C.COMP_VOUCHER_TYPE =16 ";
                if (strBranchID != "")
                {
                    strSQL = strSQL + "AND C.BRANCH_ID ='" + strBranchID + "' ";
                }

                if (intstatus < 2)
                {
                    strSQL = strSQL + "AND l.LEDGER_STATUS =" + intstatus + " ";
                }
                else
                {
                    strSQL = strSQL + "AND l.LEDGER_STATUS in (0,1)";
                }
                if (intOption == 2)
                {
                    if (strLedger != "")
                    {
                        strSQL = strSQL + "AND SMART0005.dbo.ACC_LEDGER_Z_D_A.LEDGER_NAME_MERZE IN(  " + strLedger + " ) ";
                    }
                }
                else if (intOption == 3)
                {
                    if (strLedger != "")
                    {
                        strSQL = strSQL + "AND SMART0005.dbo.ACC_LEDGER_Z_D_A.AREA IN(" + strLedger + ") ";
                    }
                }
                else if (intOption == 4)
                {
                    if (strLedger != "")
                    {
                        strSQL = strSQL + "AND SMART0005.dbo.ACC_LEDGER_Z_D_A.DIVISION IN(" + strLedger + ") ";
                    }
                }
                else if (intOption == 5)
                {
                    if (strLedger != "")
                    {
                        strSQL = strSQL + "AND SMART0005.dbo.ACC_LEDGER_Z_D_A.ZONE IN(" + strLedger + ") ";
                    }
                }
                if (gstrUserName != "")
                {
                    strSQL = strSQL + " AND SMART0005.dbo.ACC_LEDGER_Z_D_A.DIVISION in( select LEDGER_GROUP_NAME from SMART0005.dbo.USER_PRIVILEGES_COLOR WHERE USER_LOGIN_NAME ='" + gstrUserName + "')";
                }
                strSQL = strSQL + "group by g.GR_PARENT , g.GR_NAME , l.LEDGER_PARENT_GROUP,l.LEDGER_NAME_MERZE,l.TERITORRY_CODE,l.TERRITORRY_NAME";
                cmdInsert.CommandText = strSQL;
                cmdInsert.ExecuteNonQuery();
                //Return
                strSQL = "INSERT INTO SMART0005.dbo.ACC_MARKET_MONITROING_SHEET(ZONE,DIVISION,AREA,TERITORRY_NAME,TERITORRY_CODE,MR_NAME,TOTAL_OS,USER_NAME) ";
                strSQL = strSQL + "select g.GR_PARENT AS zone, g.GR_NAME AS Division, l.LEDGER_PARENT_GROUP AS area,l.TERRITORRY_NAME , l.TERITORRY_CODE,l.LEDGER_NAME_MERZE, ";
                strSQL = strSQL + "abs(ISNULL(SUM(C.COMP_VOUCHER_NET_AMOUNT),0))*-1 RETURN_AMOUNT,'" + gstrUserName + "'  ";
                strSQL = strSQL + "from SMART0005.dbo.ACC_LEDGERGROUP g,SMART0005.dbo.ACC_LEDGERGROUP_CATEGORY_VIEW v,SMART0005.dbo.ACC_LEDGER l,SMART0005.dbo.ACC_COMPANY_VOUCHER c,SMART0005.dbo.ACC_LEDGER_Z_D_A ";
                strSQL = strSQL + "where L.LEDGER_NAME =SMART0005.dbo.ACC_LEDGER_Z_D_A.LEDGER_NAME AND  g.GR_NAME=v.GR_PARENT  and v.GR_NAME=l.LEDGER_PARENT_GROUP and l.LEDGER_NAME =c.LEDGER_NAME ";
                strSQL = strSQL + "AND C.COMP_VOUCHER_DATE < " + Utility.cvtSQLDateString(strFate) + " ";
                strSQL = strSQL + "AND  C.COMP_VOUCHER_TYPE =13 ";
                if (strBranchID != "")
                {
                    strSQL = strSQL + "AND C.BRANCH_ID ='" + strBranchID + "' ";
                }

                if (intstatus < 2)
                {
                    strSQL = strSQL + "AND l.LEDGER_STATUS =" + intstatus + " ";
                }
                else
                {
                    strSQL = strSQL + "AND l.LEDGER_STATUS in (0,1)";
                }
                if (intOption == 2)
                {
                    if (strLedger != "")
                    {
                        strSQL = strSQL + "AND SMART0005.dbo.ACC_LEDGER_Z_D_A.LEDGER_NAME_MERZE IN(" + strLedger + ") ";
                    }
                }
                else if (intOption == 3)
                {
                    if (strLedger != "")
                    {
                        strSQL = strSQL + "AND SMART0005.dbo.ACC_LEDGER_Z_D_A.AREA IN(" + strLedger + ") ";
                    }
                }
                else if (intOption == 4)
                {
                    if (strLedger != "")
                    {
                        strSQL = strSQL + "AND SMART0005.dbo.ACC_LEDGER_Z_D_A.DIVISION IN(" + strLedger + ") ";
                    }
                }
                else if (intOption == 5)
                {
                    if (strLedger != "")
                    {
                        strSQL = strSQL + "AND SMART0005.dbo.ACC_LEDGER_Z_D_A.ZONE IN(" + strLedger + ") ";
                    }
                }
                if (gstrUserName != "")
                {
                    strSQL = strSQL + " AND SMART0005.dbo.ACC_LEDGER_Z_D_A.DIVISION in( select LEDGER_GROUP_NAME from SMART0005.dbo.USER_PRIVILEGES_COLOR WHERE USER_LOGIN_NAME ='" + gstrUserName + "')";
                }
                strSQL = strSQL + "group by g.GR_PARENT , g.GR_NAME , l.LEDGER_PARENT_GROUP,l.LEDGER_NAME_MERZE,l.TERITORRY_CODE,l.TERRITORRY_NAME";
                cmdInsert.CommandText = strSQL;
                cmdInsert.ExecuteNonQuery();
                //Debit Amount

                strSQL = "INSERT INTO SMART0005.dbo.ACC_MARKET_MONITROING_SHEET(ZONE,DIVISION,AREA,TERITORRY_NAME,TERITORRY_CODE,MR_NAME,TOTAL_OS,USER_NAME) ";
                strSQL = strSQL + "select g.GR_PARENT AS zone, g.GR_NAME AS Division, l.LEDGER_PARENT_GROUP AS area,l.TERRITORRY_NAME , l.TERITORRY_CODE,l.LEDGER_NAME_MERZE, ";
                strSQL = strSQL + "abs(ISNULL(SUM(av.VOUCHER_DEBIT_AMOUNT-av.VOUCHER_CREDIT_AMOUNT),0)) DEBIT_AMOUNT,'" + gstrUserName + "'  ";
                strSQL = strSQL + "from SMART0005.dbo.ACC_LEDGERGROUP g,SMART0005.dbo.ACC_LEDGERGROUP_CATEGORY_VIEW v,SMART0005.dbo.ACC_LEDGER l,SMART0005.dbo.ACC_COMPANY_VOUCHER c,SMART0005.dbo.ACC_VOUCHER av,SMART0005.dbo.ACC_LEDGER_Z_D_A ";
                strSQL = strSQL + "where L.LEDGER_NAME =SMART0005.dbo.ACC_LEDGER_Z_D_A.LEDGER_NAME AND  g.GR_NAME=v.GR_PARENT  and v.GR_NAME=l.LEDGER_PARENT_GROUP and l.LEDGER_NAME =av.LEDGER_NAME  and c.COMP_REF_NO =av.COMP_REF_NO ";
                strSQL = strSQL + "AND aV.VOUCHER_TOBY ='Dr' AND aV.COMP_VOUCHER_TYPE =3 ";
                strSQL = strSQL + "AND C.COMP_VOUCHER_DATE < " + Utility.cvtSQLDateString(strFate) + " ";
                if (strBranchID != "")
                {
                    strSQL = strSQL + "AND C.BRANCH_ID ='" + strBranchID + "' ";
                }
                strSQL = strSQL + " AND  C.SP_JOURNAL =0 ";
                strSQL = strSQL + " AND  C.AUTOJV =0 ";
                strSQL = strSQL + " AND  C.DISABLE_VOUCHER =0 ";

                if (intstatus < 2)
                {
                    strSQL = strSQL + "AND l.LEDGER_STATUS =" + intstatus + " ";
                }
                else
                {
                    strSQL = strSQL + "AND l.LEDGER_STATUS in (0,1)";
                }
                if (intOption == 2)
                {
                    if (strLedger != "")
                    {
                        strSQL = strSQL + "AND SMART0005.dbo.ACC_LEDGER_Z_D_A.LEDGER_NAME_MERZE IN( " + strLedger + " ) ";
                    }
                }
                else if (intOption == 3)
                {
                    if (strLedger != "")
                    {
                        strSQL = strSQL + "AND SMART0005.dbo.ACC_LEDGER_Z_D_A.AREA IN(" + strLedger + ") ";
                    }
                }
                else if (intOption == 4)
                {
                    if (strLedger != "")
                    {
                        strSQL = strSQL + "AND SMART0005.dbo.ACC_LEDGER_Z_D_A.DIVISION IN(" + strLedger + ") ";
                    }
                }
                else if (intOption == 5)
                {
                    if (strLedger != "")
                    {
                        strSQL = strSQL + "AND SMART0005.dbo.ACC_LEDGER_Z_D_A.ZONE IN(" + strLedger + ") ";
                    }
                }
                if (gstrUserName != "")
                {
                    strSQL = strSQL + " AND SMART0005.dbo.ACC_LEDGER_Z_D_A.DIVISION in( select LEDGER_GROUP_NAME from SMART0005.dbo.USER_PRIVILEGES_COLOR WHERE USER_LOGIN_NAME ='" + gstrUserName + "')";
                }
                strSQL = strSQL + "group by g.GR_PARENT , g.GR_NAME , l.LEDGER_PARENT_GROUP,l.LEDGER_NAME_MERZE,l.TERITORRY_CODE,l.TERRITORRY_NAME ";
                cmdInsert.CommandText = strSQL;
                cmdInsert.ExecuteNonQuery();
                //Modified_24-11-19
                strSQL = "INSERT INTO SMART0005.dbo.ACC_MARKET_MONITROING_SHEET(ZONE,DIVISION,AREA,TERITORRY_NAME,TERITORRY_CODE,MR_NAME,TOTAL_OS,USER_NAME) ";
                strSQL = strSQL + "select g.GR_PARENT AS zone, g.GR_NAME AS Division, l.LEDGER_PARENT_GROUP AS area,l.TERRITORRY_NAME , l.TERITORRY_CODE,l.LEDGER_NAME_MERZE, ";
                strSQL = strSQL + "abs(ISNULL(SUM(av.VOUCHER_DEBIT_AMOUNT-av.VOUCHER_CREDIT_AMOUNT),0)) DEBIT_AMOUNT,'" + gstrUserName + "'   ";
                strSQL = strSQL + "from SMART0005.dbo.ACC_LEDGERGROUP g,SMART0005.dbo.ACC_LEDGERGROUP_CATEGORY_VIEW v,SMART0005.dbo.ACC_LEDGER l,SMART0005.dbo.ACC_COMPANY_VOUCHER c,SMART0005.dbo.ACC_VOUCHER av,SMART0005.dbo.ACC_LEDGER_Z_D_A ";
                strSQL = strSQL + "where L.LEDGER_NAME =SMART0005.dbo.ACC_LEDGER_Z_D_A.LEDGER_NAME AND g.GR_NAME=v.GR_PARENT  and v.GR_NAME=l.LEDGER_PARENT_GROUP and l.LEDGER_NAME =av.LEDGER_NAME  and c.COMP_REF_NO =av.COMP_REF_NO ";
                strSQL = strSQL + "AND aV.VOUCHER_TOBY ='Dr' AND aV.COMP_VOUCHER_TYPE =3 ";
                strSQL = strSQL + "AND C.COMP_VOUCHER_DATE < " + Utility.cvtSQLDateString(strFate) + " ";
                if (strBranchID != "")
                {
                    strSQL = strSQL + "AND C.BRANCH_ID ='" + strBranchID + "' ";
                }
                strSQL = strSQL + " AND  C.SP_JOURNAL =0 ";
                strSQL = strSQL + " AND  C.DISABLE_VOUCHER =1 ";
                strSQL = strSQL + " AND  C.AUTOJV =0 ";

                if (intstatus < 2)
                {
                    strSQL = strSQL + "AND l.LEDGER_STATUS =" + intstatus + " ";
                }
                else
                {
                    strSQL = strSQL + "AND l.LEDGER_STATUS in (0,1)";
                }
                if (intOption == 2)
                {
                    if (strLedger != "")
                    {
                        strSQL = strSQL + "AND SMART0005.dbo.ACC_LEDGER_Z_D_A.LEDGER_NAME_MERZE IN( " + strLedger + " ) ";
                    }
                }
                else if (intOption == 3)
                {
                    if (strLedger != "")
                    {
                        strSQL = strSQL + "AND SMART0005.dbo.ACC_LEDGER_Z_D_A.AREA IN(" + strLedger + ") ";
                    }
                }
                else if (intOption == 4)
                {
                    if (strLedger != "")
                    {
                        strSQL = strSQL + "AND SMART0005.dbo.ACC_LEDGER_Z_D_A.DIVISION IN(" + strLedger + ") ";
                    }
                }
                else if (intOption == 5)
                {
                    if (strLedger != "")
                    {
                        strSQL = strSQL + "AND SMART0005.dbo.ACC_LEDGER_Z_D_A.ZONE IN(" + strLedger + ") ";
                    }
                }
                if (gstrUserName != "")
                {
                    strSQL = strSQL + " AND SMART0005.dbo.ACC_LEDGER_Z_D_A.DIVISION in( select LEDGER_GROUP_NAME from SMART0005.dbo.USER_PRIVILEGES_COLOR WHERE USER_LOGIN_NAME ='" + gstrUserName + "')";
                }
                strSQL = strSQL + "group by g.GR_PARENT , g.GR_NAME , l.LEDGER_PARENT_GROUP,l.LEDGER_NAME_MERZE,l.TERITORRY_CODE,l.TERRITORRY_NAME ";
                cmdInsert.CommandText = strSQL;
                cmdInsert.ExecuteNonQuery();



                //Credit Amount
                strSQL = "INSERT INTO SMART0005.dbo.ACC_MARKET_MONITROING_SHEET(ZONE,DIVISION,AREA,TERITORRY_NAME,TERITORRY_CODE,MR_NAME,TOTAL_OS,USER_NAME) ";
                strSQL = strSQL + "SELECT  ZDA.ZONE , ZDA.DIVISION , ZDA.AREA  ,l.TERRITORRY_NAME , l.TERITORRY_CODE,l.LEDGER_NAME_MERZE, isnull(SUM(v.VOUCHER_CREDIT_AMOUNT-v.VOUCHER_DEBIT_AMOUNT),0) *-1,'" + gstrUserName + "' ";
                strSQL = strSQL + "FROM SMART0005.dbo.ACC_LEDGER_Z_D_A ZDA ,SMART0005.dbo.ACC_VOUCHER v,SMART0005.dbo.ACC_LEDGER L,SMART0005.dbo.ACC_COMPANY_VOUCHER c  WHERE L.LEDGER_NAME =ZDA.LEDGER_NAME AND V.LEDGER_NAME =L.LEDGER_NAME  and c.COMP_REF_NO =v.COMP_REF_NO ";
                strSQL = strSQL + "AND (V.COMP_VOUCHER_DATE <  ";
                strSQL = strSQL + " " + Utility.cvtSQLDateString(strFate) + ") ";
                strSQL = strSQL + "AND V.COMP_VOUCHER_TYPE=3 ";
                if (strBranchID != "")
                {
                    strSQL = strSQL + " AND L.BRANCH_ID ='" + strBranchID + "' ";
                }
                //strSQL = strSQL + "and c.SP_JOURNAL =0 ";
                strSQL = strSQL + "and c.SP_JOURNAL IN(0,2) ";//nEW Modified by Mayhedi 10_02_24
                strSQL = strSQL + "and v.VOUCHER_TOBY ='Cr' ";


                if (intstatus < 2)
                {
                    strSQL = strSQL + "AND l.LEDGER_STATUS =" + intstatus + " ";
                }
                else
                {
                    strSQL = strSQL + "AND l.LEDGER_STATUS in (0,1)";
                }
                if (intOption == 2)
                {
                    if (strLedger != "")
                    {
                        strSQL = strSQL + "AND ZDA.LEDGER_NAME_MERZE IN(  " + strLedger + " ) ";
                    }
                }
                else if (intOption == 3)
                {
                    if (strLedger != "")
                    {
                        strSQL = strSQL + "AND ZDA.AREA IN(" + strLedger + ") ";
                    }
                }
                else if (intOption == 4)
                {
                    if (strLedger != "")
                    {
                        strSQL = strSQL + "AND ZDA.DIVISION IN(" + strLedger + ") ";
                    }
                }
                else if (intOption == 5)
                {
                    if (strLedger != "")
                    {
                        strSQL = strSQL + "AND ZDA.ZONE IN(" + strLedger + ") ";
                    }
                }
                if (gstrUserName != "")
                {
                    strSQL = strSQL + " AND ZDA.DIVISION in( select LEDGER_GROUP_NAME from SMART0005.dbo.USER_PRIVILEGES_COLOR WHERE USER_LOGIN_NAME ='" + gstrUserName + "')";
                }
                strSQL = strSQL + " GROUP BY ZDA.ZONE, ZDA.DIVISION , ZDA.AREA ,l.TERRITORRY_NAME , l.TERITORRY_CODE,l.LEDGER_NAME_MERZE ";
                cmdInsert.CommandText = strSQL;
                cmdInsert.ExecuteNonQuery();

                //Cash
                strSQL = "INSERT INTO SMART0005.dbo.ACC_MARKET_MONITROING_SHEET(ZONE,DIVISION,AREA,TERITORRY_NAME,TERITORRY_CODE,MR_NAME,TOTAL_OS,USER_NAME) ";
                strSQL = strSQL + "select g.GR_PARENT AS zone, g.GR_NAME AS Division, l.LEDGER_PARENT_GROUP AS area,l.TERRITORRY_NAME,l.TERITORRY_CODE, l.LEDGER_NAME_MERZE, ";
                strSQL = strSQL + " abs(ISNULL(SUM(av.VOUCHER_CREDIT_AMOUNT),0)) *-1 COLL_CASH_TT,'" + gstrUserName + "' ";
                strSQL = strSQL + "from SMART0005.dbo.ACC_LEDGERGROUP g,SMART0005.dbo.ACC_LEDGERGROUP_CATEGORY_VIEW v,SMART0005.dbo.ACC_LEDGER l,ACC_COMPANY_VOUCHER c,SMART0005.dbo.ACC_VOUCHER av,SMART0005.dbo.ACC_LEDGER_Z_D_A  ";
                strSQL = strSQL + "where L.LEDGER_NAME =SMART0005.dbo.ACC_LEDGER_Z_D_A.LEDGER_NAME AND  g.GR_NAME=v.GR_PARENT  and v.GR_NAME=l.LEDGER_PARENT_GROUP and l.LEDGER_NAME =av.LEDGER_NAME  and c.COMP_REF_NO =av.COMP_REF_NO  ";
                strSQL = strSQL + "AND av.COMP_VOUCHER_TYPE=1 ";
                strSQL = strSQL + "AND C.COMP_VOUCHER_DATE < " + Utility.cvtSQLDateString(strFate) + " ";
                ////strSQL = strSQL + "AND l.BRANCH_ID ='" + strBranchID + "' ";
                if (strBranchID != "")
                {
                    strSQL = strSQL + "AND av.BRANCH_ID ='" + strBranchID + "' ";
                }

                if (intstatus < 2)
                {
                    strSQL = strSQL + "AND l.LEDGER_STATUS =" + intstatus + " ";
                }
                else
                {
                    strSQL = strSQL + "AND l.LEDGER_STATUS in (0,1)";
                }
                if (intOption == 2)
                {
                    if (strLedger != "")
                    {
                        strSQL = strSQL + "AND SMART0005.dbo.ACC_LEDGER_Z_D_A.LEDGER_NAME_MERZE IN( " + strLedger + " ) ";
                    }
                }
                else if (intOption == 3)
                {
                    if (strLedger != "")
                    {
                        strSQL = strSQL + "AND SMART0005.dbo.ACC_LEDGER_Z_D_A.AREA IN(" + strLedger + ") ";
                    }
                }
                else if (intOption == 4)
                {
                    if (strLedger != "")
                    {
                        strSQL = strSQL + "AND SMART0005.dbo.ACC_LEDGER_Z_D_A.DIVISION IN(" + strLedger + ") ";
                    }
                }
                else if (intOption == 5)
                {
                    if (strLedger != "")
                    {
                        strSQL = strSQL + "AND SMART0005.dbo.ACC_LEDGER_Z_D_A.ZONE IN(" + strLedger + ") ";
                    }
                }
                if (gstrUserName != "")
                {
                    strSQL = strSQL + " AND SMART0005.dbo.ACC_LEDGER_Z_D_A.DIVISION in( select LEDGER_GROUP_NAME from SMART0005.dbo.USER_PRIVILEGES_COLOR WHERE USER_LOGIN_NAME ='" + gstrUserName + "')";
                }
                strSQL = strSQL + "group by g.GR_PARENT , g.GR_NAME , l.LEDGER_PARENT_GROUP,l.LEDGER_NAME_MERZE,l.TERITORRY_CODE,l.TERRITORRY_NAME ";
                cmdInsert.CommandText = strSQL;
                cmdInsert.ExecuteNonQuery();
                //SP Journal
                strSQL = "INSERT INTO SMART0005.dbo.ACC_MARKET_MONITROING_SHEET(ZONE,DIVISION,AREA,TERITORRY_NAME,TERITORRY_CODE,MR_NAME,TOTAL_OS,USER_NAME) ";
                strSQL = strSQL + "select g.GR_PARENT AS zone, g.GR_NAME AS Division, l.LEDGER_PARENT_GROUP AS area,l.TERRITORRY_NAME ,l.TERITORRY_CODE,l.LEDGER_NAME_MERZE,";
                strSQL = strSQL + "ABS(ISNULL(SUM(aV.VOUCHER_CREDIT_AMOUNT-aV.VOUCHER_DEBIT_AMOUNT),0)) *-1 COLL_VOUCHER,'" + gstrUserName + "'   ";
                strSQL = strSQL + "from SMART0005.dbo.ACC_LEDGERGROUP g,SMART0005.dbo.ACC_LEDGERGROUP_CATEGORY_VIEW v,SMART0005.dbo.ACC_LEDGER l,SMART0005.dbo.ACC_COMPANY_VOUCHER c,SMART0005.dbo.ACC_VOUCHER av,SMART0005.dbo.ACC_LEDGER_Z_D_A ";
                strSQL = strSQL + "where L.LEDGER_NAME =SMART0005.dbo.ACC_LEDGER_Z_D_A.LEDGER_NAME AND g.GR_NAME=v.GR_PARENT  and v.GR_NAME=l.LEDGER_PARENT_GROUP and l.LEDGER_NAME =av.LEDGER_NAME  and c.COMP_REF_NO =av.COMP_REF_NO ";
                strSQL = strSQL + "AND aV.COMP_VOUCHER_TYPE =3 and c.SP_JOURNAL=1 ";
                strSQL = strSQL + "AND C.COMP_VOUCHER_DATE < " + Utility.cvtSQLDateString(strFate) + " ";
                if (strBranchID != "")
                {
                    strSQL = strSQL + "AND C.BRANCH_ID ='" + strBranchID + "' ";
                }
                strSQL = strSQL + " AND  C.AUTOJV =0 ";

                if (intstatus < 2)
                {
                    strSQL = strSQL + "AND l.LEDGER_STATUS =" + intstatus + " ";
                }
                else
                {
                    strSQL = strSQL + "AND l.LEDGER_STATUS in (0,1)";
                }
                if (intOption == 2)
                {
                    if (strLedger != "")
                    {
                        strSQL = strSQL + "AND SMART0005.dbo.ACC_LEDGER_Z_D_A.LEDGER_NAME_MERZE IN(" + strLedger + ") ";
                    }
                }
                else if (intOption == 3)
                {
                    if (strLedger != "")
                    {
                        strSQL = strSQL + "AND SMART0005.dbo.ACC_LEDGER_Z_D_A.AREA IN(" + strLedger + ") ";
                    }
                }
                else if (intOption == 4)
                {
                    if (strLedger != "")
                    {
                        strSQL = strSQL + "AND SMART0005.dbo.ACC_LEDGER_Z_D_A.DIVISION IN(" + strLedger + ") ";
                    }
                }
                else if (intOption == 5)
                {
                    if (strLedger != "")
                    {
                        strSQL = strSQL + "AND SMART0005.dbo.ACC_LEDGER_Z_D_A.ZONE IN(" + strLedger + ") ";
                    }
                }
                if (gstrUserName != "")
                {
                    strSQL = strSQL + " AND SMART0005.dbo.ACC_LEDGER_Z_D_A.DIVISION in( select LEDGER_GROUP_NAME from USER_PRIVILEGES_COLOR WHERE USER_LOGIN_NAME ='" + gstrUserName + "')";
                }
                strSQL = strSQL + "group by g.GR_PARENT , g.GR_NAME , l.LEDGER_PARENT_GROUP,l.LEDGER_NAME_MERZE,l.TERITORRY_CODE,l.TERRITORRY_NAME ";
                cmdInsert.CommandText = strSQL;
                cmdInsert.ExecuteNonQuery();

                //New minus pf HL 17_07_20

                strSQL = "INSERT INTO SMART0005.dbo.ACC_MARKET_MONITROING_SHEET(ZONE,DIVISION,AREA,TERITORRY_NAME,TERITORRY_CODE,MR_NAME,TOTAL_OS,USER_NAME) ";
                strSQL = strSQL + "SELECT Z.ZONE,Z.DIVISION,Z.AREA,Z.TERRITORRY_NAME,Z.TERITORRY_CODE,  Z.LEDGER_NAME_MERZE ,ISNULL(SUM(v.VOUCHER_DEBIT_AMOUNT-V.VOUCHER_CREDIT_AMOUNT),0) *-1 ";
                strSQL = strSQL + " ,'" + gstrUserName + "' FROM SMART0005.dbo.ACC_LEDGER_Z_D_A Z,ACC_VOUCHER V WHERE Z.LEDGER_NAME=V.REVERSE_LEDGER1  AND V.COMP_VOUCHER_TYPE=3 ";
                strSQL = strSQL + "AND V.COMP_VOUCHER_DATE < " + Utility.cvtSQLDateString(strFate) + " ";
                if (strBranchID != "")
                {
                    strSQL = strSQL + "AND V.BRANCH_ID ='" + strBranchID + "' ";
                }
                strSQL = strSQL + " AND  V.AUTOJV=1";


                if (intstatus < 2)
                {
                    strSQL = strSQL + "AND Z.LEDGER_STATUS =" + intstatus + " ";
                }
                else
                {
                    strSQL = strSQL + "AND Z.LEDGER_STATUS in (0,1)";
                }
                if (intOption == 2)
                {
                    if (strLedger != "")
                    {
                        strSQL = strSQL + "AND Z.LEDGER_NAME_MERZE IN(" + strLedger + ") ";
                    }
                }
                else if (intOption == 3)
                {
                    if (strLedger != "")
                    {
                        strSQL = strSQL + "AND Z.AREA IN(" + strLedger + ") ";
                    }
                }
                else if (intOption == 4)
                {
                    if (strLedger != "")
                    {
                        strSQL = strSQL + "AND Z.DIVISION IN(" + strLedger + ") ";
                    }
                }
                else if (intOption == 5)
                {
                    if (strLedger != "")
                    {
                        strSQL = strSQL + "AND Z.ZONE IN(" + strLedger + ") ";
                    }
                }
                if (gstrUserName != "")
                {
                    strSQL = strSQL + " AND Z.DIVISION in( select LEDGER_GROUP_NAME from USER_PRIVILEGES_COLOR WHERE USER_LOGIN_NAME ='" + gstrUserName + "')";
                }
                strSQL = strSQL + "GROUP by  Z.ZONE,Z.DIVISION,Z.AREA ,Z.LEDGER_NAME,Z.TERRITORRY_NAME,Z.TERITORRY_CODE, Z.LEDGER_NAME_MERZE ";
                cmdInsert.CommandText = strSQL;
                cmdInsert.ExecuteNonQuery();


                //**************cOLLECTION
                if (intBaseTarget == 1)
                {
                    strSQL = "INSERT INTO SMART0005.dbo.ACC_MARKET_MONITROING_SHEET(ZONE,DIVISION,AREA,TERITORRY_NAME,TERITORRY_CODE,MR_NAME,COMMITMENT,USER_NAME) ";
                    strSQL = strSQL + " select g.GR_PARENT AS zone, g.GR_NAME AS Division, l.LEDGER_PARENT_GROUP AS area,l.TERRITORRY_NAME , l.TERITORRY_CODE,l.LEDGER_NAME_MERZE,";
                    strSQL = strSQL + " ISNULL(SUM(C.COLL_TARGET_COLL_AMT),0) COMMITMENT,'" + gstrUserName + "'   ";
                    ////strSQL = strSQL + " ( sum(C.COLL_TARGET_COLL_AMT)/" + intDay + ")* " + Convert.ToInt32(intUpdDay) + " COMMITMENT    ";
                    strSQL = strSQL + " from SMART0005.dbo.ACC_LEDGERGROUP g,SMART0005.dbo.ACC_LEDGERGROUP_CATEGORY_VIEW v,SMART0005.dbo.ACC_LEDGER l,SMART0005.dbo.SALES_COLL_TARGET_MASTER m,SMART0005.dbo.SALES_COLL_TARGET_TRAN c,SMART0005.dbo.ACC_LEDGER_Z_D_A ";
                    strSQL = strSQL + " where L.LEDGER_NAME =SMART0005.dbo.ACC_LEDGER_Z_D_A.LEDGER_NAME AND g.GR_NAME=v.GR_PARENT  and v.GR_NAME=l.LEDGER_PARENT_GROUP and l.LEDGER_NAME =c.LEDGER_NAME  and m.COLL_TARGET_KEY=c.COLL_TARGET_KEY ";
                    strSQL = strSQL + " AND Upper(c.MONTH_ID) in (" + strString + ")";

                    if (strBranchID != "")
                    {
                        strSQL = strSQL + " AND m.BRANCH_ID ='" + strBranchID + "' ";
                    }
                    if (intstatus < 2)
                    {
                        strSQL = strSQL + "AND l.LEDGER_STATUS =" + intstatus + " ";
                    }
                    if (intOption == 2)
                    {
                        if (strLedger != "")
                        {
                            strSQL = strSQL + "AND SMART0005.dbo.ACC_LEDGER_Z_D_A.LEDGER_NAME_MERZE IN(" + strLedger + ") ";
                        }
                    }
                    else if (intOption == 3)
                    {
                        if (strLedger != "")
                        {
                            strSQL = strSQL + "AND SMART0005.dbo.ACC_LEDGER_Z_D_A.AREA IN(" + strLedger + ") ";
                        }
                    }
                    else if (intOption == 4)
                    {
                        if (strLedger != "")
                        {
                            strSQL = strSQL + "AND SMART0005.dbo.ACC_LEDGER_Z_D_A.DIVISION IN(" + strLedger + ") ";
                        }
                    }
                    else if (intOption == 5)
                    {
                        if (strLedger != "")
                        {
                            strSQL = strSQL + "AND SMART0005.dbo.ACC_LEDGER_Z_D_A.ZONE IN(" + strLedger + ") ";
                        }
                    }
                    if (gstrUserName != "")
                    {
                        strSQL = strSQL + " AND SMART0005.dbo.ACC_LEDGER_Z_D_A.DIVISION in( select LEDGER_GROUP_NAME from SMART0005.dbo.USER_PRIVILEGES_COLOR WHERE USER_LOGIN_NAME ='" + gstrUserName + "')";
                    }
                    strSQL = strSQL + " group by g.GR_PARENT , g.GR_NAME , l.LEDGER_PARENT_GROUP,l.LEDGER_NAME_MERZE,l.TERITORRY_CODE,l.TERRITORRY_NAME ";
                    //strSQL = strSQL + " having sum(COLL_TARGET_COLL_AMT) > 0 ";
                    cmdInsert.CommandText = strSQL;
                    cmdInsert.ExecuteNonQuery();
                }
                else
                {
                    strSQL = "INSERT INTO SMART0005.dbo.ACC_MARKET_MONITROING_SHEET(ZONE,DIVISION,AREA,TERITORRY_NAME,TERITORRY_CODE,MR_NAME,COMMITMENT,USER_NAME) ";
                    strSQL = strSQL + " select g.GR_PARENT AS zone, g.GR_NAME AS Division, l.LEDGER_PARENT_GROUP AS area,l.TERRITORRY_NAME , l.TERITORRY_CODE,l.LEDGER_NAME_MERZE,";
                    strSQL = strSQL + " ISNULL(SUM(C.TARGET_CHANGE_AMNT),0) COMMITMENT,'" + gstrUserName + "'   ";
                    //strSQL = strSQL + " ( sum(C.TARGET_CHANGE_AMNT)/" + intDay + ")* " + Convert.ToInt32(intUpdDay) + " COMMITMENT    ";
                    strSQL = strSQL + " from SMART0005.dbo.ACC_LEDGERGROUP g,SMART0005.dbo.ACC_LEDGERGROUP_CATEGORY_VIEW v,SMART0005.dbo.ACC_LEDGER l,SMART0005.dbo.SALES_COLL_TARGET_MASTER m,SMART0005.dbo.SALES_COLL_TARGET_TRAN c,SMART0005.dbo.ACC_LEDGER_Z_D_A ";
                    strSQL = strSQL + " where L.LEDGER_NAME =SMART0005.dbo.ACC_LEDGER_Z_D_A.LEDGER_NAME AND g.GR_NAME=v.GR_PARENT  and v.GR_NAME=l.LEDGER_PARENT_GROUP and l.LEDGER_NAME =c.LEDGER_NAME  and m.COLL_TARGET_KEY=c.COLL_TARGET_KEY ";
                    strSQL = strSQL + " AND Upper(c.MONTH_ID) in (" + strString + ")";

                    if (strBranchID != "")
                    {
                        strSQL = strSQL + " AND m.BRANCH_ID ='" + strBranchID + "' ";
                    }
                    if (intstatus < 2)
                    {
                        strSQL = strSQL + "AND l.LEDGER_STATUS =" + intstatus + " ";
                    }
                    if (intOption == 2)
                    {
                        if (strLedger != "")
                        {
                            strSQL = strSQL + "AND SMART0005.dbo.ACC_LEDGER_Z_D_A.LEDGER_NAME_MERZE IN(" + strLedger + ") ";
                        }
                    }
                    else if (intOption == 3)
                    {
                        if (strLedger != "")
                        {
                            strSQL = strSQL + "AND SMART0005.dbo.ACC_LEDGER_Z_D_A.AREA IN(" + strLedger + ") ";
                        }
                    }
                    else if (intOption == 4)
                    {
                        if (strLedger != "")
                        {
                            strSQL = strSQL + "AND SMART0005.dbo.ACC_LEDGER_Z_D_A.DIVISION IN(" + strLedger + ") ";
                        }
                    }
                    else if (intOption == 5)
                    {
                        if (strLedger != "")
                        {
                            strSQL = strSQL + "AND SMART0005.dbo.ACC_LEDGER_Z_D_A.ZONE IN(" + strLedger + ") ";
                        }
                    }
                    if (gstrUserName != "")
                    {
                        strSQL = strSQL + " AND SMART0005.dbo.ACC_LEDGER_Z_D_A.DIVISION in( select LEDGER_GROUP_NAME from SMART0005.dbo.USER_PRIVILEGES_COLOR WHERE USER_LOGIN_NAME ='" + gstrUserName + "')";
                    }
                    strSQL = strSQL + " group by g.GR_PARENT , g.GR_NAME , l.LEDGER_PARENT_GROUP,l.LEDGER_NAME_MERZE,l.TERITORRY_CODE,l.TERRITORRY_NAME ";
                    //strSQL = strSQL + " having sum(COLL_TARGET_COLL_AMT) > 0 ";
                    cmdInsert.CommandText = strSQL;
                    cmdInsert.ExecuteNonQuery();
                }

                //commit

                //*************************

                //collect amnt
                strSQL = "INSERT INTO SMART0005.dbo.ACC_MARKET_MONITROING_SHEET(ZONE,DIVISION,AREA,TERITORRY_NAME,TERITORRY_CODE,MR_NAME,COLLECTION_AMNT,USER_NAME) ";
                strSQL = strSQL + " select g.GR_PARENT AS zone, g.GR_NAME AS Division, l.LEDGER_PARENT_GROUP AS area,l.TERRITORRY_NAME ,l.TERITORRY_CODE,l.LEDGER_NAME_MERZE, ";
                strSQL = strSQL + " ISNULL(SUM(AV.VOUCHER_CREDIT_AMOUNT),0) COLLECTION_AMNT,'" + gstrUserName + "'   ";
                strSQL = strSQL + " from SMART0005.dbo.ACC_LEDGERGROUP g,SMART0005.dbo.ACC_LEDGERGROUP_CATEGORY_VIEW v,SMART0005.dbo.ACC_LEDGER l,SMART0005.dbo.ACC_COMPANY_VOUCHER c,SMART0005.dbo.ACC_VOUCHER av,SMART0005.dbo.ACC_LEDGER_Z_D_A ";
                strSQL = strSQL + " where L.LEDGER_NAME =SMART0005.dbo.ACC_LEDGER_Z_D_A.LEDGER_NAME AND g.GR_NAME=v.GR_PARENT  and v.GR_NAME=l.LEDGER_PARENT_GROUP and l.LEDGER_NAME =av.LEDGER_NAME  and c.COMP_REF_NO =av.COMP_REF_NO ";
                strSQL = strSQL + " AND aV.COMP_VOUCHER_TYPE =1 and C.COMP_VOUCHER_NET_AMOUNT > 0 ";
                strSQL = strSQL + " AND C.COMP_VOUCHER_DATE BETWEEN " + Utility.cvtSQLDateString(strFate) + " ";
                strSQL = strSQL + " AND " + Utility.cvtSQLDateString(strTDate);
                if (strBranchID != "")
                {
                    //strSQL = strSQL + " AND l.BRANCH_ID ='" + strBranchID + "' ";
                    strSQL = strSQL + " AND aV.BRANCH_ID ='" + strBranchID + "' ";
                }
                if (intstatus < 2)
                {
                    strSQL = strSQL + "AND l.LEDGER_STATUS =" + intstatus + " ";
                }
                if (intOption == 2)
                {
                    if (strLedger != "")
                    {
                        strSQL = strSQL + "AND SMART0005.dbo.ACC_LEDGER_Z_D_A.LEDGER_NAME_MERZE IN(" + strLedger + ") ";
                    }
                }
                else if (intOption == 3)
                {
                    if (strLedger != "")
                    {
                        strSQL = strSQL + "AND SMART0005.dbo.ACC_LEDGER_Z_D_A.AREA IN(" + strLedger + ") ";
                    }
                }
                else if (intOption == 4)
                {
                    if (strLedger != "")
                    {
                        strSQL = strSQL + "AND SMART0005.dbo.ACC_LEDGER_Z_D_A.DIVISION IN(" + strLedger + ") ";
                    }
                }
                else if (intOption == 5)
                {
                    if (strLedger != "")
                    {
                        strSQL = strSQL + "AND SMART0005.dbo.ACC_LEDGER_Z_D_A.ZONE IN(" + strLedger + ") ";
                    }
                }
                if (gstrUserName != "")
                {
                    strSQL = strSQL + " AND SMART0005.dbo.ACC_LEDGER_Z_D_A.DIVISION in( select LEDGER_GROUP_NAME from SMART0005.dbo.USER_PRIVILEGES_COLOR WHERE USER_LOGIN_NAME ='" + gstrUserName + "')";
                }
                strSQL = strSQL + " group by g.GR_PARENT , g.GR_NAME , l.LEDGER_PARENT_GROUP,l.LEDGER_NAME_MERZE,l.TERITORRY_CODE,l.TERRITORRY_NAME ";
                cmdInsert.CommandText = strSQL;
                cmdInsert.ExecuteNonQuery();
                //**********hlpf
                strSQL = "INSERT INTO SMART0005.dbo.ACC_MARKET_MONITROING_SHEET(ZONE,DIVISION,AREA,TERITORRY_NAME,TERITORRY_CODE,MR_NAME,COLLECTION_AMNT,USER_NAME) ";
                strSQL = strSQL + "SELECT Z.ZONE,Z.DIVISION,Z.AREA,Z.TERRITORRY_NAME,Z.TERITORRY_CODE,  Z.LEDGER_NAME_MERZE ,ISNULL(SUM(V.VOUCHER_CREDIT_AMOUNT-v.VOUCHER_DEBIT_AMOUNT),0) *-1,'" + gstrUserName + "' ";
                strSQL = strSQL + " FROM SMART0005.dbo.ACC_LEDGER_Z_D_A Z,SMART0005.dbo.ACC_VOUCHER V WHERE Z.LEDGER_NAME=V.REVERSE_LEDGER1  AND V.COMP_VOUCHER_TYPE=3 ";
                strSQL = strSQL + "AND V.COMP_VOUCHER_DATE BETWEEN " + Utility.cvtSQLDateString(strFate) + " ";
                strSQL = strSQL + " AND " + Utility.cvtSQLDateString(strTDate);
                if (strBranchID != "")
                {
                    strSQL = strSQL + " AND V.BRANCH_ID ='" + strBranchID + "' ";
                }
                strSQL = strSQL + " AND  V.AUTOJV=1";
                if (intstatus < 2)
                {
                    strSQL = strSQL + "AND Z.LEDGER_STATUS =" + intstatus + " ";
                }
                if (intOption == 2)
                {
                    if (strLedger != "")
                    {
                        strSQL = strSQL + "AND Z.LEDGER_NAME_MERZE IN(" + strLedger + ") ";
                    }
                }
                else if (intOption == 3)
                {
                    if (strLedger != "")
                    {
                        strSQL = strSQL + "AND Z.AREA IN(" + strLedger + ") ";
                    }
                }
                else if (intOption == 4)
                {
                    if (strLedger != "")
                    {
                        strSQL = strSQL + "AND Z.DIVISION IN(" + strLedger + ") ";
                    }
                }
                else if (intOption == 5)
                {
                    if (strLedger != "")
                    {
                        strSQL = strSQL + "AND Z.ZONE IN(" + strLedger + ") ";
                    }
                }
                if (gstrUserName != "")
                {
                    strSQL = strSQL + " AND Z.DIVISION in( select LEDGER_GROUP_NAME from SMART0005.dbo.USER_PRIVILEGES_COLOR WHERE USER_LOGIN_NAME ='" + gstrUserName + "')";
                }
                strSQL = strSQL + "GROUP by  Z.ZONE,Z.DIVISION,Z.AREA ,Z.LEDGER_NAME,Z.TERRITORRY_NAME,Z.TERITORRY_CODE, Z.LEDGER_NAME_MERZE ";
                cmdInsert.CommandText = strSQL;
                cmdInsert.ExecuteNonQuery();
                //******************
                //SP
                strSQL = "INSERT INTO SMART0005.dbo.ACC_MARKET_MONITROING_SHEET(ZONE,DIVISION,AREA,TERITORRY_NAME,TERITORRY_CODE,MR_NAME,SP_VOUCHER,USER_NAME) ";
                strSQL = strSQL + "select g.GR_PARENT AS zone, g.GR_NAME AS Division, l.LEDGER_PARENT_GROUP AS area,l.TERRITORRY_NAME ,l.TERITORRY_CODE,l.LEDGER_NAME_MERZE,";
                strSQL = strSQL + "ABS(ISNULL(SUM(aV.VOUCHER_CREDIT_AMOUNT-aV.VOUCHER_DEBIT_AMOUNT),0)) COLL_VOUCHER,'" + gstrUserName + "'   ";
                strSQL = strSQL + "from SMART0005.dbo.ACC_LEDGERGROUP g,SMART0005.dbo.ACC_LEDGERGROUP_CATEGORY_VIEW v,SMART0005.dbo.ACC_LEDGER l,SMART0005.dbo.ACC_COMPANY_VOUCHER c,SMART0005.dbo.ACC_VOUCHER av,SMART0005.dbo.ACC_LEDGER_Z_D_A ";
                strSQL = strSQL + "where L.LEDGER_NAME =SMART0005.dbo.ACC_LEDGER_Z_D_A.LEDGER_NAME AND  g.GR_NAME=v.GR_PARENT  and v.GR_NAME=l.LEDGER_PARENT_GROUP and l.LEDGER_NAME =av.LEDGER_NAME  and c.COMP_REF_NO =av.COMP_REF_NO ";
                strSQL = strSQL + "AND aV.COMP_VOUCHER_TYPE =3 and c.SP_JOURNAL=1 ";
                //and c.SP_JOURNAL=1
                strSQL = strSQL + "AND C.COMP_VOUCHER_DATE  BETWEEN " + Utility.cvtSQLDateString(strFate) + " ";
                strSQL = strSQL + " AND " + Utility.cvtSQLDateString(strTDate);
                if (strBranchID != "")
                {
                    strSQL = strSQL + " AND C.BRANCH_ID ='" + strBranchID + "' ";
                }
                strSQL = strSQL + " AND  C.AUTOJV =0 ";
                if (intstatus < 2)
                {
                    strSQL = strSQL + "AND l.LEDGER_STATUS =" + intstatus + " ";
                }
                if (intOption == 2)
                {
                    if (strLedger != "")
                    {
                        strSQL = strSQL + "AND SMART0005.dbo.ACC_LEDGER_Z_D_A.LEDGER_NAME_MERZE IN(" + strLedger + ") ";
                    }
                }
                else if (intOption == 3)
                {
                    if (strLedger != "")
                    {
                        strSQL = strSQL + "AND SMART0005.dbo.ACC_LEDGER_Z_D_A.AREA IN(" + strLedger + ") ";
                    }
                }
                else if (intOption == 4)
                {
                    if (strLedger != "")
                    {
                        strSQL = strSQL + "AND SMART0005.dbo.ACC_LEDGER_Z_D_A.DIVISION IN(" + strLedger + ") ";
                    }
                }
                else if (intOption == 5)
                {
                    if (strLedger != "")
                    {
                        strSQL = strSQL + "AND SMART0005.dbo.ACC_LEDGER_Z_D_A.ZONE IN(" + strLedger + ") ";
                    }
                }
                if (gstrUserName != "")
                {
                    strSQL = strSQL + " AND SMART0005.dbo.ACC_LEDGER_Z_D_A.DIVISION in( select LEDGER_GROUP_NAME from SMART0005.dbo.USER_PRIVILEGES_COLOR WHERE USER_LOGIN_NAME ='" + gstrUserName + "')";
                }
                strSQL = strSQL + "group by g.GR_PARENT , g.GR_NAME , l.LEDGER_PARENT_GROUP,l.LEDGER_NAME_MERZE,l.TERITORRY_CODE,l.TERRITORRY_NAME ";
                cmdInsert.CommandText = strSQL;
                cmdInsert.ExecuteNonQuery();
                strSQL = "INSERT INTO SMART0005.dbo.ACC_MARKET_MONITROING_SHEET(ZONE,DIVISION,AREA,TERITORRY_NAME,TERITORRY_CODE,MR_NAME,JV_DEBIT,USER_NAME) ";
                strSQL = strSQL + "select g.GR_PARENT AS zone, g.GR_NAME AS Division, l.LEDGER_PARENT_GROUP AS area,l.TERRITORRY_NAME ,l.TERITORRY_CODE,l.LEDGER_NAME_MERZE,";
                strSQL = strSQL + "ABS(ISNULL(SUM(aV.VOUCHER_DEBIT_AMOUNT),0))  COLL_VOUCHER,'" + gstrUserName + "'   ";
                strSQL = strSQL + "from SMART0005.dbo.ACC_LEDGERGROUP g,SMART0005.dbo.ACC_LEDGERGROUP_CATEGORY_VIEW v,SMART0005.dbo.ACC_LEDGER l,SMART0005.dbo.ACC_COMPANY_VOUCHER c,SMART0005.dbo.ACC_VOUCHER av,SMART0005.dbo.ACC_LEDGER_Z_D_A ";
                strSQL = strSQL + "where L.LEDGER_NAME =SMART0005.dbo.ACC_LEDGER_Z_D_A.LEDGER_NAME AND g.GR_NAME=v.GR_PARENT  and v.GR_NAME=l.LEDGER_PARENT_GROUP and l.LEDGER_NAME =av.LEDGER_NAME  and c.COMP_REF_NO =av.COMP_REF_NO ";
                strSQL = strSQL + "AND aV.COMP_VOUCHER_TYPE =3 and c.SP_JOURNAL=0 ";
                //and c.SP_JOURNAL=1
                strSQL = strSQL + "AND C.COMP_VOUCHER_DATE  BETWEEN " + Utility.cvtSQLDateString(strFate) + " ";
                strSQL = strSQL + " AND " + Utility.cvtSQLDateString(strTDate);
                if (strBranchID != "")
                {
                    strSQL = strSQL + " AND C.BRANCH_ID ='" + strBranchID + "' ";
                }
                strSQL = strSQL + " AND  C.AUTOJV =0 ";
                if (intstatus < 2)
                {
                    strSQL = strSQL + "AND l.LEDGER_STATUS =" + intstatus + " ";
                }
                if (intOption == 2)
                {
                    if (strLedger != "")
                    {
                        strSQL = strSQL + "AND SMART0005.dbo.ACC_LEDGER_Z_D_A.LEDGER_NAME_MERZE IN(" + strLedger + ") ";
                    }
                }
                else if (intOption == 3)
                {
                    if (strLedger != "")
                    {
                        strSQL = strSQL + "AND SMART0005.dbo.ACC_LEDGER_Z_D_A.AREA IN(" + strLedger + ") ";
                    }
                }
                else if (intOption == 4)
                {
                    if (strLedger != "")
                    {
                        strSQL = strSQL + "AND SMART0005.dbo.ACC_LEDGER_Z_D_A.DIVISION IN(" + strLedger + ") ";
                    }
                }
                else if (intOption == 5)
                {
                    if (strLedger != "")
                    {
                        strSQL = strSQL + "AND SMART0005.dbo.ACC_LEDGER_Z_D_A.ZONE IN(" + strLedger + ") ";
                    }
                }
                if (gstrUserName != "")
                {
                    strSQL = strSQL + " AND SMART0005.dbo.ACC_LEDGER_Z_D_A.DIVISION in( select LEDGER_GROUP_NAME from SMART0005.dbo.USER_PRIVILEGES_COLOR WHERE USER_LOGIN_NAME ='" + gstrUserName + "')";
                }
                strSQL = strSQL + "group by g.GR_PARENT , g.GR_NAME , l.LEDGER_PARENT_GROUP,l.LEDGER_NAME_MERZE,l.TERITORRY_CODE,l.TERRITORRY_NAME ";
                cmdInsert.CommandText = strSQL;
                cmdInsert.ExecuteNonQuery();
                strSQL = "INSERT INTO SMART0005.dbo.ACC_MARKET_MONITROING_SHEET(ZONE,DIVISION,AREA,TERITORRY_NAME,TERITORRY_CODE,MR_NAME,JV_CREDIT,USER_NAME) ";
                strSQL = strSQL + "select g.GR_PARENT AS zone, g.GR_NAME AS Division, l.LEDGER_PARENT_GROUP AS area,l.TERRITORRY_NAME ,l.TERITORRY_CODE,l.LEDGER_NAME_MERZE,";
                strSQL = strSQL + "ABS(ISNULL(SUM(aV.VOUCHER_CREDIT_AMOUNT),0))  COLL_VOUCHER,'" + gstrUserName + "'   ";
                strSQL = strSQL + "from SMART0005.dbo.ACC_LEDGERGROUP g,SMART0005.dbo.ACC_LEDGERGROUP_CATEGORY_VIEW v,SMART0005.dbo.ACC_LEDGER l,SMART0005.dbo.ACC_COMPANY_VOUCHER c,SMART0005.dbo.ACC_VOUCHER av,SMART0005.dbo.ACC_LEDGER_Z_D_A ";
                strSQL = strSQL + "where L.LEDGER_NAME =SMART0005.dbo.ACC_LEDGER_Z_D_A.LEDGER_NAME AND g.GR_NAME=v.GR_PARENT  and v.GR_NAME=l.LEDGER_PARENT_GROUP and l.LEDGER_NAME =av.LEDGER_NAME  and c.COMP_REF_NO =av.COMP_REF_NO ";
                strSQL = strSQL + "AND aV.COMP_VOUCHER_TYPE =3 and c.SP_JOURNAL=0 ";
                //and c.SP_JOURNAL=1
                strSQL = strSQL + "AND C.COMP_VOUCHER_DATE  BETWEEN " + Utility.cvtSQLDateString(strFate) + " ";
                strSQL = strSQL + " AND " + Utility.cvtSQLDateString(strTDate);
                if (strBranchID != "")
                {
                    strSQL = strSQL + " AND C.BRANCH_ID ='" + strBranchID + "' ";
                }
                strSQL = strSQL + " AND  C.AUTOJV =0 ";
                if (intstatus < 2)
                {
                    strSQL = strSQL + "AND l.LEDGER_STATUS =" + intstatus + " ";
                }
                if (intOption == 2)
                {
                    if (strLedger != "")
                    {
                        strSQL = strSQL + "AND SMART0005.dbo.ACC_LEDGER_Z_D_A.LEDGER_NAME_MERZE IN(" + strLedger + ") ";
                    }
                }
                else if (intOption == 3)
                {
                    if (strLedger != "")
                    {
                        strSQL = strSQL + "AND SMART0005.dbo.ACC_LEDGER_Z_D_A.AREA IN(" + strLedger + ") ";
                    }
                }
                else if (intOption == 4)
                {
                    if (strLedger != "")
                    {
                        strSQL = strSQL + "AND SMART0005.dbo.ACC_LEDGER_Z_D_A.DIVISION IN(" + strLedger + ") ";
                    }
                }
                else if (intOption == 5)
                {
                    if (strLedger != "")
                    {
                        strSQL = strSQL + "AND SMART0005.dbo.ACC_LEDGER_Z_D_A.ZONE IN(" + strLedger + ") ";
                    }
                }
                if (gstrUserName != "")
                {
                    strSQL = strSQL + " AND SMART0005.dbo.ACC_LEDGER_Z_D_A.DIVISION in( select LEDGER_GROUP_NAME from SMART0005.dbo.USER_PRIVILEGES_COLOR WHERE USER_LOGIN_NAME ='" + gstrUserName + "')";
                }
                strSQL = strSQL + "group by g.GR_PARENT , g.GR_NAME , l.LEDGER_PARENT_GROUP,l.LEDGER_NAME_MERZE,l.TERITORRY_CODE,l.TERRITORRY_NAME ";
                cmdInsert.CommandText = strSQL;
                cmdInsert.ExecuteNonQuery();

                strSQL = "INSERT INTO SMART0005.dbo.ACC_MARKET_MONITROING_SHEET(ZONE,DIVISION,AREA,TERITORRY_NAME,TERITORRY_CODE,MR_NAME,SALES_RETURN,USER_NAME) ";
                strSQL = strSQL + "select g.GR_PARENT AS zone, g.GR_NAME AS Division, l.LEDGER_PARENT_GROUP AS area,l.TERRITORRY_NAME ,l.TERITORRY_CODE,l.LEDGER_NAME_MERZE,";
                strSQL = strSQL + "ABS(ISNULL(SUM(aV.VOUCHER_CREDIT_AMOUNT-aV.VOUCHER_DEBIT_AMOUNT),0))  COLL_VOUCHER,'" + gstrUserName + "'   ";
                strSQL = strSQL + "from SMART0005.dbo.ACC_LEDGERGROUP g,SMART0005.dbo.ACC_LEDGERGROUP_CATEGORY_VIEW v,SMART0005.dbo.ACC_LEDGER l,SMART0005.dbo.ACC_COMPANY_VOUCHER c,SMART0005.dbo.ACC_VOUCHER av,SMART0005.dbo.ACC_LEDGER_Z_D_A ";
                strSQL = strSQL + "where L.LEDGER_NAME =SMART0005.dbo.ACC_LEDGER_Z_D_A.LEDGER_NAME AND g.GR_NAME=v.GR_PARENT  and v.GR_NAME=l.LEDGER_PARENT_GROUP and l.LEDGER_NAME =av.LEDGER_NAME  and c.COMP_REF_NO =av.COMP_REF_NO ";
                strSQL = strSQL + "AND aV.COMP_VOUCHER_TYPE =13 ";
                //and c.SP_JOURNAL=1
                strSQL = strSQL + "AND C.COMP_VOUCHER_DATE  BETWEEN " + Utility.cvtSQLDateString(strFate) + " ";
                strSQL = strSQL + " AND " + Utility.cvtSQLDateString(strTDate);
                if (strBranchID != "")
                {
                    strSQL = strSQL + " AND C.BRANCH_ID ='" + strBranchID + "' ";
                }
                strSQL = strSQL + " AND  C.AUTOJV =0 ";
                if (intstatus < 2)
                {
                    strSQL = strSQL + "AND l.LEDGER_STATUS =" + intstatus + " ";
                }
                if (intOption == 2)
                {
                    if (strLedger != "")
                    {
                        strSQL = strSQL + "AND SMART0005.dbo.ACC_LEDGER_Z_D_A.LEDGER_NAME_MERZE IN(" + strLedger + ") ";
                    }
                }
                else if (intOption == 3)
                {
                    if (strLedger != "")
                    {
                        strSQL = strSQL + "AND SMART0005.dbo.ACC_LEDGER_Z_D_A.AREA IN(" + strLedger + ") ";
                    }
                }
                else if (intOption == 4)
                {
                    if (strLedger != "")
                    {
                        strSQL = strSQL + "AND SMART0005.dbo.ACC_LEDGER_Z_D_A.DIVISION IN(" + strLedger + ") ";
                    }
                }
                else if (intOption == 5)
                {
                    if (strLedger != "")
                    {
                        strSQL = strSQL + "AND SMART0005.dbo.ACC_LEDGER_Z_D_A.ZONE IN(" + strLedger + ") ";
                    }
                }
                if (gstrUserName != "")
                {
                    strSQL = strSQL + " AND SMART0005.dbo.ACC_LEDGER_Z_D_A.DIVISION in( select LEDGER_GROUP_NAME from SMART0005.dbo.USER_PRIVILEGES_COLOR WHERE USER_LOGIN_NAME ='" + gstrUserName + "')";
                }
                strSQL = strSQL + "group by g.GR_PARENT , g.GR_NAME , l.LEDGER_PARENT_GROUP,l.LEDGER_NAME_MERZE,l.TERITORRY_CODE,l.TERRITORRY_NAME ";
                cmdInsert.CommandText = strSQL;
                cmdInsert.ExecuteNonQuery();
                //sales target
                if (intBaseTarget == 1)
                {


                    strSQL = "INSERT INTO SMART0005.dbo.ACC_MARKET_MONITROING_SHEET(ZONE,DIVISION,AREA,TERITORRY_NAME,TERITORRY_CODE,MR_NAME,SALES_TARGET,USER_NAME) ";
                    strSQL = strSQL + " select g.GR_PARENT AS zone, g.GR_NAME AS Division, l.LEDGER_PARENT_GROUP AS area,l.TERRITORRY_NAME ,l.TERITORRY_CODE, l.LEDGER_NAME_MERZE,";
                    strSQL = strSQL + " sum(C.TARGET_ACHIEVE_AMOUNT) COMMITMENT,'" + gstrUserName + "'    ";
                    //strSQL = strSQL + " ( sum(C.TARGET_ACHIEVE_AMOUNT)/" + intDay + ")* " + Convert.ToInt32(intUpdDay) + " COMMITMENT    ";
                    strSQL = strSQL + " from SMART0005.dbo.ACC_LEDGERGROUP g,SMART0005.dbo.ACC_LEDGERGROUP_CATEGORY_VIEW v,SMART0005.dbo.ACC_LEDGER l,SMART0005.dbo.SALES_TARGET_ACHIEVEMENT_MASTER m,SMART0005.dbo.SALES_TARGET_ACHIEVEMENT c,SMART0005.dbo.ACC_LEDGER_Z_D_A ";
                    strSQL = strSQL + " where L.LEDGER_NAME =SMART0005.dbo.ACC_LEDGER_Z_D_A.LEDGER_NAME AND g.GR_NAME=v.GR_PARENT  and v.GR_NAME=l.LEDGER_PARENT_GROUP and l.LEDGER_NAME =c.LEDGER_NAME  and m.TARGET_ACHIEVE_KEY=c.TARGET_ACHIEVE_KEY ";
                    strSQL = strSQL + " AND Upper(c.TARGET_ACHIEVE_MONTH_ID) in (" + strString + ")";
                    if (strBranchID != "")
                    {
                        strSQL = strSQL + " AND m.BRANCH_ID ='" + strBranchID + "' ";
                    }
                    if (intstatus < 2)
                    {
                        strSQL = strSQL + "AND l.LEDGER_STATUS =" + intstatus + " ";
                    }
                    if (intOption == 2)
                    {
                        if (strLedger != "")
                        {
                            strSQL = strSQL + "AND SMART0005.dbo.ACC_LEDGER_Z_D_A.LEDGER_NAME_MERZE IN(" + strLedger + ") ";
                        }
                    }
                    else if (intOption == 3)
                    {
                        if (strLedger != "")
                        {
                            strSQL = strSQL + "AND SMART0005.dbo.ACC_LEDGER_Z_D_A.AREA IN(" + strLedger + ") ";
                        }
                    }
                    else if (intOption == 4)
                    {
                        if (strLedger != "")
                        {
                            strSQL = strSQL + "AND SMART0005.dbo.ACC_LEDGER_Z_D_A.DIVISION IN(" + strLedger + ") ";
                        }
                    }
                    else if (intOption == 5)
                    {
                        if (strLedger != "")
                        {
                            strSQL = strSQL + "AND SMART0005.dbo.ACC_LEDGER_Z_D_A.ZONE IN(" + strLedger + ") ";
                        }
                    }
                    if (gstrUserName != "")
                    {
                        strSQL = strSQL + " AND SMART0005.dbo.ACC_LEDGER_Z_D_A.DIVISION in( select LEDGER_GROUP_NAME from SMART0005.dbo.USER_PRIVILEGES_COLOR WHERE USER_LOGIN_NAME ='" + gstrUserName + "')";
                    }
                    strSQL = strSQL + " group by g.GR_PARENT , g.GR_NAME , l.LEDGER_PARENT_GROUP,l.LEDGER_NAME_MERZE,l.TERITORRY_CODE,l.TERRITORRY_NAME ";
                    strSQL = strSQL + " having SUM(C.TARGET_ACHIEVE_AMOUNT) > 0 ";
                    cmdInsert.CommandText = strSQL;
                    cmdInsert.ExecuteNonQuery();

                }
                else
                {
                    strSQL = "INSERT INTO SMART0005.dbo.ACC_MARKET_MONITROING_SHEET(ZONE,DIVISION,AREA,TERITORRY_NAME,TERITORRY_CODE,MR_NAME,SALES_TARGET,USER_NAME) ";
                    strSQL = strSQL + " select g.GR_PARENT AS zone, g.GR_NAME AS Division, l.LEDGER_PARENT_GROUP AS area,l.TERRITORRY_NAME ,l.TERITORRY_CODE, l.LEDGER_NAME_MERZE,";
                    //strSQL = strSQL + " ( sum(C.TARGET_CHANGE_AMNT)/" + intDay + ")* " + Convert.ToInt32(intUpdDay) + " COMMITMENT    ";
                    strSQL = strSQL + " sum(C.TARGET_CHANGE_AMNT) COMMITMENT,'" + gstrUserName + "'    ";
                    strSQL = strSQL + " from SMART0005.dbo.ACC_LEDGERGROUP g,SMART0005.dbo.ACC_LEDGERGROUP_CATEGORY_VIEW v,SMART0005.dbo.ACC_LEDGER l,SMART0005.dbo.SALES_TARGET_ACHIEVEMENT_MASTER m,SMART0005.dbo.SALES_TARGET_ACHIEVEMENT c,SMART0005.dbo.ACC_LEDGER_Z_D_A ";
                    strSQL = strSQL + " where L.LEDGER_NAME =SMART0005.dbo.ACC_LEDGER_Z_D_A.LEDGER_NAME AND g.GR_NAME=v.GR_PARENT  and v.GR_NAME=l.LEDGER_PARENT_GROUP and l.LEDGER_NAME =c.LEDGER_NAME  and m.TARGET_ACHIEVE_KEY=c.TARGET_ACHIEVE_KEY ";
                    strSQL = strSQL + " AND Upper(c.TARGET_ACHIEVE_MONTH_ID) in (" + strString + ")";
                    if (strBranchID != "")
                    {
                        strSQL = strSQL + " AND m.BRANCH_ID ='" + strBranchID + "' ";
                    }
                    if (intstatus < 2)
                    {
                        strSQL = strSQL + "AND l.LEDGER_STATUS =" + intstatus + " ";
                    }
                    if (intOption == 2)
                    {
                        if (strLedger != "")
                        {
                            strSQL = strSQL + "AND SMART0005.dbo.ACC_LEDGER_Z_D_A.LEDGER_NAME_MERZE IN(" + strLedger + ") ";
                        }
                    }
                    else if (intOption == 3)
                    {
                        if (strLedger != "")
                        {
                            strSQL = strSQL + "AND SMART0005.dbo.ACC_LEDGER_Z_D_A.AREA IN(" + strLedger + ") ";
                        }
                    }
                    else if (intOption == 4)
                    {
                        if (strLedger != "")
                        {
                            strSQL = strSQL + "AND SMART0005.dbo.ACC_LEDGER_Z_D_A.DIVISION IN(" + strLedger + ") ";
                        }
                    }
                    else if (intOption == 5)
                    {
                        if (strLedger != "")
                        {
                            strSQL = strSQL + "AND SMART0005.dbo.ACC_LEDGER_Z_D_A.ZONE IN(" + strLedger + ") ";
                        }
                    }
                    if (gstrUserName != "")
                    {
                        strSQL = strSQL + " AND SMART0005.dbo.ACC_LEDGER_Z_D_A.DIVISION in( select LEDGER_GROUP_NAME from SMART0005.dbo.USER_PRIVILEGES_COLOR WHERE USER_LOGIN_NAME ='" + gstrUserName + "')";
                    }
                    strSQL = strSQL + " group by g.GR_PARENT , g.GR_NAME , l.LEDGER_PARENT_GROUP,l.LEDGER_NAME_MERZE,l.TERITORRY_CODE,l.TERRITORRY_NAME ";
                    strSQL = strSQL + " having SUM(C.TARGET_ACHIEVE_AMOUNT) > 0 ";
                    cmdInsert.CommandText = strSQL;
                    cmdInsert.ExecuteNonQuery();
                }
                //Achieve
                strSQL = "INSERT INTO SMART0005.dbo.ACC_MARKET_MONITROING_SHEET(ZONE,DIVISION,AREA,TERITORRY_NAME,TERITORRY_CODE,MR_NAME,ACHIEVE,USER_NAME) ";
                strSQL = strSQL + " select g.GR_PARENT AS zone, g.GR_NAME AS Division, l.LEDGER_PARENT_GROUP AS area,l.TERRITORRY_NAME , l.TERITORRY_CODE,l.LEDGER_NAME_MERZE, ";
                strSQL = strSQL + "  isnull(SUM(c.VOUCHER_DEBIT_AMOUNT-c.VOUCHER_CREDIT_AMOUNT),0) ACHIEVE,'" + gstrUserName + "'   ";
                strSQL = strSQL + " from SMART0005.dbo.ACC_LEDGERGROUP g,SMART0005.dbo.ACC_LEDGERGROUP_CATEGORY_VIEW v,SMART0005.dbo.ACC_LEDGER l,SMART0005.dbo.ACC_VOUCHER c,SMART0005.dbo.ACC_LEDGER_Z_D_A ";
                strSQL = strSQL + " where L.LEDGER_NAME =SMART0005.dbo.ACC_LEDGER_Z_D_A.LEDGER_NAME AND  g.GR_NAME=v.GR_PARENT  and v.GR_NAME=l.LEDGER_PARENT_GROUP and l.LEDGER_NAME =c.LEDGER_NAME ";
                strSQL = strSQL + " AND C.COMP_VOUCHER_TYPE IN(16)  ";
                if (strBranchID != "")
                {
                    strSQL = strSQL + " AND C.BRANCH_ID ='" + strBranchID + "' ";
                }
                strSQL = strSQL + " AND C.COMP_VOUCHER_DATE BETWEEN " + Utility.cvtSQLDateString(strFate) + " ";
                strSQL = strSQL + " AND " + Utility.cvtSQLDateString(strTDate);
                if (intstatus < 2)
                {
                    strSQL = strSQL + "AND l.LEDGER_STATUS =" + intstatus + " ";
                }
                if (intOption == 2)
                {
                    if (strLedger != "")
                    {
                        strSQL = strSQL + "AND SMART0005.dbo.ACC_LEDGER_Z_D_A.LEDGER_NAME_MERZE IN(" + strLedger + ") ";
                    }
                }
                else if (intOption == 3)
                {
                    if (strLedger != "")
                    {
                        strSQL = strSQL + "AND SMART0005.dbo.ACC_LEDGER_Z_D_A.AREA IN(" + strLedger + ") ";
                    }
                }
                else if (intOption == 4)
                {
                    if (strLedger != "")
                    {
                        strSQL = strSQL + "AND SMART0005.dbo.ACC_LEDGER_Z_D_A.DIVISION IN(" + strLedger + ") ";
                    }
                }
                else if (intOption == 5)
                {
                    if (strLedger != "")
                    {
                        strSQL = strSQL + "AND SMART0005.dbo.ACC_LEDGER_Z_D_A.ZONE IN(" + strLedger + ") ";
                    }
                }
                if (gstrUserName != "")
                {
                    strSQL = strSQL + " AND SMART0005.dbo.ACC_LEDGER_Z_D_A.DIVISION in( select LEDGER_GROUP_NAME from SMART0005.dbo.USER_PRIVILEGES_COLOR WHERE USER_LOGIN_NAME ='" + gstrUserName + "')";
                }
                strSQL = strSQL + " group by g.GR_PARENT , g.GR_NAME , l.LEDGER_PARENT_GROUP,l.LEDGER_NAME_MERZE,l.TERITORRY_CODE,l.TERRITORRY_NAME ";
                cmdInsert.CommandText = strSQL;
                cmdInsert.ExecuteNonQuery();


                strSQL = "INSERT INTO SMART0005.dbo.ACC_MARKET_MONITROING_SHEET(ZONE,DIVISION,AREA,TERITORRY_NAME,TERITORRY_CODE,MR_NAME,ACHIEVE,USER_NAME) ";
                strSQL = strSQL + "select g.GR_PARENT AS zone, g.GR_NAME AS Division, l.LEDGER_PARENT_GROUP AS area,l.TERRITORRY_NAME , l.TERITORRY_CODE,l.LEDGER_NAME_MERZE, ";
                strSQL = strSQL + "abs(ISNULL(SUM(C.COMP_VOUCHER_NET_AMOUNT),0))*-1 RETURN_AMOUNT,'" + gstrUserName + "'   ";
                strSQL = strSQL + "from SMART0005.dbo.ACC_LEDGERGROUP g,SMART0005.dbo.ACC_LEDGERGROUP_CATEGORY_VIEW v,SMART0005.dbo.ACC_LEDGER l,SMART0005.dbo.ACC_COMPANY_VOUCHER c,SMART0005.dbo.ACC_LEDGER_Z_D_A ";
                strSQL = strSQL + "where L.LEDGER_NAME =SMART0005.dbo.ACC_LEDGER_Z_D_A.LEDGER_NAME AND g.GR_NAME=v.GR_PARENT  and v.GR_NAME=l.LEDGER_PARENT_GROUP and l.LEDGER_NAME =c.LEDGER_NAME ";
                strSQL = strSQL + " AND C.COMP_VOUCHER_DATE BETWEEN " + Utility.cvtSQLDateString(strFate) + " ";
                strSQL = strSQL + " AND " + Utility.cvtSQLDateString(strTDate);
                strSQL = strSQL + "AND  C.COMP_VOUCHER_TYPE =13 ";
                strSQL = strSQL + "AND  C.SALES_RET_STA =0 ";
                if (strBranchID != "")
                {
                    strSQL = strSQL + " AND l.BRANCH_ID ='" + strBranchID + "' ";
                }

                if (intstatus < 2)
                {
                    strSQL = strSQL + "AND l.LEDGER_STATUS =" + intstatus + " ";
                }
                if (intOption == 2)
                {
                    if (strLedger != "")
                    {
                        strSQL = strSQL + "AND SMART0005.dbo.ACC_LEDGER_Z_D_A.LEDGER_NAME_MERZE IN(" + strLedger + ") ";
                    }
                }
                else if (intOption == 3)
                {
                    if (strLedger != "")
                    {
                        strSQL = strSQL + "AND SMART0005.dbo.ACC_LEDGER_Z_D_A.AREA IN(" + strLedger + ") ";
                    }
                }
                else if (intOption == 4)
                {
                    if (strLedger != "")
                    {
                        strSQL = strSQL + "AND SMART0005.dbo.ACC_LEDGER_Z_D_A.DIVISION IN(" + strLedger + ") ";
                    }
                }
                else if (intOption == 5)
                {
                    if (strLedger != "")
                    {
                        strSQL = strSQL + "AND SMART0005.dbo.ACC_LEDGER_Z_D_A.ZONE IN(" + strLedger + ") ";
                    }
                }
                if (gstrUserName != "")
                {
                    strSQL = strSQL + " AND SMART0005.dbo.ACC_LEDGER_Z_D_A.DIVISION in( select LEDGER_GROUP_NAME from SMART0005.dbo.USER_PRIVILEGES_COLOR WHERE USER_LOGIN_NAME ='" + gstrUserName + "')";
                }
                strSQL = strSQL + "group by g.GR_PARENT , g.GR_NAME , l.LEDGER_PARENT_GROUP,l.LEDGER_NAME_MERZE,l.TERITORRY_CODE,l.TERRITORRY_NAME,g.GR_PARENT_POSITION";
                cmdInsert.CommandText = strSQL;
                cmdInsert.CommandText = strSQL;
                cmdInsert.ExecuteNonQuery();

                //***************** after discussion with sayfull that why comments this is equal with ledger closing
                //// Closing Balance start
                //Dilution New Freeze 
                //strSQL = "INSERT INTO ACC_MARKET_MONITROING_SHEET(ZONE,DIVISION,AREA,TERITORRY_NAME,TERITORRY_CODE,MR_NAME,CLOSING_VAL,USER_NAME) ";
                //strSQL = strSQL + "SELECT ACC_LEDGER_Z_D_A.ZONE,ACC_LEDGER_Z_D_A.DIVISION,ACC_LEDGER_Z_D_A.AREA,ACC_LEDGER_Z_D_A.TERRITORRY_NAME,ACC_LEDGER_Z_D_A.TERITORRY_CODE,ACC_LEDGER_Z_D_A.LEDGER_NAME_MERZE,ISNULL(SUM(COMP_VOUCHER_NET_AMOUNT),0) COMP_VOUCHER_NET_AMOUNT ";
                //strSQL = strSQL + ",'"+ gstrUserName +"' FROM ACC_COMPANY_VOUCHER,ACC_LEDGER_Z_D_A WHERE ACC_LEDGER_Z_D_A.LEDGER_NAME =ACC_COMPANY_VOUCHER.LEDGER_NAME  ";
                //strSQL = strSQL + "AND ACC_COMPANY_VOUCHER.COMP_VOUCHER_DATE <" + Utility.cvtSQLDateString(strTDate) + " ";
                //strSQL = strSQL + " and COMP_VOUCHER_TYPE =12 AND DIL_FREEZE =8  ";
                //strSQL = strSQL + "AND ACC_COMPANY_VOUCHER.BRANCH_ID='" + strBranchID + "' ";
                //if (intstatus < 2)
                //{
                //    strSQL = strSQL + "AND ACC_LEDGER_Z_D_A.LEDGER_STATUS =" + intstatus + " ";
                //}
                //strSQL = strSQL + "GROUP BY ACC_LEDGER_Z_D_A.ZONE,ACC_LEDGER_Z_D_A.DIVISION,ACC_LEDGER_Z_D_A.AREA,ACC_LEDGER_Z_D_A.TERRITORRY_NAME,ACC_LEDGER_Z_D_A.TERITORRY_CODE,ACC_LEDGER_Z_D_A.LEDGER_NAME_MERZE ";
                //cmdInsert.CommandText = strSQL;
                //cmdInsert.ExecuteNonQuery();
                ////pRESAJOL
                //strSQL = "INSERT INTO ACC_MARKET_MONITROING_SHEET(ZONE,DIVISION,AREA,TERITORRY_NAME,TERITORRY_CODE,MR_NAME,CLOSING_VAL,USER_NAME) ";
                //strSQL = strSQL + "SELECT ACC_LEDGER_Z_D_A.ZONE,ACC_LEDGER_Z_D_A.DIVISION,ACC_LEDGER_Z_D_A.AREA,ACC_LEDGER_Z_D_A.TERRITORRY_NAME,ACC_LEDGER_Z_D_A.TERITORRY_CODE,ACC_LEDGER_Z_D_A.LEDGER_NAME_MERZE ,ISNULL(SUM(ACC_BILL_TRAN.BILL_NET_AMOUNT),0) *-1 ";
                //strSQL = strSQL + " ,'"+ gstrUserName +"' FROM ACC_BILL_TRAN,ACC_COMPANY_VOUCHER,ACC_LEDGER_Z_D_A  WHERE ACC_COMPANY_VOUCHER.COMP_REF_NO= ACC_BILL_TRAN.COMP_REF_NO  AND ACC_LEDGER_Z_D_A.LEDGER_NAME =ACC_COMPANY_VOUCHER.LEDGER_NAME AND ACC_BILL_TRAN.COMP_VOUCHER_TYPE =16 ";
                //strSQL = strSQL + "AND ACC_LEDGER_Z_D_A.LEDGER_STATUS =0 ";
                //strSQL = strSQL + "AND ACC_BILL_TRAN.STOCKITEM_NAME IN(SELECT DISTINCT STOCKITEM_NAME  FROM SALES_CREDIT_LIMIT_CHILD_ITEM WHERE ISACTIVE=0) ";
                //if (strBranchID != "")
                //{
                //    strSQL = strSQL + " AND ACC_COMPANY_VOUCHER.BRANCH_ID ='" + strBranchID + "' ";
                //}

                //if (intstatus < 2)
                //{
                //    strSQL = strSQL + "AND ACC_LEDGER_Z_D_A.LEDGER_STATUS =" + intstatus + " ";
                //}
                //strSQL = strSQL + "AND  (ACC_COMPANY_VOUCHER.COMP_VOUCHER_DATE < " + Utility.cvtSQLDateString(strTDate) + ") ";
                ////strSQL = strSQL + "AND  ACC_COMPANY_VOUCHER.COMP_VOUCHER_DATE between " + Utility.cvtSQLDateString(strdate4) + " and " + Utility.cvtSQLDateString(strlastdate) + " ";
                //strSQL = strSQL + "GROUP BY ACC_LEDGER_Z_D_A.ZONE,ACC_LEDGER_Z_D_A.DIVISION,ACC_LEDGER_Z_D_A.AREA,ACC_LEDGER_Z_D_A.TERRITORRY_NAME,ACC_LEDGER_Z_D_A.TERITORRY_CODE,ACC_LEDGER_Z_D_A.LEDGER_NAME_MERZE ";
                //cmdInsert.CommandText = strSQL;
                //cmdInsert.ExecuteNonQuery();
                //*******************************
                strSQL = "INSERT INTO SMART0005.dbo.ACC_MARKET_MONITROING_SHEET(ZONE,DIVISION,AREA,TERITORRY_NAME,TERITORRY_CODE,MR_NAME,CLOSING_VAL,USER_NAME) ";
                strSQL = strSQL + "SELECT g.GR_PARENT AS zone, g.GR_NAME AS Division, l.LEDGER_PARENT_GROUP AS area,l.TERRITORRY_NAME , l.TERITORRY_CODE,l.LEDGER_NAME_MERZE, isnull(sum(l.LEDGER_OPENING_BALANCE),0) *-1 PDUES   ";
                strSQL = strSQL + " ,'" + gstrUserName + "' from SMART0005.dbo.ACC_LEDGERGROUP g,SMART0005.dbo.ACC_LEDGERGROUP_CATEGORY_VIEW v,SMART0005.dbo.ACC_LEDGER l,SMART0005.dbo.ACC_LEDGER_Z_D_A where L.LEDGER_NAME =SMART0005.dbo.ACC_LEDGER_Z_D_A.LEDGER_NAME AND  g.GR_NAME=v.GR_PARENT  and v.GR_NAME=l.LEDGER_PARENT_GROUP ";
                if (strBranchID != "")
                {
                    strSQL = strSQL + "AND l.BRANCH_ID ='" + strBranchID + "' ";
                }

                if (intstatus < 2)
                {
                    strSQL = strSQL + "AND l.LEDGER_STATUS =" + intstatus + " ";
                }
                else
                {
                    strSQL = strSQL + "AND l.LEDGER_STATUS in (0,1)";
                }
                if (intOption == 2)
                {
                    if (strLedger != "")
                    {
                        strSQL = strSQL + "AND SMART0005.dbo.ACC_LEDGER_Z_D_A.LEDGER_NAME_MERZE IN(" + strLedger + ") ";
                    }
                }
                else if (intOption == 3)
                {
                    if (strLedger != "")
                    {
                        strSQL = strSQL + "AND SMART0005.dbo.ACC_LEDGER_Z_D_A.AREA IN(" + strLedger + ") ";
                    }
                }
                else if (intOption == 4)
                {
                    if (strLedger != "")
                    {
                        strSQL = strSQL + "AND SMART0005.dbo.ACC_LEDGER_Z_D_A.DIVISION IN(" + strLedger + ") ";
                    }
                }
                else if (intOption == 5)
                {
                    if (strLedger != "")
                    {
                        strSQL = strSQL + "AND SMART0005.dbo.ACC_LEDGER_Z_D_A.ZONE IN(" + strLedger + ") ";
                    }
                }
                if (gstrUserName != "")
                {
                    strSQL = strSQL + " AND SMART0005.dbo.ACC_LEDGER_Z_D_A.DIVISION in( select LEDGER_GROUP_NAME from SMART0005.dbo.USER_PRIVILEGES_COLOR WHERE USER_LOGIN_NAME ='" + gstrUserName + "')";
                }
                strSQL = strSQL + "group by g.GR_PARENT , g.GR_NAME , l.LEDGER_PARENT_GROUP,l.LEDGER_NAME_MERZE,l.TERITORRY_CODE,l.TERRITORRY_NAME ";
                cmdInsert.CommandText = strSQL;
                cmdInsert.ExecuteNonQuery();

                //Current Month
                strSQL = "INSERT INTO SMART0005.dbo.ACC_MARKET_MONITROING_SHEET(ZONE,DIVISION,AREA,TERITORRY_NAME,TERITORRY_CODE,MR_NAME,CLOSING_VAL,USER_NAME) ";
                strSQL = strSQL + "select g.GR_PARENT AS zone, g.GR_NAME AS Division, l.LEDGER_PARENT_GROUP AS area,l.TERRITORRY_NAME , l.TERITORRY_CODE,l.LEDGER_NAME_MERZE, ";
                strSQL = strSQL + "abs(ISNULL(SUM(C.COMP_VOUCHER_NET_AMOUNT),0)) SALES_CURRENT_MONTH,'" + gstrUserName + "'   ";
                strSQL = strSQL + " from SMART0005.dbo.ACC_LEDGERGROUP g,SMART0005.dbo.ACC_LEDGERGROUP_CATEGORY_VIEW v,SMART0005.dbo.ACC_LEDGER l,SMART0005.dbo.ACC_COMPANY_VOUCHER c,SMART0005.dbo.ACC_LEDGER_Z_D_A ";
                strSQL = strSQL + "where L.LEDGER_NAME =SMART0005.dbo.ACC_LEDGER_Z_D_A.LEDGER_NAME AND  g.GR_NAME=v.GR_PARENT  and v.GR_NAME=l.LEDGER_PARENT_GROUP and l.LEDGER_NAME =c.LEDGER_NAME ";
                strSQL = strSQL + "AND C.COMP_VOUCHER_DATE <= " + Utility.cvtSQLDateString(strTDate) + " ";
                strSQL = strSQL + "AND  C.COMP_VOUCHER_TYPE =16 ";
                if (strBranchID != "")
                {
                    strSQL = strSQL + "AND C.BRANCH_ID ='" + strBranchID + "' ";
                }

                if (intstatus < 2)
                {
                    strSQL = strSQL + "AND l.LEDGER_STATUS =" + intstatus + " ";
                }
                else
                {
                    strSQL = strSQL + "AND l.LEDGER_STATUS in (0,1)";
                }
                if (intOption == 2)
                {
                    if (strLedger != "")
                    {
                        strSQL = strSQL + "AND SMART0005.dbo.ACC_LEDGER_Z_D_A.LEDGER_NAME_MERZE IN(" + strLedger + ") ";
                    }
                }
                else if (intOption == 3)
                {
                    if (strLedger != "")
                    {
                        strSQL = strSQL + "AND SMART0005.dbo.ACC_LEDGER_Z_D_A.AREA IN(" + strLedger + ") ";
                    }
                }
                else if (intOption == 4)
                {
                    if (strLedger != "")
                    {
                        strSQL = strSQL + "AND SMART0005.dbo.ACC_LEDGER_Z_D_A.DIVISION IN(" + strLedger + ") ";
                    }
                }
                else if (intOption == 5)
                {
                    if (strLedger != "")
                    {
                        strSQL = strSQL + "AND SMART0005.dbo.ACC_LEDGER_Z_D_A.ZONE IN(" + strLedger + ") ";
                    }
                }
                if (gstrUserName != "")
                {
                    strSQL = strSQL + " AND SMART0005.dbo.ACC_LEDGER_Z_D_A.DIVISION in( select LEDGER_GROUP_NAME from SMART0005.dbo.USER_PRIVILEGES_COLOR WHERE USER_LOGIN_NAME ='" + gstrUserName + "')";
                }
                strSQL = strSQL + "group by g.GR_PARENT , g.GR_NAME , l.LEDGER_PARENT_GROUP,l.LEDGER_NAME_MERZE,l.TERITORRY_CODE,l.TERRITORRY_NAME";
                cmdInsert.CommandText = strSQL;
                cmdInsert.ExecuteNonQuery();
                //Return
                strSQL = "INSERT INTO SMART0005.dbo.ACC_MARKET_MONITROING_SHEET(ZONE,DIVISION,AREA,TERITORRY_NAME,TERITORRY_CODE,MR_NAME,CLOSING_VAL,USER_NAME) ";
                strSQL = strSQL + "select g.GR_PARENT AS zone, g.GR_NAME AS Division, l.LEDGER_PARENT_GROUP AS area,l.TERRITORRY_NAME , l.TERITORRY_CODE,l.LEDGER_NAME_MERZE, ";
                strSQL = strSQL + "abs(ISNULL(SUM(C.COMP_VOUCHER_NET_AMOUNT),0))*-1 RETURN_AMOUNT,'" + gstrUserName + "'  ";
                strSQL = strSQL + "from SMART0005.dbo.ACC_LEDGERGROUP g,SMART0005.dbo.ACC_LEDGERGROUP_CATEGORY_VIEW v,SMART0005.dbo.ACC_LEDGER l,SMART0005.dbo.ACC_COMPANY_VOUCHER c,SMART0005.dbo.ACC_LEDGER_Z_D_A ";
                strSQL = strSQL + "where L.LEDGER_NAME =SMART0005.dbo.ACC_LEDGER_Z_D_A.LEDGER_NAME AND  g.GR_NAME=v.GR_PARENT  and v.GR_NAME=l.LEDGER_PARENT_GROUP and l.LEDGER_NAME =c.LEDGER_NAME ";
                strSQL = strSQL + "AND C.COMP_VOUCHER_DATE <= " + Utility.cvtSQLDateString(strTDate) + " ";
                strSQL = strSQL + "AND  C.COMP_VOUCHER_TYPE =13 ";
                if (strBranchID != "")
                {
                    strSQL = strSQL + "AND C.BRANCH_ID ='" + strBranchID + "' ";
                }

                if (intstatus < 2)
                {
                    strSQL = strSQL + "AND l.LEDGER_STATUS =" + intstatus + " ";
                }
                else
                {
                    strSQL = strSQL + "AND l.LEDGER_STATUS in (0,1)";
                }
                if (intOption == 2)
                {
                    if (strLedger != "")
                    {
                        strSQL = strSQL + "AND SMART0005.dbo.ACC_LEDGER_Z_D_A.LEDGER_NAME_MERZE IN(" + strLedger + ") ";
                    }
                }
                else if (intOption == 3)
                {
                    if (strLedger != "")
                    {
                        strSQL = strSQL + "AND SMART0005.dbo.ACC_LEDGER_Z_D_A.AREA IN(" + strLedger + ") ";
                    }
                }
                else if (intOption == 4)
                {
                    if (strLedger != "")
                    {
                        strSQL = strSQL + "AND SMART0005.dbo.ACC_LEDGER_Z_D_A.DIVISION IN(" + strLedger + ") ";
                    }
                }
                else if (intOption == 5)
                {
                    if (strLedger != "")
                    {
                        strSQL = strSQL + "AND SMART0005.dbo.ACC_LEDGER_Z_D_A.ZONE IN(" + strLedger + ") ";
                    }
                }
                if (gstrUserName != "")
                {
                    strSQL = strSQL + " AND SMART0005.dbo.ACC_LEDGER_Z_D_A.DIVISION in( select LEDGER_GROUP_NAME from SMART0005.dbo.USER_PRIVILEGES_COLOR WHERE USER_LOGIN_NAME ='" + gstrUserName + "')";
                }
                strSQL = strSQL + "group by g.GR_PARENT , g.GR_NAME , l.LEDGER_PARENT_GROUP,l.LEDGER_NAME_MERZE,l.TERITORRY_CODE,l.TERRITORRY_NAME";
                cmdInsert.CommandText = strSQL;
                cmdInsert.ExecuteNonQuery();
                //Debit Amount

                strSQL = "INSERT INTO SMART0005.dbo.ACC_MARKET_MONITROING_SHEET(ZONE,DIVISION,AREA,TERITORRY_NAME,TERITORRY_CODE,MR_NAME,CLOSING_VAL,USER_NAME) ";
                strSQL = strSQL + "select g.GR_PARENT AS zone, g.GR_NAME AS Division, l.LEDGER_PARENT_GROUP AS area,l.TERRITORRY_NAME , l.TERITORRY_CODE,l.LEDGER_NAME_MERZE, ";
                strSQL = strSQL + "abs(ISNULL(SUM(av.VOUCHER_DEBIT_AMOUNT-av.VOUCHER_CREDIT_AMOUNT),0)) DEBIT_AMOUNT,'" + gstrUserName + "'  ";
                strSQL = strSQL + "from SMART0005.dbo.ACC_LEDGERGROUP g,SMART0005.dbo.ACC_LEDGERGROUP_CATEGORY_VIEW v,SMART0005.dbo.ACC_LEDGER l,SMART0005.dbo.ACC_COMPANY_VOUCHER c,SMART0005.dbo.ACC_VOUCHER av,SMART0005.dbo.ACC_LEDGER_Z_D_A ";
                strSQL = strSQL + "where L.LEDGER_NAME =SMART0005.dbo.ACC_LEDGER_Z_D_A.LEDGER_NAME AND g.GR_NAME=v.GR_PARENT  and v.GR_NAME=l.LEDGER_PARENT_GROUP and l.LEDGER_NAME =av.LEDGER_NAME  and c.COMP_REF_NO =av.COMP_REF_NO ";
                strSQL = strSQL + "AND aV.VOUCHER_TOBY ='Dr' AND aV.COMP_VOUCHER_TYPE =3 ";
                strSQL = strSQL + "AND C.COMP_VOUCHER_DATE <= " + Utility.cvtSQLDateString(strTDate) + " ";
                if (strBranchID != "")
                {
                    strSQL = strSQL + "AND C.BRANCH_ID ='" + strBranchID + "' ";
                }
                strSQL = strSQL + " AND  C.SP_JOURNAL =0 ";
                strSQL = strSQL + " AND  C.AUTOJV =0 ";
                strSQL = strSQL + " AND  C.DISABLE_VOUCHER =0 ";

                if (intstatus < 2)
                {
                    strSQL = strSQL + "AND l.LEDGER_STATUS =" + intstatus + " ";
                }
                else
                {
                    strSQL = strSQL + "AND l.LEDGER_STATUS in (0,1)";
                }
                if (intOption == 2)
                {
                    if (strLedger != "")
                    {
                        strSQL = strSQL + "AND SMART0005.dbo.ACC_LEDGER_Z_D_A.LEDGER_NAME_MERZE IN(" + strLedger + ") ";
                    }
                }
                else if (intOption == 3)
                {
                    if (strLedger != "")
                    {
                        strSQL = strSQL + "AND SMART0005.dbo.ACC_LEDGER_Z_D_A.AREA IN(" + strLedger + ") ";
                    }
                }
                else if (intOption == 4)
                {
                    if (strLedger != "")
                    {
                        strSQL = strSQL + "AND SMART0005.dbo.ACC_LEDGER_Z_D_A.DIVISION IN(" + strLedger + ") ";
                    }
                }
                else if (intOption == 5)
                {
                    if (strLedger != "")
                    {
                        strSQL = strSQL + "AND SMART0005.dbo.ACC_LEDGER_Z_D_A.ZONE IN(" + strLedger + ") ";
                    }
                }
                if (gstrUserName != "")
                {
                    strSQL = strSQL + " AND SMART0005.dbo.ACC_LEDGER_Z_D_A.DIVISION in( select LEDGER_GROUP_NAME from SMART0005.dbo.USER_PRIVILEGES_COLOR WHERE USER_LOGIN_NAME ='" + gstrUserName + "')";
                }
                strSQL = strSQL + "group by g.GR_PARENT , g.GR_NAME , l.LEDGER_PARENT_GROUP,l.LEDGER_NAME_MERZE,l.TERITORRY_CODE,l.TERRITORRY_NAME ";
                cmdInsert.CommandText = strSQL;
                cmdInsert.ExecuteNonQuery();
                //Modified_24-11-19
                strSQL = "INSERT INTO SMART0005.dbo.ACC_MARKET_MONITROING_SHEET(ZONE,DIVISION,AREA,TERITORRY_NAME,TERITORRY_CODE,MR_NAME,CLOSING_VAL,USER_NAME) ";
                strSQL = strSQL + "select g.GR_PARENT AS zone, g.GR_NAME AS Division, l.LEDGER_PARENT_GROUP AS area,l.TERRITORRY_NAME , l.TERITORRY_CODE,l.LEDGER_NAME_MERZE, ";
                strSQL = strSQL + "abs(ISNULL(SUM(av.VOUCHER_DEBIT_AMOUNT-av.VOUCHER_CREDIT_AMOUNT),0)) DEBIT_AMOUNT,'" + gstrUserName + "'   ";
                strSQL = strSQL + "from SMART0005.dbo.ACC_LEDGERGROUP g,SMART0005.dbo.ACC_LEDGERGROUP_CATEGORY_VIEW v,SMART0005.dbo.ACC_LEDGER l,SMART0005.dbo.ACC_COMPANY_VOUCHER c,SMART0005.dbo.ACC_VOUCHER av,SMART0005.dbo.ACC_LEDGER_Z_D_A ";
                strSQL = strSQL + "where L.LEDGER_NAME =SMART0005.dbo.ACC_LEDGER_Z_D_A.LEDGER_NAME AND g.GR_NAME=v.GR_PARENT  and v.GR_NAME=l.LEDGER_PARENT_GROUP and l.LEDGER_NAME =av.LEDGER_NAME  and c.COMP_REF_NO =av.COMP_REF_NO ";
                strSQL = strSQL + "AND aV.VOUCHER_TOBY ='Dr' AND aV.COMP_VOUCHER_TYPE =3 ";
                strSQL = strSQL + "AND C.COMP_VOUCHER_DATE <= " + Utility.cvtSQLDateString(strTDate) + " ";
                if (strBranchID != "")
                {
                    strSQL = strSQL + "AND C.BRANCH_ID ='" + strBranchID + "' ";
                }
                strSQL = strSQL + " AND  C.SP_JOURNAL =0 ";
                strSQL = strSQL + " AND  C.DISABLE_VOUCHER =1 ";
                strSQL = strSQL + " AND  C.AUTOJV =0 ";

                if (intstatus < 2)
                {
                    strSQL = strSQL + "AND l.LEDGER_STATUS =" + intstatus + " ";
                }
                else
                {
                    strSQL = strSQL + "AND l.LEDGER_STATUS in (0,1)";
                }
                if (intOption == 2)
                {
                    if (strLedger != "")
                    {
                        strSQL = strSQL + "AND SMART0005.dbo.ACC_LEDGER_Z_D_A.LEDGER_NAME_MERZE IN(" + strLedger + ") ";
                    }
                }
                else if (intOption == 3)
                {
                    if (strLedger != "")
                    {
                        strSQL = strSQL + "AND SMART0005.dbo.ACC_LEDGER_Z_D_A.AREA IN(" + strLedger + ") ";
                    }
                }
                else if (intOption == 4)
                {
                    if (strLedger != "")
                    {
                        strSQL = strSQL + "AND SMART0005.dbo.ACC_LEDGER_Z_D_A.DIVISION IN(" + strLedger + ") ";
                    }
                }
                else if (intOption == 5)
                {
                    if (strLedger != "")
                    {
                        strSQL = strSQL + "AND SMART0005.dbo.ACC_LEDGER_Z_D_A.ZONE IN(" + strLedger + ") ";
                    }
                }
                if (gstrUserName != "")
                {
                    strSQL = strSQL + " AND SMART0005.dbo.ACC_LEDGER_Z_D_A.DIVISION in( select LEDGER_GROUP_NAME from SMART0005.dbo.USER_PRIVILEGES_COLOR WHERE USER_LOGIN_NAME ='" + gstrUserName + "')";
                }
                strSQL = strSQL + "group by g.GR_PARENT , g.GR_NAME , l.LEDGER_PARENT_GROUP,l.LEDGER_NAME_MERZE,l.TERITORRY_CODE,l.TERRITORRY_NAME ";
                cmdInsert.CommandText = strSQL;
                cmdInsert.ExecuteNonQuery();

                //Credit Amount
                strSQL = "INSERT INTO SMART0005.dbo.ACC_MARKET_MONITROING_SHEET(ZONE,DIVISION,AREA,TERITORRY_NAME,TERITORRY_CODE,MR_NAME,CLOSING_VAL,USER_NAME) ";
                strSQL = strSQL + "SELECT  ZDA.ZONE , ZDA.DIVISION , ZDA.AREA  ,l.TERRITORRY_NAME , l.TERITORRY_CODE,l.LEDGER_NAME_MERZE, isnull(SUM(v.VOUCHER_CREDIT_AMOUNT-v.VOUCHER_DEBIT_AMOUNT),0) *-1,'" + gstrUserName + "' ";
                strSQL = strSQL + "FROM SMART0005.dbo.ACC_LEDGER_Z_D_A ZDA ,SMART0005.dbo.ACC_VOUCHER v,SMART0005.dbo.ACC_LEDGER L,SMART0005.dbo.ACC_COMPANY_VOUCHER c  WHERE L.LEDGER_NAME =ZDA.LEDGER_NAME AND V.LEDGER_NAME =L.LEDGER_NAME  and c.COMP_REF_NO =v.COMP_REF_NO ";
                strSQL = strSQL + "AND (V.COMP_VOUCHER_DATE <=  ";
                strSQL = strSQL + " " + Utility.cvtSQLDateString(strTDate) + ") ";
                strSQL = strSQL + "AND V.COMP_VOUCHER_TYPE=3 ";
                if (strBranchID != "")
                {
                    strSQL = strSQL + " AND L.BRANCH_ID ='" + strBranchID + "' ";
                }
                //strSQL = strSQL + "and c.SP_JOURNAL =0 ";
                strSQL = strSQL + "and c.SP_JOURNAL IN(0,2) ";//nEW Modified by Mayhedi 10_02_24
                strSQL = strSQL + "and v.VOUCHER_TOBY ='Cr' ";


                if (intstatus < 2)
                {
                    strSQL = strSQL + "AND l.LEDGER_STATUS =" + intstatus + " ";
                }
                else
                {
                    strSQL = strSQL + "AND l.LEDGER_STATUS in (0,1)";
                }
                if (intOption == 2)
                {
                    if (strLedger != "")
                    {
                        strSQL = strSQL + "AND ZDA.LEDGER_NAME_MERZE IN(" + strLedger + ") ";
                    }
                }
                else if (intOption == 3)
                {
                    if (strLedger != "")
                    {
                        strSQL = strSQL + "AND ZDA.AREA IN(" + strLedger + ") ";
                    }
                }
                else if (intOption == 4)
                {
                    if (strLedger != "")
                    {
                        strSQL = strSQL + "AND ZDA.DIVISION IN(" + strLedger + ") ";
                    }
                }
                else if (intOption == 5)
                {
                    if (strLedger != "")
                    {
                        strSQL = strSQL + "AND ZDA.ZONE IN(" + strLedger + ") ";
                    }
                }
                if (gstrUserName != "")
                {
                    strSQL = strSQL + " AND ZDA.DIVISION in( select LEDGER_GROUP_NAME from SMART0005.dbo.USER_PRIVILEGES_COLOR WHERE USER_LOGIN_NAME ='" + gstrUserName + "')";
                }
                strSQL = strSQL + " GROUP BY ZDA.ZONE, ZDA.DIVISION , ZDA.AREA ,l.TERRITORRY_NAME , l.TERITORRY_CODE,l.LEDGER_NAME_MERZE ";
                cmdInsert.CommandText = strSQL;
                cmdInsert.ExecuteNonQuery();

                //Cash
                strSQL = "INSERT INTO SMART0005.dbo.ACC_MARKET_MONITROING_SHEET(ZONE,DIVISION,AREA,TERITORRY_NAME,TERITORRY_CODE,MR_NAME,CLOSING_VAL,USER_NAME) ";
                strSQL = strSQL + "select g.GR_PARENT AS zone, g.GR_NAME AS Division, l.LEDGER_PARENT_GROUP AS area,l.TERRITORRY_NAME,l.TERITORRY_CODE, l.LEDGER_NAME_MERZE, ";
                strSQL = strSQL + " abs(ISNULL(SUM(av.VOUCHER_CREDIT_AMOUNT),0)) *-1 COLL_CASH_TT,'" + gstrUserName + "' ";
                strSQL = strSQL + "from SMART0005.dbo.ACC_LEDGERGROUP g,SMART0005.dbo.ACC_LEDGERGROUP_CATEGORY_VIEW v,SMART0005.dbo.ACC_LEDGER l,SMART0005.dbo.ACC_COMPANY_VOUCHER c,SMART0005.dbo.ACC_VOUCHER av,SMART0005.dbo.ACC_LEDGER_Z_D_A  ";
                strSQL = strSQL + "where L.LEDGER_NAME =SMART0005.dbo.ACC_LEDGER_Z_D_A.LEDGER_NAME AND  g.GR_NAME=v.GR_PARENT  and v.GR_NAME=l.LEDGER_PARENT_GROUP and l.LEDGER_NAME =av.LEDGER_NAME  and c.COMP_REF_NO =av.COMP_REF_NO  ";
                strSQL = strSQL + "AND av.COMP_VOUCHER_TYPE='1' ";
                strSQL = strSQL + "AND C.COMP_VOUCHER_DATE <= " + Utility.cvtSQLDateString(strTDate) + " ";
                ////strSQL = strSQL + "AND l.BRANCH_ID ='" + strBranchID + "' ";
                if (strBranchID != "")
                {
                    strSQL = strSQL + "AND av.BRANCH_ID ='" + strBranchID + "' ";
                }

                if (intstatus < 2)
                {
                    strSQL = strSQL + "AND l.LEDGER_STATUS =" + intstatus + " ";
                }
                else
                {
                    strSQL = strSQL + "AND l.LEDGER_STATUS in (0,1)";
                }
                if (intOption == 2)
                {
                    if (strLedger != "")
                    {
                        strSQL = strSQL + "AND SMART0005.dbo.ACC_LEDGER_Z_D_A.LEDGER_NAME_MERZE IN(" + strLedger + ") ";
                    }
                }
                else if (intOption == 3)
                {
                    if (strLedger != "")
                    {
                        strSQL = strSQL + "AND SMART0005.dbo.ACC_LEDGER_Z_D_A.AREA IN(" + strLedger + ") ";
                    }
                }
                else if (intOption == 4)
                {
                    if (strLedger != "")
                    {
                        strSQL = strSQL + "AND SMART0005.dbo.ACC_LEDGER_Z_D_A.DIVISION IN(" + strLedger + ") ";
                    }
                }
                else if (intOption == 5)
                {
                    if (strLedger != "")
                    {
                        strSQL = strSQL + "AND SMART0005.dbo.ACC_LEDGER_Z_D_A.ZONE IN(" + strLedger + ") ";
                    }
                }
                if (gstrUserName != "")
                {
                    strSQL = strSQL + " AND SMART0005.dbo.ACC_LEDGER_Z_D_A.DIVISION in( select LEDGER_GROUP_NAME from SMART0005.dbo.USER_PRIVILEGES_COLOR WHERE USER_LOGIN_NAME ='" + gstrUserName + "')";
                }
                strSQL = strSQL + "group by g.GR_PARENT , g.GR_NAME , l.LEDGER_PARENT_GROUP,l.LEDGER_NAME_MERZE,l.TERITORRY_CODE,l.TERRITORRY_NAME ";
                cmdInsert.CommandText = strSQL;
                cmdInsert.ExecuteNonQuery();
                //SP Journal
                strSQL = "INSERT INTO SMART0005.dbo.ACC_MARKET_MONITROING_SHEET(ZONE,DIVISION,AREA,TERITORRY_NAME,TERITORRY_CODE,MR_NAME,CLOSING_VAL,USER_NAME) ";
                strSQL = strSQL + "select g.GR_PARENT AS zone, g.GR_NAME AS Division, l.LEDGER_PARENT_GROUP AS area,l.TERRITORRY_NAME ,l.TERITORRY_CODE,l.LEDGER_NAME_MERZE,";
                strSQL = strSQL + "ABS(ISNULL(SUM(aV.VOUCHER_CREDIT_AMOUNT-aV.VOUCHER_DEBIT_AMOUNT),0)) *-1 COLL_VOUCHER,'" + gstrUserName + "'   ";
                strSQL = strSQL + "from SMART0005.dbo.ACC_LEDGERGROUP g,SMART0005.dbo.ACC_LEDGERGROUP_CATEGORY_VIEW v,SMART0005.dbo.ACC_LEDGER l,SMART0005.dbo.ACC_COMPANY_VOUCHER c,SMART0005.dbo.ACC_VOUCHER av,SMART0005.dbo.ACC_LEDGER_Z_D_A ";
                strSQL = strSQL + "where L.LEDGER_NAME =SMART0005.dbo.ACC_LEDGER_Z_D_A.LEDGER_NAME AND  g.GR_NAME=v.GR_PARENT  and v.GR_NAME=l.LEDGER_PARENT_GROUP and l.LEDGER_NAME =av.LEDGER_NAME  and c.COMP_REF_NO =av.COMP_REF_NO ";
                strSQL = strSQL + "AND aV.COMP_VOUCHER_TYPE =3 and c.SP_JOURNAL=1 ";
                strSQL = strSQL + "AND C.COMP_VOUCHER_DATE <= " + Utility.cvtSQLDateString(strTDate) + " ";
                if (strBranchID != "")
                {
                    strSQL = strSQL + "AND C.BRANCH_ID ='" + strBranchID + "' ";
                }
                strSQL = strSQL + " AND  C.AUTOJV =0 ";

                if (intstatus < 2)
                {
                    strSQL = strSQL + "AND l.LEDGER_STATUS =" + intstatus + " ";
                }
                else
                {
                    strSQL = strSQL + "AND l.LEDGER_STATUS in (0,1)";
                }
                if (intOption == 2)
                {
                    if (strLedger != "")
                    {
                        strSQL = strSQL + "AND SMART0005.dbo.ACC_LEDGER_Z_D_A.LEDGER_NAME_MERZE IN(" + strLedger + ") ";
                    }
                }
                else if (intOption == 3)
                {
                    if (strLedger != "")
                    {
                        strSQL = strSQL + "AND SMART0005.dbo.ACC_LEDGER_Z_D_A.AREA IN(" + strLedger + ") ";
                    }
                }
                else if (intOption == 4)
                {
                    if (strLedger != "")
                    {
                        strSQL = strSQL + "AND SMART0005.dbo.ACC_LEDGER_Z_D_A.DIVISION IN(" + strLedger + ") ";
                    }
                }
                else if (intOption == 5)
                {
                    if (strLedger != "")
                    {
                        strSQL = strSQL + "AND SMART0005.dbo.ACC_LEDGER_Z_D_A.ZONE IN(" + strLedger + ") ";
                    }
                }
                if (gstrUserName != "")
                {
                    strSQL = strSQL + " AND SMART0005.dbo.ACC_LEDGER_Z_D_A.DIVISION in( select LEDGER_GROUP_NAME from SMART0005.dbo.USER_PRIVILEGES_COLOR WHERE USER_LOGIN_NAME ='" + gstrUserName + "')";
                }
                strSQL = strSQL + "group by g.GR_PARENT , g.GR_NAME , l.LEDGER_PARENT_GROUP,l.LEDGER_NAME_MERZE,l.TERITORRY_CODE,l.TERRITORRY_NAME ";
                cmdInsert.CommandText = strSQL;
                cmdInsert.ExecuteNonQuery();

                //New minus pf HL 17_07_20

                strSQL = "INSERT INTO SMART0005.dbo.ACC_MARKET_MONITROING_SHEET(ZONE,DIVISION,AREA,TERITORRY_NAME,TERITORRY_CODE,MR_NAME,CLOSING_VAL,USER_NAME) ";
                strSQL = strSQL + "SELECT Z.ZONE,Z.DIVISION,Z.AREA,Z.TERRITORRY_NAME,Z.TERITORRY_CODE,  Z.LEDGER_NAME_MERZE ,ISNULL(SUM(v.VOUCHER_DEBIT_AMOUNT-V.VOUCHER_CREDIT_AMOUNT),0) *-1 ";
                strSQL = strSQL + " ,'" + gstrUserName + "' FROM SMART0005.dbo.ACC_LEDGER_Z_D_A Z,SMART0005.dbo.ACC_VOUCHER V WHERE Z.LEDGER_NAME=V.REVERSE_LEDGER1  AND V.COMP_VOUCHER_TYPE=3 ";
                strSQL = strSQL + "AND V.COMP_VOUCHER_DATE <= " + Utility.cvtSQLDateString(strTDate) + " ";
                if (strBranchID != "")
                {
                    strSQL = strSQL + "AND V.BRANCH_ID ='" + strBranchID + "' ";
                }
                strSQL = strSQL + " AND  V.AUTOJV=1";


                if (intstatus < 2)
                {
                    strSQL = strSQL + "AND Z.LEDGER_STATUS =" + intstatus + " ";
                }
                else
                {
                    strSQL = strSQL + "AND Z.LEDGER_STATUS in (0,1)";
                }
                if (intOption == 2)
                {
                    if (strLedger != "")
                    {
                        strSQL = strSQL + "AND Z.LEDGER_NAME_MERZE IN(" + strLedger + ") ";
                    }
                }
                else if (intOption == 3)
                {
                    if (strLedger != "")
                    {
                        strSQL = strSQL + "AND Z.AREA IN(" + strLedger + ") ";
                    }
                }
                else if (intOption == 4)
                {
                    if (strLedger != "")
                    {
                        strSQL = strSQL + "AND Z.DIVISION IN(" + strLedger + ") ";
                    }
                }
                else if (intOption == 5)
                {
                    if (strLedger != "")
                    {
                        strSQL = strSQL + "AND Z.ZONE IN(" + strLedger + ") ";
                    }
                }
                if (gstrUserName != "")
                {
                    strSQL = strSQL + " AND Z.DIVISION in( select LEDGER_GROUP_NAME from SMART0005.dbo.USER_PRIVILEGES_COLOR WHERE USER_LOGIN_NAME ='" + gstrUserName + "')";
                }
                strSQL = strSQL + "GROUP by  Z.ZONE,Z.DIVISION,Z.AREA ,Z.LEDGER_NAME,Z.TERRITORRY_NAME,Z.TERITORRY_CODE, Z.LEDGER_NAME_MERZE ";
                cmdInsert.CommandText = strSQL;
                cmdInsert.ExecuteNonQuery();
                //end closing balance

                //Credit Limit closing balance 
                strSQL = "INSERT INTO SMART0005.dbo.ACC_MARKET_MONITROING_SHEET(ZONE,DIVISION,AREA,TERITORRY_NAME,TERITORRY_CODE,MR_NAME,Credit_Limit,USER_NAME) ";
                strSQL = strSQL + "select SMART0005.dbo.ACC_LEDGER_Z_D_A.ZONE,SMART0005.dbo.ACC_LEDGER_Z_D_A.DIVISION,SMART0005.dbo.ACC_LEDGER_Z_D_A.AREA, ";
                strSQL = strSQL + "SMART0005.dbo.ACC_LEDGER_Z_D_A.TERRITORRY_NAME,SMART0005.dbo.ACC_LEDGER_Z_D_A.TERITORRY_CODE,SMART0005.dbo.ACC_LEDGER_Z_D_A.LEDGER_NAME_MERZE,sum(CL.CREDIT_LIMIT_AMOUNT+CL.ITEM_CREDIT_AMOUNT) as CREDIT_LIMIT,'" + gstrUserName + "' FROM  ";
                strSQL = strSQL + "SMART0005.dbo.ACC_LEDGER_Z_D_A INNER JOIN SMART0005.dbo.ACC_LEDGER l ON SMART0005.dbo.ACC_LEDGER_Z_D_A.LEDGER_NAME = l.LEDGER_NAME RIGHT OUTER JOIN SMART0005.dbo.SALES_CREDIT_LIMIT AS CL ON l.LEDGER_NAME = CL.LEDGER_NAME ";
                strSQL = strSQL + "LEFT OUTER JOIN SMART0005.dbo.SALES_CREDIT_LIMIT_MASTER AS M ON CL.CREDIT_LIMIT_KEY = M.CREDIT_LIMIT_KEY  ";
                strSQL = strSQL + "where l.LEDGER_NAME= CL.LEDGER_NAME and CL.CREDIT_LIMIT_FROM_DATE>=" + Utility.cvtSQLDateString(strFate) + " and CL.CREDIT_LIMIT_TO_DATE <=" + Utility.cvtSQLDateString(strTDate) + " ";
                if (strBranchID != "")
                {
                    strSQL = strSQL + " AND M.BRANCH_ID ='" + strBranchID + "' ";
                }

                if (intstatus < 2)
                {
                    strSQL = strSQL + "AND l.LEDGER_STATUS  =" + intstatus + " ";
                }
                if (intOption == 2)
                {
                    if (strLedger != "")
                    {
                        strSQL = strSQL + "AND SMART0005.dbo.ACC_LEDGER_Z_D_A.LEDGER_NAME_MERZE IN(" + strLedger + ") ";
                    }
                }
                else if (intOption == 3)
                {
                    if (strLedger != "")
                    {
                        strSQL = strSQL + "AND SMART0005.dbo.ACC_LEDGER_Z_D_A.AREA IN(" + strLedger + ") ";
                    }
                }
                else if (intOption == 4)
                {
                    if (strLedger != "")
                    {
                        strSQL = strSQL + "AND SMART0005.dbo.ACC_LEDGER_Z_D_A.DIVISION IN(" + strLedger + ") ";
                    }
                }
                else if (intOption == 5)
                {
                    if (strLedger != "")
                    {
                        strSQL = strSQL + "AND SMART0005.dbo.ACC_LEDGER_Z_D_A.ZONE IN(" + strLedger + ") ";
                    }
                }
                if (gstrUserName != "")
                {
                    strSQL = strSQL + " AND SMART0005.dbo.ACC_LEDGER_Z_D_A.DIVISION in( select LEDGER_GROUP_NAME from SMART0005.dbo.USER_PRIVILEGES_COLOR WHERE USER_LOGIN_NAME ='" + gstrUserName + "')";
                }
                strSQL = strSQL + "GROUP BY SMART0005.dbo.ACC_LEDGER_Z_D_A.ZONE,SMART0005.dbo.ACC_LEDGER_Z_D_A.DIVISION,SMART0005.dbo.ACC_LEDGER_Z_D_A.AREA,SMART0005.dbo.ACC_LEDGER_Z_D_A.ZONE, ";
                strSQL = strSQL + "SMART0005.dbo.ACC_LEDGER_Z_D_A.TERRITORRY_NAME,SMART0005.dbo.ACC_LEDGER_Z_D_A.TERITORRY_CODE,SMART0005.dbo.ACC_LEDGER_Z_D_A.LEDGER_NAME_MERZE ";
                cmdInsert.CommandText = strSQL;
                cmdInsert.ExecuteNonQuery();
                #endregion
                #region "Pending"
                // Opening----------
                strSQL = "INSERT INTO SMART0005.dbo.ACC_MARKET_MONITROING_SHEET(ZONE,DIVISION,AREA,TERITORRY_NAME,TERITORRY_CODE,MR_NAME,PENDING,USER_NAME) ";
                strSQL = strSQL + "SELECT SMART0005.dbo.ACC_LEDGER_Z_D_A.ZONE,SMART0005.dbo.ACC_LEDGER_Z_D_A.DIVISION,SMART0005.dbo.ACC_LEDGER_Z_D_A.AREA,SMART0005.dbo.ACC_LEDGER_Z_D_A.TERRITORRY_NAME,SMART0005.dbo.ACC_LEDGER_Z_D_A.TERITORRY_CODE,SMART0005.dbo.ACC_LEDGER_Z_D_A.LEDGER_NAME_MERZE,SMART0005.dbo.ACC_LEDGER.LEDGER_OPENING_BALANCE *-1, '" + gstrUserName + "' ";
                strSQL = strSQL + "FROM SMART0005.dbo.ACC_LEDGER_Z_D_A,SMART0005.dbo.ACC_LEDGER WHERE SMART0005.dbo.ACC_LEDGER.LEDGER_NAME =SMART0005.dbo.ACC_LEDGER_Z_D_A.LEDGER_NAME  ";
                // strSQL = strSQL + "AND ACC_LEDGER.LEDGER_STATUS =0  AND ACC_LEDGER_Z_D_A.BRANCH_ID ='" + strBranchID + "' ";
                if (strBranchID != "")
                {
                    strSQL = strSQL + " AND SMART0005.dbo.ACC_LEDGER_Z_D_A.BRANCH_ID ='" + strBranchID + "' ";
                }

                if (intstatus < 2)
                {
                    strSQL = strSQL + "AND SMART0005.dbo.ACC_LEDGER.LEDGER_STATUS  =" + intstatus + " ";
                }
                if (intOption == 2)
                {
                    if (strLedger != "")
                    {
                        strSQL = strSQL + "AND SMART0005.dbo.ACC_LEDGER_Z_D_A.LEDGER_NAME_MERZE IN(" + strLedger + ") ";
                    }
                }
                else if (intOption == 3)
                {
                    if (strLedger != "")
                    {
                        strSQL = strSQL + "AND SMART0005.dbo.ACC_LEDGER_Z_D_A.AREA IN(" + strLedger + ") ";
                    }
                }
                else if (intOption == 4)
                {
                    if (strLedger != "")
                    {
                        strSQL = strSQL + "AND SMART0005.dbo.ACC_LEDGER_Z_D_A.DIVISION IN(" + strLedger + ") ";
                    }
                }
                else if (intOption == 5)
                {
                    if (strLedger != "")
                    {
                        strSQL = strSQL + "AND SMART0005.dbo.ACC_LEDGER_Z_D_A.ZONE IN(" + strLedger + ") ";
                    }
                }
                if (gstrUserName != "")
                {
                    strSQL = strSQL + " AND SMART0005.dbo.ACC_LEDGER_Z_D_A.DIVISION in( select LEDGER_GROUP_NAME from SMART0005.dbo.USER_PRIVILEGES_COLOR WHERE USER_LOGIN_NAME ='" + gstrUserName + "')";
                }

                cmdInsert.CommandText = strSQL;
                cmdInsert.ExecuteNonQuery();

                ////Credit Limit

                //strSQL = "INSERT INTO ACC_MARKET_MONITROING_SHEET(ZONE,DIVISION,AREA,TERITORRY_NAME,TERITORRY_CODE,MR_NAME,PENDING,USER_NAME) ";
                //strSQL = strSQL + "SELECT ACC_LEDGER_Z_D_A.ZONE,ACC_LEDGER_Z_D_A.DIVISION,ACC_LEDGER_Z_D_A.AREA,ACC_LEDGER_Z_D_A.TERRITORRY_NAME,ACC_LEDGER_Z_D_A.TERITORRY_CODE,ACC_LEDGER_Z_D_A.LEDGER_NAME_MERZE,SUM(SALES_CREDIT_LIMIT.CREDIT_LIMIT_AMOUNT)CREDIT_LIMIT_AMOUNT, '" + gstrMacAddress + "'  ";
                //strSQL = strSQL + "FROM ACC_LEDGER_Z_D_A,SALES_CREDIT_LIMIT WHERE ACC_LEDGER_Z_D_A.LEDGER_NAME  ";
                //strSQL = strSQL + "=SALES_CREDIT_LIMIT.LEDGER_NAME  ";
                //strSQL = strSQL + "AND SALES_CREDIT_LIMIT.CREDIT_LIMIT_FROM_DATE >=" + Utility.cvtSQLDateString(strFate) + " ";
                //strSQL = strSQL + "AND SALES_CREDIT_LIMIT.CREDIT_LIMIT_TO_DATE <=" + Utility.cvtSQLDateString(strTDate) + " ";
                //strSQL = strSQL + "AND ACC_LEDGER_Z_D_A.BRANCH_ID ='" + strBranchID + "' AND ACC_LEDGER_Z_D_A.LEDGER_STATUS =0 ";
                //strSQL = strSQL + "GROUP BY  ACC_LEDGER_Z_D_A.ZONE,ACC_LEDGER_Z_D_A.DIVISION,ACC_LEDGER_Z_D_A.AREA,ACC_LEDGER_Z_D_A.TERRITORRY_NAME,ACC_LEDGER_Z_D_A.TERITORRY_CODE,ACC_LEDGER_Z_D_A.LEDGER_NAME_MERZE  ";
                //cmdInsert.CommandText = strSQL;
                //cmdInsert.ExecuteNonQuery();

                //Invoice
                strSQL = "INSERT INTO SMART0005.dbo.ACC_MARKET_MONITROING_SHEET(ZONE,DIVISION,AREA,TERITORRY_NAME,TERITORRY_CODE,MR_NAME,PENDING,USER_NAME) ";
                strSQL = strSQL + "SELECT SMART0005.dbo.ACC_LEDGER_Z_D_A.ZONE,SMART0005.dbo.ACC_LEDGER_Z_D_A.DIVISION,SMART0005.dbo.ACC_LEDGER_Z_D_A.AREA,SMART0005.dbo.ACC_LEDGER_Z_D_A.TERRITORRY_NAME,SMART0005.dbo.ACC_LEDGER_Z_D_A.TERITORRY_CODE,SMART0005.dbo.ACC_LEDGER_Z_D_A.LEDGER_NAME_MERZE,ABS(ISNULL(SUM(ACC_VOUCHER.VOUCHER_DEBIT_AMOUNT-ACC_VOUCHER.VOUCHER_CREDIT_AMOUNT),0))*1 Sales, '" + gstrUserName + "'   ";
                strSQL = strSQL + "FROM SMART0005.dbo.ACC_LEDGER_Z_D_A,SMART0005.dbo.ACC_VOUCHER,SMART0005.dbo.ACC_COMPANY_VOUCHER  ";
                strSQL = strSQL + "WHERE SMART0005.dbo.ACC_LEDGER_Z_D_A.LEDGER_NAME =SMART0005.dbo.ACC_VOUCHER.LEDGER_NAME AND SMART0005.dbo.ACC_COMPANY_VOUCHER.COMP_REF_NO = SMART0005.dbo.ACC_VOUCHER.COMP_REF_NO   ";
                strSQL = strSQL + "AND SMART0005.dbo.ACC_VOUCHER.COMP_VOUCHER_DATE  <= " + Utility.cvtSQLDateString(strTDate) + " AND SMART0005.dbo.ACC_VOUCHER.COMP_VOUCHER_TYPE =16 ";
                strSQL = strSQL + "AND SMART0005.dbo.ACC_COMPANY_VOUCHER.I_CL_PEN=0 ";
                //strSQL = strSQL + "AND ACC_LEDGER_Z_D_A.BRANCH_ID ='" + strBranchID + "' AND ACC_LEDGER_Z_D_A.LEDGER_STATUS =0 ";
                if (strBranchID != "")
                {
                    strSQL = strSQL + " AND SMART0005.dbo.ACC_LEDGER_Z_D_A.BRANCH_ID ='" + strBranchID + "' ";
                }

                if (intstatus < 2)
                {
                    strSQL = strSQL + "AND SMART0005.dbo.ACC_LEDGER_Z_D_A.LEDGER_STATUS  =" + intstatus + " ";
                }
                if (intOption == 2)
                {
                    if (strLedger != "")
                    {
                        strSQL = strSQL + "AND SMART0005.dbo.ACC_LEDGER_Z_D_A.LEDGER_NAME_MERZE IN(" + strLedger + ") ";
                    }
                }
                else if (intOption == 3)
                {
                    if (strLedger != "")
                    {
                        strSQL = strSQL + "AND SMART0005.dbo.ACC_LEDGER_Z_D_A.AREA IN(" + strLedger + ") ";
                    }
                }
                else if (intOption == 4)
                {
                    if (strLedger != "")
                    {
                        strSQL = strSQL + "AND SMART0005.dbo.ACC_LEDGER_Z_D_A.DIVISION IN(" + strLedger + ") ";
                    }
                }
                else if (intOption == 5)
                {
                    if (strLedger != "")
                    {
                        strSQL = strSQL + "AND SMART0005.dbo.ACC_LEDGER_Z_D_A.ZONE IN(" + strLedger + ") ";
                    }
                }
                if (gstrUserName != "")
                {
                    strSQL = strSQL + " AND SMART0005.dbo.ACC_LEDGER_Z_D_A.DIVISION in( select LEDGER_GROUP_NAME from SMART0005.dbo.USER_PRIVILEGES_COLOR WHERE USER_LOGIN_NAME ='" + gstrUserName + "')";
                }
                strSQL = strSQL + "GROUP BY  SMART0005.dbo.ACC_LEDGER_Z_D_A.ZONE,SMART0005.dbo.ACC_LEDGER_Z_D_A.DIVISION,SMART0005.dbo.ACC_LEDGER_Z_D_A.AREA,SMART0005.dbo.ACC_LEDGER_Z_D_A.TERRITORRY_NAME,SMART0005.dbo.ACC_LEDGER_Z_D_A.TERITORRY_CODE,SMART0005.dbo.ACC_LEDGER_Z_D_A.LEDGER_NAME_MERZE  ";
                cmdInsert.CommandText = strSQL;
                cmdInsert.ExecuteNonQuery();

                strSQL = "INSERT INTO SMART0005.dbo.ACC_MARKET_MONITROING_SHEET(ZONE,DIVISION,AREA,TERITORRY_NAME,TERITORRY_CODE,MR_NAME,PENDING,USER_NAME) ";
                strSQL = strSQL + "SELECT SMART0005.dbo.ACC_LEDGER_Z_D_A.ZONE,SMART0005.dbo.ACC_LEDGER_Z_D_A.DIVISION,SMART0005.dbo.ACC_LEDGER_Z_D_A.AREA,SMART0005.dbo.ACC_LEDGER_Z_D_A.TERRITORRY_NAME,SMART0005.dbo.ACC_LEDGER_Z_D_A.TERITORRY_CODE,SMART0005.dbo.ACC_LEDGER_Z_D_A.LEDGER_NAME_MERZE,ABS(ISNULL(SUM(SMART0005.dbo.ACC_VOUCHER.VOUCHER_DEBIT_AMOUNT-SMART0005.dbo.ACC_VOUCHER.VOUCHER_CREDIT_AMOUNT),0))*-1 Sales, '" + gstrUserName + "'   ";
                strSQL = strSQL + "FROM SMART0005.dbo.ACC_LEDGER_Z_D_A,SMART0005.dbo.ACC_VOUCHER,SMART0005.dbo.ACC_COMPANY_VOUCHER  ";
                strSQL = strSQL + "WHERE SMART0005.dbo.ACC_LEDGER_Z_D_A.LEDGER_NAME =SMART0005.dbo.ACC_VOUCHER.LEDGER_NAME AND SMART0005.dbo.ACC_COMPANY_VOUCHER.COMP_REF_NO = SMART0005.dbo.ACC_VOUCHER.COMP_REF_NO   ";
                strSQL = strSQL + "AND SMART0005.dbo.ACC_VOUCHER.COMP_VOUCHER_DATE >=  " + Utility.cvtSQLDateString(strFate) + "  AND SMART0005.dbo.ACC_VOUCHER.COMP_VOUCHER_DATE <=  " + Utility.cvtSQLDateString(strTDate) + " AND SMART0005.dbo.ACC_VOUCHER.COMP_VOUCHER_TYPE =16 ";
                strSQL = strSQL + "AND SMART0005.dbo.ACC_COMPANY_VOUCHER.I_CL_PEN=1 ";
                //strSQL = strSQL + "AND ACC_LEDGER_Z_D_A.BRANCH_ID ='" + strBranchID + "' AND ACC_LEDGER_Z_D_A.LEDGER_STATUS =0 ";
                if (strBranchID != "")
                {
                    strSQL = strSQL + " AND SMART0005.dbo.ACC_LEDGER_Z_D_A.BRANCH_ID ='" + strBranchID + "' ";
                }

                if (intstatus < 2)
                {
                    strSQL = strSQL + "AND SMART0005.dbo.ACC_LEDGER_Z_D_A.LEDGER_STATUS  =" + intstatus + " ";
                }
                if (intOption == 2)
                {
                    if (strLedger != "")
                    {
                        strSQL = strSQL + "AND SMART0005.dbo.ACC_LEDGER_Z_D_A.LEDGER_NAME_MERZE IN(" + strLedger + ") ";
                    }
                }
                else if (intOption == 3)
                {
                    if (strLedger != "")
                    {
                        strSQL = strSQL + "AND SMART0005.dbo.ACC_LEDGER_Z_D_A.AREA IN(" + strLedger + ") ";
                    }
                }
                else if (intOption == 4)
                {
                    if (strLedger != "")
                    {
                        strSQL = strSQL + "AND SMART0005.dbo.ACC_LEDGER_Z_D_A.DIVISION IN(" + strLedger + ") ";
                    }
                }
                else if (intOption == 5)
                {
                    if (strLedger != "")
                    {
                        strSQL = strSQL + "AND SMART0005.dbo.ACC_LEDGER_Z_D_A.ZONE IN(" + strLedger + ") ";
                    }
                }
                if (gstrUserName != "")
                {
                    strSQL = strSQL + " AND SMART0005.dbo.ACC_LEDGER_Z_D_A.DIVISION in( select LEDGER_GROUP_NAME from SMART0005.dbo.USER_PRIVILEGES_COLOR WHERE USER_LOGIN_NAME ='" + gstrUserName + "')";
                }
                strSQL = strSQL + "GROUP BY  SMART0005.dbo.ACC_LEDGER_Z_D_A.ZONE,SMART0005.dbo.ACC_LEDGER_Z_D_A.DIVISION,SMART0005.dbo.ACC_LEDGER_Z_D_A.AREA,SMART0005.dbo.ACC_LEDGER_Z_D_A.TERRITORRY_NAME,SMART0005.dbo.ACC_LEDGER_Z_D_A.TERITORRY_CODE,SMART0005.dbo.ACC_LEDGER_Z_D_A.LEDGER_NAME_MERZE  ";
                cmdInsert.CommandText = strSQL;
                cmdInsert.ExecuteNonQuery();

                strSQL = "INSERT INTO ACC_MARKET_MONITROING_SHEET(ZONE,DIVISION,AREA,TERITORRY_NAME,TERITORRY_CODE,MR_NAME,PENDING,USER_NAME) ";
                strSQL = strSQL + "SELECT SMART0005.dbo.ACC_LEDGER_Z_D_A.ZONE,SMART0005.dbo.ACC_LEDGER_Z_D_A.DIVISION,SMART0005.dbo.ACC_LEDGER_Z_D_A.AREA,SMART0005.dbo.ACC_LEDGER_Z_D_A.TERRITORRY_NAME,SMART0005.dbo.ACC_LEDGER_Z_D_A.TERITORRY_CODE,SMART0005.dbo.ACC_LEDGER_Z_D_A.LEDGER_NAME_MERZE,ABS(ISNULL(SUM(SMART0005.dbo.ACC_VOUCHER.VOUCHER_DEBIT_AMOUNT-SMART0005.dbo.ACC_VOUCHER.VOUCHER_CREDIT_AMOUNT),0))* -1 Returnval, '" + gstrUserName + "'  ";
                strSQL = strSQL + "FROM SMART0005.dbo.ACC_LEDGER_Z_D_A,SMART0005.dbo.ACC_VOUCHER,SMART0005.dbo.ACC_COMPANY_VOUCHER ";
                strSQL = strSQL + "WHERE ACC_LEDGER_Z_D_A.LEDGER_NAME =ACC_VOUCHER.LEDGER_NAME AND ACC_COMPANY_VOUCHER.COMP_REF_NO = SMART0005.dbo.ACC_VOUCHER.COMP_REF_NO   ";
                strSQL = strSQL + "AND SMART0005.dbo.ACC_VOUCHER.COMP_VOUCHER_DATE  <= " + Utility.cvtSQLDateString(strTDate) + " AND SMART0005.dbo.ACC_VOUCHER.COMP_VOUCHER_TYPE =13 AND SMART0005.dbo.ACC_COMPANY_VOUCHER.I_CL_PEN=0 ";
                //strSQL = strSQL + "AND ACC_LEDGER_Z_D_A.BRANCH_ID ='" + strBranchID + "' AND ACC_LEDGER_Z_D_A.LEDGER_STATUS =0 ";
                if (strBranchID != "")
                {
                    strSQL = strSQL + " AND SMART0005.dbo.ACC_LEDGER_Z_D_A.BRANCH_ID ='" + strBranchID + "' ";
                }

                if (intstatus < 2)
                {
                    strSQL = strSQL + "AND SMART0005.dbo.ACC_LEDGER_Z_D_A.LEDGER_STATUS  =" + intstatus + " ";
                }
                if (intOption == 2)
                {
                    if (strLedger != "")
                    {
                        strSQL = strSQL + "AND SMART0005.dbo.ACC_LEDGER_Z_D_A.LEDGER_NAME_MERZE IN(" + strLedger + ") ";
                    }
                }
                else if (intOption == 3)
                {
                    if (strLedger != "")
                    {
                        strSQL = strSQL + "AND SMART0005.dbo.ACC_LEDGER_Z_D_A.AREA IN(" + strLedger + ") ";
                    }
                }
                else if (intOption == 4)
                {
                    if (strLedger != "")
                    {
                        strSQL = strSQL + "AND SMART0005.dbo.ACC_LEDGER_Z_D_A.DIVISION IN(" + strLedger + ") ";
                    }
                }
                else if (intOption == 5)
                {
                    if (strLedger != "")
                    {
                        strSQL = strSQL + "AND SMART0005.dbo.ACC_LEDGER_Z_D_A.ZONE IN(" + strLedger + ") ";
                    }
                }
                if (gstrUserName != "")
                {
                    strSQL = strSQL + " AND SMART0005.dbo.ACC_LEDGER_Z_D_A.DIVISION in( select LEDGER_GROUP_NAME from SMART0005.dbo.USER_PRIVILEGES_COLOR WHERE USER_LOGIN_NAME ='" + gstrUserName + "')";
                }
                strSQL = strSQL + "GROUP BY  SMART0005.dbo.ACC_LEDGER_Z_D_A.ZONE,SMART0005.dbo.ACC_LEDGER_Z_D_A.DIVISION,SMART0005.dbo.ACC_LEDGER_Z_D_A.AREA,SMART0005.dbo.ACC_LEDGER_Z_D_A.TERRITORRY_NAME,SMART0005.dbo.ACC_LEDGER_Z_D_A.TERITORRY_CODE,SMART0005.dbo.ACC_LEDGER_Z_D_A.LEDGER_NAME_MERZE  ";
                cmdInsert.CommandText = strSQL;
                cmdInsert.ExecuteNonQuery();

                //--*************iTEM SALES
                strSQL = "INSERT INTO ACC_MARKET_MONITROING_SHEET(ZONE,DIVISION,AREA,TERITORRY_NAME,TERITORRY_CODE,MR_NAME,PENDING,USER_NAME) ";
                strSQL = strSQL + "SELECT SMART0005.dbo.ACC_LEDGER_Z_D_A.ZONE,SMART0005.dbo.ACC_LEDGER_Z_D_A.DIVISION,SMART0005.dbo.ACC_LEDGER_Z_D_A.AREA,SMART0005.dbo.ACC_LEDGER_Z_D_A.TERRITORRY_NAME,SMART0005.dbo.ACC_LEDGER_Z_D_A.TERITORRY_CODE,SMART0005.dbo.ACC_LEDGER_Z_D_A.LEDGER_NAME_MERZE,ABS(ISNULL(SUM(SMART0005.dbo.ACC_VOUCHER.VOUCHER_DEBIT_AMOUNT-SMART0005.dbo.ACC_VOUCHER.VOUCHER_CREDIT_AMOUNT),0))  *1 ProductSales, '" + gstrUserName + "'  ";
                strSQL = strSQL + "FROM SMART0005.dbo.ACC_LEDGER_Z_D_A,SMART0005.dbo.ACC_VOUCHER,SMART0005.dbo.ACC_COMPANY_VOUCHER    ";
                strSQL = strSQL + "WHERE ACC_LEDGER_Z_D_A.LEDGER_NAME =ACC_VOUCHER.LEDGER_NAME AND SMART0005.dbo.ACC_COMPANY_VOUCHER.COMP_REF_NO = SMART0005.dbo.ACC_VOUCHER.COMP_REF_NO   ";
                strSQL = strSQL + "AND SMART0005.dbo.ACC_VOUCHER.COMP_VOUCHER_DATE  <= " + Utility.cvtSQLDateString(strTDate) + " AND SMART0005.dbo.ACC_VOUCHER.COMP_VOUCHER_TYPE =16 AND SMART0005.dbo.ACC_COMPANY_VOUCHER.I_CL_PEN=1 ";
                //strSQL = strSQL + "AND ACC_LEDGER_Z_D_A.BRANCH_ID ='" + strBranchID + "' AND ACC_LEDGER_Z_D_A.LEDGER_STATUS =0 ";
                if (strBranchID != "")
                {
                    strSQL = strSQL + " AND SMART0005.dbo.ACC_LEDGER_Z_D_A.BRANCH_ID ='" + strBranchID + "' ";
                }

                if (intstatus < 2)
                {
                    strSQL = strSQL + "AND SMART0005.dbo.ACC_LEDGER_Z_D_A.LEDGER_STATUS  =" + intstatus + " ";
                }
                if (intOption == 2)
                {
                    if (strLedger != "")
                    {
                        strSQL = strSQL + "AND SMART0005.dbo.ACC_LEDGER_Z_D_A.LEDGER_NAME_MERZE IN(" + strLedger + ") ";
                    }
                }
                else if (intOption == 3)
                {
                    if (strLedger != "")
                    {
                        strSQL = strSQL + "AND SMART0005.dbo.ACC_LEDGER_Z_D_A.AREA IN(" + strLedger + ") ";
                    }
                }
                else if (intOption == 4)
                {
                    if (strLedger != "")
                    {
                        strSQL = strSQL + "AND SMART0005.dbo.ACC_LEDGER_Z_D_A.DIVISION IN(" + strLedger + ") ";
                    }
                }
                else if (intOption == 5)
                {
                    if (strLedger != "")
                    {
                        strSQL = strSQL + "AND SMART0005.dbo.ACC_LEDGER_Z_D_A.ZONE IN(" + strLedger + ") ";
                    }
                }
                if (gstrUserName != "")
                {
                    strSQL = strSQL + " AND SMART0005.dbo.ACC_LEDGER_Z_D_A.DIVISION in( select LEDGER_GROUP_NAME from SMART0005.dbo.USER_PRIVILEGES_COLOR WHERE USER_LOGIN_NAME ='" + gstrUserName + "')";
                }
                strSQL = strSQL + "GROUP BY  SMART0005.dbo.ACC_LEDGER_Z_D_A.ZONE,SMART0005.dbo.ACC_LEDGER_Z_D_A.DIVISION,SMART0005.dbo.ACC_LEDGER_Z_D_A.AREA,SMART0005.dbo.ACC_LEDGER_Z_D_A.TERRITORRY_NAME,SMART0005.dbo.ACC_LEDGER_Z_D_A.TERITORRY_CODE,SMART0005.dbo.ACC_LEDGER_Z_D_A.LEDGER_NAME_MERZE  ";
                cmdInsert.CommandText = strSQL;
                cmdInsert.ExecuteNonQuery();


                strSQL = "INSERT INTO ACC_MARKET_MONITROING_SHEET(ZONE,DIVISION,AREA,TERITORRY_NAME,TERITORRY_CODE,MR_NAME,PENDING,USER_NAME) ";
                strSQL = strSQL + "SELECT SMART0005.dbo.ACC_LEDGER_Z_D_A.ZONE,SMART0005.dbo.ACC_LEDGER_Z_D_A.DIVISION,SMART0005.dbo.ACC_LEDGER_Z_D_A.AREA,SMART0005.dbo.ACC_LEDGER_Z_D_A.TERRITORRY_NAME,SMART0005.dbo.ACC_LEDGER_Z_D_A.TERITORRY_CODE,SMART0005.dbo.ACC_LEDGER_Z_D_A.LEDGER_NAME_MERZE,ABS(ISNULL(SUM(SMART0005.dbo.ACC_VOUCHER.VOUCHER_DEBIT_AMOUNT-SMART0005.dbo.ACC_VOUCHER.VOUCHER_CREDIT_AMOUNT),0)) * -1 Collection, '" + gstrUserName + "'  ";
                strSQL = strSQL + "FROM SMART0005.dbo.ACC_LEDGER_Z_D_A,SMART0005.dbo.ACC_VOUCHER  ";
                strSQL = strSQL + "WHERE SMART0005.dbo.ACC_LEDGER_Z_D_A.LEDGER_NAME =SMART0005.dbo.ACC_VOUCHER.LEDGER_NAME  ";
                strSQL = strSQL + "AND SMART0005.dbo.ACC_VOUCHER.COMP_VOUCHER_DATE  <= " + Utility.cvtSQLDateString(strTDate) + " AND SMART0005.dbo.ACC_VOUCHER.COMP_VOUCHER_TYPE =1  ";
                //strSQL = strSQL + "AND ACC_LEDGER_Z_D_A.BRANCH_ID ='" + strBranchID + "' AND ACC_LEDGER_Z_D_A.LEDGER_STATUS =0 ";
                if (strBranchID != "")
                {
                    strSQL = strSQL + " AND SMART0005.dbo.ACC_LEDGER_Z_D_A.BRANCH_ID ='" + strBranchID + "' ";
                }

                if (intstatus < 2)
                {
                    strSQL = strSQL + "AND SMART0005.dbo.ACC_LEDGER_Z_D_A.LEDGER_STATUS  =" + intstatus + " ";
                }
                if (intOption == 2)
                {
                    if (strLedger != "")
                    {
                        strSQL = strSQL + "AND SMART0005.dbo.ACC_LEDGER_Z_D_A.LEDGER_NAME_MERZE IN(" + strLedger + ") ";
                    }
                }
                else if (intOption == 3)
                {
                    if (strLedger != "")
                    {
                        strSQL = strSQL + "AND SMART0005.dbo.ACC_LEDGER_Z_D_A.AREA IN(" + strLedger + ") ";
                    }
                }
                else if (intOption == 4)
                {
                    if (strLedger != "")
                    {
                        strSQL = strSQL + "AND SMART0005.dbo.ACC_LEDGER_Z_D_A.DIVISION IN(" + strLedger + ") ";
                    }
                }
                else if (intOption == 5)
                {
                    if (strLedger != "")
                    {
                        strSQL = strSQL + "AND SMART0005.dbo.ACC_LEDGER_Z_D_A.ZONE IN(" + strLedger + ") ";
                    }
                }
                if (gstrUserName != "")
                {
                    strSQL = strSQL + " AND SMART0005.dbo.ACC_LEDGER_Z_D_A.DIVISION in( select LEDGER_GROUP_NAME from SMART0005.dbo.USER_PRIVILEGES_COLOR WHERE USER_LOGIN_NAME ='" + gstrUserName + "')";
                }
                strSQL = strSQL + "GROUP BY  SMART0005.dbo.ACC_LEDGER_Z_D_A.ZONE,SMART0005.dbo.ACC_LEDGER_Z_D_A.DIVISION,SMART0005.dbo.ACC_LEDGER_Z_D_A.AREA,SMART0005.dbo.ACC_LEDGER_Z_D_A.TERRITORRY_NAME,SMART0005.dbo.ACC_LEDGER_Z_D_A.TERITORRY_CODE,SMART0005.dbo.ACC_LEDGER_Z_D_A.LEDGER_NAME_MERZE  ";
                cmdInsert.CommandText = strSQL;
                cmdInsert.ExecuteNonQuery();

                strSQL = "INSERT INTO ACC_MARKET_MONITROING_SHEET(ZONE,DIVISION,AREA,TERITORRY_NAME,TERITORRY_CODE,MR_NAME,PENDING,USER_NAME) ";
                strSQL = strSQL + "SELECT SMART0005.dbo.ACC_LEDGER_Z_D_A.ZONE,SMART0005.dbo.ACC_LEDGER_Z_D_A.DIVISION,SMART0005.dbo.ACC_LEDGER_Z_D_A.AREA,SMART0005.dbo.ACC_LEDGER_Z_D_A.TERRITORRY_NAME,SMART0005.dbo.ACC_LEDGER_Z_D_A.TERITORRY_CODE,SMART0005.dbo.ACC_LEDGER_Z_D_A.LEDGER_NAME_MERZE,ABS(ISNULL(SUM(SMART0005.dbo.ACC_VOUCHER.VOUCHER_DEBIT_AMOUNT),0)) *1 Adjustment, '" + gstrUserName + "'  ";
                strSQL = strSQL + "FROM SMART0005.dbo.ACC_LEDGER_Z_D_A,SMART0005.dbo.ACC_COMPANY_VOUCHER, SMART0005.dbo.ACC_VOUCHER   ";
                strSQL = strSQL + "WHERE SMART0005.dbo.ACC_LEDGER_Z_D_A.LEDGER_NAME =SMART0005.dbo.ACC_COMPANY_VOUCHER.LEDGER_NAME AND SMART0005.dbo.ACC_COMPANY_VOUCHER.COMP_REF_NO =SMART0005.dbo.ACC_VOUCHER.COMP_REF_NO  ";
                strSQL = strSQL + "AND SMART0005.dbo.ACC_VOUCHER.COMP_VOUCHER_DATE  <= " + Utility.cvtSQLDateString(strTDate) + " AND SMART0005.dbo.ACC_VOUCHER.COMP_VOUCHER_TYPE =3 AND SMART0005.dbo.ACC_COMPANY_VOUCHER.AUTOJV =1 ";
                //strSQL = strSQL + "AND ACC_LEDGER_Z_D_A.BRANCH_ID ='" + strBranchID + "' AND ACC_LEDGER_Z_D_A.LEDGER_STATUS =0 ";
                if (strBranchID != "")
                {
                    strSQL = strSQL + " AND SMART0005.dbo.ACC_LEDGER_Z_D_A.BRANCH_ID ='" + strBranchID + "' ";
                }

                if (intstatus < 2)
                {
                    strSQL = strSQL + "AND SMART0005.dbo.ACC_LEDGER_Z_D_A.LEDGER_STATUS  =" + intstatus + " ";
                }
                if (intOption == 2)
                {
                    if (strLedger != "")
                    {
                        strSQL = strSQL + "AND SMART0005.dbo.ACC_LEDGER_Z_D_A.LEDGER_NAME_MERZE IN(" + strLedger + ") ";
                    }
                }
                else if (intOption == 3)
                {
                    if (strLedger != "")
                    {
                        strSQL = strSQL + "AND SMART0005.dbo.ACC_LEDGER_Z_D_A.AREA IN(" + strLedger + ") ";
                    }
                }
                else if (intOption == 4)
                {
                    if (strLedger != "")
                    {
                        strSQL = strSQL + "AND SMART0005.dbo.ACC_LEDGER_Z_D_A.DIVISION IN(" + strLedger + ") ";
                    }
                }
                else if (intOption == 5)
                {
                    if (strLedger != "")
                    {
                        strSQL = strSQL + "AND SMART0005.dbo.ACC_LEDGER_Z_D_A.ZONE IN(" + strLedger + ") ";
                    }
                }
                if (gstrUserName != "")
                {
                    strSQL = strSQL + " AND SMART0005.dbo.ACC_LEDGER_Z_D_A.DIVISION in( select LEDGER_GROUP_NAME from SMART0005.dbo.USER_PRIVILEGES_COLOR WHERE USER_LOGIN_NAME ='" + gstrUserName + "')";
                }
                strSQL = strSQL + "GROUP BY  SMART0005.dbo.ACC_LEDGER_Z_D_A.ZONE,SMART0005.dbo.ACC_LEDGER_Z_D_A.DIVISION,SMART0005.dbo.ACC_LEDGER_Z_D_A.AREA,SMART0005.dbo.ACC_LEDGER_Z_D_A.TERRITORRY_NAME,SMART0005.dbo.ACC_LEDGER_Z_D_A.TERITORRY_CODE,SMART0005.dbo.ACC_LEDGER_Z_D_A.LEDGER_NAME_MERZE  ";
                cmdInsert.CommandText = strSQL;
                cmdInsert.ExecuteNonQuery();

                //--AUTO JV

                strSQL = "INSERT INTO SMART0005.dbo.ACC_MARKET_MONITROING_SHEET(ZONE,DIVISION,AREA,TERITORRY_NAME,TERITORRY_CODE,MR_NAME,PENDING,USER_NAME) ";
                strSQL = strSQL + "SELECT Z.ZONE,Z.DIVISION,Z.AREA,Z.TERRITORRY_NAME,Z.TERITORRY_CODE,Z.LEDGER_NAME_MERZE, ABS(ISNULL(SUM(aV.VOUCHER_CREDIT_AMOUNT-aV.VOUCHER_DEBIT_AMOUNT),0)) *-1 COLL_VOUCHER, '" + gstrUserName + "' ";
                strSQL = strSQL + "from SMART0005.dbo.ACC_LEDGERGROUP g,SMART0005.dbo.ACC_LEDGERGROUP_CATEGORY_VIEW v,SMART0005.dbo.ACC_LEDGER l, SMART0005.dbo.ACC_COMPANY_VOUCHER c,SMART0005.dbo.ACC_VOUCHER av, SMART0005.dbo.ACC_LEDGER_Z_D_A Z where g.GR_NAME=v.GR_PARENT  and v.GR_NAME=l.LEDGER_PARENT_GROUP  ";
                strSQL = strSQL + "and l.LEDGER_NAME =av.LEDGER_NAME  and l.LEDGER_NAME =Z.LEDGER_NAME  and c.COMP_REF_NO =av.COMP_REF_NO AND aV.COMP_VOUCHER_TYPE =3 and c.SP_JOURNAL=1 ";
                strSQL = strSQL + "AND C.COMP_VOUCHER_DATE <= " + Utility.cvtSQLDateString(strTDate) + "  ";
                strSQL = strSQL + " AND  C.AUTOJV =0  ";
                //strSQL = strSQL + "AND C.BRANCH_ID ='" + strBranchID + "'  ";
                //strSQL = strSQL + "AND l.LEDGER_STATUS =0  ";

                if (strBranchID != "")
                {
                    strSQL = strSQL + " AND C.BRANCH_ID ='" + strBranchID + "' ";
                }

                if (intstatus < 2)
                {
                    strSQL = strSQL + "AND l.LEDGER_STATUS  =" + intstatus + " ";
                }
                if (intOption == 2)
                {
                    if (strLedger != "")
                    {
                        strSQL = strSQL + "AND Z.LEDGER_NAME_MERZE IN(" + strLedger + ") ";
                    }
                }
                else if (intOption == 3)
                {
                    if (strLedger != "")
                    {
                        strSQL = strSQL + "AND Z.AREA IN(" + strLedger + ") ";
                    }
                }
                else if (intOption == 4)
                {
                    if (strLedger != "")
                    {
                        strSQL = strSQL + "AND Z.DIVISION IN(" + strLedger + ") ";
                    }
                }
                else if (intOption == 5)
                {
                    if (strLedger != "")
                    {
                        strSQL = strSQL + "AND Z.ZONE IN(" + strLedger + ") ";
                    }
                }
                if (gstrUserName != "")
                {
                    strSQL = strSQL + " AND Z.DIVISION in( select LEDGER_GROUP_NAME from SMART0005.dbo.USER_PRIVILEGES_COLOR WHERE USER_LOGIN_NAME ='" + gstrUserName + "')";
                }
                strSQL = strSQL + "GROUP BY  Z.ZONE,Z.DIVISION,Z.AREA,Z.TERRITORRY_NAME,Z.TERITORRY_CODE,Z.LEDGER_NAME_MERZE  ";
                cmdInsert.CommandText = strSQL;
                cmdInsert.ExecuteNonQuery();

                //--AUTO JV
                strSQL = "INSERT INTO SMART0005.dbo.ACC_MARKET_MONITROING_SHEET(ZONE,DIVISION,AREA,TERITORRY_NAME,TERITORRY_CODE,MR_NAME,PENDING,USER_NAME) ";
                strSQL = strSQL + "SELECT  ZDA.ZONE , ZDA.DIVISION , ZDA.AREA  ,ZDA.TERRITORRY_NAME,ZDA.TERITORRY_CODE,l.LEDGER_NAME_MERZE, isnull(SUM(v.VOUCHER_CREDIT_AMOUNT-v.VOUCHER_DEBIT_AMOUNT),0) *-1 dblpf, '" + gstrUserName + "' ";
                strSQL = strSQL + "FROM SMART0005.dbo.ACC_LEDGER_Z_D_A ZDA ,SMART0005.dbo.ACC_VOUCHER v,SMART0005.dbo.ACC_LEDGER L,SMART0005.dbo.ACC_COMPANY_VOUCHER c  WHERE L.LEDGER_NAME =ZDA.LEDGER_NAME  ";
                strSQL = strSQL + "AND V.LEDGER_NAME =L.LEDGER_NAME  and c.COMP_REF_NO =v.COMP_REF_NO AND (V.COMP_VOUCHER_DATE <=   " + Utility.cvtSQLDateString(strTDate) + ")  ";
                strSQL = strSQL + "AND V.COMP_VOUCHER_TYPE=3   and c.SP_JOURNAL IN(0,2) and v.VOUCHER_TOBY ='Cr'  ";
                //strSQL = strSQL + "  AND l.LEDGER_STATUS =0 AND L.BRANCH_ID ='" + strBranchID + "' ";
                if (strBranchID != "")
                {
                    strSQL = strSQL + " AND L.BRANCH_ID ='" + strBranchID + "' ";
                }

                if (intstatus < 2)
                {
                    strSQL = strSQL + "AND l.LEDGER_STATUS  =" + intstatus + " ";
                }
                if (intOption == 2)
                {
                    if (strLedger != "")
                    {
                        strSQL = strSQL + "AND ZDA.LEDGER_NAME_MERZE IN(" + strLedger + ") ";
                    }
                }
                else if (intOption == 3)
                {
                    if (strLedger != "")
                    {
                        strSQL = strSQL + "AND ZDA.AREA IN(" + strLedger + ") ";
                    }
                }
                else if (intOption == 4)
                {
                    if (strLedger != "")
                    {
                        strSQL = strSQL + "AND ZDA.DIVISION IN(" + strLedger + ") ";
                    }
                }
                else if (intOption == 5)
                {
                    if (strLedger != "")
                    {
                        strSQL = strSQL + "AND ZDA.ZONE IN(" + strLedger + ") ";
                    }
                }
                if (gstrUserName != "")
                {
                    strSQL = strSQL + " AND ZDA.DIVISION in( select LEDGER_GROUP_NAME from SMART0005.dbo.USER_PRIVILEGES_COLOR WHERE USER_LOGIN_NAME ='" + gstrUserName + "')";
                }

                strSQL = strSQL + "GROUP BY  ZDA.ZONE,ZDA.DIVISION,ZDA.AREA,ZDA.TERRITORRY_NAME,ZDA.TERITORRY_CODE,l.LEDGER_NAME_MERZE  ";
                cmdInsert.CommandText = strSQL;
                cmdInsert.ExecuteNonQuery();

                //--Corpo
                strSQL = "INSERT INTO SMART0005.dbo.ACC_MARKET_MONITROING_SHEET(ZONE,DIVISION,AREA,TERITORRY_NAME,TERITORRY_CODE,MR_NAME,PENDING,USER_NAME) ";
                strSQL = strSQL + "SELECT SMART0005.dbo.ACC_LEDGER_Z_D_A.ZONE,SMART0005.dbo.ACC_LEDGER_Z_D_A.DIVISION,SMART0005.dbo.ACC_LEDGER_Z_D_A.AREA,SMART0005.dbo.ACC_LEDGER_Z_D_A.TERRITORRY_NAME,SMART0005.dbo.ACC_LEDGER_Z_D_A.TERITORRY_CODE,SMART0005.dbo.ACC_LEDGER_Z_D_A.LEDGER_NAME_MERZE,ABS(ISNULL(SUM(v.VOUCHER_DEBIT_AMOUNT),0)) *-1  CorporetSales, '" + gstrUserName + "'  ";
                strSQL = strSQL + "FROM SMART0005.dbo.ACC_VOUCHER v,SMART0005.dbo.ACC_COMPANY_VOUCHER CV,  ";
                strSQL = strSQL + "SMART0005.dbo.ACC_LEDGER C,SMART0005.dbo.ACC_LEDGER_Z_D_A   WHERE V.COMP_REF_NO=CV.COMP_REF_NO AND  V.LEDGER_NAME=CV.LEDGER_NAME  AND  CV.SALES_REP=C.LEDGER_NAME   ";
                strSQL = strSQL + "AND SMART0005.dbo.ACC_LEDGER_Z_D_A.LEDGER_NAME=v.LEDGER_NAME  AND V.COMP_VOUCHER_DATE <=  " + Utility.cvtSQLDateString(strTDate) + "  ";
                //strSQL = strSQL + "AND CV.BRANCH_ID ='" + strBranchID + "' AND C.LEDGER_STATUS  =0  ";
                strSQL = strSQL + " AND CV.COMP_VOUCHER_TYPE =16 ";
                strSQL = strSQL + "AND C.LEDGER_PARENT_GROUP  ='COR (Corporate Customer)'  ";
                if (strBranchID != "")
                {
                    strSQL = strSQL + " AND CV.BRANCH_ID ='" + strBranchID + "' ";
                }

                if (intstatus < 2)
                {
                    strSQL = strSQL + "AND C.LEDGER_STATUS  =" + intstatus + " ";
                }
                if (intOption == 2)
                {
                    if (strLedger != "")
                    {
                        strSQL = strSQL + "AND SMART0005.dbo.ACC_LEDGER_Z_D_A.LEDGER_NAME_MERZE IN(" + strLedger + ") ";
                    }
                }
                else if (intOption == 3)
                {
                    if (strLedger != "")
                    {
                        strSQL = strSQL + "AND SMART0005.dbo.ACC_LEDGER_Z_D_A.AREA IN(" + strLedger + ") ";
                    }
                }
                else if (intOption == 4)
                {
                    if (strLedger != "")
                    {
                        strSQL = strSQL + "AND SMART0005.dbo.ACC_LEDGER_Z_D_A.DIVISION IN(" + strLedger + ") ";
                    }
                }
                else if (intOption == 5)
                {
                    if (strLedger != "")
                    {
                        strSQL = strSQL + "AND SMART0005.dbo.ACC_LEDGER_Z_D_A.ZONE IN(" + strLedger + ") ";
                    }
                }
                if (gstrUserName != "")
                {
                    strSQL = strSQL + " AND SMART0005.dbo.ACC_LEDGER_Z_D_A.DIVISION in( select LEDGER_GROUP_NAME from SMART0005.dbo.USER_PRIVILEGES_COLOR WHERE USER_LOGIN_NAME ='" + gstrUserName + "')";
                }
                strSQL = strSQL + "GROUP BY  SMART0005.dbo.ACC_LEDGER_Z_D_A.ZONE,SMART0005.dbo.ACC_LEDGER_Z_D_A.DIVISION,SMART0005.dbo.ACC_LEDGER_Z_D_A.AREA,SMART0005.dbo.ACC_LEDGER_Z_D_A.TERRITORRY_NAME,SMART0005.dbo.ACC_LEDGER_Z_D_A.TERITORRY_CODE,SMART0005.dbo.ACC_LEDGER_Z_D_A.LEDGER_NAME_MERZE  ";
                cmdInsert.CommandText = strSQL;
                cmdInsert.ExecuteNonQuery();

                //CorporetColl
                strSQL = "INSERT INTO ACC_MARKET_MONITROING_SHEET(ZONE,DIVISION,AREA,TERITORRY_NAME,TERITORRY_CODE,MR_NAME,PENDING,USER_NAME) ";
                strSQL = strSQL + "SELECT SMART0005.dbo.ACC_LEDGER_Z_D_A.ZONE,SMART0005.dbo.ACC_LEDGER_Z_D_A.DIVISION,SMART0005.dbo.ACC_LEDGER_Z_D_A.AREA,SMART0005.dbo.ACC_LEDGER_Z_D_A.TERRITORRY_NAME,SMART0005.dbo.ACC_LEDGER_Z_D_A.TERITORRY_CODE,SMART0005.dbo.ACC_LEDGER_Z_D_A.LEDGER_NAME_MERZE,ABS(ISNULL(SUM(SMART0005.dbo.VECTOR_TRANSACTION.VT_TRAN_AMOUNT),0))*1 CorporetColl , '" + gstrUserName + "'   ";
                strSQL = strSQL + "FROM SMART0005.dbo.ACC_COMPANY_VOUCHER,SMART0005.dbo.VECTOR_TRANSACTION,SMART0005.dbo.ACC_LEDGER_Z_D_A,SMART0005.dbo.ACC_LEDGER  ";
                strSQL = strSQL + "WHERE  SMART0005.dbo.ACC_COMPANY_VOUCHER.COMP_REF_NO =SMART0005.dbo.VECTOR_TRANSACTION.COMP_REF_NO   ";
                strSQL = strSQL + "AND SMART0005.dbo.ACC_LEDGER_Z_D_A.LEDGER_NAME =SMART0005.dbo.ACC_COMPANY_VOUCHER.LEDGER_NAME AND SMART0005.dbo.VECTOR_TRANSACTION.LEDGER_NAME =SMART0005.dbo.ACC_LEDGER.LEDGER_NAME   ";
                strSQL = strSQL + "AND SMART0005.dbo.ACC_COMPANY_VOUCHER.COMP_VOUCHER_DATE <=  " + Utility.cvtSQLDateString(strTDate) + "  ";
                //strSQL = strSQL + "AND ACC_COMPANY_VOUCHER.BRANCH_ID ='" + strBranchID + "' AND ACC_LEDGER_Z_D_A.LEDGER_STATUS  =0  ";
                strSQL = strSQL + "AND SMART0005.dbo.ACC_COMPANY_VOUCHER.COMP_VOUCHER_TYPE =1 ";
                strSQL = strSQL + "AND SMART0005.dbo.ACC_LEDGER.LEDGER_PARENT_GROUP  ='COR (Corporate Customer)'  ";
                if (strBranchID != "")
                {
                    strSQL = strSQL + " AND SMART0005.dbo.ACC_COMPANY_VOUCHER.BRANCH_ID ='" + strBranchID + "' ";
                }

                if (intstatus < 2)
                {
                    strSQL = strSQL + "AND SMART0005.dbo.ACC_LEDGER_Z_D_A.LEDGER_STATUS  =" + intstatus + " ";
                }
                if (intOption == 2)
                {
                    if (strLedger != "")
                    {
                        strSQL = strSQL + "AND SMART0005.dbo.ACC_LEDGER_Z_D_A.LEDGER_NAME_MERZE IN(" + strLedger + ") ";
                    }
                }
                else if (intOption == 3)
                {
                    if (strLedger != "")
                    {
                        strSQL = strSQL + "AND SMART0005.dbo.ACC_LEDGER_Z_D_A.AREA IN(" + strLedger + ") ";
                    }
                }
                else if (intOption == 4)
                {
                    if (strLedger != "")
                    {
                        strSQL = strSQL + "AND SMART0005.dbo.ACC_LEDGER_Z_D_A.DIVISION IN(" + strLedger + ") ";
                    }
                }
                else if (intOption == 5)
                {
                    if (strLedger != "")
                    {
                        strSQL = strSQL + "AND SMART0005.dbo.ACC_LEDGER_Z_D_A.ZONE IN(" + strLedger + ") ";
                    }
                }
                if (gstrUserName != "")
                {
                    strSQL = strSQL + " AND SMART0005.dbo.ACC_LEDGER_Z_D_A.DIVISION in( select LEDGER_GROUP_NAME from SMART0005.dbo.USER_PRIVILEGES_COLOR WHERE USER_LOGIN_NAME ='" + gstrUserName + "')";
                }
                strSQL = strSQL + "GROUP BY  SMART0005.dbo.ACC_LEDGER_Z_D_A.ZONE,SMART0005.dbo.ACC_LEDGER_Z_D_A.DIVISION,SMART0005.dbo.ACC_LEDGER_Z_D_A.AREA,SMART0005.dbo.ACC_LEDGER_Z_D_A.TERRITORRY_NAME,SMART0005.dbo.ACC_LEDGER_Z_D_A.TERITORRY_CODE,SMART0005.dbo.ACC_LEDGER_Z_D_A.LEDGER_NAME_MERZE  ";
                cmdInsert.CommandText = strSQL;
                cmdInsert.ExecuteNonQuery();


                //CorporetAdjustment
                strSQL = "INSERT INTO SMART0005.dbo.ACC_MARKET_MONITROING_SHEET(ZONE,DIVISION,AREA,TERITORRY_NAME,TERITORRY_CODE,MR_NAME,PENDING,USER_NAME) ";
                strSQL = strSQL + "SELECT SMART0005.dbo.ACC_LEDGER_Z_D_A.ZONE,SMART0005.dbo.ACC_LEDGER_Z_D_A.DIVISION,SMART0005.dbo.ACC_LEDGER_Z_D_A.AREA,SMART0005.dbo.ACC_LEDGER_Z_D_A.TERRITORRY_NAME,SMART0005.dbo.ACC_LEDGER_Z_D_A.TERITORRY_CODE,SMART0005.dbo.ACC_LEDGER_Z_D_A.LEDGER_NAME_MERZE,ABS(ISNULL(SUM(SMART0005.dbo.VECTOR_TRANSACTION.VT_TRAN_AMOUNT),0))*1 CorporetAdjustment, '" + gstrUserName + "'   ";
                strSQL = strSQL + "FROM SMART0005.dbo.ACC_COMPANY_VOUCHER,SMART0005.dbo.VECTOR_TRANSACTION,SMART0005.dbo.ACC_LEDGER_Z_D_A,SMART0005.dbo.ACC_LEDGER  ";
                strSQL = strSQL + "WHERE  SMART0005.dbo.ACC_COMPANY_VOUCHER.COMP_REF_NO =SMART0005.dbo.VECTOR_TRANSACTION.COMP_REF_NO  ";
                strSQL = strSQL + "AND SMART0005.dbo.ACC_LEDGER_Z_D_A.LEDGER_NAME =SMART0005.dbo.ACC_COMPANY_VOUCHER.LEDGER_NAME AND SMART0005.dbo.VECTOR_TRANSACTION.LEDGER_NAME =SMART0005.dbo.ACC_LEDGER.LEDGER_NAME   ";
                strSQL = strSQL + " AND SMART0005.dbo.ACC_COMPANY_VOUCHER.COMP_VOUCHER_DATE <=  " + Utility.cvtSQLDateString(strTDate) + "  ";
                //strSQL = strSQL + "AND ACC_COMPANY_VOUCHER.BRANCH_ID ='" + strBranchID + "' AND ACC_LEDGER_Z_D_A.LEDGER_STATUS  =0  ";
                strSQL = strSQL + "AND SMART0005.dbo.ACC_COMPANY_VOUCHER.COMP_VOUCHER_TYPE =3 ";
                strSQL = strSQL + "AND SMART0005.dbo.ACC_LEDGER.LEDGER_PARENT_GROUP  ='COR (Corporate Customer)'  ";
                if (strBranchID != "")
                {
                    strSQL = strSQL + " AND SMART0005.dbo.ACC_COMPANY_VOUCHER.BRANCH_ID ='" + strBranchID + "' ";
                }

                if (intstatus < 2)
                {
                    strSQL = strSQL + "AND SMART0005.dbo.ACC_LEDGER_Z_D_A.LEDGER_STATUS  =" + intstatus + " ";
                }
                if (intOption == 2)
                {
                    if (strLedger != "")
                    {
                        strSQL = strSQL + "AND SMART0005.dbo.ACC_LEDGER_Z_D_A.LEDGER_NAME_MERZE IN(" + strLedger + ") ";
                    }
                }
                else if (intOption == 3)
                {
                    if (strLedger != "")
                    {
                        strSQL = strSQL + "AND SMART0005.dbo.ACC_LEDGER_Z_D_A.AREA IN(" + strLedger + ") ";
                    }
                }
                else if (intOption == 4)
                {
                    if (strLedger != "")
                    {
                        strSQL = strSQL + "AND SMART0005.dbo.ACC_LEDGER_Z_D_A.DIVISION IN(" + strLedger + ") ";
                    }
                }
                else if (intOption == 5)
                {
                    if (strLedger != "")
                    {
                        strSQL = strSQL + "AND SMART0005.dbo.ACC_LEDGER_Z_D_A.ZONE IN(" + strLedger + ") ";
                    }
                }
                if (gstrUserName != "")
                {
                    strSQL = strSQL + " AND SMART0005.dbo.ACC_LEDGER_Z_D_A.DIVISION in( select LEDGER_GROUP_NAME from SMART0005.dbo.USER_PRIVILEGES_COLOR WHERE USER_LOGIN_NAME ='" + gstrUserName + "')";
                }
                strSQL = strSQL + "GROUP BY  SMART0005.dbo.ACC_LEDGER_Z_D_A.ZONE,SMART0005.dbo.ACC_LEDGER_Z_D_A.DIVISION,SMART0005.dbo.ACC_LEDGER_Z_D_A.AREA,SMART0005.dbo.ACC_LEDGER_Z_D_A.TERRITORRY_NAME,SMART0005.dbo.ACC_LEDGER_Z_D_A.TERITORRY_CODE,SMART0005.dbo.ACC_LEDGER_Z_D_A.LEDGER_NAME_MERZE  ";
                cmdInsert.CommandText = strSQL;
                cmdInsert.ExecuteNonQuery();

                //Dailutionpending Order
                strSQL = "INSERT INTO SMART0005.dbo.ACC_MARKET_MONITROING_SHEET(ZONE,DIVISION,AREA,TERITORRY_NAME,TERITORRY_CODE,MR_NAME,PENDING,USER_NAME) ";
                //strSQL = "SELECT abs( ISNULL(SUM(ACC_COMPANY_VOUCHER.COMP_VOUCHER_NET_AMOUNT), 0))*1 as COMP_VOUCHER_NET_AMOUNT  ";
                strSQL = strSQL + "SELECT SMART0005.dbo.ACC_LEDGER_Z_D_A.ZONE,SMART0005.dbo.ACC_LEDGER_Z_D_A.DIVISION,SMART0005.dbo.ACC_LEDGER_Z_D_A.AREA,SMART0005.dbo.ACC_LEDGER_Z_D_A.TERRITORRY_NAME,SMART0005.dbo.ACC_LEDGER_Z_D_A.TERITORRY_CODE,SMART0005.dbo.ACC_LEDGER_Z_D_A.LEDGER_NAME_MERZE, abs( ISNULL(SUM(SMART0005.dbo.ACC_COMPANY_VOUCHER.COMP_VOUCHER_NET_AMOUNT), 0))*1 Dailutionpending, '" + gstrUserName + "'   ";
                strSQL = strSQL + "FROM SMART0005.dbo.ACC_COMPANY_VOUCHER INNER JOIN ";
                strSQL = strSQL + "SMART0005.dbo.ACC_LEDGER_Z_D_A ON SMART0005.dbo.ACC_COMPANY_VOUCHER.LEDGER_NAME = SMART0005.dbo.ACC_LEDGER_Z_D_A.LEDGER_NAME INNER JOIN ";
                strSQL = strSQL + "ACC_LEDGER AS l ON ACC_COMPANY_VOUCHER.LEDGER_NAME = l.LEDGER_NAME ";
                strSQL = strSQL + "WHERE SMART0005.dbo.ACC_COMPANY_VOUCHER.COMP_VOUCHER_DATE <=" + Utility.cvtSQLDateString(strTDate) + " AND (SMART0005.dbo.ACC_COMPANY_VOUCHER.COMP_VOUCHER_TYPE = 12) ";
                strSQL = strSQL + "AND SMART0005.dbo.ACC_COMPANY_VOUCHER.DIL_FREEZE IN(2,8)  ";
                // strSQL = strSQL + "ACC_COMPANY_VOUCHER.BRANCH_ID = '" + strBranchID + "'  
                strSQL = strSQL + "AND (SMART0005.dbo.ACC_COMPANY_VOUCHER.COMP_VOUCHER_STATUS = 0) ";
                if (strBranchID != "")
                {
                    strSQL = strSQL + " AND SMART0005.dbo.ACC_COMPANY_VOUCHER.BRANCH_ID ='" + strBranchID + "' ";
                }

                if (intstatus < 2)
                {
                    strSQL = strSQL + "AND SMART0005.dbo.ACC_LEDGER_Z_D_A.LEDGER_STATUS  =" + intstatus + " ";
                }
                if (intOption == 2)
                {
                    if (strLedger != "")
                    {
                        strSQL = strSQL + "AND SMART0005.dbo.ACC_LEDGER_Z_D_A.LEDGER_NAME_MERZE IN(" + strLedger + ") ";
                    }
                }
                else if (intOption == 3)
                {
                    if (strLedger != "")
                    {
                        strSQL = strSQL + "AND SMART0005.dbo.ACC_LEDGER_Z_D_A.AREA IN(" + strLedger + ") ";
                    }
                }
                else if (intOption == 4)
                {
                    if (strLedger != "")
                    {
                        strSQL = strSQL + "AND SMART0005.dbo.ACC_LEDGER_Z_D_A.DIVISION IN(" + strLedger + ") ";
                    }
                }
                else if (intOption == 5)
                {
                    if (strLedger != "")
                    {
                        strSQL = strSQL + "AND SMART0005.dbo.ACC_LEDGER_Z_D_A.ZONE IN(" + strLedger + ") ";
                    }
                }
                if (gstrUserName != "")
                {
                    strSQL = strSQL + " AND SMART0005.dbo.ACC_LEDGER_Z_D_A.DIVISION in( select LEDGER_GROUP_NAME from SMART0005.dbo.USER_PRIVILEGES_COLOR WHERE USER_LOGIN_NAME ='" + gstrUserName + "')";
                }
                strSQL = strSQL + "GROUP BY  SMART0005.dbo.ACC_LEDGER_Z_D_A.ZONE,SMART0005.dbo.ACC_LEDGER_Z_D_A.DIVISION,SMART0005.dbo.ACC_LEDGER_Z_D_A.AREA,SMART0005.dbo.ACC_LEDGER_Z_D_A.TERRITORRY_NAME,SMART0005.dbo.ACC_LEDGER_Z_D_A.TERITORRY_CODE,SMART0005.dbo.ACC_LEDGER_Z_D_A.LEDGER_NAME_MERZE  ";
                cmdInsert.CommandText = strSQL;
                cmdInsert.ExecuteNonQuery();

                strSQL = "INSERT INTO SMART0005.dbo.ACC_MARKET_MONITROING_SHEET(ZONE,DIVISION,AREA,TERITORRY_NAME,TERITORRY_CODE,MR_NAME,PENDING,USER_NAME) ";
                strSQL = strSQL + "SELECT  SMART0005.dbo.ACC_LEDGER_Z_D_A.ZONE,SMART0005.dbo.ACC_LEDGER_Z_D_A.DIVISION,SMART0005.dbo.ACC_LEDGER_Z_D_A.AREA,SMART0005.dbo.ACC_LEDGER_Z_D_A.TERRITORRY_NAME,SMART0005.dbo.ACC_LEDGER_Z_D_A.TERITORRY_CODE,SMART0005.dbo.ACC_LEDGER_Z_D_A.LEDGER_NAME_MERZE,SUM(SMART0005.dbo.ACC_VOUCHER.VOUCHER_DEBIT_AMOUNT -SMART0005.dbo.ACC_VOUCHER.VOUCHER_CREDIT_AMOUNT) COMP_VOUCHER_NET_AMOUNT, '" + gstrUserName + "'  from SMART0005.dbo.ACC_COMPANY_VOUCHER ,SMART0005.dbo.ACC_VOUCHER,ACC_LEDGER_Z_D_A  ";
                strSQL = strSQL + "where SMART0005.dbo.ACC_COMPANY_VOUCHER.COMP_REF_NO =SMART0005.dbo.ACC_VOUCHER.COMP_REF_NO AND SMART0005.dbo.ACC_LEDGER_Z_D_A.LEDGER_NAME =SMART0005.dbo.ACC_VOUCHER.LEDGER_NAME  and SMART0005.dbo.ACC_COMPANY_VOUCHER.COMP_VOUCHER_TYPE =3 ";
                strSQL = strSQL + "and SMART0005.dbo.ACC_COMPANY_VOUCHER.COMP_VOUCHER_DATE <= " + Utility.cvtSQLDateString(strTDate) + " ";
                //strSQL = strSQL + "and ACC_VOUCHER.LEDGER_NAME ='" + vstrLedgerName.Replace("'", "''") + "' ";
                //strSQL = strSQL + "AND COMP_VOUCHER_NARRATION LIKE '%yELLOW%' ";
                strSQL = strSQL + "AND SMART0005.dbo.ACC_VOUCHER.TRANSFER_TYPE =1  and SMART0005.dbo.ACC_VOUCHER.autojv=0  and SMART0005.dbo.ACC_VOUCHER.VOUCHER_TOBY ='Dr' ";
                if (strBranchID != "")
                {
                    strSQL = strSQL + " AND SMART0005.dbo.ACC_COMPANY_VOUCHER.BRANCH_ID ='" + strBranchID + "' ";
                }

                if (intstatus < 2)
                {
                    strSQL = strSQL + "AND SMART0005.dbo.ACC_LEDGER_Z_D_A.LEDGER_STATUS  =" + intstatus + " ";
                }
                if (intOption == 2)
                {
                    if (strLedger != "")
                    {
                        strSQL = strSQL + "AND SMART0005.dbo.ACC_LEDGER_Z_D_A.LEDGER_NAME_MERZE IN(" + strLedger + ") ";
                    }
                }
                else if (intOption == 3)
                {
                    if (strLedger != "")
                    {
                        strSQL = strSQL + "AND SMART0005.dbo.ACC_LEDGER_Z_D_A.AREA IN(" + strLedger + ") ";
                    }
                }
                else if (intOption == 4)
                {
                    if (strLedger != "")
                    {
                        strSQL = strSQL + "AND SMART0005.dbo.ACC_LEDGER_Z_D_A.DIVISION IN(" + strLedger + ") ";
                    }
                }
                else if (intOption == 5)
                {
                    if (strLedger != "")
                    {
                        strSQL = strSQL + "AND ACC_LEDGER_Z_D_A.ZONE IN(" + strLedger + ") ";
                    }
                }
                if (gstrUserName != "")
                {
                    strSQL = strSQL + " AND SMART0005.dbo.ACC_LEDGER_Z_D_A.DIVISION in( select LEDGER_GROUP_NAME from SMART0005.dbo.USER_PRIVILEGES_COLOR WHERE USER_LOGIN_NAME ='" + gstrUserName + "')";
                }
                strSQL = strSQL + "GROUP BY SMART0005.dbo.ACC_LEDGER_Z_D_A.ZONE,SMART0005.dbo.ACC_LEDGER_Z_D_A.DIVISION,SMART0005.dbo.ACC_LEDGER_Z_D_A.AREA,SMART0005.dbo.ACC_LEDGER_Z_D_A.TERRITORRY_NAME,SMART0005.dbo.ACC_LEDGER_Z_D_A.TERITORRY_CODE,SMART0005.dbo.ACC_LEDGER_Z_D_A.LEDGER_NAME_MERZE ";
                cmdInsert.CommandText = strSQL;
                cmdInsert.ExecuteNonQuery();
                #endregion
                #region "View"
                if (intBaseTarget == 2)
                {
                    strSQL = "ALTER VIEW SMART0005.dbo.ACC_Commission_View AS  ";
                    strSQL = strSQL + "SELECT SMART0005.dbo.ACC_MARKET_MONITROING_SHEET.USER_NAME, '' TEAM_NAME,SMART0005.dbo.ACC_LEDGER_Z_D_A.LEDGER_NAME, SMART0005.dbo.ACC_MARKET_MONITROING_SHEET.ZONE,SMART0005.dbo.ACC_MARKET_MONITROING_SHEET.DIVISION,SMART0005.dbo.ACC_MARKET_MONITROING_SHEET.AREA,SMART0005.dbo.ACC_MARKET_MONITROING_SHEET.TERITORRY_NAME,SMART0005.dbo.ACC_MARKET_MONITROING_SHEET.TERITORRY_CODE,SMART0005.dbo.ACC_MARKET_MONITROING_SHEET.MR_NAME,SMART0005.dbo.ACC_LEDGER_Z_D_A.LEDGER_CLASS, ";
                    strSQL = strSQL + "ISNULL(SUM(SMART0005.dbo.ACC_MARKET_MONITROING_SHEET.TOTAL_OS),0) TOTAL_OS,isnull(SUM(SMART0005.dbo.ACC_MARKET_MONITROING_SHEET.COMMITMENT),0) COMMITMENT,ISNULL(SUM(SMART0005.dbo.ACC_MARKET_MONITROING_SHEET.COLLECTION_AMNT),0) COLLECTION_AMNT, ";
                    strSQL = strSQL + "ISNULL(SUM(SMART0005.dbo.ACC_MARKET_MONITROING_SHEET.SALES_TARGET),0) SALES_TARGET, ISNULL(SUM(SMART0005.dbo.ACC_MARKET_MONITROING_SHEET.ACHIEVE),0) SALES, ";
                    strSQL = strSQL + "(case when ISNULL(SUM(SMART0005.dbo.ACC_MARKET_MONITROING_SHEET.SALES_TARGET),0)  <> 0 then (ISNULL(SUM(SMART0005.dbo.ACC_MARKET_MONITROING_SHEET.ACHIEVE),0)*100)/  ISNULL(SUM(SMART0005.dbo.ACC_MARKET_MONITROING_SHEET.SALES_TARGET),0)  else 0 end ) AchPerSales , ";
                    strSQL = strSQL + "(case when isnull(SUM(SMART0005.dbo.ACC_MARKET_MONITROING_SHEET.COMMITMENT),0)   <> 0 then (ISNULL(SUM(SMART0005.dbo.ACC_MARKET_MONITROING_SHEET.COLLECTION_AMNT),0) *100)/ isnull(SUM(SMART0005.dbo.ACC_MARKET_MONITROING_SHEET.COMMITMENT),0)   else 0 end ) AchPerColl , ";
                    strSQL = strSQL + "ISNULL(SUM(SMART0005.dbo.ACC_MARKET_MONITROING_SHEET.SALES_RETURN),0) SALES_RETURN, ISNULL(SUM(SMART0005.dbo.ACC_MARKET_MONITROING_SHEET.JV_DEBIT),0) JV_DEBIT, ";
                    strSQL = strSQL + "ISNULL(SUM(SMART0005.dbo.ACC_MARKET_MONITROING_SHEET.JV_CREDIT),0) JV_CREDIT,";
                    strSQL = strSQL + "ISNULL(SUM(SMART0005.dbo.ACC_MARKET_MONITROING_SHEET.CLOSING_VAL),0) ClosingBalnce,";
                    strSQL = strSQL + "ISNULL(SUM(SMART0005.dbo.ACC_MARKET_MONITROING_SHEET.Credit_Limit),0) Credit_Limit,";
                    strSQL = strSQL + "ISNULL(SUM(SMART0005.dbo.ACC_MARKET_MONITROING_SHEET.SP_VOUCHER),0) SP_VOUCHER,ISNULL(SUM(PENDING),0)PENDING FROM SMART0005.dbo.ACC_MARKET_MONITROING_SHEET,SMART0005.dbo.ACC_LEDGER_Z_D_A ";
                    strSQL = strSQL + "WHERE  SMART0005.dbo.ACC_MARKET_MONITROING_SHEET.MR_NAME =SMART0005.dbo.ACC_LEDGER_Z_D_A.LEDGER_NAME_MERZE ";
                    strSQL = strSQL + "AND SMART0005.dbo.ACC_MARKET_MONITROING_SHEET.USER_NAME ='" + gstrUserName + "' ";
                    if (intOption == 2)
                    {
                        if (strLedger != "")
                        {
                            strSQL = strSQL + "AND SMART0005.dbo.ACC_MARKET_MONITROING_SHEET.LEDGER_NAME_MERZE IN(" + strLedger + ") ";
                        }
                    }
                    else if (intOption == 3)
                    {
                        if (strLedger != "")
                        {
                            strSQL = strSQL + "AND SMART0005.dbo.ACC_MARKET_MONITROING_SHEET.AREA IN(" + strLedger + ") ";
                        }
                    }
                    else if (intOption == 4)
                    {
                        if (strLedger != "")
                        {
                            strSQL = strSQL + "AND SMART0005.dbo.ACC_MARKET_MONITROING_SHEET.DIVISION IN(" + strLedger + ") ";
                        }
                    }
                    else if (intOption == 5)
                    {
                        if (strLedger != "")
                        {
                            strSQL = strSQL + "AND SMART0005.dbo.ACC_MARKET_MONITROING_SHEET.ZONE IN(" + strLedger + ") ";
                        }
                    }
                    if (gstrUserName != "")
                    {
                        strSQL = strSQL + " AND SMART0005.dbo.ACC_LEDGER_Z_D_A.DIVISION in( select LEDGER_GROUP_NAME from SMART0005.dbo.USER_PRIVILEGES_COLOR WHERE USER_LOGIN_NAME ='" + gstrUserName + "')";
                    }
                    strSQL = strSQL + "GROUP by  SMART0005.dbo.ACC_LEDGER_Z_D_A.LEDGER_NAME, SMART0005.dbo.ACC_MARKET_MONITROING_SHEET.ZONE,SMART0005.dbo.ACC_MARKET_MONITROING_SHEET.DIVISION,SMART0005.dbo.ACC_MARKET_MONITROING_SHEET.AREA,SMART0005.dbo.ACC_MARKET_MONITROING_SHEET.TERITORRY_NAME, ";
                    strSQL = strSQL + "SMART0005.dbo.ACC_MARKET_MONITROING_SHEET.TERITORRY_CODE,SMART0005.dbo.ACC_MARKET_MONITROING_SHEET.MR_NAME,SMART0005.dbo.ACC_LEDGER_Z_D_A.LEDGER_CLASS ";

                    cmdInsert.CommandText = strSQL;
                    cmdInsert.ExecuteNonQuery();
                }
                #endregion
                cmdInsert.Transaction.Commit();
                List<RFinalStatement> ooAccLedger = new List<RFinalStatement>();

                if (intBaseTarget == 2) //iNCENTIVE rEWARD 2
                {
                    if (intOption == 5)
                    {
                        strSQL = "select * from  (select   1 RowNO,ZONE,LEDGER_NAME,TEAM_NAME,AchPerSales,AchPerColl,TERITORRY_NAME,TERITORRY_CODE,MR_NAME,LEDGER_CLASS,COMMITMENT,COLLECTION_AMNT,SALES_TARGET,SALES,SP_VOUCHER,ClosingBalnce,Credit_Limit,PENDING from ACC_Commission_View ";
                        strSQL = strSQL + "where AchPerSales>=100 and AchPerColl>=100 ";
                        strSQL = strSQL + "AND SMART0005.dbo.ACC_Commission_View.USER_NAME ='" + gstrUserName + "' ";
                        strSQL = strSQL + "UNION ALL ";
                        strSQL = strSQL + "select  2 RowNO,ZONE,TEAM_NAME,LEDGER_NAME,AchPerSales,AchPerColl,TERITORRY_NAME,TERITORRY_CODE,MR_NAME,LEDGER_CLASS,COMMITMENT,COLLECTION_AMNT,SALES_TARGET,SALES,SP_VOUCHER,ClosingBalnce,Credit_Limit,PENDING  from SMART0005.dbo.ACC_Commission_View where  SMART0005.dbo.ACC_Commission_View.USER_NAME ='" + gstrUserName + "' AND LEDGER_NAME not in(select LEDGER_NAME from SMART0005.dbo.ACC_Commission_View ";
                        strSQL = strSQL + "where AchPerSales>=100 and AchPerColl>=100 AND SMART0005.dbo.ACC_Commission_View.USER_NAME ='" + gstrUserName + "' )) as tb1 ";
                        strSQL = strSQL + "order by ZONE, tb1.RowNO ";

                    }
                    else if (intOption == 4)
                    {
                        strSQL = "select * from  (select   1 RowNO,DIVISION as ZONE,TEAM_NAME,LEDGER_NAME,AchPerSales,AchPerColl,TERITORRY_NAME,TERITORRY_CODE,MR_NAME,LEDGER_CLASS,COMMITMENT,COLLECTION_AMNT,SALES_TARGET,SALES,SP_VOUCHER,ClosingBalnce,Credit_Limit,PENDING from ACC_Commission_View ";
                        strSQL = strSQL + "where AchPerSales>=100 and AchPerColl>=100 ";
                        strSQL = strSQL + "UNION ALL ";
                        strSQL = strSQL + "select  2 RowNO,DIVISION as ZONE,TEAM_NAME,LEDGER_NAME,AchPerSales,AchPerColl,TERITORRY_NAME,TERITORRY_CODE,MR_NAME,LEDGER_CLASS,COMMITMENT,COLLECTION_AMNT,SALES_TARGET,SALES,SP_VOUCHER,ClosingBalnce,Credit_Limit,PENDING  from SMART0005.dbo.ACC_Commission_View where LEDGER_NAME not in(select LEDGER_NAME from SMART0005.dbo.ACC_Commission_View ";
                        strSQL = strSQL + "where AchPerSales>=100 and AchPerColl>=100)) as tb1 ";
                        strSQL = strSQL + "order by  ZONE,tb1.RowNO ";
                    }
                    else if (intOption == 3)
                    {

                        strSQL = "select * from  (select   1 RowNO,AREA as ZONE,TEAM_NAME,LEDGER_NAME,AchPerSales,AchPerColl,TERITORRY_NAME,TERITORRY_CODE,MR_NAME,LEDGER_CLASS,COMMITMENT,COLLECTION_AMNT,SALES_TARGET,SALES,SP_VOUCHER,ClosingBalnce,Credit_Limit,PENDING from ACC_Commission_View ";
                        strSQL = strSQL + "where AchPerSales>=100 and AchPerColl>=100 ";
                        strSQL = strSQL + "UNION ALL ";
                        strSQL = strSQL + "select  2 RowNO,AREA as ZONE,TEAM_NAME,LEDGER_NAME,AchPerSales,AchPerColl,TERITORRY_NAME,TERITORRY_CODE,MR_NAME,LEDGER_CLASS,COMMITMENT,COLLECTION_AMNT,SALES_TARGET,SALES,SP_VOUCHER,ClosingBalnce,Credit_Limit,PENDING from SMART0005.dbo.ACC_Commission_View where LEDGER_NAME not in(select LEDGER_NAME from SMART0005.dbo.ACC_Commission_View ";
                        strSQL = strSQL + "where AchPerSales>=100 and AchPerColl>=100)) as tb1 ";
                        strSQL = strSQL + "order by  ZONE,tb1.RowNO ";
                    }
                    else
                    {

                        strSQL = "select * from  (select   1 RowNO,ZONE,TEAM_NAME,LEDGER_NAME,AchPerSales,AchPerColl,TERITORRY_NAME,TERITORRY_CODE,MR_NAME,LEDGER_CLASS,COMMITMENT,COLLECTION_AMNT,SALES_TARGET,SALES,SP_VOUCHER,ClosingBalnce,Credit_Limit,PENDING from ACC_Commission_View ";
                        strSQL = strSQL + "where AchPerSales>=100 and AchPerColl>=100 ";
                        strSQL = strSQL + "UNION ALL ";
                        strSQL = strSQL + "select  2 RowNO,ZONE,TEAM_NAME,LEDGER_NAME,AchPerSales,AchPerColl,TERITORRY_NAME,TERITORRY_CODE,MR_NAME,LEDGER_CLASS,COMMITMENT,COLLECTION_AMNT,SALES_TARGET,SALES,SP_VOUCHER,ClosingBalnce,Credit_Limit,PENDING  from SMART0005.dbo.ACC_Commission_View where LEDGER_NAME not in(select LEDGER_NAME from SMART0005.dbo.ACC_Commission_View ";
                        strSQL = strSQL + "where AchPerSales>=100 and AchPerColl>=100)) as tb1 ";
                        strSQL = strSQL + "order by  ZONE,tb1.RowNO ";
                    }
                }
                else
                {
                    if (intOption == 6)
                    {
                        strSQL = "SELECT SMART0005.dbo.TEAM_CONFIG.TEAM_NAME,SMART0005.dbo.ACC_LEDGER_Z_D_A.LEDGER_NAME, SMART0005.dbo.TEAM_CONFIG.TEAM_NAME ZONE,SMART0005.dbo.ACC_MARKET_MONITROING_SHEET.DIVISION,SMART0005.dbo.ACC_MARKET_MONITROING_SHEET.AREA,SMART0005.dbo.ACC_MARKET_MONITROING_SHEET.TERITORRY_NAME,SMART0005.dbo.ACC_MARKET_MONITROING_SHEET.TERITORRY_CODE,SMART0005.dbo.ACC_MARKET_MONITROING_SHEET.MR_NAME,SMART0005.dbo.ACC_LEDGER_Z_D_A.LEDGER_CLASS, ";
                        strSQL = strSQL + "ISNULL(SUM(SMART0005.dbo.ACC_MARKET_MONITROING_SHEET.TOTAL_OS),0) TOTAL_OS,isnull(SUM(SMART0005.dbo.ACC_MARKET_MONITROING_SHEET.COMMITMENT),0) COMMITMENT,ISNULL(SUM(SMART0005.dbo.ACC_MARKET_MONITROING_SHEET.COLLECTION_AMNT),0) COLLECTION_AMNT, ";
                        strSQL = strSQL + "ISNULL(SUM(SMART0005.dbo.ACC_MARKET_MONITROING_SHEET.SALES_TARGET),0) SALES_TARGET, ISNULL(SUM(SMART0005.dbo.ACC_MARKET_MONITROING_SHEET.ACHIEVE),0) SALES, ";
                        strSQL = strSQL + "ISNULL(SUM(SMART0005.dbo.ACC_MARKET_MONITROING_SHEET.SALES_RETURN),0) SALES_RETURN, ISNULL(SUM(SMART0005.dbo.ACC_MARKET_MONITROING_SHEET.JV_DEBIT),0) JV_DEBIT, ";
                        strSQL = strSQL + "ISNULL(SUM(SMART0005.dbo.ACC_MARKET_MONITROING_SHEET.JV_CREDIT),0) JV_CREDIT,";
                        strSQL = strSQL + "ISNULL(SUM(SMART0005.dbo.ACC_MARKET_MONITROING_SHEET.CLOSING_VAL),0) ClosingBalnce,";
                        strSQL = strSQL + "ISNULL(SUM(SMART0005.dbo.ACC_MARKET_MONITROING_SHEET.Credit_Limit),0) Credit_Limit,";
                        strSQL = strSQL + "ISNULL(SUM(SMART0005.dbo.ACC_MARKET_MONITROING_SHEET.SP_VOUCHER),0) SP_VOUCHER,ISNULL(SUM(SMART0005.dbo.ACC_MARKET_MONITROING_SHEET.PENDING),0)PENDING FROM SMART0005.dbo.ACC_MARKET_MONITROING_SHEET,SMART0005.dbo.ACC_LEDGER_Z_D_A,SMART0005.dbo.TEAM_CONFIG ";
                        strSQL = strSQL + "WHERE  SMART0005.dbo.ACC_MARKET_MONITROING_SHEET.MR_NAME =SMART0005.dbo.ACC_LEDGER_Z_D_A.LEDGER_NAME_MERZE AND SMART0005.dbo.ACC_LEDGER_Z_D_A.ZONE =SMART0005.dbo.TEAM_CONFIG.ZONE_NAME ";
                        if (gstrUserName != "")
                        {
                            strSQL = strSQL + " AND SMART0005.dbo.TEAM_CONFIG.TEAM_NAME in( SELECT TEAM_NAME from SMART0005.dbo.USER_PRIVILEGES_ZONE WHERE USER_LOGIN_NAME ='" + gstrUserName + "')";
                        }
                        if (strLedger != "")
                        {
                            strSQL = strSQL + "AND SMART0005.dbo.TEAM_CONFIG.TEAM_NAME IN(" + strLedger + ") ";
                        }
                        strSQL = strSQL + "AND SMART0005.dbo.ACC_MARKET_MONITROING_SHEET.USER_NAME ='" + gstrUserName + "' ";
                        strSQL = strSQL + "GROUP by  SMART0005.dbo.TEAM_CONFIG.TEAM_NAME,SMART0005.dbo.ACC_LEDGER_Z_D_A.LEDGER_NAME, SMART0005.dbo.ACC_MARKET_MONITROING_SHEET.DIVISION,SMART0005.dbo.ACC_MARKET_MONITROING_SHEET.AREA,SMART0005.dbo.ACC_MARKET_MONITROING_SHEET.TERITORRY_NAME, ";
                        strSQL = strSQL + "SMART0005.dbo.ACC_MARKET_MONITROING_SHEET.TERITORRY_CODE,SMART0005.dbo.ACC_MARKET_MONITROING_SHEET.MR_NAME,SMART0005.dbo.ACC_LEDGER_Z_D_A.LEDGER_CLASS ";
                    }
                    else
                    {
                        strSQL = "SELECT SMART0005.dbo.TEAM_CONFIG.TEAM_NAME,SMART0005.dbo.ACC_LEDGER_Z_D_A.LEDGER_NAME, SMART0005.dbo.ACC_MARKET_MONITROING_SHEET.ZONE,SMART0005.dbo.ACC_MARKET_MONITROING_SHEET.DIVISION,SMART0005.dbo.ACC_MARKET_MONITROING_SHEET.AREA,SMART0005.dbo.ACC_MARKET_MONITROING_SHEET.TERITORRY_NAME,SMART0005.dbo.ACC_MARKET_MONITROING_SHEET.TERITORRY_CODE,SMART0005.dbo.ACC_MARKET_MONITROING_SHEET.MR_NAME,SMART0005.dbo.ACC_LEDGER_Z_D_A.LEDGER_CLASS, ";
                        strSQL = strSQL + "ISNULL(SUM(SMART0005.dbo.ACC_MARKET_MONITROING_SHEET.TOTAL_OS),0) TOTAL_OS,isnull(SUM(SMART0005.dbo.ACC_MARKET_MONITROING_SHEET.COMMITMENT),0) COMMITMENT,ISNULL(SUM(SMART0005.dbo.ACC_MARKET_MONITROING_SHEET.COLLECTION_AMNT),0) COLLECTION_AMNT, ";
                        strSQL = strSQL + "ISNULL(SUM(SMART0005.dbo.ACC_MARKET_MONITROING_SHEET.SALES_TARGET),0) SALES_TARGET, ISNULL(SUM(SMART0005.dbo.ACC_MARKET_MONITROING_SHEET.ACHIEVE),0) SALES, ";
                        strSQL = strSQL + "ISNULL(SUM(SMART0005.dbo.ACC_MARKET_MONITROING_SHEET.SALES_RETURN),0) SALES_RETURN, ISNULL(SUM(SMART0005.dbo.ACC_MARKET_MONITROING_SHEET.JV_DEBIT),0) JV_DEBIT, ";
                        strSQL = strSQL + "ISNULL(SUM(SMART0005.dbo.ACC_MARKET_MONITROING_SHEET.JV_CREDIT),0) JV_CREDIT,";
                        strSQL = strSQL + "ISNULL(SUM(SMART0005.dbo.ACC_MARKET_MONITROING_SHEET.CLOSING_VAL),0) ClosingBalnce,";
                        strSQL = strSQL + "ISNULL(SUM(SMART0005.dbo.ACC_MARKET_MONITROING_SHEET.Credit_Limit),0) Credit_Limit,";
                        strSQL = strSQL + "ISNULL(SUM(SMART0005.dbo.ACC_MARKET_MONITROING_SHEET.SP_VOUCHER),0) SP_VOUCHER,ISNULL(SUM(SMART0005.dbo.ACC_MARKET_MONITROING_SHEET.PENDING),0)PENDING FROM SMART0005.dbo.ACC_MARKET_MONITROING_SHEET,SMART0005.dbo.ACC_LEDGER_Z_D_A,SMART0005.dbo.TEAM_CONFIG ";
                        strSQL = strSQL + "WHERE  SMART0005.dbo.ACC_MARKET_MONITROING_SHEET.MR_NAME =SMART0005.dbo.ACC_LEDGER_Z_D_A.LEDGER_NAME_MERZE AND SMART0005.dbo.ACC_LEDGER_Z_D_A.ZONE =SMART0005.dbo.TEAM_CONFIG.ZONE_NAME ";
                        strSQL = strSQL + "AND SMART0005.dbo.ACC_MARKET_MONITROING_SHEET.USER_NAME ='" + gstrUserName + "' ";
                        if (intOption == 2)
                        {
                            if (strLedger != "")
                            {
                                strSQL = strSQL + "AND SMART0005.dbo.ACC_MARKET_MONITROING_SHEET.MR_NAME IN(" + strLedger + ") ";
                            }
                        }
                        else if (intOption == 3)
                        {
                            if (strLedger != "")
                            {
                                strSQL = strSQL + "AND SMART0005.dbo.ACC_MARKET_MONITROING_SHEET.AREA IN(" + strLedger + ") ";
                            }
                        }
                        else if (intOption == 4)
                        {
                            if (strLedger != "")
                            {
                                strSQL = strSQL + "AND SMART0005.dbo.ACC_MARKET_MONITROING_SHEET.DIVISION IN(" + strLedger + ") ";
                            }
                        }
                        else if (intOption == 5)
                        {
                            if (strLedger != "")
                            {
                                strSQL = strSQL + "AND SMART0005.dbo.ACC_MARKET_MONITROING_SHEET.ZONE IN(" + strLedger + ") ";
                            }
                        }
                        if (gstrUserName != "")
                        {
                            strSQL = strSQL + " AND SMART0005.dbo.ACC_LEDGER_Z_D_A.DIVISION in( select LEDGER_GROUP_NAME from SMART0005.dbo.USER_PRIVILEGES_COLOR WHERE USER_LOGIN_NAME ='" + gstrUserName + "')";
                        }
                        strSQL = strSQL + "GROUP by  SMART0005.dbo.TEAM_CONFIG.TEAM_NAME,SMART0005.dbo.ACC_LEDGER_Z_D_A.LEDGER_NAME, SMART0005.dbo.ACC_MARKET_MONITROING_SHEET.ZONE,SMART0005.dbo.ACC_MARKET_MONITROING_SHEET.DIVISION,SMART0005.dbo.ACC_MARKET_MONITROING_SHEET.AREA,SMART0005.dbo.ACC_MARKET_MONITROING_SHEET.TERITORRY_NAME, ";
                        strSQL = strSQL + "SMART0005.dbo.ACC_MARKET_MONITROING_SHEET.TERITORRY_CODE,SMART0005.dbo.ACC_MARKET_MONITROING_SHEET.MR_NAME,SMART0005.dbo.ACC_LEDGER_Z_D_A.LEDGER_CLASS ";
                        if (intOption == 4)
                        {
                            strSQL = strSQL + "ORDER BY SMART0005.dbo.ACC_MARKET_MONITROING_SHEET.ZONE,SMART0005.dbo.ACC_MARKET_MONITROING_SHEET.DIVISION,SMART0005.dbo.ACC_MARKET_MONITROING_SHEET.AREA,SMART0005.dbo.ACC_MARKET_MONITROING_SHEET.TERITORRY_CODE  ";
                        }
                        else if (intOption == 5)
                        {
                            strSQL = strSQL + "ORDER BY SMART0005.dbo.ACC_MARKET_MONITROING_SHEET.ZONE,SMART0005.dbo.ACC_MARKET_MONITROING_SHEET.DIVISION,SMART0005.dbo.ACC_MARKET_MONITROING_SHEET.AREA,SMART0005.dbo.ACC_MARKET_MONITROING_SHEET.TERITORRY_CODE  ";
                        }
                        else
                        {
                            strSQL = strSQL + "ORDER BY SMART0005.dbo.ACC_MARKET_MONITROING_SHEET.ZONE,SMART0005.dbo.ACC_MARKET_MONITROING_SHEET.DIVISION,SMART0005.dbo.ACC_MARKET_MONITROING_SHEET.AREA,SMART0005.dbo.ACC_MARKET_MONITROING_SHEET.TERITORRY_CODE  ";
                        }
                    }
                }

                SqlCommand cmd = new SqlCommand(strSQL, gcnMain);
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    RFinalStatement oLedg = new RFinalStatement();
                    oLedg.strTeam = dr["TEAM_NAME"].ToString();
                    if (intBaseTarget == 2)
                    {
                        oLedg.dblPreviousDues = 0;
                        if (dr["ZONE"].ToString() != "")
                        {
                            oLedg.strZone = dr["ZONE"].ToString();
                            if (strledger == dr["ZONE"].ToString())
                            {
                                strledger = dr["ZONE"].ToString();
                                intCheck += 1;
                                oLedg.intADL += intCheck;
                            }
                            else
                            {
                                strledger = "";
                                strledger = dr["ZONE"].ToString();
                                intCheck = 0;
                                intCheck += 1;
                                oLedg.intADL += intCheck;
                            }
                        }

                    }
                    else
                    {

                        oLedg.dblPreviousDues = Convert.ToDouble(dr["TOTAL_OS"].ToString());

                        if (dr["ZONE"].ToString() != "")
                        {
                            oLedg.strZone = dr["ZONE"].ToString();
                        }
                        else
                        {
                            oLedg.strZone = "";
                        }

                        if (dr["DIVISION"].ToString() != "")
                        {
                            oLedg.strDivision = dr["DIVISION"].ToString();
                        }
                        else
                        {
                            oLedg.strDivision = "";
                        }

                        if (dr["AREA"].ToString() != "")
                        {
                            oLedg.strArea = dr["AREA"].ToString();
                        }
                        else
                        {
                            oLedg.strArea = "";
                        }
                    }

                    if (dr["TERITORRY_NAME"].ToString() != "")
                    {
                        oLedg.strTerritory = dr["TERITORRY_NAME"].ToString();
                    }
                    else
                    {
                        oLedg.strteritorryname = "";
                    }

                    if (dr["TERITORRY_CODE"].ToString() != "")
                    {
                        oLedg.strTeritorryCode = dr["TERITORRY_CODE"].ToString();
                    }
                    else
                    {
                        oLedg.strTeritorryCode = "";
                    }

                    if (dr["MR_NAME"].ToString() != "")
                    {
                        oLedg.strMrname = dr["MR_NAME"].ToString();
                    }
                    else
                    {
                        oLedg.strMrname = "";
                    }
                    if (dr["LEDGER_NAME"].ToString() != "")
                    {
                        oLedg.strLedger = dr["TERITORRY_CODE"].ToString() + "-" + dr["LEDGER_NAME"].ToString();
                    }
                    else
                    {
                        oLedg.strLedger = "";
                    }
                    if (dr["LEDGER_CLASS"].ToString() != "")
                    {
                        oLedg.strClass = dr["LEDGER_CLASS"].ToString();
                    }
                    else
                    {
                        oLedg.strClass = "";
                    }

                    oLedg.dblCurrentSales = Convert.ToDouble(dr["SALES_TARGET"].ToString());
                    oLedg.dblcollecCommi = Convert.ToDouble(dr["COLLECTION_AMNT"].ToString());
                    oLedg.dblCommitment = Convert.ToDouble(dr["COMMITMENT"].ToString());
                    oLedg.dblSpVoucher = Convert.ToDouble(dr["SALES"].ToString());
                    oLedg.dblSAVoucherPayment = Convert.ToDouble(dr["SP_VOUCHER"].ToString());
                    oLedg.dblClosing = Convert.ToDouble(dr["ClosingBalnce"].ToString());
                    oLedg.dblCreditLimit = Convert.ToDouble(dr["Credit_Limit"].ToString());
                    //oLedg.dblPending = Convert.ToDouble(dr["PENDING"].ToString());
                    oLedg.dblPending = Convert.ToDouble(dr["Credit_Limit"].ToString()) - Convert.ToDouble(dr["PENDING"].ToString());
                    ooAccLedger.Add(oLedg);

                }
                if (!dr.HasRows)
                {
                    RFinalStatement oLedg = new RFinalStatement();
                    oLedg.strZone = "";
                    oLedg.strDivision = "";
                    oLedg.strArea = "";
                    oLedg.strteritorryname = "";
                    oLedg.strTeritorryCode = "";
                    oLedg.strMrname = "";
                    oLedg.strMrname = "";
                    oLedg.dblPreviousDues = 0;
                    oLedg.dblCurrentSales = 0;
                    oLedg.dblcollecCommi = 0;
                    oLedg.dblCommitment = 0;
                    oLedg.dblSpVoucher = 0;
                    oLedg.dblCreditLimit = 0;
                    ooAccLedger.Add(oLedg);
                }
                dr.Close();
                strSQL = "DELETE FROM SMART0005.dbo.ACC_MARKET_MONITROING_SHEET WHERE USER_NAME='" + gstrUserName + "' ";
                cmdInsert.CommandText = strSQL;
                cmdInsert.ExecuteNonQuery();
                gcnMain.Close();
                return ooAccLedger;

            }
        }

        //#endregion

        public int intstatus { get; set; }

        public string strBranchID { get; set; }

        public int intOption { get; set; }

        public string strLedger { get; set; }

        public int intBaseTarget { get; set; }

        public string strString { get; set; }
    }
}