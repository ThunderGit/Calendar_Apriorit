using Calendar_Apriorit.Infastructure;
using Calendar_Apriorit.ViewModel;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Calendar_Apriorit.Controllers
{
    public class AccountController : BaseController
    {
       

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(RegisterVM model)//Переделать под другую логин модель
        {
            await SetInitialDataAsync();
            if (ModelState.IsValid)
            {
                using (var DomainUser = WebContext.Factory.GetService<IUserDM>())
                {
                    //UserDTO userDto = new UserDTO { Email = model.Email, Password = model.Password };
                    ClaimsIdentity claim = await DomainUser.Authenticate(model);
                    if (claim == null)
                    {
                        ModelState.AddModelError("", "Неверный логин или пароль.");
                    }
                    else
                    {
                        AuthenticationManager.SignOut();
                        AuthenticationManager.SignIn(new AuthenticationProperties
                        {
                            IsPersistent = true
                        }, claim);
                        return RedirectToAction("Index", "Home");
                    }
                }
            }
            return View(model);
        }

        public ActionResult Logout()
        {
            AuthenticationManager.SignOut();
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterVM model)
        {
            await SetInitialDataAsync();
            if (ModelState.IsValid)
            {
                using (var DomainUser = WebContext.Factory.GetService<IUserDM>())
                {
                    
                    OperationDetails operationDetails = await DomainUser.Create(model);
                    if (operationDetails.Succedeed)
                        return View("SuccessRegister");
                    else
                        ModelState.AddModelError(operationDetails.Property, operationDetails.Message);

                }
                    
            }
            return View(model);
        }
        private async Task SetInitialDataAsync()
        {
            using (var DomainUser = WebContext.Factory.GetService<IUserDM>(WebContext.RootContext))
            {
                await DomainUser.SetInitialData(new RegisterVM
                {
                    Email = "somemail@mail.ru",
                    Name = "somemail@mail.ru",
                    Password = "ad46D_ewr3",
                    ConfirmPassword = "ad46D_ewr3"
                }, new List<string> { "user"});
            }
        }
    }

}