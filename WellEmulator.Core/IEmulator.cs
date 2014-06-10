using System;
using System.Collections.Generic;
using WellEmulator.Models;

namespace WellEmulator.Core
{
    public interface IEmulator
    {
        TimeSpan ValuesDelay { get; set; }
        bool IsRunning { get; }
        TimeSpan AutoSaveReportPeriod { get; set; }
        TimeSpan GenerationPeriod { get; set; }
        IEnumerable<string> TagNames { get; }
        IList<Tag> Tags { get; set; }
        void AddTag(Tag tag);
        Tag GetTag(int tagId);
        void RemoveTag(Tag tag);
        void Start();
        void Restart();
        void Stop();
    }
}