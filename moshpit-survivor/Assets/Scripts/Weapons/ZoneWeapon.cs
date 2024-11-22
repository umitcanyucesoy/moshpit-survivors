using System;
using Datas;
using EnemyScripts;
using SFX;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Weapons
{
    public class ZoneWeapon : WeaponService
    {
        [SerializeField] private WeaponData weaponData;
        [SerializeField] private Transform holder;
        [SerializeField] private Transform zoneHolder;
        
        public EnemyDamager damager;

        private IObjectResolver _resolver;
        private float _timeBetweenAttacks, _spawnCounter;
        

        [Inject]
        public void Construct(IObjectResolver resolver)
        {
            _resolver = resolver;
        }
        
        private void Start()
        {
            _spawnCounter = _timeBetweenAttacks;
        }

        private void Update()
        {
            SetStats();
            ZoneWeaponActive();
        }

        private void ZoneWeaponActive()
        {
            _spawnCounter -= Time.deltaTime;
            
            if (_spawnCounter <= 0)
            {
                _spawnCounter = _timeBetweenAttacks;
                
                _resolver.Instantiate(zoneHolder, zoneHolder.position, Quaternion.identity, holder).gameObject.SetActive(true);
                
                SFXManager.instance.PlaySfxPitched(10);
            }
        }

        private void SetStats()
        {
            weaponData.damage = weaponStats[weaponLevel].damage;
            damager.weaponLifeTime = weaponStats[weaponLevel].duration;
            _timeBetweenAttacks = weaponStats[weaponLevel].timeBetweenAttacks;
            damager.timeBetweenDamage = weaponStats[weaponLevel].speed;
            transform.localScale = Vector3.one * weaponStats[weaponLevel].range;
        }
    }
}