using System.Collections;
using System.Collections.Generic;

namespace Calendar_Apriorit.DAL.Entities
{
    public class Group
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<User> Users { get; set; }
        public virtual ICollection<Event> Events { get; set; }

    }
}