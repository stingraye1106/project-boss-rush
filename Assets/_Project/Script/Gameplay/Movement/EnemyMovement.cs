using NF.Main.Core;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.AI;

namespace NF.Main.Gameplay.Movement
{
    public class EnemyMovement : MonoExt, IMovement
    {
        [TabGroup("Movement Settings")][SerializeField] private NavMeshAgent _navMeshAgent;
        [TabGroup("Movement Settings")][SerializeField] private bool _canMove;
        [TabGroup("Movement Settings")][SerializeField] private float _speedMultiplier;

        private float _speed;
        private Vector3 _direction;
        private float _destUpdateInterval = 0.1f;
        private float _intervalTimer = 0f;
        private NavMeshPath _path;

        public float Speed { get => _speed; set => _speed = value; }
        public float SpeedMultiplier { get => _speedMultiplier; set => _speedMultiplier = value; }
        public Vector3 Direction { get => _direction; set => _direction = value; }
        public bool CanMove { get => _canMove; set => _canMove = value; }




        private void Awake()
        {
            _path = new NavMeshPath();
        }

        private void OnDisable()
        {
            _intervalTimer = 0f;
        }

        public override void Initialize(object data = null)
        {
            base.Initialize(data);
            UpdateMovementSpeed();
        }

        private void Update()
        {
            if (!_canMove)
            {
                StopMovement();
            } else
            {
                StartMovement();
            }
            RecalculatePath();
        }

        private void StartMovement()
        {
            _navMeshAgent.isStopped = false;
        }

        private void StopMovement()
        {
            _navMeshAgent.isStopped = true;
        }

        private void SetDestination()
        {
            _navMeshAgent.SetDestination(_direction);
        }

        private void RecalculatePath()
        {
            _intervalTimer -= Time.deltaTime;

            if (_intervalTimer <= 0f)
            {
                NavMesh.CalculatePath(_navMeshAgent.transform.position, _direction, NavMesh.AllAreas, _path);
                _navMeshAgent.SetPath(_path);

                _intervalTimer = _destUpdateInterval;
            }
        }

        private void UpdateMovementSpeed()
        {
            _navMeshAgent.speed = _speed * _speedMultiplier;
        }
    }
}

