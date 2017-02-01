using System.Threading.Tasks;

namespace AnApiOfIceAndFire.Data
{
    public interface IRepositoryFactory
    {
        Task<IRepository> CreateAsync();
    }
}