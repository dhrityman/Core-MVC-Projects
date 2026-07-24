using Microsoft.AspNetCore.Identity;

namespace UserRoles.Models
{
    /*
     * The Users class is a custom implementation of the IdentityUser class, which is part of the ASP.NET Core Identity framework. 
     * It represents a user in the application and extends the default functionality provided by IdentityUser.
     * 
     * By inheriting from IdentityUser, the Users class gains access to all the built-in properties and methods for managing user authentication and authorization, such as UserName, Email, PasswordHash, and more. 
     * The FullName property is an additional custom property that allows you to store the user's full name, providing more flexibility in user management.
     */
    public class Users : IdentityUser
    {
        public string FullName { get; set; }
    }
}
