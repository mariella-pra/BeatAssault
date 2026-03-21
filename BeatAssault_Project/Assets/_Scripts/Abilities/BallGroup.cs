using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;



    public class BallGroup : MonoBehaviour
    {
        public PlayerController _playerShooting;
        [SerializeField]  List<GameObject> _ballPositions = new List<GameObject>();
        private bool addedToList = false;
        public bool spawned;
        public int nextBall;
        public float rotationSpeed = 10f;
        public bool rotate = true;


        public int activeBalls;
        private void Start()
        {
            gameObject.transform.parent = null;
            
        }

        public void AssignPlayerShooting(PlayerController _player)
        {
            _playerShooting = _player;
        }

        private void Update()
        {
            if(!_playerShooting) DestroyGroup();
            // if(_playerShooting is not null)
            if(_playerShooting) transform.position = _playerShooting.gameObject.transform.position;
            if(rotate)
            {
                Rotate();
            }

            if (_playerShooting && _playerShooting.GetComponent<PlayerVariables>()._curPattern is null)
            {
                DestroyGroup();
            }
            

            // if (_playerShooting.GetComponent<PlayInstrument>().shootBalls)
            // {
            //     _playerShooting.GetComponent<PlayInstrument>().selectedAbilityIndex += 1;
            //     _playerShooting.GetComponent<PlayInstrument>().shootBalls = false;
            // }
        }

        public void Rotate()
        {
            transform.RotateAround(gameObject.transform.position, new Vector3(0, 1, 0), rotationSpeed * Time.deltaTime * 10f);
        }

        public void AddToList()
        {
            if (!addedToList)
            {
                foreach (Transform child in gameObject.transform)
                {
                    _ballPositions.Add(child.gameObject);
                }
                addedToList = true;
            }
        }
        public void AddBall()
        {
            if (activeBalls < _ballPositions.Count)
            {
                _ballPositions[activeBalls].SetActive(true);
            }
            // if (activeBalls < _ballPositions.Count)
            // {
            //     _ballPositions[activeBalls].SetActive(true);
            //     activeBalls++;
            // }

            // if (_playerShooting.GetComponent<PlayInstrument>().shootBalls)
            // {
            //     _playerShooting.GetComponent<PlayInstrument>().selectedAbilityIndex += 1;
            //     _playerShooting.GetComponent<PlayInstrument>().shootBalls = false;
            // }
            // if(_ballPositions[nextBall].name == "SHOOT")
            // {
            //     _playerShooting.GetComponent<PlayInstrument>().selectedAbilityIndex += 1;
            // }
        }
        

        public void ShootBalls()
        {
            // Debug.Log("Shoot");
            for (int i = 0; i < _ballPositions.Count; i++)
            {
                if(_ballPositions[i].transform.parent is not null) _ballPositions[i].transform.parent = null;
                _ballPositions[i].GetComponent<BallBehaviour>().move = true;
                // _ballPositions.RemoveAt(i);
                // if (_ballPositions[i].gameObject.name == "SHOOT") break;

                // if (_ballPositions[i].gameObject.name == "SHOOT")
                // {
                //     _playerShooting.GetComponent<PlayInstrument>().selectedAbilityIndex -= 1;
                //     
                //     // for (int j = 0; j < i; j++)
                //     // {
                //     //     _ballPositions.Remove(_ballPositions[j]);
                //     // }
                //     break;
                // }
                // _ballPositions.Remove(_ballPositions[i]);

            }

            _playerShooting.GetComponent<PlayerVariables>().selectedAbilityIndex -= 2;
            // addedToList = false;    
            // AddToList();
        }

        public void DeleteBallGroup()
        {
            StartCoroutine(Delete());
        }
        public void DestroyGroup()
        {
            Destroy(gameObject);
        }

        IEnumerator Delete()
        {
            yield return new WaitForSeconds(10f);
            Destroy(gameObject);
        }
        
    }
