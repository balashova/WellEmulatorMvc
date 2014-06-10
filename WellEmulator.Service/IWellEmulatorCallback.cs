using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using WellEmulator.Models;

namespace WellEmulator.Service
{
    interface IWellEmulatorCallback
    {
        [OperationContract(IsOneWay = true)]
        void OnPdgtmDataChanged(List<PdgtmValue> pdgtmValues);

        [OperationContract(IsOneWay = true)]
        void OnHistorianDataChanged(List<HistorianValue>  historianValues);
    }
}
