using System;
using System.Linq.Expressions;
using DigitalMuseums.Core.Domain.Models.Domain;

namespace DigitalMuseums.Core.Infrastructure.Filter_Pipeline.Pipes
{
    public class MuseumFilterCountryPipe : FilterPipe<Museum>, IFilterPipe<Museum>
    {
        private readonly int _countryId;

        public MuseumFilterCountryPipe(int countryId)
        {
            _countryId = countryId;
        }
        
        public Expression<Func<Museum, bool>> Apply(Expression<Func<Museum, bool>> leftLeaf)
        {
            Expression<Func<Museum, bool>> param = museum => museum.Location.City.Region.CountryId == _countryId;

            return CombineExpressions(leftLeaf, param);
        }
    }
}