using System;
using BrackeysJam.Core;
using BrackeysJam.Core.SOs;
using BrackeysJam.EnemyController.SOs;
using BrackeysJam.PlayerController;
using gishadev.tools.StateMachine;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace BrackeysJam.EnemyController
{
    [RequireComponent(typeof(EnemyMovementController))]
    public abstract class Enemy : MonoBehaviour, IDamageableWithPhysicsImpact
    {
        [SerializeField, Required] private EnemyMovementController _enemyMovementController;
        [SerializeField, Required] private Rigidbody2D _rigidbody;
        [SerializeField, Required] private SpriteRenderer _spriteRenderer;

        [Inject] private GameMasterDataSO _gameMasterDataSO;
        
        protected StateMachine StateMachine;

        public abstract EnemyDataSO EnemyDataSO { get; protected set; }

        public event Action<int> HealthChanged;
        public int StartHealth => EnemyDataSO.StartHealth;
        protected EnemyMovementController EnemyMovementController => _enemyMovementController;

        public int CurrentHealth { get; private set; }
        public PhysicsImpactEffector PhysicsImpactEffector { get; private set; }
        public Vector2 StartPosition { get; private set; }
        public Player Player { get; private set; }

        private EnemyMaterialController _enemyMaterialController;

        protected virtual void Awake()
        {
            Initialize();
        }

        private void Start()
        {
            OnSpawned();
            PhysicsImpactEffector = new PhysicsImpactEffector(EnemyMovementController.Rigidbody, EnemyMovementController);
        }

        private void Initialize()
        {
            _enemyMovementController.Initialize(_rigidbody, _spriteRenderer);
            _enemyMaterialController = new EnemyMaterialController(_spriteRenderer, _gameMasterDataSO);
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            _enemyMovementController = GetComponentInChildren<EnemyMovementController>();
            _rigidbody = GetComponentInChildren<Rigidbody2D>();
            _spriteRenderer = GetComponentInChildren<SpriteRenderer>();

            UnityEditor.EditorUtility.SetDirty(this);
        }
#endif

        private void OnEnable()
        {
            Player = GameObject.FindGameObjectWithTag(Constants.PLAYER_TAG_NAME).GetComponent<Player>();
        }

        private void OnDisable() => StateMachine.CurrentState.OnExit();
        private void Update() => StateMachine.Tick();
        
        public void OnSpawned()
        {
            CurrentHealth = EnemyDataSO.StartHealth;
            StartPosition = transform.position;

            InitStateMachine();
            HandleElite();
        }

        private void HandleElite()
        {
            if (EnemyDataSO.IsElite)
            {
                transform.localScale = Vector3.one * 2f;
                _enemyMaterialController.SetOutlineMaterial();
            }
            else
            {
                transform.localScale = Vector3.one;
                _enemyMaterialController.SetDefaultMaterial();
            }
        }

        protected abstract void InitStateMachine();

        public virtual void TakeDamage(int count)
        {
            CurrentHealth -= count;
            Debug.Log($"{count}/{EnemyDataSO.StartHealth}");
            HealthChanged?.Invoke(CurrentHealth);
        }

        public void SetData(EnemyDataSO enemyData)
        {
            EnemyDataSO = enemyData;

            if (enemyData.IsElite)
                Debug.Log(("boss"));
        }

        protected bool IsInStartArea() =>
            Vector3.Distance(transform.position, StartPosition) < EnemyDataSO.StartAreaRadius;

        protected bool InSightWithPlayer() =>
            GetDistanceToPlayer() < EnemyDataSO.FollowRadius;

        protected bool InMeleeAttackReachWithPlayer() =>
            GetDistanceToPlayer() < EnemyDataSO.MeleeAttackRadius;

        protected float GetDistanceToPlayer() => Vector3.Distance(Player.transform.position, transform.position);


        protected virtual void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, EnemyDataSO.MeleeAttackRadius);

            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(StartPosition, EnemyDataSO.StartAreaRadius);

            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, EnemyDataSO.FollowRadius);
        }
    }
}