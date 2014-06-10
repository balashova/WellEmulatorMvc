using System;
using System.Collections.Generic;
using WellEmulator.Models;

namespace WellEmulator.Core
{
    public interface IReplicator
    {
        bool IsRunning { get; }
        TimeSpan ReplicationPeriod { get; set; }
        List<MapItem> Mappings { get; set; }
        void AddMapping(MapItem mapItem);
        void RemoveMapping(List<int> mapItems);
        void Start();
        void Stop();
    }
}