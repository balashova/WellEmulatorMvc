using System;
using System.Collections.Generic;
using System.Configuration;
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
    public class PdgtmDbAdapter
    {
        private readonly Logger _logger = LogManager.GetCurrentClassLogger();
        private readonly string _connectionString;

        public PdgtmDbAdapter()
        {
            try
            {
                _connectionString = ConfigurationManager.ConnectionStrings["TeamworkConnection"].ConnectionString;
            }
            catch (Exception ex)
            {
                _logger.FatalException("Connection initialization failed.", ex);
                throw new PDGTMConnectionStringException(ex);
            }
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
                    _logger.FatalException("sql command execution failed", ex);
                    throw;
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        public IEnumerable<Well> GetWells()
        {
            var wells = new List<Well>();
            using (var connection = new SqlConnection(_connectionString))
            {
                try
                {
                    connection.Open();
                    using (var command = new SqlCommand(_connectionString, connection)
                    {
                        CommandText = "select w.id, w.name " +
                                      "from PDGTM.dbo.Well w"
                    })
                    {
                        using (var reader = command.ExecuteReader())
                        {
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
                    _logger.FatalException("sql command execution failed", ex);
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
                    _logger.FatalException(commandText, ex);
                    throw;
                }
                finally
                {
                    connection.Close();
                }
            }
        }
    }
}
