using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace SportStoreCore2.Models
{
    public static class IdentitySeedData
    {
        private const string AdminUser = "Admin";
        private const string AdminPassword = "Secret123$";

        public static async void EnsurePopulated(IApplicationBuilder app)
        {
            var userManager = app.ApplicationServices.GetRequiredService<UserManager<IdentityUser>>();
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
