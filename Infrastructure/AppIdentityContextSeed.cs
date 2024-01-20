using Core.IdentityEntities;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure
{
    public class AppIdentityContextSeed
    {
        public static async Task SeedUserAsync(UserManager<AppUser> userManager)
        {
            if(!userManager.Users.Any())
            {
                var user = new AppUser
                {
                    DisplayName = "Abdelrahman",
                    Email = "Badran@yahoo.com",
                    UserName = "Badran",
                    Address = new Address
                    {
                       FirstName = "Abdelrahman",
                       LastName = "Badran",
                       Street  = "22",
                       State= "Sharkia",
                       City  = "Hehia",
                       ZipCode  = "7223",
                    }

                };
                await userManager.CreateAsync(user , "Password123!");
            }
        }
    }
}
