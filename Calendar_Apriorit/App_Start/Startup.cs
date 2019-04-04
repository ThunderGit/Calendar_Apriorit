using System;
using System.Threading.Tasks;
using Calendar_Apriorit.Infastructure;
using Calendar_Apriorit.Initialazer;
using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;

[assembly: OwinStartup(typeof(Calendar_Apriorit.App_Start.Startup))]

namespace Calendar_Apriorit.App_Start
{

    public class Startup
    {
        

        public void Configuration(IAppBuilder app)
        {
            //app.CreatePerOwinContext(() => DependencyResolver.Current.GetService<ApplicationUserManager>());
            //app.CreatePerOwinContext(() => DependencyResolver.Current.GetService<ApplicationSignInManager>());
            //app.CreatePerOwinContext<IUserDM>(CreateUserService);
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login"),
            });
        }

        //private IUserDM CreateUserService()
        //{
        //    return Context = new WebContext("DefaultConnection");
        //}
    }
}
