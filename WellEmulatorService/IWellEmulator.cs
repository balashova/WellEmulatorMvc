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
        void RemoveTag(int tagId);

        [OperationContract]
        Tag GetTag(int tagId);

        [OperationContract]
        IEnumerable<string> GetTagList(string wellName);

        [OperationContract]
        IEnumerable<Tag> GetTags();

        [OperationContract]
        IEnumerable<Well> GetWellList();

        [OperationContract]
        IEnumerable<string> GetWitsmlObjects(string standard);

        [OperationContract]
        IEnumerable<string> GetWitsmlElements(string standard, string @object);

        [OperationContract]
        void AddMapItem(MapItem mapItem);

        [OperationContract]
        void RemoveMapItem(MapItem mapItem);

        [OperationContract]
        IEnumerable<MapItem> GetMappings();
    }
}
