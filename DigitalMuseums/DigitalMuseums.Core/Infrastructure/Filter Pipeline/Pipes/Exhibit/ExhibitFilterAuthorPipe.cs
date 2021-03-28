using System;
using System.Linq.Expressions;

namespace DigitalMuseums.Core.Infrastructure.Filter_Pipeline.Pipes.Exhibit
{
    public class ExhibitFilterAuthorPipe : FilterPipe<Domain.Models.Domain.Exhibit>, IFilterPipe<Domain.Models.Domain.Exhibit> 
    {
        private readonly string _author;

        public ExhibitFilterAuthorPipe(string author)
        {
            _author = author;
        }
        
        public Expression<Func<Domain.Models.Domain.Exhibit, bool>> Apply(Expression<Func<Domain.Models.Domain.Exhibit, bool>> leftLeaf)
        {
            Expression<Func<Domain.Models.Domain.Exhibit, bool>> param = exhibit =>
                exhibit.Author.ToLower().Contains(_author.ToLower());
                
            return CombineExpressions(leftLeaf, param);
        }
    }
}