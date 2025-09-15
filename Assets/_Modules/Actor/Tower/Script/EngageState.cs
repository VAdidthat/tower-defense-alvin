using Abilities;
using UnityEngine;

namespace Alvin.TowerDefense.Combat
{
    public class EngageState : ActorState
    {
        [SerializeField] private bool hasAbility;
        [SerializeField] private BaseAbility ability;

        protected override void OnEnter()
        {
            base.OnEnter();
        }

        protected override void OnExit()
        {
            base.OnExit();
        }

        protected override void OnUpdate()
        {
            base.OnUpdate();
            if (hasAbility)
            {
                this.ability.Execute(Owner);
            }
        }
    }
}