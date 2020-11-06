using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;


namespace Hospital_Management_System.Models
{
    public class AuditTrial
    {
        public int Id { get; set; }
        [Required]
        public string Who { get; set; }
        [Required]
        public string Transaction { get; set; }

        [Required]
        public string Where { get; set; }
        public DateTime When { get; set; }

    }
}