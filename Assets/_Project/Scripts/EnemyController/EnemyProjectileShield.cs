using System.Collections.Generic;
using BrackeysJam.Core;
using BrackeysJam.PlayerController;
using UnityEngine;

namespace BrackeysJam.EnemyController
{
    public class EnemyProjectileShield : MonoBehaviour
    {
        [SerializeField] private Collider2D _projectileShieldCollider;

        private Transform _playerTrans;
        private float Radius => transform.localScale.x / 2f * transform.parent.localScale.x;

        private void OnEnable()
        {
            _playerTrans = GameObject.FindGameObjectWithTag(Constants.PLAYER_TAG_NAME).transform;
        }

        private List<EnemyProjectileShield> GetNearbyShields()
        {
            var shields = FindObjectsByType<EnemyProjectileShield>(FindObjectsSortMode.None);
            List<EnemyProjectileShield> result = new List<EnemyProjectileShield>();

            foreach (var shield in shields)
            {
                var distance = Vector3.Distance(shield.transform.position, transform.position) - Radius;
                distance = Mathf.Max(0f, distance);
                if (distance < Radius)
                    result.Add(shield);
            }

            return result;
        }

        private bool IsPlayerInsideOneOfShields(List<EnemyProjectileShield> shields)
        {
            foreach (var shieldToCheck in shields)
            {
                var distance = Vector3.Distance(shieldToCheck.transform.position, _playerTrans.transform.position);
                if (distance < Radius)
                    return true;
            }

            return false;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.TryGetComponent(out Player _) && !other.TryGetComponent(out EnemyProjectileShield _))
                return;

            var nearbyShields = GetNearbyShields();
            foreach (var shield in nearbyShields) shield._projectileShieldCollider.enabled = false;
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (!other.TryGetComponent(out Player _) && !other.TryGetComponent(out EnemyProjectileShield _))
                return;

            var nearbyShields = GetNearbyShields();
            var isInside = IsPlayerInsideOneOfShields(nearbyShields);
            if (!isInside)
                foreach (var shield in nearbyShields)
                    shield._projectileShieldCollider.enabled = true;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.magenta;
            Gizmos.DrawWireSphere(transform.position, Radius);
        }
    }
}