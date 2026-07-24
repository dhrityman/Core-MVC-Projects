using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using UserRoles.Models;
using UserRoles.ViewModels;

namespace UserRoles.Controllers
{
    /*
     * The AccountController class is a controller in an ASP.NET Core MVC application that handles user account-related actions, such as login, registration, email verification, password change, and logout. 
     * It utilizes the ASP.NET Core Identity framework to manage user authentication and authorization.
     * 
     * The controller uses dependency injection to obtain instances of SignInManager<Users>, UserManager<Users>, and RoleManager<IdentityRole> for managing user sign-in, user creation, and role management, respectively.
     */
    public class AccountController : Controller
    {
        /*
         * The signInManager field is a private readonly instance of SignInManager<Users> used for managing user sign-in operations, such as password sign-in and sign-out.
         */
        private readonly SignInManager<Users> signInManager;
        /*
         * The userManager field is a private readonly instance of UserManager<Users> used for managing user-related operations, such as creating users, finding users by email or username, and managing user roles.
         */
        private readonly UserManager<Users> userManager;
        /*
         * The roleManager field is a private readonly instance of RoleManager<IdentityRole> used for managing role-related operations, such as creating roles and checking if roles exist.
         */
        private readonly RoleManager<IdentityRole> roleManager;

        /*
         * The constructor for the AccountController class takes instances of SignInManager<Users>, UserManager<Users>, and RoleManager<IdentityRole> as parameters, which are injected by the ASP.NET Core dependency injection system. 
         * It initializes the corresponding fields with the provided instances.
         * 
         * @param signInManager An instance of SignInManager<Users> used for managing user sign-in operations.
         * @param userManager An instance of UserManager<Users> used for managing user-related operations.
         * @param roleManager An instance of RoleManager<IdentityRole> used for managing role-related operations.
         */
        public AccountController(SignInManager<Users> signInManager, UserManager<Users> userManager, RoleManager<IdentityRole> roleManager)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
            this.roleManager = roleManager;
        }

