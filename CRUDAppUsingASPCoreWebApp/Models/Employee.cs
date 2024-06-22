using System.ComponentModel.DataAnnotations;

namespace CRUDAppUsingASPCoreWebApp.Models
{
public class Employee
    {
        public string? id { get; set; }
        [Required]
        public string? name { get; set; }
        [Required]
        public string? email { get; set; }
        [Required]
        public string? phone { get; set; }
        [Required(ErrorMessage =("Please enter salary greater than 0"))]
        [Range(1,int.MaxValue)]
        public decimal salary { get; set; }
    }
}

