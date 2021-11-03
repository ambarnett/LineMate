using System.Collections.Generic;
using LineMate.Models;

namespace LineMate.Repositories
{
    public interface ISpecialTeamsRepository
    {
        void Add(SpecialTeams specialTeams);
        void Delete(int id);
        List<SpecialTeams> GetAllSpecialTeams();
        SpecialTeams GetSpecialTeamsById(int id);
        void Update(SpecialTeams specialTeams);
    }
}