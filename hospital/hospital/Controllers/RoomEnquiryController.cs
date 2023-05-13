using hospital.DAL;
using hospital.Models;
using hospital.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace hospital.Controllers
{
    [Authorize(Roles = "Admin")]
    public class RoomEnquiryController : Controller
    {
        private readonly AppDbContext _db;
        public RoomEnquiryController(AppDbContext db)
        {
            _db = db;
        }

        public async Task<IActionResult> Index()
        {
            RoomVM roomVM = new RoomVM
            {
                RoomMasters= await _db.RoomMasters.Include(x=>x.Floor).Include(x=>x.RoomCategory).Include(x=>x.RoomBeds).ToListAsync(),
                RoomBeds= await _db.RoomBeds.ToListAsync(),
                RoomCategories= await _db.RoomCategories.ToListAsync(),
                OPDs= await _db.OPDs.ToListAsync(),
                AppDetails= await _db.AppDetails.Include(x=>x.Doctor).ToListAsync(),
                
            };


            return View(roomVM);
        }

     
    }
}
