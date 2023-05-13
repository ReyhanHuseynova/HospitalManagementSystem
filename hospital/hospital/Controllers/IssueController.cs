using DocumentFormat.OpenXml.Office2010.ExcelAc;
using hospital.DAL;
using hospital.Models;
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
    public class IssueController : Controller
    {
        private readonly AppDbContext _db;
        public IssueController(AppDbContext db)
        {
            _db = db;   
        }
        public async Task<IActionResult> Index()
        {
            List<IssueItem>issueitems = await _db.IssueItems.Include(x=>x.Product).ThenInclude(x=>x.ItemCategory).
            ToListAsync();
            return View(issueitems);
        }

        #region IssueCreate

        public async Task<IActionResult> Create()
        {

            ViewBag.Products = await _db.Products.Include(x => x.ItemCategory).ToListAsync();
            ViewBag.Category = await _db.ItemCategories.ToListAsync();

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]



        public async Task<IActionResult> Create(IssueItem issue, int productId, int catId)
        {
            ViewBag.Products = await _db.Products.Include(x => x.ItemCategory).ToListAsync();
            ViewBag.Category = await _db.ItemCategories.ToListAsync();

            if (!ModelState.IsValid)
            {
                return View();
            }
            issue.ProductId = catId;
            issue.ProductId = productId;
           


            await _db.IssueItems.AddAsync(issue);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        #endregion

        #region IssueUpdate
        public async Task<IActionResult> Update(int? id)
        {
            ViewBag.Products = await _db.Products.Include(x => x.ItemCategory).ToListAsync();
            ViewBag.Category = await _db.ItemCategories.ToListAsync();
            if (id == null)
            {
                return NotFound();
            }
            IssueItem dbIssueItem = await _db.IssueItems.FirstOrDefaultAsync(x => x.Id == id);
            if (dbIssueItem == null)
            {
                return BadRequest();
            }


            return View(dbIssueItem);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int? id, IssueItem issue, int productId, int catId)
        {
            ViewBag.Products = await _db.Products.Include(x => x.ItemCategory).ToListAsync();
            ViewBag.Category = await _db.ItemCategories.ToListAsync();
            if (id == null)
            {
                return NotFound();
            }
            IssueItem dbIssueItem = await _db.IssueItems.FirstOrDefaultAsync(x => x.Id == id);
            if (dbIssueItem == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return View(dbIssueItem);
            }

           
           dbIssueItem.ProductId = productId;
            dbIssueItem.Product.ItemCategoryId = catId;
            dbIssueItem.ReturnDate = issue.ReturnDate;
            dbIssueItem.Quantity = issue.Quantity;
            dbIssueItem.IssueDate = issue.IssueDate;
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        #endregion

        #region IssueActivity
        public async Task<IActionResult> Activity(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            IssueItem dbIssueItem = await _db.IssueItems.FirstOrDefaultAsync(x => x.Id == id);
            if (dbIssueItem == null)
            {
                return BadRequest();
            }
            if (dbIssueItem.IsDeactive == true)
            {
                dbIssueItem.IsDeactive = false;
            }
            else
            {
                dbIssueItem.IsDeactive = true;
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
   