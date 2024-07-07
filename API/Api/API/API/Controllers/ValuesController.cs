using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace API.Controllers
{
    public class ValuesController : ApiController
    {
        // GET api/values
      
        // POST api/values
        public string  pagesize(List<string> Header)
        {
            string response = "";
            int inti = 1;
            try
            {
                response = "Total List of Line : " + Header.Count().ToString();
                foreach (string objHeder in Header)
                {
                    response = response + " " + "Header Line:" + inti + " " + objHeder.ToString().Length;
                    inti += 1;
                }
                return (response);
            }
            catch (Exception ex)
            {
                return "json format error";
            }
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
