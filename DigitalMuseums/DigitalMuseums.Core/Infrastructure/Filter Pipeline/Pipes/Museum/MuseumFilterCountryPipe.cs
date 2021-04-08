using System;
using System.Linq.Expressions;

namespace DigitalMuseums.Core.Infrastructure.Filter_Pipeline.Pipes.Museum
{
    public class MuseumFilterCountryPipe : FilterPipe<Domain.Models.Domain.Museum>, IFilterPipe<Domain.Models.Domain.Museum>
    {
        private readonly int _countryId;

        public MuseumFilterCountryPipe(int countryId)
        {
            _countryId = countryId;
        }
        
        public Expression<Func<Domain.Models.Domain.Museum, bool>> Apply(Expression<Func<Domain.Models.Domain.Museum, bool>> leftLeaf)
        {
            Expression<Func<Domain.Models.Domain.Museum, bool>> param = museum => museum.Location.City.Region.CountryId == _countryId;

            return CombineExpressions(leftLeaf, param);
        }
    }
}