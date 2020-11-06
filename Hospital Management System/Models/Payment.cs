using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Hospital_Management_System.Models
{
    public class Payment
    {
        public int Id { get; set; }
      

        public Patient Patient { get; set; }
        [Display(Name = "Patient Name")]
        public int PatientId { get; set; }

        [Display(Name = "Patient Address")]
        public string PatientAddress { get; set; }

        [Display(Name = "Patient Name")]
        public string PatientName { get; set; }


        [Display(Name = "Patient Number")]
        public string PatientNumber { get; set; }

        [Display(Name = "Patient Email")]
        public string PatientEmail { get; set; }

        [Display(Name = "Patient Gender")]
        public string PatientGender { get; set; }

        public string PsychologistName { get; set; }

        public string PsychologistSpecialist { get; set; }

        public string PsychologistContact { get; set; }

        public string CentreContact { get; set; }

        public string CentrLocation { get; set; }

        [Display(Name = "Patient Name")]
        public string CenterName { get; set; }

        public Psychologist  Psychologist { get; set; }
        [Display(Name = "Psychologist Name")]
        public int PsychologistId { get; set; }

        [Display(Name = "Payment Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? PaymentDate { get; set; } = DateTime.Now;

        [Display(Name = "DateOfBirth Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? DateOfBirth { get; set; } = DateTime.Now;

        [Display(Name = "Service Recived")]
        public string ServiceRecived { get; set; }

        [Display(Name = "Hours Of Service")]
        public int HoursOfService { get; set; }

        [Display(Name = "Service Amount (R)")]
        public int ServiceAmount { get; set; }

        [Display(Name = "Paid by Medical Aid (R)")]
        public int PaidbyMedicalAid { get; set; }

        [Display(Name = "Pay by Cash (R)")]
        public int PayByCash { get; set; }

        [Display(Name = "Total Due")]
        public string TotalDue { get; set; }

        [Display(Name = "Invoice Refference")]
        public string InvoiceRefNo { get; set; }

    }

    
}