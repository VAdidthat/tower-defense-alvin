using Actors;
using TypeReferences;
using UnityEngine;

namespace Alvin.TowerDefense.Combat
{
    public class IdleState : ActorState
    {
        [SerializeField, ClassExtends(typeof(ActorState))]
        private ClassTypeReference enemyFoundState;

        protected override void OnEnter()
        {
            base.OnEnter();
            Owner.Vision.OnEnemyFound += EnemyFoundHandler;
        }

        protected override void OnExit()
        {
            base.OnExit();
            Owner.Vision.OnEnemyFound -= EnemyFoundHandler;
        }

        private void EnemyFoundHandler(Actor enemy)
        {
            Owner.StateMachine.ChangeState(this.enemyFoundState);
        }
    }
}