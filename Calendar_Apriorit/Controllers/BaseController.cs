using Calendar_Apriorit.Infastructure;
using Calendar_Apriorit.Initialazer;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Calendar_Apriorit.Controllers
{
    public class BaseController : Controller
    {
        #region Constructors
        public BaseController()
        {
            WebContext = new WebContext(ConfigurationManager.ConnectionStrings["Calendar_Apriorit"].ConnectionString);
        }
        #endregion

        protected IWebContext WebContext { get; }


        protected override void OnException(ExceptionContext filterContext)
        {
            if (filterContext.HttpContext.Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            {
                filterContext.Result = new JsonResult
                {
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                    Data = new { error = true, message = filterContext.Exception.Message }
                };
            }
            else
            {
                base.OnException(filterContext);
            }
        }
    }

}