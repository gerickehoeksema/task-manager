using AutoMapper;
using System;
using TaskManager.Application.Interfaces;
using TaskManager.Application.Mappings;
using TaskManager.Infrastucture.Persistance;

namespace TaskManager.Application.Test
{
    public class QueryTestBase : IDisposable
    {
        public IMapper Mapper { get; }
        public IApplicationDbContext Context { get; }

        public QueryTestBase()
        {
            var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });

            Mapper = configurationProvider.CreateMapper();
            Context = ApplicationDbContextFactory.Create();
        }

        public void Dispose()
        {
            ApplicationDbContextFactory.Destroy((ApplicationDbContext)Context);
        }
    }
}
