using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace DostPatiler.Data
{
    [Authorize]
    public static class ContextSeed
    {
        public static void Seed(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            SeedRoles(roleManager);
            SeedUsers(userManager);
        }

        private static void SeedRoles(RoleManager<IdentityRole> roleManager)
        {
            if (!roleManager.RoleExistsAsync("Admin").Result)
            {
                var role = new IdentityRole
                {
                    Name = "Admin"
                };
                var result = roleManager.CreateAsync(role).Result;
            }
            if (!roleManager.RoleExistsAsync("User").Result)
            {
                var role = new IdentityRole
                {
                    Name = "User"
                };
                var result = roleManager.CreateAsync(role);
            }
        }

        private static void SeedUsers(UserManager<IdentityUser> userManager)
        {
            if(userManager.Users.Count() == 0)
            {
                var user = new IdentityUser
                {
                    UserName = "g191210387@sakarya.edu.tr",
                    Email = "g191210387@sakarya.edu.tr",
                    EmailConfirmed = true
                };
                var user1 = new IdentityUser
                {
                    UserName = "user@sakarya.edu.tr",
                    Email = "user@sakarya.edu.tr",
                    EmailConfirmed = true
                };
                var result = userManager.CreateAsync(user, "sau").Result;
                userManager.CreateAsync(user1, "sau");
                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "Admin").Wait();
                    userManager.AddToRoleAsync(user1, "User").Wait();
                }
            }
        }
    }
}
