using gdsc_web_backend.Controllers;
using gdsc_web_backend.Models;
using Microsoft.AspNetCore.Http;
using Xunit;
using Xunit.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace gdsc_web_backend.tests
{
    public class ContactControllerTests : TestingBase
    {
        public ContactControllerTests(ITestOutputHelper outputHelper) : base(outputHelper)
        {
        }

        [Fact]
        public void Post_ReturnsErrors_WhenIdNotUnique()
        {
            //arrange
            var controller = new ContactController();
            var example1 = new ContactModel
            {
                Id = "1",
                Name = "Bla",
                Email = "bla@email",
                Subject = "bla subject",
                Text = "bla text"
            };
            var example2 = new ContactModel
            {
                Id = "1",
                Name = "Bsagsag",
                Email = "bla@emgsagasail",
                Subject = "bla subjeasgsact",
                Text = "bla texgsagsat"
            };
            //Act
            var added1 = controller.Post(example1).Result as OkObjectResult;
            var added2 = controller.Post(example2).Result as BadRequestObjectResult;


            //Assert
            Assert.NotNull(added1);
            Assert.NotNull(added2);


            Assert.Equal(StatusCodes.Status200OK, added1.StatusCode);
            Assert.Equal(example1, added1.Value as ContactModel);
            Assert.Equal(StatusCodes.Status400BadRequest, added2.StatusCode);
        }

        [Fact]
        public void Post_ReturnsCreateObject()
        {
            var controller = new ContactController();
            
            var example1 = new ContactModel
            {
                Id = "1",
                Name = "Bla",
                Email = "bla@email",
                Subject = "bla subject",
                Text = "bla text"
            };
            var example2 = new ContactModel
            {
                Id = "2",
                Name = "Bsagsag",
                Email = "bla@emgsagasail",
                Subject = "bla subjeasgsact",
                Text = "bla texgsagsat"
            };
            //Act
            var added1 = controller.Post(example1).Result as OkObjectResult;
            var added2 = controller.Post(example2).Result as OkObjectResult;
            
            //Assert
            Assert.NotNull(added1);
            Assert.NotNull(added2);
            
            Assert.Equal(StatusCodes.Status200OK, added1.StatusCode);
            Assert.Equal(example1, added1.Value as ContactModel);

            Assert.Equal(StatusCodes.Status200OK, added2.StatusCode);
            Assert.Equal(example1, added1.Value as ContactModel);

        }
    }
}