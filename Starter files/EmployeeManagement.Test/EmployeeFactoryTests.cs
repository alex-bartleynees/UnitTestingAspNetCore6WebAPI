using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using EmployeeManagement.Business;
using EmployeeManagement.DataAccess.Entities;

namespace EmployeeManagement.Test
{
    public class EmployeeFactoryTests
    {
        [Fact]
        public void CreateEmployee_ConstructInternalEmployee_SalaryMustBe2500()
        {
            var EmployeeFactory = new EmployeeFactory();

            var employee = (InternalEmployee)EmployeeFactory.CreateEmployee("Kevin", "Dockx");

            Assert.Equal(2500, employee.Salary);
        }

        [Fact]
        public void CreateEmployee_ConstructInternalEmployee_SalaryMustBeBetween2500And3500()
        {
            var EmployeeFactory = new EmployeeFactory();

            var employee = (InternalEmployee)EmployeeFactory.CreateEmployee("Kevin", "Dockx");

            Assert.True(employee.Salary >= 2500 && employee.Salary <= 3500);
        }

        [Fact]
        public void CreateEmployee_ConstructInternalEmployee_SalaryMustBeBetween2500And350_Alternative()
        {
            var EmployeeFactory = new EmployeeFactory();

            var employee = (InternalEmployee)EmployeeFactory.CreateEmployee("Kevin", "Dockx");

            Assert.InRange(employee.Salary, 2500, 3500);
        }

        [Fact]
        public void CreateEmployee_ConstructInternalEmployee_SalaryMustBe2500Precision()
        {
            var EmployeeFactory = new EmployeeFactory();

            var employee = (InternalEmployee)EmployeeFactory.CreateEmployee("Kevin", "Dockx");
            employee.Salary = 2500.123m;

            Assert.Equal(2500, employee.Salary, 0);
        }

        [Fact]
        public void CreateEmployee_IsExternalIsTrue_ReturnTypeMustBeExternalEmployee()
        {
            // Arrange
            var factory = new EmployeeFactory();

            // Act
            var employee = factory.CreateEmployee("Alex", "Bartley Nees", "Valocity", true);

            // Assert
            Assert.IsType<ExternalEmployee>(employee);

            //Assert.IsAssignableFrom<Employee>(employee);
        }
    }
}
