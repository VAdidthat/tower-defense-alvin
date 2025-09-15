using Actors;
using Mimi.Math.Utils;
using MyPooler;
using Sirenix.OdinInspector;
using Stats;
using Statuses;
using UnityEngine;

namespace Alvin.TowerDefense.Combat
{
    public class CannonTravelState : ActorState, IPooledObject
    {
        [ShowInInspector, ReadOnly] public Actor Target { set; get; }
        private float damage;
        private float resistance;
        [SerializeField] private BaseStatus[] statuses;
        [SerializeField] private float travelTime = 1.5f;   
        [SerializeField] private float arcHeight = 3f;  
        
        private Vector3 startPos;
        private float elapsedTime;

        void Start()
        {
            startPos = transform.position;
        }
        void HitTarget(Actor target, float chance)
        {
            // On hit effect
            foreach (BaseStatus status in this.statuses)
            {
                if (chance <= status.Chance)
                {
                    target.StatusEngine.AddStatus(status.gameObject);
                }
            }
            
            // take damage
            damage = Owner.StatCollection.GetCurrentValue(StatId.Damage);
            resistance = target.StatCollection.GetCurrentValue(StatId.Armor);
            float damageTaken = Attack.DealDamage(damage, resistance);
            target.Health.Decrease(damageTaken);

            // remove bullet
            DiscardToPool();
        }
        void Update()
        {
            if (Target == null)
            {
                DiscardToPool();
                return;
            }

            elapsedTime += Time.deltaTime;
            float t = elapsedTime / travelTime;
            Vector3 endPos = Target.Position;
            Vector3 currentPos = Parabola(startPos, endPos, arcHeight, t);
            transform.position = currentPos;
                
            if (t < 1f)
            {
                Vector3 nextPos = Parabola(startPos, endPos, arcHeight, t + 0.01f);
                Vector3 dir = (nextPos - currentPos).normalized;
                if (dir.sqrMagnitude > 0.001f)
                    transform.rotation = Quaternion.LookRotation(dir);
            }
                
            if (t >= 1f)
            {
                float chanceRoll = Random.Range(0f, 1f);
                foreach (Actor enemy in Owner.Vision.GetTargetInRange())
                {
                    HitTarget(enemy, chanceRoll);
                }
            }
        }

        Vector3 Parabola(Vector3 start, Vector3 end, float height, float t)
        {
            Vector3 mid = Vector3.Lerp(start, end, t);
            float parabola = 4 * height * t * (1 - t);
            mid.y += parabola;
            return mid;
        }

        public void OnRequestedFromPool()
        {
            
        }

        public void DiscardToPool()
        {
            ObjectPooler.Instance.ReturnToPool(gameObject.tag ,this.gameObject);
        }
    }
}