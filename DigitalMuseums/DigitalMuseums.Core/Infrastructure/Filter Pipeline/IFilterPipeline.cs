using System;
using System.Linq.Expressions;

namespace DigitalMuseums.Core.Infrastructure.Filter_Pipeline
{
    public interface IFilterPipeline<TEntity, TFilter>
    {
        Expression<Func<TEntity, bool>> BuildQuery(TFilter filter);
    }
}