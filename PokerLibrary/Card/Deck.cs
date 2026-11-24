using PokerLibrary.LinkedListForControlQuPlayer;
using System.Security.Cryptography;
using PokerLibrary.Enums;
namespace PokerLibrary;

internal class Deck
{
    private readonly List<Card> _cardInDeck = new List<Card>();
    public int Count => _cardInDeck.Count;

    public Deck()
    {
        CoreSuids[] Suits = (CoreSuids[])Enum.GetValues(typeof(CoreSuids));
        CardNumber[] Numbers = (CardNumber[])Enum.GetValues(typeof(CardNumber));

        foreach (var suit in Suits)
            foreach (var number in Numbers)
                _cardInDeck.Add(new Card(suit, number, (int)number + 1));
    }

    public void Shaffle()
    {

        for (int i = _cardInDeck.Count - 1; i > 0; i--)
        {
            int j = RandomNumberGenerator.GetInt32(i + 1);
            (_cardInDeck[i], _cardInDeck[j]) = (_cardInDeck[j], _cardInDeck[i]);
        }

    }

    private Card RemoveCardInDeck()
    {
        int i = RandomNumberGenerator.GetInt32(_cardInDeck.Count);
        Card temp = _cardInDeck[i];
        _cardInDeck.RemoveAt(i);

        return temp;
    }
    private void RemoveCardInPlayer(Player temp)
    {
        if (temp.First != null)
            _cardInDeck.Add(temp.First);
        if (temp.Second != null)
            _cardInDeck.Add(temp.Second);

        temp.First = null;
        temp.Second = null;
    }

    public void CardsForPlayer(PlayersLinkedList players)
    {
        Node temp = players.Head;
        for (int i = 0; i < players.Count; i++)
        {
            temp.Value.First = RemoveCardInDeck();
            temp.Value.Second = RemoveCardInDeck();

            temp = temp.Next;
        }
    }

    public void ClearCardInPlayers(PlayersLinkedList players)
    {
        Node temp = players.Head;
        for (int i = 0; i < players.Count; i++)
        {
            RemoveCardInPlayer(temp.Value);
            temp = temp.Next;
        }
    }

    public void CardsForTeable(Table temp) 
    {
        int countCardsInTeable = 5;
        for (int i = 0; i < countCardsInTeable; i++) 
        {
            temp.cardsForTeable.Add(RemoveCardInDeck());
        }
    }

    public void ClearTeable(Table temp) 
    {
        int countCardsInTeable = temp.cardsForTeable.Count;
        for (int i = 0; i < countCardsInTeable; i++) 
        {
            _cardInDeck.Add(temp.cardsForTeable[i]);
        }
        temp.cardsForTeable.Clear();
    }

}


