using PokerLibrary.Enums;
namespace PokerLibrary;

public class Card
{

    public static List<Card> cards = new List<Card>();
    public CoreSuids Suits { get; set; }
    public CardNumber Numbers { get; set; }
    public int value = 0;
    public void CreateCards()
    {
        CoreSuids[] Suits = (CoreSuids[])Enum.GetValues(typeof(CoreSuids));
        CardNumber[] Numbers = (CardNumber[])Enum.GetValues(typeof(CardNumber));
        foreach (var suits in Suits)
            foreach (var number in Numbers)
                cards.Add(new Card { Suits = suits, Numbers = number, value = (int)number + 1 });
    }
}

