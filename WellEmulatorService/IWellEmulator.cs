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
        void DisableReplication();

        [OperationContract]
        void EnableReplication();

        [OperationContract]
        bool IsReplicate();

        [OperationContract]
        bool IsRunning();

        [OperationContract]
        void Start();

        [OperationContract]
        void Stop();

        [OperationContract]
        void SetSettings(Settings settings);

        [OperationContract]
        Settings GetSettings();

        [OperationContract]
        void AddTag(Tag tag);

        [OperationContract]
        void RemoveTag(Tag tag);

        [OperationContract]
        void RemoveTagByName(string tagName);

        [OperationContract]
        Tag GetTag(string name);

        [OperationContract]
        IEnumerable<string> GetTagList(string wellName);

        [OperationContract]
        IEnumerable<Tag> GetTags();

        [OperationContract]
        IEnumerable<Well> GetWellList();
    }
}
