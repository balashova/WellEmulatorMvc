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
        private readonly string _historianConnectionString; 
        private readonly string _pdgtmConnectionString;
        private readonly Logger _logger = LogManager.GetCurrentClassLogger();

        public event EventHandler OnHistorianDataChanged;
        public event EventHandler OnPdgtmDataChanged;

        public DatabaseObserver(
            string historianConnectionString, string pdgtmConnectionString)
        {
            _historianConnectionString = historianConnectionString;
            _pdgtmConnectionString = pdgtmConnectionString;

            SqlDependency.Start(_historianConnectionString);
            SqlDependency.Start(_pdgtmConnectionString);
            ObserveHistorian();
            ObservePdgtm();
        }

        public void ObserveHistorian()
        {
            try
            {
                using (var connection = new SqlConnection(_historianConnectionString))
                {
                    connection.Open();
                    using (
                        var command = new SqlCommand(@"SELECT [Id],[TagName],[Value],[Time] FROM [dbo].[History]",
                            connection))
                    {
                        command.Notification = null;
                        var dependency = new SqlDependency(command);
                        dependency.OnChange += Historian_OnChange;
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Error("Error observing History table", ex);
                throw;
            }
        }

        public void ObservePdgtm()
        {
            try
            {
                using (var connection = new SqlConnection(_pdgtmConnectionString))
                {
                    connection.Open();
                    using (
                        var command =
                            new SqlCommand(
                                @"SELECT [Id],[WellId],[OilRate],[GasRate],[WaterRate],[Time] FROM [dbo].[Values]",
                                connection))
                    {
                        command.Notification = null;
                        var dependency = new SqlDependency(command);
                        dependency.OnChange += Pdgtm_OnChange;
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Error("Error observing PDGTM table", ex);
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
            SqlDependency.Stop(_historianConnectionString);
            SqlDependency.Stop(_pdgtmConnectionString);
        }
    }
}
