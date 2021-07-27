using System;
using TaskManager.Application.Interfaces;

namespace TaskManager.Infrastucture.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        public long UserId => throw new NotImplementedException();
    }
}
