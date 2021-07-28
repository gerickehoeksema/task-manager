using System;
using TaskManager.Application.Interfaces;
using TaskManager.Infrastucture.Persistance;

namespace TaskManager.Application.Test
{
    public class CommandTestBase : IDisposable
    {
        public IApplicationDbContext Context { get; set; }
        public CommandTestBase()
        {
            Context = ApplicationDbContextFactory.Create();
        }

        public void Dispose()
        {
            ApplicationDbContextFactory.Destroy((ApplicationDbContext)Context);
        }
    }
}
