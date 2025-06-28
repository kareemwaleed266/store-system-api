using Microsoft.EntityFrameworkCore;
using Store.Data.Context;
using Store.Data.Entities;
using Store.Repository.Interfaces;
using Store.Repository.Specification;

namespace Store.Repository.Repositories
{
    public class GenericRepository<TEntity, TKey> : IGenericRepository<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        private readonly StoreDbContext _Context;
        public GenericRepository(StoreDbContext context)
        {
            _Context = context;
        }

        public async Task AddAsync(TEntity entity)
         => await _Context.Set<TEntity>().AddAsync(entity);

        public void Delete(TEntity entity)
         => _Context.Set<TEntity>().Remove(entity);


        public async Task<IReadOnlyList<TEntity>> GetAllAsync()
         => await _Context.Set<TEntity>().ToListAsync();

        public async Task<IReadOnlyList<TEntity>> GetAllWithSpecificationAsync(ISpecification<TEntity> specs)
            => await ApplySpecification(specs).ToListAsync();

        public async Task<TEntity> GetByIdAsync(TKey? id)
        => await _Context.Set<TEntity>().FindAsync(id);

        public async Task<TEntity> GetWithSpecificationByIdAsync(ISpecification<TEntity> specs)
            => await ApplySpecification(specs).FirstOrDefaultAsync();

        public void Update(TEntity entity)
         => _Context.Set<TEntity>().Update(entity);
        public async Task<int> CountWithSpecificationAsync(ISpecification<TEntity> specs)
             => await ApplySpecification(specs).CountAsync();

        private IQueryable<TEntity> ApplySpecification(ISpecification<TEntity> specs)
            => SpecificationEvaluater<TEntity, TKey>.GetQuery(_Context.Set<TEntity>(), specs);
    }
}
