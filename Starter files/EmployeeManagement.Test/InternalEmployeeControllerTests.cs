using EmployeeManagement.Business;
using Moq;
using EmployeeManagement.DataAccess.Entities;
using EmployeeManagement.Controllers;
using Microsoft.AspNetCore.Mvc;
using EmployeeManagement.Models;
using AutoMapper;

namespace EmployeeManagement.Test
{
    public class InternalEmployeeControllerTests
    {
        private readonly InternalEmployeesController _internalEmployeesController;

        private InternalEmployee _firstEmployee;
        public InternalEmployeeControllerTests()
        {

            _firstEmployee = new InternalEmployee("Megan", "Jones", 2, 3000, false, 2)
            {
                Id = Guid.Parse("651aef9b-20dc-4639-ae2e-b363a57db587"),
                SuggestedBonus = 400
            };
            var employeeServiceMock = new Mock<IEmployeeService>();

            employeeServiceMock.Setup(m => m.FetchInternalEmployeesAsync())
            .ReturnsAsync(new List<InternalEmployee>()
            {
                _firstEmployee,
                new InternalEmployee("Jaimy", "Johnson", 3, 3400, true, 1),
                new InternalEmployee("Anne", "Adams", 3, 4000, false, 3),
            });

            //var mapperMock = new Mock<IMapper>();
            //mapperMock.Setup(m => m.Map<InternalEmployee, Models.InternalEmployeeDto>(
            //    It.IsAny<InternalEmployee>()))
            //    .Returns(new Models.InternalEmployeeDto());

            var mapperConfig = new MapperConfiguration(
                cfg => cfg.AddProfile<MapperProfiles.EmployeeProfile>());
            var mapper = new Mapper(mapperConfig);
            _internalEmployeesController = new InternalEmployeesController(employeeServiceMock.Object, mapper);
        }

        [Fact]
        public async Task GetInternalEmployees_GetAction_MustReturnOkObjectResult()
        {
            // Arrange
   

            // Act

            var result = await _internalEmployeesController.GetInternalEmployees();


            // Assert

            Assert.IsType<ActionResult<IEnumerable<InternalEmployeeDto>>>(result);
            Assert.IsType<OkObjectResult>(result.Result);

        }

        [Fact]
        public async Task GetInternalEmployees_GetAction_MustReturnIEnumerableOfInternalEmployeeDtoAsModelType()
        {
            // Arrange

            // Act

            var result = await _internalEmployeesController.GetInternalEmployees();

            // Assert

            var actionResult = Assert.IsType<ActionResult<IEnumerable<InternalEmployeeDto>>>(result);
            Assert.IsAssignableFrom<IEnumerable<InternalEmployeeDto>>(((OkObjectResult)actionResult.Result).Value);
        }

        [Fact]
        public async Task GetInteralEmployees_GetAction_ReturnsOKObjectResultWithCorrectAmountOfInternalEmployees()
        {
            // Arrange
            // Act
            var result = await _internalEmployeesController.GetInternalEmployees();
            // Assert
            var actionResult = Assert.IsType<ActionResult<IEnumerable<InternalEmployeeDto>>>(result);
            var okObjectResult = Assert.IsType<OkObjectResult>(actionResult.Result);
            var internalEmployees = Assert.IsAssignableFrom<IEnumerable<InternalEmployeeDto>>(okObjectResult.Value);
            Assert.Equal(3, internalEmployees.Count());
            var firstEmployee = internalEmployees.First();
            Assert.Equal("Megan", firstEmployee.FirstName);
            Assert.Equal("Jones", firstEmployee.LastName);
            Assert.Equal(3000, firstEmployee.Salary);

        }

    }
}
