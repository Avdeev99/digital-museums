using System;
using System.Linq.Expressions;

namespace DigitalMuseums.Core.Infrastructure.Filter_Pipeline.Pipes.Exhibition
{
    public class ExhibitionFilterMuseumIdPipe : FilterPipe<Domain.Models.Domain.Exhibition>, IFilterPipe<Domain.Models.Domain.Exhibition>
    {
        private readonly int _museumId;

        public ExhibitionFilterMuseumIdPipe(int museumId)
        {
            _museumId = museumId;
        }

        public Expression<Func<Domain.Models.Domain.Exhibition, bool>> Apply(Expression<Func<Domain.Models.Domain.Exhibition, bool>> leftLeaf)
        {
            Expression<Func<Domain.Models.Domain.Exhibition, bool>> param = exhibit =>
                exhibit.MuseumId == _museumId;

            return CombineExpressions(leftLeaf, param);
        }
    }
}
