using System;
using System.Linq.Expressions;

namespace DigitalMuseums.Core.Infrastructure.Filter_Pipeline.Pipes.Exhibition
{
    public class ExhibitionFilterNamePipe : FilterPipe<Domain.Models.Domain.Exhibition>, IFilterPipe<Domain.Models.Domain.Exhibition>
    {
        private readonly string _name;

        public ExhibitionFilterNamePipe(string name)
        {
            _name = name;
        }
        
        public Expression<Func<Domain.Models.Domain.Exhibition, bool>> Apply(Expression<Func<Domain.Models.Domain.Exhibition, bool>> leftLeaf)
        {
            Expression<Func<Domain.Models.Domain.Exhibition, bool>> param = exhibit =>
                exhibit.Name.ToLower().Contains(_name.ToLower());
                
            return CombineExpressions(leftLeaf, param);
        }
    }
}