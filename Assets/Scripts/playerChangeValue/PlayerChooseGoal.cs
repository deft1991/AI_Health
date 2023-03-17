using data;
using UnityEngine;
using UnityEngine.UI;

namespace playerChangeValue
{
    public class PlayerChooseGoal : MonoBehaviour
    {
        [SerializeField] private Button dry;
        [SerializeField] private Button same;
        [SerializeField] private Button increase;

        [SerializeField] private Button generateResultButton;
        /**
         * todo add it with messenger system, send event choose smth
         */
        public void OnClickDry()
        {
            Debug.Log("Goal Dry");
            Managers.PlayerInfoManager.Player.goal = NutritionProgramGoal.DRY;
            generateResultButton.gameObject.SetActive(true);
        }

        /**
         * todo add it with messenger system, send event choose smth
         */
        public void OnClickSame()
        {
            Debug.Log("Goal Same");
            Managers.PlayerInfoManager.Player.goal = NutritionProgramGoal.SAME;
            generateResultButton.gameObject.SetActive(true);
        }

        /**
         * todo add it with messenger system, send event choose smth
         */
        public void OnClickIncrease()
        {
            Debug.Log("Goal Increase");
            Managers.PlayerInfoManager.Player.goal = NutritionProgramGoal.INCREASE;
            generateResultButton.gameObject.SetActive(true);
        }
    }
}