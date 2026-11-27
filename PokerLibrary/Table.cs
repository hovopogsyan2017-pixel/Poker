using PokerLibrary.LinkedListForControlQuPlayer;
using System.ComponentModel.DataAnnotations.Schema;
namespace PokerLibrary;

public class Table
{
    public int Id { get; set; }
    public int playerId { get; set; }
    public List<Player> player { get; set; } = null!;
    [NotMapped]
    public List<Card> cardsForTeable { get; } = new();
    [NotMapped]
    public List<int> playersLocations { get; } = new();
    public Table() { }
    public Table(PlayersLinkedList temp)
    {
        Node current = temp.Tail;
        for (int i = 0; i < temp.Count; i++)
        {
            playersLocations.Add(current.Value.id);
            current = current.Next;
        }
    }
}

