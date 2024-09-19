using combofind.Domain.Entities;
using combofind.Domain.Interface;
using combofind.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace combofind.Infrastructure.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
    {
        protected readonly AppDbContext _context;

        public BaseRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Create(T entity)
        {
            _context.Add(entity);
        }

        public void Update(T entity)
        {
            entity.UpdateDate();
            _context.Update(entity);
        }

        public void Delete(T entity)
        {
            entity.MarkAsDeleted();
            _context.Remove(entity);
        }

        public async Task<List<T>> GetAll()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<T> Get(Guid id)
        {
            return await _context.Set<T>().FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
