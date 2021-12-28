using CustomerRegistration.Models.DTO;

namespace CustomerRegistration.Services.IServices
{
    public interface ICustomerService
    {
        List<CustomerDto>? GetAllCustomers();
        object GetAllCustomersByPaging(int pageNumber, int pageSize, string name);
        CustomerDto GetCustomer(int customerId);
        List<CustomerDto>? SearchCustomers(string name);
        CustomerDto CreateUpdateCustomer(CustomerDto customerDto);
        bool DeleteCustomer(int customerId);
    }
}
