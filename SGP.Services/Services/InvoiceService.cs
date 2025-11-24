using Microsoft.EntityFrameworkCore;
using SGP.Domain.Entities;
using SGP.Persistence.Context;
using SGP.Services.DTO.Invoice;
using SGP.Services.Interfaces.Invoice;

namespace SGP.Services.Services
{
    public class InvoiceService: IInvoiceService
    {
        private readonly AppDbContext _context;

        public InvoiceService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<InvoiceDto> AddInvoiceAsync(AddInvoiceDto dto)
        {
            // 1. Validar que el Cliente exista
            var customer = await _context.Customers.FindAsync(dto.CustomerId);
            if (customer == null) throw new Exception($"Cliente {dto.CustomerId} no encontrado.");

            // 2. Preparar la Entidad Factura (Cabecera)
            var newInvoice = new Invoice
            {
                CustomerId = dto.CustomerId,
                InvoiceDate = DateTime.Now,
                InvoiceDetails = new List<InvoiceDetail>() 
            };

            // 3. Procesar los Detalles (Iterar sobre los items que mandó el usuario)
            foreach (var itemDto in dto.Items)
            {
                // A. Buscar el producto real para obtener su precio actual
                var product = await _context.Products.FindAsync(itemDto.ProductId);

                if (product == null)
                    throw new Exception($"Producto {itemDto.ProductId} no encontrado.");

                // B. Crear la entidad Detalle
                var detail = new InvoiceDetail
                {
                    ProductId = product.Id,
                    Quantity = itemDto.Quantity,
                    UnitPrice = product.Price, 
                };

                // C. Agregarlo a la lista de la factura
                newInvoice.InvoiceDetails.Add(detail);
            }

            // 4. Calcular el Total
            newInvoice.CalculateTotalAmount();

            // 5. Guardar en Base de Datos (Transacción Atómica)
            _context.Invoices.Add(newInvoice);

            await _context.SaveChangesAsync();

            // 6. Retornar el DTO mapeado
            return await GetByIdAsync(newInvoice.Id);
        }

        public async Task<InvoiceDto?> GetByIdAsync(int id)
        {
            // Aqui se usa include, ya que si no los Items vendrán vacíos y el Cliente será null.
            var invoice = await _context.Invoices
                .Include(i => i.Customer)       // Trae datos del cliente
                .Include(i => i.InvoiceDetails) // Trae la lista de detalles
                    .ThenInclude(d => d.Product)// Trae el producto de cada detalle
                .FirstOrDefaultAsync(i => i.Id == id);

            if (invoice == null) return null;

            return new InvoiceDto
            {
                Id = invoice.Id,
                CustomerId = invoice.CustomerId,
                CustomerName = invoice.Customer.Name,
                Date = invoice.InvoiceDate,
                TotalAmount = invoice.TotalAmount,
                Items = invoice.InvoiceDetails.Select(d => new InvoiceDetailDto
                {
                    ProductId = d.ProductId,
                    ProductName = d.Product.Name,
                    Quantity = d.Quantity,
                    UnitPrice = d.UnitPrice
                }).ToList()
            };
        }
    }
}
