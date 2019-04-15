using Calendar_Apriorit.Infastructure;
using Calendar_Apriorit.ViewModel;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.IO;
using System.Text;
using Newtonsoft.Json;

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
        public async Task<string> CreateNewEvent(string EventVM,string EventInfoVM,string RepeatInfoVM)
        {
            EventVM model = new EventVM();
            model = DeserializeEventVM(EventVM, EventInfoVM, RepeatInfoVM);

            //if (ModelState.IsValid)
            {
                using (var CalendarDomain = WebContext.Factory.GetService<ICalendarDM>(WebContext.RootContext))
                {
                    

                    var email2 = User.Identity.Name;

                    OperationDetails result = await CalendarDomain.AddNewEvent(model, email2);
                    if (result.Succedeed)
                        return "Успех";
                    else return "Провал :" + result.Message;






                }
            }
           
        }

        private static EventVM DeserializeEventVM(string EventVM, string EventInfoVM, string RepeatInfoVM)
        {
            EventVM model;
            using (var ms = new MemoryStream(Encoding.Unicode.GetBytes((string)EventVM)))
            {
                var serialiser = new DataContractJsonSerializer(typeof(EventVM));
                model = (EventVM)serialiser.ReadObject(ms);
               
            }
            using (var ms = new MemoryStream(Encoding.Unicode.GetBytes((string)EventInfoVM)))
            {
                var serialiser = new DataContractJsonSerializer(typeof(EventInfoVM), new DataContractJsonSerializerSettings() { DateTimeFormat = new DateTimeFormat("yyyy'-'MM'-'dd'T'HH':'mm") });
                //model = (EventVM)serialiser.ReadObject(ms);
                model.EventInfo = (EventInfoVM)serialiser.ReadObject(ms);
                if (model.EventInfo.IsRepeated == true)
                {
                    using (var ms2 = new MemoryStream(Encoding.Unicode.GetBytes(RepeatInfoVM)))
                    {
                        var serialiser2 = new DataContractJsonSerializer(typeof(RepeatInfoVM));

                        model.EventInfo.RepeatInfo = (RepeatInfoVM)serialiser2.ReadObject(ms2);

                    }

                }
            }

            return model;
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
