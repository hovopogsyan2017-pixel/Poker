using PokerLibrary.LinkedListForControlQuPlayer;
using System.ComponentModel.DataAnnotations.Schema;

namespace PokerLibrary;

public class Player
{
    public int id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public decimal? balance { get; set; }
    [NotMapped]
    public Card? First { get; set; }
    [NotMapped]
    public Card? Second { get; set; }
    public Player(string name, string surname, int balance)
    {
        FirstName = name;
        LastName = surname;
        this.balance = balance;
    }

    public void Fold(PlayersLinkedList temp, Player player)
       => temp.Remove(player);

    public void Check(PlayersLinkedList temp)
       => temp.MoveNext();

    public decimal Raize(Player temp, decimal money, Table table)
    {

        if (temp.balance < money)
            throw new ArgumentException("No Money");

        else
        {
            temp.balance -= money;
            return money;
        }
    }
    public decimal AllIn(Player temp, Table table)
    {
        if (temp.balance == 0)
            throw new ArgumentException("No Money");

        else
        {
            decimal amount = (decimal)temp.balance;
            temp.balance = 0;
            return amount;
        }
    }

    public decimal Bet(Player temp, Table table, decimal money = 50)
    {
        if (temp.balance < money)
            throw new ArgumentException("NO MONEY");

        else
        {
            temp.balance -= money;
            return money;
        }
    }

    public decimal Call(Player temp, Table table)
    {

        return 0;
    }
}