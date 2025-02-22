using System;
using BrackeysJam.EnemyController.SOs;
using BrackeysJam.EnemyController.States;
using gishadev.tools.StateMachine;
using Sirenix.OdinInspector;
using UnityEngine;

namespace BrackeysJam.EnemyController
{
    public class TankEnemy : Enemy
    {
        [field: SerializeField, InlineEditor] public TankEnemyDataSO TankData { get; private set; }
        [SerializeField, Required] private EnemyProjectileShield _projectileShield;

        public override EnemyDataSO EnemyDataSO
        {
            get => TankData;
            protected set => TankData = (TankEnemyDataSO)value;
        }

        protected override void Awake()
        {
            base.Awake();
            if (_projectileShield == null)
                _projectileShield = GetComponentInChildren<EnemyProjectileShield>();
            _projectileShield.transform.localScale =
                Vector3.one * TankData.ProjectileShieldRadius * 2f;
        }

        protected override void InitStateMachine()
        {
            StateMachine = new StateMachine();

            var meleeAttack = new MeleeAttack(this, EnemyMovementController);
            var die = new Die(this);

            #region Idle/Follow/Return

            var idle = new Idle(EnemyMovementController);
            var follow = new Follow(this, EnemyMovementController);
            var returnToStart = new ReturnToStart(this, EnemyMovementController);
            At(idle, follow, InSightWithPlayer);
            At(follow, returnToStart, () => !InSightWithPlayer());
            At(returnToStart, idle, IsInStartArea);
            At(returnToStart, follow, InSightWithPlayer);

            #endregion

            At(follow, meleeAttack, InMeleeAttackReachWithPlayer);
            At(meleeAttack, follow, () => !InMeleeAttackReachWithPlayer());

            Aat(die, () => CurrentHealth <= 0);

            StateMachine.SetState(idle);

            void At(IState from, IState to, Func<bool> cond) => StateMachine.AddTransition(from, to, cond);
            void Aat(IState to, Func<bool> cond) => StateMachine.AddAnyTransition(to, cond);
        }
    }
}