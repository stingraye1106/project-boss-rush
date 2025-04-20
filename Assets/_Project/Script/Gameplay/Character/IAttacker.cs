namespace NF.Main.Gameplay.Character
{
    public interface IAttacker
    {
        bool HasAttackBuff { get; set; }
        void Attack();
    }
}

