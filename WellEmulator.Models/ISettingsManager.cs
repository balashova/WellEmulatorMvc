using System.Collections.Generic;

namespace WellEmulator.Models
{
    public interface ISettingsManager
    {
        void SaveSettings(Settings settings);
        Settings GetSettings();
        bool IsExist(Tag tag);
        void AddTag(Tag tag);
        void RemoveTag(Tag tag);
        void AddMapItem(MapItem mapItem);
        void RemoveMapItems(IEnumerable<int> mapIds);
        List<MapItem> GetMappings();
        IEnumerable<MapItem> GetMappings(string wellName, Db db);
        Tag GetTag(int tagId);
        List<Tag> GetTags();
    }
}