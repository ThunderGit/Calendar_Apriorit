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
                if (cal == null)
                {
                    user.UserCalendar = new Calendar { User = user };
                    var result2 = await Database.UserManager.UpdateAsync(user);
                    if (result2.Errors.Count() > 0)
                        return new OperationDetails(false, result2.Errors.FirstOrDefault(), "");
                }
                if (!IsEventSatisfiesSchedule(eventVM.EventInfo.StartTime, eventVM.EventInfo.EndTime, cal))
                    return new OperationDetails(false, "Event isn`t satisfies schedule", "DateTimes");
                AddFromEventVMtoCalendar(eventVM, cal);
                var result = await Database.UserManager.UpdateAsync(user);
                if (result.Errors.Count() > 0)
                    return new OperationDetails(false, result.Errors.FirstOrDefault(), "");
                await Database.SaveAsync();
                return new OperationDetails(true, "Success", "");


            }
        }

        private bool IsEventSatisfiesSchedule(DateTime start,DateTime end, Calendar cal)
        {
            
            bool IsAnyEventsThatStartsBetweenStartAndEndTime = cal.Events.Where(ev => ev.EventInfo.StartTime.CompareTo(start) >= 0 && ev.EventInfo.EndTime.CompareTo(start) <= 0).ToList().Count() > 0;
            bool IsAnyEventsThatEndsBetweenStartAndEndTime = cal.Events.Where(ev => ev.EventInfo.StartTime.CompareTo(end) >= 0 && ev.EventInfo.EndTime.CompareTo(end) <= 0).ToList().Count() > 0;
            if (IsAnyEventsThatStartsBetweenStartAndEndTime || IsAnyEventsThatEndsBetweenStartAndEndTime)
                return false;
            else return true;

        }

        private void AddFromEventVMtoCalendar(EventVM eventVM,Calendar calendar)
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

        public async Task<OperationDetails> EditEvent(EventVM eventVM, string EMail)
        {
            using (var Database = Context.Factory.GetService<IUnitOfWork>(Context.RootContext))
            {
                User user = await Database.UserManager.FindByEmailAsync(EMail);
                Calendar cal = user.UserCalendar;
                if (!IsEventSatisfiesSchedule(eventVM.EventInfo.StartTime, eventVM.EventInfo.EndTime, cal))
                    return new OperationDetails(false, "Edit event isn`t satisfies schedule", "DateTimes");
                Event editThisEvent = cal.Events.First(c => c.Id == eventVM.IdEvent);
                if (editThisEvent == null)
                    return new OperationDetails(false, "Old event didn`t find in database for this user", "");

                try
                {
                    await EditEventInDatabase(Database, editThisEvent, eventVM);
                }
                catch(Exception ex)
                {
                    return new OperationDetails(false, ex.Message, "");
                }

                return new OperationDetails(true, "Done", "");




            }
        }

        private async Task EditEventInDatabase(IUnitOfWork database, Event editThisEvent, EventVM eventVM)
        {
            EventInfo eventInfoEdit = editThisEvent.EventInfo;
            RepeatInfo repeatInfoEdit = eventInfoEdit.RepeatInfo;
            editThisEvent.Title = eventVM.Title;
            //проверить не создает ли автомаппер новую ссылку
            eventInfoEdit = Context.Mapper.MapTo<EventInfo, EventInfoVM>(eventVM.EventInfo);
            if(eventVM.EventInfo.IsRepeated == false)
            {
                eventInfoEdit.RepeatInfo = null;
                database.RepeatInfos.Remove(repeatInfoEdit);
            }
            else
            {
                repeatInfoEdit = Context.Mapper.MapTo<RepeatInfo, RepeatInfoVM>(eventVM.EventInfo.RepeatInfo);
            }
            database.Events.Update(editThisEvent);
            await database.SaveAsync();

            

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
