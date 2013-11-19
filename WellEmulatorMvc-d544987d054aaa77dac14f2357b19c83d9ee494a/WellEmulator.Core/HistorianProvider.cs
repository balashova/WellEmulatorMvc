using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data.SqlClient;

namespace WellEmulator.Core
{
    class HistorianProvider
    {
        private SqlConnection _connection;
        private string _connStr;

        HistorianProvider()
        {
            _connStr = ConfigurationManager.ConnectionStrings["HistorianConnection"].ConnectionString;
        }

        private void OpenConnection()
        {
            _connection.Open();
            _connection.BeginTransaction();

            SqlCommand command = new SqlCommand();
            command.CommandText = "SELECT * From History";
        }
    }
}
