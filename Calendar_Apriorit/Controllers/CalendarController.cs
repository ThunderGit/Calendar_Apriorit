using Calendar_Apriorit.Infastructure;
using Calendar_Apriorit.ViewModel;
using Microsoft.Owin.Security;
using System;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Calendar_Apriorit.Controllers
{
    public class CalendarController : BaseController
    {
       

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateNewEvent(/*EventVM model*/)
        {
           
            //if (ModelState.IsValid)
            {
                using (var CalendarDomain = WebContext.Factory.GetService<ICalendarDM>(WebContext.RootContext))
                {
                    EventVM model = new EventVM()
                    {
                        Title = "title",
                        EventInfo = new EventInfoVM() { Description = " description", IsRepeated = false, StartTime = DateTime.Today }

                    };
                    var claims = AuthenticationManager.AuthenticationResponseGrant.Identity.FindFirst(c => c.Type == ClaimTypes.Email);
                    var email = claims.Value;
                    
                    var result = await CalendarDomain.AddNewEvent(model,email);
                    
                    
                }
            }
            return View();
        }
    }

}