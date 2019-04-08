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
        #endregion
        public async Task<OperationDetails> AddNewEvent(EventVM eventVM,string EMail)
        {
            using (var Database = Context.Factory.GetService<IUnitOfWork>(Context.RootContext))
            {
                User user = await Database.UserManager.FindByEmailAsync(EMail);
                Calendar cal = user.UserCalendar;
                if(cal == null)
                {
                    user.UserCalendar = new Calendar { User = user };
                    var result2 = await Database.UserManager.UpdateAsync(user);
                    if (result2.Errors.Count() > 0)
                        return new OperationDetails(false, result2.Errors.FirstOrDefault(), "");
                }
                GetCalendarFromVM(eventVM, cal);
                var result = await Database.UserManager.UpdateAsync(user);
                if (result.Errors.Count() > 0)
                    return new OperationDetails(false, result.Errors.FirstOrDefault(), "");
                await Database.SaveAsync();
                return new OperationDetails(true, "Success", "");
                
                
            }
        }
        private void GetCalendarFromVM(EventVM eventVM,Calendar calendar)
        {
            EventInfo eventInfo = Context.Mapper.MapTo<EventInfo, EventInfoVM>(eventVM.EventInfo);
            Event _event = Context.Mapper.MapTo<Event, EventVM>(eventVM);
            if (eventVM.EventInfo.IsRepeated)
            {
                RepeatInfo repeatInfo = Context.Mapper.MapTo<RepeatInfo, RepeatInfoVM>(eventVM.EventInfo.RepeatInfo);
                repeatInfo.EventInfo = eventInfo;
                eventInfo.RepeatInfo = repeatInfo;
            }
            

            eventInfo.EventForThisInfo = _event;
            _event.EventInfo = eventInfo;
            _event.Calendars = new List<Calendar>
            {
                calendar
            };
            if (calendar.Events.Count == 0)
                calendar.Events = new List<Event>();
            calendar.Events.Add(_event);

        }

        public Task<OperationDetails> EditEvent(EventVM eventVM, string EMail)
        {
            throw new NotImplementedException();
        }

        public async Task<List<EventVM>> GetEvents(string EMail)
        {
            using (var Database = Context.Factory.GetService<IUnitOfWork>(Context.RootContext))
            {
                User user = await Database.UserManager.FindByEmailAsync(EMail);
                Calendar cal = user.UserCalendar;


                return null;//заглушка
            }
        }
        

    }
}
