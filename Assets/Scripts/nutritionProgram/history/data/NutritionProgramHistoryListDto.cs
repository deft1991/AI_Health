using System.Collections.Generic;
using Unity.Collections;

namespace nutritionProgram.history.data
{
    public class NutritionProgramHistoryListDto
    {
        public Dictionary<string, NutritionProgramHistoryDto> nutritionPrograms = new();
    }
}