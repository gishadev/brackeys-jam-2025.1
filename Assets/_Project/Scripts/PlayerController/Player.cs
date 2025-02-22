using System;
using BrackeysJam.Core;
using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;
using UnityEngine;

namespace BrackeysJam.PlayerController
{
    public partial class Player : MonoBehaviour
    {
        [Header("Data")]
        [SerializeField] private float _speed;

        [Header("Controllers")] 
        [SerializeField, Required] private PlayerMovementController _movementController;
        [SerializeField, Required] private PlayerAttackController _attackController;

        [Header("Native Components")] 
        [SerializeField, Required] private Rigidbody2D _rb;
        [SerializeField, Required] private SpriteRenderer _spriteRenderer;
        
        public Rigidbody2D Rigidbody => _rb;

        private async UniTaskVoid Awake()
        {
            await Initialize();
        }
        
        public async UniTask Initialize()
        {
            _movementController.Initialize(_speed, Rigidbody, _spriteRenderer);
            _attackController.Initialize();
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

    // TODO: remove partial after merge.
    public partial class Player
    {
        [field: SerializeField] public int StartHealth { get; private set; } = 20;

        public int CurrentHealth { get; private set; }
        public PhysicsImpactEffector PhysicsImpactEffector { get; private set; }

        public static event Action Died;
        public event Action<int> HealthChanged;
        private bool _isAlive = true;

        private void Start()
        {
            PhysicsImpactEffector = new PhysicsImpactEffector(_rb, _movementController);
        }

        private void OnEnable()
        {
            CurrentHealth = StartHealth;
        }

        public void TakeDamage(int count)
        {
            if (!_isAlive)
                return;

            CurrentHealth -= count;

            Debug.Log($"Current health count: <color=red>{CurrentHealth}</color>");
            if (CurrentHealth <= 0)
            {
                CurrentHealth = 0;
                Die();
            }

            HealthChanged?.Invoke(CurrentHealth);
        }

        private void Die()
        {
            _isAlive = false;
            Died?.Invoke();
            gameObject.SetActive(false);
        }
    }
}