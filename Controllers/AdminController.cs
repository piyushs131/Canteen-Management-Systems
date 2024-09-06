using DBMS_Project.Data;
using DBMS_Project.Models;
using DBMS_Project.Views.Admin;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace DBMS_Project.Controllers;

public class AdminController : Controller
{
    private readonly ApplicationDbContext _db;

    public AdminController(ApplicationDbContext db)
    {
        _db = db;
    }
    
   // public IActionResult Orders()
   //  {
   //      
   //      
   //      // Retrieve categories from the database based on the selected place
   //      var pays = _db.Payment.ToList();
   //       
   //      return View(pays);
   //  }

    public IActionResult Login()
    {
        return RedirectToAction("Index1", "Payment");
    }
    
    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Login(Admin model)
    {
        // Validate admin credentials
        var admin = _db.Admin.FirstOrDefault(a => a.Email == model.Email && a.Password == model.Password);
        if (admin != null)
        {
            // Redirect to payments page if credentials are valid
            return RedirectToAction("Index1", "Payment");
        }
        else
        {
            // Show error message if credentials are invalid
            ModelState.AddModelError(string.Empty, "Invalid email or password");
            return View("Index", model);
        }
    }

    // public  IActionResult Orders()
    //     {
    //     
    //     
    //         // Retrieve categories from the database based on the selected place
    //         var pay = _db.Payment.ToList();
    //     
    //         return View();
    //     }
    
}