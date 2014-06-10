using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data.SqlClient;
using NLog;
using WellEmulator.Models;

namespace WellEmulator.Core
{
    public class HistorianAdapter
    {
        private readonly Logger _logger = LogManager.GetCurrentClassLogger();
        private readonly string _connectionString;

        public HistorianAdapter()
        {
            try
            {
                _connectionString = ConfigurationManager.ConnectionStrings["HistorianConnection"].ConnectionString;
            }
            catch (Exception ex)
            {
                _logger.FatalException("Connection initialization failed.", ex);
                throw new HistorianConnectionStringException(ex);
            }
        }

        public IEnumerable<string> GetAllTags()
        {
            var tags = new List<string>();
            using (var connection = new SqlConnection(_connectionString))
            {
                try
                {
                    connection.Open();
                    using (var command = new SqlCommand(_connectionString, connection)
                    {
                        CommandText = "select tag.TagName " +
                                      "from Runtime.dbo.Tag tag "
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
                }
                catch (Exception ex)
                {
                    _logger.FatalException("sql command execution failed", ex);
                    throw;
                }
                finally
                {
                    connection.Close();
                }
            }
            return tags;
        }

        public IEnumerable<string> GetTags(string wellName)
        {
            var allTags = GetAllTags();
            var tags = from tag in allTags
                select tag.Split('.')
                into names
                let well = names.First()
                let tagName = names.Last()
                where well.Equals(wellName)
                select tagName;
            return tags;
        }

        public Dictionary<string, List<string>> GetTagsGroupingByWell()
        {
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
                try
                {
                    connection.Open();
                    using (var command = new SqlCommand(_connectionString, connection)
                    {
                        CommandText = string.Format("Insert into Runtime.dbo.Tag " +
                                                    "(TagName) Values ('{0}.{1}');", tag.WellName, tag.Name)
                    })
                    {
                        command.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    _logger.FatalException("sql command execution failed", ex);
                    throw;
                }
                finally
                {
                    connection.Close();
                }
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
                try
                {
                    connection.Open();
                    using (var command = new SqlCommand(_connectionString, connection)
                    {
                        CommandText = string.Format("Delete from Runtime.dbo.Tag where TagName = '{0}'; " +
                                                    "Delete from Runtime.dbo.History where TagName = '{0}'", tagName)
                    })
                    {
                        command.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    _logger.FatalException("sql command execution failed", ex);
                    throw;
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        /// <summary>
        /// Имя тега в формате "wellName.tagName".
        /// </summary>
        /// <param name="tags">"wellName.tagName"</param>
        /// <returns></returns>
        public Dictionary<string, double> GetTagValues(List<string> tags)
        {
            var dictionary = new Dictionary<string, double>(tags.Count);
            using (var connection = new SqlConnection(_connectionString))
            {
                try
                {
                    connection.Open();
                    using (var command = new SqlCommand(_connectionString, connection)
                    {
                        CommandText = string.Format("select h.TagName, h.Value " +
                                                    "from Runtime.dbo.History h " +
                                                    "where h.Id in (select max(Id) " +
                                                    "from Runtime.dbo.History where TagName in ({0}) " +
                                                    "group by TagName ); ",
                            tags.Aggregate(new StringBuilder(), (builder, tag) => builder.
                                Append((builder.Length == 0 ? "" : ", ") + "'" + tag + "'")))
                    })
                    {
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var tagName = reader["TagName"].ToString();
                                var value = Double.Parse(reader["Value"].ToString());

                                dictionary.Add(tagName, value);
                            }
                        }
                    }
                }
                catch (SqlException ex)
                {
                    _logger.FatalException(
                        tags.Aggregate("", (s, tag) => s += (s.Length == 0 ? "" : ", ") + "'" + tag + "'"), ex);
                    throw new HistorianServerNotRunningException(ex);
                }
                finally
                {
                    connection.Close();
                }
            }
            return dictionary;
        }

        public void InsertTagValues(Dictionary<string, List<double>> tagsValues)
        {
            if(tagsValues == null || !tagsValues.Any()) return;

            var com = new StringBuilder("insert into Runtime.dbo.History (TagName, Value) Values ");
            var isFirstRow = true;
            foreach (var tag in tagsValues)
            {
                foreach (var value in tag.Value)
                {
                    if (!isFirstRow) com.Append(",");
                    else isFirstRow = false;
                    com.Append(" ('" + tag.Key + "', '" + value.ToString("F1", CultureInfo.InvariantCulture) + "') ");
                }
            }

            using (var connection = new SqlConnection(_connectionString))
            {
                try
                {
                    connection.Open();
                    using (var command = new SqlCommand(_connectionString, connection)
                    {
                        CommandText = com.ToString()
                    })
                    {
                        command.ExecuteNonQuery();
                    }
                }
                catch (SqlException ex)
                {
                    _logger.FatalException(com.ToString(), ex);
                    throw new HistorianServerNotRunningException(ex);
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        public List<HistorianValue> GetValues(int number)
        {
            List<HistorianValue> list = null;
            using (var connection = new SqlConnection(_connectionString))
            {
                try
                {
                    connection.Open();
                    using (
                        var command =
                            new SqlCommand(
                                string.Format("SELECT TOP {0} [Id], [TagName], [Value], [Time] FROM [dbo].[History] ORDER BY [Id] DESC",
                                    number),
                                connection))
                    {
                        using (var reader = command.ExecuteReader())
                        {
                            list = reader.Cast<IDataRecord>()
                                .Select(x => new HistorianValue
                                {
                                    Id = x.GetInt32(0),
                                    TagName = x.GetString(1),
                                    Value = Double.Parse(x[2].ToString()),
                                    Time = x.GetDateTime(3)
                                }).ToList();
                        }
                    }
                }
                catch (SqlException ex)
                {
                    _logger.FatalException(string.Format("Row number: {0}", number), ex);
                    throw;
                }
                finally
                {
                    connection.Close();
                }
            }
            return list;
        } 
    }
}
