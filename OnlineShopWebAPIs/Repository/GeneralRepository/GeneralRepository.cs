using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OnlineShopWebAPIs.Interfaces.IGeneralRepository;
using OnlineShopWebAPIs.Models.DBContext;

namespace OnlineShopWebAPIs.Repository.GeneralRepository
{
    public class GeneralRepository<T> : IGeneralRepository<T> where T:class
    {

        private readonly DbContext _context;
        private readonly DbSet<T> _dbSet;

        public GeneralRepository(DbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }


        public T GetById(int id)
        {
            return _dbSet.Find(id);
        }
        public IEnumerable<T> GetAll(List<string> includes = null, Func<IQueryable<T>,IOrderedQueryable<T>> orderBy = null)
        {
            IQueryable<T> query = _dbSet;

            if (includes != null)
            {
                foreach (var include in includes)
                {
                    query.Include(include);
                }
            }


            if(orderBy != null)
            {
                query = orderBy(query);
            }

            return query.AsNoTracking().ToList();

        }
        public IEnumerable<T> Find(System.Linq.Expressions.Expression<Func<T, bool>> predicate, List<string> includes = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null)
        {
            IQueryable<T> query = _dbSet;

            if (predicate != null)
            {
                query.Where(predicate);
            }


            if (includes != null)
            {
                foreach (var include in includes)
                {
                    query.Include(include);
                }
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }

            return query.AsNoTracking();

        }


        public void Insert(T entity)
        {
            _dbSet.Add(entity);
        }
        public void InsertRange(List<T> entities)
        {
            _dbSet.AddRange(entities);
        }

        public void Delete(T entity)
        {
            _dbSet.Remove(entity);
        }
        public void DeleteRange(List<T> entities)
        {
            _dbSet.RemoveRange(entities);
        }

        public void Update(T entity)
        {
            _dbSet.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
            
        }
    }
}
