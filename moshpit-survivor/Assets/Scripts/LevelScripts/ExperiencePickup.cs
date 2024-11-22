using System;
using Datas;
using PlayerScripts;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace LevelScripts
{
    public class ExperiencePickup : MonoBehaviour
    {
        [SerializeField] private float timeBetweenChecks;
        [SerializeField] private float expMoveSpeed;
        
        public int expValue;

        private PlayerController _playerController;
        private LevelController _levelController;
        private PlayerData _playerData;
        private Transform _target;
        private bool _movingToPlayer;
        private float _checkCounter;
        

        [Inject]
        public void Construct(LevelController levelController, PlayerController playerController,PlayerData playerData)
        {
            _levelController = levelController;
            _playerController = playerController;
            _playerData = playerData;
            _target = playerController.transform;
        }

        private void Update()
        {
            ExpDistance();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            _playerController = other.GetComponent<PlayerController>();

            if (_playerController)
            {
                _levelController.GetExp(expValue);
                
                Destroy(gameObject);
            }
        }

        private void ExpDistance()
        {
            if (_movingToPlayer)
            {
                transform.position = Vector3.MoveTowards(transform.position, _target.position,
                    expMoveSpeed * Time.deltaTime);
            }
            else
            {
                _checkCounter -= Time.deltaTime;
                if (_checkCounter <= 0)
                {
                    _checkCounter = timeBetweenChecks;

                    if (Vector3.Distance(transform.position,_target.position) < _playerData.pickupRange)
                    {
                        _movingToPlayer = true;
                        expMoveSpeed += _playerData.moveSpeed;
                    }
                }
            }
        }

        
    }
}