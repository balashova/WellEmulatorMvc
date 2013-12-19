using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data.SqlClient;
using WellEmulator.Models;

namespace WellEmulator.Core
{
    public class HistorianAdapter
    {
        private readonly string _connectionString;

        public HistorianAdapter()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["HistorianConnection"].ConnectionString;
        }

        public List<string> GetTagList()
        {
            var tags = new List<string>();
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand(_connectionString, connection)
                {
                    CommandText = "select tag.TagName " +
                                  "from WINL6VE6TCINN4.Runtime.dbo.Tag tag " +
                                  "where tag.IOServerKey = 2"
                })
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            tags.Add(reader["TagName"].ToString());
                        }
                    }
                }
                connection.Close();
            }
            return tags;
        }

        public void AddTag(Tag tag)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand(_connectionString, connection)
                {
                    CommandText = string.Format("Insert into WINL6VE6TCINN4.Runtime.dbo.Tag " +
                                                "(TagName,    IOServerKey, StorageNodeKey, TopicKey, AcquisitionType, " +
                                                "StorageType, TagType,     Description,    Status,   CEVersion) Values " +
                                                "('{0}.{1}',  2,           1,              2,        2,               " +
                                                "3,           1,           '{0}.{1}',      0,        1)", tag.WellName, tag.Name) + "; " +

                                  string.Format("Insert into WINL6VE6TCINN4.Runtime.dbo.AnalogTag " +
                                                "(TagName,   EUKey, MinEU, MaxEU, MinRaw, MaxRaw, RawType, IntegerSize) Values " +
                                                "('{0}.{1}', 44,    {2},   {3},   {2},    {3},    2,       0)",
                                                tag.WellName, tag.Name, tag.MinValue, tag.MaxValue)
                })
                {
                    command.ExecuteNonQuery();
                }

                connection.Close();
            }
        }

        public void RemoveTag(Tag tag)
        {
            RemoveTag(string.Format("{0}.{1}", tag.WellName, tag.Name));
        }

        private void RemoveTag(string tagName)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand(_connectionString, connection)
                {
                    CommandText = string.Format("Delete from WINL6VE6TCINN4.Runtime.dbo.AnalogTag where TagName = '{0}'; " +
                                                "Delete from WINL6VE6TCINN4.Runtime.dbo.Tag where TagName = '{0}'", tagName)
                })
                {
                    command.ExecuteNonQuery();
                }
                connection.Close();
            }
        }

        public Tag GetTag(string tagName)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand(_connectionString, connection)
                {
                    CommandText = string.Format("select t.TagName, t.MinRaw, t.MaxRaw " +
                                  "from WINL6VE6TCINN4.Runtime.dbo.AnalogTag t " +
                                  "where t.TagName = '{0}'", tagName)
                })
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            return new Tag
                            {
                                Name = reader["TagName"].ToString().Split('.').Last(),
                                WellName = reader["TagName"].ToString().Split('.').First(),
                                MaxValue = Int32.Parse(reader["MaxRaw"].ToString()),
                                MinValue = Int32.Parse(reader["MinRaw"].ToString()),
                            };
                        }
                    }
                }
                connection.Close();
            }
            return null;
        }
    }
}
