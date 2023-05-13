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
    public class ItemCategoriesController : Controller
    {
        private readonly AppDbContext _db;
        public ItemCategoriesController(AppDbContext db)
        {
            _db = db;
        }
        public async Task<IActionResult> Index()
        {
            List<ItemCategory> itemcategories = await _db.ItemCategories.ToListAsync();
            return View(itemcategories);
        }

        #region CategoryCreate
        public IActionResult Create()

        {
          
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ItemCategory ıtemCategory)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            bool isExist = await _db.ItemCategories.AnyAsync(x => x.Name == ıtemCategory.Name);
            if (isExist)
            {
                ModelState.AddModelError("Name", "This category is already exist");
                return View();

            }
          
            await _db.ItemCategories.AddAsync(ıtemCategory);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        #endregion

        #region CategoryUpdate
        public async Task<IActionResult> Update(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            ItemCategory dbItemCategory = await _db.ItemCategories.FirstOrDefaultAsync(x => x.Id == id);
            if (dbItemCategory == null)
            {
                return BadRequest();
            }
            return View(dbItemCategory);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int? id, ItemCategory itemCategory)
        {
            if (id == null)
            {
                return NotFound();
            }
            ItemCategory dbItemCategory = await _db.ItemCategories.FirstOrDefaultAsync(x => x.Id == id);
            if (dbItemCategory == null)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return View();
            }
            bool isExist = await _db.RoomCategories.AnyAsync(x => x.CategoryName == itemCategory.Name && x.Id != id);
            if (isExist)
            {
                ModelState.AddModelError("Name", "This category is already exist");
                return View();

            }
            dbItemCategory.Name = itemCategory.Name;
            dbItemCategory.Description = itemCategory.Description;
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        #endregion

        #region CategoryActivity
        public async Task<IActionResult> Activity(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            ItemCategory dbItemCategory = await _db.ItemCategories.FirstOrDefaultAsync(x => x.Id == id);
            if (dbItemCategory == null)
            {
                return BadRequest();
            }
            if (dbItemCategory.IsDeactive == true)
            {
                dbItemCategory.IsDeactive = false;
            }
            else
            {
                dbItemCategory.IsDeactive = true;
            }
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        #endregion
    }
}
