using Microsoft.AspNetCore.Identity;
using Store.Data.Entities.IdentityEntities;

namespace Store.Repository
{
    public static class AppIdentityContextSeed
    {
        public static async Task SeedUserAsync(UserManager<AppUser> userManager)
        {
            if (!userManager.Users.Any())
            {
                var user = new AppUser
                {
                    DisplayName = "Kareem waleed",
                    Email = "kareem@gmail.com",
                    UserName = "kareemwaleed",
                    Address = new Address
                    {
                        FirstName = "Kareem",
                        LastName = "Waleed",
                        City = "Helwan",
                        State = "Cairo",
                        Street = "9",
                        ZipCode = "12345"
                    }
                };
                await userManager.CreateAsync(user, "Kareem123#");
            }
        }
    }
}
