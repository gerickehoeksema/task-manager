using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Application.Interfaces;
using TaskManager.Infrastucture.Identity;

namespace TaskManager.Infrastucture.Persistance
{
    public class ApplicationDbContextSeed
    {
        public static async Task SeedAsync(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager, IApplicationDbContext context)
        {
            #region ROLES
            if (!roleManager.Roles.Any())
            {
                await roleManager.CreateAsync(new ApplicationRole
                {
                    Name = "Admin",
                    NormalizedName = "ADMIN"
                }).ConfigureAwait(false);
                await roleManager.CreateAsync(new ApplicationRole
                {
                    Name = "Member",
                    NormalizedName = "MEMBER"
                }).ConfigureAwait(false);
            }
            #endregion ROLES

            #region ADMIN USER
            var adminUser = new ApplicationUser
            {
                UserName = "admin@user",
                Email = "admin@user",
                Name = "Admin",
                Surname = "User",
                IsActive = true,
                IsMember = false
            };

            if (userManager.Users.All(u => u.UserName != adminUser.UserName))
            {
                await userManager.CreateAsync(adminUser, "Password1!").ConfigureAwait(false);
                await userManager.AddToRoleAsync(adminUser, "Admin").ConfigureAwait(false);
            }
            #endregion ADMIN USER
        }
    }
}
