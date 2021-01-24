using System;
using System.Collections.Generic;
using System.Linq;

namespace DigitalMuseums.Core.Infrastructure.Filter_Pipeline.Pipes
{
    public interface IOrderByPipe<T> where T : class
    {
        Func<IEnumerable<T>, IOrderedEnumerable<T>> Apply();
    }
}