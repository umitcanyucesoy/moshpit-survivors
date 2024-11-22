using System;
using System.Collections.Generic;
using PlayerScripts;
using UnityEngine;
using VContainer;

namespace Weapons
{
    [Serializable]
    public class WeaponStats
    {
        public float speed, damage, range, timeBetweenAttacks, amount, duration;
        public string upgradeText;
    }
    
    public class WeaponService : MonoBehaviour
    {
        [Header("----- WEAPON CONTROLLER ------")]
        public List<WeaponStats> weaponStats;
        public int weaponLevel;
        public Sprite icon;


        private PlayerController _playerController;
        
        [Inject]
        public void Construct(PlayerController playerController)
        {
            _playerController = playerController;
        }
        
        public void WeaponStatsChange()
        {
            if (weaponLevel < weaponStats.Count -1)
            {
                weaponLevel++;

                if (weaponLevel >= weaponStats.Count -1)
                {
                    _playerController.fullyLevelledWeapons.Add(this);
                    _playerController.assignedWeapons.Remove(this);
                }
            }
        }
    }
}