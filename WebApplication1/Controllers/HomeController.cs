using System;
using System.Web.Mvc;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class HomeController : Controller
    {

        public ActionResult Index()
        {
            return View(new EscapeStringResponse
            {
                Input = "",
                Result = ""
            });
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Index(string userInput = "", bool attachResultVar = false)
        {
            string parsedString = !string.IsNullOrWhiteSpace(userInput) ? EscapeSQLStringToCSharpString(userInput, attachResultVar) : "";

            ViewData["attachResultVar"] = attachResultVar;

            return View(new EscapeStringResponse
            {
                Input = userInput,
                Result = parsedString
            });
        }

        private string EscapeSQLStringToCSharpString(string userInput, bool attachResultVar)
        {
            string result = userInput;

            result = result.Replace("'+@", "\" + ");
            result = result.Replace("+@", " + ");
            result = result.Replace("+'", " + \"");

            result = result.Replace("\"", "\\\"");

            result = result.Replace(Environment.NewLine, "\\r\"+\n\"\\n");


            //replace the first and the last ' chars
            if (result.IndexOf('\'', 0, 1) > -1)
            {
                result = result.Remove(0, 1);
                result = "\"" + result;
                if (attachResultVar)
                    result = "result = " + result;
            }
            if (result.IndexOf('\'', result.Length - 1, 1) > -1)
            {
                result = result.Remove(result.Length - 1, 1);
                result += "\"";
                if(attachResultVar)
                    result += ";";
            }

            return result;
        }

    }
}