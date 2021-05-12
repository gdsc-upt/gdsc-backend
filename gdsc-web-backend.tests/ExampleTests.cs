// using System.Collections.Generic;
// using gdsc_web_backend.Controllers.v1;
// using gdsc_web_backend.Models;
// using gdsc_web_backend.Models.Enums;
// using Microsoft.AspNetCore.Http;
// using Microsoft.AspNetCore.Mvc;
// using Xunit;
// using Xunit.Abstractions;
//
// namespace gdsc_web_backend.tests
// {
//     public class ExampleTests : TestingBase
//     {
//
//         public ExampleTests(ITestOutputHelper outputHelper) : base(outputHelper)
//         {
//         }
//
//         [Fact]
//         public async void Post_ReturnsCreatedObject()
//         {
//             // Arrange
//             var controller = new ExamplesController();
//             var example1 = new ExampleModel
//             {
//                 Number = 2,
//                 Title = "First example",
//                 Type = ExampleTypeEnum.EasyExample
//             };
//             var example2 = new ExampleModel
//             {
//                 Number = 4,
//                 Title = "Second example",
//                 Type = ExampleTypeEnum.WtfExample
//             };
//
//             // Act
//             var added1 = await controller.Post(example1);
//             var added2 = await controller.Post(example2);
//
//             // Assert
//             Assert.NotNull(added1);
//             // Assert.Equal(StatusCodes.Status201Created);
//             Assert.Equal(example1, added1.Value);
//
//             Assert.NotNull(added2);
//             // Assert.Equal(StatusCodes.Status201Created, added2.StatusCode);
//             Assert.Equal(example2, added2.Value);
//         }
//
//         // [Fact]
//         // public async void Post_ReturnsError_WhenIdNotUnique()
//         // {
//         //     // Arrange
//         //     var controller = new ExamplesController();
//         //     var example1 = new ExampleModel
//         //     {
//         //         Id = "1",
//         //         Number = 2,
//         //         Title = "First example",
//         //         Type = ExampleTypeEnum.EasyExample
//         //     };
//         //     var example2 = new ExampleModel
//         //     {
//         //         Id = "1",
//         //         Number = 4,
//         //         Title = "Second example",
//         //         Type = ExampleTypeEnum.WtfExample
//         //     };
//         //
//         //     // Act
//         //     var added1 = await controller.Post(example1);
//         //     var added2 = await controller.Post(example2);
//         //     Assert.NotNull(added2);
//         //     var error = added2.Value;
//         //
//         //     // Assert
//         //     Assert.NotNull(added1);
//         //     // Assert.Equal(StatusCodes.Status201Created, added1.StatusCode);
//         //     Assert.Equal(example1, added1.Value as ExampleModel);
//         //
//         //     Assert.NotNull(error);
//         //     // Assert.Equal(StatusCodes.Status400BadRequest, added2.StatusCode);
//         //     Assert.Equal("An object with the same ID already exists", error.Message);
//         // }
//
//         // [Fact]
//         // public void Get_ReturnsAllExamples()
//         // {
//         //     // Arrange
//         //     var controller = new ExamplesController();
//         //     var examples = new List<ExampleModel>
//         //     {
//         //         new()
//         //         {
//         //             Id = "1",
//         //             Number = 2,
//         //             Title = "First example",
//         //             Type = ExampleTypeEnum.EasyExample
//         //         },
//         //         new()
//         //         {
//         //             Id = "4",
//         //             Number = 4,
//         //             Title = "Second example",
//         //             Type = ExampleTypeEnum.WtfExample
//         //         }
//         //     };
//         //     controller.Post(examples[0]);
//         //     controller.Post(examples[1]);
//         //
//         //     // Act
//         //     var result = controller.Get().Result as OkObjectResult;
//         //
//         //     // Assert
//         //     Assert.NotNull(result);
//         //     var items = Assert.IsAssignableFrom<IEnumerable<ExampleModel>>(result.Value);
//         //     WriteLine(items); // This will print items to console as a json object
//         //     Assert.Equal(examples, items);
//         // }
//     // }
// }
