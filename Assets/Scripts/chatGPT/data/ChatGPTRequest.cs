using System.Collections.Generic;
using Newtonsoft.Json;
using OpenAI;

public class ChatGPTRequest
{
    [JsonProperty(PropertyName = "messages")]
    public List<ChatMessage> Messages { get; set; }
    
    [JsonProperty(PropertyName = "model")]
    public string Model { get; set; }
}