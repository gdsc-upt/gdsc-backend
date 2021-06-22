using System;
using Newtonsoft.Json;
using Xunit.Abstractions;

namespace GdscBackend.Tests
{
    public class TestingBase : IDisposable
    {
        protected readonly ITestOutputHelper OutputHelper;

        protected TestingBase(ITestOutputHelper outputHelper)
        {
            OutputHelper = outputHelper;
        }

        // Cleanup hook after each test
        public virtual void Dispose()
        {
        }

        // This method will print formatted objects to testing console
        protected void WriteLine(object obj)
        {
            OutputHelper.WriteLine(JsonConvert.SerializeObject(obj, Formatting.Indented));
        }

        protected void WriteLine(string obj)
        {
            OutputHelper.WriteLine(obj);
        }
    }
}