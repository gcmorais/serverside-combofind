using combofind.Domain.Entities;

namespace combofind.Domain.Interface
{
    public interface IColletionRepository : IBaseRepository<Collection>
    {
        Task<Collection> GetById(Guid id);
        Task<Collection> GetByColor(string color);
        Task<Collection> GetByBudget(string budget);
        Task<List<Collection>> GetAllCollections();
    }
}
