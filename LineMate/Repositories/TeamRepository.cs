using Microsoft.Extensions.Configuration;
using LineMate.Models;
using LineMate.Utils;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Linq;

namespace LineMate.Repositories
{
    public class TeamRepository : BaseRepository, ITeamRepository
    {
        public TeamRepository(IConfiguration configuration) : base(configuration) { }

        public List<Team> GetAllTeams()
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT 
                                            t.Id, 
                                            t.Name, 
                                            t.CreatedByUserProfileId,
                                                p.FirstName, 
                                                p.LastName, 
                                                p.Position,
                                                p.JerseyNumber,
                                                p.Line, 
                                                p.TeamId,
                                                p.Id AS PlayerId
                                            FROM Team t 
                                            JOIN Players p on p.TeamId = t.Id";
                    var reader = cmd.ExecuteReader();
                    var teams = new List<Team>();
                    while (reader.Read())
                    {
                        teams.Add(new Team()
                        {
                            Id = (int)DbUtils.GetNullableInt(reader, "Id"),
                            Name = DbUtils.GetString(reader, "Name"),
                            CreatedByUserProfileId = (int)DbUtils.GetNullableInt(reader, "CreatedByUserProfileId"),
                            Player = new Player()
                            {
                                Id = DbUtils.GetInt(reader, "PlayerId"),
                                FirstName = DbUtils.GetString(reader, "FirstName"),
                                LastName = DbUtils.GetString(reader, "LastName"),
                                Position = DbUtils.GetString(reader, "Position"),
                                JerseyNumber = DbUtils.GetInt(reader, "JerseyNumber"),
                                TeamId = DbUtils.GetInt(reader, "TeamId"),
                                Line = DbUtils.GetInt(reader, "Line"),
                            }
                        });
                    }
                    reader.Close();

                    return teams;
                }
            }
        }

        public List<Team> GetTeamById(int id)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT 
                                            t.Id, 
                                            t.Name, 
                                            t.CreatedByUserProfileId,
                                                p.FirstName, 
                                                p.LastName, 
                                                p.Position,
                                                p.JerseyNumber,
                                                p.TeamId,
                                                p.Line, 
                                                p.Id AS PlayerId
                                            FROM Team t 
                                            JOIN Players p on t.Id = p.TeamId
                                            WHERE t.Id = @id";
                    DbUtils.AddParameter(cmd, "@id", id);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        var team = new List<Team>();
                        while (reader.Read())
                        {
                            var existingTeam = team.FirstOrDefault(t => t.Id == id);
                            if(existingTeam == null)
                            {
                                existingTeam = new Team()
                                {
                                    Id = id,
                                    Name = DbUtils.GetString(reader, "Name"),
                                    CreatedByUserProfileId = DbUtils.GetInt(reader, "CreatedByUserProfileId"),
                                    Players = new List<Player>(),
                                };
                                team.Add(existingTeam);
                            }
                            if (DbUtils.IsNotDbNull(reader, "PlayerId"))
                            {
                                existingTeam.Players.Add(new Player()
                                {
                                    Id = DbUtils.GetInt(reader, "PlayerId"),
                                    FirstName = DbUtils.GetString(reader, "FirstName"),
                                    LastName = DbUtils.GetString(reader, "LastName"),
                                    Position = DbUtils.GetString(reader, "Position"),
                                    JerseyNumber = DbUtils.GetInt(reader, "JerseyNumber"),
                                    TeamId = DbUtils.GetInt(reader, "TeamId"),
                                    Line = DbUtils.GetInt(reader, "Line"),

                                });
                            }

                        }
                        return team;
                    }
                }
            }
        }

        public void Add(Team team)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"INSERT INTO Team (Name, CreatedByUserProfileId) 
                                        OUTPUT INSERTED.ID 
                                        VALUES (@Name, @CreatedByUserProfileId)";
                    DbUtils.AddParameter(cmd, "@Name", team.Name);
                    DbUtils.AddParameter(cmd, "@CreatedByUserProfileId", team.CreatedByUserProfileId);

                    team.Id = (int)cmd.ExecuteScalar();
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
                    cmd.CommandText = "DELETE FROM Team WHERE @Id = Id";
                    DbUtils.AddParameter(cmd, "@Id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Update(Team team)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"UPDATE 
                                            Team 
                                        SET 
                                            Name = @Name, 
                                            CreatedByUserProfileId = @CreatedByUserProfileId 
                                        WHERE Id = @Id";
                    DbUtils.AddParameter(cmd, "@Name", team.Name);
                    DbUtils.AddParameter(cmd, "@CreatedByUserProfileId", team.CreatedByUserProfileId);

                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}

// Copy and use paste special, paste JSON as classes
//public class Rootobject
//{
//    public string name { get; set; }
//    public int age { get; set; }
//    public string city { get; set; }
//}
