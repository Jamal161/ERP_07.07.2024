using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Api.Controllers
{
     [RoutePrefix("api/ApprovedSalesOrder")]
    public class ApprovedSalesOrderController : ApiController
    {
         OTP.SWAPIClient objW = new OTP.SWAPIClient();
        // GET api/approvedsalesorder
      
         
         [HttpPost]
         [Route("Approved") ]
         public IHttpActionResult Approved(List<summary> param)
         {
             string strSummary = "",strBranchId="",i="";
             foreach (var item in param)
             {
                 strBranchId = item.branchid;
                 strSummary = strSummary + item.orderId + "|" + item.approveBy + "|" + item.approveDate + "|" + item.branchid + "~";
                 ////strSummary = "6839631438|x|01-01-2023|0001~";
             }
             if (strBranchId == "0003")
             {
                 i = objW.UpdateAPISalesOrder("0005", strSummary);
             }
             else
             {
                  i = objW.UpdateAPISalesOrder("0005", strSummary);
             }
             return Json(i);
         }

         [HttpPost]
         [Route("ApprovedHerbal")]
         public IHttpActionResult ApprovedHerbal(List<summary> param)
         {
             string strSummary = "";
             foreach (var item in param)
             {
                 strSummary = strSummary + item.orderId + "|" + item.approveBy + "|" + item.approveDate + "|" + item.branchid + "~";
                 ////strSummary = "6839631438|x|01-01-2023|0001~";
             }
             string i = objW.UpdateAPISalesOrderHerbal("0005", strSummary);
             return Json(i);
         }



         public class summary
         {
             public string approveBy { get; set; }
             public string approveDate { get; set; }
             public string orderId { get; set; }
             public string branchid { get; set; }
             
         }


    }
}
