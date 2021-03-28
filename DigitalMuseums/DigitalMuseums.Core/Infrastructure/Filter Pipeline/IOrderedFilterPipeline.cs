using System;
using System.Collections.Generic;
using System.Linq;

namespace DigitalMuseums.Core.Infrastructure.Filter_Pipeline
{
    public interface IOrderedFilterPipeline<TEntity, TFilter> : IFilterPipeline<TEntity, TFilter>
    {
        Func<IEnumerable<TEntity>, IOrderedEnumerable<TEntity>> BuildOrderByQuery(TFilter filter);
    }
}