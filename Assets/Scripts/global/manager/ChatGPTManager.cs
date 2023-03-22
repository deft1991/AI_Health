using System;
using System.Collections.Generic;
using System.Linq;
using chatGPT.data;
using data;
using OpenAI;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using ChatMessage = chatGPT.data.ChatMessage;
using Logger = playerChangeValue.util.Logger;

namespace global
{
    public class ChatGPTManager : MonoBehaviour, IGameManager
    {
        public ManagerStatus Status { get; private set; }
        private OpenAIApi openai = new OpenAIApi("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");

        public void Startup()
        {
            Debug.Log("ChatGPTManager starting...");

            Status = ManagerStatus.Started;
            Debug.Log("ChatGPTManager: started");
        }

        public void LoadFillDataScene()
        {
            SceneManager.LoadScene("MainAppScene");
        }

        public void GetNutritionProgramRecommendation()
        {
            Managers.InterstitialAdExample.LoadAd();

            Debug.Log("Get Chat GPT Nutrition program recommendation");
            Messenger.Broadcast(RecommendationEvent.NUTRITION_PROGRAM_IN_PROCESS);

            PlayerIfoDto playerIfoDto = Managers.PlayerInfoManager.Player;

            // string requestMessage = "generate nutrition program for %GOAL for: %AGE age, %GENDER, %HEIGHT sm height, %WEIGHT kg weight";
            string requestMessageTemplate =
                "generate nutrition program for {0} for: {1} age, {2}, {3} sm height, {4} kg weight";

            var goal = GetGoalString(playerIfoDto);

            string requestMessage = String.Format(requestMessageTemplate,
                goal,
                playerIfoDto.age.ToString(),
                playerIfoDto.gender.ToString(),
                playerIfoDto.height.ToString(),
                playerIfoDto.weight.ToString());
            // var newMessage = new ChatMessage()
            // {
            //     Role = "user",
            //     Content = requestMessage
            // };
            // List<ChatMessage> messages = new List<ChatMessage>();
            // messages.Add(newMessage);

            StartCoroutine(ChatGPTClient.Instance.Ask(requestMessage, (r) => NutritionProgramProcessResponse(r)));
        }

        public void NutritionProgramProcessResponse(MyCreateChatCompletionResponse response)
        {
            Debug.Log("completionResponse: " + response);
            Debug.Log("completionResponse.Choices: " + response.Choices);
            Debug.Log("completionResponse.Choices.Count: " + response.Choices.Count);

            ChatMessage message = new ChatMessage();
            if (response.Choices != null && response.Choices.Count > 0)
            {
                message = response.Choices[0].Message;
                message.Content.Replace(" ", "  ");
            }
            else
            {
                Debug.LogWarning("No text was generated from this prompt.");
            }

            Debug.Log("Nutrition Program completed: " + message.Content);
            Messenger<string>.Broadcast(RecommendationEvent.NUTRITION_PROGRAM_RECEIVED, message.Content);
        }

        public async void GetWorkoutRecommendation()
        {
            Managers.InterstitialAdExample.LoadAd();

            Debug.Log("Get Workout Chat GPT recommendation");
            Messenger.Broadcast(RecommendationEvent.WORKOUT_PROGRAM_IN_PROCESS);

            PlayerIfoDto playerIfoDto = Managers.PlayerInfoManager.Player;

            string muscleGroups = String.Join(", ", Managers.PlayerInfoManager.WorkoutPrograms.ToArray());
            int duration = Managers.PlayerInfoManager.WorkoutDuration;
            string workoutLevel = Managers.PlayerInfoManager.WorkoutDifficultyLevel.ToString();

            // string requestMessage = "generate nutrition program for %GOAL for: %AGE age, %GENDER, %HEIGHT sm height, %WEIGHT kg weight";
            string requestMessageTemplate =
                "Generate workout program for {0} muscle groups. " +
                "Program on {1} min duration. " +
                "For {2} person. " +
                "For {3}. " +
                "For {4} years old {5} with {6} cm length and {7} kg weight.";

            var goal = GetGoalString(playerIfoDto);

            string requestMessage = String.Format(requestMessageTemplate,
                muscleGroups,
                duration.ToString(),
                workoutLevel,
                goal,
                playerIfoDto.age.ToString(),
                playerIfoDto.gender.ToString(),
                playerIfoDto.height.ToString(),
                playerIfoDto.weight.ToString());

            StartCoroutine(ChatGPTClient.Instance.Ask(requestMessage, (r) => WorkoutProgramProcessResponse(r)));
        }

        private static string GetGoalString(PlayerIfoDto playerIfoDto)
        {
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

            return goal;
        }

        public void WorkoutProgramProcessResponse(MyCreateChatCompletionResponse response)
        {
            ChatMessage message = new ChatMessage();
            if (response.Choices != null && response.Choices.Count > 0)
            {
                message = response.Choices[0].Message;
                message.Content.Replace(" ", "  ");
            }
            else
            {
                Debug.LogWarning("No text was generated from this prompt.");
            }

            Debug.Log("Workout Program completed: " + message.Content);
            Messenger<string>.Broadcast(RecommendationEvent.WORKOUT_PROGRAM_RECEIVED, message.Content);
        }
    }
}