using UnityEngine;

[CreateAssetMenu(fileName = "ShockwaveAbility", menuName = "Abilities/ShockwaveAbility")]
public class ShockwaveAbility : Abilities
{
    [SerializeField] private GameObject ShockwavePrefab;
    public override void Use(GameObject _player, PlayerController _playerController, int value)
    {
        Vector3 pos = _player.GetComponent<PlayerVariables>().feet.transform.position;
        GameObject newShockwavePrefab = Instantiate(ShockwavePrefab, pos, _player.gameObject.transform.rotation);
        newShockwavePrefab.GetComponent<Shockwave>().AssignPlayerShooting(_playerController);
        // var vector3 = newShockwavePrefab.transform.position;
        // vector3.y = newShockwavePrefab.transform.position.y - 1f;
        // newShockwavePrefab.transform.position = vector3;
    }
}