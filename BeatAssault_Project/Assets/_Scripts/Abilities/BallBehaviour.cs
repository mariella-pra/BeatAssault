using System;
using UnityEngine;
using DG.Tweening;

    public class BallBehaviour : MonoBehaviour
    {
        public int damage;
        public bool move;
        public float moveSpeed = 2f;
        private PlayerController _playerShooting;
        public bool hasBalls;
        
        public void AssignPlayerShooting(PlayerController _player)
        {
            _playerShooting = _player;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                PlayerController player = other.gameObject.GetComponent<PlayerController>();
                if (player is not null && player != _playerShooting)
                {
                    player.GetComponent<PlayerHealthControl>().DamagePlayer(damage);
                    gameObject.SetActive(false);
                    // Destroy(gameObject);
                }
            }
        }

        private void Update()
        {
            if (move)
            {
                transform.position += transform.forward * (Time.deltaTime * moveSpeed);
            }
        }
    }
