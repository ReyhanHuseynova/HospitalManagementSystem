
using DocumentFormat.OpenXml.Spreadsheet;
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
    public class ProductsController : Controller
    {
        private readonly AppDbContext _db;
        public ProductsController(AppDbContext db)
        {
            _db = db;
        }
        public async Task<IActionResult> Index()
        {
            List<Product> products =await _db.Products.Include(x=>x.ItemCategory).ToListAsync();
           
            return View(products);
        }

        #region ProductCreate
        public async Task<IActionResult> Create()
        {
            ViewBag.ItemCategories = await _db.ItemCategories.ToListAsync();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Create(Product product, int catId)
        {
            ViewBag.ItemCategories = await _db.ItemCategories.ToListAsync();

            if (!ModelState.IsValid)
            {
                return View();
            }
            product.ItemCategoryId = catId;
            await _db.Products.AddAsync(product);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        #endregion

        #region ProductUpdate
        public async Task<IActionResult> Update(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Product dbProduct = await _db.Products.FirstOrDefaultAsync(x => x.Id == id);
            if (dbProduct == null)
            {
                return BadRequest();
            }
            ViewBag.ItemCategories = await _db.ItemCategories.ToListAsync();

            return View(dbProduct);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int? id, Product product, int catId)
        {
            if (id == null)
            {
                return NotFound();
            }
            Product dbProduct = await _db.Products.FirstOrDefaultAsync(x => x.Id == id);
            if (dbProduct == null)
            {
                return BadRequest();
            }
            ViewBag.BloodGroups = await _db.BloodGroups.ToListAsync();
            if (!ModelState.IsValid)
            {
                return View(dbProduct);
            }

            dbProduct.ItemCategoryId = catId;
            dbProduct.Name = product.Name;
            dbProduct.Description = product.Description;
          
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        #endregion

        #region ProductActivity
        public async Task<IActionResult> Activity(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Product dbProduct = await _db.Products.FirstOrDefaultAsync(x => x.Id == id);
            if (dbProduct == null)
            {
                return BadRequest();
            }
            if (dbProduct.IsDeactive == true)
            {
                dbProduct.IsDeactive = false;
            }
            else
            {
                dbProduct.IsDeactive = true;
            }
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        #endregion

    }
}
