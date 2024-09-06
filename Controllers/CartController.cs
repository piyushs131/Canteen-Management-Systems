using DBMS_Project.Data;
using DBMS_Project.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Net;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace DBMS_Project.Controllers
{
    public class CartController : Controller
    {
       

        private readonly ApplicationDbContext _db;
        private readonly UserManager <IdentityUser> _userManager;

        public CartController(ApplicationDbContext db,UserManager<IdentityUser> userManager)
        {
            _db = db;
            _userManager = userManager;
        }

        
        [HttpPost]
        
        public async Task<IActionResult> AddToCart(int itemId)
        {
            // Retrieve the selected item from the Category model
            var selectedItem = await _db.Category.FindAsync(itemId);
           
            // Check if the user is authenticated
            // if (User.Identity.IsAuthenticated)
            // {
            //     // User is authenticated, continue with the operation
            //     RedirectToAction([Authorize])
            // }
            // else
            // {
            //     // User is not authenticated, handle accordingly (e.g., redirect to login page)
            //     return RedirectToAction("Login", "Account");
            // }


            // if ( [Authorize])
            // {
            //     Continue;
            // }
            
            
        if (selectedItem != null)
            {
                // Get the current user
                var currentUser = await _userManager.GetUserAsync(User);
                
                if (!User.Identity.IsAuthenticated)
                {
                    // User is not authenticated, challenge authentication and redirect to the specified URL
                    return Challenge(new AuthenticationProperties { RedirectUri = "/Identity/Account/Login" });
                }

        
                if (currentUser != null)
                {
                    // Check if the item already exists in the cart for the current user
                    var existingCartItem = _db.Cart.FirstOrDefault(c => c.Id == itemId && c.UserId == currentUser.Id);

                    if (existingCartItem != null)
                    {
                        // Update the quantity of the existing item in the cart
                        existingCartItem.quantity++;
                    }
                    else
                    {
                        // Create a new instance of the Cart model and populate its attributes
                        var cartItem = new Cart
                        {
                            Name = selectedItem.Name,
                            Price = selectedItem.Price,
                            Type = selectedItem.Type,
                            Place = selectedItem.Place,
                            IsAvailable = selectedItem.IsAvailable,
                            Id = selectedItem.Id,
                            Description = selectedItem.Description,
                            Size = selectedItem.Size,
                            Image = selectedItem.Image,
                            quantity = 1,
                            UserId = currentUser.Id, // Associate the cart item with the current user's ID
                        };

                        // Save the cart item to the database
                        _db.Cart.Add(cartItem);
                    }

                    await _db.SaveChangesAsync();

                    return RedirectToAction("Index1"); // Redirect to the Cart page
                }
            }

            return NotFound(); // Handle the case where the selected item is not found
        }



        public IActionResult Index()
        {
           

            // Retrieve categories from the database based on the selected place
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var items = _db.Cart.ToList().Where(c => c.UserId == userId).ToList();
        
            return View(items);
        }
        
        public IActionResult Index1()
        {
            // Get the URL of the previous page from the Referer header
            string previousPageUrl = Request.Headers["Referer"];

            // Redirect to the previous page
            return Redirect(previousPageUrl);
        }
        
        public IActionResult Index2()
        {
            TempData["PreviousPageUrl"] = Request.Headers["Referer"];
            return RedirectToAction("Index");
        }
       

        // public IActionResult YourCategoryAction()
        // {
        //     // Retrieve the count of items in the cart from the database
        //     int itemCount = _db.Cart.Count();
        //
        //     // Pass the count to the view
        //     ViewBag.ItemCount = itemCount;
        //
        //     // Other code...
        //     return View("Index");
        // }
        
        [HttpPost]
        public IActionResult RemoveItem(int itemId)
        {
            var itemToRemove = _db.Cart.FirstOrDefault(c => c.Id == itemId);
            if (itemToRemove != null)
            {
                _db.Cart.Remove(itemToRemove);
                _db.SaveChanges();
            }

            // Redirect to the cart page
            return RedirectToAction("Index");
        }
        
        [HttpPost]
        public IActionResult UpdateQuantity(int itemId, int newQuantity)
        {
            var cartItem = _db.Cart.Find(itemId);
            if (cartItem != null)
            {
                cartItem.quantity = newQuantity;
                _db.SaveChanges();
        
                // Calculate total price
                var totalPrice = cartItem.Price * cartItem.quantity;
        
                // Return updated data
               // return Json(new { success = true, totalPrice = totalPrice });
               return RedirectToAction("Index");
            }
            else
            {
                return Json(new { success = false, error = "Item not found" });
            }
        }






        

    }
}
        