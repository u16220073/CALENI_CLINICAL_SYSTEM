using System;
using System.ComponentModel.DataAnnotations;
using Hospital_Management_System.Common;

namespace Hospital_Management_System.Models
{
    public class Patient
    {
        public int Id { get; set; }

        public ApplicationUser ApplicationUser { get; set; }
        public string ApplicationUserId { get; set; }

        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Display(Name = "Name")]
        public string FullName { get; set; }

        [Display(Name = "Profile Picture")]
        public string ProPic { get; set; }

        [EmailAddress]
        [Display(Name = "Email Id")]
        public string EmailAddress { get; set; }
       
        [Phone]
        [Display(Name = "Phone No")]
        public string PhoneNo { get; set; }
        public string Contact { get; set; }



        [Display(Name = "Age")]
        public int Age { get; set; }

        [Display(Name = "Level Of Education")]
        public string LevelOfEducation { get; set; }

        [Display(Name = "Language")]
        public string Language { get; set; }

        public string Gender { get; set; }

        [Display(Name = "Date of Birth")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DateRange("1990/01/01")]
        public DateTime? DateOfBirth { get; set; }

        public string Address { get; set; }

        [Display(Name = "Marital Status ")]
        public string MaritalStatus { get; set; }

    }

}