using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace DBMS_Project.Models;

public class Payment
{
   
        public int? Id { get; set; }
        
        public string? FirstName { get; set; } // User's first name
        public string? ItemsJson { get; set; } // JSON string containing items and quantities
        public decimal? TotalAmount { get; set; }  // Total amount of the payment
        public string? TransactionId { get; set; } // Uniquely generated transaction ID
        public DateTime? PaymentDateTime { get; set; } // Date and time of the payment
        [ForeignKey("User")]
        public string? UserId { get; set; } // Foreign key to AspNetUsers table

        public IdentityUser User { get; set; } // Navigation property to ApplicationUser
        
        public int? Status { get; set; }
    }

