using UnityEngine;

namespace data
{
    public class PlayerIfoDto
    {
        public string name;
        public int age;
        public GenderType gender;
        public int weight;
        public int height;
        public NutritionProgramGoal goal;

        public MealDto MealDto;
        
        public static string GetGoalString(PlayerIfoDto playerIfoDto)
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
        
        public static string GetGoalString(NutritionProgramGoal playerIfoDto)
        {
            string goal = "";
            switch (playerIfoDto)
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
    }
}