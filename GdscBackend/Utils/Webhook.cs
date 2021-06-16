using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using GdscBackend.Auth;
using Newtonsoft.Json;
// using Discord;

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
        // public Embed Embed { get; set; }
        
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
            // var builder = new EmbedBuilder {Author = new EmbedAuthorBuilder().WithName(author), Title = subject};
            // builder.AddField("Message: ", "\"" + message + "\"", true);
            // builder.AddField("Email: ", mail, true);
            // Embed = builder.Build();
            Username = "Contact Hook";
            Content = ContactContentBuilder(author, mail, subject, message);
            var payload = new StringContent(JsonConvert.SerializeObject(this), Encoding.UTF8, "application/json");
            return await _httpClient.PostAsync(_webhookUrl, payload);
        }
    }
}