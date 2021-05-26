using System.Collections.Generic;
using GdscBackend.Models;
using Xunit;
using Xunit.Abstractions;

namespace GdscBackend.Tests
{
    public class TeamsControllerTests : TestingBase
    {
        private readonly IEnumerable<TeamModel> _testData = _;
        
        public TeamsControllerTests(ITestOutputHelper helper) : base(helper)
        {
        }

        [Fact]
        public async void Post_ReturnsCreatedObject()
        {
            
        }

        [Fact]
        public async void Get_ReturnsAllExamples()
        {
            
        }
        
        [Fact]
        public async void Get_ReturnsAllExamplesById()
        {
            
        }

    }
}