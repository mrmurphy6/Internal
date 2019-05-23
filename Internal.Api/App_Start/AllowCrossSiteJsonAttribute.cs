using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Mvc;
using Common;

namespace Internal.Api.App_Start
{
    public class AllowCrossSiteJsonAttribute: ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            List<string> _domains = WebConfigHelper.GetStringValue("CrossDomain").SplitToList<string>(';');
            var context = filterContext.RequestContext.HttpContext;
            var host = context.Request.UrlReferrer?.Host;
            if (host != null && _domains.Contains(host))
            {
                filterContext.RequestContext.HttpContext.Response.AddHeader("Access-Control-Allow-Origin", "*");
            }
            base.OnActionExecuting(filterContext);
        }
    }
}