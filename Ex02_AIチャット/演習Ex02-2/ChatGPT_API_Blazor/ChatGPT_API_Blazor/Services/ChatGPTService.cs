using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text;

namespace ChatGPT_API_Blazor.Services
{
    public class ChatGPTService
    {
        private HttpClient _client = new HttpClient();
        static readonly string _apiKey = Environment.GetEnvironmentVariable("GPT_APIKEY");
        static readonly string _apiEndpoint = "https://api.openai.com/v1/chat/completions";

        public async Task<string> AskGPT4(string message)
        {
            var requestData = new GPTRequest
            {
                model = "gpt-4-turbo-preview",
                messages = [
                    new Message { role = "user", content = message }
                ]
            };

            var requestContent = JsonConvert.SerializeObject(requestData);
            var content = new StringContent(requestContent, Encoding.UTF8, "application/json");

            _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _apiKey);

            var response = await _client.PostAsync(_apiEndpoint, content);
            response.EnsureSuccessStatusCode();

            var responseBody = await response.Content.ReadAsStringAsync();
            var responseObject = JsonConvert.DeserializeObject<GPTResponse>(responseBody);

            return responseObject.choices[0].message.content;
        }

        public async Task<string> AskGPT4WithDynamicType(string message)
        {
            dynamic requestData = new JObject();
            requestData.model = "gpt-4-turbo-preview";
            requestData.messages = new JArray(new JObject { { "role", "user" }, { "content", message } });

            var requestContent = requestData.ToString();
            var content = new StringContent(requestContent, Encoding.UTF8, "application/json");
            _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _apiKey);

            var response = await _client.PostAsync(_apiEndpoint, content);
            response.EnsureSuccessStatusCode();

            var responseBody = await response.Content.ReadAsStringAsync();
            dynamic responseObject = JObject.Parse(responseBody);

            return responseObject.choices[0].message.content;
        }

        record GPTRequest
        {
            public string model { get; init; }
            public Message[] messages { get; init; }
        }

        record Message
        {
            public string role { get; init; }
            public string content { get; init; }
        }

        record GPTResponse
        {
            public Choice[] choices { get; init; }
        }

        record Choice
        {
            public InnerMessage message { get; init; }
        }

        record InnerMessage
        {
            public string content { get; init; }
        }
    }
}
