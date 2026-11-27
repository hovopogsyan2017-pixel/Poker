using PokerLibrary;
using PokerLibrary.BalanceTopUp;
using PokerLibrary.Data;
using PokerLibrary.LinkedListForControlQuPlayer;
using static System.Console;

namespace Poker; 

public static class Game
{
    private const int StartBalance = 1500;
    private const int MinPlayers = 2;
    private const int MaxPlayers = 10;

    private static readonly PlayersLinkedList players = new PlayersLinkedList();

    public static void Start()
    {
        BackgroundColor = ConsoleColor.Cyan;
        ForegroundColor = ConsoleColor.Black;
        
        var dealer = new Dealer();
        var table = new Table(players);

        PlayersForGame();

        while (true)
        {
            WriteLine("\n--- MENU ---");
            WriteLine("1. Deal cards (New Round)");
            WriteLine("2. Top-up player balance");
            WriteLine("3. Show current players");
            WriteLine("4. Show table cards");
            WriteLine("5. Exit");

            switch (ReadInt("Choose option: ", 1, 5))
            {
                case 1:
                    PlayRound(dealer, table);
                    break;
                case 2:
                    MoneyForBalance();
                    break;
                case 3:
                    PrintPlayers();
                    break;
                case 4:
                    PrintCardsInTeable(table);
                    break;
                case 5:
                    return;
            }
        }
    }
    private static void PlayRound(Dealer dealer, Table table)
    {
        dealer.ClearCardInPlayers(players);
        dealer.ClearTeable(table);

        dealer.Shaffle();
        dealer.CradsForPlayer(players);
        dealer.CardsForTeable(table);

        PrintPlayers();
        PrintCardsInTeable(table);
    }

    private static void PlayersForGame()
    {
        int quantity = ReadInt($"Input quantity players ({MinPlayers}-{MaxPlayers}): ", MinPlayers, MaxPlayers);

        for (int i = 0; i < quantity; i++)
        {
            string name = ReadText($"Player {i + 1} First Name: ", "Player");
            string surName = ReadText($"Player {i + 1} Last Name: ", (i + 1).ToString());

            var newPlayer = new Player(name, surName, StartBalance);
            DbManager.AddPlayerToDatabase(name, surName, StartBalance, i + 1);

            var dbPlayer = DbManager.GetAllPlayer().OrderBy(p => p.id).LastOrDefault();
            if (dbPlayer != null)
                newPlayer.id = dbPlayer.id;

            players.Add(newPlayer);
        }
    }

    private static void MoneyForBalance()
    {
        PrintPlayers();

        int playerNumber = ReadInt("Input player number: ", 1, players.Count);
        Player targetPlayer = GetPlayer(playerNumber);

        BalanceTo_Up methodPay = ReadPaymentMethod(targetPlayer);
        decimal pay = ReadDecimal("Input Money: ");

        try
        {
            DbManager.BalanceToUpFromDb(targetPlayer.id, pay);
            methodPay.Pay(pay);

            WriteLine($"Done! New balance for {targetPlayer.FirstName}: {targetPlayer.balance}");
        }
        catch (Exception ex)
        {
            WriteLine($"Top-up failed: {ex.Message}");
        }
    }

    private static BalanceTo_Up ReadPaymentMethod(Player player)
    {
        while (true)
        {
            WriteLine("Input method top-up:\n -> Arca\n -> Visa\n -> PayPal");
            string input = ReadText("Method: ", "");

            string? method = input.ToLowerInvariant() switch
            {
                "arca" => "Arca",
                "visa" => "Visa",
                "paypal" => "PayPal",
                _ => null
            };

            if (method != null)
                return BalanceTo_Up.ObjectForBalanceCard(method, player);

            WriteLine("Unknown method. Choose Arca, Visa or PayPal.");
        }
    }

    private static Player GetPlayer(int number)
    {
        if (number < 1 || number > players.Count || players.Head == null)
            throw new ArgumentOutOfRangeException(nameof(number), "Invalid player number");

        Node temp = players.Head;
        for (int i = 1; i < number; i++)
            temp = temp.Next!;
        return temp.Value;
    }

    private static void PrintPlayers()
    {
        WriteLine("\n=== Players ===");

        Node? temp = players.Head;
        for (int i = 0; i < players.Count && temp != null; i++)
        {
            Player p = temp.Value;
            WriteLine($"#{i + 1} (DB ID: {p.id}) | {p.FirstName} {p.LastName} | Balance: {p.balance}");

            if (p.First != null && p.Second != null)
            {
                WriteLine($"   Card 1: {p.First.number} of {(char)p.First.suids}");
                WriteLine($"   Card 2: {p.Second.number} of {(char)p.Second.suids}");
            }
            temp = temp.Next;
        }
        WriteLine();
    }

    private static void PrintCardsInTeable(Table table)
    {
        WriteLine("=== Table Cards ===");

        if (table.cardsForTeable.Count == 0)
        {
            WriteLine("No cards on the table yet.\n");
            return;
        }

        foreach (var card in table.cardsForTeable)
        WriteLine($"Table Card: {card.number} of {(char)card.suids}");
        WriteLine();
    }

    private static int ReadInt(string prompt, int min, int max)
    {
        while (true)
        {
            Write(prompt);
            if (int.TryParse(ReadLine(), out int value) && value >= min && value <= max)
                return value;
            WriteLine($"Invalid input. Enter a number from {min} to {max}.");
        }
    }

    private static decimal ReadDecimal(string prompt)
    {
        while (true)
        {
            Write(prompt);
            if (decimal.TryParse(ReadLine(), out decimal value) && value > 0)
                return value;
            WriteLine("Invalid amount. Enter a positive number.");
        }
    }

    private static string ReadText(string prompt, string fallback)
    {
        Write(prompt);
        string? text = ReadLine();
        return string.IsNullOrWhiteSpace(text) ? fallback : text.Trim();
    }
}