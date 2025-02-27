﻿using combofind.Domain.Entities;

namespace combofind.Domain.Interface
{
    public interface IBaseRepository<T> where T : BaseEntity
    {
        void Create(T entity);
        void Update(T entity);
        void Delete(T entity);
        Task<T> Get(Guid id);
        Task<List<T>> GetAll();
    }
}
