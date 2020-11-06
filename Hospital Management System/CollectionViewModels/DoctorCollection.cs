using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Hospital_Management_System.Models;

namespace Hospital_Management_System.CollectionViewModels
{
    public class DoctorCollection
    {
        public RegisterViewModel ApplicationUser { get; set; }
        public Psychologist Psychologist { get; set; }
        public IEnumerable<Centre> Centres { get; set; }
    }
}