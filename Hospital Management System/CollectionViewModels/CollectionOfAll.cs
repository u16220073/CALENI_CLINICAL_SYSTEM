using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Hospital_Management_System.Models;

namespace Hospital_Management_System.CollectionViewModels
{
    public class CollectionOfAll
    {
      
        public IEnumerable<Centre> Departments { get; set; }
        public IEnumerable<Psychologist> Psychologists { get; set; }
        public IEnumerable<Patient> Patients { get; set; }
        public IEnumerable<Appointment> ActiveAppointments { get; set; }
        public IEnumerable<Appointment> PendingAppointments { get; set; }
      
        public IEnumerable<Announcement> Announcements { get; set; }
    }
}