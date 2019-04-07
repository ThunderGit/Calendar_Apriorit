using Calendar_Apriorit.DAL.Entities;
using Calendar_Apriorit.DAL.Interfaces;
using Calendar_Apriorit.Infastructure;
using Calendar_Apriorit.ViewModel;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;


namespace Calendar_Apriorit.BLL
{
    public class CalendarDM : BaseDomain, ICalendarDM
    {
        #region Constructors
        public CalendarDM(IRootContext context) : base(context) { }

        public async Task<OperationDetails> AddNewEvent(EventVM eventVM,string EMail)
        {
            using (var Database = Context.Factory.GetService<IUnitOfWork>(Context.RootContext))
            {
                User user = await Database.UserManager.FindByEmailAsync(EMail);
                Calendar cal = user.UserCalendar;
                
                
            }
        }

       

        public Task<OperationDetails> EditEvent(EventVM eventVM, string EMail)
        {
            throw new NotImplementedException();
        }

        public Task<List<EventVM>> GetEvents(string EMail)
        {
            throw new NotImplementedException();
        }
        #endregion

    }
}
