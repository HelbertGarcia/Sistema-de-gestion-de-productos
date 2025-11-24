using System;
using System.Collections.Generic;
using System.Text;

namespace SGP.Services.DTO.Invoice
{
    public class InvoiceDto
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public string CustomerName { get; set; } // Extra útil
        public DateTime Date { get; set; }
        public decimal TotalAmount { get; set; }
        public List<InvoiceDetailDto> Items { get; set; }
    }
}
