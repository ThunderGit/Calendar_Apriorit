using Calendar_Apriorit.Infastructure;
using Calendar_Apriorit.Initialazer.MapperInit;

namespace Calendar_Apriorit.Initialazer
{
    public class RootContext : IRootContext
    {
        #region Constructors
        public RootContext(string connectionString)
        {
            _connectionString = connectionString;
            _factory = UnitySetup.CreateServiceProviderFactory();
            _mapper = new MapperService();
        }
        #endregion

        private string _connectionString;
        private IServiceProviderFactory _factory;
        private IMapperService _mapper;

        public string ConnectionString => _connectionString;

        public IServiceProviderFactory Factory => _factory;
        public IMapperService Mapper => _mapper;
    }
}