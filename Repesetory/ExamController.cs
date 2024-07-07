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




             public ActionResult mGetExamResultList(ExamResult obj)
        {

            var allLedger = mGetExamResult(obj);
            return Json(allLedger, JsonRequestBehavior.AllowGet);
        }





             public ActionResult mGetExamList()
             {
                 List<Exam> upcomingExams = GetExamsWithDateFilter(true);
                 List<Exam> pastExams = GetExamsWithDateFilter(false);

                 var examData = new { UpcomingExams = upcomingExams, PastExams = pastExams };

                 return Json(examData, JsonRequestBehavior.AllowGet);
             }



         [HttpPost]
        public string mPostExam(Exam obj)
    {
       
        string connectionString = Utility.SQLConnstringComSwitch("0001");

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                SqlTransaction transaction = connection.BeginTransaction();

                
                string examId = DateTime.UtcNow.ToString("yyMMddHHmmss");

               
                string userInsertQuery = @"INSERT INTO HRS_EXAM_USER_NEW(examId, ExamTitle, ExamNotice, NATIONAL_HEAD, TEAM, ZONE, DIVISION, AREA, MARKET, ROUTE, ROLE) 
                                           VALUES (@examId, @ExamTitle, @ExamNotice, @NATIONAL_HEAD, @TEAM, @ZONE, @DIVISION, @AREA, @MARKET, @ROUTE, @ROLE);
                                           SELECT SCOPE_IDENTITY();";
                SqlCommand userInsertCommand = new SqlCommand(userInsertQuery, connection, transaction);
                userInsertCommand.Parameters.AddWithValue("@examId", examId);
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



                string examInsertQuery = @"INSERT INTO HRS_EXAM_NEW (examId, totalMarks, passmarks , timeLimit, examDate,starttime,endtime) 
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
                    string questionInsertQuery = @"INSERT INTO HRS_EXAM_QUESTION (examId, title, marks, type, answer, options) 
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

                 strSQL = "SELECT * FROM HRS_EXAM_USER_NEW N, HRS_EXAM_NEW U, HRS_EXAM_QUESTION M " +
                         "WHERE N.examId = U.examId AND N.examId = M.examId ";

                
                 if (upcoming)
                 {
                     strSQL += "AND CAST(examDate AS datetime) >= CAST(GETDATE() AS date)";
                 }
                 else
                 {
                     strSQL += "AND CAST(examDate AS datetime) < CAST(GETDATE() AS date)";
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
             
                             exam.strexamId = dr["examId"].ToString();
                             exam.strExamTitle = dr["ExamTitle"].ToString();
                             exam.strExamNotice = dr["ExamNotice"].ToString();
                             exam.strNATIONAL_HEAD = dr["NATIONAL_HEAD"].ToString();
                             exam.strTEAM = dr["TEAM"].ToString();
                             exam.strZONE = dr["ZONE"].ToString();
                             exam.strDIVISION = dr["DIVISION"].ToString();
                             exam.strAREA = dr["AREA"].ToString();
                             exam.strMARKET = dr["MARKET"].ToString();
                             exam.strROUTE = dr["ROUTE"].ToString();
                             exam.strROLE = dr["ROLE"].ToString();
                             exam.strtotalMarks = dr["totalMarks"].ToString();
                             exam.strpassmarks = dr["passmarks"].ToString();
                             exam.strtimeLimit = dr["timeLimit"].ToString();
                             exam.strexamDate = Convert.ToDateTime(dr["examDate"]).ToString("dd-MM-yyyy");
                             exam.strstarttime = Convert.ToDateTime(dr["starttime"]).ToString("hh:mm tt");
                             exam.strendtime = Convert.ToDateTime(dr["endtime"]).ToString("hh:mm tt");
                             exam.strtitle = dr["title"].ToString();
                             exam.strmarks = dr["marks"].ToString();
                             exam.strtype = dr["type"].ToString();
                             exam.stranswer = dr["answer"].ToString();
                             exam.stroptions = dr["options"].ToString();
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




        public List<ExamResult> mGetExamResult(ExamResult obj)
        {
            string strSQL = null;
            string connectionString = Utility.SQLConnstringComSwitch("0001");
            List<ExamResult> ExamResultList = new List<ExamResult>();

            using (SqlConnection gcnMain = new SqlConnection(connectionString))
            {
                gcnMain.Open();

                strSQL = "select * from HRS_EXAM_RESULTS";

                using (SqlCommand cmd = new SqlCommand(strSQL, gcnMain))
                {
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            ExamResult ExamResults = new ExamResult();
                            ExamResults.strexamId = dr["examId"].ToString();
                            ExamResults.strcardNO = dr["cardNO"].ToString();
                            ExamResults.struserType = dr["userType"].ToString();
                            ExamResults.strledgerName = dr["ledgerName"].ToString();
                            ExamResults.strtotalScore = dr["totalScore"].ToString();
                            ExamResults.strcorrect = dr["correct"].ToString();
                            ExamResults.strinCorrect = dr["inCorrect"].ToString();
                            ExamResults.strresult = dr["result"].ToString();
                            ExamResults.strtotalMark = dr["totalMark"].ToString();
                            ExamResults.strexamTime = dr["examTime"].ToString();
                            ExamResults.strsubmittedTime = dr["submittedTime"].ToString();
                            ExamResults.strcreatedate = Convert.ToDateTime(dr["createdate"]).ToString(" dd-MMM-yyyy hh:mm tt");
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
}