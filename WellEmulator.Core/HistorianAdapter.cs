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
            try
            {
                _connectionString = ConfigurationManager.ConnectionStrings["HistorianConnection"].ConnectionString;
            }
            catch (Exception ex)
            {
                throw new HistorianConnectionStringException(ex);
            }
        }

        public IEnumerable<string> GetAllTags()
        {
            var tags = new List<string>();
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand(_connectionString, connection)
                {
                    CommandText = "select tag.TagName " +
                                  "from Runtime.dbo.Tag tag " +
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

        public IEnumerable<string> GetTags(string wellName)
        {
            var allTags = GetAllTags();
            var tags = new List<string>();
            foreach (var tag in allTags)
            {
                var names = tag.Split('.');
                var well = names.First();
                var tagName = names.Last();
                if (well.Equals(wellName)) tags.Add(tagName);
            }
            return tags;
        }

        public Dictionary<string, List<string>> GetTagsGroupingByWell()
        {
            //return GetTagList().GroupBy(t => t.Split('.').First()).
            //                    ToDictionary(well => well.Key, well => well.Select(w => w.Split('.').Last()));

            var tags = GetAllTags();
            var dict = new Dictionary<string, List<string>>();
            foreach (var tag in tags)
            {
                var names = tag.Split('.');
                if (!dict.ContainsKey(names[0])) dict.Add(names[0], new List<string>());
                dict[names[0]].Add(names[1]);
            }
            return dict;
        }

        public IEnumerable<string> GetWells()
        {
            var tags = GetAllTags();
            var wells = new List<string>();
            foreach (var tag in tags)
            {
                var well = tag.Split('.').First();
                if (!wells.Contains(well)) wells.Add(well);
            }
            return wells;
        }

        public void AddTag(Tag tag)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand(_connectionString, connection)
                {
                    CommandText = string.Format("Insert into Runtime.dbo.Tag " +
                                                "(TagName,    IOServerKey, StorageNodeKey, TopicKey, AcquisitionType, " +
                                                "StorageType, TagType,     Description,    Status,   CEVersion, " +
                                                "AITag,       AIHistory) Values " +
                                                "('{0}.{1}',  2,           1,              2,        2, " +
                                                "3,           1,           '{0}.{1}',      0,        0, " +
                                                "'false',     'false')", tag.WellName, tag.Name) + "; " +

                                  string.Format("Insert into Runtime.dbo.AnalogTag " +
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

        /// <summary>
        /// Имя тега в формате "wellName.tagName".
        /// </summary>
        /// <param name="tagName">"wellName.tagName"</param>
        private void RemoveTag(string tagName)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand(_connectionString, connection)
                {
                    CommandText = string.Format("Delete from Runtime.dbo.AnalogTag where TagName = '{0}'; " +
                                                "Delete from Runtime.dbo.Tag where TagName = '{0}'", tagName)
                })
                {
                    command.ExecuteNonQuery();
                }
                connection.Close();
            }
        }

        /// <summary>
        /// Имя тега в формате "wellName.tagName".
        /// </summary>
        /// <param name="tagName">"wellName.tagName"</param>
        /// <returns></returns>
        public Tag GetTag(string tagName)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand(_connectionString, connection)
                {
                    CommandText = string.Format("select t.TagName, t.MinRaw, t.MaxRaw " +
                                  "from Runtime.dbo.AnalogTag t " +
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
                                MaxValue = Double.Parse(reader["MaxRaw"].ToString()),
                                MinValue = Double.Parse(reader["MinRaw"].ToString()),
                            };
                        }
                    }
                }
                connection.Close();
            }
            return null;
        }

        /// <summary>
        /// Имя тега в формате "wellName.tagName".
        /// </summary>
        /// <param name="tags">"wellName.tagName"</param>
        /// <returns></returns>
        public Dictionary<string, List<double>> GetTagValues(List<string> tags)
        {
            var dictionary = new Dictionary<string, List<double>>(tags.Count);
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand(_connectionString, connection)
                {
                    CommandText = string.Format("select h.TagName, h.Value " +
                                                "from Runtime.dbo.History h " +
                                                "where h.TagName in ({0})",
                                                tags.Aggregate(new StringBuilder(), (builder, tag) => builder.
                                                     Append((builder.Length == 0 ? "" : ", ") + "'" + tag + "'")))
                })
                {
                    try
                    {
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var tagName = reader["TagName"].ToString();
                                
                                double value;
                                Double.TryParse(reader["Value"].ToString(), out value);
                                
                                if (dictionary.ContainsKey(tagName))
                                {
                                    dictionary[tagName].Add(value);
                                }
                                else
                                {
                                    dictionary.Add(tagName, new List<double> { value });
                                }
                            }
                        }
                    }
                    catch (SqlException ex)
                    {
                        throw new HistorianServerNotRunningException(ex);
                    }
                }
                connection.Close();
            }
            return dictionary;
        }
    }
}
