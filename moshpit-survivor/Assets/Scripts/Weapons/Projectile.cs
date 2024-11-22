using System;
using Datas;
using UnityEngine;

namespace Weapons
{
    public class Projectile : MonoBehaviour
    {

        [SerializeField] private WeaponData weaponData;


        private void Update()
        {
            transform.position += transform.up * (weaponData.moveSpeed * Time.deltaTime);
        }
    }
}