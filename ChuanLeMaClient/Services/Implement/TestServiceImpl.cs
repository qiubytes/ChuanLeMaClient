using ChuanLeMaClient.Services.Inteface;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChuanLeMaClient.Services.Implement
{
    public class TestServiceImpl : ITestService
    {

        private readonly ILogger<TestServiceImpl> _logger;

        public TestServiceImpl(ILogger<TestServiceImpl>  logger)
        {
            _logger = logger;
        }
        public void Hello()
        {
            _logger.LogInformation("Hello method in TestServiceImpl called.");
            Console.WriteLine("ceshi hello");
        }
    }
}
