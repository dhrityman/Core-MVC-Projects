using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;

namespace BestStoreMVC.Controllers
{
    /// <summary>
    /// Step 28: Due to asp-controller="Language" setting in  Step 27.2 in each Dreodown Item , crwate new Controller named as "LanguageController" in the Controllers 
    /// folder to handle the language selection and change the culture accordingly.
    /// </summary>
    public class LanguageController : Controller
    {

        /// <summary>
        /// Step 28.1: add a SetLanguage action method that takes a culture parameter and returns a view.
        /// </summary>
        /// <param name="culture"></param>
        /// <returns></returns>
        public IActionResult SetLanguage(string culture)
        {
            // Step 28.2: Use the CookieRequestCultureProvider to set the culture in a cookie and redirect the user back to the previous page.
            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }            
            );
            string returnUrl = Request.Headers.Referer.ToString();
            return Redirect(returnUrl);
        }
    }
}
