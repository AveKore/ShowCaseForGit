namespace CodeBase.Hero
{
    public enum CharacteristicType
    {
        Damage = 0,
        AttackRadius = 1 << 1,
        AttackCooldown = 1 << 2,
        Speed = 1 << 3,
        MaxHealth = 1 << 4,
    }
}