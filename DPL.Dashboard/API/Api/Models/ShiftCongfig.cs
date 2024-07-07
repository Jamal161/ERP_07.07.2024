using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Api.Models
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
    }
}