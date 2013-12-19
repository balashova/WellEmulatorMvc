﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.18051
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WellEmulatorServiceClient.ServiceReference {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="Settings", Namespace="http://schemas.datacontract.org/2004/07/WellEmulator.Models")]
    [System.SerializableAttribute()]
    public partial class Settings : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private bool IsReplicateField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private bool IsRunningField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string MessageField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.TimeSpan ReplicationPeriodField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.TimeSpan ReportAutoSavePeriodField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.TimeSpan SamplingRateField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.TimeSpan ValuesDelayField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public bool IsReplicate {
            get {
                return this.IsReplicateField;
            }
            set {
                if ((this.IsReplicateField.Equals(value) != true)) {
                    this.IsReplicateField = value;
                    this.RaisePropertyChanged("IsReplicate");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public bool IsRunning {
            get {
                return this.IsRunningField;
            }
            set {
                if ((this.IsRunningField.Equals(value) != true)) {
                    this.IsRunningField = value;
                    this.RaisePropertyChanged("IsRunning");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Message {
            get {
                return this.MessageField;
            }
            set {
                if ((object.ReferenceEquals(this.MessageField, value) != true)) {
                    this.MessageField = value;
                    this.RaisePropertyChanged("Message");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.TimeSpan ReplicationPeriod {
            get {
                return this.ReplicationPeriodField;
            }
            set {
                if ((this.ReplicationPeriodField.Equals(value) != true)) {
                    this.ReplicationPeriodField = value;
                    this.RaisePropertyChanged("ReplicationPeriod");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.TimeSpan ReportAutoSavePeriod {
            get {
                return this.ReportAutoSavePeriodField;
            }
            set {
                if ((this.ReportAutoSavePeriodField.Equals(value) != true)) {
                    this.ReportAutoSavePeriodField = value;
                    this.RaisePropertyChanged("ReportAutoSavePeriod");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.TimeSpan SamplingRate {
            get {
                return this.SamplingRateField;
            }
            set {
                if ((this.SamplingRateField.Equals(value) != true)) {
                    this.SamplingRateField = value;
                    this.RaisePropertyChanged("SamplingRate");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.TimeSpan ValuesDelay {
            get {
                return this.ValuesDelayField;
            }
            set {
                if ((this.ValuesDelayField.Equals(value) != true)) {
                    this.ValuesDelayField = value;
                    this.RaisePropertyChanged("ValuesDelay");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="Tag", Namespace="http://schemas.datacontract.org/2004/07/WellEmulator.Models")]
    [System.SerializableAttribute()]
    public partial class Tag : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private double DeltaField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string GroupField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private double MaxValueField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private double MinValueField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string NameField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string ObjectField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private double ValueField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string WellNameField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public double Delta {
            get {
                return this.DeltaField;
            }
            set {
                if ((this.DeltaField.Equals(value) != true)) {
                    this.DeltaField = value;
                    this.RaisePropertyChanged("Delta");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Group {
            get {
                return this.GroupField;
            }
            set {
                if ((object.ReferenceEquals(this.GroupField, value) != true)) {
                    this.GroupField = value;
                    this.RaisePropertyChanged("Group");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public double MaxValue {
            get {
                return this.MaxValueField;
            }
            set {
                if ((this.MaxValueField.Equals(value) != true)) {
                    this.MaxValueField = value;
                    this.RaisePropertyChanged("MaxValue");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public double MinValue {
            get {
                return this.MinValueField;
            }
            set {
                if ((this.MinValueField.Equals(value) != true)) {
                    this.MinValueField = value;
                    this.RaisePropertyChanged("MinValue");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Name {
            get {
                return this.NameField;
            }
            set {
                if ((object.ReferenceEquals(this.NameField, value) != true)) {
                    this.NameField = value;
                    this.RaisePropertyChanged("Name");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Object {
            get {
                return this.ObjectField;
            }
            set {
                if ((object.ReferenceEquals(this.ObjectField, value) != true)) {
                    this.ObjectField = value;
                    this.RaisePropertyChanged("Object");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public double Value {
            get {
                return this.ValueField;
            }
            set {
                if ((this.ValueField.Equals(value) != true)) {
                    this.ValueField = value;
                    this.RaisePropertyChanged("Value");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string WellName {
            get {
                return this.WellNameField;
            }
            set {
                if ((object.ReferenceEquals(this.WellNameField, value) != true)) {
                    this.WellNameField = value;
                    this.RaisePropertyChanged("WellName");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="Well", Namespace="http://schemas.datacontract.org/2004/07/WellEmulator.Models")]
    [System.SerializableAttribute()]
    public partial class Well : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int IdField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string NameField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int Id {
            get {
                return this.IdField;
            }
            set {
                if ((this.IdField.Equals(value) != true)) {
                    this.IdField = value;
                    this.RaisePropertyChanged("Id");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Name {
            get {
                return this.NameField;
            }
            set {
                if ((object.ReferenceEquals(this.NameField, value) != true)) {
                    this.NameField = value;
                    this.RaisePropertyChanged("Name");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="http://WellEmulator.com", ConfigurationName="ServiceReference.IWellEmulator")]
    public interface IWellEmulator {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://WellEmulator.com/IWellEmulator/DisableReplication", ReplyAction="http://WellEmulator.com/IWellEmulator/DisableReplicationResponse")]
        void DisableReplication();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://WellEmulator.com/IWellEmulator/DisableReplication", ReplyAction="http://WellEmulator.com/IWellEmulator/DisableReplicationResponse")]
        System.Threading.Tasks.Task DisableReplicationAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://WellEmulator.com/IWellEmulator/EnableReplication", ReplyAction="http://WellEmulator.com/IWellEmulator/EnableReplicationResponse")]
        void EnableReplication();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://WellEmulator.com/IWellEmulator/EnableReplication", ReplyAction="http://WellEmulator.com/IWellEmulator/EnableReplicationResponse")]
        System.Threading.Tasks.Task EnableReplicationAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://WellEmulator.com/IWellEmulator/IsReplicate", ReplyAction="http://WellEmulator.com/IWellEmulator/IsReplicateResponse")]
        bool IsReplicate();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://WellEmulator.com/IWellEmulator/IsReplicate", ReplyAction="http://WellEmulator.com/IWellEmulator/IsReplicateResponse")]
        System.Threading.Tasks.Task<bool> IsReplicateAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://WellEmulator.com/IWellEmulator/IsRunning", ReplyAction="http://WellEmulator.com/IWellEmulator/IsRunningResponse")]
        bool IsRunning();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://WellEmulator.com/IWellEmulator/IsRunning", ReplyAction="http://WellEmulator.com/IWellEmulator/IsRunningResponse")]
        System.Threading.Tasks.Task<bool> IsRunningAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://WellEmulator.com/IWellEmulator/Start", ReplyAction="http://WellEmulator.com/IWellEmulator/StartResponse")]
        void Start();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://WellEmulator.com/IWellEmulator/Start", ReplyAction="http://WellEmulator.com/IWellEmulator/StartResponse")]
        System.Threading.Tasks.Task StartAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://WellEmulator.com/IWellEmulator/Stop", ReplyAction="http://WellEmulator.com/IWellEmulator/StopResponse")]
        void Stop();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://WellEmulator.com/IWellEmulator/Stop", ReplyAction="http://WellEmulator.com/IWellEmulator/StopResponse")]
        System.Threading.Tasks.Task StopAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://WellEmulator.com/IWellEmulator/SetSettings", ReplyAction="http://WellEmulator.com/IWellEmulator/SetSettingsResponse")]
        void SetSettings(WellEmulatorServiceClient.ServiceReference.Settings settings);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://WellEmulator.com/IWellEmulator/SetSettings", ReplyAction="http://WellEmulator.com/IWellEmulator/SetSettingsResponse")]
        System.Threading.Tasks.Task SetSettingsAsync(WellEmulatorServiceClient.ServiceReference.Settings settings);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://WellEmulator.com/IWellEmulator/GetSettings", ReplyAction="http://WellEmulator.com/IWellEmulator/GetSettingsResponse")]
        WellEmulatorServiceClient.ServiceReference.Settings GetSettings();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://WellEmulator.com/IWellEmulator/GetSettings", ReplyAction="http://WellEmulator.com/IWellEmulator/GetSettingsResponse")]
        System.Threading.Tasks.Task<WellEmulatorServiceClient.ServiceReference.Settings> GetSettingsAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://WellEmulator.com/IWellEmulator/AddTag", ReplyAction="http://WellEmulator.com/IWellEmulator/AddTagResponse")]
        void AddTag(WellEmulatorServiceClient.ServiceReference.Tag tag);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://WellEmulator.com/IWellEmulator/AddTag", ReplyAction="http://WellEmulator.com/IWellEmulator/AddTagResponse")]
        System.Threading.Tasks.Task AddTagAsync(WellEmulatorServiceClient.ServiceReference.Tag tag);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://WellEmulator.com/IWellEmulator/RemoveTag", ReplyAction="http://WellEmulator.com/IWellEmulator/RemoveTagResponse")]
        void RemoveTag(WellEmulatorServiceClient.ServiceReference.Tag tag);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://WellEmulator.com/IWellEmulator/RemoveTag", ReplyAction="http://WellEmulator.com/IWellEmulator/RemoveTagResponse")]
        System.Threading.Tasks.Task RemoveTagAsync(WellEmulatorServiceClient.ServiceReference.Tag tag);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://WellEmulator.com/IWellEmulator/RemoveTagByName", ReplyAction="http://WellEmulator.com/IWellEmulator/RemoveTagByNameResponse")]
        void RemoveTagByName(string tagName);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://WellEmulator.com/IWellEmulator/RemoveTagByName", ReplyAction="http://WellEmulator.com/IWellEmulator/RemoveTagByNameResponse")]
        System.Threading.Tasks.Task RemoveTagByNameAsync(string tagName);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://WellEmulator.com/IWellEmulator/GetTag", ReplyAction="http://WellEmulator.com/IWellEmulator/GetTagResponse")]
        WellEmulatorServiceClient.ServiceReference.Tag GetTag(string name);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://WellEmulator.com/IWellEmulator/GetTag", ReplyAction="http://WellEmulator.com/IWellEmulator/GetTagResponse")]
        System.Threading.Tasks.Task<WellEmulatorServiceClient.ServiceReference.Tag> GetTagAsync(string name);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://WellEmulator.com/IWellEmulator/GetTagList", ReplyAction="http://WellEmulator.com/IWellEmulator/GetTagListResponse")]
        string[] GetTagList(string wellName);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://WellEmulator.com/IWellEmulator/GetTagList", ReplyAction="http://WellEmulator.com/IWellEmulator/GetTagListResponse")]
        System.Threading.Tasks.Task<string[]> GetTagListAsync(string wellName);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://WellEmulator.com/IWellEmulator/GetTags", ReplyAction="http://WellEmulator.com/IWellEmulator/GetTagsResponse")]
        WellEmulatorServiceClient.ServiceReference.Tag[] GetTags();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://WellEmulator.com/IWellEmulator/GetTags", ReplyAction="http://WellEmulator.com/IWellEmulator/GetTagsResponse")]
        System.Threading.Tasks.Task<WellEmulatorServiceClient.ServiceReference.Tag[]> GetTagsAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://WellEmulator.com/IWellEmulator/GetWellList", ReplyAction="http://WellEmulator.com/IWellEmulator/GetWellListResponse")]
        WellEmulatorServiceClient.ServiceReference.Well[] GetWellList();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://WellEmulator.com/IWellEmulator/GetWellList", ReplyAction="http://WellEmulator.com/IWellEmulator/GetWellListResponse")]
        System.Threading.Tasks.Task<WellEmulatorServiceClient.ServiceReference.Well[]> GetWellListAsync();
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IWellEmulatorChannel : WellEmulatorServiceClient.ServiceReference.IWellEmulator, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class WellEmulatorClient : System.ServiceModel.ClientBase<WellEmulatorServiceClient.ServiceReference.IWellEmulator>, WellEmulatorServiceClient.ServiceReference.IWellEmulator {
        
        public WellEmulatorClient() {
        }
        
        public WellEmulatorClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public WellEmulatorClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public WellEmulatorClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public WellEmulatorClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public void DisableReplication() {
            base.Channel.DisableReplication();
        }
        
        public System.Threading.Tasks.Task DisableReplicationAsync() {
            return base.Channel.DisableReplicationAsync();
        }
        
        public void EnableReplication() {
            base.Channel.EnableReplication();
        }
        
        public System.Threading.Tasks.Task EnableReplicationAsync() {
            return base.Channel.EnableReplicationAsync();
        }
        
        public bool IsReplicate() {
            return base.Channel.IsReplicate();
        }
        
        public System.Threading.Tasks.Task<bool> IsReplicateAsync() {
            return base.Channel.IsReplicateAsync();
        }
        
        public bool IsRunning() {
            return base.Channel.IsRunning();
        }
        
        public System.Threading.Tasks.Task<bool> IsRunningAsync() {
            return base.Channel.IsRunningAsync();
        }
        
        public void Start() {
            base.Channel.Start();
        }
        
        public System.Threading.Tasks.Task StartAsync() {
            return base.Channel.StartAsync();
        }
        
        public void Stop() {
            base.Channel.Stop();
        }
        
        public System.Threading.Tasks.Task StopAsync() {
            return base.Channel.StopAsync();
        }
        
        public void SetSettings(WellEmulatorServiceClient.ServiceReference.Settings settings) {
            base.Channel.SetSettings(settings);
        }
        
        public System.Threading.Tasks.Task SetSettingsAsync(WellEmulatorServiceClient.ServiceReference.Settings settings) {
            return base.Channel.SetSettingsAsync(settings);
        }
        
        public WellEmulatorServiceClient.ServiceReference.Settings GetSettings() {
            return base.Channel.GetSettings();
        }
        
        public System.Threading.Tasks.Task<WellEmulatorServiceClient.ServiceReference.Settings> GetSettingsAsync() {
            return base.Channel.GetSettingsAsync();
        }
        
        public void AddTag(WellEmulatorServiceClient.ServiceReference.Tag tag) {
            base.Channel.AddTag(tag);
        }
        
        public System.Threading.Tasks.Task AddTagAsync(WellEmulatorServiceClient.ServiceReference.Tag tag) {
            return base.Channel.AddTagAsync(tag);
        }
        
        public void RemoveTag(WellEmulatorServiceClient.ServiceReference.Tag tag) {
            base.Channel.RemoveTag(tag);
        }
        
        public System.Threading.Tasks.Task RemoveTagAsync(WellEmulatorServiceClient.ServiceReference.Tag tag) {
            return base.Channel.RemoveTagAsync(tag);
        }
        
        public void RemoveTagByName(string tagName) {
            base.Channel.RemoveTagByName(tagName);
        }
        
        public System.Threading.Tasks.Task RemoveTagByNameAsync(string tagName) {
            return base.Channel.RemoveTagByNameAsync(tagName);
        }
        
        public WellEmulatorServiceClient.ServiceReference.Tag GetTag(string name) {
            return base.Channel.GetTag(name);
        }
        
        public System.Threading.Tasks.Task<WellEmulatorServiceClient.ServiceReference.Tag> GetTagAsync(string name) {
            return base.Channel.GetTagAsync(name);
        }
        
        public string[] GetTagList(string wellName) {
            return base.Channel.GetTagList(wellName);
        }
        
        public System.Threading.Tasks.Task<string[]> GetTagListAsync(string wellName) {
            return base.Channel.GetTagListAsync(wellName);
        }
        
        public WellEmulatorServiceClient.ServiceReference.Tag[] GetTags() {
            return base.Channel.GetTags();
        }
        
        public System.Threading.Tasks.Task<WellEmulatorServiceClient.ServiceReference.Tag[]> GetTagsAsync() {
            return base.Channel.GetTagsAsync();
        }
        
        public WellEmulatorServiceClient.ServiceReference.Well[] GetWellList() {
            return base.Channel.GetWellList();
        }
        
        public System.Threading.Tasks.Task<WellEmulatorServiceClient.ServiceReference.Well[]> GetWellListAsync() {
            return base.Channel.GetWellListAsync();
        }
    }
}
