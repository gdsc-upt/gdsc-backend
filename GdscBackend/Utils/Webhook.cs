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
        public string AvatarUrl { get; set; } = "https://www.pngitem.com/pimgs/m/156-1568414_book-contact-icon-volkswagen-hd-png-download.png";
        
        public Webhook(string webhookUrl)
        {
            _httpClient = new HttpClient();
            _webhookUrl = webhookUrl;
        }

        private string ContactContentBuilder(string author, string mail, string subject, string message)
        {
            return "**Name:**  " + author + "\n" + "**Email:**  " + mail + "\n" + "**Subject:**  " + subject + "\n" + "**Message:**  " + message + "\n";
        }
        
        public async Task<HttpResponseMessage> Send(string author, string mail, string subject, string message)
        {
            Username = "Baiatu' cu contactele";
            Content = ContactContentBuilder(author, mail, subject, message);
            var payload = new StringContent(JsonConvert.SerializeObject(this), Encoding.UTF8, "application/json");
            return await _httpClient.PostAsync(_webhookUrl, payload);
        }
    }
}