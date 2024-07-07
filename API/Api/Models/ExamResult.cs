using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Api.Models
{
    public class ExamResult
    {
        public string strcardNO { get; set; }



        public string strledgerName { get; set; }

        public int inttotalScore { get; set; }



        public int intincorrect { get; set; }

        public string strresult { get; set; }

        public int inttotalMark { get; set; }

        public string strexamTime { get; set; }



        public string struserType { get; set; }

        public int intcorrect { get; set; }

        public string strsubmittedTime { get; set; }

        public string strexamId { get; set; }
    }
}