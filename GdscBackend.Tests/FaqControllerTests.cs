// /*using GdscBackend.Controllers.v1;
// using GdscBackend.Models;
// using GdscBackend.RequestModels;
// using GdscBackend.Tests;
// using Microsoft.AspNetCore.Http;
// using Microsoft.AspNetCore.Mvc;
// using Xunit;
// using Xunit.Abstractions;
//
// namespace gdsc_web_backend.tests
// {
//     public class FaqControllerTests : TestingBase
//     {
//         public FaqControllerTests(ITestOutputHelper outputHelper) : base(outputHelper)
//         {
//         }
//
//         [Fact]
//         public void Post_ReturnsErrors_WhenIdNotUnique()/ {
//             var controller = new FaqsController();
//             var example1 = new FaqModel
//             {
//                 Id = "1",
//                 Question = "Ce ai facut la examene?",
//                 Answer = "Le-am luat, verisoru."
//             };
//
//             var example2 = new FaqModel
//             {
//                 Id = "1",
//                 Question = "Apropo,tu ce ai facut?",
//                 Answer = "Am mai si picat, varu"
//             };
//
//             //Act
//             var added1 = controller.Post(example1).Result as OkObjectResult;
//             var added2 = controller.Post(example2).Result as BadRequestObjectResult;
//
//             //Assert
//             Assert.NotNull(added1);
//             Assert.Equal(StatusCodes.Status200OK, added1.StatusCode);
//             Assert.Equal(example1, added1.Value as FaqModel);
//
//             Assert.NotNull(added2);
//             Assert.Equal(StatusCodes.Status400BadRequest, added2.StatusCode);
//         }
//
//         [Fact]
//         public void Post_ReturnsCreatedObject()
//         {
//             var controller = new FaqsController();
//             var example1 = new FaqRequest
//             {
//                 Id = "1",
//                 Question = "Ce ai facut la examene?",
//                 Answer = "Le-am luat, verisoru."
//             };
//
//             var example2 = new FaqRequest
//             {
//                 Id = "2",
//                 Question = "Apropo,tu ce ai facut?",
//                 Answer = "Am mai si picat, varu"
//             };
//
//             var added1 = controller.Post(example1);
//             var added2 = controller.Post(example2);
//
//             var newadd1=added1.Result as CreatedResult;
//             var newadd2=added2.Result as CreatedResult;
//
//             Assert.NotNull(added1);
//             Assert.Equal(StatusCodes.Status200OK, newadd1.StatusCode);
//             Assert.Equal(example1, newadd1.Value);
//
//             Assert.NotNull(added2);
//             Assert.Equal(StatusCodes.Status200OK, newadd2.StatusCode);
//             Assert.Equal(example2, newadd2.Value);
//         }
//     }
// }*/
