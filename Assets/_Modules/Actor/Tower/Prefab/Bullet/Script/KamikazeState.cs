using Actors;
using Mimi.Math.Utils;
using MyPooler;
using Sirenix.OdinInspector;
using Stats;
using Statuses;
using UnityEngine;

namespace Alvin.TowerDefense.Combat
{
    public class KamikazeState : ActorState, IPooledObject
    {
        [SerializeField] private float travelSpeed = 20.0f;
        private float damage;
        private float resistance;
        [SerializeField] private BaseStatus[] statuses;
        [ShowInInspector, ReadOnly] public Actor Target { set; get; }

        void HitTarget()
        {
            // On hit effect
            foreach (BaseStatus status in this.statuses)
            {
                if (RandomUtils.RandomBoolean(status.Chance))
                {
                    Target.StatusEngine.AddStatus(status.gameObject);
                }
            }

            // take damage
            damage = Owner.StatCollection.GetCurrentValue(StatId.Damage);
            resistance = Target.StatCollection.GetCurrentValue(StatId.Armor);
            float damageTaken = Attack.DealDamage(damage, resistance);
            Target.Health.Decrease(damageTaken);

            //remove bullet
            DiscardToPool();
        }

        void Update()
        {
            if (Target == null)
            {
                DiscardToPool();
                return;
            }

            Vector3 direction = (Target.transform.position - Machine.transform.position).normalized;
            float distanceThisFrame = travelSpeed * Time.deltaTime;
            if (Vector3.Distance(Machine.transform.position, Target.transform.position) <= distanceThisFrame)
            {
                HitTarget();
                return;
            }

            transform.Translate(direction * distanceThisFrame, Space.World);
            transform.rotation = Quaternion.LookRotation(direction) * Quaternion.Euler(-90, 0, 0);;
        }
        public void OnRequestedFromPool()
        {
            
        }

        public void DiscardToPool()
        {
            ObjectPooler.Instance.ReturnToPool(gameObject.tag,this.gameObject);
        }
    }
}