        /*
         * The Login action method handles GET requests to the login page of the application. 
         * It returns the default view associated with the Login action.
         * 
         * @return An IActionResult representing the result of the action, which is typically a ViewResult that renders the Login view.
         */
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        /*
         * The Login action method handles POST requests for user login. 
         * It takes a LoginViewModel as a parameter, which contains the user's email, password, and remember-me option. 
         * If the model state is valid, it attempts to sign in the user using the SignInManager. 
         * If the sign-in is successful, it redirects to the home page; otherwise, it adds an error message to the model state and returns the login view.
         * 
         * @param model An instance of LoginViewModel containing the user's login credentials.
         * @return An IActionResult representing the result of the action, which can be a redirect to the home page or a ViewResult that renders the Login view with error messages.
         */
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var result = await signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: false);

            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError(string.Empty, "Invalid Login Attempt.");
            return View(model);
        }

        /*
         * The Register action method handles GET requests to the registration page of the application. 
         * It returns the default view associated with the Register action.
         * 
         * @return An IActionResult representing the result of the action, which is typically a ViewResult that renders the Register view.
         */
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        /*
         * The Register action method handles POST requests for user registration. 
         * It takes a RegisterViewModel as a parameter, which contains the user's name, email, and password. 
         * If the model state is valid, it creates a new user using the UserManager and assigns them the "User" role. 
         * If the registration is successful, it signs in the user and redirects to the login page; otherwise, it adds error messages to the model state and returns the registration view.
         * 
         * @param model An instance of RegisterViewModel containing the user's registration information.
         * @return An IActionResult representing the result of the action, which can be a redirect to the login page or a ViewResult that renders the Register view with error messages.
         */
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = new Users
            {
                FullName = model.Name,
                UserName = model.Email,
                NormalizedUserName = model.Email.ToUpper(),
                Email = model.Email,
                PhoneNumber = model.PhoneNumber,
                NormalizedEmail = model.Email.ToUpper()
            };

            var result = await userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                var roleExist = await roleManager.RoleExistsAsync("User");

                if (!roleExist)
                {
                    var role = new IdentityRole("User");
                    await roleManager.CreateAsync(role);
                }

                await userManager.AddToRoleAsync(user, "User");

                await signInManager.SignInAsync(user, isPersistent: false);
                return RedirectToAction("Login", "Account");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return View(model);
        }

        /*
         * The VerifyEmail action method handles GET requests to the email verification page of the application. 
         * It returns the default view associated with the VerifyEmail action.
         * 
         * @return An IActionResult representing the result of the action, which is typically a ViewResult that renders the VerifyEmail view.
         */
        [HttpGet]
        public IActionResult VerifyEmail()
        {
            return View();
        }

        /*
         * The VerifyEmail action method handles POST requests for email verification. 
         * It takes a VerifyEmailViewModel as a parameter, which contains the user's email. 
         * If the model state is valid, it attempts to find the user by their email using the UserManager. 
         * If the user is found, it redirects to the ChangePassword action; otherwise, it adds an error message to the model state and returns the email verification view.
         * 
         * @param model An instance of VerifyEmailViewModel containing the user's email for verification.
         * @return An IActionResult representing the result of the action, which can be a redirect to the ChangePassword action or a ViewResult that renders the VerifyEmail view with error messages.
         */
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> VerifyEmail(VerifyEmailViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await userManager.FindByNameAsync(model.Email);

            if (user == null)
            {
                ModelState.AddModelError("", "User not found!");
                return View(model);
            }
            else
            {
                return RedirectToAction("ChangePassword", "Account", new { username = user.UserName });
            }
        }

        /*
         * The ChangePassword action method handles GET requests to the change password page of the application. 
         * It takes a username as a parameter and checks if it is null or empty. If it is, it redirects to the VerifyEmail action; otherwise, it returns the change password view with a ChangePasswordViewModel containing the user's email.
         * 
         * @param username A string representing the username of the user whose password is to be changed.
         * @return An IActionResult representing the result of the action, which can be a redirect to the VerifyEmail action or a ViewResult that renders the ChangePassword view with the user's email.
         */
        [HttpGet]
        public IActionResult ChangePassword(string username)
        {
            if (string.IsNullOrEmpty(username))
            {
                return RedirectToAction("VerifyEmail", "Account");
            }

            return View(new ChangePasswordViewModel { Email = username });
        }

        /*
         * The ChangePassword action method handles POST requests for changing a user's password. 
         * It takes a ChangePasswordViewModel as a parameter, which contains the user's email and new password. 
         * If the model state is valid, it attempts to find the user by their email using the UserManager. 
         * If the user is found, it removes the existing password and adds the new password. 
         * If the password change is successful, it redirects to the login page; otherwise, it adds error messages to the model state and returns the change password view.
         * 
         * @param model An instance of ChangePasswordViewModel containing the user's email and new password.
         * @return An IActionResult representing the result of the action, which can be a redirect to the login page or a ViewResult that renders the ChangePassword view with error messages.
         */
        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Something went wrong");
                return View(model);
            }

            var user = await userManager.FindByNameAsync(model.Email);

            if(user == null)
            {
                ModelState.AddModelError("", "User not found!");
                return View(model);
            }

            var result = await userManager.RemovePasswordAsync(user);
            if (result.Succeeded)
            {
                result = await userManager.AddPasswordAsync(user, model.NewPassword);
                return RedirectToAction("Login", "Account");
            }
            else
            {
                foreach(var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

                return View(model);
            }
        }

        /*
         * The Logout action method handles POST requests for logging out the currently signed-in user. 
         * It uses the SignInManager to sign out the user and then redirects to the home page of the application.
         * 
         * @return An IActionResult representing the result of the action, which is a redirect to the Index action of the Home controller.
         */
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
