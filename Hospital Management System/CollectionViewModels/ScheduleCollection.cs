using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Hospital_Management_System.Models;

namespace Hospital_Management_System.CollectionViewModels
{
    public class ScheduleCollection
    {
        public Schedule Schedule { get; set; }
        public IEnumerable<Psychologist> Psychologists { get; set; }
        public IEnumerable<Centre> Centres { get; set; }
        public IEnumerable<Patient> Patients { get; set; }
        public string Problem { get; set; }
    }
}