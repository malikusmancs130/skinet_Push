using Core.Entities.Identity;
using Microsoft.AspNetCore.Identity;

namespace Core.Identity
{
    public class AppUser : IdentityUser
    {
        public string DisplayName { get; set; }
        public Address Address { get; set; }
        
        
    }
}