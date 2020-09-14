using System;
using System.Linq.Expressions;

namespace BetterExtensions.Domain.Common
{
    public sealed class ProjectedQueryParams<TEntity, TProjected>
    {
        public int Skip { get; set; }
        public int Top { get; set; }
        public Expression<Func<TEntity, TProjected>> Projection { get; set; }
        public Expression<Func<TProjected, bool>> WhereProjectedPredicate { get; set; }
    }
}