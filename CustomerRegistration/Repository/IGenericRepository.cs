using System.Linq.Expressions;

namespace Saruwata.Service.AdvertisingApi.Repository
{
    public interface IGenericRepository<T> where T : class
    {
        List<T> Get(Expression<Func<T, bool>>? filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
            string includeProperties = "",
            Expression<Func<T, bool>>[]? filters = null);

        object GetPaging(List<Expression<Func<T, bool>>>? filters = null,
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
            string includeProperties = "", int pageNumber = 0, int pageSize = 0);

        T GetById(object id);

        void Insert(T entity);

        void Delete(object id);

        void Delete(T entityToDelete);

        void Update(T entityToUpdate);
    }
}

