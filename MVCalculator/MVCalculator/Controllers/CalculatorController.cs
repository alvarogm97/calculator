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
            
            string res = HttpContext.Request.Method.ToString();

            Encoding enc = System.Text.Encoding.GetEncoding("utf-8");
            StreamReader responseStream = new StreamReader(HttpContext.Request.Body, enc);
            string body = responseStream.ReadToEnd();

            Add add = JsonConvert.DeserializeObject<Add>(body);

            Calculator C = new Calculator();
            int sum = C.add(add.Addends);

            AddRes addres = new AddRes();
            addres.Sum = sum;

            string param = JsonConvert.SerializeObject(addres);

            HttpContext.Response.ContentType = "application/json";
            using (var streamWriter = new StreamWriter(HttpContext.Response.Body, enc))
            {

                streamWriter.Write(param);
                streamWriter.Flush();
                streamWriter.Close();
            }

        }

        // 
        // POST: /Calculator/Sub

        public void Sub()
        {
            string res = HttpContext.Request.Method.ToString();

            Encoding enc = System.Text.Encoding.GetEncoding("utf-8");
            StreamReader responseStream = new StreamReader(HttpContext.Request.Body, enc);
            string body = responseStream.ReadToEnd();

            Sub subs = JsonConvert.DeserializeObject<Sub>(body);

            Calculator C = new Calculator();
            int dif = C.sub(subs.Minuend, subs.Substraend);

            SubRes subres = new SubRes();
            subres.Difference = dif;
            string param = JsonConvert.SerializeObject(subres);

            HttpContext.Response.ContentType = "application/json";
            using (var streamWriter = new StreamWriter(HttpContext.Response.Body, enc))
            {

                streamWriter.Write(param);
                streamWriter.Flush();
                streamWriter.Close();
            }
        }

        // 
        // POST: /Calculator/Mult

        public void Mult()
        {
            string res = HttpContext.Request.Method.ToString();

            Encoding enc = System.Text.Encoding.GetEncoding("utf-8");
            StreamReader responseStream = new StreamReader(HttpContext.Request.Body, enc);
            string body = responseStream.ReadToEnd();

            Mult mult = JsonConvert.DeserializeObject<Mult>(body);

            Calculator C = new Calculator();
            int pro = C.mult(mult.Factors);

            MultRes multres = new MultRes();
            multres.Product = pro;
            string param = JsonConvert.SerializeObject(multres);

            HttpContext.Response.ContentType = "application/json";
            using (var streamWriter = new StreamWriter(HttpContext.Response.Body, enc))
            {

                streamWriter.Write(param);
                streamWriter.Flush();
                streamWriter.Close();
            }
        }

        // 
        // POST: /Calculator/Div

        public void Div()
        {
            string res = HttpContext.Request.Method.ToString();

            Encoding enc = System.Text.Encoding.GetEncoding("utf-8");
            StreamReader responseStream = new StreamReader(HttpContext.Request.Body, enc);
            string body = responseStream.ReadToEnd();

            Div div = JsonConvert.DeserializeObject<Div>(body);

            Calculator C = new Calculator();
            int quo = C.div(div.Dividend, div.Divisor);
            int rem = C.rem(div.Dividend, div.Divisor);

            DivRes divres = new DivRes();
            divres.Quotient = quo;
            divres.Remainder = rem;
            string param = JsonConvert.SerializeObject(divres);

            HttpContext.Response.ContentType = "application/json";
            using (var streamWriter = new StreamWriter(HttpContext.Response.Body, enc))
            {

                streamWriter.Write(param);
                streamWriter.Flush();
                streamWriter.Close();
            }
        }

        // 
        // POST: /Calculator/Sqrt

        public void Sqrt()
        {
            string res = HttpContext.Request.Method.ToString();

            Encoding enc = System.Text.Encoding.GetEncoding("utf-8");
            StreamReader responseStream = new StreamReader(HttpContext.Request.Body, enc);
            string body = responseStream.ReadToEnd();

            Sqrt sqrt = JsonConvert.DeserializeObject<Sqrt>(body);

            Calculator C = new Calculator();
            int squ = C.sqrt(sqrt.Number);

            SqrtRes sqrtres = new SqrtRes();
            sqrtres.Square = squ;
            string param = JsonConvert.SerializeObject(sqrtres);

            HttpContext.Response.ContentType = "application/json";
            using (var streamWriter = new StreamWriter(HttpContext.Response.Body, enc))
            {

                streamWriter.Write(param);
                streamWriter.Flush();
                streamWriter.Close();
            }
        }



    }
}