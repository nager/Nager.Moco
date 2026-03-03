namespace Nager.Moco.Models
{
    public class CustomerVat
    {
        public int Id { get; set; }
        public float Tax { get; set; }
        public string Description { get; set; }
        public bool ReverseCharge { get; set; }
        public bool IntraEu { get; set; }
        public bool PrintGrossTotal { get; set; }
        public string NoticeTaxExemption { get; set; }
        public string NoticeTaxExemptionEn { get; set; }
        public string NoticeTaxExemptionAlt { get; set; }
        public bool Active { get; set; }
        public object Code { get; set; }
        public object CreditAccount { get; set; }
    }
}
