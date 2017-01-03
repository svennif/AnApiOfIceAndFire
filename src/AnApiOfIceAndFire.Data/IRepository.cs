using System;
using System.Linq;
using System.Threading.Tasks;
using AnApiOfIceAndFire.Data.Entities;
using SimplePagedList;

namespace AnApiOfIceAndFire.Data
{
    public interface IRepository
    {
        Task<TEntity> GetAsync<TEntity>(int id) where TEntity : BaseEntity;

        Task<IPagedList<TEntity>> GetPageAsync<TEntity>(Func<IQueryable<TEntity>, IQueryable<TEntity>> queryFilter, int pageNumber, int pageSize) where TEntity : BaseEntity;
    }
}