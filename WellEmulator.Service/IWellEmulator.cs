using System;
using System.Collections.Generic;
using System.ServiceModel;
using JetBrains.Annotations;
using WellEmulator.Models;

namespace WellEmulator.Service
{
    [ServiceContract(Namespace = "http://WellEmulator.com", CallbackContract = typeof(IWellEmulatorCallback))]
    public interface IWellEmulator
    {
        [OperationContract]
        void SetQueryRange(TimeSpan timeSpan);
        [OperationContract]
        TimeSpan GetQueryRange();

        [OperationContract]
        bool Connect();

        [OperationContract]
        void Disconnect();

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
        IEnumerable<string> GetWitsmlObjects(string standard);

        [OperationContract]
        IEnumerable<string> GetWitsmlElements(string standard, string @object);

        [OperationContract]
        void AddMapItem(MapItem mapItem);

        [OperationContract]
        void RemoveMapItems(List<int> mapItems);

        [OperationContract]
        IEnumerable<MapItem> GetMappings();

        [OperationContract]
        IEnumerable<Tag> GetSettingsTags();

        [OperationContract]
        IEnumerable<string> GetAllHistTags();

        [OperationContract]
        IEnumerable<string> GetHistWells();

        [OperationContract]
        IEnumerable<string> GetHistTags(string wellName);

        [OperationContract]
        IEnumerable<string> GetPdgtmTags(string wellName);

        [OperationContract]
        IEnumerable<Well> GetPdgtmWells();

        [OperationContract]
        IEnumerable<string> GetNotMappedHistTags(string wellName);

        [OperationContract]
        IEnumerable<string> GetNotMappedPdgtmTags(string wellName);
    }
}
