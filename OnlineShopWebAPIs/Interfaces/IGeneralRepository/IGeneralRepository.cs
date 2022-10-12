using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace OnlineShopWebAPIs.Interfaces.IGeneralRepository
{
    public interface IGeneralRepository<T> where T : class
    {
        public T GetById(int id);
        public IEnumerable<T> GetAll(List<string> includes = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null);
        public IEnumerable<T> Find(Expression<Func<T, bool>> predicate, List<string> includes = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null);

        public void Insert(T entity);
        public void InsertRange(List<T> entities);

        public void Delete(T entity);
        public void DeleteRange(List<T> entities);

        public void Update(T entity);
    }
}
