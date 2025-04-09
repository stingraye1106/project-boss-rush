using UnityEngine;

namespace NF.Main.Gameplay.Movement
{
    public class FacingController : MonoBehaviour
    {
        private static readonly Vector3 DefaultDirection = Vector3.zero;

        [SerializeField] private float _rotateSpeed;

        private Vector3 _facingDirection;
        private IMovement _movement;

        private void Start()
        {
            _facingDirection = DefaultDirection;
            _movement = GetComponent<IMovement>();
        }

        private void Update()
        {
            FaceAtDirection();
        }

        private void FaceAtDirection()
        {
            if (_movement.Direction.sqrMagnitude > 0f)
            {
                _facingDirection = _movement.Direction;
            }

            var lookRotation = Quaternion.LookRotation(_facingDirection, Vector3.up);
            var step = _rotateSpeed * Time.deltaTime;
            transform.rotation = Quaternion.RotateTowards(transform.rotation, lookRotation, step);
        }
    }
}

