using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;

namespace WellEmulator.Core.Tests
{
    [TestFixture]
    public class DatabaseObserverTests
    {
        private string _connStr;

        [TestFixtureSetUp]
        public void Init()
        {
            _connStr = @"Data Source=.\SQLEXPRESS;Initial Catalog=master;Integrated Security=true;";

            var sql =
                "IF EXISTS(select * from sys.databases where name='Runtime_Test') " +
                "BEGIN " +
                "    ALTER DATABASE [Runtime_Test] SET SINGLE_USER WITH ROLLBACK IMMEDIATE " +
                "    DROP DATABASE [Runtime_Test] " +
                "END " +
                "CREATE DATABASE [Runtime_Test] " ;
            var sql2 = 
                "ALTER DATABASE [Runtime_Test] SET ENABLE_BROKER WITH ROLLBACK IMMEDIATE " +
                "CREATE TABLE [Runtime_Test].[dbo].[Well] ( " +
                "    [Id]   INT          IDENTITY (1, 1) NOT NULL, " +
                "    [Name] VARCHAR (50) NULL, " +
                "    CONSTRAINT [PK_Well] PRIMARY KEY CLUSTERED ([Id] ASC) " +
                ") ";
            using (var connection = new SqlConnection(_connStr))
            {
                connection.Open();
                using (var command = new SqlCommand(sql,  connection))
                {
                    command.ExecuteNonQuery();
                }
                using (var command = new SqlCommand(sql2, connection))
                {
                    command.ExecuteNonQuery();
                }
            }
        }

        public interface IStub
        {
            void Do(object s, EventArgs e);
        }

        [Test]
        public void Should_raise_event_after_insert()
        {
            // Arrange 
            var connStr = @"Data Source=.\SQLEXPRESS;Initial Catalog=Runtime_Test;Integrated Security=true;";
            var mock = new Mock<IStub>();
            var observer = new DatabaseObserver();
            observer.StartObserverOn(connStr);
            observer.Observe(@"SELECT [Id], [Name] FROM [Runtime_Test].[dbo].[Well]", connStr, mock.Object.Do);

            // Act
            using (var connection = new SqlConnection(connStr))
            {
                connection.Open();
                using (var command = new SqlCommand(@"INSERT INTO [Runtime_Test].[dbo].[Well] ([Name]) VALUES ('Name1');", connection))
                {
                    command.ExecuteNonQuery();
                }
            }

            // Assert
            mock.Verify(x => x.Do(It.IsAny<object>(), It.IsAny<EventArgs>()), Times.Once);
        }

        [TestFixtureTearDown]
        public void Cleanup()
        {
            var sql = "IF EXISTS(select * from sys.databases where name='Runtime_Test') " +
                      "BEGIN " +
                      "    ALTER DATABASE [Runtime_Test] SET SINGLE_USER WITH ROLLBACK IMMEDIATE " +
                      "    DROP DATABASE [Runtime_Test] " +
                      "END ";
            using (var connection = new SqlConnection(_connStr))
            {
                connection.Open();
                using (var command = new SqlCommand(sql, connection))
                {
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
