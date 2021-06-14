using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using AutoMapper.Configuration;
using FactoryBot;
using Faker;
using GdscBackend.Controllers.v1;
using GdscBackend.Database;
using GdscBackend.Models;
using GdscBackend.RequestModels;
using GdscBackend.Utils.Mappers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Xunit;
using Xunit.Abstractions;

namespace GdscBackend.Tests
{
    public class ContactsControllerTests : TestingBase
    {
        private readonly IMapper _mapper;
        private static readonly IEnumerable<ContactModel> TestData = _getTestData();
        private ContactController _controller;
        private Repository<ContactModel> _repository;

        public ContactsControllerTests(ITestOutputHelper outputHelper) : base(outputHelper)
        {
            _repository = new Repository<ContactModel>(new TestDbContext<ContactModel>().Object);
            _controller = new ContactController(_repository, _mapper);
            var mapconfig = new MapperConfiguration(cfg => cfg.AddProfile(new MappingProfiles()));
            _mapper = mapconfig.CreateMapper();
        }

        [Fact]
        public async void Get_Should_Return_All_Contacts()
        {
            _repository = new Repository<ContactModel>(new TestDbContext<ContactModel>(TestData).Object);
            _controller = new ContactController(_repository, _mapper);

            // Act
            var actionResult = await _controller.Get();
            var result = actionResult.Result as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            var items = Assert.IsAssignableFrom<IEnumerable<ContactRequest>>(result.Value);
            WriteLine(items); // This will print items to console as a json object
        }

        [Fact]
        public async void Post_ReturnsCreatedObject()
        {
            // Arrange
            var contact1 = new ContactRequest
            {
                Name = Name.FullName(),
                Email = Lorem.Words(3).ToString(),
                Subject = Lorem.Words(1).ToString(),
                Text = Lorem.Words(3).ToString()
            };
            var contact2 = new ContactRequest
            {
                Name = Name.FullName(),
                Email = Lorem.Words(3).ToString(),
                Subject = Lorem.Words(1).ToString(),
                Text = Lorem.Words(3).ToString()
            };

            // Act
            var added1 = await _controller.Post(contact1);
            var added2 = await _controller.Post(contact2);

            var result1 = added1.Result as CreatedAtActionResult;
            var result2 = added2.Result as CreatedAtActionResult;

            // Assert
            Assert.NotNull(result1);
            Assert.NotNull(result2);
            var entity1 = result1.Value as ContactRequest;
            var entity2 = result2.Value as ContactRequest;

            Assert.NotNull(entity1);
            Assert.Equal(StatusCodes.Status201Created, result1.StatusCode);
            Assert.Equal(contact1.Email, entity1.Email);
            Assert.Equal(contact1.Name, entity1.Name);
            Assert.Equal(contact1.Subject, entity1.Subject);
            Assert.Equal(contact1.Text, entity1.Text);

            Assert.NotNull(entity2);
            Assert.Equal(StatusCodes.Status201Created, result2.StatusCode);
            Assert.Equal(contact2.Name, entity2.Name);
            Assert.Equal(contact2.Subject, entity2.Subject);
            Assert.Equal(contact2.Text, entity2.Text);
        }

        [Fact]
        public async void Delete_Should_Delete_Contact_By_ID()
        {
            //
            _repository = new Repository<ContactModel>(new TestDbContext<ContactModel>(TestData).Object);
            _controller = new ContactController(_repository, _mapper);

            // Act
            var deleted = await _controller.Delete(TestData.First().Id);
            var result = deleted.Result as OkObjectResult;

            // Assert
            var entity = result.Value as ContactRequest;
            Assert.NotNull(result);
            Assert.Equal(TestData.First().Email, entity.Email);
            Assert.Equal(TestData.First().Name, entity.Name);
            Assert.Equal(TestData.First().Subject, entity.Subject);
            Assert.Equal(TestData.First().Text, entity.Text);
        }

        [Fact]
        public async void Delete_Multiple_Contacts()
        {
            //
            _repository = new Repository<ContactModel>(new TestDbContext<ContactModel>(TestData).Object);
            _controller = new ContactController(_repository, _mapper);

            // Act
            string[] listOfIds = {TestData.First().Id, TestData.ElementAt(1).Id};
            List<ContactModel> listOfContacts = new() {TestData.First(), TestData.ElementAt(1)};
            var deleted = await _controller.Delete(listOfIds);
            var result = deleted.Result as OkObjectResult;

            // Assert
            var entity = result.Value as IEnumerable<ContactModel>;
            Assert.NotNull(result);
            
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
                Subject = x.Strings.Any(),
                Text = x.Strings.Any(),
                Created = x.Dates.Any(),
                Updated = x.Dates.Any()
            });

            return Bot.BuildSequence<ContactModel>().Take(10).ToList();
        }
    }
}