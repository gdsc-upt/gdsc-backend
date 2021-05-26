using System.Collections.Generic;
using System.Linq;
using FactoryBot;
using gdsc_web_backend.Database;
using GdscBackend.Controllers.v1;
using GdscBackend.Models;
using GdscBackend.Models.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Xunit;
using Xunit.Abstractions;

namespace GdscBackend.Tests
{
    public class ContactsControllerTests : TestingBase
    {
        private readonly IEnumerable<ContactModel> _testData = _getTestData();
        private ContactController _controller;
        private Repository<ContactModel> _repository;

        public ContactsControllerTests(ITestOutputHelper outputHelper) : base(outputHelper)
        {
            _repository = new Repository<ContactModel>(new TestDbContext<ContactModel>().Object);
            _controller = new ContactController(_repository);
        }

        [Fact]
        public async void Get_Should_Return_All_Contacts()
        {
            
            _repository = new Repository<ContactModel>(new TestDbContext<ContactModel>(_testData).Object);
            _controller = new ContactController(_repository);

            // Act
            var actionResult = await _controller.Get();
            var result = actionResult.Result as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            var items = Assert.IsAssignableFrom<IEnumerable<ContactModel>>(result.Value);
            WriteLine(items); // This will print items to console as a json object
            Assert.Equal(_testData, items);
        }

        [Fact]
        public async void Post_ReturnsCreatedObject()
        {
            // Arrange
            var contact1 = new ContactModel
            {
                Name = Faker.Name.FullName(),
                Email = Faker.Lorem.Words(3).ToString(),
                Subject = Faker.Lorem.Words(1).ToString(),
                Text = Faker.Lorem.Words(3).ToString()
            };
            var contact2 = new ContactModel
            {
                Name = Faker.Name.FullName(),
                Email = Faker.Lorem.Words(3).ToString(),
                Subject = Faker.Lorem.Words(1).ToString(),
                Text = Faker.Lorem.Words(3).ToString()
            };

            // Act
            var added1 = await _controller.Post(contact1);
            var added2 = await _controller.Post(contact2);

            var result1 = added1.Result as CreatedAtActionResult;
            var result2 = added2.Result as CreatedAtActionResult;

            // Assert
            Assert.NotNull(result1);
            Assert.NotNull(result2);
            var entity1 = result1.Value as ContactModel;
            var entity2 = result2.Value as ContactModel;

            Assert.NotNull(entity1);
            Assert.NotNull(entity1.Id);
            Assert.Equal(StatusCodes.Status201Created, result1.StatusCode);
            Assert.Equal(contact1, entity1);

            Assert.NotNull(entity2);
            Assert.NotNull(entity2.Id);
            Assert.Equal(StatusCodes.Status201Created, result2.StatusCode);
            Assert.Equal(contact2, entity2);
        }

        [Fact]
        public async void Delete_Should_Delete_Contact_By_ID()
        {
            //
            _repository = new Repository<ContactModel>(new TestDbContext<ContactModel>(_testData).Object);
            _controller = new ContactController(_repository);
            
            // Act
            var deleted = await _controller.Delete(_testData.First().Id);
            var result = deleted.Result as OkObjectResult;
            
            // Assert
            var entity = result.Value as ContactModel;
            Assert.NotNull(result);
            Assert.Equal(_testData.First(), entity);

        }

        [Fact]

        public async void Delete_Multiple_Contacts()
        {
            //
            _repository = new Repository<ContactModel>(new TestDbContext<ContactModel>(_testData).Object);
            _controller = new ContactController(_repository);
            
            // Act
            string[] listOfIds = {_testData.First().Id, _testData.ElementAt(1).Id};
            List<ContactModel> listOfContacts = new() {_testData.First(), _testData.ElementAt(1)}; 
            var deleted = await _controller.Delete(listOfIds);
            var result = deleted.Result as OkObjectResult;
            
            // Assert
            var entity = result.Value as IEnumerable<ContactModel>;
            Assert.NotNull(result);
            Assert.Equal(listOfContacts, entity);
        }

        public override void Dispose()
        {
            base.Dispose();
            _controller = null;
            _repository = null;
        }
        
        private static IEnumerable<ContactModel> _getTestData()
        {
            Bot.Define(x => new ContactModel
            {
                Id = x.Strings.Guid(),
                Name = x.Names.FullName(),
                Email = x.Strings.Any(),
                Subject= x.Strings.Any(),
                Text = x.Strings.Any(),
                Created = x.Dates.Any(),
                Updated = x.Dates.Any()
            });

            return Bot.BuildSequence<ContactModel>().Take(10).ToList();
        }
    }
}
