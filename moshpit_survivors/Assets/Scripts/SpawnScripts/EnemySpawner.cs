using System;
using System.Collections.Generic;
using PlayerScripts;
using UnityEngine;
using VContainer;
using VContainer.Unity;
using Random = UnityEngine.Random;

namespace SpawnScripts
{
    public class EnemySpawner : MonoBehaviour
    {

        [SerializeField] private Transform minSpawnPoint, maxSpawnPoint;
        [SerializeField] private Transform target;
        [SerializeField] private int checkPerFrame;
        [SerializeField] private Transform parent;
        
        public List<WaveService> waves;
        
        private readonly List<GameObject> _spawnedEnemies = new List<GameObject>();
        
        private IObjectResolver _objectResolver;
        private WaveService _waveService;
        private PlayerController _playerController;
        private float _spawnCounter;
        private float _despawnDistance;
        private float _waveCounter;
        private int _currentWave = -1;
        private int _enemyToCheck;
        
        
        [Inject]
        public void Construct(IObjectResolver objectResolver,WaveService waveService,PlayerController playerController)
        {
            _objectResolver = objectResolver;
            _waveService = waveService;
            _playerController = playerController;
        }
        
        private void Start()
        {
            _spawnCounter = 1;
            _despawnDistance = Vector3.Distance(transform.position, maxSpawnPoint.position) + 4f;
        }

        private void Update()
        {
            
            if (!_playerController.gameObject.activeSelf) return;
            
            EnemySpawn();
            WaveControl();
            
        }

        private void EnemySpawn()
        {
            _spawnCounter -= Time.deltaTime; 
            if (_spawnCounter <= 0)
            {
                _spawnCounter = waves[_currentWave].timeBetweenSpawns;
                var newEnemy = _objectResolver.Instantiate(waves[_currentWave].enemyToSpawn, SelectSpawnPoint(), Quaternion.identity);
                _spawnedEnemies.Add(newEnemy);
                newEnemy.transform.SetParent(parent);
            }
            
            transform.position = target.position;
            
            DespawnEnemy();
        }

        private void DespawnEnemy()
        {
            var checkTarget = _enemyToCheck + checkPerFrame;

            while (_enemyToCheck < checkTarget)
            {
                if (_enemyToCheck < _spawnedEnemies.Count)
                {
                    if (_spawnedEnemies[_enemyToCheck] != null)
                    {
                        if (Vector3.Distance(transform.position, _spawnedEnemies[_enemyToCheck].transform.position) > _despawnDistance)
                        {
                            Destroy(_spawnedEnemies[_enemyToCheck]);
                            
                            _spawnedEnemies.RemoveAt(_enemyToCheck);
                            checkTarget--;
                        }
                        else
                        {
                            _enemyToCheck++;
                        }
                    }
                    else
                    {
                        _spawnedEnemies.RemoveAt(_enemyToCheck);
                        checkTarget--;
                    }
                }
                else
                {
                    _enemyToCheck = 0;
                    checkTarget = 0;
                }
            }
        }

        private void WaveControl()
        {
            if (_currentWave < waves.Count)
            {
                _waveCounter -= Time.deltaTime;
                if (_waveCounter <= 0)
                {
                    NextWave();
                }
            }
        }

        private void NextWave()
        {
            _currentWave++;

            if (_currentWave >= waves.Count)
            {
               _currentWave = 0;
            }

            _waveCounter = waves[_currentWave].waveLength;
            _spawnCounter = waves[_currentWave].timeBetweenSpawns;
        }

        private Vector3 SelectSpawnPoint()
        {
            var spawnPoint = Vector3.zero;
            
            var spawnVerticalEdge = Random.Range(0f, 1f) > .5f;
            
            if (spawnVerticalEdge)
            {
                spawnPoint.y = Random.Range(minSpawnPoint.position.y, maxSpawnPoint.position.y);
                
                if (Random.Range(0f,1f) > .5f)
                {
                    spawnPoint.x = maxSpawnPoint.position.x;
                }
                else
                {
                    spawnPoint.x = minSpawnPoint.position.x;
                }
            }
            else
            {
                spawnPoint.x = Random.Range(minSpawnPoint.position.x, maxSpawnPoint.position.x);
                
                if (Random.Range(0f,1f) > .5f)
                {
                    spawnPoint.y = maxSpawnPoint.position.y;
                }
                else
                {
                    spawnPoint.y = minSpawnPoint.position.y;
                }
            }

            return spawnPoint;
        }
    }
}