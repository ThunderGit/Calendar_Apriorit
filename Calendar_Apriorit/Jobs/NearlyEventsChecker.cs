using Calendar_Apriorit.Hubs;
using Calendar_Apriorit.Infastructure;
using Calendar_Apriorit.Initialazer;
using Quartz;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.IO;
using System.Timers;

namespace Calendar_Apriorit.Jobs
{
    public class NearlyEventsChecker
    {
        #region Constructors
        static NearlyEventsChecker()
        {
            WebContext = new WebContext(ConfigurationManager.ConnectionStrings["Calendar_Apriorit"].ConnectionString);
            
        }
        #endregion
        private static Timer aTimer;
        protected static IWebContext WebContext { get; }
        public static void StartTimer()
        {
            // Create a timer and set a 60 second interval.
            aTimer = new Timer
            {
                Interval = 2*60000
            };

            // Hook up the Elapsed event for the timer. 
            aTimer.Elapsed += DoWork;

            // Have the timer fire repeated events (true is the default)
            aTimer.AutoReset = true;

            // Start the timer
            aTimer.Enabled = true;
        }
        public static void DoWork(object obj, System.Timers.ElapsedEventArgs e)
        {
            using (var CalendarDomain = WebContext.Factory.GetService<ICalendarDM>(WebContext.RootContext))
            {
                using (StreamWriter str = new StreamWriter("D:\\calendar\\job.txt", true))
                {
                    str.WriteLine(DateTime.Now);

                }

                var EventsAndUsers = CalendarDomain.GetAllEventsThatStartsInHour();
                var contextHub = Microsoft.AspNet.SignalR.GlobalHost.ConnectionManager.GetHubContext<NotificationHub>();
                foreach(var userAndEvents in EventsAndUsers)
                {
                    string titles = "";
                    foreach (var Event in userAndEvents.Value)
                    {
                        titles = titles + " " + Event.Title;

                    }
                    contextHub.Clients.Group(userAndEvents.Key).displayNotification(titles);

                }
            }
        }
    }
}