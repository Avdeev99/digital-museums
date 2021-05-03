using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace DigitalMuseums.Core.Infrastructure.Filter_Pipeline.Pipes.Exhibition
{
    public class ExhibitionFilterTagsPipe : FilterPipe<Domain.Models.Domain.Exhibition>, IFilterPipe<Domain.Models.Domain.Exhibition>
    {
        private readonly List<string> _tags;

        public ExhibitionFilterTagsPipe(List<string> tags)
        {
            _tags = tags;
        }
        
        public Expression<Func<Domain.Models.Domain.Exhibition, bool>> Apply(Expression<Func<Domain.Models.Domain.Exhibition, bool>> leftLeaf)
        {
            Expression<Func<Domain.Models.Domain.Exhibition, bool>> param = exhibit =>
                exhibit.Tags.Any(tag => _tags.Contains(tag));
                
            return CombineExpressions(leftLeaf, param);
        }
    }
}