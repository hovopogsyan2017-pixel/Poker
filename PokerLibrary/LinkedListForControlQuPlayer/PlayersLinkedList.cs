namespace PokerLibrary.LinkedListForControlQuPlayer;

public class PlayersLinkedList
{
    public Node? Head { get; private set; }
    public Node? Tail { get; private set; }
    public Node? Current { get; private set; }
    public int Count { get; private set; }
    public Dictionary<int, Player> historyRemovePlayers = new();

    #region Add

    public void Add(Player player)
    {

        var node = new Node(player);
        if (Head == null)
        {

            Head = node;
            Tail = node;
            Current = node;
        }

        else
        {
            if (Tail != null)
            {
                Tail.Next = node;
                node.Next = Head;
                Tail = node;
            }
            else throw new ArgumentException("Tail is NULL ");
        }

        Count++;
    }
    private Player FindTempHead(Table temp)
    {
        Player tempHead = null;
        for (int i = 0; i < temp.playersLocations.Count; i++)
        {
            int playerId = temp.playersLocations[i];
            if (historyRemovePlayers.ContainsKey(playerId))
            {
                tempHead = historyRemovePlayers[playerId];
                return tempHead;
            }
            else continue;
        }
            return null;
    }

    public void AddPlayerThenRemove(Table temp)
    {
        Player tempHead = FindTempHead(temp);
        

        if (tempHead != null)
        {
            while (temp.playersLocations.Count != Count)
            {
                int index=0;
                while (tempHead.id != temp.playersLocations[index])
                    index++;



            }

        }

    }

    #endregion

    public void MoveNext()
    {
        if (Current != null)
            Current = Current.Next;

        else throw new ArgumentException("No Player");
    }
    #region Remove
    public void Remove(Player player)
    {
        if (Count == 0)
            throw new ArgumentException("No Player");

        else if (Count == 1 && Head.Value == player)
            Head = Tail = null;

        else
        {

            if (Head.Value == player)
            {
                Tail.Next = Head.Next;
                Head = Head.Next;
            }

            else if (Tail.Value == player)
            {
                Node temp = Head;
                while (temp.Next != Tail)
                    temp = temp.Next;

                temp.Next = Head;
                Tail = temp;
            }

            else
            {
                Node temp = Tail;
                while (temp.Next.Value != player)
                    temp = temp.Next;

                Node flag = temp.Next;
                temp.Next = flag.Next;
            }
        }

        Count--;
        removePlayers.Add(player.id, player);
    }
    #endregion
}

