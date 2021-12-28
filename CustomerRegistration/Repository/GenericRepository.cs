using CustomerRegistration.DbContexts;
using CustomerRegistration.Models.DTO;
using Microsoft.EntityFrameworkCore;
using Saruwata.Service.AdvertisingApi.Repository;
using System.Linq.Expressions;

namespace CustomerRegistration.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly ApplicationDbContext _applicationDbContext;
        internal DbSet<T> DbSet;

        public GenericRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
            DbSet = _applicationDbContext.Set<T>();
        }

        public virtual List<T> Get(Expression<Func<T, bool>>? filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
            string includeProperties = "",
            Expression<Func<T, bool>>[]? filters = null)
        {
            IQueryable<T>? query = null;

            if (filter != null)
            {
                query = DbSet.Where(filter);
            }
            else if (filter == null)
            {
                query = DbSet;
            }

            if (filters != null)
            {
                foreach (var expression in filters)
                {
                    if (expression != null)
                    {
#pragma warning disable CS8604 // Possible null reference argument.
                        query = query.Where(expression);
#pragma warning restore CS8604 // Possible null reference argument.
                    }
                }
            }

            foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
#pragma warning disable CS8604 // Possible null reference argument.
                query = query.Include(includeProperty);
#pragma warning restore CS8604 // Possible null reference argument.
            }

#pragma warning disable CS8604 // Possible null reference argument.
            return orderBy != null ? orderBy(query).AsSplitQuery().ToList() : query.AsSplitQuery().ToList();
#pragma warning restore CS8604 // Possible null reference argument.
        }

        public virtual object GetPaging(List<Expression<Func<T, bool>>>? filters = null,
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
            string includeProperties = "", int pageNumber = 0, int pageSize = 0)
        {
            IQueryable<T> query = DbSet;

            if (filters != null)
            {
                foreach (var expression in filters)
                {
                    query = query.Where(expression);
                }
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }

            var count = query.Count();

            foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            return (ResponseDto)(new()
            {
                Result = query.Skip((pageNumber - 1) * pageSize).Take(pageSize).AsSplitQuery().AsEnumerable(),
                TotalRecords = count
            });
        }

        public virtual T GetById(object id)
        {
#pragma warning disable CS8603 // Possible null reference return.
            return DbSet.Find(id);
#pragma warning restore CS8603 // Possible null reference return.
        }

        public virtual void Insert(T entity)
        {
            DbSet.Add(entity);
        }

        public virtual void Delete(object id)
        {
            var entityToDelete = DbSet.Find(id);
#pragma warning disable CS8604 // Possible null reference argument.
            Delete(entityToDelete);
#pragma warning restore CS8604 // Possible null reference argument.
        }

        public virtual void Delete(T entityToDelete)
        {
            if (_applicationDbContext.Entry(entityToDelete).State == EntityState.Modified)
            {
                DbSet.Attach(entityToDelete);
            }
            DbSet.Remove(entityToDelete);
        }

        public virtual void Update(T entityToUpdate)
        {
            DbSet.Attach(entityToUpdate);
            _applicationDbContext.Entry(entityToUpdate).State = EntityState.Modified;
        }
    }
}

