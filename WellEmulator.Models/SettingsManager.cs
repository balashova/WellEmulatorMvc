using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLog;

namespace WellEmulator.Models
{
    public enum Db
    {
        Historian,
        Pdgtm
    };

    public class SettingsManager : ISettingsManager
    {
        private readonly Logger _logger = LogManager.GetCurrentClassLogger();
        private readonly string _connectionString;

        public SettingsManager(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void SaveSettings(Settings settings)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                try
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
                            settings.SamplingRate.Ticks, settings.ReportAutoSavePeriod.Ticks,
                            settings.ValuesDelay.Ticks, settings.ReplicationPeriod.Ticks)
                    })
                    {
                        command.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    _logger.Fatal("sql command execution failed", ex);
                    throw;
                }
            }
        }

        public Settings GetSettings()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                try
                {
                    connection.Open();
                    using (var command = new SqlCommand(_connectionString, connection)
                    {
                        CommandText =
                            "select s.SamplingRate, s.ReportAutoSavePeriod, s.ValuesDelay, s.ReplicationPeriod " +
                            "from WellEmulatorSettings.dbo.Settings s;"
                    })
                    {
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                return new Settings
                                {
                                    SamplingRate = TimeSpan.FromTicks(Int64.Parse(reader["SamplingRate"].ToString())),
                                    ReportAutoSavePeriod =
                                        TimeSpan.FromTicks(Int64.Parse(reader["ReportAutoSavePeriod"].ToString())),
                                    ValuesDelay = TimeSpan.FromTicks(Int64.Parse(reader["ValuesDelay"].ToString())),
                                    ReplicationPeriod =
                                        TimeSpan.FromTicks(Int64.Parse(reader["ReplicationPeriod"].ToString()))
                                };
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    _logger.Fatal("sql command execution failed", ex);
                    throw;
                }
            }
            return null;
        }

        public bool IsExist(Tag tag)
        {
            var tagComparer = new TagComparer();
            return GetTags().Contains(tag, tagComparer);
        }

        public void AddTag(Tag tag)
        {
            if (IsExist(tag))
            {
                throw new Exception("Такой элемент уже содержится в базе данных");
            }
            using (var connection = new SqlConnection(_connectionString))
            {
                try
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
                }
                catch (Exception ex)
                {
                    _logger.Fatal("sql command execution failed", ex);
                    throw;
                }
            }
        }

        public void RemoveTag(Tag tag)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                try
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
                }
                catch (Exception ex)
                {
                    _logger.Fatal("Removing tag from settings db failed", ex);
                    throw;
                }
            }
        }

        public void AddMapItem(MapItem mapItem)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                try
                {
                    connection.Open();
                    using (var command = new SqlCommand(_connectionString, connection)
                    {
                        CommandText = string.Format("Insert into WellEmulatorSettings.dbo.MapItems " +
                                                    "(HistorianTag, PdgtmTag, PdgtmWellName, HistorianWellName) Values " +
                                                    "('{0}', '{1}', '{2}', '{3}') ",
                            mapItem.HistorianTag, mapItem.PdgtmTag, mapItem.PdgtmWellName, mapItem.HistorianWellName)
                    })
                    {
                        command.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    _logger.Fatal("sql command execution failed", ex);
                    throw;
                }
            }
        }

        public void RemoveMapItems(IEnumerable<int> mapIds)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                try
                {
                    connection.Open();
                    using (var command = new SqlCommand(_connectionString, connection)
                    {
                        CommandText = string.Format("delete from WellEmulatorSettings.dbo.MapItems " +
                                                    "where Id IN({0}); ",
                            mapIds.Aggregate(new StringBuilder(), (builder, map) => builder
                                .Append((builder.Length == 0 ? "" : ", ") + map)))
                    })
                    {
                        command.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    _logger.Fatal("sql command execution failed", ex);
                    throw;
                }
            }
        }

        public List<MapItem> GetMappings()
        {
            var mappings = new List<MapItem>();
            using (var connection = new SqlConnection(_connectionString))
            {
                try
                {
                    connection.Open();
                    using (var command = new SqlCommand(_connectionString, connection)
                    {
                        CommandText = "select m.Id, m.HistorianTag, m.PdgtmTag, m.PdgtmWellName, m.HistorianWellName " +
                                      "from WellEmulatorSettings.dbo.MapItems m;"
                    })
                    {
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                mappings.Add(new MapItem
                                {
                                    Id = Int32.Parse(reader["Id"].ToString()),
                                    HistorianTag = reader["HistorianTag"].ToString(),
                                    PdgtmTag = reader["PdgtmTag"].ToString(),
                                    PdgtmWellName = reader["PdgtmWellName"].ToString(),
                                    HistorianWellName = reader["HistorianWellName"].ToString()
                                });
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    _logger.Fatal("sql command execution failed", ex);
                    throw;
                }
            }
            return mappings;
        }

        public IEnumerable<MapItem> GetMappings(string wellName, Db db)
        {
            var mappings = new List<MapItem>();
            using (var connection = new SqlConnection(_connectionString))
            {
                try
                {
                    connection.Open();
                    using (var command = new SqlCommand(_connectionString, connection)
                    {
                        CommandText =
                            string.Format(
                                "select m.Id, m.HistorianTag, m.PdgtmTag, m.PdgtmWellName, m.HistorianWellName " +
                                "from WellEmulatorSettings.dbo.MapItems m " +
                                "where m.{1} = '{0}'; ", wellName,
                                db == Db.Pdgtm ? "PdgtmWellName" : "HistorianWellName")
                    })
                    {
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                mappings.Add(new MapItem
                                {
                                    Id = Int32.Parse(reader["Id"].ToString()),
                                    HistorianTag = reader["HistorianTag"].ToString(),
                                    PdgtmTag = reader["PdgtmTag"].ToString(),
                                    PdgtmWellName = reader["PdgtmWellName"].ToString(),
                                    HistorianWellName = reader["HistorianWellName"].ToString()
                                });
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    _logger.Fatal("sql command execution failed", ex);
                    throw;
                }
            }
            return mappings;
        }

        public Tag GetTag(int tagId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                try
                {
                    connection.Open();
                    using (var command = new SqlCommand(_connectionString, connection)
                    {
                        CommandText =
                            string.Format(
                                "select t.Id, t.[Group], t.WellName, t.Object, t.Name, t.Value, t.MaxValue, t.MinValue, t.Delta " +
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
                }
                catch (Exception ex)
                {
                    _logger.Fatal("sql command execution failed", ex);
                    throw;
                }
            }
            return null;
        }

        public List<Tag> GetTags()
        {
            var tags = new List<Tag>();
            using (var connection = new SqlConnection(_connectionString))
            {
                try
                {
                    connection.Open();
                    using (var command = new SqlCommand(_connectionString, connection)
                    {
                        CommandText =
                            "select t.Id, t.[Group], t.WellName, t.Object, t.Name, t.Value, t.MaxValue, t.MinValue, t.Delta " +
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
                                    Value = Double.Parse(reader["Value"].ToString()),
                                    MaxValue = Double.Parse(reader["MaxValue"].ToString()),
                                    MinValue = Double.Parse(reader["MinValue"].ToString()),
                                    Delta = Double.Parse(reader["Delta"].ToString()),
                                });
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    _logger.Fatal("sql command execution failed", ex);
                    throw;
                }
            }
            return tags;
        }
    }
}
