using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using BetterExtensions.Domain.Base;

namespace BetterExtensions.Domain.Repository
{
    public interface IWriteRepository<TRoot> 
        where TRoot : AggregateRoot
    {
        Task<TRoot> GetByIdAsync(int id);
        Task<TRoot> GetFirstAsync(Expression<Func<TRoot, bool>> wherePredicate);
        
        void Save(TRoot chat);
        void SaveRange(List<TRoot> chats);
        
        void Delete(TRoot entity);
    }
}