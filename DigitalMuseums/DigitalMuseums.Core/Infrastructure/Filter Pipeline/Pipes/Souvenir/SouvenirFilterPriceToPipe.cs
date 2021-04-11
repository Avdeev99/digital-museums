using System;
using System.Linq.Expressions;

namespace DigitalMuseums.Core.Infrastructure.Filter_Pipeline.Pipes.Souvenir
{
    public class SouvenirFilterPriceToPipe : FilterPipe<Domain.Models.Domain.Souvenir>, IFilterPipe<Domain.Models.Domain.Souvenir>
    {
        private readonly decimal _priceTo;

        public SouvenirFilterPriceToPipe(decimal priceTo)
        {
            _priceTo = priceTo;
        }
        
        public Expression<Func<Domain.Models.Domain.Souvenir, bool>> Apply(Expression<Func<Domain.Models.Domain.Souvenir, bool>> leftLeaf)
        {
            Expression<Func<Domain.Models.Domain.Souvenir, bool>> param = x => x.Price <= _priceTo;
                
            return CombineExpressions(leftLeaf, param);
        }
    }
}