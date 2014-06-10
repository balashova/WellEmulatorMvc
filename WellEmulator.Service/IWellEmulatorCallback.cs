using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace WellEmulator.Service
{
    interface IWellEmulatorCallback
    {
        [OperationContract(IsOneWay = true)]
        void OnDataAdded(string data);
    }
}
