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
        /// <summary>
        /// Maybe useless method, need to think
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public IEnumerable<Calendar> Find(Func<Calendar, bool> predicate)
        {
            return context.Calendars.Where(predicate).ToList();
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
        /// <summary>
        /// Метод по сути ничего не делает в этой реализации,надо ли контекст диспозить, если в EFUnitOfWork context диспозится
        /// </summary>
        public void Dispose()
        {
            
            //возможно здесь нужно вызывать диспоуз на контекст, но я не знаю если честно
        }
    }
}
