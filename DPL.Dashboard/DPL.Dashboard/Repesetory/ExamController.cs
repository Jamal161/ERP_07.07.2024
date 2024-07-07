using DPL.DASHBOARD.Models;
using Dutility;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DPL.DASHBOARD.Repesetory
{
    public class ExamController : Controller
    {
        //
        // GET: /Exam/
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult mPostExam()
        {

            var allLedger = "";
            return Json(allLedger, JsonRequestBehavior.AllowGet);
        }

      
     

             public ActionResult mPostExamResult()
        {

            var allLedger = "";
            return Json(allLedger, JsonRequestBehavior.AllowGet);
        }




        //     public ActionResult mGetExamResultList(ExamResult obj)
        //{

        //    var allLedger = mGetExamResult(obj);
        //    return Json(allLedger, JsonRequestBehavior.AllowGet);
        //}





             public ActionResult mGetExamResultList(ExamResult obj)
             {
                 List<ExamResult> UserList = new List<ExamResult>();
                 UserList = mGetExamResult(obj);
                 return Json(UserList, JsonRequestBehavior.AllowGet);
             }





             public ActionResult mGetExamAlldataList(ExamQuestionData obj)
             {
                 List<ExamQuestionData> UserList = new List<ExamQuestionData>();
                 UserList = mGetExamAlldata(obj);
                 return Json(UserList, JsonRequestBehavior.AllowGet);
             }



             public ActionResult mGetExamList()
             {
                 List<Exam> upcomingExams = GetExamsWithDateFilter(true);
                 List<Exam> pastExams = GetExamsWithDateFilter(false);

                 var examData = new { UpcomingExams = upcomingExams, PastExams = pastExams };

                 return Json(examData, JsonRequestBehavior.AllowGet);
             }



         [HttpPost]
             public string mPostExam(ExamQuestion obj)
    {
       
        string connectionString = Utility.SQLConnstringComSwitch("0001");

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                SqlTransaction transaction = connection.BeginTransaction();

                
                string examId = DateTime.UtcNow.ToString("yyMMddHHmmss");



         
                string userInsertQuery = @"INSERT INTO HRS_EXAM_USER(EXAMID,EMP_CARD_NO, EXAM_TITLE, EXAM_NOTICE, NATIONAL_HEAD, TEAM, ZONE, DIVISION, AREA, MARKET, ROUTE, ROLE) 
                                           VALUES (@examId,@EMP_CARD_NO, @ExamTitle, @ExamNotice, @NATIONAL_HEAD, @TEAM, @ZONE, @DIVISION, @AREA, @MARKET, @ROUTE, @ROLE);
                                           SELECT SCOPE_IDENTITY();";
                SqlCommand userInsertCommand = new SqlCommand(userInsertQuery, connection, transaction);
                userInsertCommand.Parameters.AddWithValue("@examId", examId);
                userInsertCommand.Parameters.AddWithValue("@EMP_CARD_NO", obj.EMP_CARD_NO);
                userInsertCommand.Parameters.AddWithValue("@ExamTitle", obj.ExamTitle);
                userInsertCommand.Parameters.AddWithValue("@ExamNotice", obj.ExamNotice);
                userInsertCommand.Parameters.AddWithValue("@NATIONAL_HEAD", obj.NATIONAL_HEAD);
                userInsertCommand.Parameters.AddWithValue("@TEAM", obj.TEAM); 
                userInsertCommand.Parameters.AddWithValue("@ZONE", obj.ZONE);
                userInsertCommand.Parameters.AddWithValue("@DIVISION", obj.DIVISION);
                userInsertCommand.Parameters.AddWithValue("@AREA", obj.AREA);
                userInsertCommand.Parameters.AddWithValue("@MARKET", obj.MARKET); 
                userInsertCommand.Parameters.AddWithValue("@ROUTE", obj.ROUTE);
                userInsertCommand.Parameters.AddWithValue("@ROLE", obj.ROLE);
                userInsertCommand.ExecuteNonQuery();



                string examInsertQuery = @"INSERT INTO HRS_EXAM_DETAILS (EXAMID, TOTAL_MARKS, PASS_MARKS , TIMELIMIT, EXAM_DATE,START_TIME,END_TIME) 
                                           VALUES (@examId, @totalMarks,@passmarks, @timeLimit, @examDate, @starttime ,@endtime);
                                           SELECT SCOPE_IDENTITY();";
                SqlCommand examInsertCommand = new SqlCommand(examInsertQuery, connection, transaction);
                examInsertCommand.Parameters.AddWithValue("@examId", examId);
                examInsertCommand.Parameters.AddWithValue("@totalMarks", obj.totalMarks);
                examInsertCommand.Parameters.AddWithValue("@passmarks", obj.passmarks);
                examInsertCommand.Parameters.AddWithValue("@timeLimit", obj.timeLimit);
                examInsertCommand.Parameters.AddWithValue("@examDate", obj.examDate);
                examInsertCommand.Parameters.AddWithValue("@starttime", obj.starttime);
                examInsertCommand.Parameters.AddWithValue("@endtime", obj.endtime);

                examInsertCommand.ExecuteNonQuery();
                
                foreach (var question in obj.questions)
                {
                    string questionInsertQuery = @"INSERT INTO HRS_EXAM_QUESTION (EXAMID, TITLE, MARKS, TYPE, ANSWER, OPTIONS) 
                                                  VALUES (@examId, @title, @marks, @type, @answer, @options)";
                    SqlCommand questionInsertCommand = new SqlCommand(questionInsertQuery, connection, transaction);
                    questionInsertCommand.Parameters.AddWithValue("@examId", examId);
                    questionInsertCommand.Parameters.AddWithValue("@title", question.title);
                    questionInsertCommand.Parameters.AddWithValue("@marks", question.marks);
                    questionInsertCommand.Parameters.AddWithValue("@type", question.type);
                    questionInsertCommand.Parameters.AddWithValue("@answer", question.answer);
                    string optionsString = (question.options != null) ? string.Join(",", question.options) : string.Empty;
                    questionInsertCommand.Parameters.AddWithValue("@options", optionsString);
                    questionInsertCommand.ExecuteNonQuery();
                }

                transaction.Commit();
                return ("Exam inserted successfully");
            }
            catch (SqlException ex)
            {


                return ex.Message.ToString();
            }
            finally
            {
                connection.Close();
            }
        }
    }




        

         private List<Exam> GetExamsWithDateFilter(bool upcoming)
         {
             string strSQL = null;
             string connectionString = Utility.SQLConnstringComSwitch("0001"); 

             List<Exam> examList = new List<Exam>();

             using (SqlConnection gcnMain = new SqlConnection(connectionString))
             {
                 gcnMain.Open();

                 strSQL = "SELECT * FROM HRS_EXAM_USER N, HRS_EXAM_DETAILS U, HRS_EXAM_QUESTION M " +
                         "WHERE N.examId = U.examId AND N.examId = M.examId ";

                
                 if (upcoming)
                 {
                     strSQL += "AND CAST(EXAM_DATE AS datetime) >= CAST(GETDATE() AS date)";
                 }
                 else
                 {
                     strSQL += "AND CAST(EXAM_DATE AS datetime) < CAST(GETDATE() AS date)";
                 }

                 using (SqlCommand cmd = new SqlCommand(strSQL, gcnMain))
                 {
                     using (SqlDataReader dr = cmd.ExecuteReader())
                     {
                         while (dr.Read())
                         {
                             Exam exam = new Exam();
                             PopulateExamFromDataReader(dr, exam); 
                             examList.Add(exam);
                         }
                     }
                 }
             }

             return examList;
         }

        
         private void PopulateExamFromDataReader(SqlDataReader dr, Exam exam)
         {
             
                             //exam.strexamId = dr["examId"].ToString();
                             //exam.strExamTitle = dr["ExamTitle"].ToString();
                             //exam.strExamNotice = dr["ExamNotice"].ToString();
                             //exam.strEMP_CARD_NO = dr["EMP_CARD_NO"].ToString();
                             //exam.strNATIONAL_HEAD = dr["NATIONAL_HEAD"].ToString();
                             //exam.strTEAM = dr["TEAM"].ToString();
                             //exam.strZONE = dr["ZONE"].ToString();
                             //exam.strDIVISION = dr["DIVISION"].ToString();
                             //exam.strAREA = dr["AREA"].ToString();
                             //exam.strMARKET = dr["MARKET"].ToString();
                             //exam.strROUTE = dr["ROUTE"].ToString();
                             //exam.strROLE = dr["ROLE"].ToString();
                             //exam.strtotalMarks = dr["totalMarks"].ToString();
                             //exam.strpassmarks = dr["passmarks"].ToString();
                             //exam.strtimeLimit = dr["timeLimit"].ToString();
                             //exam.strexamDate = Convert.ToDateTime(dr["EXAM_DATE"]).ToString("dd-MM-yyyy");
                             //exam.strstarttime = Convert.ToDateTime(dr["starttime"]).ToString("hh:mm tt");
                             //exam.strendtime = Convert.ToDateTime(dr["endtime"]).ToString("hh:mm tt");
                             //exam.strtitle = dr["title"].ToString();
                             //exam.strmarks = dr["marks"].ToString();
                             //exam.strtype = dr["type"].ToString();
                             //exam.stranswer = dr["answer"].ToString();
                             //exam.stroptions = dr["options"].ToString();

                             exam.strexamId = dr["examId"].ToString();
                             exam.strEXAM_TITLE = dr["EXAM_TITLE"].ToString();
                             exam.strEXAM_NOTICE = dr["EXAM_NOTICE"].ToString();
                             exam.strEMP_CARD_NO = dr["EMP_CARD_NO"].ToString();
                             exam.strNATIONAL_HEAD = dr["NATIONAL_HEAD"].ToString();
                             exam.strTEAM = dr["TEAM"].ToString();
                             exam.strZONE = dr["ZONE"].ToString();
                             exam.strDIVISION = dr["DIVISION"].ToString();
                             exam.strAREA = dr["AREA"].ToString();
                             exam.strMARKET = dr["MARKET"].ToString();
                             exam.strROUTE = dr["ROUTE"].ToString();
                             exam.strROLE = dr["ROLE"].ToString();
                             exam.strTOTAL_MARKS = dr["TOTAL_MARKS"].ToString();
                             exam.strPASS_MARKS = dr["PASS_MARKS"].ToString();
                             exam.strTIMELIMIT = dr["TIMELIMIT"].ToString();
                             exam.strEXAM_DATE = Convert.ToDateTime(dr["EXAM_DATE"]).ToString("dd-MM-yyyy");
                             exam.strSTART_TIME = Convert.ToDateTime(dr["START_TIME"]).ToString("hh:mm tt");
                             exam.strEND_TIME = Convert.ToDateTime(dr["END_TIME"]).ToString("hh:mm tt");
                             exam.strTITLE = dr["TITLE"].ToString();
                             exam.strMARKS = dr["MARKS"].ToString();
                             exam.strTYPE = dr["TYPE"].ToString();
                             exam.strANSWER = dr["ANSWER"].ToString();
                             exam.strOPTIONS = dr["OPTIONS"].ToString();
         }











        [HttpPost]
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

        //strSQL = "INSERT INTO HRS_EXAM_RESULTS (";
        //             strSQL = strSQL + " EXAMID,EMP_CARD_NO, USER_TYPE,MPO_NAME, ";
        //             strSQL = strSQL + "TOTAL_SCORE, CORRECT, ";
        //             strSQL = strSQL + " INCORRECT, RESULT , TOTAL_MARKS , EXAMTIME , SUBMITTED_TIME ";
        //             strSQL = strSQL + ") ";

         [HttpPost]
        public List<ExamResult> mGetExamResult(ExamResult obj)
        {
            string strSQL = null;
            string connectionString = Utility.SQLConnstringComSwitch("0001");
            List<ExamResult> ExamResultList = new List<ExamResult>();

            using (SqlConnection gcnMain = new SqlConnection(connectionString))
            {
                gcnMain.Open();

                strSQL = "SELECT * FROM HRS_EXAM_RESULTS WHERE EXAMID = '" + obj.strexamId + "';";

                using (SqlCommand cmd = new SqlCommand(strSQL, gcnMain))
                {
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            ExamResult ExamResults = new ExamResult();
                            ExamResults.strexamId = dr["EXAMID"].ToString();
                            ExamResults.strcardNO = dr["EMP_CARD_NO"].ToString();
                            ExamResults.struserType = dr["USER_TYPE"].ToString();
                            ExamResults.strledgerName = dr["MPO_NAME"].ToString();
                            ExamResults.strtotalScore = dr["TOTAL_SCORE"].ToString();
                            ExamResults.strcorrect = dr["CORRECT"].ToString();
                            ExamResults.strinCorrect = dr["INCORRECT"].ToString();
                            ExamResults.strresult = dr["RESULT"].ToString();
                            ExamResults.strtotalMark = dr["TOTAL_MARKS"].ToString();
                            ExamResults.strexamTime = dr["EXAMTIME"].ToString();
                            ExamResults.strsubmittedTime = dr["SUBMITTED_TIME"].ToString();
                            ExamResults.strcreatedate = Convert.ToDateTime(dr["INSERT_DATE"]).ToString(" dd-MMM-yyyy hh:mm tt");
                            ExamResultList.Add(ExamResults);
                        }
                    }
                }
            }

            if (ExamResultList.Count == 0)
            {

                ExamResultList.Add(new ExamResult());
            }

            return ExamResultList;
        }



        [HttpPost]
         public List<ExamQuestionData> mGetExamAlldata(ExamQuestionData obj)
        {
            string strSQL = null;
            string connectionString = Utility.SQLConnstringComSwitch("0001");
            List<ExamQuestionData> ExamList = new List<ExamQuestionData>();

            using (SqlConnection gcnMain = new SqlConnection(connectionString))
            {
                gcnMain.Open();


               
                strSQL = "SELECT *FROM HRS_EXAM_USER N  JOIN HRS_EXAM_DETAILS U ON N.examId = U.examId JOIN HRS_EXAM_QUESTION M ON N.examId = M.examId  WHERE N.examId = '" + obj.strexamId + "';";

                using (SqlCommand cmd = new SqlCommand(strSQL, gcnMain))
                {
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            ExamQuestionData Exams = new ExamQuestionData();



                            Exams.strexamId = dr["EXAMID"].ToString();
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

            if (ExamList.Count == 0)
            {

                ExamList.Add(new ExamQuestionData());
            }

            return ExamList;
        }





    }

}











