using System;
using BrackeysJam.Core;
using UnityEngine;

namespace BrackeysJam.Weapons.Projectiles
{
    public class Projectile : MonoBehaviour, IProjectile
    {
        public event Action<IDamageable> OnHit;
        
        [SerializeField] private float _speed;
        [SerializeField] private float _maxTime;

        [SerializeField] private Rigidbody2D _rigidbody;

        [SerializeField] private ParticleSystem _hitEffect;
        
        protected Vector3 _targetPosition;

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
            transform.parent = null;
            transform.gameObject.SetActive(true);

            MoveToTarget();
        }

        protected virtual void MoveToTarget()
        {
            _rigidbody.linearVelocity = (_targetPosition - transform.position).normalized * _speed;
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            OnHit?.Invoke(other.gameObject.GetComponent<IDamageable>());
            gameObject.SetActive(false);

            var hit = Instantiate(_hitEffect, transform.position, Quaternion.identity);
            Destroy(hit.gameObject, _hitEffect.main.duration);
        }
    }
}