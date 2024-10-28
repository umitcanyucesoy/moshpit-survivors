using Datas;
using Interfaces;
using PlayerScripts;
using UnityEngine;

namespace EnemyScripts
{
    public class EnemyAttack : MonoBehaviour,IAttackable
    {
        [SerializeField] private EnemyData enemyData;
        [SerializeField] private float hitWaitTime;

        private PlayerHealthController _playerHealthController;
        private float _hitCounter;

        private void Update()
        {
            WaitForHit();
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            var playerHealthController = other.gameObject.GetComponent<PlayerHealthController>();
            
            if (playerHealthController && _hitCounter <= 0f)
            {
                _playerHealthController = playerHealthController;
                
                HitDamage(enemyData.attackDamage,false);
                _hitCounter = hitWaitTime;
            }
        }

        public void HitDamage(float damage,bool knockBack)
        {
            _playerHealthController.TakeDamage(damage);
        }

        private void WaitForHit()
        {
            if (_hitCounter > 0f)
            {
                _hitCounter -= Time.deltaTime;
            }
        }
    }
}