using System.Collections.Generic;
using System.Linq;
using FactoryBot;
using gdsc_web_backend.Database;
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
        private static readonly IEnumerable<MemberModel> TestData = _getTestData();
        private MembersController _controller;
        private Repository<MemberModel> _repository;

        public MembersControllerTests(ITestOutputHelper outputHelper) : base(outputHelper)
        {
            _repository = new Repository<MemberModel>(new TestDbContext<MemberModel>().Object);
            _controller = new MembersController(_repository);
        }

        [Fact]
        public async void Get_Should_Return_All_Members()
        {

            _repository = new Repository<MemberModel>(new TestDbContext<MemberModel>(TestData).Object);
            _controller = new MembersController(_repository);

            // Act
            var actionResult = await _controller.Get();
            var result = actionResult.Result as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            var items = Assert.IsAssignableFrom<IEnumerable<MemberModel>>(result.Value);
            WriteLine(items); // This will print items to console as a json object
            Assert.Equal(TestData, items);
        }

        [Fact]
        public async void Post_ReturnsCreatedObject()
        {
            // Arrange
            var member1 = new MemberModel
            {
                Name = Faker.Name.FullName(),
                Email = Faker.Lorem.Words(3).ToString(),
                TeamId = Faker.Lorem.Words(1).ToString()
            };
            var member2 = new MemberModel
            {
                Name = Faker.Name.FullName(),
                Email = Faker.Lorem.Words(3).ToString(),
                TeamId = Faker.Lorem.Words(1).ToString()
            };

            // Act
            var added1 = await _controller.Post(member1);
            var added2 = await _controller.Post(member2);

            var result1 = added1.Result as CreatedAtActionResult;
            var result2 = added2.Result as CreatedAtActionResult;

            // Assert
            Assert.NotNull(result1);
            Assert.NotNull(result2);
            var entity1 = result1.Value as MemberModel;
            var entity2 = result2.Value as MemberModel;

            Assert.NotNull(entity1);
            Assert.NotNull(entity1.Id);
            Assert.Equal(StatusCodes.Status201Created, result1.StatusCode);
            Assert.Equal(member1, entity1);

            Assert.NotNull(entity2);
            Assert.NotNull(entity2.Id);
            Assert.Equal(StatusCodes.Status201Created, result2.StatusCode);
            Assert.Equal(member2, entity2);
        }

        [Fact]

        public async void Get_Returns_Member_by_ID()
        {
            // Arrange
            _repository = new Repository<MemberModel>(new TestDbContext<MemberModel>(TestData).Object);
            _controller = new MembersController(_repository);

            //Act

            var anElementById = TestData.First();
            var actionResult = await _controller.Get(anElementById.Id);
            var result = actionResult.Result as OkObjectResult;

            //Assert

            Assert.NotNull(result);
            var entity = result.Value as MemberModel;
            Assert.Equal(anElementById, entity);
        }

        [Fact]
        public async void Delete_Should_Delete_Member_By_ID()
        {
            //
            _repository = new Repository<MemberModel>(new TestDbContext<MemberModel>(TestData).Object);
            _controller = new MembersController(_repository);

            // Act
            var deleted = await _controller.Delete(TestData.First().Id);
            var result = deleted.Result as OkObjectResult;

            // Assert
            var entity = result.Value as MemberModel;
            Assert.NotNull(result);
            Assert.Equal(TestData.First(), entity);

        }

        public override void Dispose()
        {
            base.Dispose();
            _controller = null;
            _repository = null;
        }

        private static IEnumerable<MemberModel> _getTestData()
        {
            Bot.Define(x => new MemberModel
            {
                Id = x.Strings.Guid(),
                Name = x.Names.FullName(),
                Email = x.Strings.Any(),
                TeamId = x.Strings.Any(),
                Created = x.Dates.Any(),
                Updated = x.Dates.Any()
            });

            return Bot.BuildSequence<MemberModel>().Take(10).ToList();
        }
    }
}
