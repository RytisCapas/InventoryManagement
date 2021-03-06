// <auto-generated />
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
namespace WarehouseInventoryManagement.Web.Controllers
{
    public partial class ItemLogController
    {
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        protected ItemLogController(Dummy d) { }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        protected RedirectToRouteResult RedirectToAction(ActionResult result)
        {
            var callInfo = result.GetT4MVCResult();
            return RedirectToRoute(callInfo.RouteValueDictionary);
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        protected RedirectToRouteResult RedirectToActionPermanent(ActionResult result)
        {
            var callInfo = result.GetT4MVCResult();
            return RedirectToRoutePermanent(callInfo.RouteValueDictionary);
        }

        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.ActionResult List()
        {
            return new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.List);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.ActionResult ExportLogsToCsv()
        {
            return new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.ExportLogsToCsv);
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ItemLogController Actions { get { return MVC.ItemLog; } }
        [GeneratedCode("T4MVC", "2.0")]
        public readonly string Area = "";
        [GeneratedCode("T4MVC", "2.0")]
        public readonly string Name = "ItemLog";
        [GeneratedCode("T4MVC", "2.0")]
        public const string NameConst = "ItemLog";

        static readonly ActionNamesClass s_actions = new ActionNamesClass();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionNamesClass ActionNames { get { return s_actions; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionNamesClass
        {
            public readonly string List = "List";
            public readonly string ExportLogsToCsv = "ExportLogsToCsv";
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionNameConstants
        {
            public const string List = "List";
            public const string ExportLogsToCsv = "ExportLogsToCsv";
        }


        static readonly ActionParamsClass_List s_params_List = new ActionParamsClass_List();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_List ListParams { get { return s_params_List; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_List
        {
            public readonly string model = "model";
        }
        static readonly ActionParamsClass_ExportLogsToCsv s_params_ExportLogsToCsv = new ActionParamsClass_ExportLogsToCsv();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_ExportLogsToCsv ExportLogsToCsvParams { get { return s_params_ExportLogsToCsv; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_ExportLogsToCsv
        {
            public readonly string model = "model";
        }
        static readonly ViewsClass s_views = new ViewsClass();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ViewsClass Views { get { return s_views; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ViewsClass
        {
            static readonly _ViewNamesClass s_ViewNames = new _ViewNamesClass();
            public _ViewNamesClass ViewNames { get { return s_ViewNames; } }
            public class _ViewNamesClass
            {
                public readonly string List = "List";
            }
            public readonly string List = "~/Views/ItemLog/List.cshtml";
            static readonly _PartialClass s_Partial = new _PartialClass();
            public _PartialClass Partial { get { return s_Partial; } }
            [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
            public partial class _PartialClass
            {
                static readonly _ViewNamesClass s_ViewNames = new _ViewNamesClass();
                public _ViewNamesClass ViewNames { get { return s_ViewNames; } }
                public class _ViewNamesClass
                {
                    public readonly string FilterForm = "FilterForm";
                }
                public readonly string FilterForm = "~/Views/ItemLog/Partial/FilterForm.cshtml";
            }
        }
    }

    [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
    public partial class T4MVC_ItemLogController : WarehouseInventoryManagement.Web.Controllers.ItemLogController
    {
        public T4MVC_ItemLogController() : base(Dummy.Instance) { }

        partial void ListOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, WarehouseInventoryManagement.Models.Models.DateFilter model);

        public override System.Web.Mvc.ActionResult List(WarehouseInventoryManagement.Models.Models.DateFilter model)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.List);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "model", model);
            ListOverride(callInfo, model);
            return callInfo;
        }

        partial void ExportLogsToCsvOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, WarehouseInventoryManagement.Models.Models.DateFilter model);

        public override System.Web.Mvc.ActionResult ExportLogsToCsv(WarehouseInventoryManagement.Models.Models.DateFilter model)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.ExportLogsToCsv);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "model", model);
            ExportLogsToCsvOverride(callInfo, model);
            return callInfo;
        }

    }
}

#endregion T4MVC
#pragma warning restore 1591
