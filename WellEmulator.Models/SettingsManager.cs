using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WellEmulator.Models
{
    public class SettingsManager
    {
        private readonly SettingsDb _db;

        public SettingsManager()
        {
            try
            {
                _db = new SettingsDb();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "\n" + ex.StackTrace, ex);
            }
        }

        public void SaveSettings(Settings settings)
        {
            if (_db.Settings.Any())
            {
                foreach (var setting in _db.Settings)
                {
                    _db.Settings.Remove(setting);
                }
            }
            _db.Settings.Add(settings);
            _db.SaveChanges();
        }

        public Settings GetSettings()
        {
            var settings = _db.Settings.ToList();
            return settings.Any() ? settings.Last() : null;
        }

        public void AddTag(Tag tag)
        {
            _db.Tags.Add(tag);
            _db.SaveChanges();
        }

        public void RemoveTag(Tag tag)
        {
            _db.Tags.SqlQuery(string.Format("delete from WellEmulatorSettings.dbo.Tags " +
                                            "where Name = '{0}' and " +
                                            "where WellName = '{1}'",
                                            tag.Name, tag.WellName));
            _db.SaveChanges();
        }

        public void AddMapItem(MapItem mapItem)
        {
            _db.MapItems.Add(mapItem);
            _db.SaveChanges();
        }

        public void RemoveMapItem(MapItem mapItem)
        {
            _db.Tags.SqlQuery(string.Format("delete from WellEmulatorSettings.dbo.Tags " +
                                            "where HistorianTag = '{0}' and " +
                                            "where PdgtmTag = '{1}' and " +
                                            "where PdgtmWellName = '{2}';",
                                            mapItem.HistorianTag, mapItem.PdgtmTag, mapItem.PdgtmWellName));
            _db.SaveChanges();
        }

        public List<MapItem> GetMapping()
        {
            return _db.MapItems.ToList();
        }

        public List<Tag> GetTags()
        {
            return _db.Tags.ToList();
        }
    }
}
