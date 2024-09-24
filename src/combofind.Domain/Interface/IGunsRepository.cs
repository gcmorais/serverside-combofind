using combofind.Domain.Entities;

namespace combofind.Domain.Interface
{
    public interface IGunsRepository : IBaseRepository<GunEntity>
    {
        Task<GunEntity> GetById(Guid id);
        Task<GunEntity> GetByColor(string color);
        Task<GunEntity> GetByType(string type);
        Task<List<GunEntity>> GetAllGuns();
    }
}
