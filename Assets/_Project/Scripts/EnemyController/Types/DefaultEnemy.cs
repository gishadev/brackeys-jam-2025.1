﻿using System;
using gishadev.tools.StateMachine;
using Sirenix.OdinInspector;
using UnityEngine;

namespace BrackeysJam.EnemyController
{
    public class DefaultEnemy : Enemy
    {
        [field: SerializeField, InlineEditor] public DefaultEnemyDataSO EnemyData { get; private set; }

        public override EnemyDataSO EnemyDataSO
        {
            get => EnemyData;
            protected set => EnemyData = (DefaultEnemyDataSO) value;
        }

        protected override void InitStateMachine()
        {
            StateMachine = new StateMachine();

            var meleeAttack = new MeleeAttack(this, EnemyMovement);
            var die = new Die(this);

            #region Idle/Follow/Return

            var idle = new Idle(EnemyMovement);
            var follow = new Follow(this, EnemyMovement);
            var returnToStart = new ReturnToStart(this, EnemyMovement);
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