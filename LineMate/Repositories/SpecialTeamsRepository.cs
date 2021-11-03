using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using LineMate.Models;
using LineMate.Utils;

namespace LineMate.Repositories
{
    public class SpecialTeamsRepository : BaseRepository, ISpecialTeamsRepository
    {
        public SpecialTeamsRepository(IConfiguration configuration) : base(configuration) { }

        public List<SpecialTeams> GetAllSpecialTeams()
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT st.Id, st.Name FROM SpecialTeams st";

                    var reader = cmd.ExecuteReader();
                    var specialTeams = new List<SpecialTeams>();
                    while (reader.Read())
                    {
                        specialTeams.Add(new SpecialTeams()
                        {
                            Id = DbUtils.GetInt(reader, "Id"),
                            Name = DbUtils.GetString(reader, "Name"),
                        });
                    }
                    reader.Close();

                    return specialTeams;
                }
            }
        }

        public SpecialTeams GetSpecialTeamsById(int id)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT st.Id, st.Name FROM SpecialTeams st WHERE st.Id = @id";

                    DbUtils.AddParameter(cmd, "@id", id);

                    var reader = cmd.ExecuteReader();
                    SpecialTeams specialTeams = null;

                    if (reader.Read())
                    {
                        specialTeams = new SpecialTeams()
                        {
                            Id = id,
                            Name = DbUtils.GetString(reader, "Name"),
                        };
                    }
                    reader.Close();
                    return specialTeams;
                }
            }
        }

        public void Add(SpecialTeams specialTeams)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"INSERT INTO SpecialTeams (Name)
                                        OUTPUT INSERTED.ID
                                        VALUES (@Name)";
                    DbUtils.AddParameter(cmd, "@Name", specialTeams.Name);

                    specialTeams.Id = (int)cmd.ExecuteScalar();
                }
            }
        }

        public void Delete(int id)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using ( var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"DELETE FROM SpecialTeams WHERE @Id = Id";
                    DbUtils.AddParameter(cmd, "@Id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Update(SpecialTeams specialTeams)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"UPDATE SpecialTeams SET Name = @Name WHERE Id = @Id";
                    DbUtils.AddParameter(cmd, "@Name", specialTeams.Name);
                    DbUtils.AddParameter(cmd, "@Id", specialTeams.Id);

                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}