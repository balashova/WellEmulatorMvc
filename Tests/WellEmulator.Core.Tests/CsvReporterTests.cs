using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;

namespace WellEmulator.Core.Tests
{
    [TestFixture]
    public class CsvReporterTests
    {
        [Test]
        public void Index_property_setter_test()
        {
            // Arrange
            var mockCache = new Mock<IList<CsvReporter.CsvStruct>>();
            var reporter = new CsvReporter(new DirectoryInfo(@".\"), mockCache.Object);

            // Act
            reporter["asd"] = 2.2;

            // Assert
            mockCache.Verify(x=> x.Add(It.IsAny<CsvReporter.CsvStruct>()), Times.Once);
        }

        [Test]
        public void Should_save_report()
        {
            // Arrange
            const string dirName = @".\repTemp";
            if (Directory.Exists(dirName))
            {
                Directory.Delete(dirName, true);
            }
            Directory.CreateDirectory(dirName);
            var reporter = new CsvReporter(new DirectoryInfo(dirName), new List<CsvReporter.CsvStruct>());

            // Act
            reporter.Save();

            // Assert
            var dirInfo = new DirectoryInfo(dirName);
            Assert.AreEqual(dirInfo.GetFiles("*.csv").Count(), 1);

            // Cleanup
            if (Directory.Exists(dirName))
            {
                Directory.Delete(dirName, true);
            }    
        }
    }
}
