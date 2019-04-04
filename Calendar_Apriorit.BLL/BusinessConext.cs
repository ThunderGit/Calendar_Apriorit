using Calendar_Apriorit.Infastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Calendar_Apriorit.BLL
{
    public class BusinessContext : IBusinessContext
    {
        #region Constructor
        public BusinessContext(IRootContext context)
        {
            RootContext = context;
        }
        #endregion


        public IServiceProviderFactory Factory => RootContext.Factory;

        public IRootContext RootContext { get; set; }

        public IMapperService Mapper => RootContext.Mapper;
    }
}
