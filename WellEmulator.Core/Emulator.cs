using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using NLog;
using WellEmulator.Core.Annotations;
using WellEmulator.Models;

namespace WellEmulator.Core
{
    public class Emulator : IEmulator
    {
        private readonly Random _random = new Random();
        private List<Tag> _tags = new List<Tag>();
        private TimeSpan _autoSaveReportPeriod = new TimeSpan(0, 0, 0, 10);
        private TimeSpan _generationPeriod = new TimeSpan(0, 0, 0, 1);
        private StopableThread _emulationThread;
        private StopableThread _autoSaveThread;
        private readonly IReporter _reporter;

        private Logger _logger = LogManager.GetCurrentClassLogger();

        public Emulator(IReporter reporter)
        {
            ValuesDelay = new TimeSpan(0, 0, 3, 0);
            _reporter = reporter;
        }

        public TimeSpan ValuesDelay { get; set; }
        public bool IsRunning { get; private set; }

        public TimeSpan AutoSaveReportPeriod
        {
            get { return _autoSaveReportPeriod; }
            set
            {
                if (value.Equals(default(TimeSpan))) throw new InvalidTimeSpanException();
                _autoSaveReportPeriod = value;
                if (IsRunning) AutoSaveThreadRestart();
            }
        }

        public TimeSpan GenerationPeriod
        {
            get { return _generationPeriod; }
            set
            {
                if (value.Equals(default(TimeSpan))) throw new InvalidTimeSpanException();
                _generationPeriod = value;
                if (IsRunning) EmulationThreadRestart();
            }
        }
        
        public IEnumerable<string> TagNames
        {
            get
            {
                return _tags.Select(t => t.Name).ToList();
            }
        }

        public List<Tag> Tags
        {
            get { return _tags; }
            set
            {
                if (value == null) throw new NullReferenceException("Tags");
                lock (_tags)
                {
                    _tags = value;
                }
            }
        }

        // Methods

        public void AddTag(Tag tag)
        {
            lock (_tags)
            {
                _tags.Add(tag);
            }
        }

        public Tag GetTag(int tagId)
        {
            return _tags.Single(t => t.Id == tagId);
        }

        public void RemoveTag(Tag tag)
        {
            lock (_tags)
            {
                _tags.Remove(_tags.Single(t => t.Id == tag.Id));
            }
        }

        public void Start()
        {
            if (IsRunning) return;

            EmulationThreadStart();
            AutoSaveThreadStart();

            IsRunning = true;
        }

        public void Restart()
        {
            EmulationThreadRestart();
            AutoSaveThreadRestart();
        }

        public void Stop()
        {
            EmulationThreadStop();
            AutoSaveThreadStop();

            IsRunning = false;
        }

        private void AutoSave()
        {
            _reporter.Delay = ValuesDelay;
            _reporter.Save();
        }

        private void Emulation()
        {
            lock (_tags)
            {
                foreach (var tag in _tags)
                {
                    tag.NextValue(_random);
                    _reporter[string.Format("{0}.{1}", tag.WellName, tag.Name)] = tag.Value;
                }
            }
        }

        private void EmulationThreadStop()
        {
            if (_emulationThread != null) _emulationThread.Stop();
        }

        private void EmulationThreadStart()
        {
            _emulationThread = new StopableThread(_generationPeriod, Emulation);
            _emulationThread.Start();
        }

        private void EmulationThreadRestart()
        {
            EmulationThreadStop();
            EmulationThreadStart();
        }

        private void AutoSaveThreadStop()
        {
            if (_autoSaveThread != null) _autoSaveThread.Stop();
        }

        private void AutoSaveThreadStart()
        {
            _autoSaveThread = new StopableThread(_autoSaveReportPeriod, AutoSave);
            _autoSaveThread.Start();
        }

        private void AutoSaveThreadRestart()
        {
            AutoSaveThreadStop();
            AutoSaveThreadStart();
        }
    }
}
