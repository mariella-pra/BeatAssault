using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// //
// [CreateAssetMenu(fileName = "Projectile", menuName = "Abilities/Projectile")]
// public class Projectile : Abilities
// {
//     [SerializeField] GameObject _projectilePrefab;
//     public override void Use(GameObject player, PlayerController playerController, int value)
//     {
//         GameObject newBullet = Instantiate(_projectilePrefab,
//             player.transform.position, player.transform.rotation);
//         newBullet.GetComponent<Bullet>().AssignShooter(playerController);
//         // AssignShooter(playerController, newBullet);
//     }
// }

[CreateAssetMenu(fileName = "Projectile", menuName = "Abilities/Projectile")]
public class Projectile : Abilities
{
    [SerializeField] GameObject _projectile;
    public override void Use(GameObject _player, PlayerController _playerController, int value)
    {
        Debug.Log("projectile");
        
        GameObject newBullet = Instantiate(_projectile, _player.gameObject.transform.position, _player.gameObject.transform.rotation);
        newBullet.GetComponent<Bullet>().AssignPlayerShooting(_playerController);
    }
}