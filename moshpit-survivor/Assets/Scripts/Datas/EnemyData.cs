using UnityEngine;

namespace Datas
{
    [CreateAssetMenu(fileName = "Create", menuName = "Datas/EnemyData", order = 0)]
    public class EnemyData : ScriptableObject
    {
        [Header("----- ENEMY STAT SETTINGS -----")]
        public float health;
        public float moveSpeed;
        public float attackDamage;
        
        [Header("----- EXPERIENCE SETTINGS -----")]
        public int experience;
        
        [Header("----- COIN SETTINGS -----")]
        public int coinValue;
        public float coinDropRate;
    }
}