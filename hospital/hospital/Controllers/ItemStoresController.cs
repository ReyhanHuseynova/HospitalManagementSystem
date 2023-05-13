using DocumentFormat.OpenXml.Office2010.ExcelAc;
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
    public class ItemStoresController : Controller
    {
        private readonly AppDbContext _db;
        public ItemStoresController(AppDbContext db)
        {
            _db = db;
        }
    
        public async Task<IActionResult> Index()
        {
            List<ItemStore> itemstores = await _db.ItemStores.ToListAsync();
            return View(itemstores);
        }

        #region ItemStoreCreate
        public  IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Create(ItemStore store)
        {
            

            if (!ModelState.IsValid)
            {
                return View();
            }
            bool isExist = await _db.ItemStores.AnyAsync(x => x.StoreName == store.StoreName);
            if (isExist)
            {
                ModelState.AddModelError("StoreName", "This name is already exist");
                return View();

            }

            await _db.ItemStores.AddAsync(store);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        #endregion

        #region ItemStoreUpdate
        public async Task<IActionResult> Update(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            ItemStore dbItemStore = await _db.ItemStores.FirstOrDefaultAsync(x => x.Id == id);
            if (dbItemStore == null)
            {
                return BadRequest();
            }
           

            return View(dbItemStore);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int? id, ItemStore store)
        {
            if (id == null)
            {
                return NotFound();
            }
            ItemStore dbItemStore = await _db.ItemStores.FirstOrDefaultAsync(x => x.Id == id);
            if (dbItemStore == null)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return View(dbItemStore);
            }
            bool isExist = await _db.ItemStores.AnyAsync(x => x.StoreName == store.StoreName && x.Id != id);
            if (isExist)
            {
                ModelState.AddModelError("StoreName", "This name is already exist");
                return View(dbItemStore);

            }


            dbItemStore.StoreName = store.StoreName;
            dbItemStore.StockCode = store.StockCode;
            dbItemStore.Description = store.Description;
           
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        #endregion

        #region ItemStoreActivity
        public async Task<IActionResult> Activity(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            ItemStore dbItemStore = await _db.ItemStores.FirstOrDefaultAsync(x => x.Id == id);
            if (dbItemStore == null)
            {
                return BadRequest();
            }
            if (dbItemStore.IsDeactive == true)
            {
                dbItemStore.IsDeactive = false;
            }
            else
            {
                dbItemStore.IsDeactive = true;
            }
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        #endregion

    }
}
