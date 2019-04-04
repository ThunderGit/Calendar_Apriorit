using Calendar_Apriorit.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Calendar_Apriorit.Infastructure
{
    public interface IUserDM : IDisposable
    {
        Task<OperationDetails> Create(RegisterVM regUser);
        Task<ClaimsIdentity> Authenticate(LoginVM user);
        Task SetInitialData(RegisterVM user, List<string> roles);
    }
}
