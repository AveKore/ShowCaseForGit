using CodeBase.Hero;
using TMPro;
using UniRx;
using UnityEngine;

namespace CodeBase.Windows.Common
{
    public class SkillPointsCounterView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _skillPointsText;
        public void Init(HeroSkillPointsModel skillPointsModel)
        {
            skillPointsModel.SkillPoints.Subscribe(UpdateSkillPoints);
        }

        private void UpdateSkillPoints(int skillPoints)
        {
            _skillPointsText.text = skillPoints.ToString();
        }
    }
}