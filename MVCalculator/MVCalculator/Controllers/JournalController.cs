using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;
using MVCalculator.Models;
using System.Net;
using System.IO;
using System.Text;
using Newtonsoft.Json;
using CalculatorService.Client;
using System.Collections.Specialized;

namespace MVCalculator.Controllers
{
    public class JournalController : Controller
    { 
        // 
        // GET: /Journal/

        public IActionResult Index()
        {
            return Redirect("Journal/Query");
        }

        // 
        // POST: /Journal/Query

        public void Query()
        {
            
            string res = HttpContext.Request.Method.ToString();

            Encoding enc = System.Text.Encoding.GetEncoding("utf-8");
            StreamReader responseStream = new StreamReader(HttpContext.Request.Body, enc);
            string body = responseStream.ReadToEnd();

            Query qu = JsonConvert.DeserializeObject<Query>(body);

            Journal J = new Journal();
            List<string> opers = J.query(qu.Id);

            QueryRes qures = new QueryRes();
            qures.Operations = new List<string> { };
            qures.Operations = opers;

            string param = JsonConvert.SerializeObject(qures);

            HttpContext.Response.ContentType = "application/json";
            using (var streamWriter = new StreamWriter(HttpContext.Response.Body, enc))
            {

                streamWriter.Write(param);
                streamWriter.Flush();
                streamWriter.Close();
            }

        }

        // 
        // POST: /Journal/Store

        public void Store()
        {

            string res = HttpContext.Request.Method.ToString();

            Encoding enc = System.Text.Encoding.GetEncoding("utf-8");
            StreamReader responseStream = new StreamReader(HttpContext.Request.Body, enc);
            string body = responseStream.ReadToEnd();

            Dictionary<string, string> data = JsonConvert.DeserializeObject<Dictionary<string, string>>(body);

            Oper o = new Oper();

            o.Operation = data["operation"];
            o.Calculation = data["calculation"];
            o.Date = data["date"];


            Journal J = new Journal();
            J.store(int.Parse(data["id"]), JsonConvert.SerializeObject(o));
            
        }


    }
}