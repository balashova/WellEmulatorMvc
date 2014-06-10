using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLog;
using WellEmulator.Core.Annotations;
using WellEmulator.Models;

namespace WellEmulator.Core
{
    public class PdgtmDbAdapter : IPdgtmDbAdapter
    {
        private readonly Logger _logger = LogManager.GetCurrentClassLogger();
        private readonly string _connectionString;

        public PdgtmDbAdapter(string connectionString)
        {
            _connectionString = connectionString;
        }

        public int GetWellId(string wellName)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                try
                {
                    connection.Open();
                    using (var command = new SqlCommand(_connectionString, connection)
                    {
                        CommandText =
                            string.Format("select w.id from PDGTM.dbo.Well w where w.name = '{0}'", wellName)
                    })
                    {
                        return Convert.ToInt32(command.ExecuteScalar());
                    }
                }
                catch (Exception ex)
                {
                    _logger.Fatal(_connectionString, ex);
                    throw;
                }
            }
        }

        public IEnumerable<Well> GetWells()
        {
            var wells = new List<Well>();
            //using ()
            {
                var connection = new SqlConnection(_connectionString);
                try
                {

                    connection.Open();
                    //using (var command = new SqlCommand(_connectionString, connection)
                    //{
                    //    CommandText = "select w.id, w.name " +
                    //                  "from PDGTM.dbo.Well w"
                    //})
                    var command = new SqlCommand(_connectionString, connection)
                    {
                        CommandText = "select w.id, w.name " +
                                      "from PDGTM.dbo.Well w"
                    };
                    {
                        //using ()
                        {
                            var reader = command.ExecuteReader();
                            while (reader.Read())
                            {
                                wells.Add(new Well
                                {
                                    Name = reader["Name"].ToString(),
                                    Id = Int32.Parse(reader["Id"].ToString())
                                });
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    _logger.Fatal(_connectionString, ex);
                    throw;
                }
                finally
                {
                    connection.Close();
                }
            }
            return wells;
        }

        public IEnumerable<string> GetTags(string wellName)
        {
            return new[] { "oilRate", "gasRate", "waterRate" };
        }

        public void InsertValues(int wellId, double oilRate, double gasRate, double waterRate)
        {
            var commandText = string.Format("insert into PDGTM.dbo.[Values] " +
                                            "(wellId, oilRate, gasRate, waterRate) values " +
                                            "({0}, {1}, {2}, {3}); ",
                                            wellId,
                                            oilRate.ToString("F1", CultureInfo.InvariantCulture),
                                            gasRate.ToString("F1", CultureInfo.InvariantCulture),
                                            waterRate.ToString("F1", CultureInfo.InvariantCulture));

            using (var connection = new SqlConnection(_connectionString))
            {
                try
                {
                    connection.Open();
                    using (var command = new SqlCommand(_connectionString, connection)
                    {
                        CommandText = commandText
                    })
                    {
                        command.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    _logger.Fatal(commandText, ex);
                    throw;
                }
            }
        }

        public List<PdgtmValue> GetValues(TimeSpan range)
        {
            List<PdgtmValue> list = null;

            using (var connection = new SqlConnection(_connectionString))
            {
                try
                {
                    connection.Open();
                    using (
                        var command =
                            new SqlCommand(
                                string.Format(
                                    "SELECT [Id],[WellId],[OilRate],[GasRate],[WaterRate],[Time] FROM [dbo].[Values] WHERE [Time] > '{0}'",
                                    DateTime.Now.Subtract(range).ToString("yyyy-MM-dd HH:mm:ss")),
                                connection))
                    {
                        using (var reader = command.ExecuteReader())
                        {
                            list = reader.Cast<IDataRecord>()
                                .Select(x => new PdgtmValue
                                {
                                    Id = x.GetInt32(0),
                                    WellId = x.GetInt32(1),
                                    OilRate = Double.Parse(x[2].ToString()),
                                    GasRate = Double.Parse(x[3].ToString()),
                                    WaterRate = Double.Parse(x[4].ToString()),
                                    Time = x.GetDateTime(5)
                                }).ToList();
                        }
                    }
                }
                catch (SqlException ex)
                {
                    _logger.Fatal(string.Format("Time range: {0}", range), ex);
                    throw;
                }
            }
            return list;
        } 
    }
}
