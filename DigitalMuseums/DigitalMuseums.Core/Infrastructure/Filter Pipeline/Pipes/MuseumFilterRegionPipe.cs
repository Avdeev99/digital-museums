using System;
using System.Linq.Expressions;
using DigitalMuseums.Core.Domain.Models.Domain;

namespace DigitalMuseums.Core.Infrastructure.Filter_Pipeline.Pipes
{
    public class MuseumFilterRegionPipe : FilterPipe<Museum>, IFilterPipe<Museum>
    {
        private readonly int _regionId;

        public MuseumFilterRegionPipe(int regionId)
        {
            _regionId = regionId;
        }
        
        public Expression<Func<Museum, bool>> Apply(Expression<Func<Museum, bool>> leftLeaf)
        {
            Expression<Func<Museum, bool>> param = museum => museum.Location.City.RegionId == _regionId;

            return CombineExpressions(leftLeaf, param);
        }
    }
}