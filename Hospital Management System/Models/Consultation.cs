using System;
using System.ComponentModel.DataAnnotations;

namespace Hospital_Management_System.Models
{
    public class Consultation
    {

        public int Id { get; set; }

        public Patient Patient { get; set; }
        [Display(Name = "Patient Name")]
        public int PatientId { get; set; }

        public Psychologist  Psychologist { get; set; }
        [Display(Name = "Psychologist Name")]
        public int PsychologistId { get; set; }

        [Display(Name = "Consultation Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? ConsultationDate { get; set; }

        [Display(Name = "Diagnosis")]
        public string Diagnosis { get; set; }

        [Display(Name = "Treatment Plan")]
        public string TreatmentPlan { get; set; }

        [Required]
        [DataType(DataType.ImageUrl)]
        public string ImageUrl { get; set; }

    }
}