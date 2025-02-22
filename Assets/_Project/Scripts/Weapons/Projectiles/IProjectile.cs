using UnityEngine;

namespace BrackeysJam.Weapons.Projectiles
{
    public interface IProjectile
    {
        public Transform transform { get; }
        public void SetStartPosition(Vector2 position);
        public void SetTargetPosition(Vector2 position);
        public void Run();
    }
}