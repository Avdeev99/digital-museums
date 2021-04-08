using System;
using System.Linq.Expressions;

namespace DigitalMuseums.Core.Infrastructure.Filter_Pipeline.Pipes.Museum
{
    public class MuseumFilterNamePipe : FilterPipe<Domain.Models.Domain.Museum>, IFilterPipe<Domain.Models.Domain.Museum>
    {
        private readonly string _name;

        public MuseumFilterNamePipe(string name)
        {
            _name = name;
        }
        
        public Expression<Func<Domain.Models.Domain.Museum, bool>> Apply(Expression<Func<Domain.Models.Domain.Museum, bool>> leftLeaf)
        {
            Expression<Func<Domain.Models.Domain.Museum, bool>> param = museum =>
                museum.Name.ToLower().Contains(_name.ToLower());
                
            return CombineExpressions(leftLeaf, param);
        }
    }
}