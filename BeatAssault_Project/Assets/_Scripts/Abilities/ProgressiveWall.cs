using UnityEngine;


[CreateAssetMenu(fileName = "ProgressiveWall", menuName = "Abilities/ProgressiveWall")]
public class ProgressiveWall : Abilities
{
    [SerializeField] private GameObject wallPrefab;
    [SerializeField] private GameObject wallParticlePrefab;
    public override void Use(GameObject _player, PlayerController _playerController, int value)
    {
        GameObject newWall = Instantiate(wallPrefab,new Vector3(_player.gameObject.transform.position.x, _player.gameObject.transform.position.y - 1, _player.gameObject.transform.position.z), _player.gameObject.transform.GetChild(0).transform.rotation);
        newWall.GetComponent<ProgressiveWallScript>().AssignPlayerShooting(_playerController);
        GameObject wallP = Instantiate(wallParticlePrefab, newWall.transform.position, Quaternion.Euler(0, _player.gameObject.transform.eulerAngles.y + 90f, 0));
        newWall.GetComponent<ProgressiveWallScript>().particleSystem = wallP;
    }
}   
