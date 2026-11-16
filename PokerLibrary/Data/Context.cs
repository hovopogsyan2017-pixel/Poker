using Microsoft.EntityFrameworkCore;
namespace PokerLibrary.Data
{
    public class Context: DbContext
    {
        public DbSet<Player> player { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) 
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=PubDatabase;Trusted_Connection=True;TrustServerCertificate=True");
        }
    }
}
