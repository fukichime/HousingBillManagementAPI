using Microsoft.AspNetCore.Identity;

namespace HousingBillManagement.API.Models
{
    public class User : IdentityUser<Guid>
    {
        public string FullName { get; set; }
        public string TCNo { get; set; }
        public string PhoneNumber { get; set; }
        public Apartment Apartment { get; set; }
    }
}
