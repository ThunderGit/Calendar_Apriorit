

namespace Calendar_Apriorit.Infastructure
{
    public interface IBusinessContext
    {
        IRootContext RootContext { get; set; }
        IServiceProviderFactory Factory { get; }
        IMapperService Mapper { get; }
    }
}