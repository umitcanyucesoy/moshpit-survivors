using UnityEngine;

namespace Datas
{
    [CreateAssetMenu(fileName = "Create", menuName = "Datas/EnemyData", order = 0)]
    public class EnemyData : ScriptableObject
    {
        public float health;
        public float moveSpeed;
        public float attackDamage;
        public int experience;
    }
}