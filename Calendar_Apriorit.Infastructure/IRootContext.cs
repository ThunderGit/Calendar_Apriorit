
namespace Calendar_Apriorit.Infastructure
{
    public interface IRootContext
    {
        string ConnectionString { get; }
        IServiceProviderFactory Factory { get; }
        IMapperService Mapper { get; }
    }
}