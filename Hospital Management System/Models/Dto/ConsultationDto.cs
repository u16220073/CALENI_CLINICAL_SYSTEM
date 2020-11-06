using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Hospital_Management_System.Models.Dto
{
    public class ConsultationDto
    {
        public int Id { get; set; }

        
        [Display(Name = "Patient Name")]
        public string PatientName { get; set; }

        [Display(Name = "Psychologist Name")]
        public string PsychologistName { get; set; }

        [Display(Name = "Consultation Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? ConsultationDate { get; set; }

        [Display(Name = "Diagnosis")]
        public string Diagnosis { get; set; }


        [Display(Name = "Treatment Plan")]
        public string TreatmentPlan { get; set; }

        [DataType(DataType.Upload)]
        HttpPostedFileBase ImageUpload { get; set; }
    }
}