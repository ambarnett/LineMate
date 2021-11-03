using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using LineMate.Models;
using LineMate.Utils;

namespace LineMate.Repositories
{
    public class SpecialTeamPlayerRepository : BaseRepository, ISpecialTeamPlayerRepository
    {
        public SpecialTeamPlayerRepository(IConfiguration configuration) : base(configuration) { }

        public List<SpecialTeamPlayer> Get(int playerId)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT stp.*, st.Name 
                                        FROM SpecialTeamPlayer stp 
                                        JOIN SpecialTeams st ON stp.playerId = st.Id
                                        WHERE PlayerId = @playerId";
                    DbUtils.AddParameter(cmd, "@playerId", playerId);

                    List<SpecialTeamPlayer> specialTeamPlayers = new List<SpecialTeamPlayer>();

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            specialTeamPlayers.Add(new SpecialTeamPlayer()
                            {
                                Id = DbUtils.GetInt(reader, "Id"),
                                PlayerId = DbUtils.GetInt(reader, "PlayerId"),
                                SpecialTeamId = DbUtils.GetInt(reader, "SpecialTeamId"),
                            });
                        }
                        reader.Close();

                        return specialTeamPlayers;
                    }
                }
            }
        }

        public void Add(SpecialTeamPlayer specialTeamPlayer)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"INSERT INTO SpecialTeamPlayer (PlayerId, SpecialTeamId) 
                                        OUTPUT INSERTED.ID 
                                        VALUES (@playerId, @specialTeamId)";
                    DbUtils.AddParameter(cmd, "@playerId", specialTeamPlayer.PlayerId);
                    DbUtils.AddParameter(cmd, "@specialTeamId", specialTeamPlayer.SpecialTeamId);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Delete(SpecialTeamPlayer specialTeamPlayer)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"DELETE FROM SpecialTeamPlayer 
                                        WHERE PlayerId = @playerId AND SpecialTeamId = @specialTeamId";
                    DbUtils.AddParameter(cmd, "@playerId", specialTeamPlayer.PlayerId);
                    DbUtils.AddParameter(cmd, "@specialTeamId", specialTeamPlayer.SpecialTeamId);

                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}