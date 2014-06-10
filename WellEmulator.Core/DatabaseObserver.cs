using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Linq;
using System.Configuration;
using System.Text;
using System.Threading.Tasks;
using NLog;

namespace WellEmulator.Core
{
    public class DatabaseObserver : IDisposable, IDatabaseObserver
    {
        private readonly List<string> _connectionStrings = new List<string>(); 
        private readonly Logger _logger = LogManager.GetCurrentClassLogger();

        private readonly string _historianConnectionString;
        private readonly string _pdgtmConnectionString;

        public event EventHandler OnHistorianDataChanged;
        public event EventHandler OnPdgtmDataChanged;

        public DatabaseObserver(
            string historianConnectionString, string pdgtmConnectionString)
        {
            _historianConnectionString = historianConnectionString;
            _pdgtmConnectionString = pdgtmConnectionString;

            _connectionStrings.Add(historianConnectionString);
            _connectionStrings.Add(pdgtmConnectionString);

            SqlDependency.Start(_historianConnectionString);
            SqlDependency.Start(_pdgtmConnectionString);
            
            ObserveHistorian();
            ObservePdgtm();
        }

        public DatabaseObserver()
        {      
        }

        public void StartObserverOn(string connectionString)
        {
            _connectionStrings.Add(connectionString);
            SqlDependency.Start(connectionString);
        }

        public void ObserveHistorian()
        {
            Observe(@"SELECT [Id],[TagName],[Value],[Time] FROM [dbo].[History]", 
                _historianConnectionString,
                Historian_OnChange);
        }

        public void ObservePdgtm()
        {
            Observe(@"SELECT [Id],[WellId],[OilRate],[GasRate],[WaterRate],[Time] FROM [dbo].[Values]",
                _pdgtmConnectionString,
                Pdgtm_OnChange);
        }

        public void Observe(string query, string connectionString, OnChangeEventHandler func)
        {
            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Notification = null;
                        var dependency = new SqlDependency(command);
                        dependency.OnChange += func;
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Error("Error observing table. Query: {0}", query, ex);
                throw;
            }
        }

        private void Historian_OnChange(object sender, SqlNotificationEventArgs e)
        {
            EventHandler handler = OnHistorianDataChanged;
            if (handler != null) handler(this, e);

            ObserveHistorian();
        }

        private void Pdgtm_OnChange(object sender, SqlNotificationEventArgs e)
        {
            EventHandler handler = OnPdgtmDataChanged;
            if (handler != null) handler(this, e);
            
            ObservePdgtm();
        }

        public void Dispose()
        {
            _connectionStrings.ForEach(connStr => SqlDependency.Stop(connStr));
        }
    }
}