public class Exam
{

    public string stroptions { get; set; }
    public string stranswer { get; set; }
    public string strtype { get; set; }
    public string strmarks { get; set; }
    public string strtitle { get; set; }
    public string strexamDate { get; set; }
    public string strtimeLimit { get; set; }
    public string strtotalMarks { get; set; }
    public string strexamId { get; set; }
    public int totalMarks { get; set; }

    public int timeLimit { get; set; }
    public DateTime examDate { get; set; }
    public List<Question> questions { get; set; }



    public class Question
    {
        public string title { get; set; }
        public int marks { get; set; }
        public string type { get; set; }
        public string answer { get; set; }
        public List<string> options { get; set; }
    }


    public object ExamTitle { get; set; }

    public object ExamNotice { get; set; }

    public object NATIONAL_HEAD { get; set; }

    public object ZONE { get; set; }

    public object DIVISION { get; set; }

    public object AREA { get; set; }

    public object ROUTE { get; set; }

    public object ROLE { get; set; }

    public object TEAM { get; set; }

    public object MARKET { get; set; }

    public string strExamTitle { get; set; }

    public string strExamNotice { get; set; }

    public string strNATIONAL_HEAD { get; set; }

    public string strTEAM { get; set; }

    public string strZONE { get; set; }

    public string strDIVISION { get; set; }

    public string strAREA { get; set; }

    public string strMARKET { get; set; }

    public string strROUTE { get; set; }

    public string strROLE { get; set; }

    public string strpassmarks { get; set; }

    public string passmarks { get; set; }

    public object starttime { get; set; }

    public object endtime { get; set; }

    public string strstarttime { get; set; }

    public string strendtime { get; set; }

    public object EMP_CARD_NO { get; set; }

    public string strEMP_CARD_NO { get; set; }

    public string strEXAM_TITLE { get; set; }

    public string strEXAM_NOTICE { get; set; }

    public string strTOTAL_MARKS { get; set; }

    public string strPASS_MARKS { get; set; }

    public string strTIMELIMIT { get; set; }

    public string strEXAM_DATE { get; set; }

    public string strSTART_TIME { get; set; }

    public string strEND_TIME { get; set; }

    public string strTITLE { get; set; }

    public string strMARKS { get; set; }

    public string strTYPE { get; set; }

    public string strANSWER { get; set; }

    public string strOPTIONS { get; set; }
}