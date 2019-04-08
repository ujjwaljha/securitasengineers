using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SecuritasEngineering.Web.Models;
using System.IO;
using System.Windows.Forms;
using SecuritasEngineering.Web.Utility;
using SecuritasEngineering.Web.VeiwModel;

namespace SecuritasEngineering.Web.Controllers
{
    [Authorize]
    public class ProductsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        [AllowAnonymous]
        [Route("product/{id}")]
        public async Task<ActionResult> Product(int id)
        {
            ViewBag.Manufacturers = await db.Products.Where(m => m.ID == id).
                SelectMany(m => m.Manufacturers)
                .Select(m => m.Name).ToListAsync();
            ViewBag.Category = await db.Products.Where(m => m.ID == id).Select(m => m.Category.Name).FirstOrDefaultAsync();
            return View(await db.Products.Where(m => m.ID == id).FirstOrDefaultAsync());
        }

        // GET: Products
        public async Task<ActionResult> Index()
        {
            return View(await db.Products.ToListAsync());
        }

        // GET: Products/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = await db.Products.FindAsync(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: Products/Create
        
        public ActionResult Create()
        {

            ProductViewModel model = new ProductViewModel();
            model.Manufacturers = db.Manufacturers
                .Select(m => new _ProductViewModel
                {
                    ID = m.ID,
                    Name = m.Name,
                    IsChecked = false
                })
                .ToList();

            model.Categories = db.Categories
                .Select(m => new _ProductViewModel
                {
                    ID = m.ID,
                    Name = m.Name
                })
                .ToList();

            //ViewBag.Category = new SelectList(db.Categories, "ID", "Name");

            return View(model);
        }

        // POST: Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(ProductViewModel productViewModel, HttpPostedFileBase ImageFile)
        {
            if (ModelState.IsValid)
            {
                Product product = new Product();

                if (ImageFile != null)
                {
                    string fileName = ImageFile.ToUniqueFileName();
                    product.ImageURL = "/Content/images/Products/" + fileName;

                    fileName = Path.Combine(Server.MapPath("~/Content/images/Products/"), fileName);
                    ImageFile.SaveAs(fileName);
                }

                product.Name = productViewModel.Product.Name;


                product.Category = db.Categories.Where(c => c.ID == productViewModel.Product.ID).FirstOrDefault();

                var ids = productViewModel.Manufacturers.Where(m => m.IsChecked == true).Select(m => m.ID);

                product.Manufacturers = db.Manufacturers.Where(m => ids.Contains(m.ID)).ToList();
                    //productViewModel.Manufacturers.Select(m => new Manufacturer { ID = m.ID, Name = m.Name }).ToList();

                db.Products.Add(product);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            productViewModel.Categories = db.Categories
                .Select(m => new _ProductViewModel
                {
                    ID = m.ID,
                    Name = m.Name
                })
                .ToList();

            return View(productViewModel);
        }

        // GET: Products/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = await db.Products.FindAsync(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,Name")] Product product, ProductViewModel productViewModel, HttpPostedFileBase ImageFile)
        {
            if (ModelState.IsValid)
            {
                if (ImageFile != null)
                {
                    if (System.IO.File.Exists(Server.MapPath("~" + product.ImageURL)))
                    {
                        System.IO.File.Delete(Server.MapPath("~" + product.ImageURL));
                    }

                    string fileName = ImageFile.ToUniqueFileName();
                    product.ImageURL = "/Content/images/Products/" + fileName;

                    //fileName = Path.Combine(Server.MapPath("~/Content/images/Products/"), fileName);
                    //ImageFile.SaveAs(fileName);
                }

                //db.Entry(product).State = EntityState.Modified;
                //await db.SaveChangesAsync();
                //return RedirectToAction("Index");
            }
            return View(product);
        }

        // GET: Products/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = await db.Products.FindAsync(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Product product = await db.Products.FindAsync(id);

            if (System.IO.File.Exists(Server.MapPath("~" + product.ImageURL)))
            {
                System.IO.File.Delete(Server.MapPath("~" + product.ImageURL));
            }

            db.Products.Remove(product);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
