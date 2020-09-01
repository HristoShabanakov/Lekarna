namespace Lekarna.Data.Seeding
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Lekarna.Common;
    using Lekarna.Data.Models;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.DependencyInjection;

    using static Lekarna.Common.GlobalConstants;

    public class AdminSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider
                .GetRequiredService<UserManager<ApplicationUser>>();

            if (dbContext.ApplicationUsers.Any(u => u.UserName == AdminEmail))
            {
                return;
            }

            var user = new ApplicationUser
            {
                UserName = Administrator,
                Email = AdminEmail,
            };

            var password = AdminPassword;

            var result = await userManager.CreateAsync(user, password);

            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(user, GlobalConstants.AdministratorRoleName);
            }
        }
    }
}
