using System;

namespace WellEmulator.Core
{
    public interface IDatabaseObserver
    {
        event EventHandler OnHistorianDataChanged;
        event EventHandler OnPdgtmDataChanged;
        void ObserveHistorian();
        void ObservePdgtm();
        void Dispose();
    }
}