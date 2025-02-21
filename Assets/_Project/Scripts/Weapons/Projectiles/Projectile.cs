using UnityEngine;

namespace BrackeysJam.Weapons.Projectiles
{
    public class Projectile : MonoBehaviour, IProjectile
    {
        [SerializeField] private float _speed;
        [SerializeField] private float _maxTime;

        [SerializeField] private Rigidbody2D _rigidbody;

        [SerializeField] private ParticleSystem _hitEffect;
        
        private Vector3 _targetPosition;

        public void SetStartPosition(Vector2 position)
        {
            transform.position = position;
        }

        public void SetTargetPosition(Vector2 target)
        {
            _targetPosition = target;
        }

        public void Run()
        {
            _rigidbody.linearVelocity = (_targetPosition - transform.position).normalized * _speed;
        }

        private void OnCollisionEnter(Collision other)
        {
            Instantiate(_hitEffect, transform);
            Destroy(gameObject, _hitEffect.main.duration);
        }
    }
}