namespace Nager.Moco.Models
{
    public class Payment
    {
        public int Id { get; set; }
        public string Date { get; set; }
        public float PaidTotal { get; set; }
        public string Currency { get; set; }
        public string CreatedOn { get; set; }
        public string UpdatedOn { get; set; }
    }
}
