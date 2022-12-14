using Core.Identity;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using System.Linq;
namespace Infrastructure.Identity
{
    public class AppIdentityDbContextSeed
    {
        public static async Task SeedUserAsync(UserManager <AppUser> userManager)
        {
            if(!userManager.Users.Any())
            {
                var user = new AppUser
                {
                    DisplayName="Malik",
                    Email= "Malik@test.com",
                    UserName="Malik@test.com",
                    Address = new Address
                    {
                        FirstName = "Malik",
                        LastName = "Usman",
                        Street = "Stoke on Trent 18",
                        City="Erlangen",
                        State = "BA",
                        ZipCode="91058"

                    }
                };
                await userManager.CreateAsync(user,"M@lik1161");
            }
        }
    }
}