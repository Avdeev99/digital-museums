using System;
using System.Linq.Expressions;

namespace DigitalMuseums.Core.Infrastructure.Filter_Pipeline.Pipes.Exhibit
{
    public class ExhibitFilterMuseumIdPipe : FilterPipe<Domain.Models.Domain.Exhibit>, IFilterPipe<Domain.Models.Domain.Exhibit>
    {
        private readonly int _museumId;

        public ExhibitFilterMuseumIdPipe(int museumId)
        {
            _museumId = museumId;
        }

        public Expression<Func<Domain.Models.Domain.Exhibit, bool>> Apply(Expression<Func<Domain.Models.Domain.Exhibit, bool>> leftLeaf)
        {
            Expression<Func<Domain.Models.Domain.Exhibit, bool>> param = exhibit =>
                exhibit.MuseumId == _museumId;

            return CombineExpressions(leftLeaf, param);
        }
    }
}
