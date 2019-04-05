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
        public DbSet<Event> Events { get; set; }
        public DbSet<Group> UserGroups { get; set; }
        public DbSet<EventInfo> EventInfos { get; set; }
        public DbSet<RepeatInfo> RepeatInfos { get; set; }

        private void FixEfProviderServicesProblem()
        {
            // The Entity Framework provider type 'System.Data.Entity.SqlServer.SqlProviderServices, EntityFramework.SqlServer'
            // for the 'System.Data.SqlClient' ADO.NET provider could not be loaded. 
            // Make sure the provider assembly is available to the running application. 
            // See http://go.microsoft.com/fwlink/?LinkId=260882 for more information.
            var instance = System.Data.Entity.SqlServer.SqlProviderServices.Instance;
        }
    }
}
