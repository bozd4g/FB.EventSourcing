using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using FB.EventSourcing.Domain.SeedWork;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FB.EventSourcing.Persistence.EntityFrameworkCore
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : Entity
    {
	    private readonly IMediator _mediator;
        private readonly DbSet<TEntity> _dbSet;
        private readonly ApplicationDbContext _context;
        
        public Repository(IMediator mediator, ApplicationDbContext context)
        {
            _context = context;
            _mediator = mediator;
            _dbSet = context.Set<TEntity>();
        }

        public IQueryable<TEntity> Query()
		{
			return _dbSet.AsQueryable();
		}

		public ICollection<TEntity> GetAll()
		{
			return _dbSet.ToList();
		}

		public async Task<ICollection<TEntity>> GetAllAsync()
		{
			return await _dbSet.ToListAsync();
		}

		public TEntity GetById(int id)
		{
			return _dbSet.Find(id);
		}

		public async Task<TEntity> GetByIdAsync(int id)
		{
			return await _dbSet.FindAsync(id);
		}

		public TEntity GetByUniqueId(Guid id)
		{
			return _dbSet.Find(id);
		}

		public async Task<TEntity> GetByUniqueIdAsync(Guid id)
		{
			return await _dbSet.FindAsync(id);
		}

		public TEntity Find(Expression<Func<TEntity, bool>> match)
		{
			return _dbSet.SingleOrDefault(match);
		}

		public async Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> match)
		{
			return await _dbSet.SingleOrDefaultAsync(match);
		}

		public ICollection<TEntity> FindAll(Expression<Func<TEntity, bool>> match)
		{
			return _dbSet.Where(match).ToList();
		}

		public async Task<ICollection<TEntity>> FindAllAsync(Expression<Func<TEntity, bool>> match)
		{
			return await _dbSet.Where(match).ToListAsync();
		}

		public TEntity Add(TEntity entity)
		{
			_dbSet.Add(entity);
			return entity;
		}

		public async Task<TEntity> AddAsync(TEntity entity)
		{
			await _dbSet.AddAsync(entity);
			await _context.SaveChangesAsync();

			return entity;
		}

		public TEntity Update(TEntity updated)
		{
			if (updated == null)
				return null;

			_dbSet.Attach(updated);
			_context.Entry(updated).State = EntityState.Modified;

			return updated;
		}

		public async Task<TEntity> UpdateAsync(TEntity updated)
		{
			if (updated == null)
				return null;

			_dbSet.Attach(updated);
			_context.Entry(updated).State = EntityState.Modified;
			await _context.SaveChangesAsync();

			return updated;
		}

		public void Delete(TEntity t)
		{
			_dbSet.Remove(t);
		}

		public async Task<TEntity> DeleteAsync(TEntity t)
		{
			_dbSet.Remove(t);
			await _context.SaveChangesAsync();

			return t;
		}

		public int Count()
		{
			return _dbSet.Count();
		}

		public async Task<int> CountAsync()
		{
			return await _dbSet.CountAsync();
		}

		public IEnumerable<TEntity> Filter(Expression<Func<TEntity, bool>> filter = null,
			Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
			string includeProperties = "",
			int? page = null,
			int? pageSize = null)
		{
			IQueryable<TEntity> query = _dbSet;
			if (filter != null)
				query = query.Where(filter);

			if (orderBy != null)
				query = orderBy(query);

			if (includeProperties != null)
				foreach (var includeProperty in includeProperties.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
				{
					query = query.Include(includeProperty);
				}

			if (page != null && pageSize != null)
				query = query.Skip((page.Value - 1) * pageSize.Value).Take(pageSize.Value);

			return query.ToList();
		}

		public IQueryable<TEntity> FindBy(Expression<Func<TEntity, bool>> predicate)
		{
			return _dbSet.Where(predicate);
		}

		public bool Exist(Expression<Func<TEntity, bool>> predicate)
		{
			var exist = _dbSet.Where(predicate);
			return exist.Any();
		}

		public async Task<int> SaveChanges()
		{
			// Dispatch Domain Events collection.
			// Choices:
			// A) Right BEFORE committing data (EF SaveChanges) into the DB will make a single transaction including
			// side effects from the domain event handlers which are using the same DbContext with "InstancePerLifetimeScope" or "scoped" lifetime
			// B) Right AFTER committing data (EF SaveChanges) into the DB will make multiple transactions.
			// You will need to handle eventual consistency and compensatory actions in case of failures in any of the Handlers.

			var result = await _context.SaveChangesAsync();
			await _mediator.DispatchDomainEventsAsync(_context);

			return result;
		}
    }
}