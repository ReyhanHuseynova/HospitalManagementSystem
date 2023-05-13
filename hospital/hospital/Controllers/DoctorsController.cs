using hospital.DAL;
using hospital.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace hospital.Controllers
{
    [Authorize(Roles = "Admin")]
    public class DoctorsController : Controller
    {
        private readonly AppDbContext _db;
        public DoctorsController(AppDbContext db)

        {
            _db = db;
        }
        public async Task<IActionResult> Index()
        {
           
            List<Doctor> doctors = await _db.Doctors.Include(x => x.Department).ToListAsync();

            return View(doctors);
        }

        #region DoctorsCreate
        public async Task<IActionResult> Create()
        {
            ViewBag.Departments = await _db.Departments.ToListAsync();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Create(Doctor doctor, int depId)
        {
            ViewBag.Departments = await _db.Departments.ToListAsync();

            if (!ModelState.IsValid)
            {
                return View();
            }

            doctor.DepartmentId = depId;
            await _db.Doctors.AddAsync(doctor);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        #endregion

        #region DoctorsUpdate
        public async Task<IActionResult> Update(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Doctor dbDoctor = await _db.Doctors.Include(x => x.Department).FirstOrDefaultAsync(x => x.Id == id);
            if (dbDoctor == null)
            {
                return BadRequest();
            }
            ViewBag.Departments = await _db.Departments.ToListAsync();
            return View(dbDoctor);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int? id, Doctor doctor, int depId)
        {
            if (id == null)
            {
                return NotFound();
            }
            Doctor dbDoctor = await _db.Doctors.Include(x => x.Department).FirstOrDefaultAsync(x => x.Id == id);
            if (dbDoctor == null)
            {
                return BadRequest();
            }
            ViewBag.Departments = await _db.Departments.ToListAsync();

            if (!ModelState.IsValid)
            {
                return View(dbDoctor);
            }


            dbDoctor.DepartmentId = depId;
            dbDoctor.Name = doctor.Name;
            dbDoctor.Surname = doctor.Surname;
            dbDoctor.Age = doctor.Age;
            dbDoctor.Number = doctor.Number;


            await _db.SaveChangesAsync();

            return RedirectToAction("Index");
        }
        #endregion

        #region DoctorsActivity
        public async Task<IActionResult> Activity(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Doctor dbDoctor = await _db.Doctors.FirstOrDefaultAsync(x => x.Id == id);
            if (dbDoctor == null)
            {
                return BadRequest();
            }
            if (dbDoctor.IsDeactive == true)
            {
                dbDoctor.IsDeactive = false;
            }
            else
            {
                dbDoctor.IsDeactive = true;
            }
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        #endregion

    }
}
