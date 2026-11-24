using PokerLibrary.LinkedListForControlQuPlayer;
namespace PokerLibrary;

public class Table
{
    public List<Card> cardsForTeable { get; } = new();
    public Dictionary<Player, decimal> playersAndBet { get; } = new();
    public List<int> playersLocations { get; } = new();
    public Table(PlayersLinkedList temp)
    {
        Node current = temp.Tail;
        for (int i = 0; i < temp.Count; i++)
        {
            playersAndBet.Add(current.Value, 0);
            playersLocations.Add(current.Value.id);
            current = current.Next;
        }
    }
}

