namespace HousingBillManagement.API.Models
{
    public class Bill
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public decimal Amount { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
        public Apartment Apartment { get; set; }
        public User User { get; set; }
    }
}
