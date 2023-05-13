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
    public class RoomMasterController : Controller
    {
        private readonly AppDbContext _db;
        public RoomMasterController(AppDbContext db)
        {
            _db = db;
        }

        public async Task<IActionResult> Index()
        {
            List<RoomMaster> roomMasters = await _db.RoomMasters.Include(x=>x.RoomCategory).Include(x=>x.Floor).ToListAsync();
            return View(roomMasters);
        }

        #region RoomMasterCreate
        public async Task<IActionResult> Create()
        {
            ViewBag.Categories = await _db.RoomCategories.ToListAsync();
            ViewBag.Floors = await _db.Floors.ToListAsync();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RoomMaster roomMaster, int roomId, int floorId)
        {
            ViewBag.Categories = await _db.RoomCategories.ToListAsync();
            ViewBag.Floors = await _db.Floors.ToListAsync();
            if (!ModelState.IsValid)
            {
                return View();
            }
            bool isExist = await _db.RoomMasters.AnyAsync(x => x.RoomNumber == roomMaster.RoomNumber);
            if (isExist)
            {
                ModelState.AddModelError("RoomNumber", "This room is already exist");
                return View();

            }
            roomMaster.RoomCategoryId = roomId;
            roomMaster.FloorId = floorId;
            await _db.RoomMasters.AddAsync(roomMaster);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        #endregion

        #region RoomMasterUpdate
        public async Task<IActionResult> Update(int? id)
        {
            ViewBag.Categories = await _db.RoomCategories.ToListAsync();
            ViewBag.Floors = await _db.Floors.ToListAsync();
            if (id == null)
            {
                return NotFound();
            }
            RoomMaster dbRoomMaster = await _db.RoomMasters.FirstOrDefaultAsync(x => x.Id == id);
            if (dbRoomMaster == null)
            {
                return BadRequest();
            }

            return View(dbRoomMaster);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int? id, RoomMaster roomMaster, int roomId, int floorId)
        {
            ViewBag.Categories = await _db.RoomCategories.ToListAsync();
            ViewBag.Floors = await _db.Floors.ToListAsync();
            if (id == null)
            {
                return NotFound();
            }
            RoomMaster dbRoomMaster = await _db.RoomMasters.FirstOrDefaultAsync(x => x.Id == id);
            if (dbRoomMaster == null)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return View(dbRoomMaster);
            }

            bool isExist = await _db.RoomMasters.AnyAsync(x => x.RoomNumber == roomMaster.RoomNumber && x.Id != id);
            if (isExist)
            {
                ModelState.AddModelError("RoomNumber", "This room is already exist");
                return View(dbRoomMaster);

            }
            dbRoomMaster.RoomCategoryId = roomId;
            dbRoomMaster.FloorId = floorId;
            dbRoomMaster.RoomNumber = roomMaster.RoomNumber;
            dbRoomMaster.Rate = roomMaster.Rate;
            dbRoomMaster.TotalBed = roomMaster.TotalBed;
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        #endregion

        #region RoomMasterActivity
        public async Task<IActionResult> Activity(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            RoomMaster dbRoomMaster = await _db.RoomMasters.FirstOrDefaultAsync(x => x.Id == id);
            if (dbRoomMaster == null)
            {
                return BadRequest();
            }
            if (dbRoomMaster.IsDeactive == true)
            {
                dbRoomMaster.IsDeactive = false;
            }
            else
            {
                dbRoomMaster.IsDeactive = true;
            }
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        #endregion






    }
}
