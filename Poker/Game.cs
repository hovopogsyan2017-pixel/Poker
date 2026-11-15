using PokerLibrary;
using PokerLibrary.BalanceTopUp;
using static System.Console;
namespace Poker;

public class Game
{
    public static List<Player> players = new List<Player>();
    public static void Start()
    {
    
        var card = new Card();
        card.CreateCards();                     
        var dealer = new Dealer();              
        dealer.Shafling();                     

        PlayersForGame(dealer);                      

        WriteLine("Input <money> if you want balans to-up");
        string pay = ReadLine();
        if (pay == "money")
            MoneyForBalance();                 

        dealer.CardsForTeable();
        int quantity = players.Count;
        PrintPlayers(quantity);           
        PrintCardsInTeable(dealer);

        dealer.DistributeCardsInPlayers(players);  
        dealer.DistributeCardsInTeable();
        dealer.Shafling();
        PrintCards(dealer);
    }


    
    private static void PrintCardsInTeable(Dealer dealer)
    {
        WriteLine("Table Cards:");
        foreach (var tebleCards in dealer.teableCards)
        {
            WriteLine($"Table Card: {tebleCards.Suits} of {tebleCards.Numbers}");
        }
        WriteLine("\n");
    }
    private static void PrintPlayers(int quantity)
    {
        WriteLine("Player's Cards:");
        for (int i = 0; i < quantity; i++)
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
    }
    private static void PlayersForGame(Dealer dealer)
    {
        WriteLine("Input quantity players");
        int partistantsQuantity = int.Parse(ReadLine());
        if (partistantsQuantity > 1)
        {
            for (int i = 0; i < partistantsQuantity; i++)
            {
                string? player = ReadLine();
                string? SurName = ReadLine();
                var newPlayer = new Player(player, SurName, 1500);
                players.Add(newPlayer);
            }
            dealer.CardsForPlayers(players);
            PrintPlayers(partistantsQuantity);
        }
        else
            throw new ArgumentException("Quantity ERROR");
    }
    private static void PrintCards(Dealer dealer)
    {
        dealer.Shafling();
        foreach (var cards in dealer.cards)
        {
            Write($"Table Card: {cards.Suits} of {cards.Numbers} value: {cards.value} \n ");
        }
    }
    private static void MoneyForBalance()
    {
        WriteLine("Input number player");
        int playerNumber = int.Parse(ReadLine());
        WriteLine("Input method to-up\n" +
            "-> <Arca>\n" +
            "-> <Visa>\n" +
            "-><PayPal>\n");
        string cardPay = ReadLine();
        BalanceTo_Up methodPay = BalanceTo_Up.ObjectForBalanceCard(cardPay, players[playerNumber - 1]);
        WriteLine("Input Money");
        decimal pay = decimal.Parse(ReadLine());
        methodPay.Pay(pay);
    }

}

