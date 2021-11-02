using LineMate.Models;
using System.Collections.Generic;

namespace LineMate.Repositories
{
    public interface ITeamRepository
    {
        List<Team> GetAllTeams();
        List<Team> GetTeamById(int id);
        void Add(Team team);
        void Delete(int id);
        void Update(Team team);
    }
}