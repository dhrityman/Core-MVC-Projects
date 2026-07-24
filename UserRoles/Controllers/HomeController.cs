using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserRoles.Models;

namespace UserRoles.Controllers
{
    /*
     * The HomeController class is a controller in an ASP.NET Core MVC application that handles requests related to the home page and other views. 
     * It provides actions for rendering the Index, Privacy, Admin, and User views, as well as handling errors.
     * 
     * The controller uses dependency injection to obtain an instance of ILogger<HomeController> for logging purposes. 
     * It also utilizes the [Authorize] attribute to restrict access to certain actions based on user roles.
     */
    public class HomeController : Controller
    {
        /*
         * The _logger field is a private readonly instance of ILogger<HomeController> used for logging information, warnings, and errors within the HomeController class.
         */
        private readonly ILogger<HomeController> _logger;

        /*
         * The constructor for the HomeController class takes an ILogger<HomeController> parameter, which is injected by the ASP.NET Core dependency injection system. 
         * It initializes the _logger field with the provided logger instance.
         * 
         * @param logger An instance of ILogger<HomeController> used for logging within the controller.
         */
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        /*          * The Index action method handles GET requests to the home page of the application. 
         * It returns the default view associated with the Index action.
         * 
         * @return An IActionResult representing the result of the action, which is typically a ViewResult that renders the Index view.
         */
        public IActionResult Index()
        {
            return View();
        }
        /*
         * The Privacy action method handles GET requests to the privacy page of the application. 
         * It is decorated with the [Authorize] attribute, which restricts access to authenticated users only.
         * 
         * @return An IActionResult representing the result of the action, which is typically a ViewResult that renders the Privacy view.
         */

        [Authorize]
        public IActionResult Privacy()
        {
            return View();
        }

        /*
         * The Admin action method handles GET requests to the admin page of the application. 
         * It is decorated with the [Authorize(Roles = "Admin")] attribute, which restricts access to users who are members of the "Admin" role.
         * 
         * @return An IActionResult representing the result of the action, which is typically a ViewResult that renders the Admin view.
         */
        [Authorize(Roles = "Admin")]
        public IActionResult Admin()
        {
            return View();
        }

        /*
         * The User action method handles GET requests to the user page of the application. 
         * It is decorated with the [Authorize(Roles = "User")] attribute, which restricts access to users who are members of the "User" role.
         * 
         * @return An IActionResult representing the result of the action, which is typically a ViewResult that renders the User view.
         */
        [Authorize(Roles = "User")]
        public IActionResult User()
        {
            return View();
        }

        /*
         * The Error action method handles requests to the error page of the application. 
         * It is decorated with the [ResponseCache] attribute, which disables caching for this action to ensure that error information is always up-to-date.
         * 
         * @return An IActionResult representing the result of the action, which is typically a ViewResult that renders the Error view with an ErrorViewModel containing the current request ID.
         */
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
