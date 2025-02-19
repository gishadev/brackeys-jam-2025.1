using System;
using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;
using UnityEngine;

namespace BrackeysJam.PlayerController
{
    public class Player : MonoBehaviour
    {
        [Header("Data")]
        [SerializeField] private float _speed;

        [Header("Controllers")] 
        [SerializeField, Required] private PlayerMovementController _movementController;
        [SerializeField, Required] private PlayerAttackController _attackController;

        [Header("Native Components")] 
        [SerializeField, Required] private Rigidbody2D _rb;

        public Rigidbody2D Rigidbody => _rb;

        private async UniTaskVoid Awake()
        {
            await Initialize();
        }

        public async UniTask Initialize()
        {
            _movementController.Initialize(_speed, Rigidbody);
            await _attackController.Initialize();
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            _movementController = GetComponent<PlayerMovementController>();
            _attackController = GetComponent<PlayerAttackController>();
            _rb = GetComponent<Rigidbody2D>();

            UnityEditor.EditorUtility.SetDirty(this);
        }
#endif
    }
}