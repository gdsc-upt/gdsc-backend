using System;
using Newtonsoft.Json;
using Xunit.Abstractions;

namespace gdsc_web_backend.tests
{
    public class TestingBase : IDisposable
    {
        private readonly ITestOutputHelper _outputHelper;

        protected TestingBase(ITestOutputHelper outputHelper)
        {
            _outputHelper = outputHelper;
        }

        // Cleanup hook after each test
        public virtual void Dispose()
        {
        }

        // This method will print formatted objects to testing console
        protected void WriteLine(object obj)
        {
            _outputHelper.WriteLine(JsonConvert.SerializeObject(obj, Formatting.Indented));
        }

        protected void WriteLine(string obj)
        {
            _outputHelper.WriteLine(obj);
        }
    }
}