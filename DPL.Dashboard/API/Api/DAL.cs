using Api.Models;
using Dutility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

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
                    strSQL = "INSERT INTO HRS_TRANS_WORK_ATTENDANCE_NEW (";
                    strSQL = strSQL + " USER_NAME, ROLE,EMP_CARD_NO, ";
                    strSQL = strSQL + "TC, ATTEN_DATEIN, ";
                    strSQL = strSQL + " ATTEN_TIMEIN, LATITUDE , LONGITUDE , ADDRESS , DISTANCE, TOTAL_WORKING_HOUR ,STAY_HOUR,ATTEN_TIMEOUT,ATTEN_STATUS,ATTEN_SHIFT,SHIFT_START,ATTEN_COMMENTS, ACTION, EMP_IMAGE , EMP_JPEG_DOC ";
                    strSQL = strSQL + ") ";
                    strSQL = strSQL + "VALUES (";
                    strSQL = strSQL + "'" + obj.strUSER_NAME + "',";
                    strSQL = strSQL + "'" + obj.strROLE + "',";
                    strSQL = strSQL + "'" + obj.strEMP_CARD_NO + "',";
                    strSQL = strSQL + "'" + obj.strTC + "',";
                    strSQL = strSQL + "'" + obj.strATTEN_DATEIN + "',";
                    strSQL = strSQL + "'" + obj.strATTEN_TIMEIN + "',";
                    strSQL = strSQL + "'" + obj.strLATITUDE + "',";
                    strSQL = strSQL + "'" + obj.strLONGITUDE + "',";
                    strSQL = strSQL + "'" + obj.strADDRESS + "',";
                    strSQL = strSQL + "'" + obj.intDISTANCE + "',";
                    strSQL = strSQL + "'" + obj.intTOTAL_WORKING_HOUR + "',";
                    strSQL = strSQL + "'" + obj.strSTAY_HOUR + "',";
                    strSQL = strSQL + "'" + obj.strATTEN_TIMEOUT + "',";
                    strSQL = strSQL + "'" + obj.strATTEN_STATUS + "',";
                    strSQL = strSQL + "'" + obj.strATTEN_SHIFT+ "',";
                    strSQL = strSQL + "'" + obj.strSHIFT_START + "',";
                    strSQL = strSQL + "'" + obj.strATTEN_COMMENTS + "',";
                    strSQL = strSQL + "'" + obj.strACTION + "',";
                    strSQL = strSQL + "'" + obj.strEMP_IMAGE + "',";
                    strSQL = strSQL + "'" + obj.strEMP_JPEG_DOC + "'";
                    strSQL = strSQL + ")";
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

                strSQL = "SELECT  EMP_IMAGE,USER_NAME, ROLE, EMP_CARD_NO, ";
                strSQL = strSQL + "TC, ATTEN_DATEIN, ATTEN_TIMEIN, LATITUDE, LONGITUDE, ADDRESS, DISTANCE,TOTAL_WORKING_HOUR, STAY_HOUR, ATTEN_TIMEOUT, ATTEN_STATUS,ATTEN_SHIFT, SHIFT_START , ATTEN_COMMENTS ,ACTION,EMP_JPEG_DOC  ";
                strSQL = strSQL + "FROM           HRS_TRANS_WORK_ATTENDANCE_NEW ";


                SqlCommand cmd = new SqlCommand(strSQL, gcnMain);
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {

                    AttendentCongfig oLedg = new AttendentCongfig();
                    oLedg.strUSER_NAME = dr["USER_NAME"].ToString();
                    oLedg.strROLE = dr["ROLE"].ToString();
                    oLedg.strEMP_CARD_NO = dr["EMP_CARD_NO"].ToString();
                    oLedg.strTC = dr["TC"].ToString();
                    oLedg.strATTEN_DATEIN = dr["ATTEN_DATEIN"].ToString();
                    oLedg.strATTEN_TIMEIN = dr["ATTEN_TIMEIN"].ToString();
                    oLedg.strLATITUDE = dr["LATITUDE"].ToString();
                    oLedg.strLONGITUDE = dr["LONGITUDE"].ToString();
                    oLedg.strADDRESS = dr["ADDRESS"].ToString();
                    oLedg.intDISTANCE = Convert.ToInt32(dr["DISTANCE"]);
                    oLedg.intTOTAL_WORKING_HOUR = Convert.ToInt32(dr["TOTAL_WORKING_HOUR"]);
                    oLedg.strSTAY_HOUR = dr["STAY_HOUR"].ToString();
                    oLedg.strATTEN_TIMEOUT = dr["ATTEN_TIMEOUT"].ToString();
                    oLedg.strATTEN_STATUS = dr["ATTEN_STATUS"].ToString();
                    oLedg.strATTEN_SHIFT = dr["ATTEN_SHIFT"].ToString();
                    oLedg.strSHIFT_START = dr["SHIFT_START"].ToString();
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
                    oLedg.strTC = "";
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
                    oLedg.strTC = dr["TC"].ToString();
                    oLedg.strATTEN_DATEIN = dr["ATTEN_DATEIN"].ToString();
                    oLedg.strATTEN_TIMEIN = dr["ATTEN_TIMEIN"].ToString();
                    oLedg.strLATITUDE = dr["LATITUDE"].ToString();
                    oLedg.strLONGITUDE = dr["LONGITUDE"].ToString();
                    oLedg.strADDRESS = dr["ADDRESS"].ToString();
                    oLedg.intDISTANCE = Convert.ToInt32(dr["DISTANCE"]);
                    oLedg.intTOTAL_WORKING_HOUR = Convert.ToInt32(dr["TOTAL_WORKING_HOUR"]);
                    oLedg.strSTAY_HOUR = dr["STAY_HOUR"].ToString();
                    oLedg.strATTEN_TIMEOUT = dr["ATTEN_TIMEOUT"].ToString();
                    oLedg.strATTEN_STATUS = dr["ATTEN_STATUS"].ToString();
                    oLedg.strATTEN_SHIFT = dr["ATTEN_SHIFT"].ToString();
                    oLedg.strSHIFT_START = dr["SHIFT_START"].ToString();
                    oLedg.strATTEN_COMMENTS = dr["ATTEN_COMMENTS"].ToString();
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
                    oLedg.strTC = "";
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











        public string mPostUserLeave(LeaveConfig obj)
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

                    // Generate EMP_LEAVE_KEY by concatenating strEmpCardNO and FROM_DATE
                    string empLeaveKey = obj.strEMP_CARD_NO + obj.strFROM_DATE;

                    strSQL = "INSERT INTO HRS_EMP_LEAVE (";
                    strSQL += " EMP_LEAVE_KEY, EMP_CARD_NO, LEAVE_ID, FRIDAY, FROM_DATE, TO_DATE, NO_OF_DAYS,";
                    strSQL += "FIRST_DATE_MLEAVE, SECOND_DATE_MLEAVE, APPROVED_STATUS, COMMENTS, ";
                    strSQL += " RES_PEREMP_CARD_NO, FAL_HR_APP, DESTINATION, USER_LOGIN_NAME, INSERT_DATE, UPDATE_DATE, HOD_APP_DATE, HR_APP_DATE, B_LEAVE_KEY, B_M_R";
                    strSQL += ") ";

                    strSQL += "VALUES (";

                    strSQL += "'" + empLeaveKey + "',";
                    strSQL += "'" + obj.strEMP_CARD_NO + "',";
                    strSQL += "'" + obj.strLEAVE_ID + "',";
                    strSQL += "'" + obj.intFRIDAY + "',";
                    strSQL += "'" + obj.strFROM_DATE + "',";
                    strSQL += "'" + obj.strTO_DATE + "',";
                    strSQL += "'" + obj.intNO_OF_DAYS + "',";
                    strSQL += "'" + obj.strFIRST_DATE_MLEAVE + "',";
                    strSQL += "'" + obj.strSECOND_DATE_MLEAVE + "',";
                    strSQL += "'" + obj.strAPPROVED_STATUS + "',";
                    strSQL += "'" + obj.strCOMMENTS + "',";
                    strSQL += "'" + obj.strRES_PEREMP_CARD_NO + "',";
                    strSQL += "'" + obj.strFAL_HR_APP + "',";
                    strSQL += "'" + obj.strDESTINATION + "',";
                    strSQL += "'" + obj.strUSER_LOGIN_NAME + "',";
                    strSQL += "'" + obj.strINSERT_DATE + "',";
                    strSQL += "'" + obj.strUPDATE_DATE + "',";
                    strSQL += "'" + obj.strHOD_APP_DATE + "',";
                    strSQL += "'" + obj.strHR_APP_DATE + "',";
                    strSQL += "'" + obj.intB_LEAVE_KEY + "',";
                    strSQL += "'" + obj.strB_M_R + "'";
                    strSQL += ")";
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


                strSQL = "SELECT * FROM HRS_EMP_LEAVE WHERE EMP_CARD_NO='" + obj.strEMP_CARD_NO + "' AND EFFECTIVE_DATE >= '" + obj.strEFFECTIVE_DATE + "' AND EFFECTIVE_DATE <= '" + obj.strEFFECTIVE_DATE + "'";

                SqlCommand cmd = new SqlCommand(strSQL, gcnMain);
                dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    LeaveConfig oLedg = new LeaveConfig();

                    oLedg.strEMP_LEAVE_KEY = dr["EMP_LEAVE_KEY"].ToString();
                    oLedg.strEMP_CARD_NO = dr["EMP_CARD_NO"].ToString();
                    oLedg.strLEAVE_ID = dr["LEAVE_ID"].ToString();
                    oLedg.intFRIDAY = Convert.ToInt32(dr["FRIDAY"]);
                    oLedg.strFROM_DATE = Convert.ToDateTime(dr["FROM_DATE"]).ToString("yyyy-MM-dd");
                    oLedg.strTO_DATE = Convert.ToDateTime(dr["TO_DATE"]).ToString("yyyy-MM-dd");
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

                 strSQL = "select * from SMART0005.dbo.ACC_LEDGER_Z_D_A ";

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
                         strSQL = strSQL + "TOUR_TYPE, NOTE, EFFECTIVE_DATE, PURPOSE, ACCOMPANY_WITH, ";
                         strSQL = strSQL + "START_LOCATION, END_LOCATION, ACTION ";
                         strSQL = strSQL + ")";
                         strSQL = strSQL + " VALUES(";
                         strSQL = strSQL + "'" + tourPlan.strUSER_NAME + "',";
                         strSQL = strSQL + "'" + tourPlan.strEMP_CARD_NO + "',";
                         strSQL = strSQL + "'" + tourPlan.strMARKET_NAME + "',";
                         strSQL = strSQL + "'" + tourPlan.strROUTE + "',";
                         strSQL = strSQL + "'" + tourPlan.strSHIFT + "',";
                         strSQL = strSQL + "'" + tourPlan.strTOUR_TYPE + "',";
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





         public List<TourPlanShiftConfig> mGetTourPlanRouteReturn(TourPlanShiftConfig obj)
         {
             string strSQL = null;
             string connectionString = Utility.SQLConnstringComSwitch("0001");

             using (SqlConnection gcnMain = new SqlConnection(connectionString))
             {
                 gcnMain.Open();
                 SqlDataReader dr;

                 List<TourPlanShiftConfig> oooChequePrint = new List<TourPlanShiftConfig>();

                
                 strSQL = "SELECT COUNT(*) FROM HRS_EMP_TOUR_PLAN WHERE EMP_CARD_NO = @EMP_CARD_NO AND MONTH(EFFECTIVE_DATE) = MONTH(@EFFECTIVE_DATE) AND YEAR(EFFECTIVE_DATE) = YEAR(@EFFECTIVE_DATE)";
                 SqlCommand countCmd = new SqlCommand(strSQL, gcnMain);
                 countCmd.Parameters.AddWithValue("@EMP_CARD_NO", obj.strEMP_CARD_NO);
                 countCmd.Parameters.AddWithValue("@EFFECTIVE_DATE", obj.strEFFECTIVE_DATE);

                 int similarRecordCount = (int)countCmd.ExecuteScalar();

                 countCmd.Dispose();

                
                 if (similarRecordCount > 0)
                 {
                     DateTime effectiveDate;
                     
                     if (DateTime.TryParse(obj.strEFFECTIVE_DATE, out effectiveDate))
                     {
                         strSQL = "SELECT * FROM HRS_EMP_TOUR_PLAN WHERE EMP_CARD_NO = @EMP_CARD_NO AND MONTH(EFFECTIVE_DATE) = MONTH(@EFFECTIVE_DATE) AND YEAR(EFFECTIVE_DATE) = YEAR(@EFFECTIVE_DATE)";
                         
                         SqlCommand cmd = new SqlCommand(strSQL, gcnMain);
                         cmd.Parameters.AddWithValue("@EMP_CARD_NO", obj.strEMP_CARD_NO);
                         cmd.Parameters.AddWithValue("@EFFECTIVE_DATE", effectiveDate);

                         dr = cmd.ExecuteReader();

                         while (dr.Read())
                         {
                             TourPlanShiftConfig oLedg = new TourPlanShiftConfig();

                             
                             oLedg.strUSER_NAME = dr["USER_NAME"].ToString();
                             oLedg.strEMP_CARD_NO = dr["EMP_CARD_NO"].ToString();
                             oLedg.strMARKET_NAME = dr["MARKET_NAME"].ToString();
                             oLedg.strROUTE = dr["ROUTE"].ToString();
                             oLedg.strSHIFT = dr["SHIFT"].ToString();
                             oLedg.strTOUR_TYPE = dr["TOUR_TYPE"].ToString();
                             oLedg.strNOTE = dr["NOTE"].ToString();
                             oLedg.strEFFECTIVE_DATE = dr["EFFECTIVE_DATE"].ToString();
                             oLedg.strPURPOSE = dr["PURPOSE"].ToString();
                             oLedg.strACCOMPANY_WITH = dr["ACCOMPANY_WITH"].ToString();
                             oLedg.strSTART_LOCATION = dr["START_LOCATION"].ToString();
                             oLedg.strEND_LOCATION = dr["END_LOCATION"].ToString();
                             oLedg.strACTION = dr["ACTION"].ToString();

                             oooChequePrint.Add(oLedg);
                         }

                         dr.Close();
                         cmd.Dispose();
                     }
                 }

                 gcnMain.Close();

                 return oooChequePrint;
             }
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
                             Prescription.strACTION = dr["ACTION"].ToString();

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
                             Notices.strSTART_DATE = dr["START_DATE"].ToString();
                             Notices.strEND_DATE = dr["END_DATE"].ToString();
                             Notices.strCREATED_DATE = dr["CREATED_DATE"].ToString();
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
                             Notices.strUSER_NAME = dr["USER_NAME"].ToString();
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



         public List<Exam> mGetExam(Exam obj)
         {
             string strSQL = null;
             string connectionString = Utility.SQLConnstringComSwitch("0001");
             List<Exam> ExamList = new List<Exam>();

             using (SqlConnection gcnMain = new SqlConnection(connectionString))
             {
                 gcnMain.Open();


                 strSQL = "SELECT * FROM HRS_EXAM_USER_NEW N, HRS_EXAM_NEW U, HRS_EXAM_QUESTION M " +
                 "WHERE N.examId = U.examId AND N.examId = M.examId " +
                 "AND CAST(examDate AS datetime) >= CAST(GETDATE() AS date)";

                 using (SqlCommand cmd = new SqlCommand(strSQL, gcnMain))
                 {
                     using (SqlDataReader dr = cmd.ExecuteReader())
                     {
                         while (dr.Read())
                         {
                             Exam Exams = new Exam();

                             Exams.strexamId = dr["examId"].ToString();
                             Exams.strExamTitle = dr["ExamTitle"].ToString();
                             Exams.strExamNotice = dr["ExamNotice"].ToString();
                             Exams.strNATIONAL_HEAD = dr["NATIONAL_HEAD"].ToString();
                             Exams.strTEAM = dr["TEAM"].ToString();
                             Exams.strZONE = dr["ZONE"].ToString();
                             Exams.strDIVISION = dr["DIVISION"].ToString();
                             Exams.strAREA = dr["AREA"].ToString();
                             Exams.strMARKET = dr["MARKET"].ToString();
                             Exams.strROUTE = dr["ROUTE"].ToString();
                             Exams.strROLE = dr["ROLE"].ToString();
                             Exams.strtotalMarks = dr["totalMarks"].ToString();
                             Exams.strpassmarks = dr["passmarks"].ToString();
                             Exams.strtimeLimit = dr["timeLimit"].ToString();
                             Exams.strexamDate = Convert.ToDateTime(dr["examDate"]).ToString("dd-MM-yyyy");
                             Exams.strstarttime = Convert.ToDateTime(dr["starttime"]).ToString("hh:mm tt");
                             Exams.strendtime = Convert.ToDateTime(dr["endtime"]).ToString("hh:mm tt");
                             Exams.strtitle = dr["title"].ToString();
                             Exams.strmarks = dr["marks"].ToString();
                             Exams.strtype = dr["type"].ToString();
                             Exams.stranswer = dr["answer"].ToString();
                             Exams.stroptions = dr["options"].ToString();

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



                     SqlCommand cmdInsert = new SqlCommand();
                     SqlTransaction myTrans;
                     myTrans = gcnMain.BeginTransaction();
                     cmdInsert.Connection = gcnMain;
                     cmdInsert.Transaction = myTrans;
                     strSQL = "INSERT INTO HRS_EXAM_RESULTS (";
                     strSQL = strSQL + " examId,cardNO, userType,ledgerName, ";
                     strSQL = strSQL + "totalScore, correct, ";
                     strSQL = strSQL + " inCorrect, result , totalMark , examTime , submittedTime ";
                     strSQL = strSQL + ") ";
                     strSQL = strSQL + "VALUES (";
                     strSQL = strSQL + "'" + obj.strexamId + "',";
                     strSQL = strSQL + "'" + obj.strcardNO + "',";
                     strSQL = strSQL + "'" + obj.struserType + "',";
                     strSQL = strSQL + "'" + obj.strledgerName + "',";
                     strSQL = strSQL + "'" + obj.strtotalScore + "',";
                     strSQL = strSQL + "'" + obj.strcorrect + "',";
                     strSQL = strSQL + "'" + obj.strinCorrect + "',";
                     strSQL = strSQL + "'" + obj.strresult + "',";
                     strSQL = strSQL + "'" + obj.strtotalMark + "',";
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

                     string VistID = DateTime.UtcNow.ToString("yyMMddHHmmss");

                     SqlCommand cmdInsert = new SqlCommand();

                     SqlTransaction myTrans;
                     myTrans = gcnMain.BeginTransaction();
                     cmdInsert.Connection = gcnMain;
                     cmdInsert.Transaction = myTrans;


                     foreach (var obj in objs)
                     {

                         strSQL = "INSERT INTO HRS_DOCTOR_VISIT_NEW (";
                         strSQL = strSQL + "VistID, User_Name, CardNO, Visited_With, Doctor, Institute, Chamber, Market, Visited_At, Shift, Location, Distance, Actions, NOTE ";
                         strSQL = strSQL + ")";
                         strSQL = strSQL + " VALUES(";
                         strSQL = strSQL + "'" + VistID + "',";
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

                 strSQL = "select * from HRS_DOCTOR_VISIT_TYPE_NEW ";

                 using (SqlCommand cmd = new SqlCommand(strSQL, gcnMain))
                 {
                     using (SqlDataReader dr = cmd.ExecuteReader())
                     {
                         while (dr.Read())
                         {
                             DoctorVisitType Doctor = new DoctorVisitType();
                             Doctor.strVistID = dr["VistID"].ToString();
                             Doctor.strName = dr["Name"].ToString();
                             Doctor.strActions = dr["Actions"].ToString();


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

                                 Doctor.strVistID = reader["VistID"].ToString();
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
                     strSQL = "INSERT INTO HRS_TADA_CLAIM_NEW (";
                     strSQL = strSQL + " ExpenseId,Market,UserName,CardNO,TA,DA,TourType,ClaimDate,Note,Approvers,Actions";
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


    }
}