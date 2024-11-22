using System;
using Datas;
using EnemyScripts;
using SFX;
using UnityEngine;
using VContainer;
using VContainer.Unity;
using Random = UnityEngine.Random;

namespace Weapons
{
    public class ProjectileWeapon : WeaponService
    {
        [SerializeField] private WeaponData weaponData;
        [SerializeField] private EnemyDamager damager;
        [SerializeField] private Projectile projectile;

        public float weaponRange;
        public LayerMask whatIsTarget;

        private IObjectResolver _resolver;
        private float _shotCounter;

        [Inject]
        public void Construct(IObjectResolver resolver)
        {
            _resolver = resolver;
        }
        
        private void Update()
        {
            SetStats();
            ProjectileActive();
        }

        private void ProjectileActive()
        {
            _shotCounter -= Time.deltaTime;

            if (_shotCounter <= 0)
            {
                _shotCounter = weaponStats[weaponLevel].timeBetweenAttacks;
                
                var enemies = Physics2D.OverlapCircleAll(transform.position, weaponRange * weaponStats[weaponLevel].range, whatIsTarget);
                if (enemies.Length > 0)
                {
                    for (var i = 0; i < weaponStats[weaponLevel].amount; i++)
                    {
                        var targetPosition = enemies[Random.Range(0, enemies.Length)].transform.position;
                        var direction = targetPosition - transform.position;
                        
                        var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                        angle -= 90f;
                        projectile.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
                        
                        _resolver.Instantiate(projectile, projectile.transform.position, projectile.transform.rotation).gameObject.SetActive(true);
                    }
                    
                    SFXManager.instance.PlaySfxPitched(6);
                }
            }
        }
        
        private void SetStats()
        {
            weaponData.damage = weaponStats[weaponLevel].damage;
            weaponData.moveSpeed = weaponStats[weaponLevel].speed;
            damager.weaponLifeTime = weaponStats[weaponLevel].duration;
            damager.transform.localScale = Vector3.one * weaponStats[weaponLevel].range;
        }
        
    }
}