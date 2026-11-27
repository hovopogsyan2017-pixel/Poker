using Microsoft.EntityFrameworkCore;
namespace PokerLibrary.Data
{
    public class Context: DbContext
    {
        public DbSet<Player> player { get; set; }
        public DbSet<PokerHand> pokerHand { get; set; }
        public DbSet<Table> table { get; set; }
        public DbSet<Bet> bet { get; set;}
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) 
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=PubDatabase;Trusted_Connection=True;TrustServerCertificate=True");
        }
    }
}
