namespace PokerLibrary.Data;

public static class DbManager
{

    public static bool AddBet(int handId, int playerId, decimal amount, out string? error)
    {
        error = null;
        try
        {
            if (amount <= 0)
                throw new ArgumentOutOfRangeException(nameof(amount), "No Money");
            using (Context db = new Context())
            {

                if (db.pokerHand.Find(handId) == null)
                    throw new ArgumentOutOfRangeException(nameof(handId), "No Hand");

                if (db.player.Find(playerId) == null)
                    throw new ArgumentOutOfRangeException(nameof(playerId), "No Player");

                var bet = new Bet()
                {
                    HandId = handId,
                    PlayerId = playerId,
                    Amount = amount

                };
            }
            return true;
        }
        catch (Exception ex)
        {
            error = ex.Message;
            return false;
        }
    }

    public static List<Player>? GetPlayerFromDb(int tableId, out string? error)
    {
        error = null;
        using (Context db = new Context())
        {
            try
            {
                Table? table = db.table.Find(tableId);
                if (db.table.Find(tableId) == null)
                    throw new ArgumentOutOfRangeException(nameof(tableId), "No Table");

                      db.Entry(table)
                    .Collection(nameof(table.player))
                    .Load();

                List<Player> playerInThisGame = new List<Player>(table.player);
                return playerInThisGame;
                
            }
            catch (Exception ex)
            {
                error = ex.Message;
                return new List<Player>(collection: null);
            }
        }
    }

    public static void AddPlayerToDatabase(string FirstName, string LastName, decimal Balance, int id)
    {
        using (var db = new Context())
        {
            var player = new Player(FirstName, LastName, 1500);
            db.player.Add(player);
            db.SaveChanges();
        }
    }

    public static void BalanceToUpFromDb(int id, decimal money)
    {
        using (var db = new Context())
        {
            var player = db.player.Find(id);
            if (player == null)
                throw new ArgumentException("NO PLAYER");
            else
            {
                player.balance += Math.Abs(money);
                db.SaveChanges();
            }
        }
    }

    public static void RemoveMoneyFromBalanceFromDb(int id, decimal money)
    {
        using (var db = new Context())
        {
            var player = db.player.Find(id);
            if (player == null)
                throw new ArgumentException("NO Player");

            if (player.balance < money)
                throw new ArgumentException("NO MONEY");
            else
            {
                player.balance -= Math.Abs(money);
                db.SaveChanges();
            }
        }
    }
    public static List<Player> GetAllPlayer()
    {
        using (var db = new Context())
        {
            return db.player.ToList();
        }
    }

    public static void RemovePlayerFromDb(int id)
    {
        using (var db = new Context())
        {
            var player = db.player.Find(id);
            if (player == null)
                return;

            db.player.Remove(player);
            db.SaveChanges();
        }
    }
}

