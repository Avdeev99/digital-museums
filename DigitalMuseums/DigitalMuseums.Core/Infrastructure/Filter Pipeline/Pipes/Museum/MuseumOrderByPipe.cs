using System;
using System.Collections.Generic;
using System.Linq;
using DigitalMuseums.Core.Domain.Enumerations;

namespace DigitalMuseums.Core.Infrastructure.Filter_Pipeline.Pipes.Museum
{
    public class MuseumOrderByPipe : IOrderByPipe<Domain.Models.Domain.Museum>
    {
        private readonly SortingMethod _sortingMethod;

        public MuseumOrderByPipe(SortingMethod sortingMethod)
        {
            _sortingMethod = sortingMethod;
        }
        
        public Func<IEnumerable<Domain.Models.Domain.Museum>, IOrderedEnumerable<Domain.Models.Domain.Museum>> Apply()
        {
            switch (_sortingMethod)
            {
                case SortingMethod.MostPopular: return museums => museums.OrderByDescending(museum => museum.VisitedCount);
                default: return museums => museums.OrderByDescending(museum => museum.Id);
            }
        }
    }
}