using System;
using System.Collections.Generic;
using System.Linq;
using DigitalMuseums.Core.Domain.Enumerations;

namespace DigitalMuseums.Core.Infrastructure.Filter_Pipeline.Pipes.Souvenir
{
    public class SouvenirOrderByPipe : IOrderByPipe<Domain.Models.Domain.Souvenir>
    {
        private readonly SouvenirSortingMethod _sortingMethod;

        public SouvenirOrderByPipe(SouvenirSortingMethod sortingMethod)
        {
            _sortingMethod = sortingMethod;
        }
        
        public Func<IEnumerable<Domain.Models.Domain.Souvenir>, IOrderedEnumerable<Domain.Models.Domain.Souvenir>> Apply()
        {
            switch (_sortingMethod)
            {
                case SouvenirSortingMethod.OldFirst:
                    return x => x.OrderBy(x => x.Id);
                case SouvenirSortingMethod.PriceHighToLow:
                    return x => x.OrderByDescending(x => x.Price);
                case SouvenirSortingMethod.PriceLowToHigh:
                    return x => x.OrderBy(x => x.Price);
                case SouvenirSortingMethod.NewFirst:
                default: 
                    return museums => museums.OrderByDescending(museum => museum.Id);
            }
        }
    }
}