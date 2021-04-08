using System;
using System.Linq.Expressions;

namespace DigitalMuseums.Core.Infrastructure.Filter_Pipeline.Pipes.Museum
{
    public class MuseumFilterRegionPipe : FilterPipe<Domain.Models.Domain.Museum>, IFilterPipe<Domain.Models.Domain.Museum>
    {
        private readonly int _regionId;

        public MuseumFilterRegionPipe(int regionId)
        {
            _regionId = regionId;
        }
        
        public Expression<Func<Domain.Models.Domain.Museum, bool>> Apply(Expression<Func<Domain.Models.Domain.Museum, bool>> leftLeaf)
        {
            Expression<Func<Domain.Models.Domain.Museum, bool>> param = museum => museum.Location.City.RegionId == _regionId;

            return CombineExpressions(leftLeaf, param);
        }
    }
}