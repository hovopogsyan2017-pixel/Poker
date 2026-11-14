using PokerLibrary.BalanceTopUp;
using static System.Console;
namespace PokerLibrary;

public static class Program
{
    static void Main(string[] args)
    {
        var card = new Card();
        card.CreateCards();
        Dealer dealer = new Dealer();
        dealer.Shafling();
        foreach (var cards in dealer.cards)
        {
            Write($"Table Card: {cards.Suits} of {cards.Numbers} value: {cards.value} \n ");
        }
        List<Player> players = new List<Player>();
        for (int i = 0; i < 4; i++)
        {
            string? player = ReadLine();
            string? SurName = ReadLine();
            var newPlayer = new Player(player, SurName, 1500);
            players.Add(newPlayer);
        }
        string toUp = ReadLine();
        if (toUp == "Money")
        {
            int playerNumber = int.Parse(ReadLine());
            string cardPay = ReadLine();
            BalanceTo_Up methodPay = BalanceTo_Up.ObjectForBalanceCard(cardPay, players[playerNumber-1]);
            decimal pay = decimal.Parse(ReadLine());
            methodPay.Pay(pay);
        }

        for (int i = 0; i < 4; i++)
        {
            WriteLine($"Player {i + 1}: {players[i].Name} {players[i].SurName}, Balance: {players[i].balance}");
        }

        dealer.CardsForPlayers(players);
        dealer.CardsForTeable();

        Console.WriteLine("Player's Cards:");
        for (int i = 0; i < 4; i++)
        {
            int k = 0;
            WriteLine($"Player {i + 1}: {players[i].Name} {players[i].SurName}, Balance: {players[i].balance}");
            while (k < 2)
            {
                WriteLine($"Card {k}: {players[i].playerCards[k].Suits} of {players[i].playerCards[k].Numbers}");
                k++;
            }
            WriteLine("\n");
        }
        WriteLine("Table Cards:");
        foreach (var tebleCards in dealer.teableCards)
        {
            WriteLine($"Table Card: {tebleCards.Suits} of {tebleCards.Numbers}");
        }
        Console.WriteLine("\n");

        dealer.DistributeCardsInPlayers(players);
        dealer.DistributeCardsInTeable();
        dealer.Shafling();

        foreach (var cards in dealer.cards)
        {
            Write($"Table Card: {cards.Suits} of {cards.Numbers} value: {cards.value} \n ");
        }
    }
}