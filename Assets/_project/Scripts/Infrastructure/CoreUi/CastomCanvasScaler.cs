using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.Core.Ui
{
    public class CastomCanvasScaler : MonoBehaviour
    {
        private void Awake()
        {
            GetComponent<Canvas>().worldCamera = Camera.main;

            if ((float)Screen.height / (float)Screen.width < 16f / 9f)
            {
                GetComponent<CanvasScaler>().matchWidthOrHeight = 1;
            }
            else
            {
                GetComponent<CanvasScaler>().matchWidthOrHeight = 0;
            }
        }
    }
}