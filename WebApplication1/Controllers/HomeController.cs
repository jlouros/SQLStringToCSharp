using System;
using System.Web.Mvc;
using WebApp.Models;
using WebApp.Helpers;

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
            string parsedString = !string.IsNullOrWhiteSpace(userInput) ? CSharpEscaper.FromSQL(userInput, attachResultVar) : "";

            ViewData["attachResultVar"] = attachResultVar;

            return View(new EscapeStringResponse
            {
                Input = userInput,
                Result = parsedString
            });
        }

    }
}