using System;
using System.Collections.Generic;
using Datas;
using EnemyScripts;
using SFX;
using UI_Scripts;
using UnityEngine;
using UnityEngine.Serialization;
using VContainer;
using VContainer.Unity;

namespace Weapons._Fireball
{
    public class SpinWeapon : WeaponService
    {

        [SerializeField] private WeaponData weaponData;
        [SerializeField] private Transform weaponHolder;
        [SerializeField] private Transform fireballToSpawn;
       

        public EnemyDamager damager;
        
        private IObjectResolver _objectResolver;
        private UIController _uiController;
        private float _spawnCounter,_timeBetweenSpawn;

        [Inject]
        public void Construct(IObjectResolver objectResolver,UIController uiController)
        {
            _objectResolver = objectResolver;
            _uiController = uiController;
        }
        
        private void Start()
        {
            _spawnCounter = _timeBetweenSpawn;
        }

        private void Update()
        {
            SetStats();
            OrbitingWeapon();
            WeaponActive();
        }

        private void OrbitingWeapon()
        {
            weaponHolder.rotation = Quaternion.Euler(weaponHolder.rotation.x, weaponHolder.rotation.y,
                weaponHolder.rotation.eulerAngles.z + (weaponData.rotateSpeed * Time.deltaTime * weaponStats[weaponLevel].speed));
        }
        
        

        private void WeaponActive()
        {
            _spawnCounter -= Time.deltaTime;

            if (_spawnCounter <= 0)
            {
                _spawnCounter = _timeBetweenSpawn;

                for (var i = 0; i < weaponStats[weaponLevel].amount; i++)
                {
                    var rotate = (360f / weaponStats[weaponLevel].amount) * i;
                    
                    _objectResolver.Instantiate(fireballToSpawn, fireballToSpawn.position, Quaternion.Euler(0f,0f,rotate),
                        weaponHolder).gameObject.SetActive(true);
                    
                    SFXManager.instance.PlaySfxPitched(8);
                }
            }
        }

        private void SetStats()
        {
            weaponData.damage = weaponStats[weaponLevel].damage;
            transform.localScale = Vector3.one * weaponStats[weaponLevel].range;
            _timeBetweenSpawn = weaponStats[weaponLevel].timeBetweenAttacks;
            damager.weaponLifeTime = weaponStats[weaponLevel].duration;
        }
    }
}