using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace DigitalMuseums.Core.Infrastructure.Filter_Pipeline.Pipes.Museum
{
    public class MuseumFilterGenresPipe : FilterPipe<Domain.Models.Domain.Museum>, IFilterPipe<Domain.Models.Domain.Museum>
    {
        private readonly List<int> _genres;

        public MuseumFilterGenresPipe(List<int> genres)
        {
            _genres = genres;
        }

        public Expression<Func<Domain.Models.Domain.Museum, bool>> Apply(Expression<Func<Domain.Models.Domain.Museum, bool>> leftLeaf)
        {
            Expression<Func<Domain.Models.Domain.Museum, bool>> param = museum => _genres.Any(genreId => genreId == museum.GenreId);

            return CombineExpressions(leftLeaf, param);
        }
    }
}