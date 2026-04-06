namespace Nager.Moco.QueryFilters
{
    public class ReceiptQueryFilter
    {
        public DateOnly? DateFrom { get; set; }

        public DateOnly? DateTo { get; set; }

        public int? ProjectId { get; set; }
    }
}
