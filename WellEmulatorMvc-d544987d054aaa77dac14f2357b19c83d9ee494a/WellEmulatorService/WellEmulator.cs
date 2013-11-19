using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WellEmulator.Core;
using WellEmulator.Models;
using System.Configuration;

namespace WellEmulatorService
{
    public class WellEmulator : IWellEmulator
    {
        private readonly Emulator _emulator = new Emulator();
        private TimeSpan _replicationPeriod = new TimeSpan(0, 0, 30, 0);
        private bool _enableReplication = false;

        //private readonly PdgtmDb _pdgtmDb = new PdgtmDb();

        public bool IsReplicate
        {
            get
            {
                return _enableReplication;
            }
        }

        public bool EnableReplication(ref string message)
        {
            try
            {
                //TODO Implement! 
            }
            catch (Exception ex)
            {
                message = ex.Message;
                return false;
            }
            return true;
        }

        public bool DisableReplication(ref string message)
        {
            try
            {
                //TODO Implement! 
            }
            catch (Exception ex)
            {
                message = ex.Message;
                return false;
            }
            return true;
        }

        public bool IsRunning()
        {
            return _emulator.IsRunning;
        }

        public bool Start(ref string message)
        {
            try
            {
                _emulator.Start();
            }
            catch (Exception ex)
            {
                message = ex.Message;
                return false;
            }
            return true;
        }

        public bool Stop(ref string message)
        {
            try
            {
                _emulator.Stop();
            }
            catch (Exception ex)
            {
                message = ex.Message;
                return false;
            }
            return true;
        }

        public bool SetSettings(Settings settings, ref string message)
        {
            try
            {
                _emulator.AutoSaveReportPeriod = settings.ReportSave;
                _emulator.ValuesDelay = settings.Delay;
                _emulator.GenerationPeriod = settings.SamplingRate;
                _replicationPeriod = settings.ReplicationPeriod;
            }
            catch (Exception ex)
            {
                message = ex.Message;
                return false;
            }
            return true;
        }

        public Settings GetSettings(ref string message)
        {
            try
            {
                return new Settings
                {
                    ReportSave = _emulator.AutoSaveReportPeriod,
                    Delay = _emulator.ValuesDelay,
                    SamplingRate = _emulator.GenerationPeriod,
                    IsRunning = _emulator.IsRunning,
                    ReplicationPeriod = _replicationPeriod
                };
            }
            catch (Exception ex)
            {
                message = ex.Message;
            }
            return null;
        }

        public bool AddTag(Tag tag, ref string message)
        {
            try
            {
                _emulator.AddTag(tag);
            }
            catch (Exception ex)
            {
                message = ex.Message;
                return false;
            }
            return true;
        }

        public bool RemoveTag(string name, ref string message)
        {
            try
            {
                _emulator.RemoveTag(name);
            }
            catch (Exception ex)
            {
                message = ex.Message;
                return false;
            }
            return true;
        }

        public Tag GetTag(string name, ref string message)
        {
            try
            {
                _emulator.GetTag(name);
            }
            catch (Exception ex)
            {
                message = ex.Message;
            }
            return null;
        }

        public IEnumerable<string> GetTagList(string wellName, ref string message)
        {
            try
            {
                return _emulator.Tags;
            }
            catch (Exception ex)
            {
                message = ex.Message;
            }
            return null;
        }

        public IEnumerable<string> GetWellList(ref string message)
        {
            throw new NotImplementedException();
        }

        public void GetWell(string name, ref string message)
        {
            throw new NotImplementedException();
        }

        public bool AddWell(string well, ref string message)
        {
            throw new NotImplementedException();
        }

        public bool RemoveWell(string name, ref string message)
        {
            throw new NotImplementedException();
        }
    }
}
