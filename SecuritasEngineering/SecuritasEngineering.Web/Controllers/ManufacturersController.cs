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
using SecuritasEngineering.Web.Utility;
using System.IO;

namespace SecuritasEngineering.Web.Controllers
{
    [Authorize]
    public class ManufacturersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        [AllowAnonymous]
        [Route("manufacturer/{id}")]
        public async Task<ActionResult> ProductManufacturer(int id)
        {
            ViewBag.Manufacturer = db.Manufacturers.Where(c => c.ID == id).Select(m => m.Name).FirstOrDefault();
            return View(await db.Manufacturers.Where(m => m.ID == id).SelectMany(m => m.Products).ToListAsync());
        }

        // GET: Manufacturers
        public async Task<ActionResult> Index()
        {
            return View(await db.Manufacturers.ToListAsync());
        }

        // GET: Manufacturers/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Manufacturer manufacturer = await db.Manufacturers.FindAsync(id);
            if (manufacturer == null)
            {
                return HttpNotFound();
            }
            return View(manufacturer);
        }

        // GET: Manufacturers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Manufacturers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID,Name")] Manufacturer manufacturer, HttpPostedFileBase ImageFile)
        {
            if (ModelState.IsValid)
            {
                if (ImageFile != null)
                {
                    string fileName = ImageFile.ToUniqueFileName();
                    manufacturer.ImageURL = "/Content/images/Manufacturers/" + fileName;

                    fileName = Path.Combine(Server.MapPath("~/Content/images/Manufacturers/"), fileName);
                    ImageFile.SaveAs(fileName);
                }

                db.Manufacturers.Add(manufacturer);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(manufacturer);
        }

        // GET: Manufacturers/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Manufacturer manufacturer = await db.Manufacturers.FindAsync(id);
            if (manufacturer == null)
            {
                return HttpNotFound();
            }
            return View(manufacturer);
        }

        // POST: Manufacturers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,Name")] Manufacturer manufacturer, HttpPostedFileBase ImageFile)
        {
            if (ModelState.IsValid)
            {
                if (ImageFile != null)
                {
                    if (System.IO.File.Exists(Server.MapPath("~" + manufacturer.ImageURL)))
                    {
                        System.IO.File.Delete(Server.MapPath("~" + manufacturer.ImageURL));
                    }

                    string fileName = ImageFile.ToUniqueFileName();
                    manufacturer.ImageURL = "/Content/images/Manufacturers/" + fileName;

                    fileName = Path.Combine(Server.MapPath("~/Content/images/Manufacturers/"), fileName);
                    ImageFile.SaveAs(fileName);
                }

                db.Entry(manufacturer).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(manufacturer);
        }

        // GET: Manufacturers/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Manufacturer manufacturer = await db.Manufacturers.FindAsync(id);
            if (manufacturer == null)
            {
                return HttpNotFound();
            }
            return View(manufacturer);
        }

        // POST: Manufacturers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Manufacturer manufacturer = await db.Manufacturers.FindAsync(id);

            if (System.IO.File.Exists(Server.MapPath("~" + manufacturer.ImageURL)))
            {
                System.IO.File.Delete(Server.MapPath("~" + manufacturer.ImageURL));
            }

            db.Manufacturers.Remove(manufacturer);
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
