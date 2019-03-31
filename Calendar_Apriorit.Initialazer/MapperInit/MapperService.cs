using System;
using System.Collections.Generic;
using AutoMapper;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Calendar_Apriorit.Infastructure;

namespace Calendar_Apriorit.Initialazer.MapperInit
{
    public class MapperService : IMapperService
    {
        private IMapper _mapperInstance;

        #region Constructors
        public MapperService()
        {
            _mapperInstance = Mapper.Instance;
        }

        public MapperService(IMapper instance)
        {
            _mapperInstance = instance;
        }
        #endregion

        public TOut MapTo<TOut, TIn>(TIn entity) where TOut : class where TIn : class
        {
            return _mapperInstance.Map<TOut>(entity);
        }

        public IEnumerable<TOut> MapTo<TOut, TIn>(IEnumerable<TIn> entity) where TOut : class where TIn : class
        {
            return _mapperInstance.Map<IEnumerable<TOut>>(entity);
        }
    }
}
