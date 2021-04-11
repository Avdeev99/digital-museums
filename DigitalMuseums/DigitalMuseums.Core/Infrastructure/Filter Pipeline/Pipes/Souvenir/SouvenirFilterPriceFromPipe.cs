using System;
using System.Linq.Expressions;

namespace DigitalMuseums.Core.Infrastructure.Filter_Pipeline.Pipes.Souvenir
{
    public class SouvenirFilterPriceFromPipe : FilterPipe<Domain.Models.Domain.Souvenir>, IFilterPipe<Domain.Models.Domain.Souvenir>
    {
        private readonly decimal _priceFrom;

        public SouvenirFilterPriceFromPipe(decimal priceFrom)
        {
            _priceFrom = priceFrom;
        }
        
        public Expression<Func<Domain.Models.Domain.Souvenir, bool>> Apply(Expression<Func<Domain.Models.Domain.Souvenir, bool>> leftLeaf)
        {
            Expression<Func<Domain.Models.Domain.Souvenir, bool>> param = x => x.Price >= _priceFrom;
                
            return CombineExpressions(leftLeaf, param);
        }
    }
}