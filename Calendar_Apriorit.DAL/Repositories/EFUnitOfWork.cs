using Calendar_Apriorit.DAL.Entities;
using Calendar_Apriorit.DAL.EntityFramework;
using Calendar_Apriorit.DAL.Identity;
using Calendar_Apriorit.DAL.Interfaces;
using Calendar_Apriorit.Infastructure;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Calendar_Apriorit.DAL.Repositories
{
    public class EFUnitOfWork : IUnitOfWork
    {
        private ApplicationContext context;

        private ApplicationUserManager userManager;
        private ApplicationRoleManager roleManager;
        private IClientManager clientManager;
        private GenericRepository<Calendar> calendarRepository;

        //public EFUnitOfWork(string connectionString)
        //{
        //    context = new ApplicationContext(connectionString);
        //    userManager = new ApplicationUserManager(new UserStore<User>(context));
        //    roleManager = new ApplicationRoleManager(new RoleStore<UserRole>(context));
        //    clientManager = new ClientManager(context);
        //    calendarRepository = new GenericRepository<Calendar>(context);
        //}
        public EFUnitOfWork(IRootContext cont)
        {
            context = new ApplicationContext(cont.ConnectionString);
            userManager = new ApplicationUserManager(new UserStore<User>(context));
            roleManager = new ApplicationRoleManager(new RoleStore<UserRole>(context));
            clientManager = new ClientManager(context);
            calendarRepository = new GenericRepository<Calendar>(context);
        }
        public EFUnitOfWork()//видел что юнити нужен конструктор без параметров
        {
            context = new ApplicationContext("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename='|DataDirectory|\\Calendar_Apriorit.mdf';Integrated Security=True");
            userManager = new ApplicationUserManager(new UserStore<User>(context));
            roleManager = new ApplicationRoleManager(new RoleStore<UserRole>(context));
            clientManager = new ClientManager(context);
            calendarRepository = new GenericRepository<Calendar>(context);
        }
        public ApplicationUserManager UserManager
        {
            get { return userManager; }
        }

        public IClientManager ClientManager
        {
            get { return clientManager; }
        }
        public ApplicationRoleManager RoleManager
        {
            get { return roleManager; }
        }
        public IRepository<Calendar> Calendars
        {
            get { return calendarRepository; }
        }
        public async Task SaveAsync()
        {
            await context.SaveChangesAsync();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        private bool disposed = false;
        private void FixEfProviderServicesProblem()
        {
            // The Entity Framework provider type 'System.Data.Entity.SqlServer.SqlProviderServices, EntityFramework.SqlServer'
            // for the 'System.Data.SqlClient' ADO.NET provider could not be loaded. 
            // Make sure the provider assembly is available to the running application. 
            // See http://go.microsoft.com/fwlink/?LinkId=260882 for more information.
            var instance = System.Data.Entity.SqlServer.SqlProviderServices.Instance;
        }

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();//Уточнить нужность диспоуза контекста
                    userManager.Dispose();
                    roleManager.Dispose();
                    clientManager.Dispose();
                    calendarRepository.Dispose();

                }
                this.disposed = true;
            }
        }
    }
}
