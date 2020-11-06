using Hospital_Management_System.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Hospital_Management_System.CollectionViewModels
{
    public class ConsultationCollection
    {
        public Consultation Consultation{ get; set; }

        [DataType(DataType.Upload)]
       public  HttpPostedFileBase ImageUpload { get; set; }

        public IEnumerable<Psychologist> Psychologists { get; set; }

        public IEnumerable<Patient> Patients { get; set; }

    }
}