using System;
using DG.Tweening;
using UnityEngine;
    public class HoleWave : MonoBehaviour
    {
        PlayerController _playerShooting;
        public float damage = .5f;
        public float growVal = 2f;
        public float growTime = 0.8f;
        public float moveVal = 1.5f;
        public float moveTime = 0.5f;
        private Rigidbody rb;
        public void AssignPlayerShooting(PlayerController _player)
        {
            _playerShooting = _player;
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player") &&
                other.gameObject.GetComponent<PlayerController>() != _playerShooting)
            {
                other.gameObject.GetComponent<PlayerController>().GetComponent<PlayerHealthControl>().DamagePlayer(damage);
            }
        }
        public void Grow()
        {
            transform.DOScale(growVal, growTime);
        }
        public void Move()
        {
            transform.DOMove(transform.position + transform.forward * 10f, moveVal);
        }
        public void DestroyWall()
        {
            Destroy(gameObject);
        }
        private void Awake()
        {
            rb = GetComponent<Rigidbody>();

            
            // transform.rotation = _playerShooting.transform.rotation;
            Grow();
            Invoke("DestroyWall", .4f);
        }

        public void FixedUpdate()
        {
            Move();
        }
    }