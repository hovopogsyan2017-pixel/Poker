using PokerLibrary.Enums;
using PokerLibrary.LinkedListForControlQuPlayer;
using System.ComponentModel.DataAnnotations.Schema;

namespace PokerLibrary;

public class Player
{
    public int id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public decimal? balance { get; set; }
    [NotMapped]
    public Card? First { get; set; }
    [NotMapped]
    public Card? Second { get; set; }
    public Player() { }
    public Player(string name, string surname, int balance)
    {
        FirstName = name;
        LastName = surname;
        this.balance = balance;
    }

    public void Fold(PlayersLinkedList temp, StateGame state)
    {
        
    }

    public void Check(PlayersLinkedList temp) => temp.MoveNext();
  
    public decimal Raize(Player temp, decimal money, Table table)
    {

        if (temp.balance < money)
            throw new ArgumentException("No Money");

        else
        {
            table.playersAndBet.Add(temp, money);
            return money;
        }
    }


    public decimal AllIn(Player temp, Table table)
    {
        if (temp.balance == 0)
            throw new ArgumentException("No Money");

        else
        {
            table.playersAndBet.Add(temp, (decimal)temp.balance);
            return (decimal)temp.balance;
        }
    }
}