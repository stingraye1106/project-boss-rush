using Sirenix.OdinInspector;
using UnityEngine;

namespace NF.Main.Gameplay.Movement
{
    public class PlayerMovement : MonoBehaviour, IMovement
    {
        [TabGroup("Movement Settings")][SerializeField] private float _speed;
        [TabGroup("Movement Settings")][SerializeField] private float _speedMultiplier;
        
        private Rigidbody _rigidbody;
        private Vector3 _direction = Vector3.zero;


        public float Speed { get => _speed; }
        public float SpeedMultiplier { get => _speedMultiplier; set => _speedMultiplier = value; }
        public Vector3 Direction { get => _direction; set => _direction = value; }


        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        private void FixedUpdate()
        {
            SetVelocity(_direction);
        }

        private void SetVelocity(Vector3 moveDirection)
        {
            var currentSpeed = _speed * _speedMultiplier;
            var step = currentSpeed * Time.fixedDeltaTime;
            var moveDelta = Vector3.MoveTowards(transform.position, transform.position + moveDirection.normalized, step);
           _rigidbody.MovePosition(moveDelta);
        }
    }
}

