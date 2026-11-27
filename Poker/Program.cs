using System.Diagnostics;
using static System.Console;
using PokerLibrary.Data;
namespace Poker;

public static class Program
{
    static void Main(string[] args)
    {
        OutputEncoding = System.Text.Encoding.UTF8;
        InputEncoding = System.Text.Encoding.UTF8;

        using (var db = new PokerLibrary.Data.Context()) 
        {
            db.Database.EnsureCreated();
        }
            Game.Start();

    foreach( var pl in DbManager.GetAllPlayer())
            WriteLine($"{pl.FirstName} - {pl.LastName} - {pl.balance} - {pl.id}");


        Stopwatch sw = Stopwatch.StartNew();
        sw.Stop();
        WriteLine($"Execution time: {sw.ElapsedMilliseconds} ms");
        WriteLine($"Exact time: {sw.Elapsed.TotalMilliseconds:F3} ms");

    }
}