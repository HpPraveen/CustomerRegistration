using CustomerRegistration.Models.DTO;

namespace CustomerRegistration.Services.IServices
{
    public interface ICustomerService
    {
        object? GetAllCustomers();
        object? GetAllCustomersByPaging(int pageNumber, int pageSize);
        object? GetCustomer(int customerId);
        object? SearchCustomers(string name);
        object? CreateUpdateCustomer(CustomerDto customerDto);
        bool DeleteCustomer(int customerId);
    }
}
