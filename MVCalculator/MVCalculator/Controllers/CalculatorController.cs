using Microsoft.AspNetCore.Mvc;
using MVCalculator.Models;
using System.IO;
using System.Text;
using Newtonsoft.Json;
using CalculatorService.Client;

namespace MVCalculator.Controllers
{
    public class CalculatorController : Controller
    { 
        // 
        // GET: /Calculator/

        public IActionResult Index()
        {
            return View();
        }

        // 
        // POST: /Calculator/Add

        public void Add()
        {
            
            // Receive and process the POST data
            string res = HttpContext.Request.Method.ToString();
            Encoding enc = System.Text.Encoding.GetEncoding("utf-8");
            StreamReader responseStream = new StreamReader(HttpContext.Request.Body, enc);
            string body = responseStream.ReadToEnd();

            Add add = JsonConvert.DeserializeObject<Add>(body);

            // Get the result
            Calculator C = new Calculator();
            int sum = C.add(add.Addends);

            // Create a Result Object
            AddRes addres = new AddRes();
            addres.Sum = sum;

            // Serialize it
            string param = JsonConvert.SerializeObject(addres);


            // Send it back using POST
            HttpContext.Response.ContentType = "application/json";
            using (var streamWriter = new StreamWriter(HttpContext.Response.Body, enc))
            {

                streamWriter.Write(param);
                streamWriter.Flush();
                streamWriter.Close();
            }

            var logger = NLog.LogManager.GetCurrentClassLogger();
            logger.Info("Add operation");

        }

        // 
        // POST: /Calculator/Sub

        public void Sub()
        {
            // Receive and process the POST data
            string res = HttpContext.Request.Method.ToString();

            Encoding enc = System.Text.Encoding.GetEncoding("utf-8");
            StreamReader responseStream = new StreamReader(HttpContext.Request.Body, enc);
            string body = responseStream.ReadToEnd();

            Sub subs = JsonConvert.DeserializeObject<Sub>(body);

            // Get the result
            Calculator C = new Calculator();
            int dif = C.sub(subs.Minuend, subs.Substraend);

            // Create a Result Object
            SubRes subres = new SubRes();
            subres.Difference = dif;

            // Serialize it
            string param = JsonConvert.SerializeObject(subres);

            // Send it back using POST
            HttpContext.Response.ContentType = "application/json";
            using (var streamWriter = new StreamWriter(HttpContext.Response.Body, enc))
            {

                streamWriter.Write(param);
                streamWriter.Flush();
                streamWriter.Close();
            }

            var logger = NLog.LogManager.GetCurrentClassLogger();
            logger.Info("Sub operation");
        }

        // 
        // POST: /Calculator/Mult

        public void Mult()
        {
            // Receive and process the POST data
            string res = HttpContext.Request.Method.ToString();

            Encoding enc = System.Text.Encoding.GetEncoding("utf-8");
            StreamReader responseStream = new StreamReader(HttpContext.Request.Body, enc);
            string body = responseStream.ReadToEnd();

            Mult mult = JsonConvert.DeserializeObject<Mult>(body);

            // Get the result
            Calculator C = new Calculator();
            int pro = C.mult(mult.Factors);

            // Create a Result Object
            MultRes multres = new MultRes();
            multres.Product = pro;

            // Serialize it
            string param = JsonConvert.SerializeObject(multres);

            // Send it back using POST
            HttpContext.Response.ContentType = "application/json";
            using (var streamWriter = new StreamWriter(HttpContext.Response.Body, enc))
            {

                streamWriter.Write(param);
                streamWriter.Flush();
                streamWriter.Close();
            }

            var logger = NLog.LogManager.GetCurrentClassLogger();
            logger.Info("Mult operation");
        }

        // 
        // POST: /Calculator/Div

        public void Div()
        {
            // Receive and process the POST data
            string res = HttpContext.Request.Method.ToString();

            Encoding enc = System.Text.Encoding.GetEncoding("utf-8");
            StreamReader responseStream = new StreamReader(HttpContext.Request.Body, enc);
            string body = responseStream.ReadToEnd();

            Div div = JsonConvert.DeserializeObject<Div>(body);

            // Get the result
            Calculator C = new Calculator();
            int quo = C.div(div.Dividend, div.Divisor);
            int rem = C.rem(div.Dividend, div.Divisor);

            // Create a Result Object
            DivRes divres = new DivRes();
            divres.Quotient = quo;
            divres.Remainder = rem;

            // Serialize it
            string param = JsonConvert.SerializeObject(divres);

            // Send it back using POST
            HttpContext.Response.ContentType = "application/json";
            using (var streamWriter = new StreamWriter(HttpContext.Response.Body, enc))
            {

                streamWriter.Write(param);
                streamWriter.Flush();
                streamWriter.Close();
            }

            var logger = NLog.LogManager.GetCurrentClassLogger();
            logger.Info("Div operation");
        }

        // 
        // POST: /Calculator/Sqrt

        public void Sqrt()
        {
            // Receive and process the POST data
            string res = HttpContext.Request.Method.ToString();

            Encoding enc = System.Text.Encoding.GetEncoding("utf-8");
            StreamReader responseStream = new StreamReader(HttpContext.Request.Body, enc);
            string body = responseStream.ReadToEnd();

            Sqrt sqrt = JsonConvert.DeserializeObject<Sqrt>(body);

            // Get the result
            Calculator C = new Calculator();
            int squ = C.sqrt(sqrt.Number);

            // Create a Result Object
            SqrtRes sqrtres = new SqrtRes();
            sqrtres.Square = squ;

            // Serialize it
            string param = JsonConvert.SerializeObject(sqrtres);

            // Send it back using POST
            HttpContext.Response.ContentType = "application/json";
            using (var streamWriter = new StreamWriter(HttpContext.Response.Body, enc))
            {

                streamWriter.Write(param);
                streamWriter.Flush();
                streamWriter.Close();
            }

            var logger = NLog.LogManager.GetCurrentClassLogger();
            logger.Info("Sqrt operation");
        }

    }
}