using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using DigitalMuseums.Core.Domain.DTO;
using DigitalMuseums.Core.Domain.Models.Domain;
using DigitalMuseums.Core.Infrastructure.Filter_Pipeline.Pipes;

namespace DigitalMuseums.Core.Infrastructure.Filter_Pipeline
{
    public class MuseumFilterPipeline : IMuseumFilterPipeline
    {
        public Expression<Func<Museum, bool>> BuildQuery(FilterMuseumsDto filter)
        {
            Expression<Func<Museum, bool>> resultQuery = museum => !museum.IsDeleted;
            
            if (!string.IsNullOrEmpty(filter.Name))
            {
                var museumFilterNamePipe = new MuseumFilterNamePipe(filter.Name);
                resultQuery = museumFilterNamePipe.Apply(resultQuery);
            }

            if (filter.Genres != null && filter.Genres.Any())
            {
                var museumFilterGenresPipe = new MuseumFilterGenresPipe(filter.Genres);
                resultQuery = museumFilterGenresPipe.Apply(resultQuery);
            }

            if (filter.CountryId.HasValue)
            {
                var museumFilterCountryPipe = new MuseumFilterCountryPipe(filter.CountryId.Value);
                resultQuery = museumFilterCountryPipe.Apply(resultQuery);
                
                if (filter.RegionId.HasValue)
                {
                    var museumFilterRegionPipe = new MuseumFilterRegionPipe(filter.RegionId.Value);
                    resultQuery = museumFilterRegionPipe.Apply(resultQuery);
                    
                    if (filter.CityId.HasValue)
                    {
                        var museumFilterCityPipe = new MuseumFilterCityPipe(filter.CityId.Value);
                        resultQuery = museumFilterCityPipe.Apply(resultQuery);
                    }
                }
            }

            return resultQuery;
        }

        public Func<IEnumerable<Museum>, IOrderedEnumerable<Museum>> BuildOrderByQuery(FilterMuseumsDto filter)
        {
            var pipe = new MuseumOrderByPipe(filter.SortingMethod);

            return pipe.Apply();
        }
    }
}