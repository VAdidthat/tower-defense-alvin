using UnityEngine;
using System.Collections;

namespace Abilities
{
    public class HealingAbility : BaseAbility
    {
        [SerializeField]  private float healingAmount;
        protected override IEnumerator OnExecute()
        {
            Owner.Health.Increase(healingAmount);
            yield break;
        }
    }
}