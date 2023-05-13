
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
    public class RoomCategoriesController : Controller
    {
        private readonly AppDbContext _db;
        public RoomCategoriesController (AppDbContext db)
        {
            _db = db;
        }
        public async Task<IActionResult> Index()
        {
            List<RoomCategory> roomCategories = await _db.RoomCategories.ToListAsync();
            return View(roomCategories);
        }

        #region CategoryCreate
        public IActionResult Create()

        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RoomCategory roomCategory)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            bool isExist = await _db.RoomCategories.AnyAsync(x => x.CategoryName == roomCategory.CategoryName);
            if (isExist)
            {
                ModelState.AddModelError("CategoryName", "This category is already exist");
                return View();

            }
            await _db.RoomCategories.AddAsync(roomCategory);
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
            RoomCategory dbRoomCategory = await _db.RoomCategories.FirstOrDefaultAsync(x => x.Id == id);
            if (dbRoomCategory == null)
            {
                return BadRequest();
            }
            return View(dbRoomCategory);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int? id, RoomCategory roomCategory)
        {
            if (id == null)
            {
                return NotFound();
            }
            RoomCategory dbRoomCategory = await _db.RoomCategories.FirstOrDefaultAsync(x => x.Id == id);
            if (dbRoomCategory == null)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return View();
            }
            bool isExist = await _db.RoomCategories.AnyAsync(x => x.CategoryName == roomCategory.CategoryName && x.Id != id);
            if (isExist)
            {
                ModelState.AddModelError("CategoryName", "This category is already exist");
                return View();

            }
            dbRoomCategory.CategoryName = roomCategory.CategoryName;
            dbRoomCategory.Description = roomCategory.Description;
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
            RoomCategory dbRoomCategory = await _db.RoomCategories.FirstOrDefaultAsync(x => x.Id == id);
            if (dbRoomCategory == null)
            {
                return BadRequest();
            }
            if (dbRoomCategory.IsDeactive == true)
            {
                dbRoomCategory.IsDeactive = false;
            }
            else
            {
                dbRoomCategory.IsDeactive = true;
            }
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        #endregion
    }
}
