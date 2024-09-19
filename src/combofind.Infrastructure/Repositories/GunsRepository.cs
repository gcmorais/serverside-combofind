using combofind.Domain.Entities;
using combofind.Domain.Interface;
using combofind.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace combofind.Infrastructure.Repositories
{
    public class GunsRepository : BaseRepository<Guns>, IGunsRepository
    {
        public GunsRepository(AppDbContext context) : base(context) { }

        public async Task<List<Guns>> GetAllGuns()
        {
            return await _context.Guns.ToListAsync();
        }

        public async Task<Guns> GetByColor(string color)
        {
            return await _context.Guns.FirstOrDefaultAsync(x => x.MainColor == color);
        }

        public async Task<Guns> GetById(Guid id)
        {
            return await _context.Guns.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Guns> GetByType(string type)
        {
            return await _context.Guns.FirstOrDefaultAsync(x => x.Type == type);
        }
    }
}
