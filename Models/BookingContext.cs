using Microsoft.EntityFrameworkCore;

namespace LocalStore.Models
{
    //Använd databas för att kunna lägga till/ta bort data från tabellen
    public class BookingContext :DbContext
    {
        public DbSet<BookingModel> Boka { get; set; }
        public BookingContext()
        {
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsbuilder)
        {
            optionsbuilder.UseSqlite("Data Source=bokningsData.db");
        }
    }
}
