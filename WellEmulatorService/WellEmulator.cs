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

        public void RemoveTag(Tag tag)
        {
            _wellEmulator.RemoveTag(tag);
        }

        public void RemoveTagByName(string tagName)
        {
            _wellEmulator.RemoveTagByName(tagName);
        }

        public Tag GetTag(string name)
        {
            return _wellEmulator.GetTag(name);
        }

        public IEnumerable<string> GetTagList(string wellName)
        {
            return _wellEmulator.GetTagList(wellName);
        }

        public IEnumerable<Tag> GetTags()
        {
            return _wellEmulator.GetTags();
        }

        public IEnumerable<Well> GetWellList()
        {
            return _wellEmulator.GetWellList();
        }
    }
}
