using System.Collections.Generic;
using Xunit.Abstractions;

namespace GdscBackend.Tests
{
    public class AuthenticationControllerTests : TestingBase
    {
        private readonly IEnumerable<UserModel> _testData = _getTestData();
        public AuthenticationControllerTests(ITestOutputHelper outputHelper) : base(outputHelper)
        {
        }

        public async void login()
        {
            
        }

        public async void register()
        {
            
        }

        private static IEnumerable<UserModel> _getTestData()
        {
        }
    }
}