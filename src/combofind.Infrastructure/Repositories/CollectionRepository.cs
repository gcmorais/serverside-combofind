using combofind.Domain.Entities;
using combofind.Domain.Interface;
using combofind.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace combofind.Infrastructure.Repositories
{
    public class CollectionRepository : BaseRepository<Collection>, ICollectionRepository
    {
        public CollectionRepository(AppDbContext context) : base(context) { }

        public async Task<List<Collection>> GetAllCollections()
        {
            return await _context.Collection.Include(c => c.Guns).ToListAsync();
        }


        public async Task<Collection> GetByBudget(string budget)
        {
            return await _context.Collection.FirstOrDefaultAsync(x => x.Budget == budget);
        }

        public async Task<Collection> GetByColor(string color)
        {
            return await _context.Collection.FirstOrDefaultAsync(x => x.Color == color);
        }

        public async Task<Collection> GetById(Guid id)
        {
            return await _context.Collection.FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
