using System.ComponentModel.DataAnnotations;

namespace SimpleWebApp.Models
{
    public class Person
    {
        [Required(ErrorMessage = "Please Enter Name e.g. John")]
        [Display(Name = "First name")]
        [StringLength(30, MinimumLength = 3)]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Please Enter Surname e.g. Picasso")]
        [Display(Name = "Second name")]
        [StringLength(30, MinimumLength = 3)]
        public string SecondName { get; set; }

        [Required(ErrorMessage = "Please Enter Age e.g. 15")]
        public int Age { get; set; }

        [Required(ErrorMessage = "Please Enter Email e.g. user@gmail.com")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please Enter Id")]
        public int Id { get; set; }
    }
}