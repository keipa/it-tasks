

using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Serialization;


[System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Web.Services.WebServiceBindingAttribute(Name="Ideone_Service_v1Binding", Namespace="http://ideone.com/api/1/service")]
public partial class Ideone_Service_v1Service : System.Web.Services.Protocols.SoapHttpClientProtocol {
    
    private System.Threading.SendOrPostCallback createSubmissionOperationCompleted;
    
    private System.Threading.SendOrPostCallback getSubmissionStatusOperationCompleted;
    
    private System.Threading.SendOrPostCallback getSubmissionDetailsOperationCompleted;
    
    private System.Threading.SendOrPostCallback getLanguagesOperationCompleted;
    
    private System.Threading.SendOrPostCallback testFunctionOperationCompleted;
    
    public Ideone_Service_v1Service() {
        this.Url = "http://ideone.com/api/1/service";
    }
    
    public event createSubmissionCompletedEventHandler createSubmissionCompleted;
    
    public event getSubmissionStatusCompletedEventHandler getSubmissionStatusCompleted;
    
    public event getSubmissionDetailsCompletedEventHandler getSubmissionDetailsCompleted;
    
    public event getLanguagesCompletedEventHandler getLanguagesCompleted;
    
    public event testFunctionCompletedEventHandler testFunctionCompleted;
    
    [System.Web.Services.Protocols.SoapRpcMethodAttribute("http://ideone.com/api/1/service#createSubmission", RequestNamespace="http://ideone.com/api/1/service", ResponseNamespace="http://ideone.com/api/1/service")]
    [return: System.Xml.Serialization.SoapElementAttribute("return")]
    public object[] createSubmission(string user, string pass, string sourceCode, int language, string input, bool run, bool @private) {
        object[] results = this.Invoke("createSubmission", new object[] {
                    user,
                    pass,
                    sourceCode,
                    language,
                    input,
                    run,
                    @private});
        return ((object[])(results[0]));
    }
    
    public System.IAsyncResult BegincreateSubmission(string user, string pass, string sourceCode, int language, string input, bool run, bool @private, System.AsyncCallback callback, object asyncState) {
        return this.BeginInvoke("createSubmission", new object[] {
                    user,
                    pass,
                    sourceCode,
                    language,
                    input,
                    run,
                    @private}, callback, asyncState);
    }
    
    public object[] EndcreateSubmission(System.IAsyncResult asyncResult) {
        object[] results = this.EndInvoke(asyncResult);
        return ((object[])(results[0]));
    }
    
    public void createSubmissionAsync(string user, string pass, string sourceCode, int language, string input, bool run, bool @private) {
        this.createSubmissionAsync(user, pass, sourceCode, language, input, run, @private, null);
    }
    
    public void createSubmissionAsync(string user, string pass, string sourceCode, int language, string input, bool run, bool @private, object userState) {
        if ((this.createSubmissionOperationCompleted == null)) {
            this.createSubmissionOperationCompleted = new System.Threading.SendOrPostCallback(this.OncreateSubmissionOperationCompleted);
        }
        this.InvokeAsync("createSubmission", new object[] {
                    user,
                    pass,
                    sourceCode,
                    language,
                    input,
                    run,
                    @private}, this.createSubmissionOperationCompleted, userState);
    }
    
