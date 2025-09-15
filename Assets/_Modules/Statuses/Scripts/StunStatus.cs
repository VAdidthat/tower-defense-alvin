using System.Collections;
using System.Collections.Generic;
using Alvin.TowerDefense.Combat;
using TypeReferences;
using UnityEngine;

namespace Statuses
{
    public class StunStatus : BaseStatus
    {
        private ClassTypeReference startingState;
        protected override void OnInitialized()
        {
            startingState = Target.StartingState;
        }
        protected override void OnBegin()
        {
            Target.StateMachine.ChangeState<DeepSleepState>();
        }

        protected override void OnEnd()
        {
            Target.StateMachine.ChangeState(startingState);
        }
        
        protected override IEnumerator OnExecuting()
        {
            return null;
        }
    }
}