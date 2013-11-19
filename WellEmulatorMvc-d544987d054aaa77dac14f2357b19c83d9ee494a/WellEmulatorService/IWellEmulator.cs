using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using WellEmulator.Models;

namespace WellEmulatorService
{
    [ServiceContract(Namespace = "http://WellEmulator.com")]
    public interface IWellEmulator
    {
        [OperationContract]
        bool IsRunning();

        [OperationContract]
        bool Start(ref string message);

        [OperationContract]
        bool Stop(ref string message);

        [OperationContract]
        bool SetSettings(Settings settings, ref string message);

        [OperationContract]
        Settings GetSettings(ref string message);

        [OperationContract]
        bool AddTag(Tag tag, ref string message);

        [OperationContract]
        bool RemoveTag(string name, ref string message);

        [OperationContract]
        Tag GetTag(string name, ref string message);

        [OperationContract]
        IEnumerable<string> GetTagList(string wellName, ref string message);

        //[OperationContract]
        //IEnumerable<string> GetWellList(ref string message);

        //[OperationContract]
        //Well GetWell(string name, ref string message);

        //[OperationContract]
        //bool AddWell(Well well, ref string message);

        //[OperationContract]
        //bool RemoveWell(string name, ref string message);
    }
}
