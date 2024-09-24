using combofind.Domain.Entities;
using combofind.Domain.Interface;
using combofind.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace combofind.Infrastructure.Repositories
{
    public class GunsRepository : BaseRepository<GunEntity>, IGunsRepository
    {
        public GunsRepository(AppDbContext context) : base(context) { }

        public async Task<List<GunEntity>> GetAllGuns()
        {
            return await _context.Guns.ToListAsync();
        }

        public async Task<GunEntity> GetByColor(string color)
        {
            return await _context.Guns.FirstOrDefaultAsync(x => x.MainColor == color);
        }

        public async Task<GunEntity> GetById(Guid id)
        {
            return await _context.Guns.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<GunEntity> GetByType(string type)
        {
            return await _context.Guns.FirstOrDefaultAsync(x => x.Type == type);
        }
    }
}
