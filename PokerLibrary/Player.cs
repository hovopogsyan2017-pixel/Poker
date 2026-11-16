using System.ComponentModel.DataAnnotations.Schema;

namespace PokerLibrary;

public class Player : Attribute
{
    public int id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public decimal? balance { get; set; }
    [NotMapped]
    public List<Card> playerCards = new List<Card>();

    public Player() { }
    public Player(string name, string surname, int balance)
    {
        FirstName = name;
        LastName = surname;
        this.balance = balance;
    }

   
}

