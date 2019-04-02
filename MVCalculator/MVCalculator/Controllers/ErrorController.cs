using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.IO;
using System.Text;

namespace MVCalculator.Controllers
{
    public class ErrorController : Controller
    {
        public void PageNotFound()
        {  
            // Create a Result Object
            CalculatorService.Client.Exception ex = new CalculatorService.Client.Exception();
            ex.ErrorCode = "InternalError";
            ex.ErrorStatus = 404;
            ex.ErrorMessage = "Unable to process request: " + Request.Path.ToString();

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
