using Api.Models;
using Dutility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Api
{
    public class DAL
    {
        public List<ShiftCongfig> mGetHeader1()
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

                List<ShiftCongfig> oooChequePrint = new List<ShiftCongfig>();

                strSQL = "SELECT  SHIFT_CONFIG_SERL, EMPLOYEE_SHIFT_NAME, EMPLOYEE_SHIFT_NAME_BANGLA, ";
                strSQL = strSQL + "SHIFT_CONFIG_DATE, SHIFT_CONFIG_START_TIME, SHIFT_CONFIG_END_TIME, OT_STATUS ";
                strSQL = strSQL + "FROM            HRS_SHIFT_CONFIG ";


                SqlCommand cmd = new SqlCommand(strSQL, gcnMain);
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {

                    ShiftCongfig oLedg = new ShiftCongfig();
                    oLedg.intShiftCongfigID = Convert.ToInt32(dr["SHIFT_CONFIG_SERL"]);
                    oLedg.strShiftName = dr["EMPLOYEE_SHIFT_NAME"].ToString();
                    oLedg.strShiftNameBangla = dr["EMPLOYEE_SHIFT_NAME_BANGLA"].ToString();
                    //oLedg.strShiftCongfigDate = ToDateTime(dr["SHIFT_CONFIG_DATE"]);
                    oLedg.strShiftCongfigStartTime = dr["SHIFT_CONFIG_START_TIME"].ToString();
                    oLedg.strShiftCongfigEndTime = dr["SHIFT_CONFIG_END_TIME"].ToString();
                    oLedg.strOtStatus = dr["OT_STATUS"].ToString();
                    oooChequePrint.Add(oLedg);
                }

                if (!dr.HasRows)
                {
                    ShiftCongfig oLedg = new ShiftCongfig();
                    oLedg.intShiftCongfigID = 0;
                    oLedg.strShiftName = "";
                    oLedg.strShiftNameBangla = "";
                    oLedg.strShiftCongfigDate = "";
                    oLedg.strShiftCongfigStartTime = "";
                    oLedg.strShiftCongfigEndTime = "";
                    oLedg.strOtStatus = "";
                    oooChequePrint.Add(oLedg);
                }
                dr.Close();
                gcnMain.Close();
                cmd.Dispose();
                return oooChequePrint;
            }

        }

        public string mPostSaveAttendace(AttendentCongfig obj)
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

                    string ATTEN_KEY = DateTime.UtcNow.ToString("ddMMyyyy") + obj.strEMP_CARD_NO + obj.strATTEN_STATUS;

                    DateTime attenDateIn;
                    if (!DateTime.TryParse(obj.strATTEN_DATEIN, out attenDateIn))
                    {
                       
                        return "Error: Invalid date format for ATTEN_DATEIN";
                    }

                    strSQL = "INSERT INTO HRS_TRANS_WORK_ATTENDANCE_NEW (";
                    strSQL = strSQL + " ATTEN_KEY,USER_NAME,ROLE,EMP_CARD_NO, ";
                    strSQL = strSQL + " ATTEN_DATEIN, ";
                    strSQL = strSQL + " ATTEN_TIMEIN, LATITUDE , LONGITUDE , ADDRESS , DISTANCE,ATTEN_STATUS,ATTEN_SHIFT,ATTEN_COMMENTS, ACTION, EMP_IMAGE  ";
                    strSQL = strSQL + ") ";
                    strSQL = strSQL + "VALUES (";

                    strSQL = strSQL + "'" + ATTEN_KEY + "',";
                    strSQL = strSQL + "'" + obj.strUSER_NAME + "',";
                    strSQL = strSQL + "'" + obj.strROLE + "',";
                    strSQL = strSQL + "'" + obj.strEMP_CARD_NO + "',";
                  
                    strSQL = strSQL + "'" + obj.strATTEN_DATEIN + "',";
                    strSQL = strSQL + "'" + obj.strATTEN_TIMEIN + "',";
                    strSQL = strSQL + "'" + obj.strLATITUDE + "',";
                    strSQL = strSQL + "'" + obj.strLONGITUDE + "',";
                    strSQL = strSQL + "'" + obj.strADDRESS + "',";
                    strSQL = strSQL + "'" + obj.intDISTANCE + "',";
                   
                    
                    strSQL = strSQL + "'" + obj.strATTEN_STATUS + "',";
                    strSQL = strSQL + "'" + obj.strATTEN_SHIFT+ "',";
                  
                    strSQL = strSQL + "'" + obj.strATTEN_COMMENTS + "',";
                    strSQL = strSQL + "'" + obj.strACTION + "',";
                    strSQL = strSQL + "'" + obj.strEMP_IMAGE + "'";
                  
                    strSQL = strSQL + ")";
                    cmdInsert.CommandText = strSQL;
                    cmdInsert.ExecuteNonQuery();
                    cmdInsert.Transaction.Commit();
                    return "Insert successfully";

                    
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

        public List<AttendentCongfig> mGetHeader2()
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

                List<AttendentCongfig> oooChequePrint = new List<AttendentCongfig>();

                strSQL = "SELECT  USER_NAME, ROLE, EMP_CARD_NO, ";
                strSQL = strSQL + " ATTEN_DATEIN, ATTEN_TIMEIN, LATITUDE, LONGITUDE, ADDRESS, DISTANCE, ATTEN_STATUS,ATTEN_SHIFT, ATTEN_COMMENTS ,ACTION,EMP_IMAGE  ";
                strSQL = strSQL + "FROM           HRS_TRANS_WORK_ATTENDANCE_NEW ";


                SqlCommand cmd = new SqlCommand(strSQL, gcnMain);
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {

                    AttendentCongfig oLedg = new AttendentCongfig();
                    oLedg.strUSER_NAME = dr["USER_NAME"].ToString();
                    oLedg.strROLE = dr["ROLE"].ToString();
                    oLedg.strEMP_CARD_NO = dr["EMP_CARD_NO"].ToString();
               
                    oLedg.strATTEN_DATEIN = dr["ATTEN_DATEIN"].ToString();
                    oLedg.strATTEN_TIMEIN = dr["ATTEN_TIMEIN"].ToString();
                    oLedg.strLATITUDE = dr["LATITUDE"].ToString();
                    oLedg.strLONGITUDE = dr["LONGITUDE"].ToString();
                    oLedg.strADDRESS = dr["ADDRESS"].ToString();
                    oLedg.intDISTANCE = Convert.ToInt32(dr["DISTANCE"]);
                   
                    oLedg.strATTEN_STATUS = dr["ATTEN_STATUS"].ToString();
                    oLedg.strATTEN_SHIFT = dr["ATTEN_SHIFT"].ToString();
                
                    oLedg.strATTEN_COMMENTS = dr["ATTEN_COMMENTS"].ToString();
                    oLedg.strACTION = dr["ACTION"].ToString();
                    oLedg.strEMP_IMAGE = dr["EMP_IMAGE"].ToString();


                    oooChequePrint.Add(oLedg);
                }

                if (!dr.HasRows)
                {
                    AttendentCongfig oLedg = new AttendentCongfig();
                    oLedg.strUSER_NAME = "";
                    oLedg.strROLE = "";
                    oLedg.strEMP_CARD_NO = "";
                 
                    oLedg.strATTEN_DATEIN = "";
                    oLedg.strATTEN_TIMEIN = "";
                    oLedg.strLATITUDE = "";
                    oLedg.strLONGITUDE = "";
                    oLedg.strADDRESS = "";
                    oLedg.intDISTANCE = 0;
                  
                    oLedg.strATTEN_STATUS = "";
                    oLedg.strATTEN_SHIFT = "";
                   
                    oLedg.strATTEN_COMMENTS = "";
                    oLedg.strACTION = "";
                    oLedg.strEMP_IMAGE = "";
                    //oLedg.strEMP_JPEG_DOC = "";
                    oooChequePrint.Add(oLedg);
                }
                dr.Close();
                gcnMain.Close();
                cmd.Dispose();
                return oooChequePrint;
            }

        }



        public List<AttendentCongfig> mGetUserCard(AttendentCongfig obj)
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

                List<AttendentCongfig> oooChequePrint = new List<AttendentCongfig>();

                strSQL = "select * from HRS_MPO_LEDGER_CARD where TC='" + obj.strTC + "' ";

                SqlCommand cmd = new SqlCommand(strSQL, gcnMain);
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {

                    AttendentCongfig oLedg = new AttendentCongfig();

                    
                    oLedg.strEMP_CARD_NO = dr["EMP_CARD_NO"].ToString();
                    

                    oooChequePrint.Add(oLedg);
                }

                if (!dr.HasRows)
                {
                    AttendentCongfig oLedg = new AttendentCongfig();

                   
                    oLedg.strEMP_CARD_NO = "";
                    

                    oooChequePrint.Add(oLedg);
                }
                dr.Close();
                gcnMain.Close();
                cmd.Dispose();
                return oooChequePrint;
            }

        }







        
        public List<AttendentCongfig> mGetUserReturn(AttendentCongfig obj)
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

                List<AttendentCongfig> oooChequePrint = new List<AttendentCongfig>();

                strSQL = "select * from HRS_TRANS_WORK_ATTENDANCE_NEW where EMP_CARD_NO='" + obj.strEMP_CARD_NO + "' AND ATTEN_DATEIN='" + obj.strATTEN_DATEIN + "' ";

                SqlCommand cmd = new SqlCommand(strSQL, gcnMain);
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {

                    AttendentCongfig oLedg = new AttendentCongfig();

                    oLedg.intATTEN_SERIAL = Convert.ToInt32(dr["ATTEN_SERIAL"]);
                    oLedg.strUSER_NAME = dr["USER_NAME"].ToString();
                    oLedg.strROLE = dr["ROLE"].ToString();
                    oLedg.strEMP_CARD_NO = dr["EMP_CARD_NO"].ToString();
                    oLedg.strATTEN_DATEIN = dr["ATTEN_DATEIN"].ToString();
                    oLedg.strATTEN_TIMEIN = dr["ATTEN_TIMEIN"].ToString();
                    oLedg.strLATITUDE = dr["LATITUDE"].ToString();
                    oLedg.strLONGITUDE = dr["LONGITUDE"].ToString();
                    oLedg.strADDRESS = dr["ADDRESS"].ToString();
                    oLedg.intDISTANCE = Convert.ToInt32(dr["DISTANCE"]);
                    
                    oLedg.strATTEN_STATUS = dr["ATTEN_STATUS"].ToString();
                    oLedg.strATTEN_SHIFT = dr["ATTEN_SHIFT"].ToString();
                   
                    oLedg.strATTEN_COMMENTS = dr["ATTEN_COMMENTS"].ToString();
                    oLedg.strINSERT_DATE = dr["INSERT_DATE"].ToString();
                    oLedg.strACTION = dr["ACTION"].ToString();
                    oLedg.strEMP_IMAGE = dr["EMP_IMAGE"].ToString();

                    oooChequePrint.Add(oLedg);
                }

                if (!dr.HasRows)
                {
                    AttendentCongfig oLedg = new AttendentCongfig();

                    oLedg.intATTEN_SERIAL = 0;
                    oLedg.strUSER_NAME = "";
                    oLedg.strROLE = "";
                    oLedg.strEMP_CARD_NO = "";

                    oLedg.strATTEN_DATEIN = "";
                    oLedg.strATTEN_TIMEIN = "";
                    oLedg.strLATITUDE = "";
                    oLedg.strLONGITUDE = "";
                    oLedg.strADDRESS = "";
                    oLedg.intDISTANCE = 0;
                    oLedg.intTOTAL_WORKING_HOUR = 0;
                    oLedg.strSTAY_HOUR = "";
                    oLedg.strATTEN_TIMEOUT = "";
                    oLedg.strATTEN_STATUS = "";
                    oLedg.strATTEN_SHIFT = "";
                    oLedg.strSHIFT_START = "";
                    oLedg.strATTEN_COMMENTS = "";
                    oLedg.strINSERT_DATE = "";
                    oLedg.strACTION = "";
                    //oLedg.strEMP_IMAGE = "";
                    oLedg.strEMP_JPEG_DOC = "";

                    oooChequePrint.Add(oLedg);
                }
                dr.Close();
                gcnMain.Close();
                cmd.Dispose();
                return oooChequePrint;
            }

        }


        public List<LeaveConfig> mGetLeave()
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

                List<LeaveConfig> oooChequePrint = new List<LeaveConfig>();

                strSQL = "SELECT LEAVE_ID, LEAVE_NAME, NO_OF_DAYS,ALLOW_DEDUCTION_YN,DEDUCTION_ON,REF_HEAD,LEAVE_NATURE,DEFAULT_STATUS ";
                strSQL = strSQL + "FROM         HRS_LEAVE_CONFIG ";


                SqlCommand cmd = new SqlCommand(strSQL, gcnMain);
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {

                    LeaveConfig oLedg = new LeaveConfig();

                    oLedg.strLEAVE_ID = dr["LEAVE_ID"].ToString();
                    oLedg.strLEAVE_NAME = dr["LEAVE_NAME"].ToString();
                    oLedg.intNO_OF_DAYS = Convert.ToInt32(dr["NO_OF_DAYS"]);
                    oLedg.strALLOW_DEDUCTION_YN = dr["ALLOW_DEDUCTION_YN"].ToString();
                    oLedg.strDEDUCTION_ON = dr["DEDUCTION_ON"].ToString();
                    oLedg.strREF_HEAD = dr["REF_HEAD"].ToString();
                    oLedg.strLEAVE_NATURE = dr["LEAVE_NATURE"].ToString();
                    oLedg.strDEFAULT_STATUS = dr["DEFAULT_STATUS"].ToString();



                    oooChequePrint.Add(oLedg);
                }

                if (!dr.HasRows)
                {
                    LeaveConfig oLedg = new LeaveConfig();
                    oLedg.strLEAVE_ID = "";
                    oLedg.strLEAVE_NAME = "";
                    oLedg.intNO_OF_DAYS = 0;
                    oLedg.strALLOW_DEDUCTION_YN = "";
                    oLedg.strDEDUCTION_ON = "";
                    oLedg.strREF_HEAD = "";
                    oLedg.strLEAVE_NATURE = "";
                    oLedg.strDEFAULT_STATUS = "";


                    oooChequePrint.Add(oLedg);
                }
                dr.Close();
                gcnMain.Close();
                cmd.Dispose();
                return oooChequePrint;
            }

        }


        public List<LeaveConfig> mGetHoly()
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

                List<LeaveConfig> oooChequePrint = new List<LeaveConfig>();

                strSQL = "SELECT SERIAL_NO, HOLIDAY_DATE, DESCRIPTION, DIVISION_NAME ";
                strSQL = strSQL + "FROM           HRS_HOLIDAY ";


                SqlCommand cmd = new SqlCommand(strSQL, gcnMain);
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {

                    LeaveConfig oLedg = new LeaveConfig();
                    oLedg.intSERIAL_NO = Convert.ToInt32(dr["SERIAL_NO"]);
                    oLedg.strHOLIDAY_DATE = Convert.ToDateTime(dr["HOLIDAY_DATE"]).ToString("yyyy-MM-dd");
                    oLedg.strDESCRIPTION = dr["DESCRIPTION"].ToString();
                    oLedg.strDIVISION_NAME = dr["DIVISION_NAME"].ToString();



                    oooChequePrint.Add(oLedg);
                }

                if (!dr.HasRows)
                {
                    LeaveConfig oLedg = new LeaveConfig();
                    oLedg.intSERIAL_NO = 0;
                    oLedg.strHOLIDAY_DATE = "";
                    oLedg.strDESCRIPTION = "";
                    oLedg.strDIVISION_NAME = "";

                    oooChequePrint.Add(oLedg);
                }
                dr.Close();
                gcnMain.Close();
                cmd.Dispose();
                return oooChequePrint;
            }

        }


        //return strEmpCardNO;






        public string mPostUserLeave(Leavaes obj)
        {

            string strSQL = "", strAttenKey = "", strShiftID = "", strShiftSTime = "", strShiftEnd = "";
         
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



                    SqlDataReader dr;
                    SqlCommand cmdInsert = new SqlCommand();
                    SqlTransaction myTrans;
                    myTrans = gcnMain.BeginTransaction();
                    cmdInsert.Connection = gcnMain;
                    cmdInsert.Transaction = myTrans;

                  
                            string strEmpCardNO = "", strleavekey = "", strStartDate = "", strEndDate = "", strLeaveID = "", strCOMMENTS = "";
                            long lngBizKey = 0;

                            int intday = 0, intAprovestatus = 1;
                            strLeaveID = obj.strLEAVE_ID;
                            strStartDate = obj.strFROM_DATE ;
                            strEndDate = obj.strTO_DATE;
                            intday = Convert.ToInt32(Utility.Val(obj.intNO_OF_DAYS.ToString()));
                            strCOMMENTS = obj.strCOMMENTS.ToString();
                            strEmpCardNO = obj.strEMP_CARD_NO.ToString();

                            string[] dateParts = strStartDate.Split('-');
                            int day = int.Parse(dateParts[0]);
                            int month = int.Parse(dateParts[1]);
                            int year = int.Parse(dateParts[2]);


                            DateTime dteSdate = new DateTime(year, month, day);
                            //string strleavekey = strEmpCardNO + dteSdate.ToString("yyyyMMdd");


                            //DateTime dteSdate =Convert.ToDateTime (strStartDate);

                            strleavekey = strEmpCardNO + dteSdate.ToString("yyyyMMdd");
                            strSQL = "SELECT  EMP_CARD_NO FROM HRS_EMPLOYEE ";
                            strSQL = strSQL + "WHERE ";
                            strSQL = strSQL + " EMP_CARD_NO ='" + strEmpCardNO + "' ";
                            strSQL = strSQL + " AND EMP_STATUS < 3 ";
                            cmdInsert.CommandText = strSQL;
                            dr = cmdInsert.ExecuteReader();
                            if (!dr.Read())
                            {
                                return "Card No Mismatch";
                            }
                            dr.Close();
                            strSQL = "SELECT  LEAVE_ID FROM HRS_LEAVE_CONFIG ";
                            strSQL = strSQL + "WHERE ";
                            strSQL = strSQL + " LEAVE_ID ='" + strLeaveID + "' ";
                            cmdInsert.CommandText = strSQL;
                            dr = cmdInsert.ExecuteReader();
                            if (!dr.Read())
                            {
                                return "Leave ID Mismatch";
                            }
                            dr.Close();
                            if (intday < 0)
                            {
                                return "No of Days Cannot be Negetive";
                            }
                            strSQL = "INSERT INTO  HRS_EMP_LEAVE (";
                            strSQL = strSQL + "EMP_LEAVE_KEY, EMP_CARD_NO, LEAVE_ID, FROM_DATE, TO_DATE, NO_OF_DAYS, APPROVED_STATUS,COMMENTS,B_M_R,B_LEAVE_KEY";
                            strSQL = strSQL + ") ";
                            strSQL = strSQL + "VALUES (";
                            strSQL = strSQL + "'" + strleavekey.Trim().Replace("'", "''") + "',";
                            strSQL = strSQL + "'" + strEmpCardNO + "',";
                            strSQL = strSQL + "'" + strLeaveID + "',";
                            strSQL = strSQL + "" + Utility.cvtSQLDateString(strStartDate) + ",";
                            strSQL = strSQL + "" + Utility.cvtSQLDateString(strEndDate) + ",";
                            strSQL = strSQL + "" + intday + ",";
                            strSQL = strSQL + "" + intAprovestatus + ",";
                            strSQL = strSQL + "'" + strCOMMENTS + "',";
                            strSQL = strSQL + 1 + ",";
                            strSQL = strSQL + " " + lngBizKey + "";
                            strSQL = strSQL + ")";
                            cmdInsert.CommandText = strSQL;
                            cmdInsert.ExecuteNonQuery();
                            int intdays = 0;
                            string strLeaveToDate = strStartDate;
                            DateTime dtetodate = dteSdate;
                           
                            for (intdays = 1; intdays <= intday; intdays++)
                            {
                                strAttenKey = strEmpCardNO.Trim().Replace("'", "''") + dtetodate.ToString("yyyyMMdd");
                              


                                strSQL = "INSERT INTO HRS_EMP_LEAVE_DETAILS(ATTEN_KEY,EMP_LEAVE_KEY,EMP_CARD_NO,LEAVE_ID,LEAVE_DATE,LEAVE_APPROVED) ";
                                strSQL = strSQL + "VALUES(";
                                strSQL = strSQL + "'" + strAttenKey.Trim().Replace("'", "''") + "' ";
                                strSQL = strSQL + ",'" + strleavekey.Trim().Replace("'", "''") + "'";
                                strSQL = strSQL + ",'" + strEmpCardNO + "' ";
                                strSQL = strSQL + ",'" + strLeaveID + "' ";
                                strSQL = strSQL + "," + Utility.cvtSQLDateString(strLeaveToDate) + " ";
                                strSQL = strSQL + ", " + intAprovestatus;
                                strSQL = strSQL + ")";
                                cmdInsert.CommandText = strSQL;
                                cmdInsert.ExecuteNonQuery();
                              
                                strSQL = "INSERT INTO HRS_TRANS_WORK_ATTENDANCE (";
                                strSQL = strSQL + "ATTEN_KEY,EMP_CARD_NO,MONTH_ID,ATTEN_DATEIN,ATTEN_STATUS";
                                strSQL = strSQL + ",ATTEN_SHIFT,SHIFT_START,SHIFT_END)";
                                strSQL = strSQL + "VALUES (";
                                strSQL = strSQL + "'" + strAttenKey.Trim().Replace("'", "''") + "',";
                                strSQL = strSQL + "'" + strEmpCardNO + "',";
                                strSQL = strSQL + "'" + dtetodate.ToString("MMMyy").ToUpper() + "',";
                                strSQL = strSQL + "" + Utility.cvtSQLDateString(strLeaveToDate) + ",";
                                strSQL = strSQL + "'" + strLeaveID + "',";
                                strSQL = strSQL + "'" + strShiftID + "',";
                                strSQL = strSQL + "'" + strShiftSTime + "',";
                                strSQL = strSQL + "'" + strShiftEnd + "'";
                                strSQL = strSQL + ")";
                                cmdInsert.CommandText = strSQL;
                                cmdInsert.ExecuteNonQuery();
                                

                                dtetodate = dtetodate.AddDays(1);
                                strLeaveToDate = dtetodate.ToString("dd-MM-yyyy");
                            }
                          

                   
                    cmdInsert.Transaction.Commit();
                    return "inserted successfully ";
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











        //public string mPostUserLeave(List<LeaveConfig> LeaveConfig)
        //{
            //string strSQL = null;
            //string connectionString = Utility.SQLConnstringComSwitch("0001");

            //using (SqlConnection gcnMain = new SqlConnection(connectionString))
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

        //            // Generate EMP_LEAVE_KEY by concatenating strEmpCardNO and FROM_DATE
        //            //string empLeaveKey = obj.strEMP_CARD_NO + obj.strFROM_DATE;

                   
        //            string empLeaveKey = LeaveConfig[i].strEMP_CARD_NO + DateTime.UtcNow.ToString("yyMMddHHmmss");



        //             for (int i = 0; i < LeaveConfig.Count; i++)
        //            {
        //                int intid = i;

        //                if (intid == 0)
        //                {

        //                    strSQL = "INSERT INTO HRS_EMP_LEAVE (";
        //                    strSQL += " EMP_LEAVE_KEY, EMP_CARD_NO, LEAVE_ID, FRIDAY, FROM_DATE, TO_DATE, NO_OF_DAYS,";
        //                    strSQL += "FIRST_DATE_MLEAVE, SECOND_DATE_MLEAVE, APPROVED_STATUS, COMMENTS, ";
        //                    strSQL += " RES_PEREMP_CARD_NO, FAL_HR_APP, DESTINATION, USER_LOGIN_NAME, INSERT_DATE, UPDATE_DATE, HOD_APP_DATE, HR_APP_DATE, B_LEAVE_KEY, B_M_R";
        //                    strSQL += ") ";

        //                    strSQL += "VALUES (";

        //                    strSQL += "'" + empLeaveKey + "',";
        //                    strSQL += "'" + LeaveConfig[i].strEMP_CARD_NO + "',";
        //                    strSQL += "'" + LeaveConfig[i].strLEAVE_ID + "',";
        //                    strSQL += "'" + LeaveConfig[i].intFRIDAY + "',";
        //                    strSQL += "'" + LeaveConfig[i].strFROM_DATE + "',";
        //                    strSQL += "'" + LeaveConfig[i].strTO_DATE + "',";
        //                    strSQL += "'" + LeaveConfig[i].intNO_OF_DAYS + "',";
        //                    strSQL += "'" + LeaveConfig[i].strFIRST_DATE_MLEAVE + "',";
        //                    strSQL += "'" + LeaveConfig[i].strSECOND_DATE_MLEAVE + "',";
        //                    strSQL += "'" + LeaveConfig[i].strAPPROVED_STATUS + "',";
        //                    strSQL += "N'" + LeaveConfig[i].strCOMMENTS + "',";
        //                    strSQL += "'" + LeaveConfig[i].strRES_PEREMP_CARD_NO + "',";
        //                    strSQL += "'" + LeaveConfig[i].strFAL_HR_APP + "',";
        //                    strSQL += "'" + LeaveConfig[i].strDESTINATION + "',";
        //                    strSQL += "'" + LeaveConfig[i].strUSER_LOGIN_NAME + "',";
        //                    strSQL += "'" + LeaveConfig[i].strINSERT_DATE + "',";
        //                    strSQL += "'" + LeaveConfig[i].strUPDATE_DATE + "',";
        //                    strSQL += "'" + LeaveConfig[i].strHOD_APP_DATE + "',";
        //                    strSQL += "'" + LeaveConfig[i].strHR_APP_DATE + "',";
        //                    strSQL += "'" + LeaveConfig[i].intB_LEAVE_KEY + "',";
        //                    strSQL += "'" + LeaveConfig[i].strB_M_R + "'";
        //                    strSQL += ")";
        //                    cmdInsert.CommandText = strSQL;
        //                    cmdInsert.ExecuteNonQuery();

        //                }
        //                else if (intid == 1)
        //                {
        //                    strSQL = "INSERT INTO HRS_USER (";
        //                    strSQL = strSQL + "EMP_LEAVE_KEY,LEAVE_ID, LEAVE_DATE,LEAVE_APPROVED";

        //                    strSQL = strSQL + ")";
        //                    strSQL = strSQL + " VALUES(";
        //                    strSQL = strSQL + "'" + empLeaveKey + "',";
        //                    strSQL = strSQL + "'" + LeaveConfig[i].strEMP_CARD_NO + "',";
        //                    strSQL = strSQL + "'" + LeaveConfig[i].strLEAVE_ID + "',";
        //                    strSQL = strSQL + "'" + LeaveConfig[i].strLEAVE_DATE + "',";
        //                    strSQL = strSQL + "'" + LeaveConfig[i].intLEAVE_APPROVED + "'";
  

        //                    strSQL = strSQL + ")";

        //                    cmdInsert.CommandText = strSQL;
        //                    cmdInsert.ExecuteNonQuery();


        //                }

        //            }

        //            cmdInsert.Transaction.Commit();
                  

        //        }

        //        catch (SqlException)
        //        {
        //            return "Sorry!  already Exists..";
        //        }
        //        finally
        //        {
        //            gcnMain.Dispose();

        //        }

        //    }
        //}
      





       
        public List<LeaveConfig> mGetUserLeaveReturn(LeaveConfig obj)
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

                List<LeaveConfig> oooChequePrint = new List<LeaveConfig>();


                strSQL = "SELECT * FROM HRS_EMP_LEAVE WHERE EMP_CARD_NO='" + obj.strEMP_CARD_NO + "' AND FROM_DATE >= '" + obj.strFROM_DATE + "' AND TO_DATE <= '" + obj.strTO_DATE + "'";

                SqlCommand cmd = new SqlCommand(strSQL, gcnMain);
                dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    LeaveConfig oLedg = new LeaveConfig();

                    oLedg.strEMP_LEAVE_KEY = dr["EMP_LEAVE_KEY"].ToString();
                    oLedg.strEMP_CARD_NO = dr["EMP_CARD_NO"].ToString();
                    oLedg.strLEAVE_ID = dr["LEAVE_ID"].ToString();
                    oLedg.intFRIDAY = Convert.ToInt32(dr["FRIDAY"]);
                    oLedg.strFROM_DATE = Convert.ToDateTime(dr["FROM_DATE"]).ToString("dd-MM-yyyy");
                    oLedg.strTO_DATE = Convert.ToDateTime(dr["TO_DATE"]).ToString("dd-MM-yyyy");
                    oLedg.intNO_OF_DAYS = Convert.ToInt32(dr["NO_OF_DAYS"]);
                    oLedg.strFIRST_DATE_MLEAVE = dr["FIRST_DATE_MLEAVE"].ToString();
                    oLedg.strSECOND_DATE_MLEAVE = dr["SECOND_DATE_MLEAVE"].ToString();
                    oLedg.strAPPROVED_STATUS = dr["APPROVED_STATUS"].ToString();
                    oLedg.strCOMMENTS = dr["COMMENTS"].ToString();
                    oLedg.strRES_PEREMP_CARD_NO = dr["RES_PEREMP_CARD_NO"].ToString();
                    oLedg.strFAL_HR_APP = dr["FAL_HR_APP"].ToString();
                    oLedg.strDESTINATION = dr["DESTINATION"].ToString();
                    oLedg.strUSER_LOGIN_NAME = dr["USER_LOGIN_NAME"].ToString();
                    oLedg.strINSERT_DATE = dr["INSERT_DATE"].ToString();
                    oLedg.strUPDATE_DATE = dr["UPDATE_DATE"].ToString();
                    oLedg.strHOD_APP_DATE = dr["HOD_APP_DATE"].ToString();
                    oLedg.strHR_APP_DATE = dr["HR_APP_DATE"].ToString();
                    oLedg.intB_LEAVE_KEY = Convert.ToInt32(dr["B_LEAVE_KEY"]);
                    oLedg.strB_M_R = dr["B_M_R"].ToString();

                    oooChequePrint.Add(oLedg);
                }

                if (!dr.HasRows)
                {
                    LeaveConfig oLedg = new LeaveConfig();

                    oLedg.strEMP_LEAVE_KEY = "";
                    oLedg.strEMP_CARD_NO = "";
                    oLedg.strLEAVE_ID = "";
                    oLedg.intFRIDAY = 0;
                    oLedg.strFROM_DATE = "";
                    oLedg.strTO_DATE = "";
                    oLedg.intNO_OF_DAYS = 0;
                    oLedg.strFIRST_DATE_MLEAVE = "";
                    oLedg.strSECOND_DATE_MLEAVE = "";
                    oLedg.strAPPROVED_STATUS = "";
                    oLedg.strCOMMENTS = "";
                    oLedg.strRES_PEREMP_CARD_NO = "";
                    oLedg.strFAL_HR_APP = "";
                    oLedg.strDESTINATION = "";
                    oLedg.strUSER_LOGIN_NAME = "";
                    oLedg.strINSERT_DATE = "";
                    oLedg.strUPDATE_DATE = "";
                    oLedg.strHOD_APP_DATE = "";
                    oLedg.strHR_APP_DATE = "";
                    oLedg.intB_LEAVE_KEY = 0;
                    oLedg.strB_M_R = "";

                    oooChequePrint.Add(oLedg);
                }

                dr.Close();
                gcnMain.Close();
                cmd.Dispose();
                return oooChequePrint;
            }
        }







         public string mPostUserWeekend(LeaveConfig obj)
    {
        string strSQL = null;
        string connectionString = Utility.SQLConnstringComSwitch("0001");

        using (SqlConnection gcnMain = new SqlConnection(connectionString))
        {
            try
            {
                gcnMain.Open();

                using (SqlCommand cmdInsert = gcnMain.CreateCommand())
                {
                    SqlTransaction myTrans = gcnMain.BeginTransaction();
                    cmdInsert.Transaction = myTrans;

                    //string lastTwoDigits = obj.strEMP_CARD_NO.Substring(obj.strEMP_CARD_NO.Length - 2);
                    string[] dateParts = obj.strEFFECTIVE_DATE.Split('-');
                    string empweekenKey = obj.strEMP_CARD_NO + dateParts[0] + dateParts[1] + dateParts[2];
                    //string SerialNumberkey = lastTwoDigits + dateParts[0] + dateParts[1] + dateParts[2];

                    strSQL = "INSERT INTO HRS_EMPLOYEE_WEEKEND ( WEEKND_KEY, EMP_CARD_NO, EFFECTIVE_DATE, EMP_WEEKEND, POS_TYPE) ";
                    strSQL += "VALUES ( @empweekenKey, @empCardNo, @effectiveDate, @empWeekend, @posType)";
                    //strSQL += "VALUES (@SerialNumberkey, @empweekenKey, @empCardNo, @effectiveDate, @empWeekend, @posType)";

                    cmdInsert.CommandText = strSQL;
                    //cmdInsert.Parameters.AddWithValue("@SerialNumberkey", SerialNumberkey);
                    cmdInsert.Parameters.AddWithValue("@empweekenKey", empweekenKey);
                    cmdInsert.Parameters.AddWithValue("@empCardNo", obj.strEMP_CARD_NO);
                    cmdInsert.Parameters.AddWithValue("@effectiveDate", obj.strEFFECTIVE_DATE);
                    cmdInsert.Parameters.AddWithValue("@empWeekend", obj.strEMP_WEEKEND);
                    cmdInsert.Parameters.AddWithValue("@posType", obj.intPOS_TYPE);

                    cmdInsert.ExecuteNonQuery();
                    myTrans.Commit();
                    return "1";
                }
            }
            catch (Exception ex)
            {
              
                return ex.Message;
            }
        }
    }






         public List<LeaveConfig> mGetUserWeekendReturn(LeaveConfig obj)
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

                 List<LeaveConfig> oooChequePrint = new List<LeaveConfig>();


                 strSQL = "SELECT * FROM HRS_EMPLOYEE_WEEKEND WHERE EMP_CARD_NO='" + obj.strEMP_CARD_NO + "' AND EFFECTIVE_DATE >= '" + obj.strFROM_DATE + "' AND EFFECTIVE_DATE <= '" + obj.strTO_DATE + "'";

                 SqlCommand cmd = new SqlCommand(strSQL, gcnMain);
                 dr = cmd.ExecuteReader();

                 while (dr.Read())
                 {
                     LeaveConfig oLedg = new LeaveConfig();


                     oLedg.strEMP_CARD_NO = dr["EMP_CARD_NO"].ToString();
                     oLedg.strEFFECTIVE_DATE = Convert.ToDateTime(dr["EFFECTIVE_DATE"]).ToString("yyyy-MM-dd");
                     oLedg.strEMP_WEEKEND = dr["EMP_WEEKEND"].ToString();
                     oLedg.strACTION = dr["ACTION"].ToString();


                     oooChequePrint.Add(oLedg);
                 }

                 if (!dr.HasRows)
                 {
                     LeaveConfig oLedg = new LeaveConfig();


                     oLedg.strEMP_CARD_NO = "";
                     oLedg.strEFFECTIVE_DATE = "";
                     oLedg.strEMP_WEEKEND = "";
                     oLedg.strACTION = "";


                     oooChequePrint.Add(oLedg);
                 }

                 dr.Close();
                 gcnMain.Close();
                 cmd.Dispose();
                 return oooChequePrint;
             }
         }





         public List<TourConfig> mGetTour(TourConfig obj)
         {
             string strSQL = null;
             string connectionString = Utility.SQLConnstringComSwitch("0001");
             List<TourConfig> tourList = new List<TourConfig>();

             using (SqlConnection gcnMain = new SqlConnection(connectionString))
             {
                 gcnMain.Open();

                 strSQL = "SELECT TOUR_NAME, TOUR_STATUS FROM HRS_EMP_TOUR";

                 using (SqlCommand cmd = new SqlCommand(strSQL, gcnMain))
                 {
                     using (SqlDataReader dr = cmd.ExecuteReader())
                     {
                         while (dr.Read())
                         {
                             TourConfig tour = new TourConfig();
                             tour.strTOUR_NAME = dr["TOUR_NAME"].ToString();
                             tour.strTOUR_STATUS = dr["TOUR_STATUS"].ToString();
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




         public List<TourConfigLedger> mGetTourLedger(TourConfigLedger obj)
         {
             string strSQL = null;
             string connectionString = Utility.SQLConnstringComSwitch("0001");
             List<TourConfigLedger> tourList = new List<TourConfigLedger>();

             using (SqlConnection gcnMain = new SqlConnection(connectionString))
             {
                 gcnMain.Open();

                 strSQL = "select * from SMART0005.dbo.ACC_LEDGER_Z_D_A WHERE MPO_CARD_NO='" + obj.strMPO_CARD_NO + "'";

             

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




         public string mPostTourPlanRoute(List<TourPlanShiftConfig> tourPlans)
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


                     foreach (var tourPlan in tourPlans)
                     {

                         strSQL = "INSERT INTO HRS_EMP_TOUR_PLAN(";
                         strSQL = strSQL + " USER_NAME, EMP_CARD_NO, MARKET_NAME, ROUTE, SHIFT,  ";
                         strSQL = strSQL + "TOUR_NAME, NOTE, EFFECTIVE_DATE, PURPOSE, ACCOMPANY_WITH, ";
                         strSQL = strSQL + "START_LOCATION, END_LOCATION, STATUS ";
                         strSQL = strSQL + ")";
                         strSQL = strSQL + " VALUES(";
                         strSQL = strSQL + "'" + tourPlan.strUSER_NAME + "',";
                         strSQL = strSQL + "'" + tourPlan.strEMP_CARD_NO + "',";
                         strSQL = strSQL + "'" + tourPlan.strMARKET_NAME + "',";
                         strSQL = strSQL + "'" + tourPlan.strROUTE + "',";
                         strSQL = strSQL + "'" + tourPlan.strSHIFT + "',";
                         strSQL = strSQL + "'" + tourPlan.strTOUR_NAME + "',";
                         strSQL = strSQL + "N'" + tourPlan.strNOTE + "',";
                         strSQL = strSQL + "'" + tourPlan.strEFFECTIVE_DATE + "',";
                         strSQL = strSQL + "'" + tourPlan.strPURPOSE + "',";
                         strSQL = strSQL + "'" + tourPlan.strACCOMPANY_WITH + "',";
                         strSQL = strSQL + "'" + tourPlan.strSTART_LOCATION + "',";
                         strSQL = strSQL + "'" + tourPlan.strEND_LOCATION + "',";
                         strSQL = strSQL + "'" + tourPlan.strACTION + "'";

                         strSQL += ")";

                         cmdInsert.CommandText = strSQL;
                         cmdInsert.ExecuteNonQuery();

                     }
                     cmdInsert.Transaction.Commit();

                     return "1";
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





         //public List<TourPlanShiftConfig> mGetTourPlanRouteReturn(TourPlanShiftConfig obj)
         //{
         //    string strSQL = null;
         //    string connectionString = Utility.SQLConnstringComSwitch("0001");

         //    using (SqlConnection gcnMain = new SqlConnection(connectionString))
         //    {
         //        gcnMain.Open();
         //        SqlDataReader dr;

         //        List<TourPlanShiftConfig> oooChequePrint = new List<TourPlanShiftConfig>();

                
         //        strSQL = "SELECT COUNT(*) FROM HRS_EMP_TOUR_PLAN WHERE EMP_CARD_NO = @EMP_CARD_NO AND MONTH(EFFECTIVE_DATE) = MONTH(@EFFECTIVE_DATE) AND YEAR(EFFECTIVE_DATE) = YEAR(@EFFECTIVE_DATE)";
         //        SqlCommand countCmd = new SqlCommand(strSQL, gcnMain);
         //        countCmd.Parameters.AddWithValue("@EMP_CARD_NO", obj.strEMP_CARD_NO);

         //        countCmd.Parameters.AddWithValue("@EFFECTIVE_DATE", obj.strEFFECTIVE_DATE);

         //        int similarRecordCount = (int)countCmd.ExecuteScalar();

         //        string[] dateParts = obj.strEFFECTIVE_DATE.Split('-');
         //        int day = int.Parse(dateParts[0]);
         //        int month = int.Parse(dateParts[1]);
         //        int year = int.Parse(dateParts[2]);


         //        DateTime effectiveDate = new DateTime(year, month, day);

         //        countCmd.Dispose();

                
         //        if (similarRecordCount > 0)
         //        {

                 
         //        //DateTime effectiveDate;
                     
         //            if (DateTime.TryParse(obj.strEFFECTIVE_DATE, out effectiveDate))
         //            {
         //                strSQL = "SELECT * FROM HRS_EMP_TOUR_PLAN WHERE EMP_CARD_NO = @EMP_CARD_NO AND MONTH(EFFECTIVE_DATE) = MONTH(@EFFECTIVE_DATE) AND YEAR(EFFECTIVE_DATE) = YEAR(@EFFECTIVE_DATE)";
                         
         //                SqlCommand cmd = new SqlCommand(strSQL, gcnMain);
         //                cmd.Parameters.AddWithValue("@EMP_CARD_NO", obj.strEMP_CARD_NO);
         //                cmd.Parameters.AddWithValue("@EFFECTIVE_DATE", effectiveDate);

         //                dr = cmd.ExecuteReader();

         //                while (dr.Read())
         //                {
         //                    TourPlanShiftConfig oLedg = new TourPlanShiftConfig();

                             
         //                    oLedg.strUSER_NAME = dr["USER_NAME"].ToString();
         //                    oLedg.strEMP_CARD_NO = dr["EMP_CARD_NO"].ToString();
         //                    oLedg.strMARKET_NAME = dr["MARKET_NAME"].ToString();
         //                    oLedg.strROUTE = dr["ROUTE"].ToString();
         //                    oLedg.strSHIFT = dr["SHIFT"].ToString();
         //                    oLedg.strTOUR_NAME = dr["TOUR_NAME"].ToString();
         //                    oLedg.strNOTE = dr["NOTE"].ToString();
         //                    oLedg.strEFFECTIVE_DATE = dr["EFFECTIVE_DATE"].ToString();
         //                    oLedg.strPURPOSE = dr["PURPOSE"].ToString();
         //                    oLedg.strACCOMPANY_WITH = dr["ACCOMPANY_WITH"].ToString();
         //                    oLedg.strSTART_LOCATION = dr["START_LOCATION"].ToString();
         //                    oLedg.strEND_LOCATION = dr["END_LOCATION"].ToString();
         //                    oLedg.strSTATUS = dr["STATUS"].ToString();

         //                    oooChequePrint.Add(oLedg);
         //                }

         //                dr.Close();
         //                cmd.Dispose();
         //            }
         //        }

         //        gcnMain.Close();

         //        return oooChequePrint;
         //    }
         //}


         public List<TourPlanShiftConfig> mGetTourPlanRouteReturn(TourPlanShiftConfig obj)
         {
             List<TourPlanShiftConfig> oooChequePrint = new List<TourPlanShiftConfig>();
  
             string connectionString = Utility.SQLConnstringComSwitch("0001");

             
             using (SqlConnection gcnMain = new SqlConnection(connectionString))
             {
                 try
                 {
                     
                     gcnMain.Open();

                     
                     string strSQL = "SELECT * FROM HRS_EMP_TOUR_PLAN WHERE EMP_CARD_NO=@EMP_CARD_NO AND EFFECTIVE_DATE >= @EFFECTIVE_DATE";
                     SqlCommand cmd = new SqlCommand(strSQL, gcnMain);
                     cmd.Parameters.AddWithValue("@EMP_CARD_NO", obj.strEMP_CARD_NO);
                     cmd.Parameters.AddWithValue("@EFFECTIVE_DATE", obj.strEFFECTIVE_DATE);

                     
                     using (SqlDataReader dr = cmd.ExecuteReader())
                     {
                         
                         while (dr.Read())
                         {
                             TourPlanShiftConfig oLedg = new TourPlanShiftConfig();

                             oLedg.strUSER_NAME = dr["USER_NAME"].ToString();
                             oLedg.strEMP_CARD_NO = dr["EMP_CARD_NO"].ToString();
                             oLedg.strMARKET_NAME = dr["MARKET_NAME"].ToString();
                             oLedg.strROUTE = dr["ROUTE"].ToString();
                             oLedg.strSHIFT = dr["SHIFT"].ToString();
                             oLedg.strTOUR_NAME = dr["TOUR_NAME"].ToString();
                             oLedg.strNOTE = dr["NOTE"].ToString();
                             oLedg.strEFFECTIVE_DATE = dr["EFFECTIVE_DATE"].ToString(); 
                             oLedg.strPURPOSE = dr["PURPOSE"].ToString();
                             oLedg.strACCOMPANY_WITH = dr["ACCOMPANY_WITH"].ToString();
                             oLedg.strSTART_LOCATION = dr["START_LOCATION"].ToString();
                             oLedg.strEND_LOCATION = dr["END_LOCATION"].ToString();
                             oLedg.strSTATUS = dr["STATUS"].ToString();

                             oooChequePrint.Add(oLedg);
                         }
                     } 

                   
                     cmd.Dispose();
                 }
                 catch (SqlException sqlEx)
                 {
                     
                     Console.WriteLine("SQL Error: " + sqlEx.Message);
                 }
                 catch (Exception ex)
                 {
                    
                     Console.WriteLine("Error: " + ex.Message);
                 }
             } 

             return oooChequePrint;
         }














         public List<PrescriptionConfigTeritorry> mGetTeritorry(PrescriptionConfigTeritorry obj)
         {
             string strSQL = null;
             string connectionString = Utility.SQLConnstringComSwitch("0001");
             List<PrescriptionConfigTeritorry> PrescriptionConfigTeritorryList = new List<PrescriptionConfigTeritorry>();

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
                             PrescriptionConfigTeritorry Prescription = new PrescriptionConfigTeritorry();

                             Prescription.strTERITORRY_CODE = dr["TERITORRY_CODE"].ToString();
                             Prescription.strTERITORRY_NAME = dr["TERITORRY_NAME"].ToString();

                             PrescriptionConfigTeritorryList.Add(Prescription);
                         }
                     }
                 }
             }

             if (PrescriptionConfigTeritorryList.Count == 0)
             {

                 PrescriptionConfigTeritorryList.Add(new PrescriptionConfigTeritorry());
             }

             return PrescriptionConfigTeritorryList;
         }





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
                     strSQL += "PRESCRIPTION_IMG, MARKET_NAME, TERITORRY_CODE, PRODUCTS, LATITUDE, LONGITUDE, NOTE, STATUS";
                     strSQL += ") ";
                     strSQL += "VALUES (";
                     strSQL += "@IMG_ID, @USER_NAME, @EMP_CARD_NO, ";
                     strSQL += "@DOCTOR, @INSTITUTION, ";
                     strSQL += "@PRESCRIPTION_IMG, @MARKET_NAME, @TERITORRY_CODE, @PRODUCTS, @LATITUDE, @LONGITUDE, @NOTE, @STATUS";
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
                     cmdInsert.Parameters.AddWithValue("@STATUS", obj.strACTION);

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



         public List<Notice> mGetNotice(Notice obj)
         {
             string strSQL = null;
             string connectionString = Utility.SQLConnstringComSwitch("0001");
             List<Notice> NoticeList = new List<Notice>();

             using (SqlConnection gcnMain = new SqlConnection(connectionString))
             {
                 gcnMain.Open();

                 strSQL = "select * from HRS_NOTICE  N ,HRS_USER U,HRS_MARKET_ACCESS M where N.NOTICE_ID= U.NOTICE_ID AND N.NOTICE_ID= M.NOTICE_ID";

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
                             Notices.strSTART_DATE = dr["START_DATE"].ToString();
                             Notices.strEND_DATE = dr["END_DATE"].ToString();
                             Notices.strINSERT_DATE = dr["INSERT_DATE"].ToString();
                             Notices.strSTATUS = dr["STATUS"].ToString();
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
                             Notices.strUSER_NAME = dr["USER_NAME"].ToString();
                             Notices.strUSER_ROLE = dr["USER_ROLE"].ToString();
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





         public List<Training> mGetTrain(Training obj)
         {
             string strSQL = null;
             string connectionString = Utility.SQLConnstringComSwitch("0001");
             List<Training> TrainingList = new List<Training>();

             using (SqlConnection gcnMain = new SqlConnection(connectionString))
             {
                 gcnMain.Open();

                 strSQL = "select * from HRS_TRANNING_USER  N ,HRS_TRAINING_DASH U,HRS_MARKET_ACCESS_TRAINING M where N.NOTICE_ID= U.NOTICE_ID AND N.NOTICE_ID= M.NOTICE_ID ";

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
                             Train.strINSERT_BY = dr["INSERT_BY"].ToString();
                             Train.strNATIONAL_HEAD = dr["NATIONAL_HEAD"].ToString();
                             Train.strTEAM = dr["TEAM"].ToString();
                             Train.strZONE = dr["ZONE"].ToString();
                             Train.strDIVISION = dr["DIVISION"].ToString();
                             Train.strAREA = dr["AREA"].ToString();
                             Train.strMARKET = dr["MARKET"].ToString();
                             Train.strROUTE = dr["ROUTE"].ToString();
                             Train.strUSER_ROLE = dr["USER_ROLE"].ToString();
                             Train.strEMP_CARD_NO = dr["EMP_CARD_NO"].ToString();
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



         public List<Exam> mGetExam(Exam obj)
         {
             string strSQL = null;
             string connectionString = Utility.SQLConnstringComSwitch("0001");
             List<Exam> ExamList = new List<Exam>();

             using (SqlConnection gcnMain = new SqlConnection(connectionString))
             {
                 gcnMain.Open();

             


                 strSQL = "SELECT * FROM HRS_EXAM_USER N, HRS_EXAM_DETAILS U, HRS_EXAM_QUESTION M " +
                 "WHERE N.examId = U.examId AND N.examId = M.examId " +
                 "AND CAST(EXAM_DATE AS datetime) >= CAST(GETDATE() AS date)";

                 using (SqlCommand cmd = new SqlCommand(strSQL, gcnMain))
                 {
                     using (SqlDataReader dr = cmd.ExecuteReader())
                     {
                         while (dr.Read())
                         {
                             Exam Exams = new Exam();

                             Exams.strexamId = dr["examId"].ToString();
                             Exams.strEXAM_TITLE = dr["EXAM_TITLE"].ToString();
                             Exams.strEXAM_NOTICE = dr["EXAM_NOTICE"].ToString();
                             Exams.strEMP_CARD_NO = dr["EMP_CARD_NO"].ToString();
                             Exams.strNATIONAL_HEAD = dr["NATIONAL_HEAD"].ToString();
                             Exams.strTEAM = dr["TEAM"].ToString();
                             Exams.strZONE = dr["ZONE"].ToString();
                             Exams.strDIVISION = dr["DIVISION"].ToString();
                             Exams.strAREA = dr["AREA"].ToString();
                             Exams.strMARKET = dr["MARKET"].ToString();
                             Exams.strROUTE = dr["ROUTE"].ToString();
                             Exams.strROLE = dr["ROLE"].ToString();
                             Exams.strTOTAL_MARKS = dr["TOTAL_MARKS"].ToString();
                             Exams.strPASS_MARKS = dr["PASS_MARKS"].ToString();
                             Exams.strTIMELIMIT = dr["TIMELIMIT"].ToString();
                             Exams.strEXAM_DATE = Convert.ToDateTime(dr["EXAM_DATE"]).ToString("dd-MM-yyyy");
                             Exams.strSTART_TIME = Convert.ToDateTime(dr["START_TIME"]).ToString("hh:mm tt");
                             Exams.strEND_TIME = Convert.ToDateTime(dr["END_TIME"]).ToString("hh:mm tt");
                             Exams.strTITLE = dr["TITLE"].ToString();
                             Exams.strMARKS = dr["MARKS"].ToString();
                             Exams.strTYPE = dr["TYPE"].ToString();
                             Exams.strANSWER = dr["ANSWER"].ToString();
                             Exams.strOPTIONS = dr["OPTIONS"].ToString();

                             ExamList.Add(Exams);
                         }
                     }
                 }
             }

             return ExamList;
         }



         public string mPostExamResult(ExamResult obj)
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

                     {
 
}


                     SqlCommand cmdInsert = new SqlCommand();
                     SqlTransaction myTrans;
                     myTrans = gcnMain.BeginTransaction();
                     cmdInsert.Connection = gcnMain;
                     cmdInsert.Transaction = myTrans;
                     strSQL = "INSERT INTO HRS_EXAM_RESULTS (";
                     strSQL = strSQL + " EXAMID,EMP_CARD_NO, USER_TYPE,MPO_NAME, ";
                     strSQL = strSQL + "TOTAL_SCORE, CORRECT, ";
                     strSQL = strSQL + " INCORRECT, RESULT , TOTAL_MARKS , EXAMTIME , SUBMITTED_TIME ";
                     strSQL = strSQL + ") ";
                     strSQL = strSQL + "VALUES (";
                     strSQL = strSQL + "'" + obj.strexamId + "',";
                     strSQL = strSQL + "'" + obj.strcardNO + "',";
                     strSQL = strSQL + "'" + obj.struserType + "',";
                     strSQL = strSQL + "'" + obj.strledgerName + "',";
                     strSQL = strSQL + "'" + obj.inttotalScore + "',";
                     strSQL = strSQL + "'" + obj.intcorrect + "',";
                     strSQL = strSQL + "'" + obj.intincorrect + "',";
                     strSQL = strSQL + "'" + obj.strresult + "',";
                     strSQL = strSQL + "'" + obj.inttotalMark + "',";
                     strSQL = strSQL + "'" + obj.strexamTime + "',";
                     strSQL = strSQL + "'" + obj.strsubmittedTime + "'";

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


         public List<DailyTask> mGetdailyTask(DailyTask obj)
         {
             string strSQL = null;
             string connectionString = Utility.SQLConnstringComSwitch("0001");
             List<DailyTask> DailyTaskList = new List<DailyTask>();

             using (SqlConnection gcnMain = new SqlConnection(connectionString))
             {
                 gcnMain.Open();

                 strSQL = "SELECT * FROM HRS_DAILY_TASK WHERE EMP_CARD_NO = @EMP_CARD_NO AND DEADLINE = @DEADLINE";

                 using (SqlCommand cmd = new SqlCommand(strSQL, gcnMain))
                 {
                     cmd.Parameters.AddWithValue("@EMP_CARD_NO", obj.strEMP_CARD_NO);
                     cmd.Parameters.AddWithValue("@DEADLINE", Convert.ToDateTime(obj.strDEADLINE));

                     using (SqlDataReader dr = cmd.ExecuteReader())
                     {
                         while (dr.Read())
                         {





                             DailyTask DailyTasks = new DailyTask();
                             DailyTasks.strTASK_ID = dr["TASK_ID"].ToString();
                             DailyTasks.strTITLE = dr["TITLE"].ToString();
                             DailyTasks.strBODY = dr["BODY"].ToString();
                             DailyTasks.strDEADLINE = Convert.ToDateTime(dr["DEADLINE"]).ToString("dd-MM-yyyy");
                             DailyTasks.strSTATUS = dr["STATUS"].ToString();
                             DailyTasks.strNATIONAL_HEAD = dr["NATIONAL_HEAD"].ToString();
                             DailyTasks.strTEAM = dr["TEAM"].ToString();
                             DailyTasks.strZONE = dr["ZONE"].ToString();
                             DailyTasks.strDIVISION = dr["DIVISION"].ToString();
                             DailyTasks.strAREA = dr["AREA"].ToString();
                             DailyTasks.strMARKET = dr["MARKET"].ToString();
                             DailyTasks.strROUTE = dr["ROUTE"].ToString();
                             DailyTasks.strROLE = dr["ROLE"].ToString();
                             DailyTasks.strEMP_CARD_NO = dr["EMP_CARD_NO"].ToString();
                             DailyTaskList.Add(DailyTasks);
                         }
                     }
                 }
             }

             return DailyTaskList;
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
                         strSQL = strSQL + " VISIT_NAME,USER_NAME, EMP_CARD_NO, VISITED_WITH, DOCTOR_NAME, INSTITUTE, CHAMBER, MARKET, VISITED_AT, SHIFT, LOCATION, DISTATNCE, STATUS, NOTE ";
                         strSQL = strSQL + ")";
                         strSQL = strSQL + " VALUES(";
                         strSQL = strSQL + "'" + obj.strVISIT_NAME + "',";
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




         public List<DoctorVisitType> mGetDoctorLedgerType(DoctorVisitType obj)
         {
             string strSQL = null;
             string connectionString = Utility.SQLConnstringComSwitch("0001");
             List<DoctorVisitType> DoctorlistList = new List<DoctorVisitType>();

             using (SqlConnection gcnMain = new SqlConnection(connectionString))
             {
                 gcnMain.Open();

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




         public List<DoctorVisit> mPostDoctorVisitsss(DoctorVisit obj)
         {
             string connectionString = Utility.SQLConnstringComSwitch("0001");
             List<DoctorVisit> DoctorVisitList = new List<DoctorVisit>();

             try
             {
                 using (SqlConnection connection = new SqlConnection(connectionString))
                 {
                     connection.Open();

                     string strSQL = "SELECT * FROM HRS_DOCTOR_VISIT_NEW WHERE CardNO = @CardNO AND MONTH(Created_Date) = MONTH(@Created_Date) AND YEAR(Created_Date) = YEAR(@Created_Date);";

                     using (SqlCommand command = new SqlCommand(strSQL, connection))
                     {
                         command.Parameters.AddWithValue("@CardNO", obj.strCardNO);
                         command.Parameters.AddWithValue("@Created_Date", obj.strCreated_Date);

                         using (SqlDataReader reader = command.ExecuteReader())
                         {
                             while (reader.Read())
                             {
                                 DoctorVisit Doctor = new DoctorVisit();

                                
                                 Doctor.strUser_Name = reader["User_Name"].ToString();
                                 Doctor.strCardNO = reader["CardNO"].ToString();
                                 Doctor.strVisited_With = reader["Visited_With"].ToString();
                                 Doctor.strDoctor = reader["Doctor"].ToString();
                                 Doctor.strInstitute = reader["Institute"].ToString();
                                 Doctor.strChamber = reader["Chamber"].ToString();
                                 Doctor.strMarket = reader["Market"].ToString();
                                 Doctor.strVisited_At = reader["Visited_At"].ToString();
                                 Doctor.strShift = reader["Shift"].ToString();
                                 Doctor.strLocation = reader["Location"].ToString();
                                 Doctor.strDistance = reader["Distance"].ToString();
                                 Doctor.strActions = reader["Actions"].ToString();
                                 Doctor.strNOTE = reader["NOTE"].ToString();

                                 DoctorVisitList.Add(Doctor);
                             }
                         }
                     }
                 }
             }
             catch (Exception ex)
             {
                 
                 Console.WriteLine("An error occurred while retrieving doctor visits: " + ex.Message);
             }

            
             if (DoctorVisitList.Count == 0)
             {
                 DoctorVisitList.Add(new DoctorVisit());
             }

             return DoctorVisitList;
         }




         //public List<ProductName> mGetProductName(ProductName obj)
         //{
         //    string strSQL = null;
         //    string connectionString = Utility.SQLConnstringComSwitch("0001");
         //    List<ProductName> ProductNameList = new List<ProductName>();

         //    using (SqlConnection gcnMain = new SqlConnection(connectionString))
         //    {
         //        gcnMain.Open();

         //        strSQL = "select * from SMART0005.dbo.INV_STOCKITEM where STOCKITEM_PRIMARY_GROUP= 'Finished Goods'";

         //        using (SqlCommand cmd = new SqlCommand(strSQL, gcnMain))
         //        {
         //            using (SqlDataReader dr = cmd.ExecuteReader())
         //            {
         //                while (dr.Read())
         //                {
         //                    ProductName Product = new ProductName();
         //                    Product.strSTOCKITEM_NAME = dr["STOCKITEM_NAME"].ToString();
         //                    Product.strSTOCKITEM_ALIAS = dr["STOCKITEM_ALIAS"].ToString();
         //                    Product.strSTOCKGROUP_NAME = dr["STOCKGROUP_NAME"].ToString();
         //                    Product.strSTOCKITEM_PRIMARY_GROUP = dr["STOCKITEM_PRIMARY_GROUP"].ToString();
         //                    Product.strSTOCKCATEGORY_NAME = dr["STOCKCATEGORY_NAME"].ToString();

         //                    ProductNameList.Add(Product);
         //                }
         //            }
         //        }
         //    }

         //    if (ProductNameList.Count == 0)
         //    {

         //        ProductNameList.Add(new ProductName());
         //    }

         //    return ProductNameList;
         //}



         public List<ProductName> mGetProductName(ProductName obj)
         {
             string strSQL = null;
             string connectionString = Utility.SQLConnstringComSwitch("0001");
             List<ProductName> ProductNameList = new List<ProductName>();

             using (SqlConnection gcnMain = new SqlConnection(connectionString))
             {
                 gcnMain.Open();

                 strSQL = "SELECT *FROM SMART0005.dbo.INV_STOCKITEM AS s INNER JOIN SMART0005.dbo.INV_SALES_ITEM_PRICE_VIEW AS p ON s.STOCKITEM_NAME = p.STOCKITEM_NAME WHERE s.STOCKITEM_PRIMARY_GROUP = 'Finished Goods';";

                 using (SqlCommand cmd = new SqlCommand(strSQL, gcnMain))
                 {
                     using (SqlDataReader dr = cmd.ExecuteReader())
                     {
                         while (dr.Read())
                         {
                             ProductName Product = new ProductName();
                             Product.strSTOCKITEM_NAME = dr["STOCKITEM_NAME"].ToString();
                             Product.strSTOCKITEM_ALIAS = dr["STOCKITEM_ALIAS"].ToString();
                             Product.strSTOCKGROUP_NAME = dr["STOCKGROUP_NAME"].ToString();
                             Product.strSTOCKITEM_PRIMARY_GROUP = dr["STOCKITEM_PRIMARY_GROUP"].ToString();
                             Product.strSTOCKCATEGORY_NAME = dr["STOCKCATEGORY_NAME"].ToString();
                             Product.strSALES_PRICE_AMOUNT = dr["SALES_PRICE_AMOUNT"].ToString();


                             ProductNameList.Add(Product);
                         }
                     }
                 }
             }

             if (ProductNameList.Count == 0)
             {

                 ProductNameList.Add(new ProductName());
             }

             return ProductNameList;
         }



        
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









         public List<MPO> gstrGetMpoAreaDevisionList(MPO obj)
         {


             string strSQL = "", strbid = "", strUserRole = "";

             SqlDataReader drGetGroup;
             List<MPO> oogrp = new List<MPO>();
             MPO ogrp = new MPO();
             SqlCommand cmd = new SqlCommand();
             connstring = Utility.SQLConnstringComSwitch(obj.strDeComID);
             using (SqlConnection gcnMain = new SqlConnection(connstring))

                 connstring = Utility.SQLConnstringComSwitch(obj.strDeComID);
             using (SqlConnection gcnMain = new SqlConnection(connstring))
             {
                 if (gcnMain.State == ConnectionState.Open)
                 {
                     gcnMain.Close();
                 }
                 gcnMain.Open();
                 cmd.Connection = gcnMain;

                 if (obj.intAdnin == 0)
                 {
                     strSQL = "SELECT USER_ROLE FROM SMART0005.dbo.USER_ONLILE_SECURITY ";
                     strSQL = strSQL + "WHERE STATUS=0 ";
                     strSQL = strSQL + "AND (USER_ID =" + Utility.Val(obj.strUserID) + " ";
                     strSQL = strSQL + "OR EMP_CARD_NO ='" + obj.strCardNo + "')";
                     cmd.CommandText = strSQL;
                     drGetGroup = cmd.ExecuteReader();
                     if (drGetGroup.Read())
                     {
                         strUserRole = drGetGroup["USER_ROLE"].ToString().Trim();
                     }
                     drGetGroup.Close();

                     strSQL = "SELECT USER_ID,BRANCH_ID FROM SMART0005.dbo.USER_ONLILE_SECURITY ";
                     strSQL = strSQL + "WHERE STATUS=0 ";
                     strSQL = strSQL + "AND (USER_ID =" + Utility.Val(obj.strUserID) + " ";
                     strSQL = strSQL + "OR EMP_CARD_NO ='" + obj.strCardNo + "')";
                     cmd.CommandText = strSQL;
                     drGetGroup = cmd.ExecuteReader();
                     if (!drGetGroup.HasRows)
                     {

                         ogrp.strResponse = "User ID Mismatch" + Environment.NewLine + " ইউজার আইডি অমিল";

                         return obj.ogrp;
                     }
                     drGetGroup.Close();
                     strSQL = "SELECT USER_ID,BRANCH_ID FROM SMART0005.dbo.USER_ONLILE_SECURITY ";
                     strSQL = strSQL + "WHERE STATUS=0 ";
                     strSQL = strSQL + "AND PASSWORD ='" + obj.strPassWord + "' ";

                     strSQL = strSQL + "AND (USER_ID =" + Utility.Val(obj.strUserID) + " ";
                     strSQL = strSQL + "OR EMP_CARD_NO ='" + obj.strCardNo + "')";
                     cmd.CommandText = strSQL;
                     drGetGroup = cmd.ExecuteReader();
                     if (!drGetGroup.HasRows)
                     {

                         ogrp.strResponse = "Password Mismatch" + Environment.NewLine + " পাসওয়ার্ড মেলেনি";


                         return obj.ogrp;
                     }
                     drGetGroup.Close();


                     if (strUserRole == "MPO")
                     {
                         strSQL = "SELECT SMART0005.dbo.USER_ONLILE_SECURITY.BRANCH_ID,SMART0005.dbo.USER_ONLILE_SECURITY.USER_ID,SMART0005.dbo.USER_ONLILE_SECURITY.PASSWORD,SMART0005.dbo.TEAM_CONFIG.TEAM_NAME ,SMART0005.dbo.USER_ONLILE_SECURITY.COR_MOBILE_NO,SMART0005.dbo.USER_ONLILE_SECURITY.USER_ROLE,SMART0005.dbo.ACC_LEDGER_Z_D_A.LEDGER_NAME_MERZE ,";
                         strSQL = strSQL + "SMART0005.dbo.USER_ONLILE_SECURITY.EMP_CARD_NO ,SMART0005.dbo.ACC_LEDGER_Z_D_A.TERITORRY_CODE TC ,SMART0005.dbo.ACC_LEDGER_Z_D_A.TERRITORRY_NAME TCNAME,SMART0005.dbo.USER_ONLILE_SECURITY.MPO_TYPE,SMART0005.dbo.USER_ONLILE_SECURITY.LIST_M_D_A, ";
                         strSQL = strSQL + "SMART0005.dbo.ACC_LEDGER_Z_D_A.ZONE,SMART0005.dbo.ACC_LEDGER_Z_D_A.DIVISION,SMART0005.dbo.ACC_LEDGER_Z_D_A.AREA,SMART0005.dbo.ACC_LEDGER_Z_D_A.LEDGER_NAME,SMART0005.dbo.USER_ONLILE_SECURITY.BRANCH_ID  ";
                         strSQL = strSQL + "FROM SMART0005.dbo.USER_ONLILE_SECURITY,SMART0005.dbo.ACC_LEDGER_Z_D_A,SMART0005.dbo.TEAM_CONFIG  WHERE  SMART0005.dbo.ACC_LEDGER_Z_D_A.MPO_CARD_NO =SMART0005.dbo.USER_ONLILE_SECURITY.EMP_CARD_NO  ";
                         strSQL = strSQL + "AND SMART0005.dbo.ACC_LEDGER_Z_D_A.ZONE =SMART0005.dbo.TEAM_CONFIG.ZONE_NAME ";
                         strSQL = strSQL + "AND (SMART0005.dbo.USER_ONLILE_SECURITY.USER_ID =" + Utility.Val(obj.strUserID) + " ";
                         strSQL = strSQL + "OR SMART0005.dbo.USER_ONLILE_SECURITY.EMP_CARD_NO ='" + obj.strCardNo + "')";
                     }
                     else if (strUserRole == "AH")
                     {

                         strSQL = "SELECT DISTINCT SMART0005.dbo.USER_ONLILE_SECURITY.BRANCH_ID,SMART0005.dbo.USER_ONLILE_SECURITY.USER_ID,SMART0005.dbo.USER_ONLILE_SECURITY.PASSWORD,SMART0005.dbo.TEAM_CONFIG.TEAM_NAME ,SMART0005.dbo.USER_ONLILE_SECURITY.COR_MOBILE_NO,SMART0005.dbo.USER_ONLILE_SECURITY.USER_ROLE,SMART0005.dbo.ACC_LEDGER_Z_D_A.AREA LEDGER_NAME_MERZE  ";
                         strSQL = strSQL + ",SMART0005.dbo.USER_ONLILE_SECURITY.EMP_CARD_NO ,'' TC ,'' TCNAME,SMART0005.dbo.USER_ONLILE_SECURITY.MPO_TYPE,SMART0005.dbo.USER_ONLILE_SECURITY.LIST_M_D_A, SMART0005.dbo.ACC_LEDGER_Z_D_A.ZONE,SMART0005.dbo.ACC_LEDGER_Z_D_A.DIVISION,SMART0005.dbo.ACC_LEDGER_Z_D_A.AREA ";
                         strSQL = strSQL + ",SMART0005.dbo.ACC_LEDGER_Z_D_A.AREA LEDGER_NAME,SMART0005.dbo.USER_ONLILE_SECURITY.BRANCH_ID FROM SMART0005.dbo.USER_ONLILE_SECURITY,SMART0005.dbo.ACC_LEDGERGROUP,SMART0005.dbo.ACC_LEDGER_Z_D_A,SMART0005.dbo.TEAM_CONFIG WHERE SMART0005.dbo.ACC_LEDGERGROUP.EMP_CARD_NO =SMART0005.dbo.USER_ONLILE_SECURITY.EMP_CARD_NO AND SMART0005.dbo.ACC_LEDGER_Z_D_A.AREA =SMART0005.dbo.ACC_LEDGERGROUP.GR_NAME ";
                         strSQL = strSQL + "AND SMART0005.dbo.ACC_LEDGER_Z_D_A.ZONE =SMART0005.dbo.TEAM_CONFIG.ZONE_NAME ";
                         strSQL = strSQL + "AND (SMART0005.dbo.USER_ONLILE_SECURITY.USER_ID =" + Utility.Val(obj.strUserID) + " ";
                         strSQL = strSQL + "OR SMART0005.dbo.USER_ONLILE_SECURITY.EMP_CARD_NO ='" + obj.strCardNo + "')";
                     }
                     else if (strUserRole == "DH")
                     {
                         strSQL = "SELECT DISTINCT SMART0005.dbo.USER_ONLILE_SECURITY.BRANCH_ID,SMART0005.dbo.USER_ONLILE_SECURITY.USER_ID,SMART0005.dbo.USER_ONLILE_SECURITY.PASSWORD,SMART0005.dbo.TEAM_CONFIG.TEAM_NAME ,SMART0005.dbo.USER_ONLILE_SECURITY.COR_MOBILE_NO,SMART0005.dbo.USER_ONLILE_SECURITY.USER_ROLE,SMART0005.dbo.ACC_LEDGER_Z_D_A.DIVISION LEDGER_NAME_MERZE  ";
                         strSQL = strSQL + ",SMART0005.dbo.USER_ONLILE_SECURITY.EMP_CARD_NO ,'' TC ,'' TCNAME,SMART0005.dbo.USER_ONLILE_SECURITY.MPO_TYPE,SMART0005.dbo.USER_ONLILE_SECURITY.LIST_M_D_A, SMART0005.dbo.ACC_LEDGER_Z_D_A.ZONE,'' AREA,SMART0005.dbo.ACC_LEDGER_Z_D_A.DIVISION ";
                         strSQL = strSQL + ",SMART0005.dbo.ACC_LEDGER_Z_D_A.DIVISION LEDGER_NAME,SMART0005.dbo.USER_ONLILE_SECURITY.BRANCH_ID FROM SMART0005.dbo.USER_ONLILE_SECURITY,SMART0005.dbo.ACC_LEDGERGROUP,SMART0005.dbo.ACC_LEDGER_Z_D_A,SMART0005.dbo.TEAM_CONFIG WHERE SMART0005.dbo.ACC_LEDGERGROUP.EMP_CARD_NO =SMART0005.dbo.USER_ONLILE_SECURITY.EMP_CARD_NO AND SMART0005.dbo.ACC_LEDGER_Z_D_A.DIVISION =SMART0005.dbo.ACC_LEDGERGROUP.GR_NAME ";
                         strSQL = strSQL + "AND SMART0005.dbo.ACC_LEDGER_Z_D_A.ZONE =SMART0005.dbo.TEAM_CONFIG.ZONE_NAME ";
                         strSQL = strSQL + "AND (SMART0005.dbo.USER_ONLILE_SECURITY.USER_ID =" + Utility.Val(obj.strUserID) + " ";
                         strSQL = strSQL + "OR SMART0005.dbo.USER_ONLILE_SECURITY.EMP_CARD_NO ='" + obj.strCardNo + "')";
                     }
                     else if (strUserRole == "ZH")
                     {
                         strSQL = "SELECT DISTINCT USER_ONLILE_SECURITY.BRANCH_ID,USER_ONLILE_SECURITY.USER_ID,USER_ONLILE_SECURITY.PASSWORD,TEAM_CONFIG.TEAM_NAME ,USER_ONLILE_SECURITY.COR_MOBILE_NO,USER_ONLILE_SECURITY.USER_ROLE,ACC_LEDGER_Z_D_A.DIVISION LEDGER_NAME_MERZE  ";
                         strSQL = strSQL + ",USER_ONLILE_SECURITY.EMP_CARD_NO ,'' TC ,'' TCNAME,USER_ONLILE_SECURITY.MPO_TYPE,USER_ONLILE_SECURITY.LIST_M_D_A, ACC_LEDGER_Z_D_A.ZONE,'' AREA,ACC_LEDGER_Z_D_A.DIVISION ";
                         strSQL = strSQL + ",ACC_LEDGER_Z_D_A.DIVISION LEDGER_NAME,SMART0005.dbo.USER_ONLILE_SECURITY.BRANCH_ID FROM USER_ONLILE_SECURITY,ACC_LEDGERGROUP,ACC_LEDGER_Z_D_A,TEAM_CONFIG WHERE ACC_LEDGERGROUP.EMP_CARD_NO =USER_ONLILE_SECURITY.EMP_CARD_NO AND ACC_LEDGER_Z_D_A.DIVISION =ACC_LEDGERGROUP.GR_NAME ";
                         strSQL = strSQL + "AND ACC_LEDGER_Z_D_A.ZONE =TEAM_CONFIG.ZONE_NAME ";
                         strSQL = strSQL + "AND (SMART0005.dbo.USER_ONLILE_SECURITY.USER_ID =" + Utility.Val(obj.strUserID) + " ";
                         strSQL = strSQL + "OR SMART0005.dbo.USER_ONLILE_SECURITY.EMP_CARD_NO ='" + obj.strCardNo + "')";
                     }
                     else
                     {
                         strSQL = "SELECT DISTINCT BRANCH_ID, USER_ID, PASSWORD,'' TEAM_NAME, COR_MOBILE_NO, USER_ROLE,'' LEDGER_NAME_MERZE,'' EMP_CARD_NO, '' AS TC, '' AS TCNAME, MPO_TYPE, LIST_M_D_A,'' ZONE, '' AS AREA,'' DIVISION,'' LEDGER_NAME ";
                         strSQL = strSQL + " FROM SMART0005.dbo.USER_ONLILE_SECURITY WHERE USER_ID =" + Utility.Val(obj.strUserID) + " ";


                     }
                     cmd.Connection = gcnMain;
                     cmd.CommandText = strSQL;
                     drGetGroup = cmd.ExecuteReader();
                     if (drGetGroup.Read())
                     {

                         //MPO ogrp = new MPO();
                         if (drGetGroup["BRANCH_ID"].ToString() != "")
                         {
                             strbid = drGetGroup["BRANCH_ID"].ToString();
                         }
                         //if (strbid != obj.branchid)
                         //{

                         //    ogrp.strResponse = "Branch ID Mismatch" + Environment.NewLine + " শাখা আইডি অমিল";
                         //}
                         //else
                         //{

                         ogrp.strbranchID = drGetGroup["BRANCH_ID"].ToString();
                         ogrp.strRole = drGetGroup["USER_ROLE"].ToString();
                         ogrp.strUserID = drGetGroup["USER_ID"].ToString();
                         ogrp.strUserPassword = drGetGroup["PASSWORD"].ToString();
                         ogrp.strTeritorryCode = drGetGroup["TC"].ToString();
                         ogrp.strTeritorryName = drGetGroup["TCNAME"].ToString();
                         if (drGetGroup["LEDGER_NAME"].ToString() != "")
                         {
                             ogrp.strLedgerName = drGetGroup["LEDGER_NAME"].ToString();
                         }
                         else
                         {
                             ogrp.strLedgerName = "";
                         }
                         if (drGetGroup["LEDGER_NAME_MERZE"].ToString() != "")
                         {
                             ogrp.strMerzeName = drGetGroup["LEDGER_NAME_MERZE"].ToString();
                         }
                         else
                         {
                             ogrp.strMerzeName = "";
                         }
                         ogrp.intMpoType = Convert.ToInt16(drGetGroup["MPO_TYPE"].ToString());
                         if (drGetGroup["COR_MOBILE_NO"].ToString() != "")
                         {
                             ogrp.strMobileNo = drGetGroup["COR_MOBILE_NO"].ToString();
                         }
                         else
                         {
                             ogrp.strMobileNo = "";
                         }

                         ogrp.lngUniqueNo = Convert.ToInt16(drGetGroup["LIST_M_D_A"].ToString());
                         ogrp.strResponse = "Yes";
                         if (drGetGroup["EMP_CARD_NO"].ToString() != "")
                         {
                             ogrp.strCardNo = drGetGroup["EMP_CARD_NO"].ToString();
                             ogrp.strEMP_CARD_NO = drGetGroup["EMP_CARD_NO"].ToString();
                         }
                         else
                         {
                             ogrp.strCardNo = "";
                         }

                         if (drGetGroup["TEAM_NAME"].ToString() != "")
                         {
                             ogrp.strTeam = drGetGroup["TEAM_NAME"].ToString();
                         }
                         else
                         {
                             ogrp.strTeam = "";
                         }
                         if (drGetGroup["ZONE"].ToString() != "")
                         {
                             ogrp.strZone = drGetGroup["ZONE"].ToString();
                         }
                         else
                         {
                             ogrp.strZone = "";
                         }
                         if (drGetGroup["DIVISION"].ToString() != "")
                         {
                             ogrp.strDivision = drGetGroup["DIVISION"].ToString();
                         }
                         else
                         {
                             ogrp.strDivision = "";
                         }
                         if (drGetGroup["AREA"].ToString() != "")
                         {
                             ogrp.strArea = drGetGroup["AREA"].ToString();
                         }
                         else
                         {
                             ogrp.strArea = "";
                         }

                         //}
                         oogrp.Add(ogrp);
                     }

                     drGetGroup.Close();
                 }
                 else
                 {


                     strSQL = "SELECT SMART0005.dbo.USER_CONFIG.USER_LOGIN_SERIAL,SMART0005.dbo.USER_CONFIG.USER_LOGIN_NAME,SMART0005.dbo.USER_CONFIG.USER_PASS,SMART0005.dbo.USER_CONFIG.USER_LEBEL,SMART0005.dbo.USER_CONFIG.USER_STATUS ";
                     strSQL = strSQL + "FROM SMART0005.dbo.USER_CONFIG,USER_PRIVILEGES_BRANCH WHERE SMART0005.dbo.USER_CONFIG.USER_LOGIN_NAME =SMART0005.dbo.USER_PRIVILEGES_BRANCH.USER_LOGIN_NAME ";
                     strSQL = strSQL + "AND SMART0005.dbo.USER_PRIVILEGES_BRANCH.USER_LOGIN_NAME='" + obj.strUserID + "' and SMART0005.dbo.USER_PRIVILEGES_BRANCH.BRANCH_ID IN (SELECT BRANCH_ID FROM SMART0005.dbo.USER_PRIVILEGES_BRANCH WHERE BRANCH_ID = '" + obj.branchid + "') ";
                     cmd.Connection = gcnMain;
                     cmd.CommandText = strSQL;
                     drGetGroup = cmd.ExecuteReader();
                     if (drGetGroup.Read())
                     {

                         if (drGetGroup["USER_STATUS"].ToString() == "S")
                         {
                             ogrp.strResponse = "Sorry, The User's has been suspended, Please contact with Administrator" + Environment.NewLine + " দুঃখিত, ব্যবহারকারীর স্থগিত করা হয়েছে, প্রশাসকের সাথে যোগাযোগ করুন";
                         }
                         string vstrPassword = Utility.Decrypt(drGetGroup["USER_PASS"].ToString(), drGetGroup["USER_LOGIN_NAME"].ToString()).ToString();

                         if (vstrPassword.Trim() != obj.strPassWord.Trim())
                         {
                             ogrp.strResponse = "Login failed. Make sure user name and password are correct." + Environment.NewLine + " লগইন ব্যর্থ. নিশ্চিত করুন যে ব্যবহারকারীর নাম এবং পাসওয়ার্ড সঠিক";
                             ogrp.strUserID = drGetGroup["USER_LOGIN_NAME"].ToString();
                             ogrp.strUserPassword = "";
                             ogrp.strbranchID = "";
                             ogrp.strTeritorryCode = "";
                             ogrp.strTeritorryName = "";
                             ogrp.strLedgerName = "";
                             ogrp.intMpoType = 0;
                             ogrp.strCardNo = "";
                             oogrp.Add(ogrp);
                         }
                         else
                         {
                             ogrp.strUserID = drGetGroup["USER_LOGIN_NAME"].ToString();
                             ogrp.strUserPassword = "";
                             ogrp.strbranchID = "";
                             ogrp.strTeritorryCode = "";
                             ogrp.strTeritorryName = "";
                             ogrp.strLedgerName = "";
                             ogrp.intMpoType = 1;
                             ogrp.strResponse = "Yes";
                             ogrp.strCardNo = "";
                             oogrp.Add(ogrp);
                         }

                     }
                     else
                     {
                         ogrp.strUserID = "";
                         ogrp.strUserPassword = "";
                         ogrp.strbranchID = "";
                         ogrp.strTeritorryCode = "";
                         ogrp.strTeritorryName = "";
                         ogrp.strLedgerName = "";
                         ogrp.intMpoType = 0;
                         ogrp.strCardNo = "";

                         ogrp.strResponse = "Branch ID Mismatch" + Environment.NewLine + " শাখা আইডি অমিল";
                         oogrp.Add(ogrp);
                     }
                 }
                 drGetGroup.Close();
                 gcnMain.Dispose();
                 return oogrp;
             }
         }


         public string connstring { get; set; }
    }
}