namespace Datas
{
    [UnityEngine.CreateAssetMenu(fileName = "Create", menuName = "Datas/PlayerData", order = 0)]
    public class PlayerData : UnityEngine.ScriptableObject
    {
        public float health;
        public float moveSpeed;
        public float attackDamage;
        public float pickupRange;
    }
}