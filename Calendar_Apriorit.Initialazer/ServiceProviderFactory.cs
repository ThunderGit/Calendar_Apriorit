using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;
using Calendar_Apriorit.Infastructure;

namespace Calendar_Apriorit.Initialazer
{
    public class ServiceProviderFactory : IServiceProviderFactory
    {
        #region Constructors
        public ServiceProviderFactory(IUnityContainer container)
        {
            _container = container;
        }
        #endregion

        IUnityContainer _container;

        public object GetService(Type serviceType, params object[] resolveParams)
        {
            return _container.Resolve(serviceType, new ServiceProviderParametrResolver(resolveParams));
        }

        public object GetService(Type serviceType)
        {
            return _container.Resolve(serviceType);
        }

        public ServiceType GetService<ServiceType>()
        {
            return (ServiceType)this.GetService(typeof(ServiceType));
        }

        public TService GetService<TService>(params object[] resolveParams)
        {
            return _container.Resolve<TService>(new ServiceProviderParametrResolver(resolveParams));
        }

        public TService GetService<TService>(string name, params object[] resolveParams)
        {
            return _container.Resolve<TService>(name, new ServiceProviderParametrResolver(resolveParams));
        }
    }
}
