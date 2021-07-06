using System;
using System.Linq;
using AutoMapper;
using SimpleWebApi.Profiles.BreakingBad;
using SimpleWebApi.WebApi.Controllers;
using Xunit;

namespace SimpleWebApi.UnitTests.Controllers
{
    public class BaseApiControllerTest
    {
        protected readonly IMapper Mapper;

        protected BaseApiControllerTest()
        {
            var config = new MapperConfiguration(opts =>
            {
                opts.AddProfile<QuoteProfile>();
            });
            Mapper = config.CreateMapper();

        }

        protected void VerifyControllerHasAttribute<T>(BaseApiController controller) where T : Attribute
        {
            
            //Act
            var type = controller.GetType();
            var attributes = type.GetCustomAttributes(typeof(T), true);

            //Arrange
            Assert.True(attributes.Any());
        }
    }
}