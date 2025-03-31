using CodeBase.Configs;
using CodeBase.Entity;

namespace CodeBase.Hero
{
    public class HeroSpeedModel : EntitySpeedModel
    {
        public void BoostSpeed(float speed)
        {
            Speed.Value += speed;
        }
        
        public void LoadProgress(PlayerProgressData playerProgress)
        {
            Speed.Value = playerProgress.StatsProgress[CharacteristicType.Speed].Value;
        }
    }
}