using UniRx;

namespace CodeBase.Entity
{
    public class EntityDamageModel
    {
        public readonly ReactiveProperty<int> Damage = new();
        public readonly ReactiveProperty<float> AttackCooldown = new();
        public readonly ReactiveProperty<float> AttackRadius = new();
        
    }
}