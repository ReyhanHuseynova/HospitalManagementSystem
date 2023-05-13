using hospital.DAL;
using hospital.Models;
using hospital.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace hospital.Controllers
{
    [Authorize(Roles = "Admin")]
    public class InventoryController : Controller
    {
        private readonly AppDbContext _db;
        public InventoryController(AppDbContext db)
        {
            _db = db;
        }
        public async Task<IActionResult> Index()
        {
            List<Inventory> ınventories = await _db.Inventories.Include(x=>x.Product).
            ThenInclude(x=>x.ItemCategory).Include(x=>x.ItemStore).Include(x=>x.Supplier).ToListAsync();
            
            return View(ınventories);
        }

        #region InventoryCreate
       
        public async Task<IActionResult> Create()
        {
         
            ViewBag.Products = await _db.Products.Include(x=>x.ItemCategory).ToListAsync();
            ViewBag.Store = await _db.ItemStores.ToListAsync();
            ViewBag.Supplier = await _db.Suppliers.ToListAsync();
            ViewBag.Category = await _db.ItemCategories.ToListAsync();

            return View();
        }
       
        [HttpPost]
        [ValidateAntiForgeryToken]

         public async Task<IActionResult> Create(Inventory inv,  int productId, int storeId, int supId, int catId)
        {
          ViewBag.Products = await _db.Products.Include(x=>x.ItemCategory).ToListAsync();
            ViewBag.Store = await _db.ItemStores.ToListAsync();
            ViewBag.Supplier = await _db.Suppliers.ToListAsync();
            ViewBag.Category = await _db.ItemCategories.ToListAsync();

            if (!ModelState.IsValid)
            {
                return View();
            }
            inv.ProductId = catId;
             inv.ProductId = productId;
            inv.ItemStoreId= storeId;
            inv.SupplierId= supId;
           

            await _db.Inventories.AddAsync(inv);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        #endregion

        #region InventoryUpdate
        public async Task<IActionResult> Update(int? id)
        {
            ViewBag.Products = await _db.Products.Include(x => x.ItemCategory).ToListAsync();
            ViewBag.Store = await _db.ItemStores.ToListAsync();
            ViewBag.Supplier = await _db.Suppliers.ToListAsync();
            ViewBag.Category = await _db.ItemCategories.ToListAsync();
            if (id == null)
            {
                return NotFound();
            }
            Inventory dbInventory = await _db.Inventories.FirstOrDefaultAsync(x => x.Id == id);
            if (dbInventory == null)
            {
                return BadRequest();
            }
           

            return View(dbInventory);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int? id, Inventory inv, int productId, int storeId, int supId, int catId)
        {
            ViewBag.Products = await _db.Products.Include(x => x.ItemCategory).ToListAsync();
            ViewBag.Store = await _db.ItemStores.ToListAsync();
            ViewBag.Supplier = await _db.Suppliers.ToListAsync();
            ViewBag.Category = await _db.ItemCategories.ToListAsync();
            if (id == null)
            {
                return NotFound();
            }
            Inventory dbInventory = await _db.Inventories.FirstOrDefaultAsync(x => x.Id == id);
            if (dbInventory == null)
            {
                return BadRequest();
            }
            
            if (!ModelState.IsValid)
            {
                return View(dbInventory);
            }

            dbInventory.ProductId = productId;
            dbInventory.SupplierId = supId;
            dbInventory.ItemStoreId = storeId;
            //dbInventory.ProductId = catId;
            dbInventory.Product.ItemCategoryId = catId;
            dbInventory.Date = inv.Date;
            dbInventory.Quantity = inv.Quantity;
              await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        #endregion

        #region InventoryActivity
        public async Task<IActionResult> Activity(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Inventory dbInventory = await _db.Inventories.FirstOrDefaultAsync(x => x.Id == id);
            if (dbInventory == null)
            {
                return BadRequest();
            }
            if (dbInventory.IsDeactive == true)
            {
                dbInventory.IsDeactive = false;
            }
            else
            {
                dbInventory.IsDeactive = true;
            }
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        #endregion

        #region Ajax
        public JsonResult getstatebyid(int id)
        {
            List<Product> products = new List<Product>();
            products = _db.Products.Where(a => a.ItemCategory.Id == id).ToList();
            products.Insert(0, new Product { Id = 0, Name = "Please select" });
            return Json(new SelectList(products, "Id", "Name"));
        }
        #endregion


    }
}
 