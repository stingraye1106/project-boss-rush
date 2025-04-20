namespace NF.Main.Gameplay.Character
{
    public interface IDamageable
    {
        bool HasDefenseBuff { get; set; }
        void TakeDamage(float damage);
    }
}
