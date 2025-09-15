using Actors;
using Stats;
using UnityEngine;

namespace Alvin.TowerDefense.Combat
{
    public class AttackState : ActorState
    {
        private Actor attacker;
        private Actor castle;
        
        protected override void OnEnter()
        {
            attacker = Machine.GetComponent<Actor>();
            castle = GameObject.FindGameObjectWithTag("Castle").GetComponent<Actor>();
            AttackAndDead(attacker, castle);
        }

        public void OnHitEffect()
        {
            // hieu ung don danh
        }

        public void AttackAndDead(Actor attacker, Actor target)
        {
            float damage = attacker.StatCollection.GetCurrentValue(StatId.Damage);
            target.gameObject.GetComponent<Health>().Decrease(damage);
            Destroy(gameObject);
        }
    }
}