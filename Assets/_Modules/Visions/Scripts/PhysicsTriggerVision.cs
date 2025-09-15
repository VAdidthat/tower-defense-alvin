using System;
using System.Collections.Generic;
using Actors;
using Sirenix.OdinInspector;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditorInternal;
#endif

namespace Visions
{
    public class PhysicsTriggerVision : BaseVision
    {
        [SerializeField, ValueDropdown("GetTags")]
        private string targetTag;

        [ShowInInspector, ReadOnly] private List<Actor> targetInRange = new List<Actor>(3);

        public override event Action<Actor> OnEnemyFound;
        
        public void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag(this.targetTag))
            {
                var enemy = other.GetComponent<Actor>();
                OnEnemyFound?.Invoke(enemy);
                this.targetInRange.Add(enemy);
            }
        }

        public void OnTriggerExit(Collider other)
        {
            if (other.gameObject.CompareTag(this.targetTag))
            {
                this.targetInRange.Remove(other.GetComponent<Actor>());
            }
        }

#if UNITY_EDITOR
        private string[] GetTags()
        {
            return InternalEditorUtility.tags;
        }
#endif


        public override bool TryGetClosestFrom(Actor center, out Actor target)
        {
            float minDistance = float.MaxValue;
            target = null;

            foreach (Actor enemy in this.targetInRange)
            {
                if (enemy == null) continue;
                float distance = Vector3.Distance(center.Position, enemy.Position);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    target = enemy;
                }
            }
            return target != null;
        }

        public override List<Actor> GetTargetInRange()
        {
            return this.targetInRange;
        }
    }
}