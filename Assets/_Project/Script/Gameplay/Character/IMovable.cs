using UnityEngine;

namespace NF.Main.Gameplay.Character 
{
    public interface IMovable
    {
        void Move(Vector3 direction);
        void StopMovement();
    }
}