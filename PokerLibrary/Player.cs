namespace PokerLibrary;

public class Player
{
    public string Name { get; set; }
    public string SurName { get; set; }
    public decimal? balance { get; set; }
    public List<Card> playerCards = new List<Card>();
    public Player(string name, string surname, int balance)
    {
        Name = name;
        SurName = surname;
        this.balance = balance;
    }
}

