using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Configuration;
using System.Text;
using System.Threading.Tasks;

namespace WellEmulator.Core
{
    public class DatabaseObserver : IDisposable
    {
        public delegate void OnHistorianDataChangeEventHandler();
        public event OnHistorianDataChangeEventHandler OnHistorianDataChanged;

        public delegate void OnPdgtmDataChangeEventHandler();
        public event OnPdgtmDataChangeEventHandler OnPdgtmDataChanged;

        private readonly string _historianConnectionString =
            ConfigurationManager.ConnectionStrings["HistorianConnection"].ConnectionString;

        private readonly string _pdgtmConnectionString =
            ConfigurationManager.ConnectionStrings["TeamworkConnection"].ConnectionString;

        public DatabaseObserver()
        {
            SqlDependency.Start(_historianConnectionString);
            SqlDependency.Start(_pdgtmConnectionString);
            ObserveHistorian();
            ObservePdgtm();
        }

        public void ObserveHistorian()
        {
            using (var connection = new SqlConnection(_historianConnectionString))
            {
                connection.Open();
                using (var command = new SqlCommand(@"SELECT [Id],[TagName],[Value],[Time] FROM [dbo].[History]", connection))
                {
                    command.Notification = null;
                    var dependency = new SqlDependency(command);
                    dependency.OnChange += Historian_OnChange;
                    command.ExecuteNonQuery();
                }
            }
        }

        public void ObservePdgtm()
        {
            using (var connection = new SqlConnection(_pdgtmConnectionString))
            {
                connection.Open();
                using (var command = new SqlCommand(@"SELECT [Id],[WellId],[OilRate],[GasRate],[WaterRate],[Time] FROM [dbo].[Values]", connection))
                {
                    command.Notification = null;
                    var dependency = new SqlDependency(command);
                    dependency.OnChange += Pdgtm_OnChange;
                    command.ExecuteNonQuery();
                }
            }
        }

        private void Historian_OnChange(object sender, SqlNotificationEventArgs e)
        {
            OnHistorianDataChangeEventHandler handler = OnHistorianDataChanged;
            if (handler != null) handler();

            ObserveHistorian();
        }

        private void Pdgtm_OnChange(object sender, SqlNotificationEventArgs e)
        {
            OnPdgtmDataChangeEventHandler handler = OnPdgtmDataChanged;
            if (handler != null) handler();

            ObservePdgtm();
        }

        public void Dispose()
        {
            SqlDependency.Stop(_historianConnectionString);
            SqlDependency.Stop(_pdgtmConnectionString);
        }
    }
}
