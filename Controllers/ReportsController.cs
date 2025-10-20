using System.Linq;
using System.Web.Mvc;
using CarRentalApp.Models;
using System.IO;
using System.Text;

namespace CarRentalApp.Controllers
{
    public class ReportsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult ExportCsv()
        {
            var rentals = db.Rentals.ToList();
            var sb = new StringBuilder();
            sb.AppendLine("Id,Car,User,StartDate,EndDate,TotalPrice");
            foreach (var r in rentals)
            {
                sb.AppendLine($"{r.Id},{r.Car.Name},{r.User.Username},{r.StartDate},{r.EndDate},{r.TotalPrice}");
            }
            var bytes = Encoding.UTF8.GetBytes(sb.ToString());
            return File(bytes, "text/csv", "Laporan.csv");
        }

        // PDF/Excel generation would require external libs (iTextSharp/EPPlus). Add via NuGet in Visual Studio.
    }
}
