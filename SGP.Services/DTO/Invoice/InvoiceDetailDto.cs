using System;
using System.Collections.Generic;
using System.Text;

namespace SGP.Services.DTO.Invoice
{
    public class InvoiceDetailDto
    {
        public int ProductId { get; set; }
        public string? ProductName { get; set; } // Extra útil
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal SubTotal => Quantity * UnitPrice;
    }
}
