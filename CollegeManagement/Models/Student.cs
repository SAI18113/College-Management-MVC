using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CollegeManagement.Models
{
    public class Student : Audit
    {
        public int UserId { get; set; }
        [Required(ErrorMessage = "This field is required")]
        [StringLength(50)]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "This field is required")]
        [StringLength(50)]
        public string LastName { get; set; }
        [Required(ErrorMessage = "This field is required")]
        public string Adress { get; set; }
        [Required]
        
        public string Mobile { get; set; }
        [Required(ErrorMessage = "Age must be greater than 17")]
        [Range(18, 24)]
        public int Age { get; set; }
        [Required(ErrorMessage = "This field is required")]
        [StringLength(50)]
        public string State { get; set; }
        [Required(ErrorMessage = "This field is required")]
        [StringLength(50)]
        public string City { get; set; }
    }
}
