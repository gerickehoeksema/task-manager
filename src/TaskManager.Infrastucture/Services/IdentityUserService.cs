using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskManager.Application.Interfaces;
using TaskManager.Application.Models;
using TaskManager.Infrastucture.Identity;

namespace TaskManager.Infrastucture.Services
{
    /// <summary>
    /// Service that will handle identity implementation
    /// </summary>
    public class IdentityUserService : IIdentityUserService
    {
        internal UserManager<ApplicationUser> userManager;
        internal RoleManager<ApplicationRole> roleManager;
        internal IUserClaimsPrincipalFactory<ApplicationUser> userClaimsPrincipalFactory;
        internal IAuthorizationService authorizationService;
        internal IDateTimeService dateTimeService;

        public IdentityUserService(UserManager<ApplicationUser> userManager
            , RoleManager<ApplicationRole> roleManager
            , IUserClaimsPrincipalFactory<ApplicationUser> userClaimsPrincipalFactory
            , IAuthorizationService authorizationService
            , IDateTimeService dateTimeService)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.userClaimsPrincipalFactory = userClaimsPrincipalFactory;
            this.authorizationService = authorizationService;
            this.dateTimeService = dateTimeService;
        }

        public async Task<Result> AddUserToRoleAsync(long userId, string role)
        {
            var user = await userManager.FindByIdAsync(userId.ToString()).ConfigureAwait(false);
            // Remove all roles and add new roles
            var userRoles = await userManager.GetRolesAsync(user).ConfigureAwait(false);
            await userManager.RemoveFromRolesAsync(user, userRoles).ConfigureAwait(false);
            var result = await userManager.AddToRoleAsync(user, role).ConfigureAwait(false);

            return result.ToApplicationResult();
        }

        public async Task<Result> AddUserToRolesAsync(long userId, IList<string> roles)
        {
            var user = await userManager.FindByIdAsync(userId.ToString()).ConfigureAwait(false);
            // Remove all roles and add new roles
            var userRoles = await userManager.GetRolesAsync(user).ConfigureAwait(false);
            await userManager.RemoveFromRolesAsync(user, userRoles).ConfigureAwait(false);
            var result = await userManager.AddToRolesAsync(user, roles).ConfigureAwait(false);

            return result.ToApplicationResult();
        }

        public async Task<(Result Result, long UserId)> CreateUserAsync(string username, string email, string name, string surname, bool isActive, bool isMember, string password)
        {
            //Create new user
            var user = new ApplicationUser
            {
                NormalizedUserName = username.ToUpper(),
                UserName = username,
                Email = email,
                Name = name,
                Surname = surname,
                IsActive = isActive,
                IsMember = isMember
            };

            var result = await userManager.CreateAsync(user, password).ConfigureAwait(false);

            return (result.ToApplicationResult(), user.Id);
        }

        public async Task<Result> DeleteUserAsync(long userId)
        {
            var user = await userManager.FindByIdAsync(userId.ToString()).ConfigureAwait(false);

            if (user != null)
            {
                var result = await userManager.DeleteAsync(user).ConfigureAwait(false);

                return result.ToApplicationResult();
            }

            return Result.Failure();
        }

        public async Task<(long userId, string username, string email, string name, string surname, string passwordHash, IList<string> roles, bool isActive, bool isMember)> FindUserAsync(long userId)
        {
            var user = await userManager.FindByIdAsync(userId.ToString()).ConfigureAwait(false);
            return ReturnUserInfo(user);
        }

        public async Task<(long userId, string username, string email, string name, string surname, string passwordHash, IList<string> roles, bool isActive, bool isMember)> FindUserAsync(string email)
        {
            var user = await userManager.FindByEmailAsync(email).ConfigureAwait(false);
            return ReturnUserInfo(user);
        }

        public async Task<(long userId, string username, string email, string name, string surname, string passwordHash, IList<string> roles, bool isActive, bool isMember)> FindUserByUsernameAsync(string username)
        {
            var user = await userManager.Users
                .FirstOrDefaultAsync(u => u.UserName.Equals(username)).ConfigureAwait(false);

            return ReturnUserInfo(user);
        }

        public List<string> GetRoles()
        {
            return roleManager.Roles.Select(r => r.Name).ToList();
        }

        public async Task<string> HashPasswordAsync(long userId, string password)
        {
            var user = await userManager.FindByIdAsync(userId.ToString()).ConfigureAwait(false);
            return userManager.PasswordHasher.HashPassword(user, password);
        }

        public async Task<bool> IsInRoleAsync(long userId, string role)
        {
            var user = userManager.Users.SingleOrDefault(u => u.Id == userId);

            return await userManager.IsInRoleAsync(user, role).ConfigureAwait(false);
        }

        public async Task<Result> UpdatePasswordAsync(long userId, string passwordHash)
        {
            var user = await userManager.FindByIdAsync(userId.ToString()).ConfigureAwait(false);
            if (user != null)
            {
                user.PasswordHash = passwordHash;
                user.UpdatedDate = dateTimeService.Now;

                var result = await userManager.UpdateAsync(user).ConfigureAwait(false);

                return result.ToApplicationResult();
            }

            string[] errors = { "Unable to update user password." };
            return Result.Failure(errors);
        }

        public async Task<Result> UpdateUserAsync(long userId, string email, string firstName, string lastName, bool isActive)
        {
            var user = await userManager.FindByIdAsync(userId.ToString()).ConfigureAwait(false);
            if (user != null)
            {
                user.Email = email;
                user.Name = firstName;
                user.Surname = lastName;
                user.IsActive = isActive;
                user.UpdatedDate = dateTimeService.Now;
                var result = await userManager.UpdateAsync(user).ConfigureAwait(false);
                return result.ToApplicationResult();
            }
             string[] errors = { "Unable to update user." };
            return Result.Failure(errors);
        }

        public async Task<bool> ValidateUser(string username, string password)
        {
            var user = await userManager.FindByNameAsync(username).ConfigureAwait(false);
            if (user != null)
            {
                return await userManager.CheckPasswordAsync(user, password).ConfigureAwait(false);
            }

            return false;
        }

        #region INTERNAL METHODS
        internal (long userId, string username, string email, string name, string surname, string passwordHash, IList<string> roles, bool isActive, bool isMember) ReturnUserInfo(ApplicationUser user)
        {
            if (user == null)
                return (0, "", "", "", "", "", null, false, false);

            return (user.Id
                , user.UserName
                , user.Email
                , user.Name
                , user.Surname
                , user.PasswordHash
                , userManager.GetRolesAsync(user).Result
                , user.IsActive
                , user.IsMember);
        }
        #endregion INTERNAL METHODS
    }
}
