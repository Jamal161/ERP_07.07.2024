using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DPL.DASHBOARD.Models
{
    public class AttendentCongfig
    {

        public string strATTEN_SHIFT { get; set; }

        public string strATTEN_DATEIN { get; set; }

        public string strATTEN_TIMEIN { get; set; }

        public string strUSER_NAME { get; set; }

        public string strROLE { get; set; }

        public string strEMP_CARD_NO { get; set; }

        public string strTC { get; set; }

        public string strLATITUDE { get; set; }

        public string strLONGITUDE { get; set; }

        public string strADDRESS { get; set; }

        public int intDISTANCE { get; set; }
        public int intPresent { get; set; }

        public int intAbsent { get; set; }

        public int intLeave { get; set; }

        public int intTotal { get; set; }
        public double dblPercentage { get; set; }


        public int intTOTAL_WORKING_HOUR { get; set; }

        public string strACTION { get; set; }

        public string strEMP_IMAGE { get; set; }

        public string strEMP_JPEG_DOC { get; set; }

        public string strSTAY_HOUR { get; set; }

        public string strATTEN_TIMEOUT { get; set; }

        public string strATTEN_COMMENTS { get; set; }

        public string strATTEN_STATUS { get; set; }

        public string strSHIFT_START { get; set; }


        public string strEMP_B_NAME { get; set; }

        public int intATTEN_SERIAL { get; set; }

        public int intCountt { get; set; }

        public string strATTEN_DATEINFROM { get; set; }
        public string strATTEN_DATEINTO { get; set; }




       
        public string TYPE { get; set; }

        public string TEAM_NAME { get; set; }

        public string strZONE_NAME { get; set; }

        public string TEAM_CODE { get; set; }

        public int PRESENT { get; set; }

        public string ZONE { get; set; }

        public int ABSENT { get; set; }

        public int CL { get; set; }

        public int ML { get; set; }

        public string TOTALMPO { get; set; }

        public string TOTALAREA { get; set; }

        public string TOTALDIV { get; set; }

        public string TOTALZH { get; set; }

        public string TOTALHEAD { get; set; }
    }
}