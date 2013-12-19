using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WellEmulator.Core.Annotations;
using WellEmulator.Models;

namespace WellEmulator.Core
{
    public class PdgtmDbAdapter
    {
        private readonly string _connectionString;

        public PdgtmDbAdapter()
        {
            try
            {
                _connectionString = ConfigurationManager.ConnectionStrings["TeamworkConnection"].ConnectionString;
            }
            catch (Exception ex)
            {
                throw new PDGTMConnectionStringException(ex);
            }
        }

        public IEnumerable<Well> GetWells()
        {
            var wells = new List<Well>();
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand(_connectionString, connection)
                {
                    CommandText = "select w.idWell, w.nameWell " +
                                  "from PDGTM.dbo.Well w"
                })
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            wells.Add(new Well
                            {
                                Name = reader["nameWell"].ToString(),
                                Id = Int32.Parse(reader["idWell"].ToString())
                            });
                        }
                    }
                }
                connection.Close();
            }
            return wells;
        }

        public void InsertValues(int wellId, double oilRate, double gasRate, double waterRate)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (var command = new SqlCommand(_connectionString, connection)
                {
                    CommandText = string.Format("insert into PDGTM.dbo.WellTest" +
                                                "(idWell, name,          testDate) values (" +
                                                "{0},     'Тест от {1}', '{2}'); ", wellId,
                                                DateTime.Now.Date,
                                                DateTime.Now)
                })
                {
                    command.ExecuteNonQuery();
                }

                int lastId;

                using (var command = new SqlCommand(_connectionString, connection)
                {
                    CommandText = "SELECT @@IDENTITY AS 'Identity';"
                })
                {
                    lastId = Convert.ToInt32(command.ExecuteScalar());
                }

                using (var command = new SqlCommand(_connectionString, connection)
                {
                    CommandText = string.Format("insert into PDGTM.dbo.ProductionTest (idWellTest) values ({0})", lastId)
                })
                {
                    command.ExecuteNonQuery();
                }

                using (var command = new SqlCommand(_connectionString, connection)
                {
                    CommandText = "SELECT @@IDENTITY AS 'Identity';"
                })
                {
                    lastId = Convert.ToInt32(command.ExecuteScalar());
                }

                using (var command = new SqlCommand(_connectionString, connection)
                {
                    CommandText = string.Format("insert into PDGTM.dbo.ProductionTestResults" +
                                                "(idProductionTest, oilRate, gasRate, waterRate) values (" +
                                                "{0}, {1}, {2}, {3} ); ",
                                                lastId, oilRate, gasRate, waterRate)
                })
                {
                    command.ExecuteNonQuery();
                }

                connection.Close();
            }
        }
    }
}
