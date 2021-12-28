using CustomerRegistration.Models;
using CustomerRegistration.Repository;

namespace CustomerRegistration.DbContexts
{
    public interface IGenericUnitOfWork
    {
        public GenericRepository<Customer> CustomerRepository { get; }
        public void SaveChanges();

        public void Dispose();
    }
}
