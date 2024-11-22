using System;
using Datas;
using EnemyScripts;
using SFX;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Weapons
{
    public class CloseAttackWeapon : WeaponService
    {

        [SerializeField] private WeaponData weaponData;
        [SerializeField] private EnemyDamager damager;

        private IObjectResolver _resolver;
        private float _attackCounter, _direction;

        [Inject]
        public void Construct(IObjectResolver resolver)
        {
            _resolver = resolver;
        }

        private void Start()
        {
           
        }
        
        private void Update()
        {
            SetStats();
            CloseAttackActive();
        }
        
        
        private void CloseAttackActive()
        {
            _attackCounter -= Time.deltaTime;
            if (_attackCounter <= 0)
            {
                _attackCounter = weaponStats[weaponLevel].timeBetweenAttacks;
                    
                _direction = Input.GetAxisRaw("Horizontal");
                
                if (_direction != 0)
                {
                    if (_direction > 0)
                        damager.transform.rotation = Quaternion.identity;
                    else
                       damager.transform.rotation = Quaternion.Euler(0, 0, 180);
                }
                
                _resolver.Instantiate(damager, damager.transform.position, damager.transform.rotation, transform).gameObject.SetActive(true);

                for (var i = 1; i < weaponStats[weaponLevel].amount; i++)
                {
                    var rot = (360f / weaponStats[weaponLevel].amount) * i;
                    
                    _resolver.Instantiate(damager, damager.transform.position, Quaternion.Euler(0f,0f,damager.transform.rotation.
                        eulerAngles.z + rot), transform).gameObject.SetActive(true);
                    
                    SFXManager.instance.PlaySfxPitched(9);
                }
            }
        }

        private void SetStats()
        {
            weaponData.damage = weaponStats[weaponLevel].damage;
            damager.weaponLifeTime = weaponStats[weaponLevel].duration;
            damager.transform.localScale = Vector3.one * weaponStats[weaponLevel].range;
        }


    }
}