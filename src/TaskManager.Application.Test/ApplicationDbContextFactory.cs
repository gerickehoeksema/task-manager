using Microsoft.EntityFrameworkCore;
using System;
using TaskManager.Application.Interfaces;
using TaskManager.Application.Test.Mocks;
using TaskManager.Infrastucture.Persistance;

namespace TaskManager.Application.Test
{
    /// <summary>
    /// Create in memory database context
    /// </summary>
    public static class ApplicationDbContextFactory
    {
        public static IApplicationDbContext Create()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                    .UseInMemoryDatabase(Guid.NewGuid().ToString())
                    .Options;

            return new ApplicationDbContext(options, DateTimeServiceMock.GetDateTimeService());
        }

        public static void Destroy(ApplicationDbContext context)
        {
            context.Dispose();
        }
    }
}
