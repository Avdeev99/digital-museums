using System;
using System.Linq.Expressions;

namespace DigitalMuseums.Core.Infrastructure.Filter_Pipeline.Pipes.Souvenir
{
    public class SouvenirFilterNamePipe : FilterPipe<Domain.Models.Domain.Souvenir>, IFilterPipe<Domain.Models.Domain.Souvenir>
    {
        private readonly string _name;

        public SouvenirFilterNamePipe(string name)
        {
            _name = name;
        }
        
        public Expression<Func<Domain.Models.Domain.Souvenir, bool>> Apply(Expression<Func<Domain.Models.Domain.Souvenir, bool>> leftLeaf)
        {
            Expression<Func<Domain.Models.Domain.Souvenir, bool>> param = x =>
                x.Name.ToLower().Contains(_name.ToLower());
                
            return CombineExpressions(leftLeaf, param);
        }
    }
}