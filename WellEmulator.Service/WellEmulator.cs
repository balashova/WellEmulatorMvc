﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.ServiceModel;
using NLog;
using WellEmulator.Core;
using WellEmulator.Models;
using WellEmulator.Service.Parsers;

namespace WellEmulator.Service
{
    public class WellEmulator: IWellEmulator
    {
        private static readonly object SyncRoot = new object();
        private readonly Logger _logger = LogManager.GetCurrentClassLogger();

        private static Emulator _emulator;
        private static Replicator _replicator;
        private static PdgtmDbAdapter _pdgtmDbAdapter;
        private static HistorianAdapter _historianAdapter;
        private static SettingsManager _settingsManager;
        private static DatabaseObserver _databaseObserver;
        private static IWellEmulatorCallback _subscriber;

        private static TimeSpan _queryRange = TimeSpan.FromMinutes(6);

        private static bool _initialized = false;

        public WellEmulator()
        {
            try
            {
                if (_initialized) return;
                lock (SyncRoot)
                {
                    if (_initialized) return;
                    _initialized = true;

                    _emulator = new Emulator();
                    _replicator = new Replicator();
                    _pdgtmDbAdapter = new PdgtmDbAdapter();
                    _historianAdapter = new HistorianAdapter();
                    _settingsManager = new SettingsManager();

                    _databaseObserver = new DatabaseObserver();
                    _databaseObserver.OnHistorianDataChanged += OnHistorianDataChanged;
                    _databaseObserver.OnPdgtmDataChanged += OnPdgtmDataChanged;

                    LoadSettings();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Load failed", ex);
            }
        }

        private void OnHistorianDataChanged()
        {
            if (_subscriber == null) return;
            try
            {
                if (((ICommunicationObject) _subscriber).State == CommunicationState.Opened)
                {
                    var values = _historianAdapter.GetValues(_queryRange);
                    _subscriber.OnHistorianDataChanged(values);
                }
            }
            catch (Exception ex)
            {
                _logger.Error("Client notification PDGTM failed", ex);
                throw;
            }
        }
        
        private void OnPdgtmDataChanged()
        {
            if (_subscriber == null) return;
            try
            {
                if (((ICommunicationObject) _subscriber).State == CommunicationState.Opened)
                {
                    var values = _pdgtmDbAdapter.GetValues(_queryRange);
                    _subscriber.OnPdgtmDataChanged(values);
                }
            }
            catch (Exception ex)
            {
                _logger.Error("Client notification PDGTM failed", ex);
                throw;
            }
        }

        public void SetQueryRange(TimeSpan timeSpan)
        {
            _queryRange = timeSpan;
        }

        public TimeSpan GetQueryRange()
        {
            return _queryRange;
        }

        public bool Connect()
        {
            try
            {
                _subscriber = OperationContext.Current.GetCallbackChannel<IWellEmulatorCallback>();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public void Disconnect()
        {
            _subscriber = null;
        }

        private void LoadSettings()
        {
            try
            {
                var settings = _settingsManager.GetSettings();
                if (settings != null) SetSettings(settings);

                _emulator.Tags = _settingsManager.GetTags();
                _replicator.Mappings = _settingsManager.GetMappings();
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
                _settingsManager.AddTag(tag);
                _emulator.Tags = _settingsManager.GetTags();
                _historianAdapter.AddTag(tag);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public void RemoveTag(int tagId)
        {
            try
            {
                var tag = _settingsManager.GetTag(tagId);

                _emulator.RemoveTag(tag);
                _historianAdapter.RemoveTag(tag);
                _settingsManager.RemoveTag(tag);
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
                _settingsManager.AddMapItem(mapItem);
                _replicator.AddMapping(mapItem);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "\n" + ex.StackTrace, ex);
            }
        }

        public void RemoveMapItems(List<int> mapItems)
        {
            try
            {
                _settingsManager.RemoveMapItems(mapItems);
                _replicator.RemoveMapping(mapItems);
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
                return _settingsManager.GetMappings();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "\n" + ex.StackTrace, ex);
            }
        }

        public IEnumerable<Tag> GetSettingsTags()
        {
            return _settingsManager.GetTags();
        }

        public IEnumerable<string> GetAllHistTags()
        {
            return _historianAdapter.GetAllTags();
        }

        public IEnumerable<string> GetHistWells()
        {
            return _historianAdapter.GetWells();
        }

        public IEnumerable<string> GetHistTags(string wellName)
        {
            return _historianAdapter.GetTags(wellName);
        }

        public IEnumerable<string> GetPdgtmTags(string wellName)
        {
            return _pdgtmDbAdapter.GetTags(wellName);
        }

        public IEnumerable<string> GetNotMappedHistTags(string wellName)
        {
            var allTags = _historianAdapter.GetTags(wellName);
            var mappings = _settingsManager.GetMappings(wellName, Db.Historian).Select(m => m.HistorianTag);
            return allTags.Where(t => !mappings.Contains(t));
        }

        public IEnumerable<string> GetNotMappedPdgtmTags(string wellName)
        {
            var allTags = _pdgtmDbAdapter.GetTags(wellName);
            var mappings = _settingsManager.GetMappings(wellName, Db.Pdgtm).Select(m => m.PdgtmTag);
            return allTags.Where(t => !mappings.Contains(t));
        }

        public IEnumerable<Well> GetPdgtmWells()
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
    }
}
