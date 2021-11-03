using LineMate.Models;
using System.Collections.Generic;

namespace LineMate.Repositories
{
    public interface ISpecialTeamPlayerRepository
    {
        void Add(SpecialTeamPlayer specialTeamPlayer);
        void Delete(SpecialTeamPlayer specialTeamPlayer);
        List<SpecialTeamPlayer> Get(int playerId);
    }
}