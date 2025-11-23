using SGP.Domain.Base;

namespace SGP.Domain.Entities
{
    public class InvoiceDetail: BaseEntity
    {
        public int InvoiceId { get; set; }
        public int ProductId { get; set; }
        public Product? Product { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
