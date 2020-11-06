using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Hospital_Management_System.CollectionViewModels;
using Hospital_Management_System.Models;
using Hospital_Management_System.Models.Dto;
using Microsoft.AspNet.Identity;
using static Hospital_Management_System.Models.ApplicationDbContext;

namespace Hospital_Management_System.Controllers
{
    public class PatientController : Controller
    {
        private ApplicationDbContext db;

        //Constructor
        public PatientController()
        {
            db = new ApplicationDbContext();
        }

        //Destructor
        protected override void Dispose(bool disposing)
        {
            db.Dispose();
        }

        [Authorize(Roles = "Patient")]
        public ActionResult Index(string message)
        {
            ViewBag.Messege = message;
            string user = User.Identity.GetUserId();
            var patient = db.Patients.Single(c => c.ApplicationUserId == user);
            var date = DateTime.Now.Date;
            var model = new CollectionOfAll
            {

                Departments = db.Centre.ToList(),
                Psychologists = db.Psychologists.ToList(),
                Patients = db.Patients.ToList(),
                ActiveAppointments = db.Appointments.Where(c => c.Status).Where(c => c.PatientId == patient.Id).Where(c => c.AppointmentDate >= date).ToList(),
                PendingAppointments = db.Appointments.Where(c => c.Status == false).Where(c => c.PatientId == patient.Id).Where(c => c.AppointmentDate >= date).ToList(),

                Announcements = db.Announcements.Where(c => c.AnnouncementFor == "Patient").ToList()
            };
            return View(model);
        }

