using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace TaskManger.API.Integration.Test
{
    [SetUpFixture]
    public class Testing
    {
        private static readonly IConfigurationRoot configuration;
        private static readonly IServiceScopeFactory scopeFactory;

        public static HttpClient client;
        public static TestServer server;
    }
}
