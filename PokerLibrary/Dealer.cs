using PokerLibrary.Enums;
using PokerLibrary;
namespace PokerLibrary;

public class Dealer
{
    public List<Card> cards = new List<Card>();
    public List<Card> teableCards = new List<Card>();
    public Dealer()
    {
        for (int i = 0; i < Card.cards.Count; i++)
            cards.Add(Card.cards[i]);
    }
    //Karteri patahakan bajanum
    public void CardsForPlayers(List<Player> players)
    {

        Random rnd = new Random();
        //Qarery bajanuma xaxacoxnerin
        foreach (var player in players)
        {
            player.playerCards.Clear();
            for (int i = 0; i < 2; i++)
            {
                int index = rnd.Next(0, cards.Count);
                player.playerCards.Add(cards[index]);
                cards.RemoveAt(index);
            }
        }
    }
        // Qarery bacuma sexanin
    public void CardsForTable()
    {
        Random rnd = new Random();
        for (int i = 0; i < 5; i++)
        {
            int index = rnd.Next(0, cards.Count);
            teableCards.Add(cards[index]);
            cards.RemoveAt(index);
        }
    }

    //Xaxacoxneruc karteri havaqum
    public void DistributeCardsInPlayers(List<Player> players)
    {
        foreach (var player in players)
        {
            for(int i=0;i<2;i++)
            {
                cards.Add(player.playerCards[0]);
                player.playerCards.RemoveAt(0);
            }
        }
    }
    //Sexanic karteri havaqum
    public void DistributeCardsInTeable()
    {
        for(int i=0;i<teableCards.Count;i++)
        {
            cards.Add(teableCards[0]);
            teableCards.RemoveAt(0);
        }
    }
}


