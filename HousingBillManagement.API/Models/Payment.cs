namespace HousingBillManagement.API.Models
{
    public class Payment
    {
        public int Id { get; set; }
        public string PaymentType { get; set; }
        public DateTime PaymentDate { get; set; }
        public decimal Amount { get; set; }
        public Bill Bill { get; set; }
        public User User { get; set; }
    }
}
