using Calendar_Apriorit.DAL.Entities;
using Calendar_Apriorit.DAL.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calendar_Apriorit.DAL.Interfaces
{
    /// <summary>
    /// Connect with database by using pattern UnitOfWork
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        ApplicationUserManager UserManager { get; }
        IClientManager ClientManager { get; }
        ApplicationRoleManager RoleManager { get; }
        IRepository<Calendar> Calendars { get; } 
        Task SaveAsync();
    }
}
