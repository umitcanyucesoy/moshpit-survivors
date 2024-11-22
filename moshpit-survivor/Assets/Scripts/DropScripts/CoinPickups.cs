using Datas;
using PlayerScripts;
using UnityEngine;
using VContainer;

namespace DropScripts
{
    public class CoinPickups : MonoBehaviour
    {
        [SerializeField] private float timeBetweenChecks;
        [SerializeField] private float expMoveSpeed;
        
        public int coinAmount = 1;

        private PlayerController _playerController;
        private DropController _dropController;
        private PlayerData _playerData;
        private Transform _target;
        private bool _movingToPlayer;
        private float _checkCounter;
        

        [Inject]
        public void Construct(DropController dropController,PlayerController playerController,PlayerData playerData)
        {
            _playerController = playerController;
            _playerData = playerData;
            _target = playerController.transform;
            _dropController = dropController;
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
               _dropController.AddCoin(coinAmount);
                
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