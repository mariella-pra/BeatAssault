using UnityEngine;

public enum DamageType { InstantDamage, DamageOverTime }
public interface IDamageDealer
{
    void ApplyDamage(PlayerHealthControl target);  
    float DamageAmount { get; }
    DamageType DamageType { get; }
    float TickRate { get; }
}

public interface IDamageMaker
{
    void AssignShooter(PlayerController shooter);
    void DealDamage(IHitable target, float damage);
}

public interface IInstantDamage
{
    void DealInstantDamage(GameObject target);
}
public interface IDamageOverTime
{
    float TickRate { get; }
}
public interface IParticleSpawner
{
    GameObject ParticlePrefab { get; }
    void SpawnParticle(Vector3 position, Quaternion rotation);
}
public interface IHitable
{
    void Hit(float damage);
}