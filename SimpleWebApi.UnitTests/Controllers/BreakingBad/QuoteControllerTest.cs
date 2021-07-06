using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using SimpleWebApi.DAL.Repositories.Abstract.BreakingBad;
using SimpleWebApi.UnitTests.Repositories.BreakingBad;
using SimpleWebApi.WebApi.Controllers.BreakingBad;
using Xunit;

namespace SimpleWebApi.UnitTests.Controllers.BreakingBad
{
    public class QuoteControllerTest : BaseApiControllerTest
    {
        private readonly Mock<IQuoteRepository> _mock;
        private readonly IQuoteRepository _repository;
        private readonly ILogger<QuoteController> _logger;

        public QuoteControllerTest()
        {
            _mock = new Mock<IQuoteRepository>();
            _repository = new QuoteRepositoryTest();
            _logger = Mock.Of<ILogger<QuoteController>>();
        }
        
        [Fact]
        public void VerifyControllerHasAuthorizeAttribute()
        {
            //Arrange
            var controller = new QuoteController(Mapper, _logger, _mock.Object);

            VerifyControllerHasAttribute<AuthorizeAttribute>(controller);
        }

        #region get
        [Fact]
        public async Task GetAllReturnsOkResult()
        {
            // Arrange
            _mock.Setup(repository => repository.Get())
                .ReturnsAsync(await _repository.Get());
            var controller = new QuoteController(Mapper, _logger, _mock.Object);

            // Act
            var result = await controller.Get();

            //Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task GetAllReturnsBadResult()
        {
            // Arrange
            _mock.Setup(repository => repository.Get())
                .Throws(new Exception("test"));
            var controller = new QuoteController(Mapper, _logger, _mock.Object);

            // Act
            var result = await controller.Get();

            //Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }
        
        [Fact]
        public async Task GetReturnsOkResult()
        {
            // Arrange
            int quoteId = 1;
            _mock.Setup(repository => repository.Get(quoteId))
                .ReturnsAsync(await _repository.Get(quoteId));
            var controller = new QuoteController(Mapper, _logger, _mock.Object);

            // Act
            var result = await controller.Get(quoteId);

            //Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task GetReturnsBadResult()
        {
            // Arrange
            int quoteId = 1;
            _mock.Setup(repository => repository.Get(quoteId))
                .Throws(new Exception("test"));
            var controller = new QuoteController(Mapper, _logger, _mock.Object);

            // Act
            var result = await controller.Get(quoteId);

            //Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }
        
        [Fact]
        public async Task GetReturnsNotFoundResult()
        {
            // Arrange
            int quoteId = 3;
            _mock.Setup(repository => repository.Get(quoteId))
                .ReturnsAsync(await _repository.Get(quoteId));
            var controller = new QuoteController(Mapper, _logger, _mock.Object);

            // Act
            var result = await controller.Get(quoteId);

            //Assert
            Assert.IsType<NotFoundResult>(result);
        }

        #endregion
    }
}