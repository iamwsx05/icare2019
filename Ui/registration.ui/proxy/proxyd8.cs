﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.18408
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Serialization;

// 
// 此源代码由 wsdl 自动生成, Version=4.0.30319.33440。
// 


/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.33440")]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Web.Services.WebServiceBindingAttribute(Name="WebServiceSoapBinding", Namespace="http://com.bdcc.hoffice.webservice.WebService")]
public partial class WebService : System.Web.Services.Protocols.SoapHttpClientProtocol {
    
    private System.Threading.SendOrPostCallback TestReportsServiceOperationCompleted;
    
    private System.Threading.SendOrPostCallback subscribServiceOperationCompleted;
    
    private System.Threading.SendOrPostCallback hospitalWritebackServiceOperationCompleted;
    
    private System.Threading.SendOrPostCallback medicalInstitutionsSubscribeServiceOperationCompleted;
    
    private System.Threading.SendOrPostCallback transferTreatReferralServiceOperationCompleted;
    
    private System.Threading.SendOrPostCallback outpatientRegisterOperationCompleted;
    
    private System.Threading.SendOrPostCallback smsServiceOperationCompleted;
    
    /// <remarks/>
    public WebService() {
        this.Url = "http://10.10.2.28:8092/hoffice/services/WebService";
    }
    
    /// <remarks/>
    public event TestReportsServiceCompletedEventHandler TestReportsServiceCompleted;
    
    /// <remarks/>
    public event subscribServiceCompletedEventHandler subscribServiceCompleted;
    
    /// <remarks/>
    public event hospitalWritebackServiceCompletedEventHandler hospitalWritebackServiceCompleted;
    
    /// <remarks/>
    public event medicalInstitutionsSubscribeServiceCompletedEventHandler medicalInstitutionsSubscribeServiceCompleted;
    
    /// <remarks/>
    public event transferTreatReferralServiceCompletedEventHandler transferTreatReferralServiceCompleted;
    
    /// <remarks/>
    public event outpatientRegisterCompletedEventHandler outpatientRegisterCompleted;
    
    /// <remarks/>
    public event smsServiceCompletedEventHandler smsServiceCompleted;
    
    /// <remarks/>
    [System.Web.Services.Protocols.SoapRpcMethodAttribute("", RequestNamespace="http://webservice.hoffice.bdcc.com", ResponseNamespace="http://com.bdcc.hoffice.webservice.WebService")]
    [return: System.Xml.Serialization.SoapElementAttribute("TestReportsServiceReturn")]
    public string TestReportsService(string typeno, string inarg, string userinfo) {
        object[] results = this.Invoke("TestReportsService", new object[] {
                    typeno,
                    inarg,
                    userinfo});
        return ((string)(results[0]));
    }
    
    /// <remarks/>
    public System.IAsyncResult BeginTestReportsService(string typeno, string inarg, string userinfo, System.AsyncCallback callback, object asyncState) {
        return this.BeginInvoke("TestReportsService", new object[] {
                    typeno,
                    inarg,
                    userinfo}, callback, asyncState);
    }
    
    /// <remarks/>
    public string EndTestReportsService(System.IAsyncResult asyncResult) {
        object[] results = this.EndInvoke(asyncResult);
        return ((string)(results[0]));
    }
    
    /// <remarks/>
    public void TestReportsServiceAsync(string typeno, string inarg, string userinfo) {
        this.TestReportsServiceAsync(typeno, inarg, userinfo, null);
    }
    
    /// <remarks/>
    public void TestReportsServiceAsync(string typeno, string inarg, string userinfo, object userState) {
        if ((this.TestReportsServiceOperationCompleted == null)) {
            this.TestReportsServiceOperationCompleted = new System.Threading.SendOrPostCallback(this.OnTestReportsServiceOperationCompleted);
        }
        this.InvokeAsync("TestReportsService", new object[] {
                    typeno,
                    inarg,
                    userinfo}, this.TestReportsServiceOperationCompleted, userState);
    }
    
