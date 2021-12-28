using CustomerRegistration.Models;
using CustomerRegistration.Repository;

namespace CustomerRegistration.DbContexts
{
    public class GenericUnitOfWork : IGenericUnitOfWork
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public GenericUnitOfWork(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public GenericRepository<Customer> CustomerRepository => new(_applicationDbContext);

        public void SaveChanges()
        {
            _applicationDbContext.SaveChanges();
        }

        private bool _disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _applicationDbContext.Dispose();
                }
            }
            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
#pragma warning disable CA1816 // Dispose methods should call SuppressFinalize
            GC.SuppressFinalize(this);
#pragma warning restore CA1816 // Dispose methods should call SuppressFinalize
        }
    }
}
