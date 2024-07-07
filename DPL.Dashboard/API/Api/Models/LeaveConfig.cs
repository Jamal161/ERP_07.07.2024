using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Api.Models
{
    public class LeaveConfig
    {
        public int intSerialNumber;
        public int intSERIAL_NO { get; set; }
        public string strHOLIDAY_DATE { get; set; }
        public string strDESCRIPTION { get; set; }
        public string strDIVISION_NAME { get; set; }

        public string strLEAVE_NAME { get; set; }

        public string strLEAVE_ID { get; set; }
        public int intNO_OF_DAYS { get; set; }
        public string strALLOW_DEDUCTION_YN { get; set; }
        public string strDEDUCTION_ON { get; set; }
        public string strREF_HEAD { get; set; }
        public string strLEAVE_NATURE { get; set; }
        public string strDEFAULT_STATUS { get; set; }

        public string strEMP_LEAVE_KEY { get; set; }
        public string strEMP_CARD_NO { get; set; }

        public int intFRIDAY { get; set; }
        public string strFROM_DATE { get; set; }
        public string strTO_DATE { get; set; }


        public string strFIRST_DATE_MLEAVE { get; set; }
        public string strSECOND_DATE_MLEAVE { get; set; }
        public string strAPPROVED_STATUS { get; set; }
        public string strCOMMENTS { get; set; }
        public string strRES_PEREMP_CARD_NO { get; set; }

        public string strFAL_HR_APP { get; set; }
        public string strDESTINATION { get; set; }
        public string strUSER_LOGIN_NAME { get; set; }
        public string strINSERT_DATE { get; set; }
        public string strUPDATE_DATE { get; set; }
        public string strHOD_APP_DATE { get; set; }

        public string strHR_APP_DATE { get; set; }
        public int intB_LEAVE_KEY { get; set; }
        public string strB_M_R { get; set; }

        public string strWEEKND_KEY { get; set; }
        public string strEFFECTIVE_DATE { get; set; }
        public string strEMP_WEEKEND { get; set; }
        public int intPOS_TYPE { get; set; }
        public string strREF_NO { get; set; }



        public string strDeComIDn { get; set; }

        public string strSummary { get; set; }




        public string strACTION { get; set; }
    }
}