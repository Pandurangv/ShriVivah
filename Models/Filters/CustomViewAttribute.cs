using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShriVivah.Models.Filters
{
    public class CustomViewAttribute: ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            var result = filterContext.Result as ViewResultBase;
            if (result != null)
            {
                if (string.IsNullOrEmpty(result.ViewName))
                {
                    result.ViewName = "Index";
                }
                result.ViewName= result.ViewName+SettingsManager.Instance.Branding;
            }
        }
    }
}