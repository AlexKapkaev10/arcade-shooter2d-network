namespace Scripts.Game
{
    public interface IDamageable
    {
        public PlayerHealth PlayerHealth { get; }
        public void Damage(byte damageValue);
    }
}