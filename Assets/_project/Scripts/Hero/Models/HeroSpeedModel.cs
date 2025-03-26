using CodeBase.Configs;
using CodeBase.Entity;
using CodeBase.Extencions;

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
            if (playerProgress.TryGetCharacteristicByType(CharacteristicType.Speed, out var characteristic))
            {
                Speed.Value = characteristic.FloatValue;
            }
        }
    }
}