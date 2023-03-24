using data;
using global;
using UnityEngine;
using UnityEngine.UI;

namespace playerChangeValue
{
    public class PlayerChooseGender : MonoBehaviour
    {
        [SerializeField] private Button male;
        [SerializeField] private Button female;
        
        [SerializeField] private Button weightNextButton;

        /**
         * todo add it with messenger system, send event choose smth
         */
        public void OnClickMale()
        {
            Debug.Log("Gender Male");
            Managers.PlayerInfoManager.Player.gender = GenderType.MALE;

            weightNextButton.gameObject.SetActive(true);
            
            Messenger<GenderType>.Broadcast(ProfileChangeEvent.CHANGE_GENDER, GenderType.MALE);
        }

        /**
         * todo add it with messenger system, send event choose smth
         */
        public void OnClickFemale()
        {
            Debug.Log("Gender Female");
            Managers.PlayerInfoManager.Player.gender = GenderType.FEMALE;
            weightNextButton.gameObject.SetActive(true);
            
            Messenger<GenderType>.Broadcast(ProfileChangeEvent.CHANGE_GENDER, GenderType.FEMALE);
        }
    }
}