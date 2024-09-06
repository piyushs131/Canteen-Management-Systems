using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace DBMS_Project.Models;
using System.ComponentModel.DataAnnotations.Schema;
public class Wallet
{
    [Key]
    [ForeignKey("User")]
    public string UserId { get; set; }

    public double Balance { get; set; }

    public virtual IdentityUser User { get; set; }

  
}