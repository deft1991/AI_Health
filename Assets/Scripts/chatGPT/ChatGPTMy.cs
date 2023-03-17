using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine.Networking;

namespace OpenAI
{
    public class ChatGPTMy : MonoBehaviour
    {
        [SerializeField] private InputField inputField;
        [SerializeField] private Button button;
        [SerializeField] private RectTransform context;

        [SerializeField] private RectTransform sent;
        [SerializeField] private RectTransform received;

        private float height;
        private OpenAIApi openai = new OpenAIApi("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", "org-fVCIIGWZbVP9E8ul4IRNqKWo");

        private List<ChatMessage> messages = new List<ChatMessage>();

        private string prompt =
            "Act as a random stranger in a chat room and reply to the questions. Don't break character. Don't ever mention that you are an AI model.";

        private void Start()
        {
            button.onClick.AddListener(SendReply);
        }

        private async void AppendMessage(ChatMessage message)
        {
            context.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 0);

            var item = Instantiate(message.Role == "user" ? sent : received, context);
            item.GetChild(0).GetChild(0).GetComponent<Text>().text = message.Content;
            item.anchoredPosition = new Vector2(0, -height);

            await Task.Delay(200);

            height += item.sizeDelta.y;
            context.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, height);
        }

        private async void SendReply()
        {
            await SendRequestToChatGPT();
        }

        private async Task SendRequestToChatGPT()
        {
            var newMessage = new ChatMessage()
            {
                Role = "user",
                Content = inputField.text
            };

            AppendMessage(newMessage);

            if (messages.Count == 0) newMessage.Content = prompt + "\n" + inputField.text;

            messages.Add(newMessage);

            button.enabled = false;
            inputField.enabled = false;

            // Complete the instruction
            var completionResponse = await openai.CreateChatCompletion(new CreateChatCompletionRequest()
            {
                Model = "gpt-3.5-turbo-0301",
                Messages = messages
            });

            if (completionResponse.Choices != null && completionResponse.Choices.Count > 0)
            {
                var message = completionResponse.Choices[0].Message;
                message.Content = message.Content.Trim();

                messages.Add(message);
                AppendMessage(message);
            }
            else
            {
                Debug.LogWarning("No text was generated from this prompt.");
            }

            button.enabled = true;
            inputField.enabled = true;
        }
    }
}