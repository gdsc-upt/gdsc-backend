using Newtonsoft.Json;
using Xunit.Abstractions;

namespace gdsc_web_backend.tests
{
    public class TestingBase
    {
        private readonly ITestOutputHelper _outputHelper;

        protected TestingBase(ITestOutputHelper outputHelper)
        {
            _outputHelper = outputHelper;
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