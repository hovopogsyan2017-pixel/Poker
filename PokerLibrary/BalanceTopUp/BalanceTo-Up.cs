namespace PokerLibrary.BalanceTopUp;
public abstract class BalanceTo_Up : IBalanceToUp
{
    protected readonly Player _player;
    public static List<decimal> AllHistoryToUp = new List<decimal>();
    protected BalanceTo_Up(Player player)
    {
        _player = player;
    }
    public virtual void Pay(decimal pay)
    {
        if (pay <= 0)
            throw new ArgumentException("NO MONEY");
    }
    public static BalanceTo_Up ObjectForBalanceCard(string cardName,Player _player)
    {
        return cardName switch
        {
            "Arca" => new ArcaCard(_player),
            "PayPal" => new PayPal(_player),
            "Visa" => new VisaCard(_player),
            _ => throw new ArgumentException("Card Error")
        };
    }
}

