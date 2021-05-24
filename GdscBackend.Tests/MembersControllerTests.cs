using System.Collections.Generic;
using GdscBackend.Controllers.v1;
using GdscBackend.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Xunit;
using Xunit.Abstractions;

namespace GdscBackend.Tests
{
    public class MembersControllerTests : TestingBase
    {
        private readonly MembersController _controller;

        public MembersControllerTests(ITestOutputHelper outputHelper) : base(outputHelper)
        {
            _controller = new MembersController();
        }

        [Fact]
        public void Get_Should_Return_All_Members()
        {
            // Arrange
            var mockedList = new List<MemberModel>
            {
                new()
                {
                    Id = "1",
                    Name = "Gigel",
                    Email = "yahoo@gigel.com",
                    TeamId = "1"
                },
                new()
                {
                    Id = "2",
                    Name = "Dorel",
                    Email = "dorel@gigel.com",
                    TeamId = "2"
                }
            };

            _controller.Post(mockedList[0]);
            _controller.Post(mockedList[1]);

            // Act
            var result = _controller.Get().Result as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            var items = Assert.IsAssignableFrom<IEnumerable<MemberModel>>(result.Value);
            WriteLine(items); // This will print items to console as a json object
            Assert.Equal(mockedList, items);
        }

        [Fact]
        public void Post_ReturnsCreatedObject()
        {
            // Arrange
            var member1 = new MemberModel
            {
                Id = "1",
                Name = "Gigel",
                Email = "yahoo@gigel.com",
                TeamId = "1"
            };
            var member2 = new MemberModel
            {
                Id = "2",
                Name = "Dorel",
                Email = "dorel@gigel.com",
                TeamId = "2"
            };

            // Act
            var added1 = _controller.Post(member1).Result as CreatedResult;
            var added2 = _controller.Post(member2).Result as CreatedResult;

            // Assert
            Assert.NotNull(added1);
            Assert.Equal(StatusCodes.Status201Created, added1.StatusCode);
            Assert.Equal(member1, added1.Value as MemberModel);

            Assert.NotNull(added2);
            Assert.Equal(StatusCodes.Status201Created, added2.StatusCode);
            Assert.Equal(member2, added2.Value as MemberModel);
        }

        public override void Dispose()
        {
            base.Dispose();
            MembersController.MockMembers.Clear();
        }
    }
}