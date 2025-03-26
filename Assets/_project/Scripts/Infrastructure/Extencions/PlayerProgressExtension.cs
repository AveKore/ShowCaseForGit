using CodeBase.Configs;
using CodeBase.Hero;

namespace CodeBase.Extencions
{
    public static class PlayerProgressExtension
    {
        
        public static bool TryGetCharacteristicByType(this PlayerProgressData playerProgress, CharacteristicType characteristicType, out HeroCharacteristicModel characteristic)
        {
            return playerProgress.CharacteristicModels.TryGetValue(characteristicType, out characteristic);
        }
    }
}