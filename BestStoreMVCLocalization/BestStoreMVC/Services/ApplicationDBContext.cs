using Microsoft.EntityFrameworkCore;


namespace BestStoreMVC.Services
{
    public class ApplicationDBContext:DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options):base(options)
        {
        }
        /*
         * STEP 7: Add a DbSet for the Product model to the ApplicationDBContext class to 
         * enable you to perform basic CRUD (Create, Read, Update, Delete) operations against entities.
         */
        public DbSet<Models.Product> Products { get; set; }
    }
}
