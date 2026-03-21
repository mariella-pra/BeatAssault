using UnityEngine;


[CreateAssetMenu(fileName = "ShootBalls", menuName = "Abilities/ShootBalls")]
public class ShootBalls : Abilities
{
    public override void Use(GameObject _player, PlayerController _playerController, int value)
    {
        BallGroup ballGroup = FindObjectOfType<BallGroup>();

        ballGroup.rotate = false;
        
        // ballGroup.nextBall = int.Parse(_player.GetComponent<PlayInstrument>().currentSound);
        // ballGroup.AddBall();
        ballGroup.ShootBalls();
        // ballGroup.DeleteBallGroup();
        
        // if (_player.GetComponent<PlayInstrument>().bars == 2)
        // {
        //    _player.GetComponent<PlayInstrument>().selectedAbilityIndex -= 2;
        // }
    }
}