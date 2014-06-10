using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;

namespace WellEmulator.Core.Tests
{
    public class StopableThreadTests
    {
        [Test]
        public void At_creating_should_be_stopped()
        {
            // Arrange
            var stopableThread = new StopableThread(new TimeSpan(0, 0, 1), () => { });
            
            // Assert
            Assert.IsFalse(stopableThread.IsRunning);
        }

        [Test]
        public void Thread_should_start()
        {
            // Arrange
            var stopableThread = new StopableThread(new TimeSpan(0, 0, 0, 1), () => { });

            // Act
            stopableThread.Start();
            // Give time for starting
            Thread.Sleep(5);

            // Assert 
            Assert.IsTrue(stopableThread.IsRunning);
        }

        [Test]
        public void Thread_should_stop()
        {
            // Arrange
            var stopableThread = new StopableThread(new TimeSpan(0, 0, 1), () => { });
            
            // Act
            stopableThread.Start();
            stopableThread.Stop();

            // Assert
            Assert.IsFalse(stopableThread.IsRunning);
        }

        public interface ITestThreadJob
        {
            void Do();
        }

        [Test]
        public void Should_call_method_at_least_once()
        {
            // Arrange
            var mockThreadJob = new Mock<ITestThreadJob>();
            var stopableThread = new StopableThread(new TimeSpan(), mockThreadJob.Object.Do);

            // Act
            stopableThread.Start();
            
            // Assert
            mockThreadJob.Verify(job => job.Do(), Times.AtLeastOnce);
        }
    }
}
