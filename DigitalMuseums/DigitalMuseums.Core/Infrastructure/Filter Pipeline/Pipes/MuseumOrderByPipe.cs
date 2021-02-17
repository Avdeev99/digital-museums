using System;
using System.Collections.Generic;
using System.Linq;
using DigitalMuseums.Core.Domain.Enumerations;
using DigitalMuseums.Core.Domain.Models.Domain;

namespace DigitalMuseums.Core.Infrastructure.Filter_Pipeline.Pipes
{
    public class MuseumOrderByPipe : IOrderByPipe<Museum>
    {
        private readonly SortingMethod _sortingMethod;

        public MuseumOrderByPipe(SortingMethod sortingMethod)
        {
            _sortingMethod = sortingMethod;
        }
        
        public Func<IEnumerable<Museum>, IOrderedEnumerable<Museum>> Apply()
        {
            switch (_sortingMethod)
            {
                case SortingMethod.MostPopular: return museums => museums.OrderByDescending(museum => museum.VisitedCount);
                default: return museums => museums.OrderByDescending(museum => museum.Id);
            }
        }
    }
}