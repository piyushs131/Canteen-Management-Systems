using DBMS_Project.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using DBMS_Project.Enums;
using DBMS_Project.Models;

namespace DBMS_Project.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _db;

        public CategoryController(ApplicationDbContext db)
        {
            _db = db;
        }
        
       
        // public IActionResult Index()
        // {
        //     // Retrieve categories from the database based on the selected place
        //     var categories = _db.Category.ToList();
        //
        //     return View(categories);
        // }

        public IActionResult Index(PlaceType place)
        {
            // Retrieve categories from the database based on the selected place
            var categories = _db.Category.Where(c => @c.Place == place).ToList();
    
            return View(categories);
        }

        public IActionResult Index1()
        {
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



    }
}