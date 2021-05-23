using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using DigitalMuseums.Core.Domain.DTO.Souvenir;
using DigitalMuseums.Core.Domain.Models.Domain;
using DigitalMuseums.Core.Infrastructure.Filter_Pipeline.Pipes.Souvenir;

namespace DigitalMuseums.Core.Infrastructure.Filter_Pipeline
{
    public class SouvenirFilterPipeline : IOrderedFilterPipeline<Souvenir, FilterSouvenirsDto>
    {
        public Expression<Func<Souvenir, bool>> BuildQuery(FilterSouvenirsDto filter)
        {
            Expression<Func<Souvenir, bool>> resultQuery = x => !x.IsDeleted;
            
            if (filter.MuseumId.HasValue)
            {
                var pipe = new SouvenirFilterMuseumIdPipe(filter.MuseumId.Value);
                resultQuery = pipe.Apply(resultQuery);
            }
            
            if (!string.IsNullOrEmpty(filter.Name))
            {
                var pipe = new SouvenirFilterNamePipe(filter.Name);
                resultQuery = pipe.Apply(resultQuery);
            }

            if (filter.PriceFrom.HasValue)
            {
                var pipe = new SouvenirFilterPriceFromPipe(filter.PriceFrom.Value);
                resultQuery = pipe.Apply(resultQuery);
            }
            
            if (filter.PriceTo.HasValue)
            {
                var pipe = new SouvenirFilterPriceToPipe(filter.PriceTo.Value);
                resultQuery = pipe.Apply(resultQuery);
            }
            
            filter.Tags = filter.Tags.Where(x => !string.IsNullOrWhiteSpace(x)).ToList();
            if (filter.Tags != null && filter.Tags.Any())
            {
                var pipe = new SouvenirFilterTagsPipe(filter.Tags);
                resultQuery = pipe.Apply(resultQuery);
            }

            return resultQuery;
        }

        public Func<IEnumerable<Souvenir>, IOrderedEnumerable<Souvenir>> BuildOrderByQuery(FilterSouvenirsDto filter)
        {
            var pipe = new SouvenirOrderByPipe(filter.SortingMethod);

            return pipe.Apply();
        }
    }
}