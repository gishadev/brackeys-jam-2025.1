using System;
using BrackeysJam.Core;
using BrackeysJam.EnemyController.SOs;
using BrackeysJam.PlayerController;
using gishadev.tools.StateMachine;
using Sirenix.OdinInspector;
using UnityEngine;

namespace BrackeysJam.EnemyController
{
    [RequireComponent(typeof(EnemyMovement))]
    public abstract class Enemy : MonoBehaviour, IDamageableWithPhysicsImpact
    {
        [SerializeField, Required] private EnemyMovement _enemyMovement;
        
        public abstract EnemyDataSO EnemyDataSO { get; protected set; }

        public event Action<int> HealthChanged;
        public int StartHealth => EnemyDataSO.StartHealth;
        public int CurrentHealth { get; private set; }
        public PhysicsImpactEffector PhysicsImpactEffector { get; private set; }
        public Vector2 StartPosition { get; private set; }
        public Player Player => _player;
        protected StateMachine StateMachine;
        protected EnemyMovement EnemyMovement { get; private set; }

        private Player _player;

        protected virtual void Awake()
        {
            EnemyMovement = GetComponent<EnemyMovement>();
        }

        private void Start()
        {
            OnSpawned();
            PhysicsImpactEffector = new PhysicsImpactEffector(EnemyMovement.Rigidbody, EnemyMovement);
        }

        public void OnSpawned()
        {
            CurrentHealth = EnemyDataSO.StartHealth;
            StartPosition = transform.position;

            InitStateMachine();

            if (EnemyDataSO.IsBoss)
                transform.localScale = Vector3.one * 2f;
            else
                transform.localScale = Vector3.one;
        }

        private void OnEnable()
        {
            _player = GameObject.FindGameObjectWithTag(Constants.PLAYER_TAG_NAME).GetComponent<Player>();
        }

        private void OnDisable() => StateMachine.CurrentState.OnExit();
        private void Update() => StateMachine.Tick();

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

            if (enemyData.IsBoss)
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