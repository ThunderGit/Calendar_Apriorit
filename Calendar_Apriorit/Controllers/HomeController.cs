using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Calendar_Apriorit.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }
            else return RedirectToAction("Calendar", "Calendar");
            
        }
        
    }
}