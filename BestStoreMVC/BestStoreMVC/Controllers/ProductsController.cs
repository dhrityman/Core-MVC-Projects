using BestStoreMVC.Models;
using BestStoreMVC.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.Data;
using System.Reflection.Metadata.Ecma335;

namespace BestStoreMVC.Controllers
{
    /// <summary>
    /// Create ProductsController
    /// </summary>
    public class ProductsController : Controller
    {
        /// <summary>
        /// Represents the hosting environment for the current application.
        /// </summary>
        /// <remarks>This field provides access to environment-specific information, such as the
        /// application name, content root path, and environment name (e.g., Development, Staging, Production). It is
        /// typically used to configure application behavior based on the hosting environment.</remarks>
        private readonly IWebHostEnvironment environment;

        /// <summary>
        /// STEP 13:  Now in the Index Action (ProductsController.Index()) we need to read the list of Products from the database and we have to pass this list to the view.
        /// Now to read the data from the database, we need our Application DB context(BestStoreMVC.Services.ApplicationDBContext)
        /// that we already add to the service container and request it from service container, we need to create Constructor BestStoreMVC.Controllers.ProductsController.
        /// and inject the ApplicationDBContext into the constructor of the ProductsController class. 
        /// This will allow us to access the database context and perform CRUD operations on the Product entity.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="environment">object of IWebHostEnvironment type, to provide the Web host environment</param>
        public ProductsController(ApplicationDBContext context, IWebHostEnvironment environment) 
        {
          this.Context = context;
          this.environment = environment;

        }

        public ApplicationDBContext Context { get; }
        public IWebHostEnvironment Environment { get; }

        /// <summary>
        /// By Default, We have a Index method, which call the Action that is display a view anmed as Index.cshtml,
        /// So Index.cshtml should be availible in views folder.
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            /*
             * STEP 14: Now in the Index Action (ProductsController.Index()) we need to read the list of Products from the database and we have to pass this list to the view.
             * Now to read the data from the database, we need our Application DB context(BestStoreMVC.Services.ApplicationDBContext)
             * that we already add to the service container and request it from service container, we need to create Constructor BestStoreMVC.Controllers.ProductsController.
             * and inject the ApplicationDBContext into the constructor of the ProductsController class. 
             * This will allow us to access the database context and perform CRUD operations on the Product entity.
             * To read the Product list in the view pass the list of products to the view, 
             * we can use the ToList() method of the DbSet<Product> property of the ApplicationDBContext class.
             */
            /*To Display in acending Order by ID */
            //var products = this.Context.Products.ToList();
            /*To Display in decending Order by ID */
            var products = this.Context.Products.OrderByDescending(p =>p.Id).ToList();
            /*
             * Right click on ProductsController.Index() method, and add view, add Empty Razor view.
             * After adding a Empty Razore view named 'Index.cshtml', you will see a new view file added with the name as Index.cshtml 
             * under View=> Product folder. 
             */
            return View(products);
        }

        #region Create a new product.

        #region Add new Action named Create On Load Create.cshtml on the click of Button (Create New Product) on Index.cshtml, to load empty page to register a new product in database.

        /// <summary>
        /// STEP 18: Create a new action method named "Create" in the ProductsController class to handle the Create operation at the time of Page load "Create.cshtml". 
        /// This action method will be responsible for displaying the form to create a new product and handling the form submission.
        /// </summary>
        /// <returns></returns>
        public IActionResult Create()
        {
            /*
             * STEP 14: Now in the Create Action (ProductsController.Create()) we need to create a new Product in the database and we have to pass this Product calss with created ID from database to the view.
             */
            var productDTO = new ProductDTO();
            
            /*
             * Right click on ProductsController.Index() method, and add view, add Empty Razor view named as "Create.cshtml".
             * After adding a Empty Razore view named Create.cshtml, you will see a new view file added with the name as Create.cshtml 
             * under View=> Product folder. 
             */
            return View(productDTO);
        }

        #endregion Add new Action named Create On Load Create.cshtml on the click of Button (Create New Product) on Index.cshtml, to load empty page to register a new product in database.

        #region Add new Action named Create On Click on Submit button of Create.cshtml, to save the new product in database.

