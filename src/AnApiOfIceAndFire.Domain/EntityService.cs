using System;
using System.Linq;
using System.Threading.Tasks;
using AnApiOfIceAndFire.Data;
using AnApiOfIceAndFire.Data.Entities;
using SimplePagedList;

namespace AnApiOfIceAndFire.Domain
{
    public sealed class EntityService<TEntity> where TEntity : BaseEntity
    {
        private readonly IRepository _entityRepository;

        public EntityService(IRepository entityRepository)
        {
            if (entityRepository == null) throw new ArgumentNullException(nameof(entityRepository));
            _entityRepository = entityRepository;
        }

        public async Task<TEntity> GetAsync(int id)
        {
            return await _entityRepository.GetAsync<TEntity>(id);
        }

        public async Task<IPagedList<TEntity>> GetPageAsync(IEntityFilter<TEntity> filter, int pageNumber, int pageSize)
        {
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderedPredicate = entities =>
            {
                var filteredEntities = filter.Apply(entities);
                return filteredEntities.OrderBy(fe => fe.Id);
            };

            var pagedEntities = await _entityRepository.GetPageAsync(orderedPredicate, pageNumber, pageSize);
            if (pagedEntities == null)
            {
                return PagedList.Empty<TEntity>();
            }

            return pagedEntities;
        }
    }
}