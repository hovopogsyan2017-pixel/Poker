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
}

