using System.Security.Cryptography;
namespace PokerLibrary;

public class Dealer
{
    public List<Card> cards;
    public List<Card> teableCards = new List<Card>();
    public Dealer()
    {
        cards = new List<Card>();
        for (int i = 0; i < Card.cards.Count; i++)
            cards.Add(Card.cards[i]);
    }
    public void Shafling()
    {
        int size = cards.Count - 1;
        for (int i = size; i > 0; i--)
        {
            int j = RandomNumberGenerator.GetInt32(i + 1);
            (cards[i], cards[j]) = (cards[j], cards[i]);
        }
    }
    public void CardsForPlayers(List<Player> players)
    {

        foreach (var player in players)
        {
            player.playerCards.Clear();
            int size = cards.Count;
            for (int i = 0; i < 2; i++)
            {
                int index = RandomNumberGenerator.GetInt32(size);
                player.playerCards.Add(cards[index]);
                cards.RemoveAt(index);
            }
        }
    }
    public void CardsForTeable()
    {
        int size = cards.Count;
        for (int i = 0; i < 5; i++)
        {
            int index = RandomNumberGenerator.GetInt32(Math.Abs(size +1));
            teableCards.Add(cards[index]);
            cards.RemoveAt(index);
        }
    }
    public void DistributeCardsInPlayers(List<Player> players)
    {
        foreach (var player in players)
        {
            for (int i = 0; i < 2; i++)
            {
                cards.Add(player.playerCards[0]);
                player.playerCards.RemoveAt(0);
            }
        }
    }
    public void DistributeCardsInTeable()
    {
        int size = teableCards.Count;
        for (int i = 0; i < size; i++)
        {
            cards.Add(teableCards[0]);
            teableCards.RemoveAt(0);
        }
    }
}