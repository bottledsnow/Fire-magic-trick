public interface IHealth
{
    int iHealth { get; set; }
    void TakeDamage(int damage , PlayerDamage.DamageType damageType);
}
