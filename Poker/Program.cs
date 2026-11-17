using PokerLibrary.BalanceTopUp;
using static System.Console;
using PokerLibrary.Data;
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

    foreach( var pl in DbManager.GetAllPlayer())
            WriteLine($"{pl.FirstName} - {pl.LastName} - {pl.balance} - {pl.id}");

    }
}