using SGP.Services.DTO.Invoice;
using System;
using System.Collections.Generic;
using System.Text;

namespace SGP.Services.Interfaces.Invoice
{
    public interface IInvoiceService
    {
        Task<InvoiceDto> AddInvoiceAsync(AddInvoiceDto dto);
        Task<InvoiceDto?> GetByIdAsync(int id);
    }
}
