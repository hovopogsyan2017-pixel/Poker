using PokerLibrary.Enums;
namespace PokerLibrary;

public class Combinations
{
    private static List<Card> GetPlayerCards(Player player)
    {
        if (player.First == null || player.Second == null)
            throw new ArgumentException("No Cards");

        else return new List<Card> { player.First, player.Second };

    }
    #region CombinationChecker
    public void CheckCombinations(Player p1, List<Card> tableCards)
    {
        int repeatedRankCount = GetPlayerCards(p1)
            .Concat(tableCards)
            .GroupBy(c => c.number)
            .Count(g => g.Count() >= 2);

        (string Name, Func<Player, List<Card>, bool> Check)[] checks;

        if (repeatedRankCount == 0)
        {

            checks = new (string Name, Func<Player, List<Card>, bool> Check)[]
            {
                ("Royal-Flush", HasRoyalFlush),
                ("Straight-Flush", HasStraightFlush),
                ("Flush", HasFlush),
                ("Straight", HasStraight)
            };
        }
        else if (repeatedRankCount >= 3)
        {
            checks = new (string Name, Func<Player, List<Card>, bool> Check)[]
            {
                ("Full-House", HasFullHouse),
                ("Two Pairs", HasTwoPairs)
            };
        }
        else
        {
            checks = new (string Name, Func<Player, List<Card>, bool> Check)[]
            {
                ("Royal-Flush", HasRoyalFlush),
                ("Straight-Flush", HasStraightFlush),
                ("Four of Kind", HasKaRe),
                ("Full-House", HasFullHouse),
                ("Flush", HasFlush),
                ("Straight", HasStraight),
                ("Set", HasSet),
                ("Two Pairs", HasTwoPairs),
                ("One Pair", HasOnePair)
            };
        }

        foreach (var combination in checks)
        {
            if (combination.Check(p1, tableCards))
            {
                Console.WriteLine($"{p1.FirstName}: {combination.Name}");
                return;
            }
        }

        Console.WriteLine($"{p1.FirstName}: High card");
        HighCard(p1);
    }
    #endregion
    #region Combinations
    public bool HasRoyalFlush(Player pl, List<Card> tableCards)
    {
        var playerCardsConcatToTableCards = GetPlayerCards(pl)
             .Concat(tableCards)
             .Where(card => card.number >= CardNumber.Ten)
             .OrderByDescending(c => c.number)
             .ToList();
        int RoyalFlushCount = 5;
        return playerCardsConcatToTableCards.Count() == RoyalFlushCount && HasStraightFlush(pl, tableCards);
    }
    public bool HasStraightFlush(Player pl, List<Card> tableCards)
    => HasStraight(pl, tableCards) && HasFlush(pl, tableCards);
    public bool HasFlush(Player pl, List<Card> tableCards)
    {
        int flushReuqiredCardsCount = 5;
        int requiredFlushCardGroupCount = 1;
        var playerCardsConcatToTableCards = GetPlayerCards(pl)
            .Concat(tableCards)
            .GroupBy(card => card.suids)
            .ToList();

        return
        playerCardsConcatToTableCards
        .Count(c => c.Count() >= flushReuqiredCardsCount)
        == requiredFlushCardGroupCount;

    }
    public bool HasFullHouse(Player pl, List<Card> tableCards)// 
    {

        int requiredSetCardsCount = 3;
        int setGroupCount = 1;
        int pairGroupCount = 1;
        int requiredPairCardsCount = 2;
        var group =
            GetPlayerCards(pl).Concat(tableCards)
            .GroupBy(c => c.number)
            .ToList();

        bool hasSet =
             group.Count(g => g.Count() == requiredSetCardsCount) == setGroupCount &&
            !group.Any(g => g.Count() > requiredSetCardsCount);

        bool hasPair =
             group.Count(g => g.Count() == requiredPairCardsCount) == pairGroupCount;

        return (hasSet && hasPair);
    }
    public bool HasOnePair(Player pl, List<Card> tableCards)
    {
        int cardsPerPair = 2;
        int requiredPairCount = 1;

        var cardsGroupedByRank =
            GetPlayerCards(pl)
            .Concat(tableCards)
            .GroupBy(c => c.number)
            .ToList();

        return cardsGroupedByRank.Count(g => g.Count() == cardsPerPair) == requiredPairCount;
    }
    public bool HasStraight(Player pl, List<Card> tableCards)
    {
        tableCards =
        tableCards
        .Concat(GetPlayerCards(pl))
        .OrderByDescending(c => c.number)
        .DistinctBy(c => c.number)
        .ToList();

        int tableCardsCount = tableCards.Count;
        int StraightSuccesCounter = 5;
        bool isStraight = false;
        int iterationCounter = 0;

        while (isStraight == false)
        {
            for (int i = 0; i < tableCardsCount - 1; i++)
            {
                if (tableCards[i].value - tableCards[i + 1].value == 1)
                    iterationCounter++;

                if (iterationCounter == StraightSuccesCounter)
                {
                    isStraight = true;
                    break;
                }
                else if (tableCards.Count - i == 1)
                    break;
            }
            if (isStraight == false)
                break;
        }
        return isStraight;
    }
    public bool HasTwoPairs(Player pl, List<Card> tableCards)
    {
        int requiredTwoPairCardsCount = 2;
        int twoPairGroupCount = requiredTwoPairCardsCount;
        int setCardsCount = 3;
        bool pocketPair =
            GetPlayerCards(pl)[0].number == GetPlayerCards(pl)[1].number;

        var groups = GetPlayerCards(pl)
       .Concat(tableCards)
       .GroupBy(c => c.number)
       .ToList();

        bool hasTwoPairs =
        pocketPair &&
        groups.Count(g => g.Count() == requiredTwoPairCardsCount) >= twoPairGroupCount &&
        !groups.Any(g => g.Count() >= setCardsCount);

        return hasTwoPairs;
    }

    public string HighCard(Player pl)
        => GetPlayerCards(pl).Max(c => c.number).ToString();

    public bool HasSet(Player pl, List<Card> tableCards)
    {
        int requiredSetCardCount = 3;
        int setGroupCount = 1;

        var group =
             GetPlayerCards(pl)
            .Concat(tableCards)
            .GroupBy(c => c.number)
            .ToList();

        bool hasSet =
            group.Count(g => g.Count() == requiredSetCardCount) == setGroupCount &&
            !group.Any(g => g.Count() > requiredSetCardCount);

        return hasSet;

    }
    public bool HasKaRe(Player pl, List<Card> tableCards)
    {
        int requiredKaReCardCount = 4;
        int KaReGroupCount = 1;

        var group = GetPlayerCards(pl)
            .Concat(tableCards)
            .GroupBy(c => c.number)
            .ToList();

        bool hasKare =
             group.Count(g => g.Count() == requiredKaReCardCount) == KaReGroupCount;

        return hasKare;
    }
}
    #endregion