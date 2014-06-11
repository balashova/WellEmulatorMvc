using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using WellEmulator.Core;
using WellEmulator.Models;

namespace WellEmulator.Service.Tests
{
    [TestFixture]
    public class WellEmulatorTests
    {
        private Mock<IEmulator> mockEmulator;
        private Mock<IReplicator> mockReplicator;
        private Mock<IPdgtmDbAdapter> mockPdgtmAdapter;
        private Mock<IHistorianAdapter> mockHistorianAdapter;
        private Mock<ISettingsManager> mockSettingsManager;
        private Mock<IDatabaseObserver> mockDataObserver;
        private Mock<WellEmulator> wellEmulator;

        [SetUp]
        public void Init()
        {
            mockEmulator = new Mock<IEmulator>();
            mockReplicator = new Mock<IReplicator>();
            mockPdgtmAdapter = new Mock<IPdgtmDbAdapter>();
            mockHistorianAdapter = new Mock<IHistorianAdapter>();
            mockSettingsManager = new Mock<ISettingsManager>();
            mockDataObserver = new Mock<IDatabaseObserver>();

            wellEmulator = new Mock<WellEmulator>(
                mockEmulator.Object,
                mockReplicator.Object,
                mockPdgtmAdapter.Object,
                mockHistorianAdapter.Object,
                mockSettingsManager.Object,
                mockDataObserver.Object);
        }
        
        [Test]
        public void Should_load_settings_on_creating()
        {
            // Arrange
            mockSettingsManager.Setup(x => x.GetSettings()).Returns(new Settings() { Id = 123 });
            mockSettingsManager.Setup(x => x.GetTags()).Returns(new List<Tag>() { new Tag() { Id = 234 } });
            mockSettingsManager.Setup(x => x.GetMappings()).Returns(new List<MapItem>() { new MapItem() { Id = 345 } });
            
            // Act. Assignment cause creating WellEMulator object.
            var obj = wellEmulator.Object;

            // Assert
            mockSettingsManager.Verify(x => x.GetSettings(), Times.Once);
            mockSettingsManager.Verify(x => x.GetTags(), Times.Once);
            mockSettingsManager.Verify(x => x.GetMappings(), Times.Once);

            mockEmulator.VerifySet(x => x.Tags = It.Is<List<Tag>>(tags => tags.First().Id == 234));
            mockEmulator.VerifySet(x => x.AutoSaveReportPeriod = It.IsAny<TimeSpan>());
            mockEmulator.VerifySet(x => x.ValuesDelay = It.IsAny<TimeSpan>());
            mockEmulator.VerifySet(x => x.GenerationPeriod = It.IsAny<TimeSpan>());
            
            mockReplicator.VerifySet(x => x.Mappings = It.Is<List<MapItem>>(maps => maps.First().Id == 345));
            mockReplicator.VerifySet(x => x.ReplicationPeriod = It.IsAny<TimeSpan>());
        }

        [Test]
        public void GetSettings_test()
        {
            // Arrange
            var period = TimeSpan.FromMinutes(13);
            mockEmulator.SetupGet(x => x.AutoSaveReportPeriod).Returns(period.Add(TimeSpan.FromMinutes(1)));
            mockEmulator.SetupGet(x => x.ValuesDelay).Returns(period.Add(TimeSpan.FromMinutes(2)));
            mockEmulator.SetupGet(x => x.GenerationPeriod).Returns(period.Add(TimeSpan.FromMinutes(3)));
            mockEmulator.SetupGet(x => x.IsRunning).Returns(true);

            mockReplicator.SetupGet(x => x.ReplicationPeriod).Returns(period.Add(TimeSpan.FromMinutes(4)));
            mockReplicator.SetupGet(x => x.IsRunning).Returns(true);

            // Act
            var settings = wellEmulator.Object.GetSettings();

            // Assert
            Assert.That(settings.ReportAutoSavePeriod == period.Add(TimeSpan.FromMinutes(1)));
            Assert.That(settings.ValuesDelay == period.Add(TimeSpan.FromMinutes(2)));
            Assert.That(settings.SamplingRate == period.Add(TimeSpan.FromMinutes(3)));
            Assert.That(settings.IsRunning);
            Assert.That(settings.ReplicationPeriod == period.Add(TimeSpan.FromMinutes(4)));
            Assert.That(settings.IsReplicate);
        }

        [Test]
        public void AddTag_Test()
        {
            // Arrange
            var tag = new Tag() { Id = 123 };
            
            // Act
            wellEmulator.Object.AddTag(tag);
            
            // Assert
            mockSettingsManager.Verify(x => x.AddTag(It.Is<Tag>(t => t.Id == tag.Id)));
            mockHistorianAdapter.Verify(x => x.AddTag(It.Is<Tag>(t => t.Id == tag.Id)));
            mockEmulator.Verify(x => x.AddTag(It.Is<Tag>(t => t.Id == tag.Id)));
        }

        [Test]
        public void RemoveTag_Test()
        {
            // Arrange
            var tagId = 123;
            mockSettingsManager.Setup(x => x.GetTag(It.Is<int>(tId => tId == tagId))).Returns(new Tag() { Id = tagId });

            // Act
            wellEmulator.Object.RemoveTag(tagId);

            // Assert
            mockSettingsManager.Verify(x => x.GetTag(It.Is<int>(tId => tId == tagId)));
            mockSettingsManager.Verify(x => x.RemoveTag(It.Is<Tag>(t => t.Id == tagId)));
            mockHistorianAdapter.Verify(x => x.RemoveTag(It.Is<Tag>(t => t.Id == tagId)));
            mockEmulator.Verify(x => x.RemoveTag(It.Is<Tag>(t => t.Id == tagId)));
        }

        [Test]
        public void AddMapItem_Test()
        {
            // Arrange
            var mapItem = new MapItem() { Id = 123 };

            // Act
            wellEmulator.Object.AddMapItem(mapItem);

            // Assert
            mockSettingsManager.Verify(x => x.AddMapItem(It.Is<MapItem>(map => map.Id == mapItem.Id)));
            mockReplicator.Verify(x => x.AddMapping(It.Is<MapItem>(map => map.Id == mapItem.Id)));
        }

        [Test]
        public void RemoveMapItems_Test()
        {
            // Arrange
            var mapItemId = 123;
            var mapItemIdList = new List<int>() { mapItemId };

            // Act
            wellEmulator.Object.RemoveMapItems(mapItemIdList);

            // Assert
            mockSettingsManager.Verify(x => x.RemoveMapItems(It.Is<List<int>>(list => list.Contains(mapItemId))));
            mockReplicator.Verify(x => x.RemoveMapping(It.Is<List<int>>(list => list.Contains(mapItemId))));
        }
    }
}
