namespace BrackeysJam.Core
{
    public interface IDamageableWithPhysicsImpact : IDamageable
    {
        PhysicsImpactEffector PhysicsImpactEffector { get; }
    }
}