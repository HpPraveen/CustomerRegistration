using CustomerRegistration.Models.DTO;
using CustomerRegistration.Services.IServices;
using Microsoft.AspNetCore.Mvc;

namespace CustomerRegistration.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CustomerApiController : ControllerBase
    {
        private readonly ICustomerService _customerService;
        private readonly ResponseDto _response;

        public CustomerApiController(ICustomerService customerService)
        {
            _customerService = customerService;
            _response = new ResponseDto();
        }

        [HttpGet]
        [Route("GetAllCustomers")]
        public object? GetAllCustomers()
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

        [HttpGet]
        [Route("GetAllCustomersByPaging/{pageNumber}/{pageSize}")]
        public object? GetAllCustomersByPaging(int pageNumber, int pageSize)
        {
            try
            {
                return _customerService.GetAllCustomersByPaging(pageNumber, pageSize);
            }
            catch (Exception)
            {
                return null;
            }
        }

        [HttpGet]
        [Route("GetCustomer/{customerId}")]
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

        [HttpGet]
        [Route("SearchCustomers/{name}")]
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
        [Route("CreateCustomer")]
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
        [Route("UpdateCustomer")]
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

        [HttpGet]
        [Route("DeleteCustomer/{customerId}")]
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
