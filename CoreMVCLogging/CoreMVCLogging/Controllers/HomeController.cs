using CoreMVCLogging.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using CoreMVCLogging.Models;

namespace CoreMVCLogging.Controllers
{
    public class HomeController : Controller
    {
        //Step 08:Start
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        //Step 08:End

        public IActionResult Index()
        {
            _logger.LogInformation("Accessing Home/Index at {Time}", DateTime.UtcNow);

            try
            {
                this._logger.LogInformation("Home Controller Called in CoreMVCLogging Project");
                int x = 0;
                // Throwing the exception, by dividing zero
                int y = 5 / x;
               
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "An exception occurred in Home/Index at {time}", DateTime.UtcNow);
            }

            return View();
        }

        public IActionResult Privacy()
        {
            _logger.LogInformation("Accessing Home/Privacy at {Time}", DateTime.UtcNow);
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
