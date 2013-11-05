using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace WellEmulatorService
{
    [ServiceContract(Namespace = "http://WellEmulator.com")]
    public interface IWellEmulator
    {
        [OperationContract]
        string MethodForTest(string request);
    }
}
