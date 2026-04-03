namespace Nager.Moco.Models
{
    public class ReceiptQueryFilter
    {
        public DateOnly? DateFrom { get; set; }

        public DateOnly? DateTo { get; set; }

        public int? ProjectId { get; set; }
    }
}
