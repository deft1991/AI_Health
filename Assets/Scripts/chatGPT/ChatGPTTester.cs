using System;
using chatGPT.data;
using OpenAI.data;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Logger = playerChangeValue.util.Logger;

namespace OpenAI
{
    public class ChatGPTTester : MonoBehaviour
    {
        [SerializeField] private Button askButton;
        [SerializeField] private TMP_Text answer;
        
        [SerializeField] private string prompt;

        public void Execute()
        {
            StartCoroutine(ChatGPTClient.Instance.Ask(prompt, (r) =>  ProcessResponse(r)));
        }
        
        public void ProcessResponse(MyCreateChatCompletionResponse response)
        {
            Logger.Instance.LogInfo(response.Choices[0].Message.Content);
        }
    }
}