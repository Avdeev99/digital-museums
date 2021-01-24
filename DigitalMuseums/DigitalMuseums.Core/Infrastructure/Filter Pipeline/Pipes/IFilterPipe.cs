using System;
using System.Linq.Expressions;

namespace DigitalMuseums.Core.Infrastructure.Filter_Pipeline.Pipes
{
    public interface IFilterPipe<T> where T : class
    {
        Expression<Func<T, bool>> Apply(Expression<Func<T, bool>> leftLeaf);
    }
}