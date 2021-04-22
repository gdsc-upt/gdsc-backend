using gdsc_web_backend.Controllers;
using gdsc_web_backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Query;
using Xunit;
using Xunit.Abstractions;

namespace gdsc_web_backend.tests
{
    public class FaqControllerTests : TestingBase
    {
        public FaqControllerTests(ITestOutputHelper outputHelper) : base(outputHelper)
        {
        }

        [Fact]
        public void Post_ReturnsErrors_WhenIdNotUnique()
        {
            var controller = new FaqsController();
            var example1 = new FaqModel
            {
                Id = "1",
                Question = "Ce ai facut la examene?",
                Answer = "Le-am luat, verisoru."
            };

            var example2 = new FaqModel
            {
                Id = "1",
                Question = "Apropo,tu ce ai facut?",
                Answer = "Am mai si picat, varu"
            };
            
            //var added1 = controller.Post(example1).Result as CreatedResult;            
        }

        [Fact]

        public void Post_ReturnsCreatedObject()
        {
            var controller = new FaqsController();
            var example1 = new FaqModel()
            {
                Id = "1",
                Question = "Ce ai facut la examene?",
                Answer = "Le-am luat, verisoru."
            };

            var example2 = new FaqModel()
            {
                Id = "2",
                Question = "Ce ai facut la examene?",
                Answer = "Le-am luat, verisoru."
            };

            var added1=controller.
        }
    }
}