using Calendar_Apriorit.Infastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;

namespace Calendar_Apriorit.Initialazer
{
    public class UnitySetup
    {
        static Lazy<IUnityContainer> container = new Lazy<IUnityContainer>(() =>
        {
            IUnityContainer container = new UnityContainer();
            RegisterTypes(container);
            return container;
        });

        private static void RegisterTypes(IUnityContainer container)
        {
            UnityDependencyRegister.RegisterDependencyTypes(container);
        }

        public static IServiceProviderFactory CreateServiceProviderFactory()
        {
            return new ServiceProviderFactory(container.Value);
        }
    }
}
