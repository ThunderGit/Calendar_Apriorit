using System;

namespace Calendar_Apriorit.Infastructure
{
    public interface IServiceProviderFactory : IServiceProvider
    {
        object GetService(Type serviceType, params object[] resolveParams);
        ServiceType GetService<ServiceType>();
        TService GetService<TService>(params object[] resolveParams);
        TService GetService<TService>(string name, params object[] resolveParams);
    }
}