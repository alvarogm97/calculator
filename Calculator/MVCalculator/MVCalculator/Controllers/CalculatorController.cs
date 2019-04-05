using Microsoft.AspNetCore.Mvc;
using MVCalculator.Models;
using System.IO;
using System.Text;
using Newtonsoft.Json;
using CalculatorService.Client;
using System;

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
            try
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

            } catch (JsonReaderException je)
            {
                // Create a Result Object
                CalculatorService.Client.Exception ex = new CalculatorService.Client.Exception();
                ex.ErrorCode = "InternalError";
                ex.ErrorStatus = 500;
                ex.ErrorMessage = je.Message;

                // Serialize it
                string exec = JsonConvert.SerializeObject(ex);

                // Send it back using POST
                HttpContext.Response.ContentType = "application/json";
                Encoding enc = System.Text.Encoding.GetEncoding("utf-8");
                using (var streamWriter = new StreamWriter(HttpContext.Response.Body, enc))
                {

                    streamWriter.Write(exec);
                    streamWriter.Flush();
                    streamWriter.Close();
                }

                var logger = NLog.LogManager.GetCurrentClassLogger();
                logger.Info("Unexpected character encountered while parsing value.");
            }
            catch (NullReferenceException npe) {

                // Create a Result Object
                CalculatorService.Client.Exception ex = new CalculatorService.Client.Exception();
                ex.ErrorCode = "InternalError";
                ex.ErrorStatus = 500;
                ex.ErrorMessage = npe.Message;

                // Serialize it
                string exec = JsonConvert.SerializeObject(ex);

                // Send it back using POST
                HttpContext.Response.ContentType = "application/json";
                Encoding enc = System.Text.Encoding.GetEncoding("utf-8");
                using (var streamWriter = new StreamWriter(HttpContext.Response.Body, enc))
                {

                    streamWriter.Write(exec);
                    streamWriter.Flush();
                    streamWriter.Close();
                }

                var logger = NLog.LogManager.GetCurrentClassLogger();
                logger.Info("Object reference not set to an instance of an object.");
                
            }
            catch (System.Exception e)
            {
                // Create a Result Object
                CalculatorService.Client.Exception ex = new CalculatorService.Client.Exception();
                ex.ErrorCode = "InternalError";
                ex.ErrorStatus = 500;
                ex.ErrorMessage = e.Message;

                // Serialize it
                string exec = JsonConvert.SerializeObject(ex);

                // Send it back using POST
                HttpContext.Response.ContentType = "application/json";
                Encoding enc = System.Text.Encoding.GetEncoding("utf-8");
                using (var streamWriter = new StreamWriter(HttpContext.Response.Body, enc))
                {

                    streamWriter.Write(exec);
                    streamWriter.Flush();
                    streamWriter.Close();
                }

                var logger = NLog.LogManager.GetCurrentClassLogger();
                logger.Info("Object reference not set to an instance of an object.");
            }

        }

        // 
        // POST: /Calculator/Sub

        public void Sub()
        {
            try
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
            catch (System.Exception e)
            {
                // Create a Result Object
                CalculatorService.Client.Exception ex = new CalculatorService.Client.Exception();
                ex.ErrorCode = "InternalError";
                ex.ErrorStatus = 500;
                ex.ErrorMessage = e.Message;

                // Serialize it
                string exec = JsonConvert.SerializeObject(ex);

                // Send it back using POST
                HttpContext.Response.ContentType = "application/json";
                Encoding enc = System.Text.Encoding.GetEncoding("utf-8");
                using (var streamWriter = new StreamWriter(HttpContext.Response.Body, enc))
                {

                    streamWriter.Write(exec);
                    streamWriter.Flush();
                    streamWriter.Close();
                }

                var logger = NLog.LogManager.GetCurrentClassLogger();
                logger.Info("Object reference not set to an instance of an object.");
            }
        }

        // 
        // POST: /Calculator/Mult

        public void Mult()
        {
            try
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
            catch (System.Exception e)
            {
                // Create a Result Object
                CalculatorService.Client.Exception ex = new CalculatorService.Client.Exception();
                ex.ErrorCode = "InternalError";
                ex.ErrorStatus = 500;
                ex.ErrorMessage = e.Message;

                // Serialize it
                string exec = JsonConvert.SerializeObject(ex);

                // Send it back using POST
                HttpContext.Response.ContentType = "application/json";
                Encoding enc = System.Text.Encoding.GetEncoding("utf-8");
                using (var streamWriter = new StreamWriter(HttpContext.Response.Body, enc))
                {

                    streamWriter.Write(exec);
                    streamWriter.Flush();
                    streamWriter.Close();
                }

                var logger = NLog.LogManager.GetCurrentClassLogger();
                logger.Info("Object reference not set to an instance of an object.");
            }
        }

        // 
        // POST: /Calculator/Div

        public void Div()
        {
            try
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
            catch (System.Exception e)
            {
                // Create a Result Object
                CalculatorService.Client.Exception ex = new CalculatorService.Client.Exception();
                ex.ErrorCode = "InternalError";
                ex.ErrorStatus = 500;
                ex.ErrorMessage = e.Message;

                // Serialize it
                string exec = JsonConvert.SerializeObject(ex);

                // Send it back using POST
                HttpContext.Response.ContentType = "application/json";
                Encoding enc = System.Text.Encoding.GetEncoding("utf-8");
                using (var streamWriter = new StreamWriter(HttpContext.Response.Body, enc))
                {

                    streamWriter.Write(exec);
                    streamWriter.Flush();
                    streamWriter.Close();
                }

                var logger = NLog.LogManager.GetCurrentClassLogger();
                logger.Info("Object reference not set to an instance of an object.");
            }
        }

        // 
        // POST: /Calculator/Sqrt

        public void Sqrt()
        {
            try
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

            catch (System.Exception e)
            {
                // Create a Result Object
                CalculatorService.Client.Exception ex = new CalculatorService.Client.Exception();
                ex.ErrorCode = "InternalError";
                ex.ErrorStatus = 500;
                ex.ErrorMessage = e.Message;

                // Serialize it
                string exec = JsonConvert.SerializeObject(ex);

                // Send it back using POST
                HttpContext.Response.ContentType = "application/json";
                Encoding enc = System.Text.Encoding.GetEncoding("utf-8");
                using (var streamWriter = new StreamWriter(HttpContext.Response.Body, enc))
                {

                    streamWriter.Write(exec);
                    streamWriter.Flush();
                    streamWriter.Close();
                }

                var logger = NLog.LogManager.GetCurrentClassLogger();
                logger.Info("Object reference not set to an instance of an object.");
            }
        }

    }
}