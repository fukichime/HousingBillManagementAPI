namespace HousingBillManagement.API.Models
{
    public class Apartment
    {
        public int Id { get; set; }
        public string BlockInfo { get; set; }
        public bool IsOccupied { get; set; }
        public string ApartmentType { get; set; }
        public int Floor { get; set; }
        public int Number { get; set; }
        public bool areTheyOwner { get; set; }
    }
}
