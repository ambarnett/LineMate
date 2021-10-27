using LineMate.Models;
using System.Collections.Generic;

namespace LineMate.Repositories
{
    public interface IPlayerRepository
    {
        void Add(Player player);
        void Delete(int id);
        List<Player> GetAllPlayers();
        Player GetPlayerById(int id);
        void Update(Player player);
    }
}