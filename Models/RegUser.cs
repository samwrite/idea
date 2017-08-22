using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace idea.Models
{
    public class RegUser : BaseEntity
    {
      
        [Required(ErrorMessage = "Name Required")]
        [MinLength(2, ErrorMessage = "Name not long enough")]
        [Display(Name = "Name")]
        public string Name{ get; set; }
        
        [Required(ErrorMessage = "Alias Required")]
        [MinLength(2, ErrorMessage = "Alias not long enough")]
        [Display(Name = "Alias")]
        public string Alias { get; set; }
       
        [Required(ErrorMessage = "Email Required")]
        [Display(Name = "Email")]
        public string Email { get; set; }
        
        [Required(ErrorMessage = "Password Required")]
        [MinLength(8, ErrorMessage = "Password not long enough")]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Confirm Password Required")]
        [Display(Name = "Confirm Password")]
        [Compare("Password")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }
}