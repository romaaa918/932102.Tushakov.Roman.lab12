using Lab12.Models;
using Lab12.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Lab12.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private CalculationService _calculationService;

        public HomeController(ILogger<HomeController> logger, CalculationService calculationService)
        {
            _logger = logger;
            _calculationService = calculationService;
        }

        private string Calc()
        {
            try
            {
                int firstValue = int.Parse(Request.Form["firstValue"]);
                int secondValue = int.Parse(Request.Form["secondValue"]);
                string operation = Request.Form["operation"];
                return _calculationService.calc(firstValue, secondValue, operation);
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult SingleAction()
        {
            if (Request.Method == "GET")
                return View();
            else
            {
                ViewData["Result"] = Calc();
                return View("Result");
            }
        }

        [HttpGet, ActionName("SeparateActions")]
        public IActionResult SeparateActionsGet()
        {
            return View();
        }

        [HttpPost, ActionName("SeparateActions")]
        public IActionResult SeparateActionsPost()
        {
            ViewData["Result"] = Calc();
            return View("Result");
        }

        [HttpGet]
        public IActionResult Parameters()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Parameters(CalcModel calcModel)
        {
            ViewData["Result"] = Calc();
            return View("Result");
        }

        [HttpGet]
        public IActionResult SeparateModel()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SeparateModel(int firstValue, int secondValue, string operation)
        {
            ViewData["Result"] = Calc();
            return View("Result");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}