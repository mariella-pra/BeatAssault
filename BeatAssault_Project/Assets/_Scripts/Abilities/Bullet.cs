// using UnityEngine;
// public class Bullet : MonoBehaviour, IDamageMaker
// {
//     [SerializeField] private float bulletSpeed;
//     [SerializeField] private float damage;
//     [SerializeField] private LayerMask groundLm;
//     private Vector3 _currentDirection;
//     private PlayerController _shooter;
//     private Rigidbody _rigidbody;
//     public void AssignShooter(PlayerController shooter)
//     {
//         _shooter = shooter;
//     }
//     private void Start()
//     {
//         _rigidbody = GetComponent<Rigidbody>();
//         _currentDirection = _shooter.transform.forward.normalized;
//         _rigidbody.velocity = _currentDirection * bulletSpeed;
//     }
//     private void Update()
//     {
//         UpdateMovement();
//     }
//     private void UpdateMovement()
//     {
//         if (Physics.Raycast(transform.position, -Vector3.up, out RaycastHit hit, 100f, groundLm))
//         {
//             Quaternion rotationToSurface = Quaternion.FromToRotation(Vector3.up, hit.normal);
//             transform.rotation = Quaternion.LookRotation(_currentDirection) * rotationToSurface;
//             _currentDirection = Vector3.ProjectOnPlane(_currentDirection, hit.normal).normalized;
//             _rigidbody.velocity = _currentDirection * bulletSpeed;
//         }
//     }
//     public void DealDamage(IHitable target, float damage)
//     {
//         target.Hit(damage);
//     }
//     private void OnTriggerEnter(Collider other)
//     {
//         if (other.TryGetComponent(out IHitable hitable) && hitable != _shooter)
//         {
//             DealDamage(hitable, damage);
//             Destroy(gameObject);
//         }
//         else Destroy(gameObject);
//     }
// }


using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] Transform bulletDir;
    [SerializeField] float _bulletSpeed;
    [SerializeField] int damage;
    [SerializeField] public LayerMask groundLM;
    private Vector3 _initialDirection;
    private Vector3 _currentDirection;

    private PlayerController _playerShooting;
    public void AssignPlayerShooting(PlayerController _player)
    {
        _playerShooting = _player;
    }
    
    void Start()
    {
        _currentDirection = _playerShooting.transform.forward.normalized;
        GetComponent<Rigidbody>().velocity = _currentDirection * _bulletSpeed;
    }

    void Update()
    {
        RaycastHit hit;
        bool floor = Physics.Raycast(transform.position, -Vector3.up, out hit, 100f, groundLM);

        if (floor)
        {
            Quaternion rotationToSurface = Quaternion.FromToRotation(Vector3.up, hit.normal);
            transform.rotation = Quaternion.LookRotation(_currentDirection) * rotationToSurface;
            _currentDirection = Vector3.ProjectOnPlane(_currentDirection, hit.normal).normalized;
            GetComponent<Rigidbody>().velocity = _currentDirection * _bulletSpeed;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerController player = other.gameObject.GetComponent<PlayerController>();
            if (player is not null && player != _playerShooting)
            {
                player.GetComponent<PlayerHealthControl>().DamagePlayer(damage);
                Destroy(gameObject);
            }
        }
        if (!other.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
        
    }
}