using UnityEngine;
using UnityEngine.UI;

namespace playerChangeValue.util
{
    public class GridLayoutHelper
    {

        public static void ClearGridLayoutGroup(GridLayoutGroup gridLayout)
        {
            for (int i = 0; i < gridLayout.transform.childCount; i++)
            {
                Object.Destroy(gridLayout.transform.GetChild(i).gameObject);
            }
        }
    }
}