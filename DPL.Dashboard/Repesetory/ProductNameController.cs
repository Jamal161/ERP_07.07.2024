using DPL.DASHBOARD.Models;
using Dutility;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DPL.DASHBOARD.Repesetory
{
    public class ProductNameController : Controller
    {
        //
        // GET: /ProductName/
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult mGetProductNameList(ProductName obj)
        {

            var allLedger = mGetProductName(obj);
            var jsonResult = Json(allLedger, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;

        }



        public List<ProductName> mGetProductName(ProductName obj)
        {
            string strSQL = null;
            string connectionString = Utility.SQLConnstringComSwitch("0001");
            List<ProductName> ProductNameList = new List<ProductName>();

            using (SqlConnection gcnMain = new SqlConnection(connectionString))
            {
                gcnMain.Open();

                strSQL = "SELECT *FROM SMART0005.dbo.INV_STOCKITEM AS s INNER JOIN SMART0005.dbo.INV_SALES_ITEM_PRICE_VIEW AS p ON s.STOCKITEM_NAME = p.STOCKITEM_NAME WHERE s.STOCKITEM_PRIMARY_GROUP = 'Finished Goods';";

                using (SqlCommand cmd = new SqlCommand(strSQL, gcnMain))
                {
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            ProductName Product = new ProductName();
                            Product.strSTOCKITEM_NAME = dr["STOCKITEM_NAME"].ToString();
                            Product.strSTOCKITEM_ALIAS = dr["STOCKITEM_ALIAS"].ToString();
                            Product.strSTOCKGROUP_NAME = dr["STOCKGROUP_NAME"].ToString();
                            Product.strSTOCKITEM_PRIMARY_GROUP = dr["STOCKITEM_PRIMARY_GROUP"].ToString();
                            Product.strSTOCKCATEGORY_NAME = dr["STOCKCATEGORY_NAME"].ToString();
                            Product.strSALES_PRICE_AMOUNT = dr["SALES_PRICE_AMOUNT"].ToString();
                            

                            ProductNameList.Add(Product);
                        }
                    }
                }
            }

            if (ProductNameList.Count == 0)
            {

                ProductNameList.Add(new ProductName());
            }

            return ProductNameList;
        }





	}
}