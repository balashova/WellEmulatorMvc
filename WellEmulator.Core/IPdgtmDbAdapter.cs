using System;
using System.Collections.Generic;
using WellEmulator.Models;

namespace WellEmulator.Core
{
    public interface IPdgtmDbAdapter
    {
        int GetWellId(string wellName);
        IEnumerable<Well> GetWells();
        IEnumerable<string> GetTags(string wellName);
        void InsertValues(int wellId, double oilRate, double gasRate, double waterRate);
        List<PdgtmValue> GetValues(TimeSpan range);
    }
}