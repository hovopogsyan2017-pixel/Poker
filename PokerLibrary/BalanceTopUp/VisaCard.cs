namespace PokerLibrary.BalanceTopUp;

internal class VisaCard : BalanceTo_Up
{
    public VisaCard(Player player) : base(player) { }
    public int CountForBalanceToUp = 0;
    public List<decimal> history = new List<decimal>();

    public override void Pay(decimal pay)
    {
        base.Pay(pay);
        _player.balance += pay;
        CountForBalanceToUp++;
        history.Add(pay);
        AllHistoryToUp.Add(pay);
    }
}

