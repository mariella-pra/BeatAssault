using UnityEngine;


[CreateAssetMenu(fileName = "ActivateBalls", menuName = "Abilities/ActivateBalls")]
public class ActivateBalls : Abilities
{
    public override void Use(GameObject _player, PlayerController _playerController, int value)
    {
        BallGroup ballGroup = FindObjectOfType<BallGroup>();

        // ballGroup.AddToList();
        ballGroup.AssignPlayerShooting(_playerController);
        // ballGroup.nextBall = int.Parse(_player.GetComponent<PlayInstrument>().currentSound);
        ballGroup.activeBalls++;
        ballGroup.AddBall();
        
        // newBall.SetActive(true);
        //     ballGroup.GetComponent<BallGroup>().AssignPlayerShooting(gameObject.GetComponent<PlayerController>());
        //     ballGroup.GetComponent<BallGroup>().nextBall = int.Parse(currentSound);
        //     ballGroup.GetComponent<BallGroup>().AddBall();
    }
}