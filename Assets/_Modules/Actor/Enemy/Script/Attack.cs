using UnityEngine;
using Actors;
using Stats;

namespace Alvin.TowerDefense.Combat
{
    public class Attack : MonoBehaviour
    {

        public static float DealDamage(float damage, float resistance)
        {
            float damegeTaken = damage * (1-(resistance / (resistance + 10)));
            return  damegeTaken;
        }

    }   
}
