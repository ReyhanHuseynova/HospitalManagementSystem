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
    public class OPDController : Controller
    {
        private readonly AppDbContext _db;
        public OPDController(AppDbContext db)
        {
            _db= db;
        }
        public async Task<IActionResult> Index()
        {
            List<OPD> oPDs = await _db.OPDs.Include(x=>x.RoomBed).Include(x=>x.RoomBed.RoomMaster).Include(x=>x.Doctor).ToListAsync();
            return View(oPDs);
        }

        #region PatientsOPDCreate
        public async Task<IActionResult> Create()
        {
           
            ViewBag.RoomBed = await _db.RoomBeds.Where(x => x.IsMain == false ).ToListAsync();
            ViewBag.Doctor = await _db.Doctors.ToListAsync();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Create(OPD oPD, int roomId, int doctorId)
        {
            ViewBag.RoomBed = await _db.RoomBeds.Where(x => x.IsMain == false).ToListAsync();
            ViewBag.Doctor = await _db.Doctors.ToListAsync();

            if (!ModelState.IsValid)
            {
                return View();
            }
            oPD.RoomBedId= roomId;
            oPD.DoctorId= doctorId;
             await _db.OPDs.AddAsync(oPD);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        #endregion

        #region PatientsOPDUpdate
        public async Task<IActionResult> Update(int? id)
        {

            ViewBag.RoomBed = await _db.RoomBeds.Where(x => x.IsMain == false).ToListAsync();
            ViewBag.Doctor = await _db.Doctors.ToListAsync();

            if (id == null)
            {
                return NotFound();
            }
            OPD dbOPD = await _db.OPDs.FirstOrDefaultAsync(x => x.Id == id);
            if (dbOPD == null)
            {
                return BadRequest();
            }
            

            return View(dbOPD);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int? id, OPD opd, int doctorId, int roomId)
        {
            ViewBag.RoomBed = await _db.RoomBeds.Where(x => x.IsMain == false).ToListAsync();
            ViewBag.Doctor = await _db.Doctors.ToListAsync();

            if (id == null)
            {
                return NotFound();
            }
            OPD dbOPD = await _db.OPDs.FirstOrDefaultAsync(x => x.Id == id);
            if (dbOPD == null)
            {
                return BadRequest();
            }
           
            if (!ModelState.IsValid)
            {
                return View(dbOPD);
            }

            dbOPD.RoomBedId = roomId;
            dbOPD.DoctorId = doctorId;
            dbOPD.PatientCode = opd.PatientCode;
            dbOPD.PatientFullName = opd.PatientFullName;
            dbOPD.DateAdmit = opd.DateAdmit;
            dbOPD.DateDischarge = opd.DateDischarge;
           
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        #endregion

        #region PatientsChange
        public async Task<IActionResult> Change(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            OPD dbOPD = await _db.OPDs.Include(x=>x.RoomBed).FirstOrDefaultAsync(x => x.Id == id);
            if (dbOPD == null)
            {
                return BadRequest();
            }
            if (dbOPD.RoomBed.IsMain == true)
            {
                dbOPD.RoomBed.IsMain = false;
            }
            else
            {
                dbOPD.RoomBed.IsMain = true;
            }
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        #endregion
    }
}
