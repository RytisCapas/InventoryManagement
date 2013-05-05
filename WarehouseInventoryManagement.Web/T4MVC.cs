﻿// <auto-generated />
// This file was generated by a T4 template.
// Don't change it directly as your change would get overwritten.  Instead, make changes
// to the .tt file (i.e. the T4 template) and save it to regenerate this file.

// Make sure the compiler doesn't complain about missing Xml comments
#pragma warning disable 1591
#region T4MVC

using System;
using System.Diagnostics;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using System.Web.Mvc.Html;
using System.Web.Routing;
using T4MVC;

[GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
public static class MVC
{
    public static WarehouseInventoryManagement.Web.Controllers.AccountController Account = new WarehouseInventoryManagement.Web.Controllers.T4MVC_AccountController();
    public static WarehouseInventoryManagement.Web.Controllers.HomeController Home = new WarehouseInventoryManagement.Web.Controllers.T4MVC_HomeController();
    public static WarehouseInventoryManagement.Web.Controllers.ProductController Product = new WarehouseInventoryManagement.Web.Controllers.T4MVC_ProductController();
    public static T4MVC.SharedController Shared = new T4MVC.SharedController();
}

namespace T4MVC
{
}

namespace T4MVC
{
    [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
    public class Dummy
    {
        private Dummy() { }
        public static Dummy Instance = new Dummy();
    }
}

[GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
internal class T4MVC_System_Web_Mvc_ActionResult : System.Web.Mvc.ActionResult, IT4MVCActionResult
{
    public T4MVC_System_Web_Mvc_ActionResult(string area, string controller, string action, string protocol = null): base()
    {
        this.InitMVCT4Result(area, controller, action, protocol);
    }
     
    public override void ExecuteResult(System.Web.Mvc.ControllerContext context) { }
    
    public string Controller { get; set; }
    public string Action { get; set; }
    public string Protocol { get; set; }
    public RouteValueDictionary RouteValueDictionary { get; set; }
}



namespace Links
{
    [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
    public static class Content {
        private const string URLPATH = "~/Content";
        public static string Url() { return T4MVCHelpers.ProcessVirtualPath(URLPATH); }
        public static string Url(string fileName) { return T4MVCHelpers.ProcessVirtualPath(URLPATH + "/" + fileName); }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public static class css {
            private const string URLPATH = "~/Content/css";
            public static string Url() { return T4MVCHelpers.ProcessVirtualPath(URLPATH); }
            public static string Url(string fileName) { return T4MVCHelpers.ProcessVirtualPath(URLPATH + "/" + fileName); }
            [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
            public static class images {
                private const string URLPATH = "~/Content/css/images";
                public static string Url() { return T4MVCHelpers.ProcessVirtualPath(URLPATH); }
                public static string Url(string fileName) { return T4MVCHelpers.ProcessVirtualPath(URLPATH + "/" + fileName); }
                public static readonly string add_button_gif = Url("add-button.gif")+"?"+T4MVCHelpers.TimestampString(URLPATH + "/add-button.gif");
                public static readonly string button_gif = Url("button.gif")+"?"+T4MVCHelpers.TimestampString(URLPATH + "/button.gif");
                public static readonly string close_gif = Url("close.gif")+"?"+T4MVCHelpers.TimestampString(URLPATH + "/close.gif");
                public static readonly string del_gif = Url("del.gif")+"?"+T4MVCHelpers.TimestampString(URLPATH + "/del.gif");
                public static readonly string edit_gif = Url("edit.gif")+"?"+T4MVCHelpers.TimestampString(URLPATH + "/edit.gif");
                public static readonly string footer_gif = Url("footer.gif")+"?"+T4MVCHelpers.TimestampString(URLPATH + "/footer.gif");
                public static readonly string header_gif = Url("header.gif")+"?"+T4MVCHelpers.TimestampString(URLPATH + "/header.gif");
                public static readonly string msg_error_gif = Url("msg-error.gif")+"?"+T4MVCHelpers.TimestampString(URLPATH + "/msg-error.gif");
                public static readonly string msg_ok_gif = Url("msg-ok.gif")+"?"+T4MVCHelpers.TimestampString(URLPATH + "/msg-ok.gif");
                public static readonly string pagging_gif = Url("pagging.gif")+"?"+T4MVCHelpers.TimestampString(URLPATH + "/pagging.gif");
                public static readonly string tab_gif = Url("tab.gif")+"?"+T4MVCHelpers.TimestampString(URLPATH + "/tab.gif");
                public static readonly string th_gif = Url("th.gif")+"?"+T4MVCHelpers.TimestampString(URLPATH + "/th.gif");
            }
        
            public static readonly string style_css = T4MVCHelpers.IsProduction() && T4Extensions.FileExists(URLPATH + "/style.min.css") ? Url("style.min.css")+"?"+T4MVCHelpers.TimestampString(URLPATH + "/style.min.css") : Url("style.css")+"?"+T4MVCHelpers.TimestampString(URLPATH + "/style.css");
                 
        }
    
    }

    [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
    public static partial class Bundles
    {
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public static partial class Scripts {}
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public static partial class Styles {}
    }
}

[GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
internal static class T4MVCHelpers {
    // You can change the ProcessVirtualPath method to modify the path that gets returned to the client.
    // e.g. you can prepend a domain, or append a query string:
    //      return "http://localhost" + path + "?foo=bar";
    private static string ProcessVirtualPathDefault(string virtualPath) {
        // The path that comes in starts with ~/ and must first be made absolute
        string path = VirtualPathUtility.ToAbsolute(virtualPath);
        
        // Add your own modifications here before returning the path
        return path;
    }

    // Calling ProcessVirtualPath through delegate to allow it to be replaced for unit testing
    public static Func<string, string> ProcessVirtualPath = ProcessVirtualPathDefault;

    // Calling T4Extension.TimestampString through delegate to allow it to be replaced for unit testing and other purposes
    public static Func<string, string> TimestampString = System.Web.Mvc.T4Extensions.TimestampString;

    // Logic to determine if the app is running in production or dev environment
    public static bool IsProduction() { 
        return (HttpContext.Current != null && !HttpContext.Current.IsDebuggingEnabled); 
    }
}





#endregion T4MVC
#pragma warning restore 1591

