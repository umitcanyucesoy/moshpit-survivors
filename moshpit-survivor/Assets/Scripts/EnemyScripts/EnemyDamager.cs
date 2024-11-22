using System;
using System.Collections.Generic;
using Datas;
using EnemyScripts;
using Interfaces;
using UnityEngine;
using UnityEngine.Serialization;

namespace EnemyScripts
{
    public class EnemyDamager : MonoBehaviour,IAttackable
    {

        [Header("----- WEAPON SETTINGS -----")]
        [SerializeField] private WeaponData weaponData;
        [SerializeField] private float growSpeed;
        [SerializeField] private bool shouldKnockBack;
        [SerializeField] private bool damageOverTime = false;
        
        [Header("----- DAMAGE SETTINGS -----")]
        public float timeBetweenDamage;
        public float weaponLifeTime;
        public bool destroyOnImpact;
        public bool destroyHolder;
        
        private readonly List<EnemyHealthController> _enemiesInRange = new List<EnemyHealthController>();
        
        private EnemyHealthController _enemyHealthController;
        private Vector3 _targetSize;
        private float _damageCounter;
        
        private void Start()
        {
            _targetSize = transform.localScale;
            transform.localScale = Vector3.zero;
        }

        private void Update()
        {
            WeaponGrowth();
            WeaponZone();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            var enemyHealthController = other.gameObject.GetComponent<EnemyHealthController>();
            
            if (!damageOverTime)
            {
                if (enemyHealthController)
                {
                    _enemyHealthController = enemyHealthController;
                    HitDamage(weaponData.damage,shouldKnockBack);

                    if (destroyOnImpact)
                    {
                        Destroy(gameObject);
                    }
                } 
            }
            else
            {
                if (enemyHealthController)
                {
                    _enemyHealthController = enemyHealthController;
                    _enemiesInRange.Add(other.GetComponent<EnemyHealthController>());
                }
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (_enemiesInRange.Contains(_enemyHealthController))
                _enemiesInRange.Remove(_enemyHealthController);
        }

        public void HitDamage(float damage,bool knockBack)
        {
            _enemyHealthController.TakeDamage(damage, knockBack);
        }

        private void WeaponGrowth()
        {
            transform.localScale = Vector3.MoveTowards(transform.localScale, _targetSize, growSpeed * Time.deltaTime);

            weaponLifeTime -= Time.deltaTime;
            if (weaponLifeTime <= 0)
            {
                _targetSize = Vector3.zero;

                if (transform.localScale.x == 0f)
                {
                    if (destroyHolder)
                    {
                        Destroy(transform.parent.gameObject);
                    }
                    else
                    {
                        Destroy(gameObject);
                    }
                }
            }
            
        }
        
        private void WeaponZone()
        {
            _damageCounter -= Time.deltaTime;
            if (_damageCounter <= 0)
            {
                _damageCounter = timeBetweenDamage;

                for (var i = 0; i < _enemiesInRange.Count; i++)
                {
                    if (_enemiesInRange[i] != null)
                    {
                        _enemiesInRange[i].TakeDamage(weaponData.damage, shouldKnockBack);
                    }
                    else
                    {
                        _enemiesInRange.RemoveAt(i);
                        i++;
                    }
                }
            }
        }
    }
}