using Microsoft.AspNetCore.Mvc;
using SGP.Services.DTO.Customer;
using SGP.Services.Interfaces.Customer;

namespace SGP.Api.Controllers
{
    // [Route]: Define la URL base. [controller] se reemplaza por el nombre de la clase menos "Controller".
    // Resultado: La URL será "api/customers"
    [Route("api/[controller]")]
    [ApiController] // Este atributo activa validaciones automáticas del DTO (como [EmailAddress])
    public class CustomersController : Controller
    {
        private readonly ICustomerService _customerService;

        public CustomersController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CustomerDto>>> GetAll()
        {
            var customers = await _customerService.GetAllCustomersAsync();
            return Ok(customers); // Retorna estatus 200 OK con la lista
        }

        // GET: api/customers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerDto>> GetById(int id)
        {
            var customer = await _customerService.GetCustomerByIdAsync(id);

            if (customer == null)
            {
                return NotFound(); // Retorna estatus 404 Not Found si no existe
            }

            return Ok(customer); // Retorna estatus 200 OK con el objeto
        }

        // POST: api/customers
        [HttpPost]
        public async Task<ActionResult<CustomerDto>> Create([FromBody] AddCustomerDto customerDto)
        {
            try
            {
                var newCustomer = await _customerService.AddCustomerAsync(customerDto);

                // Retorna estatus 201 Created.
                // CreatedAtAction agrega un encabezado "Location" en la respuesta con la URL del nuevo recurso.
                // Param 1: El nombre del método para leerlo (GetById).
                // Param 2: Los parámetros que necesita ese método (el nuevo id).
                // Param 3: El objeto creado.
                return CreatedAtAction(nameof(GetById), new { id = newCustomer.Id }, newCustomer);
            }
            catch (Exception ex)
            {
                // Capturamos la excepción del email duplicado que lanzaste en el servicio
                return BadRequest(ex.Message); // Retorna estatus 400 con el mensaje de error
            }
        }

        // PUT: api/customers/5
        [HttpPut("{id}")]
        public async Task<ActionResult<CustomerDto>> Update(int id, [FromBody] UpdateCustomerDto customerDto)
        {

            var updatedCustomer = await _customerService.UpdateCustomerAsync(id, customerDto);

            if (updatedCustomer == null)
            {
                return NotFound(); 
            }

            return Ok(updatedCustomer); 
        }
    }
}
