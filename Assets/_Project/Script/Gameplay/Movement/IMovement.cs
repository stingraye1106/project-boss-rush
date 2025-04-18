using UnityEngine;

namespace NF.Main.Gameplay.Movement
{
    public interface IMovement
    {
        float Speed { get; set; }
        float SpeedMultiplier { get; set; }
        Vector3 Direction { get; set; }
        bool CanMove { get; set; }
    }
}