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
    public class EmployeeFactoryTests : IDisposable
    {
        private EmployeeFactory _employeeFactory;

        public EmployeeFactoryTests() {
            _employeeFactory = new EmployeeFactory();
        }

        [Fact]
        [Trait("Category", "EmployeeFactory_CreateEmployee_Salary")]
        public void CreateEmployee_ConstructInternalEmployee_SalaryMustBe2500()
        {

            var employee = (InternalEmployee)_employeeFactory.CreateEmployee("Kevin", "Dockx");

            Assert.Equal(2500, employee.Salary);
        }

        [Fact]
        [Trait("Category", "EmployeeFactory_CreateEmployee_Salary")]
        public void CreateEmployee_ConstructInternalEmployee_SalaryMustBeBetween2500And3500()
        {
            var employee = (InternalEmployee)_employeeFactory.CreateEmployee("Kevin", "Dockx");

            Assert.True(employee.Salary >= 2500 && employee.Salary <= 3500);
        }

        [Fact]
        [Trait("Category", "EmployeeFactory_CreateEmployee_Salary")]
        public void CreateEmployee_ConstructInternalEmployee_SalaryMustBeBetween2500And350_Alternative()
        {


            var employee = (InternalEmployee)_employeeFactory.CreateEmployee("Kevin", "Dockx");

            Assert.InRange(employee.Salary, 2500, 3500);
        }

        [Fact]
        [Trait("Category", "EmployeeFactory_CreateEmployee_Salary")]
        public void CreateEmployee_ConstructInternalEmployee_SalaryMustBe2500Precision()
        {

            var employee = (InternalEmployee)_employeeFactory.CreateEmployee("Kevin", "Dockx");
            employee.Salary = 2500.123m;

            Assert.Equal(2500, employee.Salary, 0);
        }

        [Fact]
        [Trait("Category", "EmployeeFactory_CreateEmployee_ReturnType")]
        public void CreateEmployee_IsExternalIsTrue_ReturnTypeMustBeExternalEmployee()
        {
            // Act
            var employee = _employeeFactory.CreateEmployee("Alex", "Bartley Nees", "Valocity", true);

            // Assert
            Assert.IsType<ExternalEmployee>(employee);

            //Assert.IsAssignableFrom<Employee>(employee);
        }

        public void Dispose()
        {
           // clean up the setup code if required
        }
    }
}
