using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace DigitalMuseums.Core.Infrastructure.Filter_Pipeline.Pipes.Exhibit
{
    public class ExhibitFilterTagsPipe : FilterPipe<Domain.Models.Domain.Exhibit>, IFilterPipe<Domain.Models.Domain.Exhibit>
    {
        private readonly List<string> _tags;

        public ExhibitFilterTagsPipe(List<string> tags)
        {
            _tags = tags;
        }
        
        public Expression<Func<Domain.Models.Domain.Exhibit, bool>> Apply(Expression<Func<Domain.Models.Domain.Exhibit, bool>> leftLeaf)
        {
            Expression<Func<Domain.Models.Domain.Exhibit, bool>> param = exhibit =>
                exhibit.Tags.Any(tag => _tags.Contains(tag));
                
            return CombineExpressions(leftLeaf, param);
        }
    }
}