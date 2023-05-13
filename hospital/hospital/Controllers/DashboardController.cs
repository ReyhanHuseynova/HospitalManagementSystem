using DocumentFormat.OpenXml.Office2010.ExcelAc;
using DocumentFormat.OpenXml.Wordprocessing;
using hospital.DAL;
using hospital.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace hospital.Controllers
{
    
    [Authorize(Roles = "Admin")]
    public class DashboardController : Controller
    {
        private readonly AppDbContext _db;
        public DashboardController(AppDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            ViewBag.Patient = _db.Patients.Count();
            ViewBag.Doctor = _db.Doctors.Count();
            ViewBag.Room = _db.RoomMasters.Count();
            ViewBag.Department = _db.Departments.Count();
            ViewBag.Tablet = _db.Products.Where(x => x.ItemCategoryId == 6).Count();
            ViewBag.Cream = _db.Products.Where(x => x.ItemCategoryId == 7).Count();
            ViewBag.Equ = _db.Products.Where(x => x.ItemCategoryId == 8).Count();
            ViewBag.Uni = _db.Products.Where(x => x.ItemCategoryId == 9).Count();
            return View();
        }


    }
}