    private void OnTestReportsServiceOperationCompleted(object arg) {
        if ((this.TestReportsServiceCompleted != null)) {
            System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
            this.TestReportsServiceCompleted(this, new TestReportsServiceCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
        }
    }
    
    /// <remarks/>
    [System.Web.Services.Protocols.SoapRpcMethodAttribute("", RequestNamespace="http://webservice.hoffice.bdcc.com", ResponseNamespace="http://com.bdcc.hoffice.webservice.WebService")]
    [return: System.Xml.Serialization.SoapElementAttribute("subscribServiceReturn")]
    public string subscribService(string typeno, string inarg, string userinfo) {
        object[] results = this.Invoke("subscribService", new object[] {
                    typeno,
                    inarg,
                    userinfo});
        return ((string)(results[0]));
    }
    
    /// <remarks/>
    public System.IAsyncResult BeginsubscribService(string typeno, string inarg, string userinfo, System.AsyncCallback callback, object asyncState) {
        return this.BeginInvoke("subscribService", new object[] {
                    typeno,
                    inarg,
                    userinfo}, callback, asyncState);
    }
    
    /// <remarks/>
    public string EndsubscribService(System.IAsyncResult asyncResult) {
        object[] results = this.EndInvoke(asyncResult);
        return ((string)(results[0]));
    }
    
    /// <remarks/>
    public void subscribServiceAsync(string typeno, string inarg, string userinfo) {
        this.subscribServiceAsync(typeno, inarg, userinfo, null);
    }
    
    /// <remarks/>
    public void subscribServiceAsync(string typeno, string inarg, string userinfo, object userState) {
        if ((this.subscribServiceOperationCompleted == null)) {
            this.subscribServiceOperationCompleted = new System.Threading.SendOrPostCallback(this.OnsubscribServiceOperationCompleted);
        }
        this.InvokeAsync("subscribService", new object[] {
                    typeno,
                    inarg,
                    userinfo}, this.subscribServiceOperationCompleted, userState);
    }
    
    private void OnsubscribServiceOperationCompleted(object arg) {
        if ((this.subscribServiceCompleted != null)) {
            System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
            this.subscribServiceCompleted(this, new subscribServiceCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
        }
    }
    
    /// <remarks/>
    [System.Web.Services.Protocols.SoapRpcMethodAttribute("", RequestNamespace="http://webservice.hoffice.bdcc.com", ResponseNamespace="http://com.bdcc.hoffice.webservice.WebService")]
    [return: System.Xml.Serialization.SoapElementAttribute("hospitalWritebackServiceReturn")]
    public string hospitalWritebackService(string typeno, string inarg, string userinfo) {
        object[] results = this.Invoke("hospitalWritebackService", new object[] {
                    typeno,
                    inarg,
                    userinfo});
        return ((string)(results[0]));
    }
    
    /// <remarks/>
    public System.IAsyncResult BeginhospitalWritebackService(string typeno, string inarg, string userinfo, System.AsyncCallback callback, object asyncState) {
        return this.BeginInvoke("hospitalWritebackService", new object[] {
                    typeno,
                    inarg,
                    userinfo}, callback, asyncState);
    }
    
    /// <remarks/>
    public string EndhospitalWritebackService(System.IAsyncResult asyncResult) {
        object[] results = this.EndInvoke(asyncResult);
        return ((string)(results[0]));
    }
    
    /// <remarks/>
    public void hospitalWritebackServiceAsync(string typeno, string inarg, string userinfo) {
        this.hospitalWritebackServiceAsync(typeno, inarg, userinfo, null);
    }
    
    /// <remarks/>
    public void hospitalWritebackServiceAsync(string typeno, string inarg, string userinfo, object userState) {
        if ((this.hospitalWritebackServiceOperationCompleted == null)) {
            this.hospitalWritebackServiceOperationCompleted = new System.Threading.SendOrPostCallback(this.OnhospitalWritebackServiceOperationCompleted);
        }
        this.InvokeAsync("hospitalWritebackService", new object[] {
                    typeno,
                    inarg,
                    userinfo}, this.hospitalWritebackServiceOperationCompleted, userState);
    }
    
    private void OnhospitalWritebackServiceOperationCompleted(object arg) {
        if ((this.hospitalWritebackServiceCompleted != null)) {
            System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
            this.hospitalWritebackServiceCompleted(this, new hospitalWritebackServiceCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
        }
    }
    
    /// <remarks/>
    [System.Web.Services.Protocols.SoapRpcMethodAttribute("", RequestNamespace="http://webservice.hoffice.bdcc.com", ResponseNamespace="http://com.bdcc.hoffice.webservice.WebService")]
    [return: System.Xml.Serialization.SoapElementAttribute("medicalInstitutionsSubscribeServiceReturn")]
    public string medicalInstitutionsSubscribeService(string typeno, string inarg, string userinfo) {
        object[] results = this.Invoke("medicalInstitutionsSubscribeService", new object[] {
                    typeno,
                    inarg,
                    userinfo});
        return ((string)(results[0]));
    }
    
    /// <remarks/>
    public System.IAsyncResult BeginmedicalInstitutionsSubscribeService(string typeno, string inarg, string userinfo, System.AsyncCallback callback, object asyncState) {
        return this.BeginInvoke("medicalInstitutionsSubscribeService", new object[] {
                    typeno,
                    inarg,
                    userinfo}, callback, asyncState);
    }
    
    /// <remarks/>
    public string EndmedicalInstitutionsSubscribeService(System.IAsyncResult asyncResult) {
        object[] results = this.EndInvoke(asyncResult);
        return ((string)(results[0]));
    }
    
    /// <remarks/>
    public void medicalInstitutionsSubscribeServiceAsync(string typeno, string inarg, string userinfo) {
        this.medicalInstitutionsSubscribeServiceAsync(typeno, inarg, userinfo, null);
    }
    
    /// <remarks/>
    public void medicalInstitutionsSubscribeServiceAsync(string typeno, string inarg, string userinfo, object userState) {
        if ((this.medicalInstitutionsSubscribeServiceOperationCompleted == null)) {
            this.medicalInstitutionsSubscribeServiceOperationCompleted = new System.Threading.SendOrPostCallback(this.OnmedicalInstitutionsSubscribeServiceOperationCompleted);
        }
        this.InvokeAsync("medicalInstitutionsSubscribeService", new object[] {
                    typeno,
                    inarg,
                    userinfo}, this.medicalInstitutionsSubscribeServiceOperationCompleted, userState);
    }
    
    private void OnmedicalInstitutionsSubscribeServiceOperationCompleted(object arg) {
        if ((this.medicalInstitutionsSubscribeServiceCompleted != null)) {
            System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
            this.medicalInstitutionsSubscribeServiceCompleted(this, new medicalInstitutionsSubscribeServiceCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
        }
    }
    
    /// <remarks/>
    [System.Web.Services.Protocols.SoapRpcMethodAttribute("", RequestNamespace="http://webservice.hoffice.bdcc.com", ResponseNamespace="http://com.bdcc.hoffice.webservice.WebService")]
    [return: System.Xml.Serialization.SoapElementAttribute("transferTreatReferralServiceReturn")]
    public string transferTreatReferralService(string typeno, string inarg, string userinfo) {
        object[] results = this.Invoke("transferTreatReferralService", new object[] {
                    typeno,
                    inarg,
                    userinfo});
        return ((string)(results[0]));
    }
    
    /// <remarks/>
    public System.IAsyncResult BegintransferTreatReferralService(string typeno, string inarg, string userinfo, System.AsyncCallback callback, object asyncState) {
        return this.BeginInvoke("transferTreatReferralService", new object[] {
                    typeno,
                    inarg,
                    userinfo}, callback, asyncState);
    }
    
    /// <remarks/>
    public string EndtransferTreatReferralService(System.IAsyncResult asyncResult) {
        object[] results = this.EndInvoke(asyncResult);
        return ((string)(results[0]));
    }
    
    /// <remarks/>
    public void transferTreatReferralServiceAsync(string typeno, string inarg, string userinfo) {
        this.transferTreatReferralServiceAsync(typeno, inarg, userinfo, null);
    }
    
    /// <remarks/>
    public void transferTreatReferralServiceAsync(string typeno, string inarg, string userinfo, object userState) {
        if ((this.transferTreatReferralServiceOperationCompleted == null)) {
            this.transferTreatReferralServiceOperationCompleted = new System.Threading.SendOrPostCallback(this.OntransferTreatReferralServiceOperationCompleted);
        }
        this.InvokeAsync("transferTreatReferralService", new object[] {
                    typeno,
                    inarg,
                    userinfo}, this.transferTreatReferralServiceOperationCompleted, userState);
    }
    
    private void OntransferTreatReferralServiceOperationCompleted(object arg) {
        if ((this.transferTreatReferralServiceCompleted != null)) {
            System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
            this.transferTreatReferralServiceCompleted(this, new transferTreatReferralServiceCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
        }
    }
    
    /// <remarks/>
    [System.Web.Services.Protocols.SoapRpcMethodAttribute("", RequestNamespace="http://webservice.hoffice.bdcc.com", ResponseNamespace="http://com.bdcc.hoffice.webservice.WebService")]
    [return: System.Xml.Serialization.SoapElementAttribute("outpatientRegisterReturn")]
    public string outpatientRegister(string typeno, string inarg, string userinfo) {
        object[] results = this.Invoke("outpatientRegister", new object[] {
                    typeno,
                    inarg,
                    userinfo});
        return ((string)(results[0]));
    }
    
    /// <remarks/>
    public System.IAsyncResult BeginoutpatientRegister(string typeno, string inarg, string userinfo, System.AsyncCallback callback, object asyncState) {
        return this.BeginInvoke("outpatientRegister", new object[] {
                    typeno,
                    inarg,
                    userinfo}, callback, asyncState);
    }
    
    /// <remarks/>
    public string EndoutpatientRegister(System.IAsyncResult asyncResult) {
        object[] results = this.EndInvoke(asyncResult);
        return ((string)(results[0]));
    }
    
    /// <remarks/>
    public void outpatientRegisterAsync(string typeno, string inarg, string userinfo) {
        this.outpatientRegisterAsync(typeno, inarg, userinfo, null);
    }
    
    /// <remarks/>
    public void outpatientRegisterAsync(string typeno, string inarg, string userinfo, object userState) {
        if ((this.outpatientRegisterOperationCompleted == null)) {
            this.outpatientRegisterOperationCompleted = new System.Threading.SendOrPostCallback(this.OnoutpatientRegisterOperationCompleted);
        }
        this.InvokeAsync("outpatientRegister", new object[] {
                    typeno,
                    inarg,
                    userinfo}, this.outpatientRegisterOperationCompleted, userState);
    }
    
    private void OnoutpatientRegisterOperationCompleted(object arg) {
        if ((this.outpatientRegisterCompleted != null)) {
            System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
            this.outpatientRegisterCompleted(this, new outpatientRegisterCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
        }
    }
    
    /// <remarks/>
    [System.Web.Services.Protocols.SoapRpcMethodAttribute("", RequestNamespace="http://webservice.hoffice.bdcc.com", ResponseNamespace="http://com.bdcc.hoffice.webservice.WebService")]
    [return: System.Xml.Serialization.SoapElementAttribute("smsServiceReturn")]
    public string smsService(string typeno, string inarg, string userinfo) {
        object[] results = this.Invoke("smsService", new object[] {
                    typeno,
                    inarg,
                    userinfo});
        return ((string)(results[0]));
    }
    
    /// <remarks/>
    public System.IAsyncResult BeginsmsService(string typeno, string inarg, string userinfo, System.AsyncCallback callback, object asyncState) {
        return this.BeginInvoke("smsService", new object[] {
                    typeno,
                    inarg,
                    userinfo}, callback, asyncState);
    }
    
    /// <remarks/>
    public string EndsmsService(System.IAsyncResult asyncResult) {
        object[] results = this.EndInvoke(asyncResult);
        return ((string)(results[0]));
    }
    
    /// <remarks/>
    public void smsServiceAsync(string typeno, string inarg, string userinfo) {
        this.smsServiceAsync(typeno, inarg, userinfo, null);
    }
    
    /// <remarks/>
    public void smsServiceAsync(string typeno, string inarg, string userinfo, object userState) {
        if ((this.smsServiceOperationCompleted == null)) {
            this.smsServiceOperationCompleted = new System.Threading.SendOrPostCallback(this.OnsmsServiceOperationCompleted);
        }
        this.InvokeAsync("smsService", new object[] {
                    typeno,
                    inarg,
                    userinfo}, this.smsServiceOperationCompleted, userState);
    }
    
    private void OnsmsServiceOperationCompleted(object arg) {
        if ((this.smsServiceCompleted != null)) {
            System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
            this.smsServiceCompleted(this, new smsServiceCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
        }
    }
    
    /// <remarks/>
    public new void CancelAsync(object userState) {
        base.CancelAsync(userState);
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.33440")]
public delegate void TestReportsServiceCompletedEventHandler(object sender, TestReportsServiceCompletedEventArgs e);

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.33440")]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
public partial class TestReportsServiceCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
    
    private object[] results;
    
    internal TestReportsServiceCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
            base(exception, cancelled, userState) {
        this.results = results;
    }
    
    /// <remarks/>
    public string Result {
        get {
            this.RaiseExceptionIfNecessary();
            return ((string)(this.results[0]));
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.33440")]
public delegate void subscribServiceCompletedEventHandler(object sender, subscribServiceCompletedEventArgs e);

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.33440")]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
public partial class subscribServiceCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
    
    private object[] results;
    
    internal subscribServiceCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
            base(exception, cancelled, userState) {
        this.results = results;
    }
    
    /// <remarks/>
    public string Result {
        get {
            this.RaiseExceptionIfNecessary();
            return ((string)(this.results[0]));
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.33440")]
public delegate void hospitalWritebackServiceCompletedEventHandler(object sender, hospitalWritebackServiceCompletedEventArgs e);

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.33440")]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
public partial class hospitalWritebackServiceCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
    
    private object[] results;
    
    internal hospitalWritebackServiceCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
            base(exception, cancelled, userState) {
        this.results = results;
    }
    
    /// <remarks/>
    public string Result {
        get {
            this.RaiseExceptionIfNecessary();
            return ((string)(this.results[0]));
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.33440")]
public delegate void medicalInstitutionsSubscribeServiceCompletedEventHandler(object sender, medicalInstitutionsSubscribeServiceCompletedEventArgs e);

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.33440")]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
public partial class medicalInstitutionsSubscribeServiceCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
    
    private object[] results;
    
    internal medicalInstitutionsSubscribeServiceCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
            base(exception, cancelled, userState) {
        this.results = results;
    }
    
    /// <remarks/>
    public string Result {
        get {
            this.RaiseExceptionIfNecessary();
            return ((string)(this.results[0]));
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.33440")]
public delegate void transferTreatReferralServiceCompletedEventHandler(object sender, transferTreatReferralServiceCompletedEventArgs e);

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.33440")]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
public partial class transferTreatReferralServiceCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
    
    private object[] results;
    
    internal transferTreatReferralServiceCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
            base(exception, cancelled, userState) {
        this.results = results;
    }
    
    /// <remarks/>
    public string Result {
        get {
            this.RaiseExceptionIfNecessary();
            return ((string)(this.results[0]));
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.33440")]
public delegate void outpatientRegisterCompletedEventHandler(object sender, outpatientRegisterCompletedEventArgs e);

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.33440")]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
public partial class outpatientRegisterCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
    
    private object[] results;
    
    internal outpatientRegisterCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
            base(exception, cancelled, userState) {
        this.results = results;
    }
    
    /// <remarks/>
    public string Result {
        get {
            this.RaiseExceptionIfNecessary();
            return ((string)(this.results[0]));
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.33440")]
public delegate void smsServiceCompletedEventHandler(object sender, smsServiceCompletedEventArgs e);

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.33440")]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
public partial class smsServiceCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
    
    private object[] results;
    
    internal smsServiceCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
            base(exception, cancelled, userState) {
        this.results = results;
    }
    
    /// <remarks/>
    public string Result {
        get {
            this.RaiseExceptionIfNecessary();
            return ((string)(this.results[0]));
        }
    }
}
