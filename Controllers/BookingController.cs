using LocalStore.Models;
using Microsoft.AspNetCore.Mvc;

namespace LocalStore.Controllers
{
    public class BookingController : Controller
    {
        public IActionResult Index()
        {
            //Visar lista i databasen
            using (BookingContext db = new BookingContext())
            {
                List<BookingModel> bokningslista = db.Boka.ToList();
                return View(bokningslista);

            }
                
        }
        //Visar tabellen ifrån databasen
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        //Mata in ny data ifrån webbläsaren och spara i databasen
        [HttpPost]
        public IActionResult Create(BookingModel newBooking)
        {
            using (BookingContext db = new BookingContext())
            {
                db.Boka.Add(newBooking);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }


    }
}
