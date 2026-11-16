namespace PokerLibrary.Data;

public class DbManager
{
    public void AddPlayerToDatabase(string FirstName, string LastName, decimal Balance, int id)
    {
        using (var db = new Context())
        {
            var player = new Player(FirstName, LastName, 1500);
            db.player.Add(player);
            db.SaveChanges();
        }

    }

    public List<Player> GetAllPlayer()
    {
        using (var db = new Context())
        {
            return db.player.ToList();
        }
    }

    public void RemovePlayerFromDb(int id)
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

