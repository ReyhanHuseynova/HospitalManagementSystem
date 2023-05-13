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
    public class RoomBedsController : Controller
    {
        private readonly AppDbContext _db;
        public RoomBedsController(AppDbContext db)
        {
            _db = db;
        }

        public async Task<IActionResult> Index()
        {
            List<RoomBed> roomBeds = await _db.RoomBeds.Include(x=>x.RoomMaster).ThenInclude(x=>x.Floor).ToListAsync();
            ViewBag.Floors = await _db.Floors.ToListAsync();
            ViewBag.Categories = await _db.RoomCategories.ToListAsync();

            return View(roomBeds);
        }

        #region RoomBedsCreate
        public async Task<IActionResult> Create()
        {
            ViewBag.RoomMasters = await _db.RoomMasters.ToListAsync();

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Create(RoomBed roomBed, int roomId)
        {
            ViewBag.RoomMasters = await _db.RoomMasters.ToListAsync();


            if (!ModelState.IsValid)
            {
                return View();
            }
            bool isExist = await _db.RoomBeds.AnyAsync(x => x.BedNumber == roomBed.BedNumber);
            if (isExist)
            {
                ModelState.AddModelError("BedNumber", "This bed number is already exist");
                return View();

            }
            roomBed.RoomMasterId = roomId;
            await _db.RoomBeds.AddAsync(roomBed);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        #endregion

        #region RoomBedsUpdate
        public async Task<IActionResult> Update(int? id)
        {
            ViewBag.RoomMasters = await _db.RoomMasters.ToListAsync();
            if (id == null)
            {
                return NotFound();
            }
            RoomBed dbRoomBed = await _db.RoomBeds.FirstOrDefaultAsync(x => x.Id == id);
            if (dbRoomBed == null)
            {
                return BadRequest();
            }

            return View(dbRoomBed);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int? id, RoomBed roomBed, int roomId)
        {
            ViewBag.RoomMasters = await _db.RoomMasters.ToListAsync();

            if (id == null)
            {
                return NotFound();
            }
            RoomBed dbRoomBed = await _db.RoomBeds.FirstOrDefaultAsync(x => x.Id == id);
            if (dbRoomBed == null)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return View(dbRoomBed);
            }
            bool isExist = await _db.RoomBeds.AnyAsync(x => x.BedNumber == roomBed.BedNumber && x.Id != id);
            if (isExist)
            {
                ModelState.AddModelError("BedNumber", "This bed is already exist");
                return View(dbRoomBed);

            }

            dbRoomBed.RoomMasterId = roomId;
            dbRoomBed.BedNumber = roomBed.BedNumber;
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        #endregion

        #region RoomBedsActivity
        public async Task<IActionResult> Activity(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            RoomBed dbRoomBed = await _db.RoomBeds.FirstOrDefaultAsync(x => x.Id == id);
            if (dbRoomBed == null)
            {
                return BadRequest();
            }
            if (dbRoomBed.IsDeactive == true)
            {
                dbRoomBed.IsDeactive = false;
            }
            else
            {
                dbRoomBed.IsDeactive = true;
            }
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        #endregion
    }
}
