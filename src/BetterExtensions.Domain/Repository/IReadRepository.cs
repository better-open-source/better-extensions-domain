using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using BetterExtensions.Domain.Base;
using BetterExtensions.Domain.Common;

namespace BetterExtensions.Domain.Repository
{
    public interface IReadRepository<TView> 
        where TView : View
    {
        Task<List<TView>> GetAllAsync(
            QueryParams<TView> queryParams,
            CancellationToken cancellationToken = default);

        Task<List<TResult>> GetAllAsync<TResult>(
            ProjectedQueryParams<TView, TResult> projectedQueryParams,
            CancellationToken cancellationToken = default);

        Task<int> CountAsync(
            Expression<Func<TView, bool>> wherePredicate,
            CancellationToken cancellationToken = default);

        Task<int> CountAsync<TResult>(
            Expression<Func<TView, TResult>> projection, 
            Expression<Func<TResult, bool>> whereProjectedPredicate,
            CancellationToken cancellationToken = default);
    }
}