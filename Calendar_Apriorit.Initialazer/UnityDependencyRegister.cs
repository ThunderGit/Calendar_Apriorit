using Calendar_Apriorit.DAL.Interfaces;
using Unity;
using Calendar_Apriorit.DAL.Repositories;
using System;
using Calendar_Apriorit.Infastructure;
using Calendar_Apriorit.BLL;
using Unity.Injection;

namespace Calendar_Apriorit.Initialazer
{
    public class UnityDependencyRegister
    {
        private static IUnityContainer _container;

        public static void RegisterDependencyTypes(IUnityContainer container)
        {
            _container = container;

            RegisterContexts();
            RegisterBusinessTypes();
            RegisterDataTypes();
        }

        private static void RegisterContexts()
        {
            _container.RegisterType<IWebContext, WebContext>();
            _container.RegisterType<IBusinessContext, BusinessContext>();
        }

        private static void RegisterBusinessTypes()
        {
            _container.RegisterType<IUserDM, UserDM>();
        }

        private static void RegisterDataTypes()
        {
            _container.RegisterType<IUnitOfWork, EFUnitOfWork>(/*new InjectionConstructor("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename='|DataDirectory|\\Calendar_Apriorit.mdf';Integrated Security=True")*/);
            
        }
    }
}