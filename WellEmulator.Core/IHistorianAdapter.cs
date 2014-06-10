using System;
using System.Collections.Generic;
using WellEmulator.Models;

namespace WellEmulator.Core
{
    public interface IHistorianAdapter
    {
        IEnumerable<string> GetAllTags();
        IEnumerable<string> GetTags(string wellName);
        Dictionary<string, List<string>> GetTagsGroupingByWell();
        IEnumerable<string> GetWells();
        void AddTag(Tag tag);
        void RemoveTag(Tag tag);

        /// <summary>
        /// Имя тега в формате "wellName.tagName".
        /// </summary>
        /// <param name="tagName">"wellName.tagName"</param>
        void RemoveTag(string tagName);

        /// <summary>
        /// Имя тега в формате "wellName.tagName".
        /// </summary>
        /// <param name="tags">"wellName.tagName"</param>
        /// <returns></returns>
        Dictionary<string, double> GetTagValues(List<string> tags);

        void InsertTagValues(Dictionary<string, List<double>> tagsValues);
        List<HistorianValue> GetValues(TimeSpan range);
    }
}