        //Update Patient profile
        [Authorize(Roles = "Patient")]
        public ActionResult UpdateProfile(string id)
        {
            var patient = db.Patients.Single(c => c.ApplicationUserId == id);
            return View(patient);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateProfile(string id, Patient model)
        {
            var patient = db.Patients.Single(c => c.ApplicationUserId == id);
            patient.FirstName = model.FirstName;
            patient.LastName = model.LastName;
            patient.FullName = model.FirstName + " " + model.LastName;
            patient.Contact = model.Contact;
            patient.Address = model.Address;
            patient.LevelOfEducation = model.LevelOfEducation;
            patient.Age = model.Age;
            patient.MaritalStatus = model.MaritalStatus;
            patient.Language = model.Language;
            patient.DateOfBirth = model.DateOfBirth;
            patient.Gender = model.Gender;
            patient.PhoneNo = model.PhoneNo;
            db.SaveChanges();

            string audiuserName = User.Identity.GetUserName();
            AuditExtension.AddAudit(audiuserName, "Update", "Patients");

            return View();
        }


        //Start Appointment Section

        //Add Appointment
        [Authorize(Roles = "Patient")]
        public ActionResult AddAppointment()
        {
            var collection = new AppointmentCollection
            {
                Appointment = new Appointment(),
                Psychologists = db.Psychologists.ToList(),
                Schedules = db.Schedules.Where(c => c.IsBooked == false).ToList()
            };
            return View(collection);
        }

        [Authorize(Roles = "Patient")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddAppointment(AppointmentCollection model)
        {
            var collection = new AppointmentCollection
            {
                Appointment = model.Appointment,
                Psychologists = db.Psychologists.ToList()
            };

            if (model.Appointment.AppointmentDate <= DateTime.Now.Date)
            {
                ViewBag.Messege = "Please Enter the Date greater than today or equal!!";
                return View(collection);
            }
            string user = User.Identity.GetUserId();
                var patient = db.Patients.Single(c => c.ApplicationUserId == user);
                var appointment = new Appointment();
                appointment.PatientId = patient.Id;
                appointment.Schedule.PsychologistId = model.Appointment.Schedule.PsychologistId;
                appointment.AppointmentDate = model.Appointment.AppointmentDate;
                appointment.Problem = model.Appointment.Problem;
                appointment.Status = false;
                db.Appointments.Add(appointment);
                db.SaveChanges();

            string audiuserName = User.Identity.GetUserName();
            AuditExtension.AddAudit(audiuserName, "Create", "Appointments");
            return RedirectToAction("ListOfAppointments");
        }



        //Create Appointment
        [Authorize(Roles = "Patient")]
        public ActionResult CreateAppointment(int id)
        {
            var collection = new ScheduleCollection
            {
                Centres = db.Centre.ToList(),
                Schedule = db.Schedules.Single(c => c.Id == id),
                Psychologists = db.Psychologists.ToList(),
                Patients = db.Patients.ToList()
            };
            return View(collection);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateAppointment(int id, ScheduleCollection model)
        {
            if (!ModelState.IsValid)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            string user = User.Identity.GetUserId();
            var patient = db.Patients.Single(c => c.ApplicationUserId == user);

            var schedule = db.Schedules.Single(c => c.Id == id);
            schedule.PatientId = patient.Id;
            schedule.IsBooked = true;
            db.SaveChanges();

        var appointment = new Appointment();
            appointment.PatientId = patient.Id;
            appointment.ScheduleId = schedule.Id;
            appointment.AppointmentDate = db.Schedules.FirstOrDefault(d => d.Id == schedule.Id).ScheduleDate;
            appointment.StartTime = schedule.StartTime;
            appointment.EndTime = schedule.EndTime;
            appointment.Problem = model.Problem;
            appointment.Status = false;
         
            db.Appointments.Add(appointment);
            db.SaveChanges();

            return RedirectToAction("ListOfAppointments");
        }

        //List of Appointments
        [Authorize(Roles = "Patient")]
        public ActionResult ListOfAppointments()
        {
            string user = User.Identity.GetUserId();
            var patient = db.Patients.Single(c => c.ApplicationUserId == user);
           
            var appointment = db.Appointments.Where(c => c.PatientId == patient.Id)
                .Select(e => new AppointmentDto()
                {
                    AppointmentDate = db.Schedules.FirstOrDefault(d => d.PatientId == patient.Id).ScheduleDate,
                    Id = e.Id,
                    PatientName = e.Patient.FullName,
                    Problem = e.Problem,
                    StartTime = e.StartTime,
                    EndTime = e.EndTime,
                    PsychologistName = db.Psychologists.FirstOrDefault(d => d.Id == e.Schedule.PsychologistId).FullName,
                    Status = e.Status,
    
                })
                .ToList();
            string audiuserName = User.Identity.GetUserName();
            AuditExtension.AddAudit(audiuserName, "Read", "Appointments");
            return View(appointment);
        }

        //Edit Appointment
        [Authorize(Roles = "Patient")]
        public ActionResult EditAppointment(int id)
        {
            var collection = new AppointmentCollection
            {
                Appointment = db.Appointments.Single(c => c.Id == id),
                Psychologists = db.Psychologists.ToList()
            };
            return View(collection);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditAppointment(int id, AppointmentCollection model)
        {
            var collection = new AppointmentCollection
            {
                Appointment = model.Appointment,
                Psychologists = db.Psychologists.ToList()
            };

            if (model.Appointment.AppointmentDate <= DateTime.Now.Date)
            {
                ViewBag.Messege = "Please Enter the Date greater than today or equal!!";
                return View(collection);
            }

            var appointment = db.Appointments.Single(c => c.Id == id);
                appointment.Schedule.PsychologistId = model.Appointment.Schedule.PsychologistId;
                appointment.AppointmentDate = model.Appointment.AppointmentDate;
                appointment.Problem = model.Appointment.Problem;
                db.SaveChanges();
            string audiuserName = User.Identity.GetUserName();
            AuditExtension.AddAudit(audiuserName, "Update", "Appointments");
            return RedirectToAction("ListOfAppointments");
          
        }

        //Delete Appointment
        [Authorize(Roles = "Patient")]
        public ActionResult DeleteAppointment(int? id)
        {
            var appointment = db.Appointments.Single(c => c.Id == id);
            return View(appointment);
        }

        [HttpPost, ActionName("DeleteAppointment")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteAppointment(int id)
        {
            var appointment = db.Appointments.Single(c => c.Id == id);
            db.Appointments.Remove(appointment);
            db.SaveChanges();
            string audiuserName = User.Identity.GetUserName();
            AuditExtension.AddAudit(audiuserName, "Delete", "Appointments");
            return RedirectToAction("ListOfAppointments");
        }

        //End Appointment Section

        //Start Psychologist Section

        //List of Available Psychologists
        [Authorize(Roles = "Patient")]
        public ActionResult AvailablePsychologists()
        {
            var doctor = db.Psychologists.Include(c => c.Centre).Where(c => c.Status == "Active")
                 .Select(e => new PsychologistDto()
                 {
                     FullName = e.FullName,
                     FirstName = e.FirstName,
                     LastName = e.LastName,
                     Address = e.Address,
                     CentreName = db.Centre.FirstOrDefault(d => d.Id == e.Id).Name,
                     Status = e.Status,
                     ContactNo = e.ContactNo,
                     Education = e.Education,
                     Gender = e.Gender,
                     Id = e.Id
                     
                 }).ToList();
            string audiuserName = User.Identity.GetUserName();
            AuditExtension.AddAudit(audiuserName, "Read", "Psychologists");
            return View(doctor);
        }

        //Show Psychologist Schedule
        [Authorize(Roles = "Patient")]
        public ActionResult PsychologistSchedule(int id)
        {
          
            var schedule = db.Schedules.Where(c => c.PsychologistId == id && c.IsBooked == false)
                .Select(e => new SchedulesDto()
                {
                    PsychologistName = db.Psychologists.FirstOrDefault(d => d.Id == e.PsychologistId).FullName,
                    CentreName = db.Centre.FirstOrDefault(d => d.Id == e.Id).Name,
                    EndTime = e.EndTime,
                    StartTime = e.StartTime,
                    ScheduleDate = e.ScheduleDate,
                    Id = e.Id,
                }).ToList();
            string audiuserName = User.Identity.GetUserName();
            AuditExtension.AddAudit(audiuserName, "Read", "Schedules");
            return View(schedule);
        }


        //Psychologist Detail
        [Authorize(Roles = "Patient")]
        public ActionResult PsychologistDetail(int id)
        {
            var doctor = db.Psychologists.Include(c => c.Centre).Single(c => c.Id == id);
            string audiuserName = User.Identity.GetUserName();
            AuditExtension.AddAudit(audiuserName, "Read", "Psychologists");
            return View(doctor);
        }

        //End Psychologist Section



        [Authorize(Roles = "Patient")]
        public ActionResult ListOfSchedules()
        {
            var schedule = db.Schedules.Include(c => c.Psychologist)
                .Select(e => new SchedulesDto()
                {
                    PsychologistName = e.PsychologistName,
                    // CentreName = db.Centre.FirstOrDefault(d => d.Id == e.DepartmentId).Name,
                    EndTime = e.EndTime,
                    StartTime = e.StartTime,
                    ScheduleDate = e.ScheduleDate,
                    


                    CentreName = e.CentreName,
                    Id = e.Id,


                }).ToList();
            string audiuserName = User.Identity.GetUserName();
            AuditExtension.AddAudit(audiuserName, "Read", "Schedules");
            return View(schedule);
        }


        //Start Complaint Section

        [Authorize(Roles = "Patient")]
        public ActionResult AddComplain()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddComplain(Complaint model)
        {
            var complain = new Complaint();
            complain.Complain = model.Complain;
            complain.ComplainDate = DateTime.Now.Date;
            db.Complaints.Add(complain);
            db.SaveChanges();
            string audiuserName = User.Identity.GetUserName();
            AuditExtension.AddAudit(audiuserName, "Create", "Complaints");
            return RedirectToAction("ListOfComplains");
        }

        [Authorize(Roles = "Patient")]
        public ActionResult ListOfComplains()
        {
            var complain = db.Complaints.ToList();
            string audiuserName = User.Identity.GetUserName();
            AuditExtension.AddAudit(audiuserName, "Read", "Complaints");
            return View(complain);
        }

        [Authorize(Roles = "Patient")]
        public ActionResult EditComplain(int id)
        {
            var complain = db.Complaints.Single(c => c.Id == id);
            return View(complain);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditComplain(int id, Complaint model)
        {
            var complain = db.Complaints.Single(c => c.Id == id);
            complain.Complain = model.Complain;
            db.SaveChanges();
            string audiuserName = User.Identity.GetUserName();
            AuditExtension.AddAudit(audiuserName, "Update", "Complaints");
            return RedirectToAction("ListOfComplains");
        }

        [Authorize(Roles = "Patient")]
        public ActionResult DeleteComplain()
        {
            return View();
        }

        [HttpPost, ActionName("DeleteComplain")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteComplain(int id)
        {
            var complain = db.Complaints.Single(c => c.Id == id);
            db.Complaints.Remove(complain);
            db.SaveChanges();
            string audiuserName = User.Identity.GetUserName();
            AuditExtension.AddAudit(audiuserName, "Delete", "Complaints");
            return RedirectToAction("ListOfComplains");
        }

        //End Complain Section

        //Start Prescription Section

        //List of Prescription
        [Authorize(Roles = "Patient")]
        public ActionResult ListOfPrescription()
        {
            string user = User.Identity.GetUserId();
            var patient = db.Patients.Single(c => c.ApplicationUserId == user);
            var prescription = db.Prescription.Include(c => c.Psychologist).Where(c => c.PatientId == patient.Id).ToList();
            return View(prescription);
        }

        //Prescription View
        public ActionResult PrescriptionView(int id)
        {
            var prescription = db.Prescription.Single(c => c.Id == id);
            return View(prescription);
        }

        //End Prescription 


        //List Of Schedules
        [Authorize(Roles = "Patient")]
        public ActionResult ListOfConsultation()
        {
            string user = User.Identity.GetUserId();
            var petient = db.Patients.Single(c => c.ApplicationUserId == user);
            var consultations = db.Consultations.Where(c => c.PatientId == petient.Id)
                .Select(e => new ConsultationDto()
                {
                    PatientName = db.Patients.FirstOrDefault(d => d.Id == e.Id).FullName,
                    PsychologistName = db.Psychologists.FirstOrDefault(d => d.Id == e.PsychologistId).FullName,
                    ConsultationDate = e.ConsultationDate,
                    Diagnosis = e.Diagnosis,
                    TreatmentPlan = e.TreatmentPlan,
                    Id = e.Id,
                }).ToList();
            string audiuserName = User.Identity.GetUserName();
            AuditExtension.AddAudit(audiuserName, "Read", "Consultations");
            return View(consultations);
        }
    }
}