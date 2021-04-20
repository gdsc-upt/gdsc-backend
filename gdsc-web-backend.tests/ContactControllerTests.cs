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
        public void Post_ReturnsWhenIdNotUnique()
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
            var added1 = controller.Post(example1).Result as CreatedResult;
            var added2 = controller.Post(example2).Result as BadRequestObjectResult;


            //verified if the values are not null
            Assert.NotNull(added1);
            Assert.NotNull(added2);

            //verify if the result are as expected
            Assert.Equal(StatusCodes.Status201Created, added1.StatusCode);
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

            var added1 = controller.Post(example1).Result as CreatedResult;
            var added2 = controller.Post(example2).Result as CreatedResult;
            
            Assert.NotNull(added1);
            Assert.NotNull(added2);
            
            Assert.Equal(StatusCodes.Status201Created, added1.StatusCode);
            Assert.Equal(StatusCodes.Status201Created, added2.StatusCode);
        }
    }
}