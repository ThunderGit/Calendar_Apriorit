using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calendar_Apriorit.DAL.Entities
{
    /// <summary>
    /// Main inormation, needs for login
    /// </summary>
    public class User : IdentityUser
    {
        public virtual UserInfo UserInfo { get; set; }
        public virtual Calendar UserCalendar { get; set; }
        public virtual ICollection<Group> UserGroups { get; set; }

    }
}
