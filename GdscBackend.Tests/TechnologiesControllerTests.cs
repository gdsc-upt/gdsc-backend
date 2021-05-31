using System.Collections.Generic;
using System.Linq;
using FactoryBot;
using GdscBackend.Controllers.v1;
using GdscBackend.Database;
using GdscBackend.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Xunit;
using Xunit.Abstractions;

namespace GdscBackend.Tests
{
    public class TechnologiesControllerTests : TestingBase
    {
        private readonly IEnumerable<TechnologyModel> _testData = _getTestData();
        
        public TechnologiesControllerTests(ITestOutputHelper outputHelper) : base(outputHelper)
        {
        }

        [Fact]
        public async void Post_ReturnsCreatedObject()
        {
            var repository = new Repository<TechnologyModel>(new TestDbContext<TechnologyModel>().Object);
            var controller = new TechnologiesController(repository);
            var example1 = new TechnologyModel
            {
                Name = Faker.Lorem.Words(3).ToString(),
                Description = Faker.Lorem.Sentence(7),
                Icon = Faker.Lorem.Words(1).ToString()
            };
            var example2 = new TechnologyModel
            {
                Name = Faker.Lorem.Words(3).ToString(),
                Description = Faker.Lorem.Sentence(7),
                Icon = Faker.Lorem.Words(1).ToString()
            };

            var added1 = await controller.Post(example1);
            var added2 = await controller.Post(example2);

            var result1 = added1.Result as CreatedAtActionResult;
            var result2 = added2.Result as CreatedAtActionResult;
            
            Assert.NotNull(result1);
            Assert.NotNull(result2);
            var entity1 = result1.Value as TechnologyModel;
            var entity2 = result2.Value as TechnologyModel;
            
            Assert.NotNull(entity1);
            Assert.NotNull(entity1.Id);
            Assert.Equal(StatusCodes.Status201Created, result1.StatusCode);
            Assert.Equal(example1, entity1);   
            
            Assert.NotNull(entity2);
            Assert.NotNull(entity2.Id);
            Assert.Equal(StatusCodes.Status201Created, result2.StatusCode);
            Assert.Equal(example2, entity2);   
        }

        [Fact]
        public async void Get_ReturnsAllTechnologies()
        {
            var repository = new Repository<TechnologyModel>(new TestDbContext<TechnologyModel>(_testData).Object);
            var controller = new TechnologiesController(repository);

            var actionResult = await controller.Get();
            var result = actionResult.Result as OkObjectResult;
            
            Assert.NotNull(result);
            var items = Assert.IsAssignableFrom<IEnumerable<TechnologyModel>>(result.Value);
            WriteLine(items);
            Assert.Equal(_testData, items);
        }

        [Fact]
        public async void Delete_ReturnsOkResult()
        {
            var repository = new Repository<TechnologyModel>(new TestDbContext<TechnologyModel>(_testData).Object);
            var controller = new TechnologiesController(repository);

            var actionResult = await controller.Delete(_testData.First().Id);
            var result = actionResult.Result as OkObjectResult;

            var actionGetResult = (await controller.Get()).Result as OkObjectResult;
            Assert.NotNull(actionGetResult);
            var countResult = Assert.IsAssignableFrom<IEnumerable<TechnologyModel>>(actionGetResult.Value);

            Assert.NotNull(result);
            Assert.Equal(countResult.Count(), repository.DbSet.Count());
            Assert.Equal(_testData.Count() - 1, repository.DbSet.Count());
            Assert.Equal(_testData.First(), result.Value);
        }

        private static IEnumerable<TechnologyModel> _getTestData()
        {
            Bot.Define(x => new TechnologyModel
            {
                Id = x.Strings.Guid(),
                Name = x.Names.FullName(),
                Description = x.Strings.Any(),
                Icon = x.Strings.Any(),
                Created = x.Dates.Any(),
                Updated = x.Dates.Any()
            });

            return Bot.BuildSequence<TechnologyModel>().Take(10).ToList();
        }
    }
}
