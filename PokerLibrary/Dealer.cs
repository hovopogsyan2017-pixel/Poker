using PokerLibrary.LinkedListForControlQuPlayer;
namespace PokerLibrary;

public class Dealer
{
    Deck deck;
    public Dealer()
    {
        deck = new Deck();
    }
    public void Shaffle() 
    {
        deck.Shaffle();
    }
    public void CradsForPlayer(PlayersLinkedList players) 
    {
        deck.CardsForPlayer(players);
    }
    public void CardsForTeable(Teable temp) 
    {
        deck.CardsForTeable(temp);
    }
    public void ClearTeable(Teable temp) 
    {
        deck.ClearTeable(temp);
    }
    public void ClearCardInPlayers(PlayersLinkedList players) 
    {
        deck.ClearCardInPlayers(players);

    }

}