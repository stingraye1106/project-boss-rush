using Sirenix.OdinInspector;
using UnityEngine;

namespace NF.Main.Gameplay.Movement
{
    public class PlayerMovement : MonoBehaviour, IMovement
    {
        [TabGroup("Movement Settings")][SerializeField] private float _speedMultiplier;

        private float _speed;
        private Rigidbody _rigidbody;
        private Vector3 _direction = Vector3.zero;
        private bool _canMove;


        public float Speed { get => _speed; set => _speed = value; }
        public float SpeedMultiplier { get => _speedMultiplier; set => _speedMultiplier = value; }
        public Vector3 Direction { get => _direction; set => _direction = value; }
        public bool CanMove { get => _canMove; set => _canMove = value; }


        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _canMove = true;
        }

        private void FixedUpdate()
        {
            if (_canMove)
            {
                MoveToDirection(_direction);
                Debug.Log($"Moving direction: {_direction}");
            }
        }

        private void MoveToDirection(Vector3 moveDirection)
        {
            var currentSpeed = _speed * _speedMultiplier;
            var step = currentSpeed * Time.fixedDeltaTime;
            var moveDelta = Vector3.MoveTowards(transform.position, transform.position + moveDirection.normalized, step);
           _rigidbody.MovePosition(moveDelta);
        }
    }
}

