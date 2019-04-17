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

        static ApplicationContext() 
        {
            Database.SetInitializer<ApplicationContext>(new ContextInitializer());

        }

        public ApplicationContext() : base("Data Source = (LocalDB)\\MSSQLLocalDB; AttachDbFilename='|DataDirectory|\\Calendar_Apriorit.mdf';Integrated Security = True")
        { }
        //public DbSet<UserInfo> UserInfos { get; set; }

        public DbSet<Calendar> Calendars { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Group> UserGroups { get; set; }
        public DbSet<EventInfo> EventInfos { get; set; }
        public DbSet<RepeatInfo> RepeatInfos { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Calendar>()
                .HasRequired(c => c.User)
                .WithOptional(c => c.UserCalendar);

            modelBuilder.Entity<Calendar>()
                .HasMany(c => c.Events)
                .WithMany(e => e.Calendars);

            modelBuilder.Entity<Event>()
                .HasRequired(e => e.EventInfo)
                .WithRequiredPrincipal(ei => ei.EventForThisInfo);

            modelBuilder.Entity<RepeatInfo>()
                .HasRequired(RepInfo => RepInfo.EventInfo)
                .WithOptional(EventInfo => EventInfo.RepeatInfo);

            modelBuilder.Entity<Group>()
                .HasRequired(g => g.Event)
                .WithOptional(e => e.Group);

            modelBuilder.Entity<User>()
                .HasMany(u => u.UserGroups)
                .WithMany(gr => gr.Users);
                


            base.OnModelCreating(modelBuilder);
        }

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
