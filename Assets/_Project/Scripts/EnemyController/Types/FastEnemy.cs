﻿using System;
using gishadev.tools.StateMachine;
using Sirenix.OdinInspector;
using UnityEngine;

namespace BrackeysJam.EnemyController
{
    public class FastEnemy : Enemy
    {
        [field: SerializeField, InlineEditor] public FastEnemyDataSO FastData { get; private set; }

        private int _damageSum;
            
        public override EnemyDataSO EnemyDataSO
        {
            get => FastData;
            protected set => FastData = (FastEnemyDataSO) value;
        }

        public bool Damaged { get; private set; }

        protected override void InitStateMachine()
        {
            StateMachine = new StateMachine();

            var meleeAttack = new MeleeAttack(this, EnemyMovement);
            var flee = new Flee(this, EnemyMovement);
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

            At(flee, follow, IsFleeDelayElapsed);

            Aat(die, () => CurrentHealth <= 0);
            Aat(flee, () => Damaged);

            StateMachine.SetState(idle);

            bool IsFleeDelayElapsed() => flee.GetElapsedTime() > FastData.FleeTime;

            void At(IState from, IState to, Func<bool> cond) => StateMachine.AddTransition(from, to, cond);
            void Aat(IState to, Func<bool> cond) => StateMachine.AddAnyTransition(to, cond);
        }

        public override void TakeDamage(int count)
        {
            base.TakeDamage(count);
            _damageSum += count;
            if (_damageSum >= FastData.MinDamageToFlee)
                Damaged = true;
        }

        public void ResetDamaged()
        {
            _damageSum = 0;
            Damaged = false;
        }
    }
}