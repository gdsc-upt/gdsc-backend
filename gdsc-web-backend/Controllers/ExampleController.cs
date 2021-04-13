using System.Collections.Generic;
using gdsc_web_backend.Models;
using gdsc_web_backend.Models.Enums;
using Microsoft.AspNetCore.Mvc;

namespace gdsc_web_backend.Controllers
{
    // This marks this controller as a public one that can be called from the internet
    [ApiController]  
    // This sets the URL that we can enter to call the controller's methods
    // ex: https://localhost:5000/api/Example
    [Route("api/[controller]")]  
    public class ExampleController : ControllerBase
    {
        /// <summary>
        /// This method is called when someone makes a GET request
        /// </summary>
        /// <example>GET http://localhost:5000/api/Example</example>
        /// <returns>ExampleModel</returns>
        [HttpGet]
        public List<ExampleModel> Get()
        {
            return new List<ExampleModel>
            {
                new ExampleModel
                {
                    Id = "1",
                    Title = "example 1",
                    Number = 3,
                    Type = ExampleTypeEnum.EasyExample
                },
                new ExampleModel
                {
                    Id = "2",
                    Title = "example 2",
                    Number = 1,
                    Type = ExampleTypeEnum.WtfExample
                }
            };
        }
    }
}