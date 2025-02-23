using UnityEngine;

namespace BrackeysJam.EnemyController.Spawning
{
    public interface ISpawningPositionGetter
    {
        Vector3 GetPosition();
    }
}