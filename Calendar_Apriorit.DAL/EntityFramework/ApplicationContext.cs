using Calendar_Apriorit.DAL.Entities;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calendar_Apriorit.DAL.EntityFramework
{
    /// <summary>
    /// Maybe need use other context for others classes
    /// </summary>
    public class ApplicationContext : IdentityDbContext<User> //that`s way class has already propety Users and Roles
    {
        public ApplicationContext(string conectionString) : base(conectionString) { }

        public DbSet<UserInfo> UserInfos { get; set; }

        public DbSet<Calendar> Calendars { get; set; }
    }
}
