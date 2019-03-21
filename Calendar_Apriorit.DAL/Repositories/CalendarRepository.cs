using Calendar_Apriorit.DAL.Entities;
using Calendar_Apriorit.DAL.EntityFramework;
using Calendar_Apriorit.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calendar_Apriorit.DAL.Repositories
{
    public class CalendarRepository : IRepository<Calendar>
    {
        public CalendarRepository(ApplicationContext context)
        {
            this.context = context;
        }
        private ApplicationContext context;

        public void Create(Calendar item)
        {
            context.Calendars.Add(item);
        }

        public void Delete(int id)
        {
            Calendar item = context.Calendars.Find(id);
            if(item != null)
            {
                context.Calendars.Remove(item);
            }
        }

        public IEnumerable<Calendar> Find(Func<Calendar, bool> predicate)
        {
            throw new NotImplementedException();
        }

        public Calendar Get(int id)
        {
            return context.Calendars.Find(id);
        }

        public IEnumerable<Calendar> GetAll()
        {
            return context.Calendars;
        }

        public void Update(Calendar item)
        {
            context.Entry(item).State = EntityState.Modified;
        }
    }
}
