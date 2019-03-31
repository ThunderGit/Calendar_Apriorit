using Calendar_Apriorit.DAL.Entities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calendar_Apriorit.DAL.Identity
{
    public class ApplicationRoleManager : RoleManager<UserRole>
    {
        public ApplicationRoleManager(RoleStore<UserRole> store)
                    : base(store)
        { }
    }
}
