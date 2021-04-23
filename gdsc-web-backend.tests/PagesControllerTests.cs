using gdsc_web_backend.Controllers;
using gdsc_web_backend.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Xunit;
using Xunit.Abstractions;

namespace gdsc_web_backend.tests
{
    public class PagesControllerTests : TestingBase
    {
        public PagesControllerTests(ITestOutputHelper outputHelper) : base(outputHelper)
        {
        }

        [Fact]
        public void Post_ReturnsErrors_WhenIdNotUnique()
        {
            var controller = new PagesController();
            var example1 = new PageModel
            {
                Id = "1",
                Title = "Home",
                Body = "In progress",
                isPublished = false,
                Slug = "home-page",
                ShortDescription = "Shows a description about the site",
                Image = "Smth 'bout Google",
            };

            var example2 = new PageModel
            {
                Id = "1",
                Title = "Contact",
                Body = "Still in progress",
                isPublished = false,
                Slug = "contact",
                ShortDescription = "Some data about us and how you could get in touch",
                Image = "Smth 'bout us",
            };

            //Act
            var added1 = controller.Post(example1).Result as OkObjectResult;
            var added2 = controller.Post(example2).Result as BadRequestObjectResult;

            Assert.NotNull(added1);
            Assert.Equal(StatusCodes.Status200OK, added1.StatusCode);
            Assert.Equal(example1, added1.Value as PageModel);

            Assert.NotNull(added2);
            Assert.Equal(StatusCodes.Status400BadRequest, added2.StatusCode);
            var enumerable = new ErrorViewModel().Message;
            if (enumerable != null) Assert.Equal("An object with the same ID already exists", enumerable);
        }

        [Fact]
        public void Post_ReturnsCreatedObject()
        {
            var controller = new PagesController();
            var example1 = new PageModel
            {
                Id = "1",
                Title = "Home",
                Body = "In progress",
                isPublished = false,
                Slug = "home-page",
                ShortDescription = "Shows a description about the site",
                Image = "Smth 'bout Google",
            };
            var example2 = new PageModel
            {
                Id = "2",
                Title = "Contact",
                Body = "Still in progress",
                isPublished = false,
                Slug = "contact",
                ShortDescription = "Some data about us and how you could get in touch",
                Image = "Smth 'bout us",
            };

            var added1 = controller.Post(example1).Result as OkObjectResult;
            var added2 = controller.Post(example2).Result as OkObjectResult;

            Assert.NotNull(added1);
            Assert.Equal(StatusCodes.Status200OK, added1.StatusCode);
            Assert.Equal(example1, added1.Value as PageModel);

            Assert.NotNull(added2);
            Assert.Equal(StatusCodes.Status200OK, added2.StatusCode);
            Assert.Equal(example2, added2.Value as PageModel);
        }
    }
}