using System;
using System.Linq.Expressions;

namespace DigitalMuseums.Core.Infrastructure.Filter_Pipeline.Pipes.Exhibit
{
    public class ExhibitFilterDateToPipe : FilterPipe<Domain.Models.Domain.Exhibit>, IFilterPipe<Domain.Models.Domain.Exhibit>
    {
        private readonly DateTime _dateTo;

        public ExhibitFilterDateToPipe(DateTime datTo)
        {
            _dateTo = datTo;
        }
        
        public Expression<Func<Domain.Models.Domain.Exhibit, bool>> Apply(Expression<Func<Domain.Models.Domain.Exhibit, bool>> leftLeaf)
        {
            Expression<Func<Domain.Models.Domain.Exhibit, bool>> param = exhibit => exhibit.Date < _dateTo;
                
            return CombineExpressions(leftLeaf, param);
        }
    }
}