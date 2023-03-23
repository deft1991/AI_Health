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
    }
}