using UnityEngine;

[CreateAssetMenu(fileName = "PianoWallIndicator", menuName = "Abilities/PianoWallIndicator")]
public class PianoWallIndicatorAbility : Abilities
{
    [SerializeField] private GameObject pianoWallIndicatorPrefab;
    public override void Use(GameObject _player, PlayerController _playerController, int value)
    {
        Vector3 pos = new Vector3(_player.gameObject.transform.position.x, _player.gameObject.transform.position.y - 1f,
            _player.gameObject.transform.position.z);
        GameObject newPianoWallIndicator = Instantiate(pianoWallIndicatorPrefab, pos,
            _player.gameObject.transform.rotation);
        PlayerVariables _variables = _player.GetComponent<PlayerVariables>();
        _variables.selectedAbilityIndex += 1;
        _variables._pianoWallIndicatorActive = true;
        newPianoWallIndicator.GetComponent<PianoWallIndicator>().AssignPlayerShooting(_playerController);
        newPianoWallIndicator.GetComponent<PianoWallIndicator>().SetGameObject();
    }
}