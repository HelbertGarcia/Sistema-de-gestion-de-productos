
using System.ComponentModel.DataAnnotations;

namespace SGP.Services.DTO.Invoice
{
    public class AddInvoiceDetailDto
    {
        [Required]
        public int ProductId { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "La cantidad debe ser al menos 1")]
        public int Quantity { get; set; }
    }
}
