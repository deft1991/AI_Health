using UnityEngine;
using UnityEngine.UI;

namespace data
{
    public class MuscleGroupToggle : MonoBehaviour
    {
        [SerializeField] private MuscleGroupType type;

        public MuscleGroupType Type => type;
        [SerializeField] private Toggle toggle;
    }
}