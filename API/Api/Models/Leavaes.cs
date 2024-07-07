using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Api.Models
{
    public class Leavaes
    {


       //public string strDeComID {get; set;}
       //public string strSummary { get; set; }


        public string strEMP_CARD_NO {get; set;}
        public string strLEAVE_ID  {get; set;}

        public string strFROM_DATE {get; set;}
        public string strTO_DATE { get; set; }


        public int intNO_OF_DAYS { get; set; }


        public string strCOMMENTS { get; set;  }
    }
}