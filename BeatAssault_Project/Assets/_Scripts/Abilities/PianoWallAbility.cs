using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "PianoWall", menuName = "Abilities/PianoWall")]
public class PianoWallAbility : Abilities
{
    [SerializeField] private GameObject pianoWallPrefab;
    public override void Use(GameObject _player, PlayerController _playerController, int value)
    {
        PlayerVariables _variables = _player.GetComponent<PlayerVariables>();
        GameObject newPianoWall = Instantiate(pianoWallPrefab, _variables._currentPianoWall.gameObject.transform.position,
            _variables._currentPianoWall.gameObject.transform.rotation);
        // _variables._currentPianoWall = null;
        // _variables.selectedAbilityIndex -= 1;
        // _variables._pianoWallIndicatorActive = false;
        newPianoWall.transform.GetChild(0).transform.GetChild(0).GetComponent<PianoWall>().AssignPlayerShooting(_playerController);
    }
}