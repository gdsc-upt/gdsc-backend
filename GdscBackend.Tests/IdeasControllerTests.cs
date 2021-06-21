using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using FactoryBot;
using Faker;
using GdscBackend.Controllers.v1;
using GdscBackend.Database;
using GdscBackend.Models;
using GdscBackend.RequestModels;
using GdscBackend.Tests.Mocks;
using GdscBackend.Utils;
using GdscBackend.Utils.Mappers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Xunit;
using Xunit.Abstractions;

namespace GdscBackend.Tests
{
    public class IdeasControllerTests : TestingBase
    {
        private readonly IEnumerable<IdeaModel> _testData = _getTestData();
        private readonly IEmailSender _sender;
        private readonly IMapper _mapper;

        public IdeasControllerTests(ITestOutputHelper outputHelper) : base(outputHelper)
        {
            _sender = new TestEmailSender(OutputHelper);
            var mapconfig = new MapperConfiguration(cfg => cfg.AddProfile(new MappingProfiles()));
            _mapper = mapconfig.CreateMapper();
        }

        [Fact]
        public async void Post_ReturnsCreatedObject()
        {
            var repository = new Repository<IdeaModel>(new TestDbContext<IdeaModel>().Object);
            var controller = new IdeasController(repository, _sender, _mapper);
            var example1 = new IdeaRequest
            {
                Name = Lorem.Words(3).ToString(),
                Email = Lorem.Words(4) + "@" + Lorem.Words(3) + ".com",
                Branch = Lorem.Words(2).ToString(),
                Year = RandomNumber.Next(),
                Description = Lorem.Sentence(7),
            };
            var example2 = new IdeaRequest
            {
                Name = Lorem.Words(3).ToString(),
                Email = Lorem.Words(4) + "@" + Lorem.Words(3) + ".com",
                Branch = Lorem.Words(2).ToString(),
                Year = RandomNumber.Next(),
                Description = Lorem.Sentence(7),
            };

            var added1 = await controller.Post(example1);
            var added2 = await controller.Post(example2);

            var result1 = added1.Result as CreatedResult;
            var result2 = added2.Result as CreatedResult;

            Assert.NotNull(result1);
            Assert.NotNull(result2);
            var entity1 = result1.Value as IdeaModel;
            var entity2 = result2.Value as IdeaModel;

            Assert.NotNull(entity1);
            Assert.Equal(StatusCodes.Status201Created, result1.StatusCode);
            Assert.Equal(example1.Branch, entity1.Branch);
            Assert.Equal(example1.Description, entity1.Description);
            Assert.Equal(example1.Email, entity1.Email);
            Assert.Equal(example1.Name, entity1.Name);
            Assert.Equal(example1.Year, entity1.Year);

            Assert.NotNull(entity2);
            Assert.Equal(StatusCodes.Status201Created, result2.StatusCode);
            Assert.Equal(example2.Branch, entity2.Branch);
            Assert.Equal(example2.Description, entity2.Description);
            Assert.Equal(example2.Email, entity2.Email);
            Assert.Equal(example2.Name, entity2.Name);
            Assert.Equal(example2.Year, entity2.Year);
        }

        [Fact]
        public async void Get_ReturnsAllIdeas()
        {
            var repository = new Repository<IdeaModel>(new TestDbContext<IdeaModel>(_testData).Object);
            var controller = new IdeasController(repository, _sender, _mapper);

            var actionResult = await controller.Get();
            var result = actionResult.Result as OkObjectResult;

            Assert.NotNull(result);
            var items = Assert.IsAssignableFrom<IEnumerable<IdeaModel>>(result.Value);
            WriteLine(items);
            Assert.Equal(_testData, items);
        }

        [Fact]
        public async void Get_ReturnsIdeaByID()
        {
            var repository = new Repository<IdeaModel>(new TestDbContext<IdeaModel>(_testData).Object);
            var controller = new IdeasController(repository, _sender, _mapper);

            var anElementById = _testData.First();
            var actionResult = await controller.Get(anElementById.Id);
            var result = actionResult.Result as OkObjectResult;

            Assert.NotNull(result);
            var entity = result.Value as IdeaModel;
            WriteLine(entity);
            Assert.Equal(anElementById, entity);
        }

        private static IEnumerable<IdeaModel> _getTestData()
        {
            Bot.Define(x => new IdeaModel
            {
                Id = x.Strings.Guid(),
                Name = x.Names.FullName(),
                Email = x.Strings.Any(),
                Branch = x.Strings.Any(),
                Year = x.Integer.Any(),
                Description = x.Strings.Any(),
                Created = x.Dates.Any(),
                Updated = x.Dates.Any()
            });

            return Bot.BuildSequence<IdeaModel>().Take(10).ToList();
        }
    }
}
