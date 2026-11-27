namespace PokerLibrary.Data;

public class Bet
{
    public int Id { get; set; }
    public int HandId { get; set; }
    public PokerHand Hand { get; set; } = null!;
    public int PlayerId { get; set; }
    public Player Player { get; set; } = null!;
    public decimal Amount { get; set; }
}

