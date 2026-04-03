namespace Nager.Moco.Models
{
    public class PurchaseQueryFilter
    {
        public DateOnly? DateFrom { get; set; }

        public DateOnly? DateTo { get; set; }

        public int? CompanyId { get; set; }
    }
}
