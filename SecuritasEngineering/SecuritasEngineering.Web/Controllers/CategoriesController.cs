using System.Data.Entity;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SecuritasEngineering.Web.Models;
using System.IO;
using System.Linq;
using SecuritasEngineering.Web.Utility;

namespace SecuritasEngineering.Web.Controllers
{
    [Authorize]
    public class CategoriesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        [AllowAnonymous]
        [Route("category/{id}")]
        public async Task<ActionResult> ProductCategory(int id)
        {
            ViewBag.Category = db.Categories.Where(c => c.ID == id).Select(m => m.Name).FirstOrDefault();
            return View(await db.Products.Where(i => i.Category.ID == id).ToListAsync());
        }

        // GET: Categories
        public async Task<ActionResult> Index()
        {
            return View(await db.Categories.ToListAsync());
        }

        // GET: Categories/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = await db.Categories.FindAsync(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // GET: Categories/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Categories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name")] Category category, HttpPostedFileBase ImageFile)
        {
            if (ModelState.IsValid)
            {
                if (ImageFile != null)
                {
                    string fileName = ImageFile.ToUniqueFileName();
                    category.ImageURL = "/Content/images/Categories/" + fileName;

                    fileName = Path.Combine(Server.MapPath("~/Content/images/Categories/"), fileName);
                    ImageFile.SaveAs(fileName);
                }


                db.Categories.Add(category);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(category);
        }

        // GET: Categories/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = await db.Categories.FindAsync(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // POST: Categories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,Name")] Category category, HttpPostedFileBase ImageFile)
        {
            if (ModelState.IsValid)
            {
                if (ImageFile != null)
                {
                    if (System.IO.File.Exists(Server.MapPath("~" + category.ImageURL)))
                    {
                        System.IO.File.Delete(Server.MapPath("~" + category.ImageURL));
                    }

                    string fileName = ImageFile.ToUniqueFileName();
                    category.ImageURL = "/Content/images/Categories/" + fileName;

                    fileName = Path.Combine(Server.MapPath("~/Content/images/Categories/"), fileName);
                    ImageFile.SaveAs(fileName);
                }

                db.Entry(category).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(category);
        }

        // GET: Categories/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = await db.Categories.FindAsync(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // POST: Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Category category = await db.Categories.FindAsync(id);

            if (System.IO.File.Exists(Server.MapPath("~" + category.ImageURL)))
            {
                System.IO.File.Delete(Server.MapPath("~" + category.ImageURL));
            }

            db.Categories.Remove(category);
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
