using System;
using System.Linq.Expressions;
using DigitalMuseums.Core.Domain.Models.Domain;

namespace DigitalMuseums.Core.Infrastructure.Filter_Pipeline.Pipes
{
    public class MuseumFilterNamePipe : FilterPipe<Museum>, IFilterPipe<Museum>
    {
        private readonly string _name;

        public MuseumFilterNamePipe(string name)
        {
            _name = name;
        }
        
        public Expression<Func<Museum, bool>> Apply(Expression<Func<Museum, bool>> leftLeaf)
        {
            Expression<Func<Museum, bool>> param = museum =>
                museum.Name.ToLower().Contains(_name.ToLower());
                
            return CombineExpressions(leftLeaf, param);
        }
    }
}