namespace BrackeysJam.Weapons.Projectiles
{
    public class InstantHitProjectile : Projectile
    {
        protected override void MoveToTarget()
        {
            transform.position = _targetPosition;
        }
    }
}