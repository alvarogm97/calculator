using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using MVCalculator.Models;
using System.IO;
using System.Text;
using Newtonsoft.Json;
using CalculatorService.Client;

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
            // Receive and process the POST data
            string res = HttpContext.Request.Method.ToString();

            Encoding enc = System.Text.Encoding.GetEncoding("utf-8");
            StreamReader responseStream = new StreamReader(HttpContext.Request.Body, enc);
            string body = responseStream.ReadToEnd();

            Query qu = JsonConvert.DeserializeObject<Query>(body);

            // Get the result
            Journal J = new Journal();
            List<string> opers = J.query(qu.Id);

            // Create a Result Object
            QueryRes qures = new QueryRes();
            qures.Operations = new List<string> { };
            qures.Operations = opers;

            // Serialize it
            string param = JsonConvert.SerializeObject(qures);

            // Send it back using POST
            HttpContext.Response.ContentType = "application/json";
            using (var streamWriter = new StreamWriter(HttpContext.Response.Body, enc))
            {

                streamWriter.Write(param);
                streamWriter.Flush();
                streamWriter.Close();
            }

            var logger = NLog.LogManager.GetCurrentClassLogger();
            logger.Info($"Query operation (Id {qu.Id})");
        }

        // 
        // POST: /Journal/Store

        public void Store()
        {
            // Receive and process the POST data
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

            // Store it in a global variable
            J.store(int.Parse(data["id"]), JsonConvert.SerializeObject(o));
            
        }

    }
}