using System;
using System.Linq;
using System.Web.Mvc;
using CarRentalApp.Models;

namespace CarRentalApp.Controllers
{
    public class RentalsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Create(int carId)
        {
            var car = db.Cars.Find(carId);
            if (car == null || car.Status == "Disewa") return RedirectToAction("Index", "Cars");
            return View(car);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(int carId, DateTime startDate, DateTime endDate)
        {
            var car = db.Cars.Find(carId);
            if (car == null) return RedirectToAction("Index", "Cars");
            var days = (endDate - startDate).Days;
            if (days <= 0) days = 1;
            var total = days * car.PricePerDay;
            var rental = new Rental
            {
                CarId = car.Id,
                UserId = (int)Session["UserId"],
                StartDate = startDate,
                EndDate = endDate,
                TotalPrice = total
            };
            car.Status = "Disewa";
            db.Rentals.Add(rental);
            db.SaveChanges();
            return RedirectToAction("MyRentals");
        }

        public ActionResult MyRentals()
        {
            if (Session["UserId"] == null) return RedirectToAction("Login", "Account");
            int userId = (int)Session["UserId"];
            var rentals = db.Rentals.Where(r => r.UserId == userId).ToList();
            return View(rentals);
        }

        public ActionResult ReturnCar(int id)
        {
            var rental = db.Rentals.Find(id);
            if (rental != null)
            {
                var car = db.Cars.Find(rental.CarId);
                if (car != null) car.Status = "Tersedia";
                db.SaveChanges();
            }
            return RedirectToAction("MyRentals");
        }
    }
}
