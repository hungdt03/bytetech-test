using System;
using ByteTech.Domain.Entities;

namespace ByteTech.Application.IRepositories;

public interface IRepository<T> where T : BaseEntity
{
    public Task<T> GetByIdAsync(string id);
    public Task<IEnumerable<T>> GetAllAsync();
    public Task<T> AddAsync(T entity);
    public Task UpdateAsync(T entity);
    public Task DeleteAsync(T entity);
}