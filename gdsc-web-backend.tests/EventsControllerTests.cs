using System.Collections.Generic;
using gdsc_web_backend.Controllers;
using gdsc_web_backend.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Xunit;
using Xunit.Abstractions;

namespace gdsc_web_backend.tests
{
    public class EventsControllerTest : TestingBase
    {
        public EventsControllerTest(ITestOutputHelper outputHelper) : base(outputHelper)
        {
        }

        [Fact]
        public void Post_ReturnsCreatedObject()
        {
            var controller = new EventsController();
            var example1 = new EventModel
            {
                Id = "1",
                Title = "Santa Claus is coming to town!",
                Description = "You better watch out, you better not cry.",
                Image = ""
            };
            var example2 = new EventModel
            {
                Id = "2",
                Title = "AC DC Concert",
                Description = "On a highway to hell.",
                Image = ""
            };

            // Act
            var added1 = controller.Post(example1).Result as CreatedResult;
            var added2 = controller.Post(example2).Result as CreatedResult;

            // Verify if the added values are not null and if they were transmitted well
            Assert.NotNull(added1);
            Assert.Equal(StatusCodes.Status201Created, added1.StatusCode);
            Assert.Equal(example1, added1.Value as EventModel);

            Assert.NotNull(added2);
            Assert.Equal(StatusCodes.Status201Created, added2.StatusCode);
            Assert.Equal(example2, added2.Value as EventModel);
        }

        [Fact]
        public void Post_ReturnsError_WhenIdNotUnique()
        {
            // Setting test's things to work with
            var controller = new EventsController();
            var example1 = new EventModel
            {
                Id = "1",
                Title = "Santa Claus is coming to town!",
                Description = "You better watch out, you better not cry.",
                Image = ""
            };
            var example2 = new EventModel
            {
                Id = "1",
                Title = "AC DC Concert",
                Description = "On a highway to hell.",
                Image = ""
            };

            // Act
            var added1 = controller.Post(example1).Result as CreatedResult;
            var added2 = controller.Post(example2).Result as BadRequestObjectResult;

            // Assert
            Assert.NotNull(added1);
            Assert.Equal(StatusCodes.Status201Created, added1.StatusCode);
            Assert.Equal(example1, added1.Value as EventModel);

            Assert.NotNull(added2);
            Assert.Equal(StatusCodes.Status400BadRequest, added2.StatusCode);
        }

        [Fact]
        public void Get_ReturnsAllExamples()
        {
            // Arrange
            var controller = new EventsController();
            var examples = new List<EventModel>
            {
                new()
                {
                    Id = "1",
                    Title = "Santa Claus is coming to town!",
                    Description = "You better watch out, you better not cry.",
                    Image = ""
                },
                new()
                {
                    Id = "2",
                    Title = "AC DC Concert",
                    Description = "On a highway to hell.",
                    Image = ""
                },
            };
            controller.Post(examples[0]);
            controller.Post(examples[1]);

            // Act
            var result = controller.Get().Result as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            var items = Assert.IsAssignableFrom<List<EventModel>>(result.Value);
            WriteLine(items); // This will print items to console as a json object
            Assert.Equal(examples, items);
        }
    }
}