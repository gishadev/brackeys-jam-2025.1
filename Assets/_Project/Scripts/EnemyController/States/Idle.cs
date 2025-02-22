using gishadev.tools.StateMachine;

namespace BrackeysJam.EnemyController.States
{
    public class Idle : IState
    {
        private readonly EnemyMovementController _enemyMovementController;

        public Idle(EnemyMovementController enemyMovementController)
        {
            _enemyMovementController = enemyMovementController;
        }

        public void Tick()
        {
        }

        public void OnEnter()
        {
            _enemyMovementController.Stop();
        }

        public void OnExit()
        {
        }
    }
}