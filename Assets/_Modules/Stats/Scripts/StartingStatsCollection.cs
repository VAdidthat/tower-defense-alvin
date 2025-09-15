using UnityEngine;

namespace Stats
{
    [CreateAssetMenu(fileName = "StatsCollection", menuName = "StatsCollectionScripableObject/Stats")]
    public class StartingStatsCollection : ScriptableObject
    {
        public float Health = 100;
        public float Damage = 10;
        public float AttackSpeed = 1;
        public float MovementSpeed = 5;
        public float Armor = 10;
        public float MagicResistance = 10;
        public float Price = 100;
    }
}