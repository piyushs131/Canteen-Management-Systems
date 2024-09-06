using System.ComponentModel.DataAnnotations;

namespace DBMS_Project.Models;

public class Admin
{
   
        public int Id { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    

}