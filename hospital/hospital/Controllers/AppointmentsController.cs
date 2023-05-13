using DocumentFormat.OpenXml.Office2010.Excel;
using hospital.DAL;
using hospital.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace hospital.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AppointmentsController : Controller
    {
        private readonly AppDbContext _db;
        public AppointmentsController(AppDbContext db)
        {
            _db= db;
        }
        public async Task<IActionResult> Index()
        {
            List<AppDetail> appDetails = await _db.AppDetails.Include(x=>x.Appointment).Include(x => x.Doctor).Include(x=>x.Patient).ToListAsync();
            
            return View(appDetails);
        }

        #region AddAppointment
        public async Task<IActionResult> AddAppointment()
        {
            ViewBag.Patients = await _db.Patients.ToListAsync();
            return View();
        }
        #endregion

        #region AppointmentCreate
        public async Task<IActionResult> Create()
        {
            ViewBag.Patients = await _db.Patients.ToListAsync();
            ViewBag.Doctors = await _db.Doctors.ToListAsync();
           
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Create(AppDetail appDetail, int docId, int patId)
        {
            ViewBag.Patients = await _db.Patients.ToListAsync();
            ViewBag.Doctors = await _db.Doctors.ToListAsync();
           


            if (!ModelState.IsValid)
            {
                return View();
            }
            appDetail.DoctorId = docId;
            appDetail.PatientId = patId;
            await _db.AppDetails.AddAsync(appDetail);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        #endregion

        #region AppointmentUpdate
        public async Task<IActionResult> Update(int? id)
        {
            ViewBag.Patients = await _db.Patients.ToListAsync();
            ViewBag.Doctors = await _db.Doctors.ToListAsync();
            if (id == null)
            {
                return NotFound();
            }
            AppDetail dbappDetail = await _db.AppDetails.Include(x => x.Appointment).FirstOrDefaultAsync(x => x.Id == id);
            if (dbappDetail == null)
            {
                return BadRequest();
            }
            return View(dbappDetail);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int? id, AppDetail appDetail, int docId, int patId)
        {
            ViewBag.Patients = await _db.Patients.ToListAsync();
            ViewBag.Doctors = await _db.Doctors.ToListAsync();
            if (id == null)
            {
                return NotFound();
            }
            AppDetail dbappDetail = await _db.AppDetails.Include(x => x.Appointment).FirstOrDefaultAsync(x => x.Id == id);
            if (dbappDetail == null)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return View(dbappDetail);
            }
            dbappDetail.PatientId = patId;
            dbappDetail.DoctorId = docId;
            dbappDetail.Appointment.AppointmentDate = appDetail.Appointment.AppointmentDate;
            dbappDetail.Appointment.DateVisit = appDetail.Appointment.DateVisit;
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        #endregion

        #region AppointmentActivity
        public async Task<IActionResult> Activity(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Appointment dbappointment = await _db.Appointments.FirstOrDefaultAsync(x => x.Id == id);
            if (dbappointment == null)
            {
                return BadRequest();
            }
            if (dbappointment.IsDeactive == true)
            {
                dbappointment.IsDeactive = false;
            }
            else
            {
                dbappointment.IsDeactive = true;
            }
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        #endregion

        #region AppointmentCheckIn
        public async Task<IActionResult> CheckIn(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Appointment dbappointment = await _db.Appointments.FirstOrDefaultAsync(x => x.Id == id);
            if (dbappointment == null)
            {
                return BadRequest();
            }
            if (dbappointment.Status == true)
            {
                dbappointment.Status = false;
            }
            else
            {
                dbappointment.Status = true;
            }
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        #endregion
    }
}
