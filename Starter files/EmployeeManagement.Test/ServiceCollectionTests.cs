using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmployeeManagement.DataAccess.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EmployeeManagement.Test
{
    public class ServiceCollectionTests
    {
        [Fact]
        public void RegisterDataService_Execute_DataServicesAreRegistered()
        {
            // Arrange
            IDictionary<string, string> inMemoryConfigSettings = new Dictionary<string, string>()
            {
                { "ConnectionStrings:EmployeeManagementDB", "AnyValueWillDo" }
            };
                
            var serviceCollection = new ServiceCollection();
            var configuration = new ConfigurationBuilder()
                   .AddInMemoryCollection(inMemoryConfigSettings).Build();
             
            // Act 
            serviceCollection.RegisterDataServices(configuration);
            var serviceProvider = serviceCollection.BuildServiceProvider();

            // Assert
            Assert.NotNull(serviceProvider.GetService<IEmployeeManagementRepository>());
            Assert.IsType<EmployeeManagementRepository>(serviceProvider.GetService<IEmployeeManagementRepository>());
        }
    }
}
