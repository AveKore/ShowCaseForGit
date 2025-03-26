using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.Windows
{
    public class HealthBarView : MonoBehaviour
    {
        public Image Slider;
        private int _maxHealth;

        public void UpdateHealth(int health, int maxHealth)
        {
            _maxHealth = maxHealth;
            Slider.fillAmount = (float)health / maxHealth;
        }

        public void UpdateHealth(int health)
        {
            Slider.fillAmount = (float)health / _maxHealth;
        }
    }
}