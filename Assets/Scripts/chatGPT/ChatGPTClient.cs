using System.Collections;
using System.Collections.Generic;
using chatGPT.data;
using Newtonsoft.Json;
using OpenAI.data;
using playerChangeValue.util;
using UnityEngine;
using UnityEngine.Networking;
using Logger = playerChangeValue.util.Logger;

namespace OpenAI
{
    public class ChatGPTClient : Singleton<ChatGPTClient>
    {
        [SerializeField] private ChatGPTSettings chatGptSettings;

        public IEnumerator Ask(string prompt, System.Action<MyCreateChatCompletionResponse> callback)
        {
            var url = chatGptSettings.debug ? $"{chatGptSettings.apiURL}?debug=true" : chatGptSettings.apiURL;


            var newMessage = new ChatMessage
            {
                Role = "user",
                Content = prompt
            };

            List<ChatMessage> messages = new List<ChatMessage>();
            messages.Add(newMessage);

            using (UnityWebRequest request = new UnityWebRequest(url, "POST"))
            {
                byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(
                    JsonConvert.SerializeObject(new ChatGPTRequest
                    {
                        Messages = messages,
                        Model = "gpt-3.5-turbo"
                        // todo add reminders to the question
                    }));

                request.uploadHandler = new UploadHandlerRaw(bodyRaw);
                request.downloadHandler = new DownloadHandlerBuffer();
                request.disposeDownloadHandlerOnDispose = true;
                request.disposeUploadHandlerOnDispose = true;
                request.disposeCertificateHandlerOnDispose = true;

                request.SetRequestHeader("Content-Type", "application/json");
                request.SetRequestHeader("Authorization", "Bearer " + chatGptSettings.apiKey);

                yield return request.SendWebRequest();

                if (request.result == UnityWebRequest.Result.ConnectionError ||
                    request.result == UnityWebRequest.Result.DataProcessingError)
                {
                    Logger.Instance.LogError("err");
                    Logger.Instance.LogError(request.error);
                }
                else
                {
                    string responseInfo = request.downloadHandler.text;
                    Logger.Instance.LogInfo("resp: " + responseInfo);
                    var response = new ChatGPTResponse { Data = responseInfo };
                    MyCreateChatCompletionResponse resp =
                        JsonConvert.DeserializeObject<MyCreateChatCompletionResponse>(responseInfo);
                    // Logger.Instance.LogInfo("resp object: " + resp);
                    // Logger.Instance.LogInfo("resp object.id: " + resp.Id);
                    // Logger.Instance.LogInfo("resp object.Choices: " + resp.Choices);
                    // Logger.Instance.LogInfo("resp object.Choices.Count: " + resp.Choices.Count);
                    // Logger.Instance.LogInfo("resp object.Choices[0].Message: " + resp.Choices[0].Message);
                    // Logger.Instance.LogInfo("resp object.Choices[0].Message: " + resp.Choices[0].Message.Content);
                    // Logger.Instance.LogInfo("resp object.Choices[0].Role: " + resp.Choices[0].Message.Role);
                    callback(resp);
                }
            }
        }
    }
}