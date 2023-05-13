using DocumentFormat.OpenXml.Drawing.Charts;
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
    public class PatientsController : Controller
    {
        private readonly AppDbContext _db;
        public PatientsController(AppDbContext db)
        {
            _db = db;
        }
        public async Task<IActionResult> Index()
        {
            List<Patient> patients = await _db.Patients.Include(x=>x.BloodGroup).ToListAsync();

            return View(patients);
        }

        #region PatientsCreate
        public async Task<IActionResult> Create()
        {
            ViewBag.BloodGroups = await _db.BloodGroups.ToListAsync();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Create(Patient patient, int bloodId)
        {
            ViewBag.BloodGroups = await _db.BloodGroups.ToListAsync();

            if (!ModelState.IsValid)
            {
                return View();
            }
            patient.BloodGroupId = bloodId;
            await _db.Patients.AddAsync(patient);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        #endregion

        #region PatientsUpdate
        public async Task<IActionResult> Update(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Patient dbPatient = await _db.Patients.FirstOrDefaultAsync(x => x.Id == id);
            if (dbPatient == null)
            {
                return BadRequest();
            }
            ViewBag.BloodGroups = await _db.BloodGroups.ToListAsync();

            return View(dbPatient);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int? id, Patient patient, int bloodId)
        {
            if (id == null)
            {
                return NotFound();
            }
            Patient dbPatient = await _db.Patients.FirstOrDefaultAsync(x => x.Id == id);
            if (dbPatient == null)
            {
                return BadRequest();
            }
            ViewBag.BloodGroups = await _db.BloodGroups.ToListAsync();
            if (!ModelState.IsValid)
            {
                return View(dbPatient);
            }

            dbPatient.BloodGroupId = bloodId;
            dbPatient.Name = patient.Name;
            dbPatient.Surname = patient.Surname;
            dbPatient.FatherName = patient.FatherName;
            dbPatient.Age = patient.Age;
            dbPatient.Address = patient.Address;
            dbPatient.Number = patient.Number;
            dbPatient.Gender = patient.Gender;
            dbPatient.Birthday = patient.Birthday;
            dbPatient.RegDate = patient.RegDate;
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        #endregion

        #region PatientsActivity
        public async Task<IActionResult> Activity(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Patient dbPatient = await _db.Patients.FirstOrDefaultAsync(x => x.Id == id);
            if (dbPatient == null)
            {
                return BadRequest();
            }
            if (dbPatient.IsDeactive == true)
            {
                dbPatient.IsDeactive = false;
            }
            else
            {
                dbPatient.IsDeactive = true;
            }
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        #endregion

        #region PatientsDetail
        public async Task<IActionResult> Detail(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Patient patient = await _db.Patients.Include(x=>x.BloodGroup).FirstOrDefaultAsync(x => x.Id == id);
            if (patient == null)
            {
                return BadRequest();
            }

            return View(patient);
        }
        #endregion

    }
}
