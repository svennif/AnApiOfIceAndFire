using System.Linq;
using AnApiOfIceAndFire.Data.Entities;

namespace AnApiOfIceAndFire.Domain
{
    public interface IEntityFilter<TEntity> where TEntity : BaseEntity
    {
        IQueryable<TEntity> Apply(IQueryable<TEntity> querySet);
    }
}