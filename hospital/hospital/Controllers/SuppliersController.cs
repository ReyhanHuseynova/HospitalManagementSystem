using DocumentFormat.OpenXml.Office2010.ExcelAc;
using hospital.DAL;
using hospital.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace hospital.Controllers
{
    [Authorize(Roles = "Admin")]
    public class SuppliersController : Controller
    {
        private readonly AppDbContext _db;
        public SuppliersController(AppDbContext db)
        {
            _db = db;
        }

        public async Task<IActionResult> Index()
        {
            List<Supplier> suppliers = await _db.Suppliers.ToListAsync();
            return View(suppliers);
        }

        #region SupplierCreate
        public IActionResult Create()
        {
            
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Create(Supplier supplier )
        {

            if (!ModelState.IsValid)
            {
                return View();
            }
            await _db.Suppliers.AddAsync(supplier);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        #endregion

        #region SupplierUpdate
        public async Task<IActionResult> Update(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Supplier dbSupplier = await _db.Suppliers.FirstOrDefaultAsync(x => x.Id == id);
            if (dbSupplier == null)
            {
                return BadRequest();
            }

            return View(dbSupplier);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int? id, Supplier supplier)
        {
            if (id == null)
            {
                return NotFound();
            }
            Supplier dbSupplier = await _db.Suppliers.FirstOrDefaultAsync(x => x.Id == id);
            if (dbSupplier == null)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return View(dbSupplier);
            }

          
            dbSupplier.Name = supplier.Name;
            dbSupplier.Surname = supplier.Surname;
            dbSupplier.Address = supplier.Address;
            dbSupplier.Phone = supplier.Phone;
            dbSupplier.Email = supplier.Email;
           
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        #endregion

        #region SupplierActivity
        public async Task<IActionResult> Activity(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Supplier dbSupplier = await _db.Suppliers.FirstOrDefaultAsync(x => x.Id == id);
            if (dbSupplier == null)
            {
                return BadRequest();
            }
            if (dbSupplier.IsDeactive == true)
            {
                dbSupplier.IsDeactive = false;
            }
            else
            {
                dbSupplier.IsDeactive = true;
            }
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        #endregion
    }
}
