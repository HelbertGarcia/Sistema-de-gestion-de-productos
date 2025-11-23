using SGP.Domain.Base;
using System.ComponentModel.DataAnnotations;

namespace SGP.Domain.Entities
{
    public class Invoice: BaseEntity
    {
        public int CustomerId { get; set; }
        public virtual Customer Customer { get; set; }
        public DateTime InvoiceDate { get; set; }
        public decimal TotalAmount { get; private set; }
        public virtual ICollection<InvoiceDetail> InvoiceDetails { get; set; } = new List<InvoiceDetail>();
        public void CalculateTotalAmount() 
        {
            decimal total = InvoiceDetails.Sum(d=> d.Quantity* d.UnitPrice);
            TotalAmount = total;
        }
    }
}
