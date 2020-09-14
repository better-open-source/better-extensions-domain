using System;
using System.Linq.Expressions;

namespace BetterExtensions.Domain.Common
{
    public sealed class QueryParams<TEntity>
    {
        public int Skip { get; set; }
        public int Top { get; set; }
        public Expression<Func<TEntity, bool>> WherePredicate { get; set; }
    }
}