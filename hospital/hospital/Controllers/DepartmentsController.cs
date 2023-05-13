
using ClosedXML.Excel;
using hospital.DAL;
using hospital.Helpers;
using hospital.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace hospital.Controllers
{
    [Authorize(Roles = "Admin")]
    public class DepartmentsController : Controller
    {
        private readonly AppDbContext _db;
        private readonly IWebHostEnvironment _env;
        public DepartmentsController(AppDbContext db, IWebHostEnvironment env)

        {
            _db = db;
            _env = env;
        }


        public async Task<IActionResult> Index()
        {
            List<Department> departments = await _db.Departments.ToListAsync();

            return View(departments);
        }

        #region DepartmentCreate
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Create(Department department)
        {

            if (!ModelState.IsValid)
            {
                return View();
            }
            bool isExist = await _db.Departments.AnyAsync(x => x.Name == department.Name);
            if (isExist)
            {
                ModelState.AddModelError("Name", "This department is already exist");
                return View();

            }

            if (department.Photo == null)
            {
                ModelState.AddModelError("Photo", "Shekil sech");
                return View();
            }
            if (!department.Photo.IsImage())
            {
                ModelState.AddModelError("Photo", "Shekil formati sech");
                return View();
            }
            if (department.Photo.IsOlder2Mb())
            {
                ModelState.AddModelError("Photo", "Max 2Mb");
                return View();
            }
            string folder = Path.Combine(_env.WebRootPath, "images");
            department.Image = await department.Photo.SaveImageAsync(folder);
            await _db.Departments.AddAsync(department);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        #endregion

        #region DepartmentUpdate
        public async Task<IActionResult> Update(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Department dbDepartment = await _db.Departments.FirstOrDefaultAsync(x => x.Id == id);
            if (dbDepartment == null)
            {
                return BadRequest();
            }
            return View(dbDepartment);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int? id, Department department)
        {
            if (id == null)
            {
                return NotFound();
            }
            Department dbDepartment = await _db.Departments.FirstOrDefaultAsync(x => x.Id == id);
            if (dbDepartment == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return View();
            }

            bool isExist = await _db.Departments.AnyAsync(x => x.Name == department.Name && x.Id != id);
            if (isExist)
            {
                ModelState.AddModelError("Name", "This department is already exist");
                return View();

            }
            if (department.Photo != null)
            {
                if (!department.Photo.IsImage())
                {
                    ModelState.AddModelError("Photo", "Shekil formati sech");
                    return View(dbDepartment);
                }
                if (department.Photo.IsOlder2Mb())
                {
                    ModelState.AddModelError("Photo", "Max 2Mb");
                    return View(dbDepartment);
                }
                string folder = Path.Combine(_env.WebRootPath, "images");
                string path = Path.Combine(folder, dbDepartment.Image);
                if (System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                }
                dbDepartment.Image = await department.Photo.SaveImageAsync(folder);
            }
            dbDepartment.Name = department.Name;
            dbDepartment.Photo = department.Photo;
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        #endregion

        #region DepartmentActivity
        public async Task<IActionResult> Activity(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Department dbDepartment = await _db.Departments.FirstOrDefaultAsync(x => x.Id == id);
            if (dbDepartment == null)
            {
                return BadRequest();
            }
            if (dbDepartment.IsDeactive == true)
            {
                dbDepartment.IsDeactive = false;
            }
            else
            {
                dbDepartment.IsDeactive = true;
            }
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        #endregion



    }
}
