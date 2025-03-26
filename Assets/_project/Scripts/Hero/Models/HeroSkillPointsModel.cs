using CodeBase.Configs;
using UniRx;

namespace CodeBase.Hero
{
    public class HeroSkillPointsModel
    {
        public readonly ReactiveProperty<int> SkillPoints = new();
     
        private PlayerProgressData _playerProgress;
        
        public void AddSkillPoints(int count)
        {
            SkillPoints.Value += count;
            SaveValue();
        }
        
        public void RemoveSkillPoints(int count)
        {
            SkillPoints.Value -= count;
            SaveValue();
        }

        private void SaveValue()
        {
            _playerProgress.SkillPoints = SkillPoints.Value;
        }
        
        public void LoadProgress(PlayerProgressData playerProgress)
        {
            _playerProgress = playerProgress;
            SkillPoints.Value = playerProgress.SkillPoints;
        }
    }
}