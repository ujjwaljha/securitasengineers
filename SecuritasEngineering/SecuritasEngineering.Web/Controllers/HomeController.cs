using System;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Web.Mvc;
using SecuritasEngineering.Web.Models;
using SecuritasEngineering.Web.VeiwModel;

namespace SecuritasEngineering.Web.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public async Task<ActionResult> Index()
        {
            return View(new HomeViewModel
            {
                Categories = await db.Categories.ToListAsync(),
                Manufacturers = await db.Manufacturers.ToListAsync()
            });
        }

        //[Route("category/{id?:int}")]
        //public async Task<ActionResult> Category(int? id)
        //{
        //    Category category = await db.Categories.FindAsync(id);
        //    if (category == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(category);
        //}

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}