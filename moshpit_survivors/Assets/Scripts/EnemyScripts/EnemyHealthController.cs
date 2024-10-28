using Datas;
using Interfaces;
using LevelScripts;
using UI_Scripts;
using UnityEngine;
using VContainer;

namespace EnemyScripts
{
    public class EnemyHealthController : MonoBehaviour,IDamageable
    {

        [SerializeField] private EnemyData enemyData;

        private DamageNumberController _damageNumberController;
        private EnemyController _enemyController;
        private LevelController _levelController;
        private float _currentHealth;

        [Inject]
        public void Construct(DamageNumberController damageNumberController,LevelController levelController) 
        {
            _damageNumberController = damageNumberController;
            _levelController = levelController;
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
            }
            
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