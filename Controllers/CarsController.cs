using System.Linq;
using System.Web.Mvc;
using CarRentalApp.Models;

namespace CarRentalApp.Controllers
{
    public class CarsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {
            return View(db.Cars.ToList());
        }

        // Simple role check using Session (replace with proper Authorize if using Membership)
        private bool IsAdmin()
        {
            return Session["Role"] != null && Session["Role"].ToString() == "Admin";
        }

        public ActionResult Create()
        {
            if (!IsAdmin()) return RedirectToAction("Index");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Car car)
        {
            if (!IsAdmin()) return RedirectToAction("Index");
            if (ModelState.IsValid)
            {
                db.Cars.Add(car);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(car);
        }

        public ActionResult Edit(int id)
        {
            if (!IsAdmin()) return RedirectToAction("Index");
            var car = db.Cars.Find(id);
            return View(car);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Car car)
        {
            if (!IsAdmin()) return RedirectToAction("Index");
            db.Entry(car).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            if (!IsAdmin()) return RedirectToAction("Index");
            var car = db.Cars.Find(id);
            if (car != null)
            {
                db.Cars.Remove(car);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}
