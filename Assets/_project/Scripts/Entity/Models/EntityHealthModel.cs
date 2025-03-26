using UniRx;

namespace CodeBase.Entity
{
    public class EntityHealthModel
    {
        public readonly ReactiveProperty<int> Health = new();
        public readonly ReactiveProperty<int> MaxHealth = new();
        public readonly ReactiveProperty<bool> IsDead = new();
        
        public bool TakeDamage(int damage)
        {
            if (Health.Value > damage)
            {
                Health.Value -= damage;
            }
            else
            {
                Health.Value = 0;
                IsDead.Value = true;
                return true;
            }

            return false;
        }
    }
}