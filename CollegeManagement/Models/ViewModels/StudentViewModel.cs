using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CollegeManagement.Models.ViewModels
{
    public class StudentViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "This field is required")]
        [StringLength(50)]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "This field is required")]
        [StringLength(50)]
        public string LastName { get; set; }
        [Required(ErrorMessage = "This field is required")]
        public string Adress { get; set; }
        [Required]
        [RegularExpression(@"^[a-z0-9](\.?[a-z0-9]){5,}@miraclesoft\.com$",
        ErrorMessage = "Please enter correct email address")]
        public string EmailId { get; set; }
        [Required(ErrorMessage = "This field is required")]
        [StringLength(15)]
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
