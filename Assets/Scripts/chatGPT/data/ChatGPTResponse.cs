using Newtonsoft.Json;

namespace OpenAI.data
{
    public class ChatGPTResponse
    {
        [JsonProperty(PropertyName = "data")]
        public string Data { get; set; }
    }
}