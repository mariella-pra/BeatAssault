using UnityEngine;

public abstract class Abilities : ScriptableObject
{
    public abstract void Use(GameObject _player, PlayerController _playerController, int value);
    public virtual void AssignShooter(PlayerController shooter, GameObject instantiatedAbility)
    {
        if (instantiatedAbility.TryGetComponent(out IDamageMaker damageMaker))
        {
            damageMaker.AssignShooter(shooter);
        }
    }
}