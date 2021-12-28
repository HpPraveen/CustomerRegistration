using AutoMapper;
using CustomerRegistration.DbContexts;
using CustomerRegistration.Models;
using CustomerRegistration.Models.DTO;
using CustomerRegistration.Pagination;
using CustomerRegistration.Services.IServices;
using System.Linq.Expressions;

namespace CustomerRegistration.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly IGenericUnitOfWork _genericUnitOfWork;
        private readonly IMapper _mapper;

        public CustomerService(IGenericUnitOfWork genericUnitOfWork, IMapper mapper)
        {
            _genericUnitOfWork = genericUnitOfWork;
            _mapper = mapper;
        }

        public List<CustomerDto>? GetAllCustomers()
        {
            try
            {
                var customerList = _genericUnitOfWork.CustomerRepository.Get().ToList();
                if (customerList == null) return null;
                return _mapper.Map<List<CustomerDto>>(customerList);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public object GetAllCustomersByPaging(int pageNumber, int pageSize, string name)
        {
            try
            {
                List<Expression<Func<Customer, bool>>> filters = new();

                if (!string.IsNullOrEmpty(name))
                    filters.Add(c => c.Name.Contains(name));

                static IOrderedQueryable<Customer> orderBy(IQueryable<Customer> o) => o.OrderByDescending(d => d.SysCreatedOn);

                var customerList = _genericUnitOfWork.CustomerRepository.GetPaging(orderBy: orderBy, filters: filters, pageNumber: pageNumber, pageSize: pageSize);

                ResponseDto response = (ResponseDto)customerList;
                var totalAds = response.TotalRecords;
                var classifiedAdsDto = _mapper.Map<List<CustomerDto>>(response.Result);

                return PaginationHelper.CreatePagedReponse(classifiedAdsDto, pageNumber, pageSize, totalAds, null);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public CustomerDto GetCustomer(int customerId)
        {
            try
            {
                var customerDetails = _genericUnitOfWork.CustomerRepository.Get(c => c.Id == customerId).FirstOrDefault();
                if (customerDetails == null) return null;
                return _mapper.Map<CustomerDto>(customerDetails);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<CustomerDto>? SearchCustomers(string name)
        {
            try
            {
                var customerList = _genericUnitOfWork.CustomerRepository.Get(c => c.Name.Contains(name)).ToList();
                if (customerList == null) return null;
                return _mapper.Map<List<CustomerDto>>(customerList);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public CustomerDto CreateUpdateCustomer(CustomerDto customerDto)
        {
            try
            {
                var customer = _mapper.Map<Customer>(customerDto);
                if (customer.Id > 0)
                {
                    customer.SysUpdatedOn = DateTime.Now;
                    _genericUnitOfWork.CustomerRepository.Update(customer);
                }
                else
                {
                    var availableCustomerList = _genericUnitOfWork.CustomerRepository.Get(c => c.Mail == customer.Mail /*&& c.IsDeleted == false*/).ToList();
                    if (availableCustomerList.Count > 0)
                    {
                        return null;
                    }
                    else
                    {
                        customer.SysCreatedOn = DateTime.Now;
                        _genericUnitOfWork.CustomerRepository.Insert(customer);
                    }
                }
                _genericUnitOfWork.SaveChanges();
                return _mapper.Map<CustomerDto>(customer);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public bool DeleteCustomer(int customerId)
        {
            try
            {
                var customer = _genericUnitOfWork.CustomerRepository.Get(c => c.Id == customerId).FirstOrDefault();
                if (customer == null) return false;
                _genericUnitOfWork.CustomerRepository.Delete(customer);
                _genericUnitOfWork.SaveChanges();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
