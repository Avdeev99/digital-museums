using System;
using System.Linq;
using System.Linq.Expressions;
using DigitalMuseums.Core.Domain.DTO.Exhibit;
using DigitalMuseums.Core.Domain.Models.Domain;
using DigitalMuseums.Core.Infrastructure.Filter_Pipeline.Pipes.Exhibit;

namespace DigitalMuseums.Core.Infrastructure.Filter_Pipeline
{
    public class ExhibitFilterPipeline : IFilterPipeline<Exhibit, FilterExhibitsDto>
    {
        public Expression<Func<Exhibit, bool>> BuildQuery(FilterExhibitsDto filter)
        {
            Expression<Func<Exhibit, bool>> resultQuery = exhibit => !exhibit.IsDeleted;
            
            if (!string.IsNullOrEmpty(filter.Name))
            {
                var exhibitFilterNamePipe = new ExhibitFilterNamePipe(filter.Name);
                resultQuery = exhibitFilterNamePipe.Apply(resultQuery);
            }
            
            if (!string.IsNullOrEmpty(filter.Author))
            {
                var exhibitFilterAuthorPipe = new ExhibitFilterAuthorPipe(filter.Author);
                resultQuery = exhibitFilterAuthorPipe.Apply(resultQuery);
            }

            if (filter.DateFrom.HasValue)
            {
                var exhibitFilterDateFromPipe = new ExhibitFilterDateFromPipe(filter.DateFrom.Value);
                resultQuery = exhibitFilterDateFromPipe.Apply(resultQuery);
            }
            
            if (filter.DateTo.HasValue)
            {
                var exhibitFilterDateToPipe = new ExhibitFilterDateToPipe(filter.DateTo.Value);
                resultQuery = exhibitFilterDateToPipe.Apply(resultQuery);
            }

            if (filter.Tags != null && filter.Tags.Any())
            {
                var exhibitFilterTagsPipe = new ExhibitFilterTagsPipe(filter.Tags);
                resultQuery = exhibitFilterTagsPipe.Apply(resultQuery);
            }

            return resultQuery;
        }
    }
}