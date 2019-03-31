using Calendar_Apriorit.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calendar_Apriorit.DAL.Interfaces
{
    public interface IClientManager : IDisposable
    {
        void Create(UserInfo item);
    }
}
