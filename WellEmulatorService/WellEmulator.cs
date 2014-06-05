using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WellEmulator.Core;
using WellEmulator.Models;
using System.Configuration;
using System.Windows.Forms;

namespace WellEmulatorService
{
    public class WellEmulator : IWellEmulator
    {
        private readonly WellEmulatorSingle _wellEmulator = WellEmulatorSingle.Instance;

        public void DisableReplication()
        {
            _wellEmulator.DisableReplication();
        }

        public void EnableReplication()
        {
            _wellEmulator.EnableReplication();
        }

        public bool IsReplicate()
        {
            return _wellEmulator.IsReplicate();
        }

        public bool IsRunning()
        {
            return _wellEmulator.IsRunning();
        }

        public void Start()
        {
            _wellEmulator.Start();
        }

        public void Stop()
        {
            _wellEmulator.Stop();
        }

        public void SetSettings(Settings settings)
        {
            _wellEmulator.SetSettings(settings);
        }

        public Settings GetSettings()
        {
            return _wellEmulator.GetSettings();
        }

        public void AddTag(Tag tag)
        {
            _wellEmulator.AddTag(tag);
        }

        public void RemoveTag(int tagId)
        {
            _wellEmulator.RemoveTag(tagId);
        }

        public Tag GetTag(int tagId)
        {
            return _wellEmulator.GetTag(tagId);
        }

        public IEnumerable<string> GetWitsmlObjects(string standard)
        {
            return _wellEmulator.GetWitsmlObjects(standard);
        }

        public IEnumerable<string> GetWitsmlElements(string standard, string @object)
        {
            return _wellEmulator.GetWitsmlElements(standard, @object);
        }

        public void AddMapItem(MapItem mapItem)
        {
            _wellEmulator.AddMapItem(mapItem);
        }

        public void RemoveMapItems(List<int> mapItems)
        {
            _wellEmulator.RemoveMapItems(mapItems);
        }

        public IEnumerable<MapItem> GetMappings()
        {
            return _wellEmulator.GetMappings();
        }

        public IEnumerable<Tag> GetSettingsTags()
        {
            return _wellEmulator.GetSettingsTags();
        }

        public IEnumerable<string> GetAllHistTags()
        {
            return _wellEmulator.GetAllHistTags();
        }

        public IEnumerable<string> GetHistWells()
        {
            return _wellEmulator.GetHistWells();
        }

        public IEnumerable<string> GetHistTags(string wellName)
        {
            return _wellEmulator.GetHistTags(wellName);
        }

        public IEnumerable<string> GetPdgtmTags(string wellName)
        {
            return _wellEmulator.GetPdgtmTags(wellName);
        }

        public IEnumerable<Well> GetPdgtmWells()
        {
            return _wellEmulator.GetPdgtmWells();
        }

        public IEnumerable<string> GetNotMappedHistTags(string wellName)
        {
            return _wellEmulator.GetNotMappedHistTags(wellName);
        }

        public IEnumerable<string> GetNotMappedPdgtmTags(string wellName)
        {
            return _wellEmulator.GetNotMappedPdgtmTags(wellName);
        }
    }
}
