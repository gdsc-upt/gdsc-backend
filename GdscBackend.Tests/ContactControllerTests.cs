using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Faker;
using GdscBackend.Controllers.v1;
using GdscBackend.Database;
using GdscBackend.Models;
using GdscBackend.RequestModels;
using GdscBackend.Tests.Mocks;
using GdscBackend.Utils.Mappers;
using GdscBackend.Utils.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Xunit;
using Xunit.Abstractions;

namespace GdscBackend.Tests;

public class ContactsControllerTests : TestingBase
{
    private static readonly IEnumerable<ContactModel> TestData = _getTestData();
    private readonly IMapper _mapper;
    private readonly IWebhookService _webhookService;

    public ContactsControllerTests(ITestOutputHelper outputHelper) : base(outputHelper)
    {
        var mapconfig = new MapperConfiguration(cfg => cfg.AddProfile(new MappingProfiles()));
        _mapper = mapconfig.CreateMapper();
        _webhookService = new TestWebhookService(outputHelper);
    }

    [Fact]
    public async void Get_Should_Return_All_Contacts()
    {
        var repository = new Repository<ContactModel>(new TestDbContext<ContactModel>(TestData).Object);
        var controller =
            new ContactController(repository, _mapper, new TestEmailSender(OutputHelper), _webhookService);

        // Act
        var actionResult = await controller.Get();
        var result = actionResult.Result as OkObjectResult;

        // Assert
        Assert.NotNull(result);
        var items = Assert.IsAssignableFrom<IEnumerable<ContactModel>>(result.Value);
        WriteLine(items); // This will print items to console as a json object
    }

    [Fact]
    public async void Post_ReturnsCreatedObject()
    {
        // Arrange
        var repository = new Repository<ContactModel>(new TestDbContext<ContactModel>(TestData).Object);
        var controller =
            new ContactController(repository, _mapper, new TestEmailSender(OutputHelper), _webhookService);

        var contact1 = new ContactRequest
        {
            Name = Name.FullName(),
            Email = "validemail@example.com",
            Subject = Lorem.Words(1).ToString(),
            Text = Lorem.Words(3).ToString()
        };
        var contact2 = new ContactRequest
        {
            Name = Name.FullName(),
            Email = "validemail2@example.com",
            Subject = Lorem.Words(1).ToString(),
            Text = Lorem.Words(3).ToString()
        };

        // Act
        var added1 = await controller.Post(contact1);
        var added2 = await controller.Post(contact2);

        var result1 = added1.Result as CreatedResult;
        var result2 = added2.Result as CreatedResult;

        // Assert
        Assert.NotNull(result1);
        Assert.NotNull(result2);
        var entity1 = result1.Value as ContactModel;
        var entity2 = result2.Value as ContactModel;

        Assert.NotNull(entity1);
        Assert.Equal(StatusCodes.Status201Created, result1.StatusCode);
        Assert.Equal(contact1.Email, entity1.Email);
        Assert.Equal(contact1.Name, entity1.Name);
        Assert.Equal(contact1.Subject, entity1.Subject);
        Assert.Equal(contact1.Text, entity1.Text);

        Assert.NotNull(entity2);
        Assert.Equal(StatusCodes.Status201Created, result2.StatusCode);
        Assert.Equal(contact2.Name, entity2.Name);
        Assert.Equal(contact2.Email, entity2.Email);
        Assert.Equal(contact2.Subject, entity2.Subject);
        Assert.Equal(contact2.Text, entity2.Text);
    }

    [Fact]
    public async void Delete_Should_Delete_Contact_By_ID()
    {
        //
        var repository = new Repository<ContactModel>(new TestDbContext<ContactModel>(TestData).Object);
        var controller =
            new ContactController(repository, _mapper, new TestEmailSender(OutputHelper), _webhookService);

        // Act
        var deleted = await controller.Delete(TestData.First().Id);
        var result = deleted.Result as OkObjectResult;

        // Assert
        Assert.NotNull(result);
        var entity = result.Value as ContactModel;
        Assert.NotNull(entity);
        Assert.Equal(TestData.First().Email, entity.Email);
        Assert.Equal(TestData.First().Name, entity.Name);
        Assert.Equal(TestData.First().Subject, entity.Subject);
        Assert.Equal(TestData.First().Text, entity.Text);
    }

    [Fact]
    public async void Delete_Multiple_Contacts()
    {
        //
        var repository = new Repository<ContactModel>(new TestDbContext<ContactModel>(TestData).Object);
        var controller =
            new ContactController(repository, _mapper, new TestEmailSender(OutputHelper), _webhookService);

        // Act
        string[] listOfIds = { TestData.First().Id, TestData.ElementAt(1).Id };
        var deleted = await controller.Delete(listOfIds);
        var result = deleted.Result as OkObjectResult;

        // Assert
        Assert.NotNull(result);
        var entity = result.Value as IEnumerable<ContactModel>;
        Assert.NotNull(entity);
    }

    private static IEnumerable<ContactModel> _getTestData()
    {
        var models = new List<ContactModel>();
        for (var _ = 0; _ < 10; _++)
            models.Add(new ContactModel
            {
                Id = Guid.NewGuid().ToString(),
                Name = Lorem.Words(1).ToString(),
                Email = Lorem.Words(5).ToString(),
                Subject = Lorem.Words(1).ToString(),
                Text = Lorem.Words(1).ToString()
            });

        return models;
    }
}