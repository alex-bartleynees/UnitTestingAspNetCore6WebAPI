using AutoMapper;
using EmployeeManagement.Controllers;
using EmployeeManagement.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Test
{
    public class StatisticsControllerTests
    {
        [Fact]
        public void GetStatistics_InputFromHttpConnectionFeature_MustReturnInputtedInput()
        {
            // Arrange
            var localIpAddress = System.Net.IPAddress.Parse("111.111.111.111");
            var localPort = 5000;
            var remoteIpAddress = System.Net.IPAddress.Parse("222.222.222.222");
            var remotePort = 8080;

            var featureCollectionMock = new Mock<IFeatureCollection>();
            featureCollectionMock.Setup(e => e.Get<IHttpConnectionFeature>())
            .Returns(new HttpConnectionFeature
            {
                LocalIpAddress = localIpAddress,
                LocalPort = localPort,
                RemoteIpAddress = remoteIpAddress,
                RemotePort = remotePort
            });

            var httpContextMock = new Mock<HttpContext>();
            httpContextMock.Setup(e => e.Features).Returns(featureCollectionMock.Object);

            var mapperConfig = new MapperConfiguration(
                cfg => cfg.AddProfile<MapperProfiles.StatisticsProfile>());
            var mapper = new Mapper(mapperConfig);

            var statisticsController = new StatisticsController(mapper)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = httpContextMock.Object
                }
            };

            // Act
            var result = statisticsController.GetStatistics();

            // Assert
            var actionResult = Assert.IsType<ActionResult<StatisticsDto>>(result);
            var okObjectResult = Assert.IsType<OkObjectResult>(actionResult.Result);
            var statisticsDto = Assert.IsType<StatisticsDto>(okObjectResult.Value);
        }
    }
} 