        /// <summary>
        /// Handles HTTP POST requests to create a new resource product of view Create.cshtml and Model ProductDTO.
        /// </summary>
        /// <param name="productDTO">New Product data of type Model ProductDTO</param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Create(ProductDTO productDTO)
        {
            #region commented code
            //if (productDTO.ImageFile != null && productDTO.ImageFile.Length > 0)
            //{
            //    // Save the uploaded image file to a specific location
            //    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", productDTO.ImageFile.FileName);
            //    using (var stream = new FileStream(filePath, FileMode.Create))
            //    {
            //        productDTO.ImageFile.CopyTo(stream);
            //    }
            //}
            #endregion commented code

            // Check if the uploaded file is null or has a length of 0, and add a model error if it does.
            // This will ensure that the user is informed about the issue and can correct it before resubmitting the form.
            if (productDTO.ImageFile == null || productDTO.ImageFile.Length == 0)
            {
                  ModelState.AddModelError("ImageFile", "Please upload an image file with valid file extension.");
            }
            if(productDTO.Price < 0.01m)
            {
                ModelState.AddModelError("Price", "Price must be greater than 0.00");
            }
            // Validate the model state and return the view with validation errors if any validation errors exist.
            // This will ensure that the user is informed about any issues with the submitted data and can correct them before resubmitting the form.
            if (!ModelState.IsValid)
            {
                return View(productDTO);
            }
            /*
             * Step 19.3.4: First need to save the Product Image file to the server. To do this, we need to check if the IFormFile property of the ProductDTO object is not null and has a valid file name.
             *       If it does, we can generate a unique file name for the image file and save it to a specific folder on the server using the IFormFile.CopyTo() method.
             *       Save the new Product entity to the database using the ApplicationDBContext and call SaveChanges() to persist the changes.
             *       1. Add a parameter in the contructor of Type IwebHostEnvironment to the Create action method to get the path of the wwwroot folder.
             *       2. Create a unique name of file name of the Product Image file to avoid overwriting existing files with the same name.
             *       3. Get the full path of the Product Image file by combining the wwwroot folder path, the folder name where the images will be stored, and the unique file name.
             *       4. Create a new FileStream object with the full path and FileMode.Create to create a new file or overwrite an existing file with the same name.
             *       5. Use the IFormFile.CopyTo() method to copy the contents of the uploaded image file to the new file stream, which will save the file to the server.
             *       6. Create a new Product entity and populate its properties with the data from the ProductDTO object, including the unique file name of the uploaded image to save in database.
             *       7. Save the new Product entity to the database using the ApplicationDBContext and call SaveChanges() to persist the changes.
             */
            string uniqueFileName = "ProductImage-" + DateTime.Now.ToString("yyyyMMddHHmmssfff");            
            uniqueFileName= uniqueFileName + Path.GetExtension(productDTO.ImageFile!.FileName);
            string imageFullPath = environment.WebRootPath+ "/Products_Images/"+ uniqueFileName;

            using (var stream = new FileStream(imageFullPath, FileMode.Create))
            {
                /// Save the uploaded image file to the specified path on the server.
                productDTO.ImageFile.CopyTo(stream);
            }

            /// Create a new Product entity and populate its properties with the data from the ProductDTO object, including the unique file name of the uploaded image to save in database.
            Product product =new Product()
            {
                Name = productDTO.Name,
                Brand = productDTO.Brand, 
                Category = productDTO.Category, 
                Description = productDTO.Description,                  
                Price = productDTO.Price, 
                ImageFileName = uniqueFileName, 
                CreatedAt = DateTime.Now 
            };

            ///Save in Database using ApplicationDBContext and call SaveChanges() to persist the changes.
            this.Context.Products.Add(product);
            this.Context.SaveChanges();

            /// After successfully creating the product, redirect the user to the Index action of the ProductsController to display the list of products.
            return RedirectToAction("Index","Products");            
        }

        #endregion Add new Action named Create On Click on Submit button of Create.cshtml, to save the new product in database.

        #endregion Create a new product.

        #region Add new Action named Edit On Load Create.cshtml on the click of Button (Edit) on Index.cshtml, to load fill the existing product details from the database.

