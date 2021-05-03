using System;
using System.Linq;
using System.Linq.Expressions;
using DigitalMuseums.Core.Domain.DTO.Exhibition;
using DigitalMuseums.Core.Domain.Models.Domain;
using DigitalMuseums.Core.Infrastructure.Filter_Pipeline.Pipes.Exhibition;

namespace DigitalMuseums.Core.Infrastructure.Filter_Pipeline
{
    public class ExhibitionFilterPipeline : IFilterPipeline<Exhibition, FilterExhibitionsDto>
    {
        public Expression<Func<Exhibition, bool>> BuildQuery(FilterExhibitionsDto filter)
        {
            Expression<Func<Exhibition, bool>> resultQuery = exhibition => !exhibition.IsDeleted;

            if (filter.MuseumId.HasValue)
            {
                var exhibitionFilterMuseumIdPipe = new ExhibitionFilterMuseumIdPipe(filter.MuseumId.Value);
                resultQuery = exhibitionFilterMuseumIdPipe.Apply(resultQuery);
            }

            if (!string.IsNullOrEmpty(filter.Name))
            {
                var exhibitionFilterNamePipe = new ExhibitionFilterNamePipe(filter.Name);
                resultQuery = exhibitionFilterNamePipe.Apply(resultQuery);
            }
            
            if (filter.Tags != null && filter.Tags.Any())
            {
                var exhibitFilterTagsPipe = new ExhibitionFilterTagsPipe(filter.Tags);
                resultQuery = exhibitFilterTagsPipe.Apply(resultQuery);
            }

            return resultQuery;
        }
    }
}