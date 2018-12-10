using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace SportStoreCore2.Models
{
    public static class IdentitySeedData
    {
        private const string AdminUser = "Admin";
        private const string AdminPassword = "Secret123$";

        public static async Task EnsurePopulated(UserManager<IdentityUser> userManager)
        {
            var user = await userManager.FindByIdAsync(AdminUser);
            if (user != null)
            {
                return;
            }
            user = new IdentityUser("Admin");
            await userManager.CreateAsync(user, AdminPassword);
        }
    }
}
