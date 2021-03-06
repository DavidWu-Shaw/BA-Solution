﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.261
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// 
// This code was auto-generated by Microsoft.Silverlight.ServiceReference, version 4.0.60310.0
// 
namespace BA.WebService.Silverlight.TestService {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="", ConfigurationName="TestService.TestService")]
    public interface TestService {
        
        [System.ServiceModel.OperationContractAttribute(AsyncPattern=true, Action="urn:TestService/RetrieveProductsBySupplier", ReplyAction="urn:TestService/RetrieveProductsBySupplierResponse")]
        System.IAsyncResult BeginRetrieveProductsBySupplier(int supplierId, System.AsyncCallback callback, object asyncState);
        
        System.Collections.ObjectModel.ObservableCollection<CRM.ShopComponent.Dto.ProductInfoDto> EndRetrieveProductsBySupplier(System.IAsyncResult result);
        
        [System.ServiceModel.OperationContractAttribute(AsyncPattern=true, Action="urn:TestService/RetrieveAllSupplier", ReplyAction="urn:TestService/RetrieveAllSupplierResponse")]
        System.IAsyncResult BeginRetrieveAllSupplier(System.AsyncCallback callback, object asyncState);
        
        System.Collections.ObjectModel.ObservableCollection<CRM.ShopComponent.Dto.SupplierInfoDto> EndRetrieveAllSupplier(System.IAsyncResult result);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface TestServiceChannel : BA.WebService.Silverlight.TestService.TestService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class RetrieveProductsBySupplierCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        public RetrieveProductsBySupplierCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        public System.Collections.ObjectModel.ObservableCollection<CRM.ShopComponent.Dto.ProductInfoDto> Result {
            get {
                base.RaiseExceptionIfNecessary();
                return ((System.Collections.ObjectModel.ObservableCollection<CRM.ShopComponent.Dto.ProductInfoDto>)(this.results[0]));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class RetrieveAllSupplierCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        public RetrieveAllSupplierCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        public System.Collections.ObjectModel.ObservableCollection<CRM.ShopComponent.Dto.SupplierInfoDto> Result {
            get {
                base.RaiseExceptionIfNecessary();
                return ((System.Collections.ObjectModel.ObservableCollection<CRM.ShopComponent.Dto.SupplierInfoDto>)(this.results[0]));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class TestServiceClient : System.ServiceModel.ClientBase<BA.WebService.Silverlight.TestService.TestService>, BA.WebService.Silverlight.TestService.TestService {
        
        private BeginOperationDelegate onBeginRetrieveProductsBySupplierDelegate;
        
        private EndOperationDelegate onEndRetrieveProductsBySupplierDelegate;
        
        private System.Threading.SendOrPostCallback onRetrieveProductsBySupplierCompletedDelegate;
        
        private BeginOperationDelegate onBeginRetrieveAllSupplierDelegate;
        
        private EndOperationDelegate onEndRetrieveAllSupplierDelegate;
        
        private System.Threading.SendOrPostCallback onRetrieveAllSupplierCompletedDelegate;
        
        private BeginOperationDelegate onBeginOpenDelegate;
        
        private EndOperationDelegate onEndOpenDelegate;
        
        private System.Threading.SendOrPostCallback onOpenCompletedDelegate;
        
        private BeginOperationDelegate onBeginCloseDelegate;
        
        private EndOperationDelegate onEndCloseDelegate;
        
        private System.Threading.SendOrPostCallback onCloseCompletedDelegate;
        
        public TestServiceClient() {
        }
        
        public TestServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public TestServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public TestServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public TestServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public System.Net.CookieContainer CookieContainer {
            get {
                System.ServiceModel.Channels.IHttpCookieContainerManager httpCookieContainerManager = this.InnerChannel.GetProperty<System.ServiceModel.Channels.IHttpCookieContainerManager>();
                if ((httpCookieContainerManager != null)) {
                    return httpCookieContainerManager.CookieContainer;
                }
                else {
                    return null;
                }
            }
            set {
                System.ServiceModel.Channels.IHttpCookieContainerManager httpCookieContainerManager = this.InnerChannel.GetProperty<System.ServiceModel.Channels.IHttpCookieContainerManager>();
                if ((httpCookieContainerManager != null)) {
                    httpCookieContainerManager.CookieContainer = value;
                }
                else {
                    throw new System.InvalidOperationException("Unable to set the CookieContainer. Please make sure the binding contains an HttpC" +
                            "ookieContainerBindingElement.");
                }
            }
        }
        
        public event System.EventHandler<RetrieveProductsBySupplierCompletedEventArgs> RetrieveProductsBySupplierCompleted;
        
        public event System.EventHandler<RetrieveAllSupplierCompletedEventArgs> RetrieveAllSupplierCompleted;
        
        public event System.EventHandler<System.ComponentModel.AsyncCompletedEventArgs> OpenCompleted;
        
        public event System.EventHandler<System.ComponentModel.AsyncCompletedEventArgs> CloseCompleted;
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.IAsyncResult BA.WebService.Silverlight.TestService.TestService.BeginRetrieveProductsBySupplier(int supplierId, System.AsyncCallback callback, object asyncState) {
            return base.Channel.BeginRetrieveProductsBySupplier(supplierId, callback, asyncState);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Collections.ObjectModel.ObservableCollection<CRM.ShopComponent.Dto.ProductInfoDto> BA.WebService.Silverlight.TestService.TestService.EndRetrieveProductsBySupplier(System.IAsyncResult result) {
            return base.Channel.EndRetrieveProductsBySupplier(result);
        }
        
        private System.IAsyncResult OnBeginRetrieveProductsBySupplier(object[] inValues, System.AsyncCallback callback, object asyncState) {
            int supplierId = ((int)(inValues[0]));
            return ((BA.WebService.Silverlight.TestService.TestService)(this)).BeginRetrieveProductsBySupplier(supplierId, callback, asyncState);
        }
        
        private object[] OnEndRetrieveProductsBySupplier(System.IAsyncResult result) {
            System.Collections.ObjectModel.ObservableCollection<CRM.ShopComponent.Dto.ProductInfoDto> retVal = ((BA.WebService.Silverlight.TestService.TestService)(this)).EndRetrieveProductsBySupplier(result);
            return new object[] {
                    retVal};
        }
        
        private void OnRetrieveProductsBySupplierCompleted(object state) {
            if ((this.RetrieveProductsBySupplierCompleted != null)) {
                InvokeAsyncCompletedEventArgs e = ((InvokeAsyncCompletedEventArgs)(state));
                this.RetrieveProductsBySupplierCompleted(this, new RetrieveProductsBySupplierCompletedEventArgs(e.Results, e.Error, e.Cancelled, e.UserState));
            }
        }
        
        public void RetrieveProductsBySupplierAsync(int supplierId) {
            this.RetrieveProductsBySupplierAsync(supplierId, null);
        }
        
        public void RetrieveProductsBySupplierAsync(int supplierId, object userState) {
            if ((this.onBeginRetrieveProductsBySupplierDelegate == null)) {
                this.onBeginRetrieveProductsBySupplierDelegate = new BeginOperationDelegate(this.OnBeginRetrieveProductsBySupplier);
            }
            if ((this.onEndRetrieveProductsBySupplierDelegate == null)) {
                this.onEndRetrieveProductsBySupplierDelegate = new EndOperationDelegate(this.OnEndRetrieveProductsBySupplier);
            }
            if ((this.onRetrieveProductsBySupplierCompletedDelegate == null)) {
                this.onRetrieveProductsBySupplierCompletedDelegate = new System.Threading.SendOrPostCallback(this.OnRetrieveProductsBySupplierCompleted);
            }
            base.InvokeAsync(this.onBeginRetrieveProductsBySupplierDelegate, new object[] {
                        supplierId}, this.onEndRetrieveProductsBySupplierDelegate, this.onRetrieveProductsBySupplierCompletedDelegate, userState);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.IAsyncResult BA.WebService.Silverlight.TestService.TestService.BeginRetrieveAllSupplier(System.AsyncCallback callback, object asyncState) {
            return base.Channel.BeginRetrieveAllSupplier(callback, asyncState);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Collections.ObjectModel.ObservableCollection<CRM.ShopComponent.Dto.SupplierInfoDto> BA.WebService.Silverlight.TestService.TestService.EndRetrieveAllSupplier(System.IAsyncResult result) {
            return base.Channel.EndRetrieveAllSupplier(result);
        }
        
        private System.IAsyncResult OnBeginRetrieveAllSupplier(object[] inValues, System.AsyncCallback callback, object asyncState) {
            return ((BA.WebService.Silverlight.TestService.TestService)(this)).BeginRetrieveAllSupplier(callback, asyncState);
        }
        
        private object[] OnEndRetrieveAllSupplier(System.IAsyncResult result) {
            System.Collections.ObjectModel.ObservableCollection<CRM.ShopComponent.Dto.SupplierInfoDto> retVal = ((BA.WebService.Silverlight.TestService.TestService)(this)).EndRetrieveAllSupplier(result);
            return new object[] {
                    retVal};
        }
        
        private void OnRetrieveAllSupplierCompleted(object state) {
            if ((this.RetrieveAllSupplierCompleted != null)) {
                InvokeAsyncCompletedEventArgs e = ((InvokeAsyncCompletedEventArgs)(state));
                this.RetrieveAllSupplierCompleted(this, new RetrieveAllSupplierCompletedEventArgs(e.Results, e.Error, e.Cancelled, e.UserState));
            }
        }
        
        public void RetrieveAllSupplierAsync() {
            this.RetrieveAllSupplierAsync(null);
        }
        
        public void RetrieveAllSupplierAsync(object userState) {
            if ((this.onBeginRetrieveAllSupplierDelegate == null)) {
                this.onBeginRetrieveAllSupplierDelegate = new BeginOperationDelegate(this.OnBeginRetrieveAllSupplier);
            }
            if ((this.onEndRetrieveAllSupplierDelegate == null)) {
                this.onEndRetrieveAllSupplierDelegate = new EndOperationDelegate(this.OnEndRetrieveAllSupplier);
            }
            if ((this.onRetrieveAllSupplierCompletedDelegate == null)) {
                this.onRetrieveAllSupplierCompletedDelegate = new System.Threading.SendOrPostCallback(this.OnRetrieveAllSupplierCompleted);
            }
            base.InvokeAsync(this.onBeginRetrieveAllSupplierDelegate, null, this.onEndRetrieveAllSupplierDelegate, this.onRetrieveAllSupplierCompletedDelegate, userState);
        }
        
        private System.IAsyncResult OnBeginOpen(object[] inValues, System.AsyncCallback callback, object asyncState) {
            return ((System.ServiceModel.ICommunicationObject)(this)).BeginOpen(callback, asyncState);
        }
        
        private object[] OnEndOpen(System.IAsyncResult result) {
            ((System.ServiceModel.ICommunicationObject)(this)).EndOpen(result);
            return null;
        }
        
        private void OnOpenCompleted(object state) {
            if ((this.OpenCompleted != null)) {
                InvokeAsyncCompletedEventArgs e = ((InvokeAsyncCompletedEventArgs)(state));
                this.OpenCompleted(this, new System.ComponentModel.AsyncCompletedEventArgs(e.Error, e.Cancelled, e.UserState));
            }
        }
        
        public void OpenAsync() {
            this.OpenAsync(null);
        }
        
        public void OpenAsync(object userState) {
            if ((this.onBeginOpenDelegate == null)) {
                this.onBeginOpenDelegate = new BeginOperationDelegate(this.OnBeginOpen);
            }
            if ((this.onEndOpenDelegate == null)) {
                this.onEndOpenDelegate = new EndOperationDelegate(this.OnEndOpen);
            }
            if ((this.onOpenCompletedDelegate == null)) {
                this.onOpenCompletedDelegate = new System.Threading.SendOrPostCallback(this.OnOpenCompleted);
            }
            base.InvokeAsync(this.onBeginOpenDelegate, null, this.onEndOpenDelegate, this.onOpenCompletedDelegate, userState);
        }
        
        private System.IAsyncResult OnBeginClose(object[] inValues, System.AsyncCallback callback, object asyncState) {
            return ((System.ServiceModel.ICommunicationObject)(this)).BeginClose(callback, asyncState);
        }
        
        private object[] OnEndClose(System.IAsyncResult result) {
            ((System.ServiceModel.ICommunicationObject)(this)).EndClose(result);
            return null;
        }
        
        private void OnCloseCompleted(object state) {
            if ((this.CloseCompleted != null)) {
                InvokeAsyncCompletedEventArgs e = ((InvokeAsyncCompletedEventArgs)(state));
                this.CloseCompleted(this, new System.ComponentModel.AsyncCompletedEventArgs(e.Error, e.Cancelled, e.UserState));
            }
        }
        
        public void CloseAsync() {
            this.CloseAsync(null);
        }
        
        public void CloseAsync(object userState) {
            if ((this.onBeginCloseDelegate == null)) {
                this.onBeginCloseDelegate = new BeginOperationDelegate(this.OnBeginClose);
            }
            if ((this.onEndCloseDelegate == null)) {
                this.onEndCloseDelegate = new EndOperationDelegate(this.OnEndClose);
            }
            if ((this.onCloseCompletedDelegate == null)) {
                this.onCloseCompletedDelegate = new System.Threading.SendOrPostCallback(this.OnCloseCompleted);
            }
            base.InvokeAsync(this.onBeginCloseDelegate, null, this.onEndCloseDelegate, this.onCloseCompletedDelegate, userState);
        }
        
        protected override BA.WebService.Silverlight.TestService.TestService CreateChannel() {
            return new TestServiceClientChannel(this);
        }
        
        private class TestServiceClientChannel : ChannelBase<BA.WebService.Silverlight.TestService.TestService>, BA.WebService.Silverlight.TestService.TestService {
            
            public TestServiceClientChannel(System.ServiceModel.ClientBase<BA.WebService.Silverlight.TestService.TestService> client) : 
                    base(client) {
            }
            
            public System.IAsyncResult BeginRetrieveProductsBySupplier(int supplierId, System.AsyncCallback callback, object asyncState) {
                object[] _args = new object[1];
                _args[0] = supplierId;
                System.IAsyncResult _result = base.BeginInvoke("RetrieveProductsBySupplier", _args, callback, asyncState);
                return _result;
            }
            
            public System.Collections.ObjectModel.ObservableCollection<CRM.ShopComponent.Dto.ProductInfoDto> EndRetrieveProductsBySupplier(System.IAsyncResult result) {
                object[] _args = new object[0];
                System.Collections.ObjectModel.ObservableCollection<CRM.ShopComponent.Dto.ProductInfoDto> _result = ((System.Collections.ObjectModel.ObservableCollection<CRM.ShopComponent.Dto.ProductInfoDto>)(base.EndInvoke("RetrieveProductsBySupplier", _args, result)));
                return _result;
            }
            
            public System.IAsyncResult BeginRetrieveAllSupplier(System.AsyncCallback callback, object asyncState) {
                object[] _args = new object[0];
                System.IAsyncResult _result = base.BeginInvoke("RetrieveAllSupplier", _args, callback, asyncState);
                return _result;
            }
            
            public System.Collections.ObjectModel.ObservableCollection<CRM.ShopComponent.Dto.SupplierInfoDto> EndRetrieveAllSupplier(System.IAsyncResult result) {
                object[] _args = new object[0];
                System.Collections.ObjectModel.ObservableCollection<CRM.ShopComponent.Dto.SupplierInfoDto> _result = ((System.Collections.ObjectModel.ObservableCollection<CRM.ShopComponent.Dto.SupplierInfoDto>)(base.EndInvoke("RetrieveAllSupplier", _args, result)));
                return _result;
            }
        }
    }
}
