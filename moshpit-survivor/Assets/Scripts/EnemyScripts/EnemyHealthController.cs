using System;
using Datas;
using DropScripts;
using Interfaces;
using LevelScripts;
using SFX;
using UI_Scripts;
using UnityEngine;
using VContainer;
using Random = UnityEngine.Random;

namespace EnemyScripts
{
    public class EnemyHealthController : MonoBehaviour,IDamageable
    {

        [SerializeField] private EnemyData enemyData;

        private DamageNumberController _damageNumberController;
        private EnemyController _enemyController;
        private LevelController _levelController;
        private DropController _dropController;
        private float _currentHealth;

        [Inject]
        public void Construct(DamageNumberController damageNumberController,LevelController levelController,DropController dropController) 
        {
            _damageNumberController = damageNumberController;
            _levelController = levelController;
            _dropController = dropController;
        }
        
        private void Awake()
        {
            _enemyController = GetComponent<EnemyController>();
        }

        private void Start()
        {
            _currentHealth = enemyData.health;
        }

        public void TakeDamage(float damageToTake)
        { 
            _currentHealth -= damageToTake;

            if (_currentHealth <= 0)
            {
                Destroy(gameObject);
                
                _levelController.SpawnExp(transform.position,enemyData.experience);
                
                if (Random.value <= enemyData.coinDropRate)
                    _dropController.DropCoin(transform.position,enemyData.coinValue);
                
                SFXManager.instance.PlaySfxPitched(0);
            }
            else
                SFXManager.instance.PlaySfxPitched(1);
            
            
            _damageNumberController.DamageToSpawn(damageToTake,transform.position);
        }

        public void TakeDamage(float damageToTake, bool knockBack)
        {
            TakeDamage(damageToTake);

            if (knockBack)
            {
                _enemyController.knockBackCounter = _enemyController.knockBackTime;
            }
        }
    }
    
}