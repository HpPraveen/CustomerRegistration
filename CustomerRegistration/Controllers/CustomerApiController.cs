using CustomerRegistration.Models.DTO;
using CustomerRegistration.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CustomerRegistration.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class CustomerApiController : ControllerBase
    {
        private readonly ICustomerService _customerService;
        private ResponseDto _response;

        public CustomerApiController(ICustomerService customerService)
        {
            _customerService = customerService;
            _response = new ResponseDto();
        }

        [HttpGet]
        public List<CustomerDto>? GetAllCustomers()
        {
            try
            {
                return _customerService.GetAllCustomers();
            }
            catch (Exception)
            {
                return null;
            }
        }

        [HttpGet("{pageNumber}/{pageSize}/{name}")]
        public object GetAllCustomersByPaging(int pageNumber, int pageSize, string name)
        {
            try
            {
                _response = (ResponseDto)_customerService.GetAllCustomersByPaging(pageNumber, pageSize, name);
                _response.DisplayMessage = _response.Result == null ? "Empty Customers" : "Get customers by paging. Page # = " + pageNumber + "";
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        [HttpGet("{customerId}")]
        public object? GetCustomer(int customerId)
        {
            try
            {
                return _customerService.GetCustomer(customerId);
            }
            catch (Exception)
            {
                return null;
            }
        }

        [HttpGet("{name}")]
        public object? SearchCustomers(string name)
        {
            try
            {
                return _customerService.SearchCustomers(name);
            }
            catch (Exception)
            {
                return null;
            }
        }

        [HttpPost]
        public object? CreateCustomer(CustomerDto customerDto)
        {
            try
            {
                return _customerService.CreateUpdateCustomer(customerDto);
            }
            catch (Exception)
            {
                return null;
            }
        }

        [HttpPut]
        public object? UpdateCustomer(CustomerDto customerDto)
        {
            try
            {
                return _customerService.CreateUpdateCustomer(customerDto);
            }
            catch (Exception)
            {
                return null;
            }
        }

        [HttpDelete("{customerId}")]
        public object? DeleteCustomer(int customerId)
        {
            try
            {
                return _customerService.DeleteCustomer(customerId);
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
