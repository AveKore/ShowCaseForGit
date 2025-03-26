using UniRx;

namespace CodeBase.Entity
{
    public class EntitySpeedModel
    {
        public readonly ReactiveProperty<float> Speed = new();
    }
}