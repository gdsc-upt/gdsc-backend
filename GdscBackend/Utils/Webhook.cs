using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
namespace GdscBackend.Utils
{
    [JsonObject]
    public class Webhook
    {
        private readonly HttpClient _httpClient;
        private readonly string _webhookUrl;
        
        [JsonProperty("content")]
        public string Content { get; set; } = "";
        [JsonProperty("username")]
        public string Username { get; set; } = "";
        [JsonProperty("avatar_url")]
        public string AvatarUrl { get; set; } = "";
        
        public Webhook(string webhookUrl)
        {
            _httpClient = new HttpClient();
            _webhookUrl = webhookUrl;
        }

        public Webhook(string webhookUrl, string username)
        {
            _httpClient = new HttpClient();
            _webhookUrl = webhookUrl;
            Username = username;
        }

        public Webhook(string webhookUrl, string username, string avatarUrl)
        {
            _httpClient = new HttpClient();
            _webhookUrl = webhookUrl;
            Username = username;
            AvatarUrl = avatarUrl;
        }

        public async Task<HttpResponseMessage> Send(string content)
        {
            Content = content;
            var payload = new StringContent(JsonConvert.SerializeObject(this), Encoding.UTF8, "application/json");
            return await _httpClient.PostAsync(_webhookUrl, payload);
        }
    }
}