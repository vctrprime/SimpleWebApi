using System;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;
using Microsoft.Net.Http.Headers;
using Moq;
using SimpleWebApi.UnitTests.Repositories.Common;
using SimpleWebApi.WebApi.Infrastructure.Handlers;
using Xunit;

namespace SimpleWebApi.UnitTests.Handlers
{
    public class BasicAuthenticationHandlerTest
    {
        private readonly BasicAuthenticationHandler _handler;
        
        public BasicAuthenticationHandlerTest()
        {
            var options = new Mock<IOptionsMonitor<AuthenticationSchemeOptions>>();

            options
                .Setup(x => x.Get(It.IsAny<string>()))
                .Returns(new AuthenticationSchemeOptions());

            var logger = new Mock<ILogger<BasicAuthenticationHandler>>();
            var loggerFactory = new Mock<ILoggerFactory>();
            loggerFactory.Setup(x => x.CreateLogger(It.IsAny<String>())).Returns(logger.Object);

            var encoder = new Mock<UrlEncoder>();
            var clock = new Mock<ISystemClock>();

            _handler = new BasicAuthenticationHandler(options.Object, loggerFactory.Object, encoder.Object, clock.Object, 
                new AuthorizationRepositoryTest());
        }
        
        [Fact]
        public async Task HandleAuthenticateAsyncMissingHeader()
        {
            var context = new DefaultHttpContext();

            await _handler.InitializeAsync(new AuthenticationScheme(BasicAuthenticationHandler.SchemeName, null, typeof(BasicAuthenticationHandler)), context);
            var result = await _handler.AuthenticateAsync();

            Assert.False(result.Succeeded);
            Assert.Equal(BasicAuthenticationHandler.MissingHeaderMessage, result.Failure?.Message);
        }

        [Fact]
        public async Task HandleAuthenticateAsyncReturnsSuccess()
        {
            //Arrange
            var context = new DefaultHttpContext();
            var authorizationHeader = new StringValues(BasicCredentials.BasicValidAuthorizationHeaderText);
            context.Request.Headers.Add(HeaderNames.Authorization, authorizationHeader);

            //Act
            await _handler.InitializeAsync(new AuthenticationScheme(BasicAuthenticationHandler.SchemeName, null, typeof(BasicAuthenticationHandler)), context);
            var result = await _handler.AuthenticateAsync();

            //Assert
            Assert.True(result.Succeeded);
            Assert.Equal(BasicAuthenticationHandler.SchemeName, result.Ticket?.AuthenticationScheme);
            Assert.Equal(BasicCredentials.ValidUsername, result.Ticket?.Principal.Identity?.Name);
        }

        [Fact]
        public async Task HandleAuthenticateAsyncReturnsInvalidCredentialsFail()
        {
            //Arrange
            var context = new DefaultHttpContext();
            var authorizationHeader = new StringValues(BasicCredentials.BasicInvalidAuthorizationHeaderText);
            context.Request.Headers.Add(HeaderNames.Authorization, authorizationHeader);

            //Act
            await _handler.InitializeAsync(new AuthenticationScheme(BasicAuthenticationHandler.SchemeName, null, typeof(BasicAuthenticationHandler)), context);
            var result = await _handler.AuthenticateAsync();

            //Assert
            Assert.False(result.Succeeded);
            Assert.Equal(BasicAuthenticationHandler.InvalidCredentialsMessage, result.Failure?.Message);
        }
        
        [Fact]
        public async Task HandleAuthenticateAsyncReturnsInvalidHeaderFail()
        {
            //Arrange
            var context = new DefaultHttpContext();
            var authorizationHeader = new StringValues("test");
            context.Request.Headers.Add(HeaderNames.Authorization, authorizationHeader);

            //Act
            await _handler.InitializeAsync(new AuthenticationScheme(BasicAuthenticationHandler.SchemeName, null, typeof(BasicAuthenticationHandler)), context);
            var result = await _handler.AuthenticateAsync();

            //Assert
            Assert.False(result.Succeeded);
            Assert.Equal(BasicAuthenticationHandler.InvalidAuthorizationHeaderMessage, result.Failure?.Message);
        }
    }
}