using PokerLibrary.Enums;
namespace PokerLibrary;

public class Card
{
    public CoreSuids suids { get; set; }
    public CardNumber number { get; set; }
    public int value;

    public Card(CoreSuids s, CardNumber n, int v)
    {
        suids = s;
        number = n;
        value = v;
    }
}

