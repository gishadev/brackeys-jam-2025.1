using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace BrackeysJam.PlayerController
{
    public class Player : MonoBehaviour
    {
        [Header("Data")]
        [SerializeField] private float _speed;

        [Header("References")] 
        [SerializeField, Required] private PlayerMovementController _movementController;
        [SerializeField, Required] private Rigidbody2D _rb;

        public Rigidbody2D Rigidbody => _rb;

        private void Awake()
        {
            _movementController.Initialize(_speed, Rigidbody);
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            _movementController = GetComponent<PlayerMovementController>();
            _rb = GetComponent<Rigidbody2D>();

            UnityEditor.EditorUtility.SetDirty(this);
        }
#endif
    }
}