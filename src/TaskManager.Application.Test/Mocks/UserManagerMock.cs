using Microsoft.AspNetCore.Identity;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TaskManager.Infrastucture.Identity;

namespace TaskManager.Application.Test.Mocks
{
    public static class UserManagerMock
    {
        /// <summary>
        /// Mock user manager
        /// </summary>
        /// <remarks>NOT BEING USED</remarks>
        /// <typeparam name="TUser"></typeparam>
        /// <param name="ls"></param>
        /// <returns><c>UserManager<TUser></c></returns>
        public static UserManager<TUser> GetUserManager<TUser>(List<TUser> ls) where TUser : class
        {
            var store = new Mock<IUserStore<TUser>>();
            var userManagerMock = new Mock<UserManager<TUser>>(store.Object, null, null, null, null, null, null, null, null);
            userManagerMock.Object.UserValidators.Add(new UserValidator<TUser>());
            userManagerMock.Object.PasswordValidators.Add(new PasswordValidator<TUser>());

            userManagerMock
                .Setup(x => x.DeleteAsync(It.IsAny<TUser>()))
                .ReturnsAsync(IdentityResult.Success);

            userManagerMock
                .Setup(x => x.CreateAsync(It.IsAny<TUser>(), It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Success)
                .Callback<TUser, string>((x, _) => ls.Add(x));

            userManagerMock
                .Setup(x => x.UpdateAsync(It.IsAny<TUser>()))
                .ReturnsAsync(IdentityResult.Success);

            var identityUser = It.IsAny<TUser>();

            userManagerMock
                .Setup(x => x.FindByIdAsync(It.IsAny<string>()))
                .Returns(Task.FromResult(identityUser));
            userManagerMock
                .Setup(x => x.FindByNameAsync(It.IsAny<string>()))
                .Returns(Task.FromResult(identityUser));
            userManagerMock
                .Setup(x => x.FindByEmailAsync(It.IsAny<string>()))
                .Returns(Task.FromResult(identityUser));

            return userManagerMock.Object;
        }
    }
}
