using System;
using System.Collections.Generic;
using data;
using OpenAI;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace global
{
    public class ChatGPTManager : MonoBehaviour, IGameManager
    {
        public ManagerStatus Status { get; private set; }
        private OpenAIApi openai = new OpenAIApi("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");

        public void Startup()
        {
            Debug.Log("Mission manager starting...");

            Status = ManagerStatus.Started;
            Debug.Log("ChatGPTManager: started");
        }

        public void LoadFillDataScene()
        {
            SceneManager.LoadScene("MainAppScene");
        }

        public async void GetRecommendation()
        {
            // Managers.BannerAdExample.Start();
            
            Debug.Log("Get Chat GPT recommendation");
            Messenger.Broadcast(RecommendationEvent.IN_PROCESS);

            PlayerIfoDto playerIfoDto = Managers.PlayerInfoManager.Player;

            // string requestMessage = "generate nutrition program for %GOAL for: %AGE age, %GENDER, %HEIGHT sm height, %WEIGHT kg weight";
            string requestMessageTemplate =
                "generate nutrition program for {0} for: {1} age, {2}, {3} sm height, {4} kg weight";

            string goal = "";
            switch (playerIfoDto.goal)
            {
                case NutritionProgramGoal.DRY:
                    goal = "reduce fat";
                    break;
                case NutritionProgramGoal.SAME:
                    goal = "maintain muscle mass";
                    break;
                case NutritionProgramGoal.INCREASE:
                    goal = "increase muscles";
                    break;
            }
            
            string requestMessage = String.Format(requestMessageTemplate,
                goal,
                playerIfoDto.age.ToString(),
                playerIfoDto.gender.ToString(),
                playerIfoDto.height.ToString(),
                playerIfoDto.weight.ToString());
            var newMessage = new ChatMessage()
            {
                Role = "user",
                Content = requestMessage
            };
            List<ChatMessage> messages = new List<ChatMessage>();
            messages.Add(newMessage);

            // Complete the instruction
            var completionResponse = await openai.CreateChatCompletion(new CreateChatCompletionRequest()
            {
                Model = "gpt-3.5-turbo-0301",
                Messages = messages
            });

            ChatMessage message;
            if (completionResponse.Choices != null && completionResponse.Choices.Count > 0)
            {
                message = completionResponse.Choices[0].Message;
                PlayerPrefs.SetString("recommendation", message.Content);
            }
            else
            {
                Debug.LogWarning("No text was generated from this prompt.");
            }

            Debug.Log("Recommendations completed: " + message.Content);
            Messenger<string>.Broadcast(RecommendationEvent.RECEIVED, message.Content);
        }
    }
}