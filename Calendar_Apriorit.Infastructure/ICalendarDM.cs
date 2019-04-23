using Calendar_Apriorit.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calendar_Apriorit.Infastructure
{
    public interface ICalendarDM : IDisposable
    {
        //Task<OperationDetails> CreateFirstCalendar();
        Task<OperationDetails> AddNewEvent(EventVM eventVM, string EMail);
        Task<OperationDetails> EditEvent(EventVM eventVM, string EMail);
        Task<List<EventVM>> GetEvents(string EMail);
        Task<List<EventVM>> GetEventsById(string Email, params int[] IdEvents);
        Dictionary<string, List<EventVM>> GetAllEventsThatStartsInHour();


    }
}
