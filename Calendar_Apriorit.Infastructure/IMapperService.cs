using System.Collections.Generic;

namespace Calendar_Apriorit.Infastructure
{
    public interface IMapperService
    {
        TOut MapTo<TOut, TIn>(TIn entity)
            where TOut : class
            where TIn : class;

        IEnumerable<TOut> MapTo<TOut, TIn>(IEnumerable<TIn> entity)
            where TOut : class
            where TIn : class;
    }
}