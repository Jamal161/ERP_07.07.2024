using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DPL.DASHBOARD.Models
{
    public class ExamQuestion
    {
        public object EMP_CARD_NO { get; set; }

        public object ExamTitle { get; set; }

        public object ExamNotice { get; set; }

        public object NATIONAL_HEAD { get; set; }

        public object TEAM { get; set; }

        public object ZONE { get; set; }

        public object DIVISION { get; set; }

        public object AREA { get; set; }

        public object MARKET { get; set; }

        public object ROUTE { get; set; }

        public object ROLE { get; set; }

        public object totalMarks { get; set; }

        public object passmarks { get; set; }

        public object timeLimit { get; set; }

        public object examDate { get; set; }

        public object starttime { get; set; }

        public object endtime { get; set; }

        public List<Question> questions { get; set; }



        public class Question
        {
            public string title { get; set; }
            public int marks { get; set; }
            public string type { get; set; }
            public string answer { get; set; }
            public List<string> options { get; set; }
        }
      
    }
}