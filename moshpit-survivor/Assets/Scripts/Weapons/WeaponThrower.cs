using System;
using Datas;
using EnemyScripts;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Weapons
{
    public class WeaponThrower : WeaponService
    {

        [SerializeField] private WeaponData weaponData;
        [SerializeField] private EnemyDamager damager;

        private IObjectResolver _resolver;
        private float _throwCounter;
        
        
        [Inject]
        public void Construct(IObjectResolver resolver)
        {
            _resolver = resolver;
        }

        private void Start()
        {
            _throwCounter = 0f;
        }

        private void Update()
        {
            SetStats();
            ThrownWeaponActive();
        }

        private void ThrownWeaponActive()
        {
            _throwCounter -= Time.deltaTime;

            if (_throwCounter <= 0)
            {
                _throwCounter = weaponStats[weaponLevel].timeBetweenAttacks;

                for (var i = 0; i < weaponStats[weaponLevel].amount; i++)
                {
                    _resolver.Instantiate(damager, damager.transform.position, Quaternion.identity).gameObject.SetActive(true);
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