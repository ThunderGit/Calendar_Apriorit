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
        public async Task<OperationDetails> AddNewEvent(EventVM eventVM, string EMail)
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

        private bool IsEventSatisfiesSchedule(DateTime start, DateTime end, Calendar cal)
        {

            bool IsAnyEventsThatStartsBetweenStartAndEndTime = cal.Events.Where(ev => ev.EventInfo.StartTime.CompareTo(start) >= 0 && ev.EventInfo.EndTime.CompareTo(start) <= 0).ToList().Count() > 0;
            bool IsAnyEventsThatEndsBetweenStartAndEndTime = cal.Events.Where(ev => ev.EventInfo.StartTime.CompareTo(end) >= 0 && ev.EventInfo.EndTime.CompareTo(end) <= 0).ToList().Count() > 0;
            if (IsAnyEventsThatStartsBetweenStartAndEndTime || IsAnyEventsThatEndsBetweenStartAndEndTime)
                return false;
            else return true;

        }

        private void AddFromEventVMtoCalendar(EventVM eventVM, Calendar calendar)
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
                catch (Exception ex)
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
            eventInfoEdit = Context.Mapper.MapTo<EventInfo, EventInfoVM>(eventVM.EventInfo);
            if (eventVM.EventInfo.IsRepeated == false)
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
        public async Task<List<EventVM>> GetEvents(string Email)
        {
            using (var Database = Context.Factory.GetService<IUnitOfWork>(Context.RootContext))
            {
                User user = await Database.UserManager.FindByEmailAsync(Email);
                Calendar cal = user.UserCalendar;

                if (cal == null)
                {
                    return null;
                }

                List<Event> events = cal.Events.ToList<Event>();
                List<EventVM> _eventsVM = Context.Mapper.MapTo<List<EventVM>, List<Event>>(events);
                List<EventVM> RepeatEvents = _eventsVM.Where(ev => ev.EventInfo.IsRepeated == true).ToList();
                MultiplyRepeatEvents(_eventsVM, RepeatEvents);
                return _eventsVM;
            }
        }

        private void MultiplyRepeatEvents(List<EventVM> _eventsVM, List<EventVM> RepeatEvents)
        {
            foreach (var _eventVM in RepeatEvents)
            {
                if (!_eventVM.EventInfo.RepeatInfo.IsUnlimitedQuanties)
                {
                    for (int i = 0; i < _eventVM.EventInfo.RepeatInfo.QuantityRepeats; i++)
                    {
                        _eventsVM.Add(new EventVM
                        {
                            IdEvent = _eventVM.IdEvent,
                            Title = _eventVM.Title,
                            EventInfo = new EventInfoVM()
                            {
                                Description = _eventVM.EventInfo.Description,
                                IsRepeated = true,
                                RepeatInfo = null,
                                EndTime = CalculateTimeSpanForEvent(_eventVM.EventInfo.EndTime, _eventVM.EventInfo.RepeatInfo.TypeRepeat, i + 1),
                                StartTime = CalculateTimeSpanForEvent(_eventVM.EventInfo.StartTime, _eventVM.EventInfo.RepeatInfo.TypeRepeat, i + 1)


                            }

                        });
                    }
                }
                else
                {
                    for (int i = 0; i < 25; i++)
                    {
                        _eventsVM.Add(new EventVM
                        {
                            IdEvent = _eventVM.IdEvent,
                            Title = _eventVM.Title,
                            EventInfo = new EventInfoVM()
                            {
                                Description = _eventVM.EventInfo.Description,
                                IsRepeated = true,
                                RepeatInfo = null,
                                EndTime = CalculateTimeSpanForEvent(_eventVM.EventInfo.EndTime, _eventVM.EventInfo.RepeatInfo.TypeRepeat, i + 1),
                                StartTime = CalculateTimeSpanForEvent(_eventVM.EventInfo.StartTime, _eventVM.EventInfo.RepeatInfo.TypeRepeat, i + 1)


                            }

                        });
                    }
                }
            }
        }

        private DateTime CalculateTimeSpanForEvent(DateTime dateEvent, TypeRepeat type, int numberOfRepeat)
        {
            DateTime date = DateTime.Parse(dateEvent.ToString());

            switch (type)
            {
                case TypeRepeat.Week:

                    return date.AddDays(7*numberOfRepeat);

                case TypeRepeat.Month:
                    date.AddMonths(numberOfRepeat);
                    return date;

                case TypeRepeat.Year:
                    date.AddYears(numberOfRepeat);
                    return date;
                default:
                    throw new ArgumentException();

            }

        }

        public async Task<List<EventVM>> GetEventsById(string Email, params int[] IdEvents)
        {
            using (var Database = Context.Factory.GetService<IUnitOfWork>(Context.RootContext))
            {
                User user = await Database.UserManager.FindByEmailAsync(Email);
                Calendar cal = user.UserCalendar;

                if (cal == null)
                {
                    return null;
                }

                List<Event> events = cal.Events.
                    Where(ev => IdEvents.First(idFromView => idFromView == ev.Id) == ev.Id)
                    .ToList<Event>();

                List<EventVM> _eventsVM = Context.Mapper.MapTo<List<EventVM>, List<Event>>(events);

                return _eventsVM;
            }
        }

    }
    
}
