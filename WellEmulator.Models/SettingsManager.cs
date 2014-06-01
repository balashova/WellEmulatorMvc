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
                _connectionString = ConfigurationManager.ConnectionStrings["HistorianConnection"].ConnectionString;
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

        public void AddTag(Tag tag)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand(_connectionString, connection)
                {
                    CommandText = string.Format("Insert into WellEmulatorSettings.dbo.Tags " +
                                                "(Group, WellName, Object, Name, Value, MaxValue, MinValue, Delta) values" +
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
                                                "(HistorianTag, PdgtmTag, PdgtmWellName) values" +
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
                    CommandText = string.Format("Insert into WellEmulatorSettings.dbo.MapItems " +
                                                "(Group, WellName, Object, Name, Value, MaxValue, MinValue, Delta) values" +
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

        public List<MapItem> GetMapping()
        {
            return _db.MapItems.ToList();
        }

        public Tag GetTag(int tagId)
        {
            return _db.Tags.SingleOrDefault(t => t.Id == tagId);
        }

        public List<Tag> GetTags()
        {
            return _db.Tags.ToList();
        }
    }
}
