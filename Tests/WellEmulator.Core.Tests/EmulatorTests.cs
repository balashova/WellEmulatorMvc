using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Moq;
using Moq.Protected;
using NUnit.Framework;
using WellEmulator.Models;

namespace WellEmulator.Core.Tests
{
    [TestFixture]
    public class EmulatorTests
    {
        [Test]
        public void AddTag_Test()
        {
            // Arrange
            var mockList = new Mock<IList<Tag>>();
            var mockReporter = new Mock<IReporter>();
            var emulator = new Emulator(mockReporter.Object, mockList.Object);

            // Act
            emulator.AddTag(new Tag());

            // Assert
            mockList.Verify(x => x.Add(It.IsAny<Tag>()), Times.Once);
        }

        [Test]
        public void GetTag_Test()
        {
            // Arrange
            var list = new List<Tag>() { new Tag() { Id = 123 } };
            var mockReporter = new Mock<IReporter>();
            var emulator = new Emulator(mockReporter.Object, list);

            // Act
            var tag = emulator.GetTag(123);

            // Assert
            Assert.IsNotNull(tag);
        }

        [Test]
        public void RemoveTag_Test()
        {
            // Arrange
            var tag = new Tag() { Id = 123 };
            var list = new List<Tag> { tag };
            var mockReporter = new Mock<IReporter>();
            var emulator = new Emulator(mockReporter.Object, list);

            // Act
            emulator.RemoveTag(tag);

            // Assert
            Assert.IsEmpty(list);
        }

        [Test]
        public void Should_start_and_dump_data()
        {
            // Arrange
            var mockReporter = new Mock<IReporter>();
            var emulator = new Emulator(mockReporter.Object, new List<Tag>())
            {
                GenerationPeriod = new TimeSpan(0, 0, 0, 0, 1)
            };
            emulator.AddTag(new Tag() { Name = "tag", WellName = "well", Value = 10, Delta = 1, MaxValue = 100, MinValue = 0});

            // Act
            emulator.Start();

            // Assert
            mockReporter.VerifySet(x => x["well.tag"] = It.IsAny<double>());
        }
   }
}
