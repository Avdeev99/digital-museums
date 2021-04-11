using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace DigitalMuseums.Core.Infrastructure.Filter_Pipeline.Pipes.Souvenir
{
    public class SouvenirFilterTagsPipe : FilterPipe<Domain.Models.Domain.Souvenir>, IFilterPipe<Domain.Models.Domain.Souvenir>
    {
        private readonly List<string> _tags;

        public SouvenirFilterTagsPipe(List<string> tags)
        {
            _tags = tags;
        }
        
        public Expression<Func<Domain.Models.Domain.Souvenir, bool>> Apply(Expression<Func<Domain.Models.Domain.Souvenir, bool>> leftLeaf)
        {
            Expression<Func<Domain.Models.Domain.Souvenir, bool>> param = exhibit =>
                exhibit.Tags.Any(tag => _tags.Contains(tag));
                
            return CombineExpressions(leftLeaf, param);
        }
    }
}