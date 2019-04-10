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

        public ActionResult CreateNewEvent()
        {
            return View();
        }
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateNewEvent(EventVM model)
        {
           
            //if (ModelState.IsValid)
            {
                using (var CalendarDomain = WebContext.Factory.GetService<ICalendarDM>(WebContext.RootContext))
                {
                    //EventVM model = new EventVM()
                    //{
                    //    Title = "title",
                    //    EventInfo = new EventInfoVM() { Description = " description", IsRepeated = false, StartTime = new DateTime(2019,4,11,15,0,0), EndTime = new DateTime(2019, 4, 11, 17, 0, 0) }

                    //};
                    
                    var email2 = User.Identity.Name;
                        
                    OperationDetails result = await CalendarDomain.AddNewEvent(model, email2);
                    if (result.Succedeed)
                        return View("SuccessRegister");
                        


                  
                    
                    
                }
            }
            return View();
        }
        

        public async Task<List<EventVM>> ShowEvents()
        {
            using (var CalendarDomain = WebContext.Factory.GetService<ICalendarDM>(WebContext.RootContext))
            {
                string email = User.Identity.Name;
                List<EventVM> events = await CalendarDomain.GetEvents(email);
                if (events != null)
                {
                    return events;
                }
                else
                {
                    return null;
                }
            }
        }
    }

}
