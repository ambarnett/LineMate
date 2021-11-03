using Microsoft.Extensions.Configuration;
using LineMate.Models;
using LineMate.Utils;
using System.Collections.Generic;

namespace LineMate.Repositories
{
    public class PlayerRepository : BaseRepository, IPlayerRepository
    {
        public PlayerRepository(IConfiguration configuration) : base(configuration) { }

        public List<Player> GetAllPlayers()
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT 
                                            p.Id, 
                                            p.FirstName, 
                                            p.LastName, 
                                            p.Position, 
                                            p.JerseyNumber, 
                                            ISNULL(p.TeamId, 0) AS TeamId, 
                                            p.Line, 
                                            ISNULL(t.Name, 'None') AS Name
                                        FROM Players p 
                                        LEFT JOIN Team t on p.TeamId = t.Id";
                        //@"SELECT 
                        //                    p.Id, 
                        //                    p.FirstName, 
                        //                    p.LastName, 
                        //                    p.Position, 
                        //                    p.JerseyNumber, 
                        //                    p.TeamId, 
                        //                    p.Line, 
                        //                    t.Name
                        //                FROM Players p 
                        //                LEFT JOIN Team t on p.TeamId = t.Id";

                    var reader = cmd.ExecuteReader();
                    var players = new List<Player>();
                    while (reader.Read())
                    {
                        players.Add(new Player()
                        {
                            Id = DbUtils.GetInt(reader, "Id"),
                            FirstName = DbUtils.GetString(reader, "FirstName"),
                            LastName = DbUtils.GetString(reader, "LastName"),
                            Position = DbUtils.GetString(reader, "Position"),
                            JerseyNumber = DbUtils.GetInt(reader, "JerseyNumber"),
                            TeamId = DbUtils.GetInt(reader, "TeamId"),
                            Line = DbUtils.GetInt(reader, "Line"),
                            Team = new Team()
                            {
                                Id = DbUtils.GetInt(reader, "TeamId"),
                                Name = DbUtils.GetString(reader, "Name"),
                            },
                        });
                    }
                    reader.Close();

                    return players;
                }
            }
        }

        public Player GetPlayerById(int id)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT 
                                            p.Id, 
                                            p.FirstName, 
                                            p.LastName, 
                                            p.Position, 
                                            p.JerseyNumber, 
                                            p.TeamId, 
                                            p.Line, 
                                            t.Name
                                        FROM Players p 
                                        LEFT JOIN Team t on p.TeamId = t.Id
                                        WHERE p.Id = @id";
                    DbUtils.AddParameter(cmd, "@id", id);

                    var reader = cmd.ExecuteReader();
                    Player player = null;

                    if (reader.Read())
                    {
                        player = new Player()
                        {
                            Id = id,
                            FirstName = DbUtils.GetString(reader, "FirstName"),
                            LastName = DbUtils.GetString(reader, "LastName"),
                            Position = DbUtils.GetString(reader, "Position"),
                            JerseyNumber = DbUtils.GetInt(reader, "JerseyNumber"),
                            TeamId = DbUtils.GetInt(reader, "TeamId"),
                            Line = DbUtils.GetInt(reader, "Line"),
                            Team = new Team()
                            {
                                Id = DbUtils.GetInt(reader, "TeamId"),
                                Name = DbUtils.GetString(reader, "Name"),
                            }
                        };
                    }
                    reader.Close();
                    return player;
                }
            }
        }

        public void Add(Player player)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"INSERT INTO Players (FirstName, LastName, Position, JerseyNumber, TeamId, Line)
                                        OUTPUT INSERTED.ID
                                        VALUES (@FirstName, @LastName, @Position, @JerseyNumber, @TeamId, @Line)";
                    DbUtils.AddParameter(cmd, "@FirstName", player.FirstName);
                    DbUtils.AddParameter(cmd, "@LastName", player.LastName);
                    DbUtils.AddParameter(cmd, "@Position", player.Position);
                    DbUtils.AddParameter(cmd, "@JerseyNumber", player.JerseyNumber);
                    DbUtils.AddParameter(cmd, "@TeamId", player.TeamId);
                    DbUtils.AddParameter(cmd, "@Line", player.Line);

                    player.Id = (int)cmd.ExecuteScalar();
                }
            }
        }

        public void Delete(int id)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"DELETE FROM Players WHERE @Id = Id";
                    DbUtils.AddParameter(cmd, "@Id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Update(Player player)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"UPDATE 
                                            Players 
                                        SET 
                                            FirstName = @FirstName, 
                                            LastName = @LastName, 
                                            Position = @Position, 
                                            JerseyNumber = @JerseyNumber, 
                                            TeamId = @TeamId, 
                                            Line = @Line 
                                        WHERE Id = @Id";
                    DbUtils.AddParameter(cmd, "@FirstName", player.FirstName);
                    DbUtils.AddParameter(cmd, "@LastName", player.LastName);
                    DbUtils.AddParameter(cmd, "@Position", player.Position);
                    DbUtils.AddParameter(cmd, "@JerseyNumber", player.JerseyNumber);
                    DbUtils.AddParameter(cmd, "@TeamId", player.TeamId);
                    DbUtils.AddParameter(cmd, "@Line", player.Line);
                    DbUtils.AddParameter(cmd, "@Id", player.Id);

                    cmd.ExecuteNonQuery();
                }
            }
        }
    }

}