using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WellEmulator.Core.Annotations;
using WellEmulator.Models;

namespace WellEmulator.Core
{
    public class Emulator
    {
        public bool IsRunning
        {
            get { return _isRunning; }
        }

        public TimeSpan AutoSaveReportPeriod
        {
            get { return _autoSaveReportPeriod; }
            set
            {
                if (value.Equals(default(TimeSpan))) throw new InvalidTimeSpanException();
                _autoSaveReportPeriod = value;
                if (_isRunning) AutoSaveThreadRestart();
            }
        }

        public TimeSpan GenerationPeriod
        {
            get { return _generationPeriod; }
            set
            {
                if (value.Equals(default(TimeSpan))) throw new InvalidTimeSpanException();
                _generationPeriod = value;
                if (_isRunning) EmulationThreadRestart();
            }
        }

        public TimeSpan ValuesDelay
        {
            get { return _valuesDelay; }
            set
            {
                if (value.Equals(default(TimeSpan))) throw new InvalidTimeSpanException();
                _valuesDelay = value;
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
                if (value == null) throw new NullReferenceException();
                _tags = value;
            }
        }

        private readonly Random _random = new Random(DateTime.Now.Second);
        private List<Tag> _tags = new List<Tag>();
        private TimeSpan _autoSaveReportPeriod = new TimeSpan(0, 0, 0, 10);
        private TimeSpan _generationPeriod = new TimeSpan(0, 0, 0, 1);
        private TimeSpan _valuesDelay = new TimeSpan(0, 0, 3, 0);
        private bool _isRunning;
        private StopableThread _emulationThread;
        private StopableThread _autoSaveThread;
        private readonly CsvReporter _reporter;

        public Emulator()
        {
            var path = ConfigurationManager.AppSettings.Get("ReportUpload");
            var directory = new DirectoryInfo(path ?? @"C:\Historian\Data\DataImport\FastLoad");
            if (!directory.Exists) directory.Create();
            _reporter = new CsvReporter(directory);
        }

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
            if (_isRunning) return;

            EmulationThreadStart();
            AutoSaveThreadStart();

            _isRunning = true;
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

            _isRunning = false;
        }

        private void AutoSave()
        {
            _reporter.Save(_valuesDelay);
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
