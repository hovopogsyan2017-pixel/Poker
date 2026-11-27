namespace PokerLibrary.Data;

public class PokerHand
{
    public int Id { get; set; }
    public int TableId { get; set; }
    public Table table { get; set; } = null!;
}

