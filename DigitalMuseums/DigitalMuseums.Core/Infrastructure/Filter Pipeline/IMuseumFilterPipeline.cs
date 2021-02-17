using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using DigitalMuseums.Core.Domain.DTO;
using DigitalMuseums.Core.Domain.Models.Domain;

namespace DigitalMuseums.Core.Infrastructure.Filter_Pipeline
{
    public interface IMuseumFilterPipeline
    {
        Expression<Func<Museum, bool>> BuildQuery(FilterMuseumsDto filter);
        
        Func<IEnumerable<Museum>, IOrderedEnumerable<Museum>> BuildOrderByQuery(FilterMuseumsDto filter);
    }
}