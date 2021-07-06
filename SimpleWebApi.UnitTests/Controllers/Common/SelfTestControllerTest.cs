using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using SimpleWebApi.DAL.Repositories.Abstract.Common;
using SimpleWebApi.WebApi.Controllers;
using SimpleWebApi.WebApi.Controllers.Common;
using Xunit;

namespace SimpleWebApi.UnitTests.Controllers.Common
{
    public class SelfTestControllerTest: BaseApiControllerTest
    {
        private readonly Mock<ISelfTestRepository> _mock;
        private readonly ILogger<SelfTestController> _logger;

        public SelfTestControllerTest()
        {
            _mock = new Mock<ISelfTestRepository>();
            _logger = Mock.Of<ILogger<SelfTestController>>();
        }

        [Fact]
        public void VerifyControllerHasAuthorizeAttribute()
        {
            //Arrange
            var controller = new SelfTestController(Mapper, _logger, _mock.Object);

            VerifyControllerHasAttribute<AllowAnonymousAttribute>(controller);
        }

        [Fact]
        public async Task GetReturnsOkResult()
        {
            // Arrange
            _mock.Setup(repository => repository.Test()).Verifiable();
            var controller = new SelfTestController(Mapper, _logger, _mock.Object);

            // Act
            var result = await controller.Get();

            //Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task GetReturnsBadResult()
        {
            // Arrange
            _mock.Setup(repository => repository.Test())
                .Throws(new Exception("test"));
            var controller = new SelfTestController(Mapper, _logger, _mock.Object);

            // Act
            var result = await controller.Get();

            //Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }
    }
}