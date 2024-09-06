
using DBMS_Project.Data;
using DBMS_Project.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Rotativa.AspNetCore;
using static System.Threading.Tasks.Task<Microsoft.AspNetCore.Mvc.IActionResult>;
using System.Collections.Generic;
using Rotativa.AspNetCore;

namespace DBMS_Project.Controllers
{
    public class PaymentController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager <IdentityUser> _userManager;

        
        

        public PaymentController(ApplicationDbContext db, UserManager<IdentityUser> userManager)
        {
            _db = db;
            _userManager = userManager;
        }
        
        // public IActionResult Index1(List<Payment> order)
        // {
        //     return View();
        // }
        
        public  IActionResult Index1()
        
        {
        
        
            // Retrieve categories from the database based on the selected place
            var order = _db.Payment.ToList();
            // TempData["Orders"] = order;
        
            return View(order);
        }
        
        
      
        
        
        [Authorize]
        public async Task<IActionResult> Index()
        {
            // Get the current user
            var currentUser = await _userManager.GetUserAsync(User);

            // Retrieve the last payment made by the current user
            var lastPayment = _db.Payment
                .Where(p => p.UserId == currentUser.Id)
                .OrderByDescending(p => p.PaymentDateTime)
                .FirstOrDefault();

            return View(lastPayment);
        }
        
        public async Task<IActionResult> DownloadPdf(int id)
        {
            var payment = _db.Payment.Find(id);
            if (payment == null)
            {
                return NotFound();
            }

            // Generate the PDF content
            var pdf = new ViewAsPdf("Index", payment);
            var pdfContent = await pdf.BuildFile(ControllerContext);

            // Return the PDF content as a file with Content-Disposition set to "attachment"
            return File(pdfContent, "application/pdf", "PaymentReceipt.pdf");
        }




        
        [Authorize]
        public async Task<IActionResult> ProcessPayment()
        {
            // Get the current user
            var currentUser = await _userManager.GetUserAsync(User);
            // if (currentUser == null)
            // {
            //     // User is not logged in, redirect to login
            //     return RedirectToAction("Index", "Home");
            // }
            var cartItems = _db.Cart.Where(c => c.UserId == currentUser.Id).ToList();
            string itemsJson = string.Join(", ", cartItems.Select(item => $"{item.Name} : {item.quantity} - LOCATION: {item.Place} - PRICE: {item.Price}"));


            // Get the total quantity of items ordered
            int totalQuantity = cartItems.Sum(item => item.quantity);

            // Calculate the total amount
            double? totalAmount = cartItems.Sum(item => item.Price * item.quantity);

            // Generate a unique transaction ID
            string transactionId = Guid.NewGuid().ToString();

            // Create a Payment object
            var payment = new Payment
            {
                UserId = currentUser.Id.ToString(),
                ItemsJson = itemsJson, // Serialize items to JSON if needed
                TotalAmount = Convert.ToInt32(totalAmount),
                TransactionId = transactionId,
                PaymentDateTime = DateTime.Now,
                Status = 1,
                FirstName = currentUser.UserName,
            };

            // Save the payment to the database
            _db.Payment.Add(payment);
            await _db.SaveChangesAsync();
            
            var lastPayment = await _db.Payment.OrderByDescending(p => p.PaymentDateTime).FirstOrDefaultAsync();

            // Payment processed successfully, redirect to payment success page
            return View("Index", lastPayment);
        }

        public IActionResult PaymentSuccess()
        {
            return View("Index");
        }
       

      
    }
} 