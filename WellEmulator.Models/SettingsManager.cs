using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WellEmulator.Models
{
    public class SettingsManager
    {
        private readonly string _connectionString;

        public SettingsManager()
        {
            try
            {
                _connectionString = ConfigurationManager.ConnectionStrings["SettingsDb"].ConnectionString;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "\n" + ex.StackTrace, ex);
            }
        }

        public void SaveSettings(Settings settings)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand(_connectionString, connection)
                {
                    CommandText = "truncate table WellEmulatorSettings.dbo.Settings"
                })
                {
                    command.ExecuteNonQuery();
                }

                using (var command = new SqlCommand(_connectionString, connection)
                {
                    CommandText = string.Format("insert into WellEmulatorSettings.dbo.Settings " +
                                  "(SamplingRate, ReportAutoSavePeriod, ValuesDelay, ReplicationPeriod) values " +
                                  "('{0}', '{1}', '{2}', '{3}');",
                                  settings.SamplingRate, settings.ReportAutoSavePeriod,
                                  settings.ValuesDelay, settings.ReplicationPeriod)
                })
                {
                    command.ExecuteNonQuery();
                }
                connection.Close();
            }
        }

        public Settings GetSettings()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand(_connectionString, connection)
                {
                    CommandText = "select s.SamplingRate, s.ReportAutoSavePeriod, s.ValuesDelay, s.ReplicationPeriod " +
                                  "from WellEmulatorSettings.dbo.Settings s;"
                })
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            return new Settings
                            {
                                SamplingRate = TimeSpan.Parse(reader["SamplingRate"].ToString()),
                                ReportAutoSavePeriod = TimeSpan.Parse(reader["ReportAutoSavePeriod"].ToString()),
                                ValuesDelay = TimeSpan.Parse(reader["ValuesDelay"].ToString()),
                                ReplicationPeriod = TimeSpan.Parse(reader["ReplicationPeriod"].ToString()),
                            };
                        }
                    }
                }
                connection.Close();
            }
            return null;
        }

        private bool Exist(Tag tag)
        {
            var tagComparer = new TagComparer();
            return GetTags().Contains(tag, tagComparer);
        }

        public void AddTag(Tag tag)
        {
            if (Exist(tag))
            {
                throw new Exception("Такой элемент уже содержится в базе данных");
            }
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand(_connectionString, connection)
                {
                    CommandText = string.Format("Insert into WellEmulatorSettings.dbo.Tags " +
                                                "([Group], WellName, Object, Name, Value, MaxValue, MinValue, Delta) values " +
                                                "('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}');",
                                                tag.Group, tag.WellName, tag.Object, tag.Name,
                                                tag.Value, tag.MaxValue, tag.MinValue, tag.Delta)
                })
                {
                    command.ExecuteNonQuery();
                }

                connection.Close();
            }
        }

        public void RemoveTag(Tag tag)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand(_connectionString, connection)
                {
                    CommandText = string.Format("delete from WellEmulatorSettings.dbo.Tags " +
                                                "where Id = '{0}'; ", tag.Id)
                })
                {
                    command.ExecuteNonQuery();
                }

                connection.Close();
            }
        }

        public void AddMapItem(MapItem mapItem)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand(_connectionString, connection)
                {
                    CommandText = string.Format("Insert into WellEmulatorSettings.dbo.Settings " +
                                                "(HistorianTag, PdgtmTag, PdgtmWellName) values " +
                                                "('{0}', '{1}', '{2}');",
                                                mapItem.HistorianTag, mapItem.PdgtmTag, mapItem.PdgtmWellName)
                })
                {
                    command.ExecuteNonQuery();
                }
                connection.Close();
            }
        }

        public void RemoveMapItem(MapItem mapItem)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand(_connectionString, connection)
                {
                    CommandText = string.Format("delete from WellEmulatorSettings.dbo.MapItems " +
                                                "where Id = '{0}'; ", mapItem.Id)
                })
                {
                    command.ExecuteNonQuery();
                }
                connection.Close();
            }
        }

        public List<MapItem> GetMapping()
        {
            var mappings = new List<MapItem>();
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand(_connectionString, connection)
                {
                    CommandText = "select m.Id, m.HistorianTag, m.PdgtmTag, m.PdgtmWellName " +
                                  "from WellEmulatorSettings.dbo.MapItems m;"
                })
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            mappings.Add(new MapItem
                            {
                                Id = Int32.Parse(reader["TagName"].ToString()),
                                HistorianTag = reader["HistorianTag"].ToString(),
                                PdgtmTag = reader["PdgtmTag"].ToString(),
                                PdgtmWellName = reader["PdgtmWellName"].ToString()
                            });
                        }
                    }
                }
                connection.Close();
            }
            return mappings;
        }

        public Tag GetTag(int tagId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand(_connectionString, connection)
                {
                    CommandText = string.Format("select t.Id, t.[Group], t.WellName, t.Object, t.Name, t.Value, t.MaxValue, t.MinValue, t.Delta " +
                                  "from WellEmulatorSettings.dbo.Tags t " +
                                  "where t.Id = '{0}';", tagId)
                })
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            return new Tag
                            {
                                Id = Int32.Parse(reader["Id"].ToString()),
                                Group = reader["Group"].ToString(),
                                Object = reader["Object"].ToString(),
                                Name = reader["Name"].ToString(),
                                WellName = reader["WellName"].ToString(),
                                Value = Int32.Parse(reader["Value"].ToString()),
                                MaxValue = Int32.Parse(reader["MaxValue"].ToString()),
                                MinValue = Int32.Parse(reader["MinValue"].ToString()),
                                Delta = Int32.Parse(reader["Delta"].ToString()),
                            };
                        }
                    }
                }
                connection.Close();
            }
            return null;
        }

        public List<Tag> GetTags()
        {
            var tags = new List<Tag>();
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand(_connectionString, connection)
                {
                    CommandText = "select t.Id, t.[Group], t.WellName, t.Object, t.Name, t.Value, t.MaxValue, t.MinValue, t.Delta " +
                                  "from WellEmulatorSettings.dbo.Tags t;"
                })
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            tags.Add(new Tag
                            {
                                Id = Int32.Parse(reader["Id"].ToString()),
                                Group = reader["Group"].ToString(),
                                Object = reader["Object"].ToString(),
                                Name = reader["Name"].ToString(),
                                WellName = reader["WellName"].ToString(),
                                Value = Int32.Parse(reader["Value"].ToString()),
                                MaxValue = Int32.Parse(reader["MaxValue"].ToString()),
                                MinValue = Int32.Parse(reader["MinValue"].ToString()),
                                Delta = Int32.Parse(reader["Delta"].ToString()),
                            });
                        }
                    }
                }
                connection.Close();
            }
            return tags;
        }
    }
}
