using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;

namespace WellEmulator.Core.Tests
{
    [TestFixture]
    public class DbReporterTests
    {
        [Test]
        public void Should_call_insert_method()
        {
            // Arrange
            var mockHistAdapter = new Mock<IHistorianAdapter>();
            var dbReporter = new DbReporter(mockHistAdapter.Object, new Dictionary<string, IList<double>>());

            // Act
            dbReporter.Save();

            // Assert
            mockHistAdapter.Verify(x => x.InsertTagValues(It.IsAny<IDictionary<string, IList<double>>>()), Times.Once);
        }

        [Test]
        public void Should_not_add_new_key_to_dictionary()
        {
            // Arrange
            var mockHistAdapter = new Mock<IHistorianAdapter>();
            var mockDictionary = new Mock<IDictionary<string, IList<double>>>();
            var dbReporter = new DbReporter(mockHistAdapter.Object, mockDictionary.Object);

            IList<double> list = new List<double>();
            mockDictionary
                .Setup(x => x.TryGetValue(It.Is<string>(s => s == "asd"), out list))
                .Returns(true);

            // Act
            dbReporter["asd"] = 1.2;
            dbReporter["asd"] = 1.2;

            // Assert
            mockDictionary.Verify(x => x.Add(It.IsAny<string>(), It.IsAny<IList<double>>()), Times.Never);
        }

        [Test]
        public void Should_add_new_key_to_dictionary()
        {
            // Arrange
            var mockHistAdapter = new Mock<IHistorianAdapter>();
            var mockDictionary = new Mock<IDictionary<string, IList<double>>>();
            var dbReporter = new DbReporter(mockHistAdapter.Object, mockDictionary.Object);
            var keys = new[] { "asd", "dfg" };

            // Act
            foreach (var s in keys)
            {
                dbReporter[s] = 0.1;
            }

            // Assert
            mockDictionary.Verify(x => x.Add(It.IsAny<string>(), It.IsAny<IList<double>>()), Times.Exactly(keys.Length));
        }
    }
}