    private void OncreateSubmissionOperationCompleted(object arg) {
        if ((this.createSubmissionCompleted != null)) {
            System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
            this.createSubmissionCompleted(this, new createSubmissionCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
        }
    }
    
    [System.Web.Services.Protocols.SoapRpcMethodAttribute("http://ideone.com/api/1/service#getSubmissionStatus", RequestNamespace="http://ideone.com/api/1/service", ResponseNamespace="http://ideone.com/api/1/service")]
    [return: System.Xml.Serialization.SoapElementAttribute("return")]
    public object[] getSubmissionStatus(string user, string pass, string link) {
        object[] results = this.Invoke("getSubmissionStatus", new object[] {
                    user,
                    pass,
                    link});
        return ((object[])(results[0]));
    }
    
    public System.IAsyncResult BegingetSubmissionStatus(string user, string pass, string link, System.AsyncCallback callback, object asyncState) {
        return this.BeginInvoke("getSubmissionStatus", new object[] {
                    user,
                    pass,
                    link}, callback, asyncState);
    }
    
    public object[] EndgetSubmissionStatus(System.IAsyncResult asyncResult) {
        object[] results = this.EndInvoke(asyncResult);
        return ((object[])(results[0]));
    }
    
    public void getSubmissionStatusAsync(string user, string pass, string link) {
        this.getSubmissionStatusAsync(user, pass, link, null);
    }
    
    public void getSubmissionStatusAsync(string user, string pass, string link, object userState) {
        if ((this.getSubmissionStatusOperationCompleted == null)) {
            this.getSubmissionStatusOperationCompleted = new System.Threading.SendOrPostCallback(this.OngetSubmissionStatusOperationCompleted);
        }
        this.InvokeAsync("getSubmissionStatus", new object[] {
                    user,
                    pass,
                    link}, this.getSubmissionStatusOperationCompleted, userState);
    }
    
    private void OngetSubmissionStatusOperationCompleted(object arg) {
        if ((this.getSubmissionStatusCompleted != null)) {
            System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
            this.getSubmissionStatusCompleted(this, new getSubmissionStatusCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
        }
    }
    
    [System.Web.Services.Protocols.SoapRpcMethodAttribute("http://ideone.com/api/1/service#getSubmissionDetails", RequestNamespace="http://ideone.com/api/1/service", ResponseNamespace="http://ideone.com/api/1/service")]
    [return: System.Xml.Serialization.SoapElementAttribute("return")]
    public object[] getSubmissionDetails(string user, string pass, string link, bool withSource, bool withInput, bool withOutput, bool withStderr, bool withCmpinfo) {
        object[] results = this.Invoke("getSubmissionDetails", new object[] {
                    user,
                    pass,
                    link,
                    withSource,
                    withInput,
                    withOutput,
                    withStderr,
                    withCmpinfo});
        return ((object[])(results[0]));
    }
    
   
    public System.IAsyncResult BegingetSubmissionDetails(string user, string pass, string link, bool withSource, bool withInput, bool withOutput, bool withStderr, bool withCmpinfo, System.AsyncCallback callback, object asyncState) {
        return this.BeginInvoke("getSubmissionDetails", new object[] {
                    user,
                    pass,
                    link,
                    withSource,
                    withInput,
                    withOutput,
                    withStderr,
                    withCmpinfo}, callback, asyncState);
    }
    
   
    public object[] EndgetSubmissionDetails(System.IAsyncResult asyncResult) {
        object[] results = this.EndInvoke(asyncResult);
        return ((object[])(results[0]));
    }
    
   
    public void getSubmissionDetailsAsync(string user, string pass, string link, bool withSource, bool withInput, bool withOutput, bool withStderr, bool withCmpinfo) {
        this.getSubmissionDetailsAsync(user, pass, link, withSource, withInput, withOutput, withStderr, withCmpinfo, null);
    }
    
   
    public void getSubmissionDetailsAsync(string user, string pass, string link, bool withSource, bool withInput, bool withOutput, bool withStderr, bool withCmpinfo, object userState) {
        if ((this.getSubmissionDetailsOperationCompleted == null)) {
            this.getSubmissionDetailsOperationCompleted = new System.Threading.SendOrPostCallback(this.OngetSubmissionDetailsOperationCompleted);
        }
        this.InvokeAsync("getSubmissionDetails", new object[] {
                    user,
                    pass,
                    link,
                    withSource,
                    withInput,
                    withOutput,
                    withStderr,
                    withCmpinfo}, this.getSubmissionDetailsOperationCompleted, userState);
    }
    
    private void OngetSubmissionDetailsOperationCompleted(object arg) {
        if ((this.getSubmissionDetailsCompleted != null)) {
            System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
            this.getSubmissionDetailsCompleted(this, new getSubmissionDetailsCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
        }
    }
    
   
    [System.Web.Services.Protocols.SoapRpcMethodAttribute("http://ideone.com/api/1/service#getLanguages", RequestNamespace="http://ideone.com/api/1/service", ResponseNamespace="http://ideone.com/api/1/service")]
    [return: System.Xml.Serialization.SoapElementAttribute("return")]
    public object[] getLanguages(string user, string pass) {
        object[] results = this.Invoke("getLanguages", new object[] {
                    user,
                    pass});
        return ((object[])(results[0]));
    }
    
   
    public System.IAsyncResult BegingetLanguages(string user, string pass, System.AsyncCallback callback, object asyncState) {
        return this.BeginInvoke("getLanguages", new object[] {
                    user,
                    pass}, callback, asyncState);
    }
    
   
    public object[] EndgetLanguages(System.IAsyncResult asyncResult) {
        object[] results = this.EndInvoke(asyncResult);
        return ((object[])(results[0]));
    }
    
   
    public void getLanguagesAsync(string user, string pass) {
        this.getLanguagesAsync(user, pass, null);
    }
    
   
    public void getLanguagesAsync(string user, string pass, object userState) {
        if ((this.getLanguagesOperationCompleted == null)) {
            this.getLanguagesOperationCompleted = new System.Threading.SendOrPostCallback(this.OngetLanguagesOperationCompleted);
        }
        this.InvokeAsync("getLanguages", new object[] {
                    user,
                    pass}, this.getLanguagesOperationCompleted, userState);
    }
    
    private void OngetLanguagesOperationCompleted(object arg) {
        if ((this.getLanguagesCompleted != null)) {
            System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
            this.getLanguagesCompleted(this, new getLanguagesCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
        }
    }
    
   
    [System.Web.Services.Protocols.SoapRpcMethodAttribute("http://ideone.com/api/1/service#testFunction", RequestNamespace="http://ideone.com/api/1/service", ResponseNamespace="http://ideone.com/api/1/service")]
    [return: System.Xml.Serialization.SoapElementAttribute("return")]
    public object[] testFunction(string user, string pass) {
        object[] results = this.Invoke("testFunction", new object[] {
                    user,
                    pass});
        return ((object[])(results[0]));
    }
    
   
    public System.IAsyncResult BegintestFunction(string user, string pass, System.AsyncCallback callback, object asyncState) {
        return this.BeginInvoke("testFunction", new object[] {
                    user,
                    pass}, callback, asyncState);
    }
    
   
    public object[] EndtestFunction(System.IAsyncResult asyncResult) {
        object[] results = this.EndInvoke(asyncResult);
        return ((object[])(results[0]));
    }
    
   
    public void testFunctionAsync(string user, string pass) {
        this.testFunctionAsync(user, pass, null);
    }
    
   
    public void testFunctionAsync(string user, string pass, object userState) {
        if ((this.testFunctionOperationCompleted == null)) {
            this.testFunctionOperationCompleted = new System.Threading.SendOrPostCallback(this.OntestFunctionOperationCompleted);
        }
        this.InvokeAsync("testFunction", new object[] {
                    user,
                    pass}, this.testFunctionOperationCompleted, userState);
    }
    
    private void OntestFunctionOperationCompleted(object arg) {
        if ((this.testFunctionCompleted != null)) {
            System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
            this.testFunctionCompleted(this, new testFunctionCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
        }
    }
    
   
    public new void CancelAsync(object userState) {
        base.CancelAsync(userState);
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
public delegate void createSubmissionCompletedEventHandler(object sender, createSubmissionCompletedEventArgs e);

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
public partial class createSubmissionCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
    
    private object[] results;
    
    internal createSubmissionCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
            base(exception, cancelled, userState) {
        this.results = results;
    }
    
   
    public object[] Result {
        get {
            this.RaiseExceptionIfNecessary();
            return ((object[])(this.results[0]));
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
public delegate void getSubmissionStatusCompletedEventHandler(object sender, getSubmissionStatusCompletedEventArgs e);

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
public partial class getSubmissionStatusCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
    
    private object[] results;
    
    internal getSubmissionStatusCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
            base(exception, cancelled, userState) {
        this.results = results;
    }
    
   
    public object[] Result {
        get {
            this.RaiseExceptionIfNecessary();
            return ((object[])(this.results[0]));
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
public delegate void getSubmissionDetailsCompletedEventHandler(object sender, getSubmissionDetailsCompletedEventArgs e);

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
public partial class getSubmissionDetailsCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
    
    private object[] results;
    
    internal getSubmissionDetailsCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
            base(exception, cancelled, userState) {
        this.results = results;
    }
    
   
    public object[] Result {
        get {
            this.RaiseExceptionIfNecessary();
            return ((object[])(this.results[0]));
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
public delegate void getLanguagesCompletedEventHandler(object sender, getLanguagesCompletedEventArgs e);

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
public partial class getLanguagesCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
    
    private object[] results;
    
    internal getLanguagesCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
            base(exception, cancelled, userState) {
        this.results = results;
    }
    
   
    public object[] Result {
        get {
            this.RaiseExceptionIfNecessary();
            return ((object[])(this.results[0]));
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
public delegate void testFunctionCompletedEventHandler(object sender, testFunctionCompletedEventArgs e);

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
public partial class testFunctionCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
    
    private object[] results;
    
    internal testFunctionCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
            base(exception, cancelled, userState) {
        this.results = results;
    }
    
   
    public object[] Result {
        get {
            this.RaiseExceptionIfNecessary();
            return ((object[])(this.results[0]));
        }
    }
}
