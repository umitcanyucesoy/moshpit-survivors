using UnityEngine;
using Weapons;

namespace Datas
{
    [CreateAssetMenu(fileName = "Create", menuName = "Datas/WeaponData", order = 0)]
    public class WeaponData : ScriptableObject
    {
        public string weaponName;
        public float rotateSpeed;
        public float moveSpeed;
        public float damage;
    }
}