using combofind.Domain.Entities;

namespace combofind.Domain.Interface
{
    public interface IGunsRepository : IBaseRepository<Guns>
    {
        Task<Guns> GetById(Guid id);
        Task<Guns> GetByColor(string color);
        Task<Guns> GetByType(string type);
        Task<List<Guns>> GetAllGuns();
    }
}
