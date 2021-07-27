using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TaskManager.Application.Interfaces;
using TaskManager.Domain.Enums;
using TaskManager.Infrastucture.Identity;

namespace TaskManager.Infrastucture.Persistance
{
    /// <summary>
    /// Seed data
    /// </summary>
    public static class ApplicationDbContextSeed
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

            #region MEMBER USER
            var memberUser = new ApplicationUser
            {
                UserName = "member@user",
                Email = "member@user",
                Name = "Member",
                Surname = "User",
                IsActive = true,
                IsMember = false
            };

            if (userManager.Users.All(u => u.UserName != memberUser.UserName))
            {
                await userManager.CreateAsync(memberUser, "Password1!").ConfigureAwait(false);
                await userManager.AddToRoleAsync(memberUser, "Member").ConfigureAwait(false);

                // Member meta data
                await context.Members
                    .AddAsync(new Domain.Enitities.Member { 
                        UserId = memberUser.Id
                    })
                    .ConfigureAwait(false);
            }
            #endregion MEMBER USER

            #region TASKS
            if (!context.Tasks.Any())
            {
                await context.Tasks
                .AddRangeAsync(
                    new Domain.Enitities.Task { CreatedBy = adminUser.Id, Title = "Task 1", Description = "Task 1 description", Status = TaskStatus_Enum.Unassigned, IsActive = true },
                    new Domain.Enitities.Task { CreatedBy = adminUser.Id, Title = "Task 2", Description = "Task 2 description", Status = TaskStatus_Enum.Unassigned, IsActive = true },
                    new Domain.Enitities.Task { CreatedBy = adminUser.Id, Title = "Task 3", Description = "Task 3 description", Status = TaskStatus_Enum.Unassigned, IsActive = true },
                    new Domain.Enitities.Task { CreatedBy = adminUser.Id, Title = "Task 4", Description = "Task 4 description", Status = TaskStatus_Enum.Unassigned, IsActive = true },
                    new Domain.Enitities.Task { CreatedBy = adminUser.Id, Title = "Task 5", Description = "Task 5 description", Status = TaskStatus_Enum.Unassigned, IsActive = true }
                )
                .ConfigureAwait(false);
            }
            
            #endregion TASKS

            await context.SaveChangesAsync(CancellationToken.None).ConfigureAwait(false);
        }
    }
}
