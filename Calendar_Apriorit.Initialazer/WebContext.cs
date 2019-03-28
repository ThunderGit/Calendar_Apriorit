

using Calendar_Apriorit.Infastructure;

namespace Calendar_Apriorit.Initialazer
{
    public class WebContext : IWebContext
    {
        #region Constructor
        public WebContext(string connectionString)
        {
            RootContext = new RootContext(connectionString);
        }
        #endregion

        private IServiceProviderFactory _factory;

        public IRootContext RootContext { get; set; }

        public IServiceProviderFactory Factory => RootContext.Factory;
    }
}