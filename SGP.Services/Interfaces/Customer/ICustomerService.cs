
using SGP.Services.DTO.Customer;

namespace SGP.Services.Interfaces.Customer
{
    public interface ICustomerService
    {
        public Task<IEnumerable<CustomerDto>> GetAllCustomersAsync();
        public Task<CustomerDto?> GetCustomerByIdAsync(int id);
        public Task<CustomerDto> AddCustomerAsync(AddCustomerDto createCustomerDto);
        public Task<CustomerDto?> UpdateCustomerAsync(int id, UpdateCustomerDto updateCustomerDto);
    }
}
