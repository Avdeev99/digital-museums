using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using DigitalMuseums.Core.Domain.Models.Domain;

namespace DigitalMuseums.Core.Infrastructure.Filter_Pipeline.Pipes
{
    public class MuseumFilterGenresPipe : FilterPipe<Museum>, IFilterPipe<Museum>
    {
        private readonly List<int> _genres;

        public MuseumFilterGenresPipe(List<int> genres)
        {
            _genres = genres;
        }

        public Expression<Func<Museum, bool>> Apply(Expression<Func<Museum, bool>> leftLeaf)
        {
            Expression<Func<Museum, bool>> param = museum => _genres.Any(genreId => genreId == museum.GenreId);

            return CombineExpressions(leftLeaf, param);
        }
    }
}