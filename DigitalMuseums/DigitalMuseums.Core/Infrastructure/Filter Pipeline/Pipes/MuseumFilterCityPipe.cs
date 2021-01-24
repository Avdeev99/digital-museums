using System;
using System.Linq.Expressions;
using DigitalMuseums.Core.Domain.Models.Domain;

namespace DigitalMuseums.Core.Infrastructure.Filter_Pipeline.Pipes
{
    public class MuseumFilterCityPipe : FilterPipe<Museum>, IFilterPipe<Museum>
    {
        private readonly int _cityId;

        public MuseumFilterCityPipe(int cityId)
        {
            _cityId = cityId;
        }
        
        public Expression<Func<Museum, bool>> Apply(Expression<Func<Museum, bool>> leftLeaf)
        {
            Expression<Func<Museum, bool>> param = museum => museum.Location.CityId == _cityId;

            return CombineExpressions(leftLeaf, param);
        }
    }
}