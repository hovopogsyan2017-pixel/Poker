using PokerLibrary.Enums;
using PokerLibrary;
namespace comb;

public class Combinations
{
    #region CombinationChecker
    public void CheckCombinations(Player p1, List<Card> tableCards)
    {
        int repeatedRankCount = p1.playerCards
            .Concat(tableCards)
            .GroupBy(c => c.Numbers)
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
        var playerCardsConcatToTableCards =
             pl.playerCards.Concat(tableCards)
             .Where(card => card.Numbers >= CardNumber.Ten)
             .OrderByDescending(c => c.Numbers)
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
        var playerCardsConcatToTableCards =
            pl.playerCards.Concat(tableCards)
            .GroupBy(card => card.Suits)
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
            pl.playerCards.Concat(tableCards)
            .GroupBy(c => c.Numbers)
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
            pl.playerCards
            .Concat(tableCards)
            .GroupBy(c => c.Numbers)
            .ToList();

        return cardsGroupedByRank.Count(g => g.Count() == cardsPerPair) == requiredPairCount;
    }
    public bool HasStraight(Player pl, List<Card> tableCards)
    {
        tableCards =
        tableCards
        .Concat(pl.playerCards)
        .OrderByDescending(c => c.Numbers)
        .DistinctBy(c => c.Numbers)
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
            pl.playerCards[0].Numbers == pl.playerCards[1].Numbers;

        var groups = pl.playerCards
       .Concat(tableCards)
       .GroupBy(c => c.Numbers)
       .ToList();

        bool hasTwoPairs =
        pocketPair &&
        groups.Count(g => g.Count() == requiredTwoPairCardsCount) >= twoPairGroupCount &&
        !groups.Any(g => g.Count() >= setCardsCount);

        return hasTwoPairs;
    }

    public string HighCard(Player pl)
        => pl.playerCards.Max().ToString();

    public bool HasSet(Player pl, List<Card> tableCards)
    {
        int requiredSetCardCount = 3;
        int setGroupCount = 1;

        var group =
            pl.playerCards
            .Concat(tableCards)
            .GroupBy(c => c.Numbers)
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

        var group = pl.playerCards
            .Concat(tableCards)
            .GroupBy(c => c.Numbers)
            .ToList();

        bool hasKare =
             group.Count(g => g.Count() == requiredKaReCardCount) == KaReGroupCount;

        return hasKare;
    }
}
    #endregion