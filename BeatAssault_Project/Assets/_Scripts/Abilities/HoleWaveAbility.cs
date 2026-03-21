using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "HoleWave", menuName = "Abilities/HoleWave")]
public class HoleWaveAbility : Abilities
{
    [SerializeField] private GameObject holeWavePrefab;
    public override void Use(GameObject _player, PlayerController _playerController, int value)
    {
        // PlayerVariables _variables = _player.GetComponent<PlayerVariables>();
        GameObject newHoleWave = Instantiate(holeWavePrefab, _player.gameObject.transform.position,
            _player.gameObject.transform.rotation);
        newHoleWave.GetComponent<HoleWave>().AssignPlayerShooting(_playerController);
    }
}