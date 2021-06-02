using System;
using System.Collections.Generic;
using System.Linq;
using FactoryBot;
using GdscBackend.Controllers.v1;
using GdscBackend.Database;
using GdscBackend.Models;
using GdscBackend.Models.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Xunit;
using Xunit.Abstractions;

namespace GdscBackend.Tests
{
    public class MenuItemsControllerTests : TestingBase
    {
        private readonly IEnumerable<MenuItemModel> _testData = _getTestData();

        public MenuItemsControllerTests(ITestOutputHelper outputHelper) : base(outputHelper)
        {
        }

        [Fact]
        public async void Post_ReturnsCreatedObject()
        {
            var repository = new Repository<MenuItemModel>(new TestDbContext<MenuItemModel>(_testData).Object);
            var controller = new MenuItemsController(repository);
        }

        [Fact]
        public async void GetById_ReturnsOkObjectResult()
        {
            var repository = new Repository<MenuItemModel>(new TestDbContext<MenuItemModel>(_testData).Object);
            var controller = new MenuItemsController(repository);
        }

        [Fact]
        public async void Get_ReturnsAllExamples()
        {
            var repository = new Repository<MenuItemModel>(new TestDbContext<MenuItemModel>(_testData).Object);
            var controller = new MenuItemsController(repository);

            var actionResult = await controller.Get();
            var returnedResult = actionResult.Result as OkObjectResult;
            var items = Assert.IsAssignableFrom<IEnumerable<MenuItemModel>>(returnedResult.Value);

            Assert.NotNull(returnedResult);
            Assert.Equal(StatusCodes.Status200OK, returnedResult.StatusCode);
            Assert.Equal(_testData.ToList(), items.ToList());
        }

        [Fact]
        public async void Delete_Returns404NotFoundResult()
        {
            var repository = new Repository<MenuItemModel>(new TestDbContext<MenuItemModel>(_testData).Object);
            var controller = new MenuItemsController(repository);
        }

        [Fact]
        public async void Update_ReturnsCreatedAtAction()
        {
            var repository = new Repository<MenuItemModel>(new TestDbContext<MenuItemModel>(_testData).Object);
            var controller = new MenuItemsController(repository);
        }

        private static IEnumerable<MenuItemModel> _getTestData()
        {
            Bot.Define(x => new MenuItemModel
            {
                Id = x.Strings.Guid(),
                Name = x.Names.FirstName(),
                Type = x.Enums.Any<MenuItemTypeEnum>(),
                Link = Faker.Lorem.Words(3).ToString()
            });

            return Bot.BuildSequence<MenuItemModel>().Take(10).ToList();
        }
    }
}