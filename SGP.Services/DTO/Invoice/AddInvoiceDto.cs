using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SGP.Services.DTO.Invoice
{
    public class AddInvoiceDto
    {
        [Required]
        public int CustomerId { get; set; }

        [Required]
        [MinLength(1, ErrorMessage = "La factura debe tener al menos un producto")]
        public List<AddInvoiceDetailDto>? Items { get; set; }
    }
}
