using Microsoft.Extensions.Configuration;
using LineMate.Models;
using LineMate.Utils;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;

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
                                            t.CreatedByUserProfileId
                                        FROM 
                                            Team t";
                    var reader = cmd.ExecuteReader();
                    var teams = new List<Team>();
                    while (reader.Read())
                    {
                        teams.Add(new Team()
                        {
                            Id = (int)DbUtils.GetNullableInt(reader, "Id"),
                            Name = DbUtils.GetString(reader, "Name"),
                            CreatedByUserProfileId = (int)DbUtils.GetNullableInt(reader, "CreatedByUserProfileId"),
                        });
                    }
                    reader.Close();

                    return teams;
                }
            }
        }

        public Team GetTeamById(int id)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT t.Id, t.Name, t.CreatedByUserProfileId FROM Team t WHERE t.Id = @Id";
                    DbUtils.AddParameter(cmd, "@Id", id);

                    Team team = null;

                    var reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        team = new Team()
                        {
                            Id = id,
                            Name = DbUtils.GetString(reader, "Name"),
                            CreatedByUserProfileId = DbUtils.GetInt(reader, "CreatedByUserId"),
                        };

                    }
                    reader.Close();
                    return team;
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