        public IActionResult Edit(int id)
        {
            // Retrieve the product from the database based on the provided ID
            var product = this.Context.Products.FirstOrDefault(p => p.Id == id);
            // If the product is not found, return a NotFound result
            if (product == null)
            {
                //return NotFound();
                return RedirectToAction("Index", "Products");
            }
            // Create a ProductDTO object and populate it with the existing product details
            var productDTO = new ProductDTO
            {
                Name = product.Name,
                Brand = product.Brand,
                Category = product.Category,
                Price = product.Price,
                Description = product.Description,
                // Note: ImageFile is not populated here as it's for new uploads only
            };

            ViewData["ProductId"] = product.Id; // Pass the Product ID to the view for reference
            ViewData["ImageFileName"] = product.ImageFileName; // Pass the existing image file name to the view for display]
            ViewData["CreatedAt"] = product.CreatedAt.ToString("MM/dd/yyyy"); // Pass the created date to the view for display]
            // Pass the ProductDTO to the Edit view to pre-fill the form with existing details
            return View(productDTO);
        }

        #endregion  Add new Action named Edit On Load Create.cshtml on the click of Button (Edit) on Index.cshtml, to load fill the existing product details from the database.

        #region Add new Action named Edit On Click on Submit button of Edit.cshtml, to save the updated product in database.

        /// <summary>
        /// This action method handles the HTTP POST request for editing an existing product call at Submit button call on Edit.cshtml view.
        /// It retrieves the product from the database based on the provided ID, updates its details with the data from the ProductDTO object, 
        /// and saves the changes to the database. If a new image file is uploaded, it updates the image on the server and deletes the old image. 
        /// Finally, it redirects to the Index action of the ProductsController after a successful update.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="productDTO"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Edit(int id,ProductDTO productDTO)
        {
            // Retrieve the product from the database based on the provided ID
            var product = this.Context.Products.Find(id);
            // If the product is not found, return a NotFound result
            if (product == null)
            {                
                return RedirectToAction("Index", "Products");
            }
            if(!ModelState.IsValid)
            {
                ViewData["ProductId"] = product.Id; // Pass the Product ID to the view for reference
                ViewData["ImageFileName"] = product.ImageFileName; // Pass the existing image file name to the view for display]
                ViewData["CreatedAt"] = product.CreatedAt.ToString("MM/dd/yyyy"); // Pass the created date to the view for display]

                return View(productDTO);
            }

            #region Update the Image on Server and Delete the Old Image from Server
            // Update the product image, if we have a new file image.
            string newFileName = product.ImageFileName; // Keep the existing image file name by default
            if (productDTO.ImageFile != null && productDTO.ImageFile.Length > 0)
            {

                string uniqueFileName = "ProductImage-" + DateTime.Now.ToString("yyyyMMddHHmmssfff");
                newFileName = uniqueFileName + Path.GetExtension(productDTO.ImageFile!.FileName);
                string imageFullPath = environment.WebRootPath + "/Products_Images/" + newFileName;

                using (var stream = new FileStream(imageFullPath, FileMode.Create))
                {
                    /// Save the uploaded image file to the specified path on the server.
                    productDTO.ImageFile.CopyTo(stream);
                }

                string oldImageFullPath = environment.WebRootPath + "/Products_Images/" + product.ImageFileName;
                System.IO.File.Delete(oldImageFullPath); // Delete the old image file from the server
            }

            #endregion Update the Image on Server and Delete the Old Image from Server

            #region Update the Product details in Database

            product.Name = productDTO.Name;
            product.Brand = productDTO.Brand;
            product.Category = productDTO.Category;
            product.Description = productDTO.Description;
            product.Price = productDTO.Price;
            product.ImageFileName = newFileName;

            //this.Context.Products.Update(product);

            // Save the changes to the database
            this.Context.SaveChanges(); 
            // Redirect to the Index action of the ProductsController after successful update
            return RedirectToAction("Index", "Products"); 

            #endregion Update the Product details in Database

        }
        #endregion Add new Action named Edit On Click on Submit button of Edit.cshtml, to save the updated product in database.


    }
}
