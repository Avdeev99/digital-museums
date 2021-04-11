using System;
using System.Linq.Expressions;

namespace DigitalMuseums.Core.Infrastructure.Filter_Pipeline.Pipes.Souvenir
{
    public class SouvenirFilterMuseumIdPipe : FilterPipe<Domain.Models.Domain.Souvenir>, IFilterPipe<Domain.Models.Domain.Souvenir>
    {
        private readonly int _museumId;

        public SouvenirFilterMuseumIdPipe(int museumId)
        {
            _museumId = museumId;
        }

        public Expression<Func<Domain.Models.Domain.Souvenir, bool>> Apply(Expression<Func<Domain.Models.Domain.Souvenir, bool>> leftLeaf)
        {
            Expression<Func<Domain.Models.Domain.Souvenir, bool>> param = x =>
                x.MuseumId == _museumId;

            return CombineExpressions(leftLeaf, param);
        }
    }
}