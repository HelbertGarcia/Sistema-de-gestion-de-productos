using Microsoft.EntityFrameworkCore;
using SGP.Domain.Entities;
using SGP.Persistence.Context;
using SGP.Services.DTO.Customer;
using SGP.Services.Interfaces.Customer;

namespace SGP.Services.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly AppDbContext _context;
        public CustomerService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<CustomerDto> AddCustomerAsync(AddCustomerDto createCustomerDto)
        {
            if (await _context.Customers.AnyAsync(c => c.Email == createCustomerDto.Email))
            {
                throw new Exception("El correo electrónico ya está en uso.");
            }
            var customer = new Customer
            {
                Name = createCustomerDto.Name,
                Email = createCustomerDto.Email,
                Phone = createCustomerDto.Phone
            };

            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();

            return new CustomerDto
            {
                Id = customer.Id,
                Name = customer.Name,
                Email = customer.Email,
                Phone = customer.Phone
            };

        }

        public async Task<IEnumerable<CustomerDto>> GetAllCustomersAsync()
        {
            return await _context.Customers.Select(c => new CustomerDto
            {
                Id = c.Id,
                Name = c.Name,
                Email = c.Email,
                Phone = c.Phone
            }).ToListAsync();
        }

        public async Task<CustomerDto?> GetCustomerByIdAsync(int id)
        {
            var customer = await _context.Customers.FindAsync(id);

            if (customer == null) return null;

            return new CustomerDto
            {
                Id = customer.Id,
                Name = customer.Name,
                Email = customer.Email,
                Phone = customer.Phone
            };
        }

        public async Task<CustomerDto?> UpdateCustomerAsync(int id, UpdateCustomerDto updateCustomerDto)
        {
            var customer = await _context.Customers.FindAsync(id);

            if (customer == null)
            {
                return null;
            }

            customer!.Name = updateCustomerDto.Name ?? customer.Name;
            customer.Email = updateCustomerDto.Email ?? customer.Email;
            customer.Phone = updateCustomerDto.Phone ?? customer.Phone;

            await _context.SaveChangesAsync();

            return new CustomerDto
            {
                Id = customer.Id,
                Name = customer.Name,
                Email = customer.Email,
                Phone = customer.Phone
            };
        }
    }
}
