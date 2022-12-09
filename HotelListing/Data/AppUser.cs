using Microsoft.AspNetCore.Identity;

namespace HotelListing.Data
{
    public class AppUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
