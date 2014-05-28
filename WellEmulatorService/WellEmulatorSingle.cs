﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WellEmulator.Core;
using WellEmulator.Models;
using WellEmulatorService.Parsers;

namespace WellEmulatorService
{
    public class WellEmulatorSingle : IWellEmulator
    {
        private static readonly object SyncRoot = new object();
        private static volatile WellEmulatorSingle _instance;

        private readonly Emulator _emulator;
        private readonly Replicator _replicator;
        private readonly PdgtmDbAdapter _pdgtmDbAdapter;
        private readonly HistorianAdapter _historianAdapter;
        private readonly SettingsManager _settingsManager;

        public static WellEmulatorSingle Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (SyncRoot)
                    {
                        if (_instance == null) _instance = new WellEmulatorSingle();
                    }
                }
                return _instance;
            }
        }

        private WellEmulatorSingle()
        {
            try
            {
                _emulator = new Emulator();
                _replicator = new Replicator();
                _pdgtmDbAdapter = new PdgtmDbAdapter();
                _historianAdapter = new HistorianAdapter();
                _settingsManager = new SettingsManager();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "\n" + ex.StackTrace, ex);
            }
            LoadSettings();
        }

        private void LoadSettings()
        {
            try
            {
                var settings = _settingsManager.GetSettings();
                if (settings != null) SetSettings(settings);

                _emulator.Tags = _settingsManager.GetTags();
                _replicator.Mappings = _settingsManager.GetMapping();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "\n" + ex.StackTrace, ex);
            }
        }

        public bool IsReplicate()
        {
            return _replicator.IsRunning;
        }

        public void EnableReplication()
        {
            try
            {
                _replicator.Start();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "\n" + ex.StackTrace, ex);
            }
        }

        public void DisableReplication()
        {
            try
            {
                _replicator.Stop();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "\n" + ex.StackTrace, ex);
            }
        }

        public bool IsRunning()
        {
            return _emulator.IsRunning;
        }

        public void Start()
        {
            try
            {
                _emulator.Start();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "\n" + ex.StackTrace, ex);
            }
        }

        public void Stop()
        {
            try
            {
                _emulator.Stop();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "\n" + ex.StackTrace, ex);
            }
        }

        public void SetSettings(Settings settings)
        {
            try
            {
                _settingsManager.SaveSettings(settings);

                _emulator.AutoSaveReportPeriod = settings.ReportAutoSavePeriod;
                _emulator.ValuesDelay = settings.ValuesDelay;
                _emulator.GenerationPeriod = settings.SamplingRate;
                _replicator.ReplicationPeriod = settings.ReplicationPeriod;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "\n" + ex.StackTrace, ex);
            }
        }

        public Settings GetSettings()
        {
            try
            {
                return new Settings
                {
                    ReportAutoSavePeriod = _emulator.AutoSaveReportPeriod,
                    ValuesDelay = _emulator.ValuesDelay,
                    SamplingRate = _emulator.GenerationPeriod,
                    IsRunning = _emulator.IsRunning,
                    ReplicationPeriod = _replicator.ReplicationPeriod,
                    IsReplicate = _replicator.IsRunning
                };
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "\n" + ex.StackTrace, ex);
            }
        }

        public void AddTag(Tag tag)
        {
            try
            {
                _emulator.AddTag(tag);
                _historianAdapter.AddTag(tag);
                _settingsManager.AddTag(tag);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "\n" + ex.StackTrace, ex);
            }
        }

        public void RemoveTag(Tag tag)
        {
            try
            {
                _emulator.RemoveTag(tag);
                _historianAdapter.RemoveTag(tag);
                _settingsManager.RemoveTag(tag);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "\n" + ex.StackTrace, ex);
            }
        }

        public void RemoveTagByName(string tagName)
        {
            try
            {
                var tag = _emulator.GetTag(tagName);
                RemoveTag(tag);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "\n" + ex.StackTrace, ex);
            }
        }

        public Tag GetTag(int tagId)
        {
            try
            {
                return _settingsManager.GetTag(tagId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "\n" + ex.StackTrace, ex);
            }
        }

        public IEnumerable<Tag> GetTags()
        {
            return _settingsManager.GetTags();
        }

        public IEnumerable<string> GetTagList(string wellName)
        {
            return _emulator.TagNames;
        }

        public IEnumerable<Well> GetWellList()
        {
            try
            {
                return _pdgtmDbAdapter.GetWells();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "\n" + ex.StackTrace, ex);
            }
        }

        public IEnumerable<string> GetWitsmlObjects(string standard)
        {
            try
            {
                return WitsmlElementsParser.GetWitsmlObjects(standard);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "\n" + ex.StackTrace, ex);
            }
        }

        public IEnumerable<string> GetWitsmlElements(string standard, string @object)
        {
            try
            {
                return WitsmlElementsParser.GetWitsmlElements(standard, @object);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "\n" + ex.StackTrace, ex);
            }
        }

        public void AddMapItem(MapItem mapItem)
        {
            try
            {
                _replicator.AddMapping(mapItem);
                _settingsManager.AddMapItem(mapItem);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "\n" + ex.StackTrace, ex);
            }
        }

        public void RemoveMapItem(MapItem mapItem)
        {
            try
            {
                _replicator.RemoveMapping(mapItem);
                _settingsManager.RemoveMapItem(mapItem);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "\n" + ex.StackTrace, ex);
            }
        }

        public IEnumerable<MapItem> GetMappings()
        {
            try
            {
                return _settingsManager.GetMapping();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "\n" + ex.StackTrace, ex);
            }
        }
    }
}
