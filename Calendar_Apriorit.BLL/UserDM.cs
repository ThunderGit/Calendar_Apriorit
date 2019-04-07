using Calendar_Apriorit.DAL.Entities;
using Calendar_Apriorit.DAL.Interfaces;
using Calendar_Apriorit.Infastructure;
using Calendar_Apriorit.ViewModel;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Calendar_Apriorit.BLL
{
    public class UserDM : BaseDomain, IUserDM
    {
        #region Constructors
        public UserDM(IRootContext context) : base(context) { }
        #endregion
        public async Task<ClaimsIdentity> Authenticate(LoginVM authUser)
        {
            using (var repo = Context.Factory.GetService<IUnitOfWork>(Context.RootContext))
            {
                ClaimsIdentity claim = null;
                // находим пользователя
                User user = await repo.UserManager.FindAsync(authUser.Email, authUser.Password);
                // авторизуем его и возвращаем объект ClaimsIdentity
                if (user != null)
                    claim = await repo.UserManager.CreateIdentityAsync(user,
                                                DefaultAuthenticationTypes.ApplicationCookie);
                return claim;
            }

        }

        public async Task<OperationDetails> Create(RegisterVM regUser)
        {
            using (var Database = Context.Factory.GetService<IUnitOfWork>(Context.RootContext))
            {
                User user = await Database.UserManager.FindByEmailAsync(regUser.Email);
                if (user == null)
                {
                    user = new User { Email = regUser.Email, UserName = regUser.Email };
                    user.UserCalendar = new Calendar() { User = user };
                    var result = await Database.UserManager.CreateAsync(user, regUser.Password);
                    if (result.Errors.Count() > 0)
                        return new OperationDetails(false, result.Errors.FirstOrDefault(), "");
                    await Database.UserManager.AddToRoleAsync(user.Id, regUser.Role);
                    
                    await Database.SaveAsync();
                    return new OperationDetails(true, "Регистрация успешно пройдена", "");
                }
                else
                {
                    return new OperationDetails(false, "Пользователь с таким логином уже существует", "Email");
                }
            }
        }
        

        public async Task SetInitialData(RegisterVM user, List<string> roles)
        {
            using (var Database = Context.Factory.GetService<IUnitOfWork>(Context.RootContext))
            {
                foreach (string roleName in roles)
                {
                    var role = await Database.RoleManager.FindByNameAsync(roleName);
                    if (role == null)
                    {
                        role = new UserRole { Name = roleName };
                        await Database.RoleManager.CreateAsync(role);
                    }
                }
                await Create(user);

            }
                
        }
    }
}
