using System;
using System.Linq.Expressions;

namespace DigitalMuseums.Core.Infrastructure.Filter_Pipeline.Pipes.Exhibit
{
    public class ExhibitFilterNamePipe : FilterPipe<Domain.Models.Domain.Exhibit>, IFilterPipe<Domain.Models.Domain.Exhibit>
    {
        private readonly string _name;

        public ExhibitFilterNamePipe(string name)
        {
            _name = name;
        }
        
        public Expression<Func<Domain.Models.Domain.Exhibit, bool>> Apply(Expression<Func<Domain.Models.Domain.Exhibit, bool>> leftLeaf)
        {
            Expression<Func<Domain.Models.Domain.Exhibit, bool>> param = exhibit =>
                exhibit.Name.ToLower().Contains(_name.ToLower());
                
            return CombineExpressions(leftLeaf, param);
        }
    }
}