using System.Threading.Tasks;
using gdsc_web_backend.Controllers.v1;
using gdsc_web_backend.Database;
using gdsc_web_backend.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;
using Xunit.Abstractions;

namespace gdsc_web_backend.tests
{
    public class ContactControllerTests : TestingBase
    {
        private readonly Mock<IRepository<ContactModel>> _repository = new();

        public ContactControllerTests(ITestOutputHelper helper) : base(helper)
        {
        }

        [Fact]
        public async Task Post_Object_Is_Null()
        {
            ContactModel example1 = null;
            _repository.Setup(x => x.Add(example1)).ReturnsAsync(() => null);
            var sut = new ContactController(_repository.Object);
            var result = (await sut.Post(example1)).Result as BadRequestObjectResult;
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task Post_Returns_OK()
        {
            var example = new ContactModel
            {
                Id = "1",
                Email = "blabla@gmai.com",
                Name = "bula",
                Subject = "subject",
                Text = "tralala"
            };
            _repository.Setup(x => x.Add(example)).ReturnsAsync(() => example);
            var sut = new ContactController(_repository.Object);
            var result = (await sut.Post(example)).Result as OkObjectResult;
            Assert.IsType<OkObjectResult>(result);
        }
    }
}