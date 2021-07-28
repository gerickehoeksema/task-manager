using AutoMapper;
using Microsoft.AspNetCore.Identity;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Application.Interfaces;
using TaskManager.Application.Mappings;
using TaskManager.Infrastucture.Identity;
using TaskManager.Infrastucture.Persistance;

namespace TaskManager.Application.Test.MemberTests
{
    public class GetMemberListQueryTest
    {
        internal IApplicationDbContext context;
        internal IMapper mapper;

        public GetMemberListQueryTest()
        {
            var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });

            mapper = configurationProvider.CreateMapper();
        }

        [SetUp]
        public void Setup()
        {
            context = ApplicationDbContextFactory.Create();
        }

        private async Task SeedData()
        {
            
        }
    }
}
