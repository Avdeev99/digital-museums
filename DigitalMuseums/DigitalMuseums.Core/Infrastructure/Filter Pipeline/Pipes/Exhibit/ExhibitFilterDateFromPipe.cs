using System;
using System.Linq.Expressions;

namespace DigitalMuseums.Core.Infrastructure.Filter_Pipeline.Pipes.Exhibit
{
    public class ExhibitFilterDateFromPipe : FilterPipe<Domain.Models.Domain.Exhibit>, IFilterPipe<Domain.Models.Domain.Exhibit>
    {
        private readonly DateTime _dateFrom;

        public ExhibitFilterDateFromPipe(DateTime dateFrom)
        {
            _dateFrom = dateFrom;
        }
        
        public Expression<Func<Domain.Models.Domain.Exhibit, bool>> Apply(Expression<Func<Domain.Models.Domain.Exhibit, bool>> leftLeaf)
        {
            Expression<Func<Domain.Models.Domain.Exhibit, bool>> param = exhibit => exhibit.Date > _dateFrom;
                
            return CombineExpressions(leftLeaf, param);
        }
    }
}