using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DPL.DASHBOARD.Models
{
    public class ShiftCongfig
    {
        public int intShiftCongfigID { get; set; }
        public string strShiftName { get; set; }
        public string strShiftNameBangla { get; set; }
        public string strShiftCongfigDate { get; set; }
        public string strShiftCongfigStartTime { get; set; }
        public string strShiftCongfigEndTime { get; set; }
        public string strOtStatus { get; set; }

        public string strSQL { get; set; }

 
        public string strEMPLOYEE_SHIFT_NAME { get; set; }

        public string strEMPLOYEE_SHIFT_NAME_BANGLA { get; set; }

        public string strSHIFT_CONFIG_START_TIME { get; set;  }


        public string strSHIFT_CONFIG_END_TIME { get; set; }

        public int intOT_STATUS { get; set; }




        public string strOT_STATUS { get; set; }
    }
}