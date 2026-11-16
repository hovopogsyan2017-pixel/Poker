using PokerLibrary.BalanceTopUp;
using static System.Console;
namespace Poker;

public static class Program
{
    static void Main(string[] args)
    {
        using (var db = new PokerLibrary.Data.Context()) 
        {
            db.Database.EnsureCreated();
        }
            Game.Start();
    }
}