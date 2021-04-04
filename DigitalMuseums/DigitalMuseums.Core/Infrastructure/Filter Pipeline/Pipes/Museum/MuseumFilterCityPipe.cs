using System;
using System.Linq.Expressions;

namespace DigitalMuseums.Core.Infrastructure.Filter_Pipeline.Pipes.Museum
{
    public class MuseumFilterCityPipe : FilterPipe<Domain.Models.Domain.Museum>, IFilterPipe<Domain.Models.Domain.Museum>
    {
        private readonly int _cityId;

        public MuseumFilterCityPipe(int cityId)
        {
            _cityId = cityId;
        }
        
        public Expression<Func<Domain.Models.Domain.Museum, bool>> Apply(Expression<Func<Domain.Models.Domain.Museum, bool>> leftLeaf)
        {
            Expression<Func<Domain.Models.Domain.Museum, bool>> param = museum => museum.Location.CityId == _cityId;

            return CombineExpressions(leftLeaf, param);
        }
    }
}