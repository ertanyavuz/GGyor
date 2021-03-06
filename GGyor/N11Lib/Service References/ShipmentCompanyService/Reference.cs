﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18408
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace N11Lib.ShipmentCompanyService {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="http://www.n11.com/ws/schemas", ConfigurationName="ShipmentCompanyService.ShipmentCompanyServicePort")]
    public interface ShipmentCompanyServicePort {
        
        // CODEGEN: Generating message contract since the operation GetShipmentCompanies is neither RPC nor document wrapped.
        [System.ServiceModel.OperationContractAttribute(Action="", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        N11Lib.ShipmentCompanyService.GetShipmentCompaniesResponse1 GetShipmentCompanies(N11Lib.ShipmentCompanyService.GetShipmentCompaniesRequest1 request);
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.18408")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://www.n11.com/ws/schemas")]
    public partial class GetShipmentCompaniesRequest : object, System.ComponentModel.INotifyPropertyChanged {
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.18408")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.n11.com/ws/schemas")]
    public partial class ShipmentCompanyData : object, System.ComponentModel.INotifyPropertyChanged {
        
        private long idField;
        
        private string nameField;
        
        private string shortNameField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=0)]
        public long id {
            get {
                return this.idField;
            }
            set {
                this.idField = value;
                this.RaisePropertyChanged("id");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=1)]
        public string name {
            get {
                return this.nameField;
            }
            set {
                this.nameField = value;
                this.RaisePropertyChanged("name");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=2)]
        public string shortName {
            get {
                return this.shortNameField;
            }
            set {
                this.shortNameField = value;
                this.RaisePropertyChanged("shortName");
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
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.18408")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.n11.com/ws/schemas")]
    public partial class ResultInfo : object, System.ComponentModel.INotifyPropertyChanged {
        
        private string statusField;
        
        private string errorCodeField;
        
        private string errorMessageField;
        
        private string errorCategoryField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable=true, Order=0)]
        public string status {
            get {
                return this.statusField;
            }
            set {
                this.statusField = value;
                this.RaisePropertyChanged("status");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable=true, Order=1)]
        public string errorCode {
            get {
                return this.errorCodeField;
            }
            set {
                this.errorCodeField = value;
                this.RaisePropertyChanged("errorCode");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable=true, Order=2)]
        public string errorMessage {
            get {
                return this.errorMessageField;
            }
            set {
                this.errorMessageField = value;
                this.RaisePropertyChanged("errorMessage");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable=true, Order=3)]
        public string errorCategory {
            get {
                return this.errorCategoryField;
            }
            set {
                this.errorCategoryField = value;
                this.RaisePropertyChanged("errorCategory");
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
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.18408")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://www.n11.com/ws/schemas")]
    public partial class GetShipmentCompaniesResponse : object, System.ComponentModel.INotifyPropertyChanged {
        
        private ResultInfo resultField;
        
        private ShipmentCompanyData[] shipmentCompaniesField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=0)]
        public ResultInfo result {
            get {
                return this.resultField;
            }
            set {
                this.resultField = value;
                this.RaisePropertyChanged("result");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlArrayAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=1)]
        [System.Xml.Serialization.XmlArrayItemAttribute("shipmentCompany", Form=System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable=false)]
        public ShipmentCompanyData[] shipmentCompanies {
            get {
                return this.shipmentCompaniesField;
            }
            set {
                this.shipmentCompaniesField = value;
                this.RaisePropertyChanged("shipmentCompanies");
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
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class GetShipmentCompaniesRequest1 {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://www.n11.com/ws/schemas", Order=0)]
        public N11Lib.ShipmentCompanyService.GetShipmentCompaniesRequest GetShipmentCompaniesRequest;
        
        public GetShipmentCompaniesRequest1() {
        }
        
        public GetShipmentCompaniesRequest1(N11Lib.ShipmentCompanyService.GetShipmentCompaniesRequest GetShipmentCompaniesRequest) {
            this.GetShipmentCompaniesRequest = GetShipmentCompaniesRequest;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class GetShipmentCompaniesResponse1 {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://www.n11.com/ws/schemas", Order=0)]
        public N11Lib.ShipmentCompanyService.GetShipmentCompaniesResponse GetShipmentCompaniesResponse;
        
        public GetShipmentCompaniesResponse1() {
        }
        
        public GetShipmentCompaniesResponse1(N11Lib.ShipmentCompanyService.GetShipmentCompaniesResponse GetShipmentCompaniesResponse) {
            this.GetShipmentCompaniesResponse = GetShipmentCompaniesResponse;
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface ShipmentCompanyServicePortChannel : N11Lib.ShipmentCompanyService.ShipmentCompanyServicePort, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class ShipmentCompanyServicePortClient : System.ServiceModel.ClientBase<N11Lib.ShipmentCompanyService.ShipmentCompanyServicePort>, N11Lib.ShipmentCompanyService.ShipmentCompanyServicePort {
        
        public ShipmentCompanyServicePortClient() {
        }
        
        public ShipmentCompanyServicePortClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public ShipmentCompanyServicePortClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ShipmentCompanyServicePortClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ShipmentCompanyServicePortClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        N11Lib.ShipmentCompanyService.GetShipmentCompaniesResponse1 N11Lib.ShipmentCompanyService.ShipmentCompanyServicePort.GetShipmentCompanies(N11Lib.ShipmentCompanyService.GetShipmentCompaniesRequest1 request) {
            return base.Channel.GetShipmentCompanies(request);
        }
        
        public N11Lib.ShipmentCompanyService.GetShipmentCompaniesResponse GetShipmentCompanies(N11Lib.ShipmentCompanyService.GetShipmentCompaniesRequest GetShipmentCompaniesRequest) {
            N11Lib.ShipmentCompanyService.GetShipmentCompaniesRequest1 inValue = new N11Lib.ShipmentCompanyService.GetShipmentCompaniesRequest1();
            inValue.GetShipmentCompaniesRequest = GetShipmentCompaniesRequest;
            N11Lib.ShipmentCompanyService.GetShipmentCompaniesResponse1 retVal = ((N11Lib.ShipmentCompanyService.ShipmentCompanyServicePort)(this)).GetShipmentCompanies(inValue);
            return retVal.GetShipmentCompaniesResponse;
        }
    }
}
